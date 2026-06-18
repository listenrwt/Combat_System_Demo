using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private bool _invertX = false;
    [SerializeField] private bool _invertY = false;

    [Header("Target Position")]
    [SerializeField] private Transform _followngTarget;
    [SerializeField] private float _targetDistance = 5f;
    [SerializeField] private Vector2 _framingOffset = Vector2.zero;

    [Header("Target Rotation")]
    [SerializeField] private float _mouseSensitivity = 1f;
    [SerializeField] private float _minVerticalAngle = -10f;
    [SerializeField] private float _maxVerticalAngle = 45f;

    private float _rotationX = 0f;
    private float _rotationY = 0f;

    private int _invertXVal;
    private int _invertYVal;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Set inverted values
        _invertXVal = _invertX ? -1 : 1;
        _invertYVal = _invertY ? -1 : 1;

        // Get mouse rotations
        // vertical
        _rotationX += Mouse.current.delta.y.ReadValue() * _mouseSensitivity * _invertXVal; 
        _rotationX = Mathf.Clamp(_rotationX, _minVerticalAngle, _maxVerticalAngle);
        // horizontal
        _rotationY += Mouse.current.delta.x.ReadValue() * _mouseSensitivity * _invertYVal;
        
        Quaternion targetRotation = Quaternion.Euler(_rotationX, _rotationY, 0);

        // Get target position
        Vector3 targetPosition = _followngTarget.position + new Vector3(_framingOffset.x, _framingOffset.y, 0);

        // Move camera
        transform.position = targetPosition - targetRotation * new Vector3(0, 0, _targetDistance);
        transform.rotation = targetRotation;
    }
}
