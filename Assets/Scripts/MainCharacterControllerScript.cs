using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainCharacterControllerScript : MonoBehaviour
{
    private CharacterController _characterController;
    
    // Necesarios para la caída inicial del personaje
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private float _gravity = -9.81f;
    
    // Velocidad de movimiento y rotación del jugador
    public float playerSpeed = 2.5f;
    public float playerRotation = 0.3f;

    private void Start()
    {
        // Obtenemos el character controller
        _characterController = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        
        InitialFall();

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            CharacterRotation();
        }else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            CharacterMovement();
        }else if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    /**
     * Control del movimiento horizontal
     */
    private void CharacterMovement()
    {
        Vector3 move = new Vector3(0, 0, Input.GetAxis("Vertical"));
        _characterController.Move(-transform.TransformDirection(move) * Time.deltaTime * playerSpeed);
    }

    /**
     * Control de la rotación del personaje
     */
    private void CharacterRotation()
    {
        Vector3 rotation = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        _characterController.transform.Rotate(rotation * playerRotation);
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
}
