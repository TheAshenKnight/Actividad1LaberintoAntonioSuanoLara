using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrapScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Evento entrar en collider
        TextScript.ChangeText(TextScript.TextAccion);
        
    }

    private void OnTriggerExit(Collider other)
    {
        TextScript.ChangeText(TextScript.TextBlank);
    }
}
