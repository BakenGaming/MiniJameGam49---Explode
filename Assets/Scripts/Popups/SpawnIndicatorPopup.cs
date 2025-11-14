using UnityEngine;

public class SpawnIndicatorPopup : MonoBehaviour
{
    public static SpawnIndicatorPopup Create(Vector3 position)
    {
        Transform spawnIndicatorTransform = Instantiate(GameAssets.i.pfSpawnIndicator, position, Quaternion.identity);

        SpawnIndicatorPopup spawnPopup = spawnIndicatorTransform.GetComponent<SpawnIndicatorPopup>();
        spawnPopup.Setup();

        return spawnPopup;
    }

    private static int sortingOrder;
    private SpriteRenderer sr;
    private const float DISAPPEAR_TIMER_MAX = .5f;
    private float disappearTimer;
    private Color indicatorColor;

    private void Awake()
    {
        sr = transform.GetComponent<SpriteRenderer>();
    }
    public void Setup()
    {
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        sr.sortingOrder = sortingOrder;

    }

    private void Update()
    {
        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            // First half of the popup lifetime
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            // Second half of the popup lifetime
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            // Start disappearing
            float disappearSpeed = 1.5f;
            indicatorColor.a -= disappearSpeed * Time.deltaTime;
            sr.color = indicatorColor;
            if (indicatorColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
