using UnityEngine;

public enum CollectableType
{
    spaceCrystal, energy
}
public interface ICollectable
{
    public abstract void InitializeCollectable(CollectableSO _c);
    public abstract void Collect();
    public abstract void SetTarget(Vector3 targetPosition);
}
