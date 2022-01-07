using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Cutscene
{
    [CreateAssetMenu(fileName = "Cutscene Frame", menuName = "Cutscene Frame", order = 52)]
    public class CutsceneFrame : ScriptableObject
    {
        [SerializeField]
        private List<Frame> _frames;

        public List<Frame> Frames
        { get { return _frames; } }
    }
}