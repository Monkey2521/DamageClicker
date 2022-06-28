using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IMoveable
{
    [Header("Debug settings")]
    protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected EnemyStats _stats;

    public float HP => _stats.HP;
    public float MaxHP => _stats.MaxHP;
    public float Speed => _stats.Speed;

    public void Move()
    {

    }

    public virtual void TakeDamage(Damage damage)
    {

    }

    public Transform GetTransform() => transform;
}
