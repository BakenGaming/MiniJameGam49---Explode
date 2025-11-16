using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

public enum ModifierType
{
    health, damage, speed, fireRate, critChance, critBonus
}
public class PlayerModifiers
{
    private int healthModifier;
    private int damageModifier;
    private float moveSpeedModifier;
    private float fireRateModifier;
    private float critChanceModifier;
    private float critBonusModifier;
    public PlayerModifiers()
    {
        healthModifier = 0;
        damageModifier = 0;
        moveSpeedModifier = 0f;
        fireRateModifier = 0f;
        critChanceModifier = 0f;
        critBonusModifier = 0f;
    }

    public void UpdateModifier(ModifierSO _m)
    {
        switch (_m.type)
        {
            case ModifierType.health:
                healthModifier += (int)_m.healthModifier;
                break;
            case ModifierType.damage:
                damageModifier += (int)_m.damageModifier;
                break;
            case ModifierType.speed:
                moveSpeedModifier += _m.moveSpeedModifier;
                break;
            case ModifierType.fireRate:
                fireRateModifier += _m.fireRateModifier;
                break;
            case ModifierType.critChance:
                critChanceModifier += _m.critBonusModifier;
                break;
            case ModifierType.critBonus:
                critBonusModifier += _m.critChanceModifier;
                break;
        }
    }

    public float GetModifierValue(ModifierType _m)
    {
        switch (_m)
        {
            case ModifierType.health:
                return healthModifier;
            case ModifierType.damage:
                return damageModifier;
            case ModifierType.speed:
                return moveSpeedModifier;
            case ModifierType.fireRate:
                return fireRateModifier;
            case ModifierType.critChance:
                return critChanceModifier;
            case ModifierType.critBonus:
                return critBonusModifier;
        }
        return -99f;
    }
}
