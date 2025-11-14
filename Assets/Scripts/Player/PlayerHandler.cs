using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHandler : MonoBehaviour, IHandler
{
    #region Variables
    private static PlayerHandler _i;
    public static PlayerHandler i { get { return _i; } }
    [SerializeField] private PlayerStatsSO playerStatsSO;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform firePoint;

    private StatSystem _statSystem;
    private PlayerModifiers _modifiers;

    #endregion
    #region Initialize
    public void Initialize(WeaponSO _w)
    {
        _i = this;
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

    #endregion

    #region Handle Player Functions

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
    }
    #endregion
}
