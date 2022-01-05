using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;

    public Vector2 MoveDirection { get; set; } = Vector2.left;

    private void FixedUpdate()
    {
        transform.Translate(MoveDirection * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Damage();
            gameObject.SetActive(false);
        }
    }
}