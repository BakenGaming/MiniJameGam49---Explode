using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
{
    public int health;
    public int damage;
    public float moveSpeed;
    public float fireRate;
    public float critChance;
    public float critBonus;
    public Brain[] brains;
}
