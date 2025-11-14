using UnityEngine;

public class Projectile : MonoBehaviour
{
    private WeaponStatsSO weaponStats;
    private bool isReady;
    private Rigidbody2D rb;
    private float lifeTimer;
    public void Initialize(WeaponStatsSO _w)
    {
        weaponStats = _w;
        rb = GetComponent<Rigidbody2D>();
        lifeTimer = _w.lifeTime;
        isReady = true;
    }

    void Update()
    {
        if(!isReady) return;
        
        //rb.linearVelocity = Vector3.right * weaponStats.projectileSpeed * Time.deltaTime;
        transform.position += transform.up * weaponStats.projectileSpeed * Time.deltaTime;
        UpdateTimers();
    } 
    private void UpdateTimers()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0) HandleLifetimeExpire();
    }

    private void HandleLifetimeExpire()
    {
        ObjectPooler.EnqueueObject(this, "Player Projectile");
    }


}
