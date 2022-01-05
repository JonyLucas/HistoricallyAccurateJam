using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;

    private Animator _animator;
    private PlayerMovement _moveScript;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _moveScript = GetComponent<PlayerMovement>();
    }

    public void Damage()
    {
        lives--;
        if (lives <= 0)
        {
            _animator.SetTrigger("death");
            _moveScript.enabled = false;
            //Destroy(gameObject);
        }
    }
}