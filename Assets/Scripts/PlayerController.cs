using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed = 500f;

    [Header("Gravity Setting")]
    [SerializeField] private float _gravityCheckerRadius = 0.5f;
    [SerializeField] private Vector3 _gravityCheckerOffset = Vector3.zero;
    [SerializeField] private LayerMask _groundLayer;
    private Vector3 GravityCheckerPos => transform.TransformPoint(_gravityCheckerOffset);
    private bool IsGround => Physics.CheckSphere(GravityCheckerPos, _gravityCheckerRadius, _groundLayer);
    private float _verticalVelocity = -0.55f;
    private const float DEFAULT_VERTIICAL_VELOCITY = -0.5f;

    // Components
    private CharacterController _characterController;
    private Animator _animator;
    private InputAction _moveAction;
    private CameraController _cameraController;

    // Properities
    private Quaternion _targetRotation;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _cameraController = Camera.main.GetComponent<CameraController>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Moving Velocity
        Vector2 moveActionVec = _moveAction.ReadValue<Vector2>();
        Vector3 moveDir = _cameraController.PlanarRotation * new Vector3(moveActionVec.x, 0f, moveActionVec.y);

        // Falling Velocity
        _verticalVelocity = IsGround ? DEFAULT_VERTIICAL_VELOCITY : _verticalVelocity + Physics.gravity.y * Time.deltaTime;

        Vector3 velocity = (moveDir * _moveSpeed + new Vector3(0f, _verticalVelocity, 0f)) * Time.deltaTime;
        _characterController.Move(velocity);

        if (moveActionVec.magnitude > 0) _targetRotation = Quaternion.LookRotation(moveDir); 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotateSpeed * Time.deltaTime);

        _animator.SetFloat("moveAmount", moveActionVec.magnitude, 0.2f, Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGround ? new Color(0f, 1f, 0f, 0.5f) : new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(GravityCheckerPos, _gravityCheckerRadius);
    }
}
