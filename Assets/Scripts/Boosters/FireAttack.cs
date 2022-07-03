using UnityEngine;

[CreateAssetMenu(menuName = "Boosters/Fire attack", fileName = "New fire attack")]
public sealed class FireAttack : Booster
{
    [SerializeField] private FireDamage _damage;

    [Header("Fire damage settings")]
    [SerializeField][Range(0.01f, 20f)] private float _fireDamagePerSecond;
    [SerializeField][Range(0.01f, 10f)] private float _burningTime;

    public float FireDamagePerSecond => _fireDamagePerSecond;
    public float BurningTime => _burningTime;

    public override void MakeEffect()
    {
        EventBus.Publish<IFireAttackHandler>(handler => handler.OnFireAttack(_damage));
    }
}
