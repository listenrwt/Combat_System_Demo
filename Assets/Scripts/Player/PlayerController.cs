using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [field: SerializeField] public PlayerSOData SOdata;
    public PlayerData data;

    public CharacterController CharacterController { get; private set; }
    public Animator Animator { get; private set; }
    public CameraController CameraController { get; private set; }

    // Input Actions
    public InputAction MoveAction { get; private set; }
    public InputAction AttackAction { get; private set; }
    public InputAction WalkToggleAction { get; private set; }
    public InputAction SprintAction { get; private set; }

    // State Machines
    private PlayerMovementSM movementSM;
    private PlayerCombatSM combatSM;

    private void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        CameraController = Camera.main.GetComponent<CameraController>();

        MoveAction = InputSystem.actions.FindAction("Move");
        AttackAction = InputSystem.actions.FindAction("Attack");
        WalkToggleAction = InputSystem.actions.FindAction("WalkToggle");
        SprintAction = InputSystem.actions.FindAction("Sprint");

        movementSM = new PlayerMovementSM(this);
        combatSM = new PlayerCombatSM(this);

        data = new PlayerData();
    }

    private void Update()
    {
        UpdateGroundCheck();
        movementSM.Update();
        combatSM.Update();
    }

    // ── Movement API for states ─────────────────────────────────────────────

    /// <summary>
    /// Read input toggles (walk/sprint). Call once per grounded Update before Move.
    /// </summary>
    public void UpdateMovementToggles()
    {
        bool isGamepad = MoveAction.activeControl?.device is Gamepad;

        if (isGamepad)
        {
            data.movementData.enableRunning = false;
        }
        else
        {
            if (WalkToggleAction.triggered)
                data.movementData.enableRunning = !data.movementData.enableRunning;

            if (SprintAction.triggered)
                data.movementData.isSprinting = !data.movementData.isSprinting;
        }
    }

    /// <summary>
    /// Apply movement for this frame.
    /// </summary>
    /// <param name="multiplier">Speed multiplier supplied by the current state.</param>
    /// <param name="normalize">Whether to normalize the move direction (e.g. for sprint).</param>
    public void Move(float multiplier, bool normalize)
    {
        if (data.combatData.isAttacking)
        {
            data.movementData.moveInput = Vector2.zero;
            Animator.SetFloat("moveAmount", 0f, 0.2f, Time.deltaTime);
            return;
        }

        Vector2 moveInput = data.movementData.moveInput = MoveAction.ReadValue<Vector2>();

        Vector3 moveDir = CameraController.PlanarRotation * new Vector3(moveInput.x, 0f, moveInput.y);
        if (normalize)
            moveDir = moveDir.normalized;

        Vector3 velocity = (moveDir * SOdata.moveSpeed * multiplier
                           + new Vector3(0f, data.movementData.verticalVelocity, 0f))
                           * Time.deltaTime;
        CharacterController.Move(velocity);

        if (moveInput.magnitude > 0f)
            data.movementData.targetRotation = Quaternion.LookRotation(moveDir);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            data.movementData.targetRotation,
            SOdata.rotateSpeed * Time.deltaTime);

        float animAmount = normalize ? multiplier : moveInput.magnitude * multiplier;
        Animator.SetFloat("moveAmount", animAmount, 0.2f, Time.deltaTime);
    }

    // ── Private helpers ─────────────────────────────────────────────────────

    private void UpdateGroundCheck()
    {
        Vector3 checkerPos = transform.TransformPoint(SOdata.gravityCheckerOffset);
        data.movementData.isGround = Physics.CheckSphere(checkerPos, SOdata.gravityCheckerRadius, SOdata.groundLayer);

        if (data.movementData.isGround)
            data.movementData.verticalVelocity = -0.5f;
        else
            data.movementData.verticalVelocity += Physics.gravity.y * Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (SOdata == null) return;
        Vector3 pos = transform.TransformPoint(SOdata.gravityCheckerOffset);
        bool grounded = Physics.CheckSphere(pos, SOdata.gravityCheckerRadius, SOdata.groundLayer);
        Gizmos.color = grounded ? new Color(0f, 1f, 0f, 0.5f) : new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(pos, SOdata.gravityCheckerRadius);
    }
}
