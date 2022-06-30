using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WorldBuilder : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] bool _isDebug;

    [Header("Settings")]
    [SerializeField] GameObject _groundPrefab;
    [SerializeField] GameObject _wallPrefab;
    [SerializeField][Range(2, 10)] int _groundBoxSize;

    [Tooltip("Coroutine wait time")]
    [SerializeField][Range(0.01f, 1f)] float _deltaGroundSpawnTime; 

    [Space(5)]
    [SerializeField] Transform _groundParent;

    List<GameObject> _ground = new List<GameObject>();
    List<GameObject> _walls = new List<GameObject>();

    readonly float GROUND_BOX_RADIUS = 5f;
    readonly float DELTA_GROUND_POSITION = 10f;
    readonly float EVEN_GROUND_POSITION = 5f;
    readonly float WALL_Y_POSITION = 1.5f;

    public static float MAX_SPAWN_POSITION { get; private set; }

    void Awake()
    {
        StartCoroutine(ResetWorld());
    }

    IEnumerator ResetWorld()
    {
        if (_ground.Count > 0)
        {
            foreach (GameObject ground in _ground)
                Destroy(ground);
            foreach (GameObject wall in _walls)
                Destroy(wall);

            _ground.Clear();
            _walls.Clear();
        }

        int posMultiplier = (_groundBoxSize - 1) / 2;
        Vector3 startPosition = new Vector3
            (
                (_groundBoxSize % 2 == 0 ? -EVEN_GROUND_POSITION : 0) - DELTA_GROUND_POSITION * posMultiplier,
                0f,
                (_groundBoxSize % 2 == 0 ? EVEN_GROUND_POSITION : 0) + DELTA_GROUND_POSITION * posMultiplier
            );

        MAX_SPAWN_POSITION = -(startPosition.x - GROUND_BOX_RADIUS * 0.5f);

        if (_isDebug) Debug.Log("Building ground...");

        for(int i = 0; i < _groundBoxSize; i++)
        {
            StartCoroutine(PlaceLine(i, startPosition));

            yield return new WaitForSeconds(_deltaGroundSpawnTime);
        }
    }

    IEnumerator PlaceLine(int index, Vector3 startPosition) // 
    {
        for (int j = 0; j < _groundBoxSize; j++)
        {
            Vector3 position = new Vector3
                       (
                           startPosition.x + DELTA_GROUND_POSITION * index,
                           0f,
                           startPosition.z - DELTA_GROUND_POSITION * j
                       );

            _ground.Add(Instantiate(_groundPrefab, position, Quaternion.identity, _groundParent));

            #region Wall placing
            if (index == 0 || index == _groundBoxSize - 1)
                _walls.Add(Instantiate(_wallPrefab, position + new Vector3
                    (
                        GROUND_BOX_RADIUS * (index == 0 ? -1 : 1),
                        WALL_Y_POSITION,
                        0f
                    ), Quaternion.identity, _groundParent));

            if (j == 0 || j == _groundBoxSize - 1)
                _walls.Add(Instantiate(_wallPrefab, position + new Vector3
                    (
                        0f,
                        WALL_Y_POSITION,
                        GROUND_BOX_RADIUS * (j == 0 ? 1 : -1)
                    ), Quaternion.Euler(0f, 90f, 0f), _groundParent));
            #endregion

            yield return new WaitForSeconds(_deltaGroundSpawnTime);
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();    
    }
}
