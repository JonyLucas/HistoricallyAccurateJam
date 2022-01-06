using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Cutscene Frame", menuName = "Cutscene Frame", order = 52)]
public class CutsceneFrame : ScriptableObject
{
    [SerializeField]
    private List<Frame> _frames;

    public List<Frame> Frames
    { get { return _frames; } }
}