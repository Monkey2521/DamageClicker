using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Damage/Frozen damage", fileName = "New frozen damage")]
public sealed class FrozenDamage : Damage
{
    [SerializeField] private FrozenAttack _booster;
    [SerializeField][Range(0.01f, 1f)] private float _deltaTimeForDamage;

    protected override void MakeDamageEffect(IDamageable target)
    {
        target.TakeDamage(_instantDamageValue);
        FreezeTarget(target);

        if (_isDebug) Debug.Log("Make FrozenAttack");
    }

    async private void FreezeTarget(IDamageable target)
    {
        Rigidbody rigidbody = target.GetTransform().GetComponent<Rigidbody>();
        for (int i = 0; i < (int)(_booster.SlowTime / _deltaTimeForDamage); i++)
        {
            if (target.GetTransform().gameObject.activeSelf)
            {
                target.TakeDamage(_instantDamageValue);

                if (rigidbody != null) rigidbody.velocity *= (_booster.SpeedSlowMultiplier);

                await Task.Delay((int)(_deltaTimeForDamage * 1000));
            }
            else
            {
                if (_isDebug) Debug.Log("End FrozenAttack");
                return;
            }
        }
    }
}
