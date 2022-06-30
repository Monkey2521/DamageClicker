using UnityEngine;

public abstract class Damage : ScriptableObject
{
    protected Damage _additionalDamage;

    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected string _name;
    public string Name => _name;

    [SerializeField][Range(0.1f, 1000f)] protected float _instantDamageValue;
    public float InstantDamageValue => _instantDamageValue;

    public void Init()
    {

    }

    public void AddDamage(Damage damage)
    {
        if (_additionalDamage == null)
            _additionalDamage = damage;
        else
            _additionalDamage.AddDamage(damage);
    }

    public void MakeDamage(IDamageable target)
    { 
        target.TakeDamage(_instantDamageValue);
        MakeDamageEffect(target);

        if (_additionalDamage != null)
            _additionalDamage.MakeDamage(target);
    }

    protected abstract void MakeDamageEffect(IDamageable target);
}
