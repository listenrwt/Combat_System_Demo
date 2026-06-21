using System.Collections;
using UnityEngine;

public class MeleeFighter : MonoBehaviour
{
    public bool InAction { get; private set; } = false;

    // Components
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void TryToAttack()
    {
        if (!InAction)
        {
            StartCoroutine(PerformAnimation("Slash"));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hitbox") && !InAction)
        {
            StartCoroutine(PerformAnimation("SwordImpact"));
        }
    }

    private IEnumerator PerformAnimation(string animationName)
    {
        InAction = true;
        _animator.CrossFade(animationName, 0.2f);

        yield return null; // Wait 1 frame for layer transition

        float duration = _animator.GetNextAnimatorStateInfo(1).length;
        yield return new WaitForSeconds(duration);

        InAction = false;
    }
}
