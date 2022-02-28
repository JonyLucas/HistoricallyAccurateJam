using UnityEngine;

namespace Game.CameraScripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private float _followRate;

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
            transform.position = new Vector3(transform.position.x, transform.position.y, _cameraZ);
        }
    }
}