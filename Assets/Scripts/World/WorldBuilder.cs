using System.Collections.Generic;
using UnityEngine;

public sealed class WorldBuilder : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] bool _isDebug;

    [Header("Settings")]
    [SerializeField] GameObject _groundPrefab;
    [SerializeField] GameObject _wallPrefab;
    [SerializeField][Range(1, 50)] int _groundBoxSize;

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
        ResetWorld();
    }

    [ContextMenu("Reset world")]
    async void ResetWorld()
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
            for (int j = 0; j < _groundBoxSize; j++)
            {
                Vector3 position = new Vector3
                    (
                        startPosition.x + DELTA_GROUND_POSITION * i,
                        0f,
                        startPosition.z - DELTA_GROUND_POSITION * j
                    );

                _ground.Add(Instantiate(_groundPrefab, position, Quaternion.identity, _groundParent));

                await System.Threading.Tasks.Task.Delay(250);

                #region Wall placing
                if (i == 0)
                    _walls.Add(Instantiate(_wallPrefab, position + new Vector3
                        (
                            -GROUND_BOX_RADIUS,
                            WALL_Y_POSITION,
                            0f
                        ), Quaternion.identity, _groundParent));
                if (i == _groundBoxSize - 1)
                    _walls.Add(Instantiate(_wallPrefab, position + new Vector3
                        (
                            GROUND_BOX_RADIUS,
                            WALL_Y_POSITION,
                            0f
                        ), Quaternion.identity, _groundParent));

                if (j == 0)
                    _walls.Add(Instantiate(_wallPrefab, position + new Vector3
                        (
                            0f,
                            WALL_Y_POSITION,
                            GROUND_BOX_RADIUS
                        ), Quaternion.Euler(0f, 90f, 0f), _groundParent));
                if (j == _groundBoxSize - 1)
                    _walls.Add(Instantiate(_wallPrefab, position + new Vector3
                        (
                            0f,
                            WALL_Y_POSITION,
                            -GROUND_BOX_RADIUS
                        ), Quaternion.Euler(0f, 90f, 0f), _groundParent));
                #endregion
            }
    }
}