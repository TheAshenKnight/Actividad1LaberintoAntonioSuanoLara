using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFinishGameScript : MonoBehaviour
{
    
    private GameObject _characterController;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var characterControllerScript = _characterController.GetComponent<MainCharacterControllerScript>();
        StartCoroutine(characterControllerScript.FinishGame());
        
    }
}
