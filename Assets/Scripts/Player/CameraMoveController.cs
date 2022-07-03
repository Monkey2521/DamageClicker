using System.Collections.Generic;
using UnityEngine;

public sealed class CameraMoveController : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(0.1f, 10f)] private float _slideSpeed;
    [SerializeField] private Vector3 _defaultCameraPos;

    private Vector2 _tapPosition;
    private bool _isSwiping;
    private float _timer;
    [SerializeField][Range(0.05f, 0.2f)] private float _touchDelay;

    private bool _isMobile;

    private void Awake()
    {
        _isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        Touch touch = new Touch();

        if (_isMobile)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _tapPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    if (!_isSwiping)
                        _timer += Time.deltaTime;
                    if (_timer >= _touchDelay)
                        _isSwiping = true;
                }
                else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                _tapPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                if (!_isSwiping)
                    _timer += Time.deltaTime;
                if (_timer >= _touchDelay)
                    _isSwiping = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
        }

        if (_isSwiping)
        {
            Vector3 deltaPos;
            Vector2 mousePos;

            if (_isMobile)
            {
                mousePos = touch.position;
            }
            else
            {
                mousePos = Input.mousePosition;
            }

            deltaPos = new Vector3
                (
                    mousePos.x - _tapPosition.x,
                    0f,
                    mousePos.y - _tapPosition.y
                ).normalized * _slideSpeed;

            transform.position = new Vector3
                (
                    Mathf.Clamp(transform.position.x - deltaPos.x,
                        -WorldBuilder.MAX_SPAWN_POSITION, WorldBuilder.MAX_SPAWN_POSITION),
                    _defaultCameraPos.y,
                    Mathf.Clamp(transform.position.z - deltaPos.z,
                        -WorldBuilder.MAX_SPAWN_POSITION + _defaultCameraPos.z * 0.5f, 
                        WorldBuilder.MAX_SPAWN_POSITION + _defaultCameraPos.z * 0.5f)
                );

            _tapPosition = mousePos;
        }
    }

    private void ResetSwipe()
    {
        _isSwiping = false;
        _tapPosition = Vector3.zero;
        _timer = 0f;

        if (_isDebug) Debug.Log("Reset tap");
    }
}
