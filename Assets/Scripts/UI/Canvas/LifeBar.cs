using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _lifebarImages;

    private Image _imageComponent;

    private void Awake()
    {
        _imageComponent = GetComponent<Image>();
    }

    public void UpdateLives(int index)
    {
        if (index < _lifebarImages.Count)
        {
            _imageComponent.sprite = _lifebarImages[index];
        }
    }
}