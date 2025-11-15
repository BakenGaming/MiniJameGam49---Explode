using System;
using UnityEngine;
public class WeaponHandler : MonoBehaviour
{
    private float weaponCDTimer;
    private WeaponSO weapon;
    private Transform weaponFirePoint;
    
    public void InitializeWeapon(WeaponSO _w, Transform _fp)
    {
        weapon = _w;
        weaponFirePoint = _fp;
    }
    public void FireWeapon(GameObject _t)
    {
        if(weaponCDTimer <= 0)
        {
            Projectile newProjectile = ObjectPooler.DequeueObject<Projectile>("Player Projectile");
            newProjectile.transform.position = weaponFirePoint.position;
            newProjectile.transform.rotation = weaponFirePoint.rotation;
            newProjectile.gameObject.SetActive(true);
            newProjectile.GetComponent<Projectile>().Initialize(weapon, _t);
            weaponCDTimer = weapon.weaponsStats.fireRate;
        }
    }
    void Update()
    {
        weaponCDTimer -= Time.deltaTime;
    }
}
