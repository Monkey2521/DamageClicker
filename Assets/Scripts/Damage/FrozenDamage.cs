using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Damage/Frozen damage", fileName = "New frozen damage")]
public sealed class FrozenDamage : Damage
{
    [SerializeField] private FrozenAttack _booster;
    [SerializeField][Range(0.01f, 1f)] private float _deltaTimeForDamage;
    [SerializeField] private ParticleSystem _frozenParticle;
    protected override void MakeDamageEffect(IDamageable target)
    {
        target.TakeDamage(_instantDamageValue);
        FreezeTarget(target);

        if (_isDebug) Debug.Log("Make FrozenAttack");
    }

    async private void FreezeTarget(IDamageable target)
    {
        ParticleSystem particle = Instantiate(_frozenParticle, target.GetTransform());

        Enemy enemy = target as Enemy;

        float sm = enemy.SpeedMultiplier;
        enemy.SpeedMultiplier = _booster.SpeedSlowMultiplier;

        for (int i = 0; i < (int)(_booster.SlowTime / _deltaTimeForDamage); i++)
        {
            if (target.GetTransform().gameObject.activeSelf)
            {
                target.TakeDamage(_instantDamageValue * _deltaTimeForDamage);

                await Task.Delay((int)(_deltaTimeForDamage * 1000));
            }
            else
            {
                if (_isDebug) Debug.Log("End FrozenAttack");
                break;
            }
        }

        enemy.SpeedMultiplier = sm;
        Destroy(particle);
    }
}
