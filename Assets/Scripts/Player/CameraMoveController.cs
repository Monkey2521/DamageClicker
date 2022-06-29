using System.Collections.Generic;
using UnityEngine;

public sealed class CameraMoveController : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] bool _isDebug;

    [Header("Settings")]
    [SerializeField] float _slideSpeed;
    Touch _touch;

    void Awake()
    {

    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            Debug.Log(_touch);
        }
    }
}
