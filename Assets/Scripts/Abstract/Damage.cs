using UnityEngine;

public abstract class Damage : ScriptableObject
{
    protected Damage _additionalDamage;

    [Header("Debug settings")]
    protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected string _name;
    [SerializeField][Range(0.1f, 100f)] protected float _instantDamageValue;

    public string Name => _name;
    public float DamageValue => _instantDamageValue;

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
        if (_isDebug) Debug.Log("Deal " + _name + ": " + _instantDamageValue);

        target.TakeDamage(this);
        MakeDamageEffect(target);

        if (_additionalDamage != null)
            _additionalDamage.MakeDamage(target);
    }

    protected abstract void MakeDamageEffect(IDamageable target);
}
