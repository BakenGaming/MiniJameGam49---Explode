using UnityEngine;

public class Projectile : MonoBehaviour
{
    private WeaponStatsSO weaponStats;
    private bool isReady;
    private Rigidbody2D rb;
    private float lifeTimer;
    private GameObject target;
    private StatSystem _stats;
    private PlayerModifiers _mod;
    public void Initialize(WeaponSO _w, GameObject _t)
    {
        _stats = PlanetHandler.i.GetStatSystem();
        _mod = PlanetHandler.i.GetModifierSystem();

        if(_t == null || _t.gameObject.activeInHierarchy == false) { ExpireObject(); return;}
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = _w.weaponSprite;
        weaponStats = _w.weaponsStats;
        target = _t;
        rb = GetComponent<Rigidbody2D>();
        lifeTimer = weaponStats.lifeTime;
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
            float _weaponDamage = _stats.GetDamage() + _mod.GetModifierValue(ModifierType.damage);
            float _critChance = Random.Range(0,101);  
            if(_critChance >= (100 - _stats.GetCritChance() + _mod.GetModifierValue(ModifierType.critChance)))
            {
                _weaponDamage *= _stats.GetCritBonus() + _mod.GetModifierValue(ModifierType.critBonus);
                DamagePopup.Create(other.gameObject.transform.position, (int)_weaponDamage, true);
            }
            else DamagePopup.Create(other.gameObject.transform.position, (int)_weaponDamage, false);

            _handler.TakeDamage((int)_weaponDamage);
            ExpireObject();
        }
    }
}
