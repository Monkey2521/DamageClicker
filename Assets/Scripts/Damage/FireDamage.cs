using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Damage/Fire damage", fileName = "New fire damage")]
public sealed class FireDamage : Damage
{
    [SerializeField] private FireAttack _booster;
    [SerializeField][Range(0.01f, 1f)] private float _deltaTimeForDamage;

    protected override void MakeDamageEffect(IDamageable target)
    {
        target.TakeDamage(_instantDamageValue);
        BurnTarget(target);

        if (_isDebug) Debug.Log("Make FireAttack");
    }

    async private void BurnTarget(IDamageable target)
    {
        for (int i = 0; i < (int)(_booster.BurningTime / _deltaTimeForDamage); i++)
        {
            if (target.GetTransform().gameObject.activeSelf)
            {
                target.TakeDamage(_booster.FireDamagePerSecond * _deltaTimeForDamage);
                await Task.Delay((int)(_deltaTimeForDamage * 1000));
            }
            else
            {
                if (_isDebug) Debug.Log("End FireAttack");
                return;
            }
        }
    }
}
