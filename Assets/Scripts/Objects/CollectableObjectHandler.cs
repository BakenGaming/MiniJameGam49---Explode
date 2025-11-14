using UnityEngine;

public class CollectableObjectHandler : MonoBehaviour, ICollectable
{
    private CollectableSO collectable;
    private int value;
    private float actualAttractSpeed = 50f;
    private Vector3 targetPosition;
    private bool playerFound;
    private Rigidbody2D itemRB;

    public void InitializeCollectable(CollectableSO _c)
    {
        collectable = _c;
        GetComponent<SpriteRenderer>().sprite = collectable.itemSprite;
        itemRB = GetComponent<Rigidbody2D>();
    }
    public void Collect()
    {
        Debug.Log($"{collectable.itemName} Collected");
        ObjectPooler.EnqueueObject(this, collectable.itemName);
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        playerFound = true;
    }

    private void FixedUpdate()
    {
        if (playerFound)
        {
            Vector3 targetDirection = (targetPosition - transform.position).normalized;
            itemRB.linearVelocity = new Vector2(targetDirection.x, targetDirection.y) * actualAttractSpeed;
        }
    }

}
