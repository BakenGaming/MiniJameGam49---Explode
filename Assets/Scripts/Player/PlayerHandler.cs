using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour, IHandler
{
    #region Variables
    private static PlayerHandler _i;
    public static PlayerHandler i { get { return _i; } }
    public static event Action<WeaponSO> OnNewWeaponEquipped;
    [SerializeField] private PlayerStatsSO playerStatsSO;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform firePoint;

    private StatSystem _statSystem;
    private PlayerModifiers _modifiers;
    private List<WeaponSO> equippedWeapons;

    #endregion
    #region Initialize
    public void Initialize(WeaponSO _w)
    {
        _i = this;
        equippedWeapons = new List<WeaponSO>();
        SetupPlayer(_w);
    }

    #endregion

    #region Get Functions
    public StatSystem GetStatSystem()
    {
        return _statSystem;
    }

    public PlayerModifiers GetModifierSystem()
    {
        return _modifiers;
    }
    public List<WeaponSO> GetActiveWeapons()
    {
        return equippedWeapons;
    }

    #endregion

    #region Handle Player Functions
    private void EquipNewWeapon(WeaponSO _w)
    {
        GameObject newWeapon = Instantiate (GameAssets.i.pfWeapon, weaponHolder);
        newWeapon.GetComponent<WeaponHandler>().InitializeWeapon(_w, firePoint);
        GetComponent<IAttackHandler>().Initialize(newWeapon.GetComponent<WeaponHandler>());
        equippedWeapons.Add(_w);
        OnNewWeaponEquipped?.Invoke(_w);
    }
    #endregion

    #region Player Setup
    private void SetupPlayer(WeaponSO _w)
    {
        _statSystem = new StatSystem(playerStatsSO);
        _modifiers = new PlayerModifiers();
        GetComponent<IInputHandler>().Initialize();
        GameObject newWeapon = Instantiate (GameAssets.i.pfWeapon, weaponHolder);
        newWeapon.GetComponent<WeaponHandler>().InitializeWeapon(_w, firePoint);
        GetComponent<IAttackHandler>().Initialize(newWeapon.GetComponent<WeaponHandler>());
        equippedWeapons.Add(_w);
    }
    #endregion
}
