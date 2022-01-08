using Game.Audio;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField]
        private SoundFx _deathSfx;

        private GameObject _mainCamera;

        private void Start()
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        public void Damage()
        {
            AudioSource.PlayClipAtPoint(_deathSfx.GetRandomSound(), _mainCamera.transform.position);
            Destroy(gameObject);
        }
    }
}