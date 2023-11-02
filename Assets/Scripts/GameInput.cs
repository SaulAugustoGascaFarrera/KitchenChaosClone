using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputActions();

        playerInputAction.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        //Vector2 inputVector = new Vector2(0, 0);

        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        //if (Input.GetKey(KeyCode.W))
        //{
        //    inputVector.y += 1;
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    inputVector.y -= 1;
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    inputVector.x -= 1;
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    inputVector.x += 1;
        //}


        inputVector = inputVector.normalized;

        return inputVector;
    }
}
