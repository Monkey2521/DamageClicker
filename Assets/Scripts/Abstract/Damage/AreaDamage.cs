using UnityEngine;

public abstract class AreaDamage : Damage
{
    [Header("Area damage settings")]
    [SerializeField] protected GameObject _damagePrefab;
    [SerializeField][Range(1, 500)] protected int _areaRange;
    [SerializeField][Range(0f, 10f)] protected float _damageLifetime;
}
