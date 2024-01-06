using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(CharacterController))]
public class MainCharacterControllerScript : MonoBehaviour
{
    private CharacterController _characterController;
    
    // Necesarios para la caída inicial del personaje
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private float _gravity = -9.81f;
    
    private PlayerInput _playerInput;
    private bool _isTriggered = false;
    private InputAction _closeAction;
    
    // Velocidad de movimiento y rotación del jugador
    public float playerSpeed = 2.5f;
    public float playerRotation = 0.3f;
    
    private void Start()
    {
        // Obtenemos el character controller
        _characterController = gameObject.GetComponent<CharacterController>();
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _playerInput.actions["Movement"].started += startTrigger;
        _playerInput.actions["Movement"].canceled += stopTrigger;
    }

    void Update()
    {
        InitialFall();
        
        if (_isTriggered)
        {
            CharacterMovement();
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
        var characterMovementVector = new Vector3(inputVector.x * playerSpeed, 0, inputVector.y * playerSpeed);
        _characterController.Move(-transform.TransformDirection(characterMovementVector) * playerSpeed * Time.deltaTime);
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
        _groundedPlayer = _characterController.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    public void startTrigger(InputAction.CallbackContext ctx)
    {
        _isTriggered = true;
    }
    
    public void stopTrigger(InputAction.CallbackContext ctx)
    {
        _isTriggered = false;
    }
}
