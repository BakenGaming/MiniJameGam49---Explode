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
    private float healthModifier;
    private float damageModifier;
    private float moveSpeedModifier;
    private float fireRateModifier;
    private float critChanceModifier;
    private float critBonusModifier;
    public PlayerModifiers()
    {
        healthModifier = 0f;
        damageModifier = 1f;
        moveSpeedModifier = 1f;
        fireRateModifier = 1f;
        critChanceModifier = 1f;
        critBonusModifier = 1f;
    }

    public void UpdateModifier(ModifierType _m, float _a)
    {
        switch (_m)
        {
            case ModifierType.health:
                healthModifier += _a;
                break;
            case ModifierType.damage:
                damageModifier += _a;
                break;
            case ModifierType.speed:
                moveSpeedModifier += _a;
                break;
            case ModifierType.fireRate:
                fireRateModifier += _a;
                break;
            case ModifierType.critChance:
                critChanceModifier += _a;
                break;
            case ModifierType.critBonus:
                critBonusModifier += _a;
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
