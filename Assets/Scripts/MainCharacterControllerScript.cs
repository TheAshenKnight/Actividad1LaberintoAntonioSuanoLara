using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using ScriptUtility = Unity.VisualScripting.ScriptUtility;

[RequireComponent(typeof(CharacterController))]
public class MainCharacterControllerScript : MonoBehaviour
{
    private CharacterController _characterController;
    private BlackScreenScript _blackScreenScript;
    
    // Necesarios para la caída inicial del personaje
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private float _gravity = -9.81f;
    
    private PlayerInput _playerInput;
    private bool _isTriggered = false;
    private InputAction _closeAction;
    
    // Velocidad de movimiento y rotación del jugador
    public float playerSpeed = 1.35f;
    public float playerRotation = 1f;
    
    private void Start()
    {
        // Obtenemos el character controller
        _characterController = gameObject.GetComponent<CharacterController>();
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _playerInput.actions["Movement"].started += StartTrigger;
        _playerInput.actions["Movement"].canceled += StopTrigger;
        _blackScreenScript = GameObject.FindWithTag("BlackSquare").GetComponent<BlackScreenScript>();
    }

    void Update()
    {
        InitialFall();
        
        if (_isTriggered)
        {
            CharacterMovement();
        }
        
    }

    public IEnumerator KillCharacter(string message)
    {
        _isTriggered = false;
        _playerInput.enabled = false;
        TextScript.ChangeText(message);
        StartCoroutine(_blackScreenScript.FadeInOut(true, 3));
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnAccion(InputValue value)
    {
        foreach(var puertaSwitch in GameObject.FindGameObjectsWithTag("PuertaSwitch"))
        {
            if (!puertaSwitch.GetComponent<BoxCollider>().bounds
                    .Contains(_characterController.transform.position)) continue;
            var doorSwitchScript = puertaSwitch.GetComponent<DoorSwitchScript>();
            doorSwitchScript.ActivateDoor();
        }
        
        foreach(var chest in GameObject.FindGameObjectsWithTag("Chest"))
        {
            if (!chest.GetComponent<BoxCollider>().bounds
                    .Contains(_characterController.transform.position)) continue;
            StartCoroutine(KillCharacter(TextScript.TextKilled));
        }
    }
    
    private void OnCerrar(InputValue value)
    {
        Application.Quit();
    }

    /**
     * Control del movimiento y rotación
     */
    private void CharacterMovement()
    {
        var inputVector = _playerInput.actions["Movement"].ReadValue<Vector2>();
        
        if (inputVector.y != 0)
        {
            var characterMovementVector = new Vector3(inputVector.x * playerSpeed, 0, inputVector.y * playerSpeed);
            _characterController.Move(-transform.TransformDirection(characterMovementVector) * playerSpeed * Time.deltaTime);
        }
        if (inputVector.x != 0)
        {
            _characterController.transform.Rotate(new Vector3(0, inputVector.x, 0) * playerRotation);
        }
    }

    /**
     * Necesario para la caida inicial, una vez se detecta que el personaje está grounded, se quita la velocidad
     * de movimiento en y
     */
    private void InitialFall()
    {
        StartCoroutine(_blackScreenScript.FadeInOut(false, 1));
        
        _groundedPlayer = _characterController.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    public void StartTrigger(InputAction.CallbackContext ctx)
    {
        _isTriggered = true;
    }
    
    public void StopTrigger(InputAction.CallbackContext ctx)
    {
        _isTriggered = false;
    }

    public IEnumerator FinishGame()
    {
        _isTriggered = false;
        _playerInput.enabled = false;
        TextScript.ChangeText(TextScript.TextFinish);
        StartCoroutine(_blackScreenScript.FadeInOut(true, 3));
        yield return new WaitForSeconds(5);
        Application.Quit();
    }
    
}
