using UnityEngine;
[System.Serializable]
public enum WeaponClass
{
    standard, seeker
}
[CreateAssetMenu(menuName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public Sprite weaponUISprite;
    public WeaponStatsSO weaponsStats;
    public WeaponClass weaponClass;
}
