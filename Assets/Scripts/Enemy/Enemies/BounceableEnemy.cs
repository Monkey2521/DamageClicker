using UnityEngine;

public sealed class BounceableEnemy : Enemy
{
    [Header("Bounce settings")]
    [SerializeField] LayerMask _groundMask;
    [SerializeField] float _rayDistance;
    [SerializeField] float _bounceForce;

    public override void Move()
    {
        base.Move();

        if (OnGround()) _rigidbody.AddForce(Vector3.up * _bounceForce, ForceMode.Impulse);
    }

    bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, _rayDistance, _groundMask);
    }
}
