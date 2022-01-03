using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;

    [SerializeField]
    private float _maxDistance = 10;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * _speed * Time.fixedDeltaTime);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}