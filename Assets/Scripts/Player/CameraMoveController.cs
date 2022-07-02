using System.Collections.Generic;
using UnityEngine;

public sealed class CameraMoveController : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private float _slideSpeed;
    private Touch _touch;

    private void Awake()
    {

    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            Debug.Log(_touch);
        }
    }
}
