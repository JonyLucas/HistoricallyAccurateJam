using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class TimedFadeIn : MonoBehaviour
    {
        [SerializeField]
        private float _startTime = 1;

        [SerializeField]
        private float _fadeInSpeed = 0.1f;

        private Text _textComponent;
        private Image _imageComponent;

        private void Start()
        {
            _textComponent = GetComponent<Text>();
            _imageComponent = GetComponent<Image>();
            StartCoroutine(StartFadeIn());
        }

        private IEnumerator StartFadeIn()
        {
            yield return new WaitForSeconds(_startTime);
            float alphaValue = 0;
            while (alphaValue <= 1)
            {
                yield return new WaitForSeconds(_fadeInSpeed);

                if (_imageComponent != null)
                {
                    _imageComponent.color = new Color(255, 255, 255, alphaValue);
                }
                else if (_textComponent != null)
                {
                    _textComponent.color = new Color(255, 255, 255, alphaValue);
                }
                alphaValue += _fadeInSpeed;
            }
        }
    }
}