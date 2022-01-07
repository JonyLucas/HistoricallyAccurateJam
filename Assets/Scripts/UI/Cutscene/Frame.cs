using UnityEngine;

namespace Game.UI.Cutscene
{
    [System.Serializable]
    public class Frame
    {
        [SerializeField]
        private Sprite _image;

        [SerializeField]
        private string _text;

        public Sprite Image
        { get { return _image; } }

        public string Text
        { get { return _text; } }
    }
}