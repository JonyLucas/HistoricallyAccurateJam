using Game.Enums;
using System.Collections.Generic;

namespace Game.UI.Dialog
{
    public sealed class DialogManager
    {
        private static DialogManager _instance;

        private List<DialogLanguage> _dialogs;

        public static DialogManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DialogManager();
                }
                return _instance;
            }
        }

        public void SetLanguage(Languages language)
        {
        }
    }
}