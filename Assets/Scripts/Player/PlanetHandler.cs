using System;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHandler : MonoBehaviour
{
    #region Variables
    private static PlanetHandler _i;
    public static PlanetHandler i { get { return _i; } }
    public static event Action OnHealthValueChange;
    //public static event Action OnEnergyValueChange;
    public static event Action OnCrystalValueChanged;
    public static event Action<WeaponSO> OnNewWeaponEquipped;
    [SerializeField] private PlanetStatsSO planetStatsSO;
    private GameObject shipGO;
    private Transform weaponHolder;
    private Transform firePoint;
    private List<WeaponSO> equippedWeapons;
    private StatSystem _statSystem;
    private HealthSystem _healthSystem;
    private PlayerModifiers _modifiers;
    //private EnergySystem _energySystem;
    private int crystalCount;
    #endregion
    #region Initialize
    public void Initialize()
    {
        _i = this;
        SetupPlanet();
        equippedWeapons = new List<WeaponSO>();
        CollectableObjectHandler.OnItemCollected += UpdateCollectedItemCount;
        SpawnShip();
    }

    private void SpawnShip()
    {
        shipGO = Instantiate(GameAssets.i.pfPlayerObject, transform.Find("ShipSpawnPoint"));
        shipGO.transform.parent = transform;
        weaponHolder = shipGO.transform.Find("Weapons");
        firePoint = shipGO.transform.Find("FirePoint");
        EquipNewWeapon(planetStatsSO.startingWeapon);
        shipGO.GetComponent<IInputHandler>().Initialize();
    }
    public void AddEventsForUpgrades()
    {
        ButtonHandler.OnModifierSelected += UpdateModifiers;
        ButtonHandler.OnWeaponSelected += EquipNewWeapon;
    }
    #endregion
    #region Get Functions
    public List<WeaponSO> GetActiveWeapons()
    {
        return equippedWeapons;
    }
    public HealthSystem GetHealthSystem(){return _healthSystem;}
    public StatSystem GetStatSystem(){return _statSystem;}
    //public EnergySystem GetEnergySystem(){return _energySystem;}
    public int GetCrystalCount(){return crystalCount;}
    public PlayerModifiers GetModifierSystem(){return _modifiers;}
    #endregion
    #region Handle Player Functions
    public void HandleDeath()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int _amount)
    {
        Debug.Log($"Taking {_amount} damage");
        _healthSystem.LoseHealth(_amount);
        OnHealthValueChange?.Invoke();
    }
    public void IncreaseEnergy(int _amount)
    {
        Debug.Log($"Gained {_amount} energy");
        //_energySystem.GainEnergy(_amount);
        //OnEnergyValueChange?.Invoke();
    }

    private void UpdateCollectedItemCount(CollectableSO _c)
    {
        switch(_c.type)
        {
            case CollectableType.energy:
                IncreaseEnergy(_c.itemValue);
                break;
            case CollectableType.spaceCrystal:
                crystalCount += _c.itemValue;
                OnCrystalValueChanged?.Invoke();
                break;
        }
    }
    #endregion
    #region Player Setup
    private void SetupPlanet()
    {
        _statSystem = new StatSystem(planetStatsSO);
        _modifiers = new PlayerModifiers();
        //_energySystem = new EnergySystem(_statSystem.GetEnergy());
        _healthSystem = new HealthSystem(_statSystem.GetHealth() + _modifiers.GetModifierValue(ModifierType.health));
    }
    private void EquipNewWeapon(WeaponSO _w)
    {
        GameObject newWeapon = Instantiate (GameAssets.i.pfWeapon, weaponHolder);
        newWeapon.GetComponent<WeaponHandler>().InitializeWeapon(_w, firePoint);
        shipGO.GetComponent<IAttackHandler>().Initialize(newWeapon.GetComponent<WeaponHandler>());
        equippedWeapons.Add(_w);
        OnNewWeaponEquipped?.Invoke(_w);
    }

    private void UpdateModifiers(ModifierSO _m)
    {
        _modifiers.UpdateModifier(_m);
    }
    #endregion
}
