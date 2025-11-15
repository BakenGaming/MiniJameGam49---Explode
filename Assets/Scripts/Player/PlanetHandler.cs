using System;
using UnityEngine;

public class PlanetHandler : MonoBehaviour
{
    #region Variables
    private static PlanetHandler _i;
    public static PlanetHandler i { get { return _i; } }
    public static event Action OnHealthValueChange;
    public static event Action OnEnergyValueChange;
    [SerializeField] private PlanetStatsSO planetStatsSO;

    private StatSystem _statSystem;
    private HealthSystem _healthSystem;
    private EnergySystem _energySystem;
    private int crystalCount;
    #endregion
    #region Initialize
    public void Initialize()
    {
        _i = this;
        SetupPlanet();
        CollectableObjectHandler.OnItemCollected += UpdateCollectedItemCount;
    }
    #endregion
    #region Get Functions
    public HealthSystem GetHealthSystem(){return _healthSystem;}
    public StatSystem GetStatSystem(){return _statSystem;}
    public EnergySystem GetEnergySystem(){return _energySystem;}
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
        _energySystem.GainEnergy(_amount);
        OnEnergyValueChange?.Invoke();
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
                break;
        }
    }
    #endregion
    #region Player Setup
    private void SetupPlanet()
    {
        _statSystem = new StatSystem(planetStatsSO);
        _energySystem = new EnergySystem(_statSystem.GetEnergy());
        _healthSystem = new HealthSystem(_statSystem.GetHealth());
    }
    #endregion
}
