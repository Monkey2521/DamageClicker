using UnityEngine;

[CreateAssetMenu(menuName = "Damage/Fire damage", fileName = "New fire damage")]
public sealed class FireDamage : AreaDamage
{
    [Header("Fire damage settings")]
    [SerializeField][Range(0.01f, 20f)] float _fireDamagePerSecond;
    [SerializeField][Range(0.01f, 10f)] float _burningTime;

    public float FireDamagePerSecond => _fireDamagePerSecond;
    public float BurningTime => _burningTime;

    protected override void MakeDamageEffect(IDamageable target)
    {
        
    }
}
