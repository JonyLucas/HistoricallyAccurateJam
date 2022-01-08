using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePanelScript : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}