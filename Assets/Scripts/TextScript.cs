using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    private static TextMeshProUGUI _textMeshPro;
    
    public const string TextBlank = "";
    public const string TextAccion = "Presiona acción para activar";
    public const string TextKilled = "Has muerto";
    public const string TextFinish = "Has encontrado la salida, enhorabuena.";

    // Start is called before the first frame update
    private void Start()
    {
        _textMeshPro = GameObject.Find("TextInfo").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ChangeText(string text)
    {
        _textMeshPro.SetText(text);
    }
}
