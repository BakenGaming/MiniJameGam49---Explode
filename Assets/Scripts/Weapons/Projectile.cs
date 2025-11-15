using UnityEngine;

public class Projectile : MonoBehaviour
{
    private WeaponStatsSO weaponStats;
    private bool isReady;
    private Rigidbody2D rb;
    private float lifeTimer;
    private GameObject target;
    public void Initialize(WeaponStatsSO _w, GameObject _t)
    {
        if(_t == null || _t.gameObject.activeInHierarchy == false) { ExpireObject(); return;}
        weaponStats = _w;
        target = _t;
        rb = GetComponent<Rigidbody2D>();
        lifeTimer = _w.lifeTime;
        isReady = true;
    }

    void Update()
    {
        if(!isReady) return;
        
        if(target == null || target.gameObject.activeInHierarchy == false) { ExpireObject(); return;}

        Vector3 moveDir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle - 90f);
        transform.position += moveDir * weaponStats.projectileSpeed * Time.deltaTime;
        UpdateTimers();
    } 
    private void UpdateTimers()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0) ExpireObject();
    }

    private void ExpireObject()
    {
        ObjectPooler.EnqueueObject(this, "Player Projectile");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHandler _handler = other.gameObject.GetComponent<EnemyHandler>();
        if(_handler != null)
        {
            DamagePopup.Create(other.gameObject.transform.position, weaponStats.damage, false);
            _handler.TakeDamage(weaponStats.damage);
            ExpireObject();
        }
    }
}
