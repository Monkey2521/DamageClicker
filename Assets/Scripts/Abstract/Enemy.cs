using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Enemy : MonoBehaviour, IDamageable, IMoveable, IPoolable, IPointerClickHandler, IAreaCleanerHandler
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected string _name;
    [SerializeField] protected EnemyStats _stats;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Health _health;

    public string Name => _name;
    public float HP => _stats.HP;
    public float MaxHP => _stats.MaxHP;
    public float Speed => _stats.Speed;
    public int ScoreReward => _stats.ScoreReward;

    protected Vector3 _targetPosition;
    public bool OnTarget 
    {
        get
        {
            Vector3 delta = transform.position - _targetPosition;
            return delta.x <= 0.2f && delta.z <= 0.2f;
        }
    }

    public ObjectPool Pool { get; set; }

    [HideInInspector] public float SpeedMultiplier = 1f;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    public virtual void Init(float difficultyMultiplier)
    {
        _stats.Init(difficultyMultiplier);
        _health.Init(MaxHP);
    }

    public virtual void Move()
    {
        _targetPosition = new Vector3(_targetPosition.x, transform.position.y, _targetPosition.z);
        transform.LookAt(_targetPosition);

        Vector3 velocity = transform.TransformDirection(Vector3.forward) * Speed * SpeedMultiplier;
        
        _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
    }

    public void SetTargetPosition(Vector3 position)
    {
        _targetPosition = position;
    }

    public void TakeDamage(float damage)
    {
        _stats.HP -= damage;

        _health.UpdateHealth(HP);

        if (HP <= 0)
        {
            EventBus.Publish<IEnemyKilledHandler>(handler => handler.OnEnemyKilled(this));
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isDebug) Debug.Log("Click on " + Name);

        EventBus.Publish<IEnemyClickedHandler>(handler => handler.OnEnemyClicked(this));
    }

    public void OnAreaCleaned()
    {
        if (gameObject.activeSelf)
        {
            EventBus.Publish<IEnemyKilledHandler>(handler => handler.OnEnemyKilled(this));
            ReturnToPool();
        }
    }
}
