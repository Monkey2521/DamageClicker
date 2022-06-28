using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IMoveable, IPoolable
{
    [Header("Debug settings")]
    protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected string _name;
    [SerializeField] protected EnemyStats _stats;
    [SerializeField] protected Rigidbody _rigidbody;

    public string Name => _name;
    public float HP => _stats.HP;
    public float MaxHP => _stats.MaxHP;
    public float Speed => _stats.Speed;

    public ObjectPool Pool { get; set; }

    protected Vector3 _targetPosition;
    public bool OnTarget 
    {
        get
        {
            Vector3 delta = transform.position - _targetPosition;
            return delta.x <= 0.2f && delta.z <= 0.2f;
        }
    }

    public virtual void Init(float difficultyMultiplier)
    {
        _stats.Init(difficultyMultiplier);
    }

    public virtual void Move()
    {
        _targetPosition = new Vector3(_targetPosition.x, transform.position.y, _targetPosition.z);
        transform.LookAt(_targetPosition);

        Vector3 velocity = transform.TransformDirection(Vector3.forward) * Speed;
        
        _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
    }

    public void SetTargetPosition(Vector3 position)
    {
        _targetPosition = position;
    }

    public void TakeDamage(Damage damage)
    {
        _stats.HP -= damage.InstantDamageValue;

        if (HP <= 0)
        {
            ReturnToPool();
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