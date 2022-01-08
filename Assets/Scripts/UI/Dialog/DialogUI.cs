using Game.ScriptableObjects.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Dialog
{
    public class DialogUI : MonoBehaviour
    {
        [SerializeField]
        private Text _textLabel;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private bool _isFinalDialog = false;

        [SerializeField]
        private GameEvent _finishedLevel;

        private ScriptableDialog _dialog;
        private Dialog _currentDialog;

        private int _index;

        private void OnEnable()
        {
            _index = 0;
            UpdateDialog();
        }

        public void NextDialog()
        {
            _index++;
            UpdateDialog();
        }

        public void SetDialog(ScriptableDialog dialog)
        {
            _dialog = dialog;
        }

        private void UpdateDialog()
        {
            if (_dialog != null && _index < _dialog.Dialogs.Count)
            {
                _currentDialog = _dialog.Dialogs[_index];
                _icon.sprite = _currentDialog.CharIcon;
                _textLabel.text = _currentDialog.TextDialog;
            }
            else
            {
                if (_isFinalDialog)
                {
                    _finishedLevel.OnOcurred();
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}