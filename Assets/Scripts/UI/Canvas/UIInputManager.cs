using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class UIInputManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _pauseMenu;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pauseMenu.SetActive(!_pauseMenu.activeInHierarchy);
            }
        }
    }
}