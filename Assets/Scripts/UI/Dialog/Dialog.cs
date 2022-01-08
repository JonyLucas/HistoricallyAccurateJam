using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Dialog
{
    [System.Serializable]
    public class Dialog
    {
        [SerializeField]
        private Sprite _charIcon;

        [SerializeField]
        private string _textDialog;

        public Sprite CharIcon { get { return _charIcon; } }
        public string TextDialog { get { return _textDialog; } }
    }
}

