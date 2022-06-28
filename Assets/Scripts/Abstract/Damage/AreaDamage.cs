using UnityEngine;

public abstract class AreaDamage : Damage
{
    [Header("Area damage settings")]
    [SerializeField] protected GameObject _damagePrefab;
    [SerializeField][Range(1, 5)] protected int _areaRange;
    [SerializeField][Range(0.01f, 10f)] protected float _damageLifetime;
}
