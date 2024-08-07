using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction;

    public event EventHandler OnInteractAlternateAction;

    private PlayerMovementInput playerMovementInput;


    private void Awake()
    {
        playerMovementInput = new PlayerMovementInput();

        playerMovementInput.Player.Enable();


        playerMovementInput.Player.Interaction.performed += Interaction_performed;


        playerMovementInput.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interaction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //print("PRESIONE LA LETRA E");

        OnInteractAction.Invoke(this, EventArgs.Empty);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerMovementInput.Player.Move.ReadValue<Vector2>();


        inputVector = inputVector.normalized;


        return inputVector;
    }
}
