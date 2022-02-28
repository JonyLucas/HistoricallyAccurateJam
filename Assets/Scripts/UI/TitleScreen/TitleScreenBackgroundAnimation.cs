using Game.ScriptableObjects.Events;
using UnityEngine;

namespace Game.UI.Animation
{
    public class TitleScreenBackgroundAnimation : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _event;

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
                transform.position = Vector3.Lerp(transform.position, _finalPosition, _moveRate);
            }

            if (Vector3.Distance(transform.position, _finalPosition) < 0.2f)
            {
                transform.position = _finalPosition;
                _isMoving = false;
                _event.OnOcurred();
            }
        }
    }
}