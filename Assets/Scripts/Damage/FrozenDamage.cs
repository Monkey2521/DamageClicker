using UnityEngine;

[CreateAssetMenu(menuName = "Damage/Frozen damage", fileName = "New frozen damage")]
public class FrozenDamage : Damage
{
    [SerializeField][Range(0.01f, 1f)] float _speedSlowMultiplier;
    [SerializeField][Range(0.01f, 10f)] float _slowTime;

    protected override void MakeDamageEffect(IDamageable target)
    {

    }
}
