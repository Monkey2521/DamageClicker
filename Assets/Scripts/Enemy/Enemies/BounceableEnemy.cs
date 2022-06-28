using UnityEngine;

public sealed class BounceableEnemy : Enemy
{
    [SerializeField] float _bounceForce;

    public override void Move()
    {
        base.Move();


    }
}
