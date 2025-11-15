using System.Collections.Generic;
using CodeMonkey.Utils;
using Unity.VisualScripting;
using UnityEngine;

public class AttackHandler : MonoBehaviour, IAttackHandler
{
    private List<WeaponHandler> equippedWeapons;
    private TargetLocater targetLocator;
    private Transform currentTarget;
    private bool isAttacking;    
    public void Initialize(WeaponHandler _w)
    {
        targetLocator = transform.Find("TargetLocator").GetComponent<TargetLocater>();
        equippedWeapons = new List<WeaponHandler>();
        EquipNewWeapon(_w);
        isAttacking = true;
    }

    void Update()
    {
        if(isAttacking) FireProjectile();
    }

    private void FireProjectile()
    {
        if(equippedWeapons == null) return;

        foreach(WeaponHandler weapon in equippedWeapons)
        {
            GameObject newTarget = targetLocator.FindTarget();
            weapon.FireWeapon(newTarget);
        }
    }
    private void EquipNewWeapon(WeaponHandler _weapon)
    {
        equippedWeapons.Add(_weapon);
    }
}
