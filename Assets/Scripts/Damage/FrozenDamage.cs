using UnityEngine;

[CreateAssetMenu(menuName = "Damage/Frozen damage", fileName = "New frozen damage")]
public sealed class FrozenDamage : AreaDamage
{
    [Header("Frozen damage settings")]
    [SerializeField][Range(0.01f, 1f)] private float _speedSlowMultiplier;
    [SerializeField][Range(0.01f, 10f)] private float _slowTime;

    protected override void MakeDamageEffect(IDamageable target)
    {

    }
}
