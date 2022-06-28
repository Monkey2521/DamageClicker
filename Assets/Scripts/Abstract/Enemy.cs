using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IMoveable, IPoolable
{
    [Header("Debug settings")]
    protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected string _name;
    [SerializeField] protected EnemyStats _stats;

    public string Name => _name;
    public float HP => _stats.HP;
    public float MaxHP => _stats.MaxHP;
    public float Speed => _stats.Speed;

    public ObjectPool Pool { get; set; }

    public virtual void Init()
    {
        _stats.Init();
    }

    public virtual void Move()
    {

    }

    public void TakeDamage(Damage damage)
    {
        _stats.HP -= damage.InstantDamageValue;

        if (HP <= 0)
        {

        }
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);

        Pool.ReturnToPool(this);
    }

    public void PullFromPool()
    {
        gameObject.SetActive(true);
    }

    public Transform GetTransform() => transform;
}
