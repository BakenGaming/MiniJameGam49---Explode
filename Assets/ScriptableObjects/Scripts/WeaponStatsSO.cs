using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Stats")]
public class WeaponStatsSO : ScriptableObject
{
    public float fireRate;
    public int damage;
    public float projectileSpeed;
    public float lifeTime;
}
