using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    [SerializeField][Range(0.1f, 200f)] float _maxHP;
    float _HP;

    [SerializeField][Range(0.1f, 20f)] float _speed;
    
    public float HP => _HP;
    public float MaxHP => _maxHP;
    public float Speed => _speed;

    public void Init()
    {
        _HP = _maxHP;
    }
}
