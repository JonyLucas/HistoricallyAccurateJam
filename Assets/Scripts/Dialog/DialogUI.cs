using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textLabel;

    private void Start()
    {
        //_textLabel.text = "Teste n�mero 1.";
        GetComponent<TypewriterEffect>().Run("Teste n�mero 1!\n Bom dia fam�lia!\n Estou funcionando!", _textLabel);
    }
}
