using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Detects collisions of bullets with the enemy instance. 
    Adds and removes enemy instances from the game manager.
*/

public class Enemy : MonoBehaviour
{

    [Header("Enemy Fields")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int life = 5;

    // prefab to show when the enemy is on fire (assign in inspector)
    [SerializeField] private GameObject fireVfxPrefab;

    private EnemyFSM fsm;

    // active status effects 
    private class StatusInstance
    {
        public string id;
        public float remaining;
        public float dps; // damage per second
        public GameObject vfxInstance; // optional spawned visual effect tied to this status
        public StatusInstance(string id, float duration, float dps, GameObject vfxInstance = null)
        {
            this.id = id;
            this.remaining = duration;
            this.dps = dps;
            this.vfxInstance = vfxInstance;
        }
    }
    private List<StatusInstance> activeStatuses = new List<StatusInstance>();

    // public helper to add a status effect
    public void AddStatus(string id, float duration, float dps)
    {
        GameObject spawnedVfx = null;

        // spawn VFX for fire status if a prefab is provided
        if (id == "fire" && fireVfxPrefab != null)
        {
            spawnedVfx = Instantiate(fireVfxPrefab, transform);
            spawnedVfx.transform.localPosition = Vector3.zero; // attach to enemy origin
            spawnedVfx.transform.localScale = Vector3.one; // ensure visible scale
            Debug.Log($"Spawned '{id}' VFX on {gameObject.name}"); // debug
        }
        activeStatuses.Add(new StatusInstance(id, duration, dps));
        activeStatuses.Add(new StatusInstance(id, duration, dps, spawnedVfx));
    }

    void Awake()
    {
        fsm = GetComponentInChildren<EnemyFSM>();
    }

    void Start()
    {
        GameManager.instance.addEnemy(this);
    }

    void Update()
    {
        if (activeStatuses.Count > 0)
        {
            float dt = Time.deltaTime;
            // iterate backwards to allow removal
            for (int i = activeStatuses.Count - 1; i >= 0; i--)
            {
                var s = activeStatuses[i];
                float damageThisFrame = s.dps * dt;
                if (damageThisFrame > 0f)
                {
                    TakeDamage((int) damageThisFrame);
                }
                s.remaining -= dt;
                if (s.remaining <= 0f)
                {
                    // destroy associated visual effect when the status ends
                    if (s.vfxInstance != null)
                    {
                        Destroy(s.vfxInstance);
                    }
                    activeStatuses.RemoveAt(i);
                }
            }
        }
    }
    
    // central damage function (handles death check)
    public void TakeDamage(int amount)
    {
        life -= amount;
        if (life <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // check the collision is from a player bullet
        if (other.gameObject.layer == bulletPrefab.layer)
        {
            // try to read damage from the bullet; fall back to 1 if component missing
            BulletDamage bd = other.gameObject.GetComponent<BulletDamage>();
            int damage = (bd != null) ? bd.damage : 1;

            TakeDamage((int) damage);

            // apply any effect components on the bullet
            foreach (var mb in other.gameObject.GetComponents<MonoBehaviour>())
            {
                if (mb is IApplyEffect effect)
                {
                    effect.ApplyTo(this);
                }
            }

            Destroy(other.gameObject); // destroy the bullet on hit

        }
        else if (other.gameObject.CompareTag("Checkpoint"))
        {
            fsm.updateCheckpoint();
        }
    }

    void OnDestroy() //later dont do this on destory make some event
    {
        GameManager.instance.removeEnemy(this);
    }

}
