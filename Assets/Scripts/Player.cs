using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventsArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventsArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    [Header("Clear Counter Props")]
    private ClearCounter selectedCounter;

    private bool isWalking = false;
    private Vector3 lastInteractDir;


    private void Awake()
    {
        if(Instance != null) 
        {
            Debug.LogError("There is more thjan one Player Instance");
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
            selectedCounter.Interact();
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    void MoveOnXDirection(Vector3 moveDirection,float playerHeight,float playerRadius,float moveDistance)
    {
        //cannot move toawrds direction

        //attempt onky x movement
        Vector3 moveDirX = new Vector3(moveDirection.x, 0.0f, 0.0f);

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

        if(canMove) 
        {
            //can move only on the x 
            moveDirection = moveDirX;
        }
        else
        {
            //cannot move only on the x

            //attempt only z moevemnmt
            Vector3 moveDirZ = new Vector3(0.0f, 0.0f, moveDirection.z);

            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);


            if (canMove)
            {
                //can move only on the z
                moveDirection = moveDirZ;
            }
            else
            {
                //cannot move in any direction

            }

        }

    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);


        if(moveDirection != Vector3.zero) 
        { 
            lastInteractDir = moveDirection;
        }

        float interactDistance = 1.1f;

        if(Physics.Raycast(transform.position, lastInteractDir,out RaycastHit raycastHit,interactDistance,countersLayerMask))
        {
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //Has clear counter
                if(clearCounter != selectedCounter)
                {
                    
                    SetSelectedCounter(clearCounter);

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

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();


        Vector3 moveDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;

        float playerRadius = 0.7f;

        float playerHeight = 2.0f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);


        isWalking = moveDirection != Vector3.zero;

        if (!canMove)
        {

            //cannot move toawrds direction

            //attempt onky x movement
            Vector3 moveDirX = new Vector3(moveDirection.x, 0.0f, 0.0f).normalized;

            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                //can move only on the x 
                moveDirection = moveDirX;
            }
            else
            {
                //cannot move only on the x

                //attempt only z moevemnmt
                Vector3 moveDirZ = new Vector3(0.0f, 0.0f, moveDirection.z).normalized;

                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);


                if (canMove)
                {
                    //can move only on the z
                    moveDirection = moveDirZ;
                }
                else
                {
                    //cannot move in any direction

                }

            }
        }


        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }




        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }


    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;


        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventsArgs
        {
            selectedCounter = selectedCounter
        });
    }

}
