using UnityEngine;

public class EnemyThinker : MonoBehaviour
{
    private Brain[] brain;

    public void ActivateBrain(IEnemyHandler _handler)
    {
        brain = _handler.GetStatSystem().GetBrains();
        foreach (Brain _brain in brain)
            _brain.InitializeAI(GetComponent<EnemyHandler>());
    }
    private void LateUpdate() 
    {
        foreach (Brain _brain in brain)
            _brain.Think(this);    
    }
}
