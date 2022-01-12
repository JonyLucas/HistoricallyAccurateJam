using Game.ScriptableObjects.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField]
    private GameEvent _event;

    [SerializeField]
    private float _fadeOutSpeed = 0.2f;

    [SerializeField]
    private float _startTime = 1;

    private Image _imageComponent;

    private void Start()
    {
        _imageComponent = GetComponent<Image>();
        StartCoroutine(StartFadeIn());
    }

    private IEnumerator StartFadeIn()
    {
        yield return new WaitForSeconds(_startTime);
        float alphaValue = 0;
        while (alphaValue <= 1)
        {
            yield return new WaitForSeconds(_fadeOutSpeed);

            if (_imageComponent != null)
            {
                _imageComponent.color = new Color(0, 0, 0, alphaValue);
            }
            alphaValue += _fadeOutSpeed;
        }

        _event.OnOcurred();
    }
}