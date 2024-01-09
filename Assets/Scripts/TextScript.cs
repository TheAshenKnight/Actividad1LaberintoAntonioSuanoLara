using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    private static TextMeshProUGUI textMeshPro;
    
    public static String textBlank = "";
    public static String textAccion = "Presiona acción para activar";
    
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GameObject.Find("TextInfo").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ChangeText(String text)
    {
        textMeshPro.SetText(text);
    }
}
