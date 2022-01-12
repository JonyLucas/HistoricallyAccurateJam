using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using Game.Audio;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField]
    private float _typeSpeed = 50f;

    [SerializeField]
    private SoundFx _soundFx;

    private AudioSource _audioSource;

    private bool _isTyping = false;

    public bool IsTyping
    { get { return _isTyping; } }

    public void Run(string textToType, TMP_Text textLabel)
    {
        _audioSource = GetComponent<AudioSource>();

        if (!_isTyping)
        {
            textLabel.text = "";
            StartCoroutine(TypeText(textToType, textLabel));
        }
        else
        {
            StopWriting();
        }
    }

    public void StopWriting()
    {
        _isTyping = false;
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        int charIndex = 0;
        var strBuilder = new StringBuilder();
        _isTyping = true;

        while (charIndex < textToType.Length && _isTyping)
        {
            strBuilder.Append(textToType[charIndex]);
            charIndex++;
            textLabel.text = strBuilder.ToString();

            if (_audioSource != null && _soundFx != null)
            {
                _audioSource.clip = _soundFx.GetRandomSound();
                _audioSource.Play();
            }

            yield return new WaitForSeconds(_typeSpeed);
        }

        textLabel.text = textToType;
        _isTyping = false;
    }
}