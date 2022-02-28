using UnityEngine;

namespace Game.CameraScripts
{
    [ExecuteInEditMode]
    public class CameraBounds : MonoBehaviour
    {

        [SerializeField]
        private float _boundsMinX;

        [SerializeField]
        private float _boundsMaxX;

        [SerializeField]
        private float _boundsMinY;

        [SerializeField]
        private float _boundsMaxY;
        
        private void Awake()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, _boundsMinX, _boundsMaxX),
                Mathf.Clamp(transform.position.y, _boundsMinY, _boundsMaxY),
                transform.position.z
            );
        }
    }
}