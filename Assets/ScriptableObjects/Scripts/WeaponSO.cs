using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public Sprite weaponUISprite;
    public WeaponStatsSO weaponsStats;
}
