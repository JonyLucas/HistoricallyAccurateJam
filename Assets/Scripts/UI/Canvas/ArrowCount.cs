using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ArrowCount : MonoBehaviour
    {
        private Text _textComponent;

        private void Awake()
        {
            _textComponent = GetComponent<Text>();
        }

        public void UpdateArrowCount(int count)
        {
            _textComponent.text = $"x{count}";
        }
    }
}