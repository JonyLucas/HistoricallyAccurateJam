using UnityEngine;

namespace Game.CameraScripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private float _followRate;

        [SerializeField]
        private float _boundsMinX;

        [SerializeField]
        private float _boundsMaxX;

        [SerializeField]
        private float _boundsMinY;

        [SerializeField]
        private float _boundsMaxY;

        [SerializeField]
        private float _offsetX;

        [SerializeField]
        private float _offsetY;

        private float _cameraZ;
        private GameObject _player;

        private void Awake()
        {
            _player = GameObject.Find("Player");
            _cameraZ = transform.position.z;
        }

        // Update is called once per frame
        private void Update()
        {
            transform.position += ((_player.transform.position + _offsetY * Vector3.up + _offsetX * Vector3.right) - transform.position)*_followRate;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, _boundsMinX, _boundsMaxX),
                Mathf.Clamp(transform.position.y, _boundsMinY, _boundsMaxY),
                _cameraZ
            );
        }
    }
}