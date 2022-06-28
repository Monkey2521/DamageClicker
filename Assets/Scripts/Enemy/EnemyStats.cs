using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    [Range(0.1f, 200f)] public float MaxHP;
    [HideInInspector] public float HP;

    [SerializeField][Range(0.1f, 20f)] float _speed;
    public float Speed => _speed;

    public void Init()
    {
        HP = MaxHP;
    }
}
