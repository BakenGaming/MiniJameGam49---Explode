using UnityEngine;

public interface IEnemyHandler
{    
    public abstract void Initialize(EnemySO _e);
    public abstract StatSystem GetStatSystem();
}
