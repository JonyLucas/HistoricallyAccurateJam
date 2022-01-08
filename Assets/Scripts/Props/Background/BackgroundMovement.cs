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
                var moveDirection = !_movementScript.IsFacingRight ? Vector2.right : Vector2.left;
                transform.Translate(moveDirection * _speed * Time.deltaTime);
                var playerDistance = _movementScript.transform.position - transform.position;
                if (Mathf.Abs(playerDistance.x) >= (_xTranslation * 1.1))
                {
                    var newPosition = transform.position;
                    newPosition.x += playerDistance.x > 0 ? _xTranslation * 2 : -_xTranslation * 2;
                    transform.position = newPosition;
                }
            }
        }
    }
}