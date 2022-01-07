using UnityEngine;

namespace Game.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        public void Damage()
        {
            Destroy(gameObject);
        }
    }
}