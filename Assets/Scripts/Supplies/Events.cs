using UnityEngine;
using UnityEngine.Events;

public sealed class Events : MonoBehaviour
{
    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;

    public UnityEvent<Enemy> OnEnemySpawned;
    public UnityEvent<Enemy> OnEnemyKilled;

    static Events _instance;
    public static Events GetInstance => _instance;
    
    void Awake()
    {
        if (_instance != null)
        {
            Debug.Log("Instance already exists!");
            Destroy(gameObject);
        }
        else
            _instance = this;
    }

    [ContextMenu("Start game")]
    public void StartGame()
    {
        OnGameStart?.Invoke();
    }
}
