using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private bool _invertXRotation = false;
    [SerializeField] private bool _invertYRotation = false;
    private int InvertXRotationVal => _invertXRotation ? -1 : 1; 
    private int InvertYRotationVal => _invertYRotation ? -1 : 1; 

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

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Get mouse rotations
        // vertical
        _rotationX += Mouse.current.delta.y.ReadValue() * _mouseSensitivity * InvertXRotationVal; 
        _rotationX = Mathf.Clamp(_rotationX, _minVerticalAngle, _maxVerticalAngle);
        // horizontal
        _rotationY += Mouse.current.delta.x.ReadValue() * _mouseSensitivity * InvertYRotationVal;
        
        Quaternion targetRotation = Quaternion.Euler(_rotationX, _rotationY, 0);

        // Get target position
        Vector3 targetPosition = _followngTarget.position + new Vector3(_framingOffset.x, _framingOffset.y, 0);

        // Move camera
        transform.position = targetPosition - targetRotation * new Vector3(0, 0, _targetDistance);
        transform.rotation = targetRotation;
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0f, _rotationY, 0f);
}
