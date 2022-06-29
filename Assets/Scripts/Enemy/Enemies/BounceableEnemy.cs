using UnityEngine;

public sealed class BounceableEnemy : Enemy
{
    [Header("Bounce settings")]
    [SerializeField] Animator _animator;
    [SerializeField] float _bounceForce;

    bool _onGround;
    bool _onCompression;

    public override void Init(float difficultyMultiplier)
    {
        base.Init(difficultyMultiplier);

        _onCompression = false;
        _onGround = false;
    }

    public override void Move()
    {
        if (!_onCompression) 
            base.Move();

        if (_onGround && !_onCompression && _rigidbody.velocity.y == 0) 
            _rigidbody.AddForce(Vector3.up * _bounceForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_isDebug) Debug.Log(_name + " on collision with " + collision.collider.name);

        if (!_onCompression && collision.collider.tag == Tags.Ground)
        {
            _onCompression = true;
            _animator.SetBool("Compressing", true);

            _onGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == Tags.Ground) _onGround = false;
    }

    public void DisableCompressing()
    {
        _onCompression = false;
        _animator.SetBool("Compressing", false);
    }
}
