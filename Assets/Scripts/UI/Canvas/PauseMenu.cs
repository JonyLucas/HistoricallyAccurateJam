using UnityEngine;

namespace Game.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public delegate void OpenWindow(bool isOpen);

        public static event OpenWindow OpenWindowEvent;

        private void OnEnable()
        {
            if (OpenWindowEvent != null)
                OpenWindowEvent.Invoke(true);
        }

        private void OnDisable()
        {
            if (OpenWindowEvent != null)
                OpenWindowEvent.Invoke(false);
        }
    }
}