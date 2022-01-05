using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public void Damage()
    {
        Destroy(gameObject);
    }
}