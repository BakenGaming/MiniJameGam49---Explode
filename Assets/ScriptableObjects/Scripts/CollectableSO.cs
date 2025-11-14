using UnityEngine;

[CreateAssetMenu(menuName = "Collectable")]
public class CollectableSO : ScriptableObject
{
    public string itemName;
    public int itemValue;
    public Sprite itemSprite;
    public CollectableType type;
}
