using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour,IKitchenObjectParent
{
    public static PlayerManager Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventsArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventsArgs : EventArgs
    {
        public BaseCounter baseCounterSelected;
    }

    [Header("Movement Atts")]
    [SerializeField] private float movementSpeed = 8.0f;
    [SerializeField] private float rotationSpeed = 13.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private BaseCounter baseCounterSelected;
    bool CanInteract = false;

    [Header("Parent Interface Atts")]
    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private KitchenObject kitchenObject;

   

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player Manager Instance");
        }

        Instance = this;
    }


    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;

        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if(baseCounterSelected)
        {
           baseCounterSelected.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
      
        if(baseCounterSelected != null)
        {
            baseCounterSelected.Interact(this);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        //GetMovementDirection();

        HandleMovement();

        HandleInteractions();
    }


    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 movementDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);


        Vector3 lastInteractDir = Vector3.zero;

        if (movementDirection != Vector3.zero)
        {
            lastInteractDir = movementDirection;
        }

        if (Physics.Raycast(transform.position,lastInteractDir,out RaycastHit raycastHit,1.1f,counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
               
                SetSelectedCounter(baseCounter);
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

        Vector3 movementDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);


        bool CanMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * 2.0f,0.7f, movementDirection, movementSpeed * Time.deltaTime);


        if(CanMove)
        {

            if (movementDirection != Vector3.zero)
            {


                transform.position += movementDirection * movementSpeed * Time.deltaTime;


                transform.forward += Vector3.Slerp(transform.forward, movementDirection, rotationSpeed * Time.deltaTime);

               
            }



        }
       
    }


    private Vector3 GetMovementDirection()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 movementDirection = new Vector3(inputVector.x,0.0f,inputVector.y);

        return movementDirection;
    }



    //Kitchen Object Parent Interface

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject; 
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }


    public void SetSelectedCounter(BaseCounter baseCounter)
    {
        this.baseCounterSelected = baseCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventsArgs{
            baseCounterSelected = baseCounterSelected
        });     

    }
}
