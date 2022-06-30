using UnityEngine;

public interface IDamageable
{
    public float HP { get; }
    public float MaxHP { get; }

    public void TakeDamage(float damage);

    public Transform GetTransform();
}
