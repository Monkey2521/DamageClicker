using UnityEngine;

[CreateAssetMenu(menuName = "Boosters/Frozen attack", fileName = "New frozen attack")]
public sealed class FrozenAttack : Booster
{
    [SerializeField] private FrozenDamage _damage;

    [Header("Frozen damage settings")]
    [SerializeField][Range(0.01f, 1f)] private float _speedSlowMultiplier;
    [SerializeField][Range(0.01f, 10f)] private float _slowTime;

    public float SpeedSlowMultiplier => _speedSlowMultiplier;
    public float SlowTime => _slowTime;

    public override void MakeEffect()
    {
        EventBus.Publish<IFrozenAttackHandler>(handler => handler.OnFrozenAttack(_damage));
    }
}
