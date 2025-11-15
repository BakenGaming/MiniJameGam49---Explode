using UnityEngine;

[CreateAssetMenu(menuName ="Modifier")]
public class ModifierSO : ScriptableObject
{
    public string description;
    public float healthModifier;
    public float damageModifier;
    public float moveSpeedModifier;
    public float fireRateModifier;
    public float critChanceModifier;
    public float critBonusModifier;
    public ModifierType type;
}
