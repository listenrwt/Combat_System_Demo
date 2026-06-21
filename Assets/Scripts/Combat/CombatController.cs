using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    // Components
    private MeleeFighter _meleeFighter;
    private InputAction _attackAction;

    private void Start()
    {
        _meleeFighter = GetComponent<MeleeFighter>();
        _attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        if (_attackAction.WasPressedThisFrame()) _meleeFighter.TryToAttack();
    }
}
