using System.Collections;
using System.Linq;
using UnityEngine;

public class BreakableGroundBehaviour : MonoBehaviour
{
    private Collider2D _collider;
    private Animator _animator;
    private float _animationDuration;

    // Start is called before the first frame update
    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _animationDuration = _animator.runtimeAnimatorController
                .animationClips
                .FirstOrDefault(x => x.name == "ground_breaking").length;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(GroundBreakingCoroutine());
    }

    private IEnumerator GroundBreakingCoroutine()
    {
        _animator.SetTrigger("breakGround");
        yield return new WaitForSeconds(_animationDuration / 2);
        _collider.enabled = false;
    }
}