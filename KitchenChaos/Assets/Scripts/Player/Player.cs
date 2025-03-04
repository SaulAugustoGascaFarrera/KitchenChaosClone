using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [Header("Player Atts")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 7.0f;
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float playerHeight = 2.0f;

    [Header("Game Input Atts")]
    [SerializeField] private GameInput gameInput;

    [Header("Animation Atts")]
    private bool isWalking;


    private Vector3 lastInteractDirection;
    [SerializeField] private BaseCounter selectedCounter;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            Debug.Log("YA INTERACTUA");
        }
    }

    void Update()
    {
           HandleMovement();

           HandleInteractions();
    }

    public bool IsWalking()
    {

        return isWalking; 
    }


    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 movementDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);

        if(movementDirection != Vector3.zero)
        {
            lastInteractDirection = movementDirection;
        }

        float interactionDistance = 0.85f;

        if (Physics.Raycast(transform.position,lastInteractDirection, out RaycastHit raycastHit, interactionDistance,1 << 6))
        {
           if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
           {

                if(baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);

                    
                }

            }
            else
            {
                SetSelectedCounter(null);
            }

        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 movementDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);


        float moveDistance = movementSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirection, moveDistance);


        if (!canMove)
        {
            //Attempt only X movement
            Vector3 moveDirectionX = new Vector3(movementDirection.x, 0.0f, 0.0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove)
            {
                //Can move only on the X
                movementDirection = moveDirectionX;
            }
            else
            {
                //Canot move only on the X
                //Attemp only on Z movement

                Vector3 moveDirectionZ = new Vector3(0.0f, 0.0f, movementDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);


                if (canMove)
                {
                    //Can move only on the Z
                    movementDirection = moveDirectionZ;
                }
                else
                {
                    //Cannot move in any direction

                }

            }
        }

        if (canMove)
        {
            transform.position += movementDirection * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, movementDirection, rotationSpeed * Time.deltaTime);
    }


    private void SetSelectedCounter(BaseCounter baseCounter)
    {
        this.selectedCounter = baseCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
