using UnityEngine;

[CreateAssetMenu(menuName ="Brains/Target Brain")]
public class TargetBrain : Brain
{
    private Transform planetTransform;
    public override void InitializeAI(IEnemyHandler _handler)
    {
        planetTransform = GameManager.i.GetPlanetCenter();
    }
    public override void Think(EnemyThinker _thinker)
    {
        var targetMovement = _thinker.gameObject.GetComponent<EnemyMovementHandler>();
        if(targetMovement)
        {
            targetMovement.TargetMovement();
        }
    }
}
