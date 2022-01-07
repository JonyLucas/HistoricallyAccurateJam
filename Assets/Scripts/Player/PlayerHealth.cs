using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int _lives = 4;

    [SerializeField] 
    private int _maxLives = 4;

    private Animator _animator;
    private PlayerMovement _moveScript;

    private void Awake()
    {
        _lives = _maxLives;
        _animator = GetComponent<Animator>();
        _moveScript = GetComponent<PlayerMovement>();
    }

    public void Damage()
    {
        _lives--;
        if (_lives <= 0)
        {
            _animator.SetTrigger("death");
            _moveScript.enabled = false;
            //Destroy(gameObject);
        }
    }

    public void Heal(CollectablesType collectablesType)
    {
        if (_lives < _maxLives)
        {
            if(collectablesType == CollectablesType.Pineapple)
            {
                _lives = _maxLives;
            }
            else if(collectablesType == CollectablesType.Banana)
            {
                _lives++;
            }
        }
    }
}