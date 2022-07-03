using UnityEngine;

public abstract class Booster : ScriptableObject
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected string _name;
    public string Name => _name;
    public Sprite Icon => _icon;

    [SerializeField][Range(0f, 60f)] protected float _cooldown;
    public float Cooldown => _cooldown;
 
    public abstract void MakeEffect();
}
