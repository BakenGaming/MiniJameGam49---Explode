using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSystem
{
    private int health;
    private int energy;
    public int damage;
    private float moveSpeed;
    private float fireRate;
    private float critChance;
    private float critBonus;
    private Brain[] enemyBrains;


    public StatSystem (PlayerStatsSO _stats)
    {
        moveSpeed = _stats.moveSpeed;
        fireRate = _stats.fireRateBonus;
        critChance = _stats.critChance;
        critBonus = _stats.critBonus;
    }

    public StatSystem (EnemyStatsSO _stats)
    {
        health = _stats.health;
        damage = _stats.damage;
        moveSpeed = _stats.moveSpeed;
        fireRate = _stats.fireRate;
        critChance = _stats.critChance;
        critBonus = _stats.critBonus;
        enemyBrains = _stats.brains;
    }

    public StatSystem (PlanetStatsSO _stats)
    {
        health = _stats.maxHealth;
        energy = _stats.maxEnergy;

    }

    public int GetHealth (){return health;}
    public float GetDamage(){return damage;}
    public int GetEnergy (){return energy;}
    public float GetMoveSpeed(){return moveSpeed;}
    public float GetFireRate(){return fireRate;}
    public float GetCritChance(){return critChance;}
    public float GetCritBonus(){return critBonus;}
    public Brain[] GetBrains(){return enemyBrains;}
}
