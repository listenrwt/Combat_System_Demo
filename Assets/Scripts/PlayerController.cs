using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed = 500f;

    private InputAction _moveAction;
    private CameraController _cameraController;

    private Quaternion _targetRotation;

    private Animator _animator;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _cameraController = Camera.main.GetComponent<CameraController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 moveActionVec = _moveAction.ReadValue<Vector2>();
        Vector3 moveDir = _cameraController.PlanarRotation * new Vector3(moveActionVec.x, 0, moveActionVec.y).normalized;

        if(moveActionVec.magnitude > 0)
        {
            transform.position += moveDir * _moveSpeed * Time.deltaTime;
            _targetRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotateSpeed * Time.deltaTime);

        _animator.SetFloat("moveAmount", moveActionVec.magnitude, 0.2f, Time.deltaTime);
    }
}
