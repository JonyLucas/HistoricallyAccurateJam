using Game.Audio;
using Game.Enums;
using Game.Player;
using UnityEngine;

namespace Game.Props
{
    public class CollectableActions : MonoBehaviour
    {
        [SerializeField]
        private CollectablesType _collectables;

        [SerializeField]
        private SoundFx _collectSfx;

        private GameObject _mainCamera;

        private void Awake()
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                AudioSource.PlayClipAtPoint(_collectSfx.GetRandomSound(), _mainCamera.transform.position);
                if (_collectables == CollectablesType.Banana || _collectables == CollectablesType.Pineapple)
                {
                    Regen(collision);
                }

                if (_collectables == CollectablesType.Feather)
                {
                    IncreaseArrows(collision);
                }
            }
        }

        private void Regen(Collider2D collision)
        {
            collision.gameObject.GetComponent<PlayerHealth>().Heal(_collectables);
            Destroy(gameObject);
        }

        private void IncreaseArrows(Collider2D collision)
        {
            collision.gameObject.GetComponent<PlayerAttack>().IncreaseArrows();
            Destroy(gameObject);
        }
    }
}