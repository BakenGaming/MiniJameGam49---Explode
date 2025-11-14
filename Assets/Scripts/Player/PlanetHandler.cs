using UnityEngine;

public class PlanetHandler : MonoBehaviour
{
    #region Variables
    [SerializeField] private PlanetStatsSO planetStatsSO;

    private StatSystem _statSystem;
    private HealthSystem _healthSystem;

    #endregion
    #region Initialize
    public void Initialize()
    {
        SetupPlanet();
    }

    #endregion

    #region Get Functions
    public HealthSystem GetHealthSystem()
    {
        return _healthSystem;
    }

    public StatSystem GetStatSystem()
    {
        return _statSystem;
    }

    #endregion

    #region Handle Player Functions

    public void HandleDeath()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateHealth()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Player Setup
    private void SetupPlanet()
    {
        _statSystem = new StatSystem(planetStatsSO);
        _healthSystem = new HealthSystem(_statSystem.GetPlayerHealth());
    }
    #endregion
}
