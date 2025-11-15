using System.Collections.Generic;
using UnityEngine;

public class TargetLocater : MonoBehaviour
{
    public List<EnemyHandler> possibleTargets;
    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHandler enemy = other.gameObject.GetComponent<EnemyHandler>();
        if(enemy != null) possibleTargets.Add(enemy);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        EnemyHandler enemy = other.gameObject.GetComponent<EnemyHandler>();
        if(enemy != null) possibleTargets.Remove(enemy);
    }

    public GameObject FindTarget()
    {
        float distancetoClosestEnemy = Mathf.Infinity;
        EnemyHandler closestEnemy = null;
        
        foreach(EnemyHandler currentEnemy in possibleTargets)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if(distanceToEnemy < distancetoClosestEnemy)
            {
                distancetoClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        
        if(closestEnemy != null) return closestEnemy.gameObject;
        else return null;
    }
}
