using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Player/PlayerSO")]
public class PlayerSOData : ScriptableObject
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotateSpeed = 500f;
    public float walkMultiplier = 0.2f;
    public float runMultiplier = 1f;

    [Header("Gravity Settings")]
    public float gravityCheckerRadius = 0.5f;
    public Vector3 gravityCheckerOffset = Vector3.zero;
    public LayerMask groundLayer;
}
