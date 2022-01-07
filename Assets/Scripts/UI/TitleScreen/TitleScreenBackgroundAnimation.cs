using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenBackgroundAnimation : MonoBehaviour
{
    [SerializeField]
    private Vector3 _initialPosition;

    [SerializeField]
    private Vector3 _finalPosition;

    [SerializeField]
    private float _moveRate;

    private bool _isMoving = true;

    private void Start()
    {
        transform.position = _initialPosition;
        _isMoving = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isMoving)
        {
            transform.position = Vector3.Slerp(transform.position, _finalPosition, _moveRate);
        }

        if (Vector3.Distance(transform.position, _finalPosition) < 1f)
        {
            transform.position = _finalPosition;
            _isMoving = false;
        }
    }
}