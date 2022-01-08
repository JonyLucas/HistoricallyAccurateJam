using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanishEnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    // Start is called before the first frame update
    private void Start()
    {
    }

    //private void FixedUpdate()
    //{
    //    if (_isVisible)
    //    {
    //        _distance = transform.position - _player.transform.position;
    //        var yDistance = Mathf.Floor(_distance.y);

    //        if (Mathf.Abs(yDistance) < _minYDistance)
    //        {
    //            FacePlayer();
    //            _canShoot = true;
    //        }
    //        else
    //        {
    //            _canShoot = false;
    //        }
    //    }
    //}

    //private void FacePlayer()
    //{
    //    if (_distance.x > 0)
    //    {
    //        _isFacingLeft = true;
    //    }
    //    else
    //    {
    //        _isFacingLeft = false;
    //    }

    //    _renderer.flipX = _isFacingLeft;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Damage();
        }
    }
}