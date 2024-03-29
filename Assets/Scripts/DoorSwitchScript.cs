using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorSwitchScript : MonoBehaviour
{
    
    public GameObject doorLockedObject;
    public GameObject doorUnlockedObject;
    public bool doorLocked = true;
    
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
        if(doorLocked){
            TextScript.ChangeText(TextScript.TextAccion);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TextScript.ChangeText(TextScript.TextBlank);
    }

    public void ActivateDoor()
    {
        if (!doorLockedObject.activeSelf) return;
        doorLockedObject.SetActive(false);
        doorLocked = false;
        TextScript.ChangeText(TextScript.TextBlank);
    }
}
