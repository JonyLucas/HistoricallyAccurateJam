using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Dialog
{
    [CreateAssetMenu(fileName = "Dialog Object", menuName = "Dialog Object", order = 55)]
    public class ScriptableDialog : ScriptableObject
    {
        [SerializeField]
        private List<Dialog> _dialogs;

        [SerializeField]
        private bool _isFinalDialog;

        public List<Dialog> Dialogs
        { get { return _dialogs; } }

        public bool IsFinalDialog
        { get { return _isFinalDialog; } }
    }
}