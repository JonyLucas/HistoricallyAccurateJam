using Game.ScriptableObjects.Events;
using Game.UI.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNPC : MonoBehaviour
{
    [SerializeField]
    private DialogEvent _event;

    [SerializeField]
    private ScriptableDialog _dialog;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _event.OnOcurred(_dialog);
            if (_animator != null)
            {
                _animator.SetTrigger("isTalking");
            }
        }
    }
}