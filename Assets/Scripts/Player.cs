using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();


        Vector3 moveDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);


        isWalking = moveDirection != Vector3.zero;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;


        transform.forward = Vector3.Slerp(transform.forward,moveDirection, rotationSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
