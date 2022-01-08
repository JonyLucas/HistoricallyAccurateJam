using Game.Player;
using UnityEngine;

namespace Game.Enviroment
{
    public class BackgroundMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1;

        [SerializeField]
        private float _xTranslation;

        private PlayerMovement _movementScript;

        private Vector2 _moveDirection;

        private void Start()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                _movementScript = player.GetComponent<PlayerMovement>();
            }
        }

        private void Update()
        {
            if (_movementScript != null && _movementScript.IsMoving)
            {
                _moveDirection = !_movementScript.IsFacingRight ? Vector2.right : Vector2.left;
                transform.Translate(_moveDirection * _speed * Time.deltaTime);
            }
        }

        private void OnBecameInvisible()
        {
            if (_movementScript != null)
            {
                var newPosition = transform.position;
                newPosition.x += _movementScript.IsFacingRight ? _xTranslation * 2 : -_xTranslation * 2;
                transform.position = newPosition;
            }
        }
    }
}