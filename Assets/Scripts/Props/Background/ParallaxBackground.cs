using UnityEngine;

namespace Game.Enviroment
{
    [ExecuteInEditMode]
    public class ParallaxBackground : MonoBehaviour
    {
        private Camera _camera;

        [SerializeField]
        private Vector2 _parallaxOrigin;

        // Se isso for editável pela interface do Unity, tem que ser em um objeto tipo a câmera.
        // Não pode ser por instância do ParallelBackground, senão o efeito será quebrado!
        private const float SCALE = 1.0f;

        // Start is called before the first frame update
        private void Start()
        {
            _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        // Update is called once per frame
        private void Update()
        {
            float parallaxFactor = 1.0f / (transform.position.z / Mathf.Abs(_camera.transform.position.z) / SCALE + 1.0f) - 1.0f;
            transform.position = new Vector3(
                _parallaxOrigin.x - _camera.transform.position.x * parallaxFactor,
                _parallaxOrigin.y - _camera.transform.position.y * parallaxFactor,
                transform.position.z
           );
        }
    }
}