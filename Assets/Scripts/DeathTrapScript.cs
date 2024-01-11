using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrampaEvent : MonoBehaviour
{

    private GameObject _characterController;
    
    // Start is called before the first frame update
    private void Start()
    {
        _characterController = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Evento muerte
        var characterControllerScript = _characterController.GetComponent<MainCharacterControllerScript>();
        StartCoroutine(characterControllerScript.KillCharacter(TextScript.TextKilled));
        
    }
}
