using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Dialog
{
    public class LanguageSettings : MonoBehaviour
    {
        private DialogManager _dialogManager;

        // Start is called before the first frame update
        private void Start()
        {
            _dialogManager = DialogManager.Instance;
        }

        public void SetLanguage(int index)
        {
        }
    }
}