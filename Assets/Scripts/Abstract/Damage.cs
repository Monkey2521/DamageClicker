using UnityEngine;

public abstract class Damage : ScriptableObject
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected string _name;
    public string Name => _name;

    [SerializeField][Range(0.1f, 100f)] protected float _instantDamageValue;
    public float InstantDamageValue => _instantDamageValue;

    public void MakeDamage(IDamageable target)
    { 
        target.TakeDamage(_instantDamageValue);
        MakeDamageEffect(target);
    }

    protected virtual void MakeDamageEffect(IDamageable target)
    {
        if (_isDebug) Debug.Log("Make " + _name + " effect");
    }
}
