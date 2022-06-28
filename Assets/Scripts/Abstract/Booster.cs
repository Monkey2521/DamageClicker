using UnityEngine;

public class Booster : ScriptableObject
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected string _name;
    public string Name => _name;
}
