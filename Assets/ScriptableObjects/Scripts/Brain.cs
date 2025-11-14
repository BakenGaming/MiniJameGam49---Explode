using UnityEngine;

public abstract class Brain : ScriptableObject
{
    public abstract void InitializeAI(IEnemyHandler _handler);
    public abstract void Think(EnemyThinker _thinker);
}
