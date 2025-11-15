using System;
using System.Collections;
using UnityEngine;

public class CollectableObjectHandler : MonoBehaviour, ICollectable
{
    public static event Action<CollectableSO> OnItemCollected;
    public static event Action<CollectableObjectHandler> OnCollectableCreated;
    public static event Action<CollectableObjectHandler> OnCollectableRemoved;
    private CollectableSO collectable;
    private int value;
    private float actualAttractSpeed = 50f;
    private Vector3 targetPosition;
    private bool playerFound;
    private Rigidbody2D itemRB;

    public void InitializeCollectable(CollectableSO _c)
    {
        collectable = _c;
        OnCollectableCreated?.Invoke(this);
        GetComponent<SpriteRenderer>().sprite = collectable.itemSprite;
        itemRB = GetComponent<Rigidbody2D>();
        float dropForce = 25f;
        Vector2 dropDirection = new Vector2(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f,1f));
        itemRB.AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        StartCoroutine(ImpulseStop());
        StartCoroutine(AttractDelay());
    }
    IEnumerator ImpulseStop()
    {
        yield return new WaitForSeconds(.2f);
        itemRB.linearVelocity = Vector2.zero;
    }

    IEnumerator AttractDelay()
    {
        yield return new WaitForSeconds(.6f);
        SetTarget(GameManager.i.GetPlanetCenter().position);
    }
    public void Collect()
    {
        TextPopUp.Create(UIController.i.GetCrystalUILocation().position, $"+{collectable.itemValue}");
        OnItemCollected?.Invoke(collectable);
        OnCollectableRemoved?.Invoke(this);
        ObjectPooler.EnqueueObject(this, "Collectable");
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
