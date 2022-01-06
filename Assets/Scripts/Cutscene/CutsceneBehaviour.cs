using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneBehaviour : MonoBehaviour
{
    [SerializeField]
    private CutsceneFrame _cutsceneFrame;

    private List<Frame> _frames;
    private Frame _currentFrame;

    [SerializeField]
    private Image _uiImage;

    [SerializeField]
    private GameObject _textObject;

    private TMP_Text _textComponent;

    private void Start()
    {
        _frames = _cutsceneFrame.Frames;
        _currentFrame = _frames.FirstOrDefault();
        _textComponent = _textObject.GetComponent<TMP_Text>();
        UpdateFrame();
    }

    public void NextFrame()
    {
        var index = _frames.IndexOf(_currentFrame);
        if (index < _frames.Count - 1)
        {
            _currentFrame = _frames[index + 1];
        }
        UpdateFrame();
    }

    public void PreviousFrame()
    {
        var index = _frames.IndexOf(_currentFrame);
        if (index > 0)
        {
            _currentFrame = _frames[index - 1];
        }
        UpdateFrame();
    }

    private void UpdateFrame()
    {
        _textComponent.text = _currentFrame.Text;
        _uiImage.sprite = _currentFrame.Image;
    }
}