using UnityEngine;

namespace Game.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private float _followRate = 1;

        private GameObject _player;

        private Vector3 _playerDistance;

        private void Awake()
        {
            _player = GameObject.Find("Player");
            _playerDistance = transform.position - _player.transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _player.transform.position + _playerDistance, _followRate * Time.deltaTime);
            //transform.position = _player.transform.position + _playerDistance;
        }
    }
}