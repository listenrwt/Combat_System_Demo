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
