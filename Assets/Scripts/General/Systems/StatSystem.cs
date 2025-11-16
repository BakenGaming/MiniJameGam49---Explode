using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSystem
{
    private int health;
    public int damage;
    private float moveSpeed;
    private float fireRate;
    private float critChance;
    private float critBonus;
    private Brain[] enemyBrains;

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
        damage = _stats.baseDamage;
        moveSpeed = _stats.baseMoveSpeed;
        fireRate = _stats.basefireRate;
        critChance = _stats.baseCritChance;
        critBonus = _stats.baseCritDamage;

    }

    public int GetHealth (){return health;}
    public int GetDamage(){return damage;}
    public float GetMoveSpeed(){return moveSpeed;}
    public float GetFireRate(){return fireRate;}
    public float GetCritChance(){return critChance;}
    public float GetCritBonus(){return critBonus;}
    public Brain[] GetBrains(){return enemyBrains;}
}
