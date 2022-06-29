using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    [SerializeField][Range(0.1f, 200f)] float _maxHP;
    [HideInInspector] public float MaxHP;
    [HideInInspector] public float HP;

    [SerializeField][Range(0.1f, 20f)] float _speed;
    [HideInInspector] public float Speed;
    
    [SerializeField][Range(1, 20)] int _scoreReward;
    public int ScoreReward => _scoreReward;

    public void Init(float difficultyMultiplier)
    {
        MaxHP = _maxHP * difficultyMultiplier;
        Speed = _speed * difficultyMultiplier;

        HP = MaxHP;
    }
}
