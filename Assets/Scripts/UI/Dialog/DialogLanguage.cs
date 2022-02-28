using Game.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Dialog
{
    [CreateAssetMenu(fileName = "Dialog Language", menuName = "Dialog Language", order = 56)]
    public class DialogLanguage : ScriptableObject
    {
        [SerializeField]
        private List<ScriptableDialog> _dialogs;

        [SerializeField]
        private Languages _language;

        public List<ScriptableDialog> Dialogs
        { get { return _dialogs; } }
    }
}