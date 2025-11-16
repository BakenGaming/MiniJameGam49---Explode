using UnityEngine;

[CreateAssetMenu(menuName ="Stats")]
public class PlanetStatsSO : ScriptableObject
{
    public int maxHealth;
    public int baseDamage;
    public float baseMoveSpeed;
    public float basefireRate;
    public float baseCritChance;
    public float baseCritDamage;
    public WeaponSO startingWeapon;
}
