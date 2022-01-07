using UnityEngine;

namespace Game.Props
{
    public class CollectableAnimations : MonoBehaviour
    {
        [SerializeField] private float _yDistance;
        private Vector3 _swapPosition;
        private Vector3 _initialPosition;
        private Vector3 _finalPosition;

        private Vector2 _moveDirection;

        [SerializeField]
        private float _speed = 1;

        private void Start()
        {
            _initialPosition = transform.position;
            _initialPosition.y += _yDistance;
            _finalPosition = transform.position;
            _finalPosition.y -= _yDistance;

            transform.position = _initialPosition;
        }

        private void Update()
        {
            _moveDirection = _finalPosition.y > _initialPosition.y ? Vector2.up : Vector2.down;
            transform.Translate(_moveDirection * _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _finalPosition) < 0.01f)
            {
                transform.position = _finalPosition;
                _swapPosition = _finalPosition;
                _finalPosition = _initialPosition;
                _initialPosition = _swapPosition;
            }
        }
    }
}