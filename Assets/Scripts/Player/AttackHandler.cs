using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour, IAttackHandler
{
    private List<WeaponHandler> equippedWeapons;
    private bool isAttacking;    
    public void Initialize(WeaponHandler _w)
    {
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
            weapon.FireWeapon();
        }
    }
    private void EquipNewWeapon(WeaponHandler _weapon)
    {
        equippedWeapons.Add(_weapon);
    }
}
