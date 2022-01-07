using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableActions : MonoBehaviour
{
    [SerializeField]
    private CollectablesType _collectables;

    private PlayerHealth health;
    private void Awake()
    {
        //_collectables = GetComponent<CollectablesType>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
