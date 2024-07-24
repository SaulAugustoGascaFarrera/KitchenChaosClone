using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour,IKitchenObjectParent
{
    public static PlayerManager Instance { get; private set; }

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

    [SerializeField] private GameObject[] visualArray;

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
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        //print("YA INTERACTUE");
        if(CanInteract)
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
                //print("TOQUE UN CLUNTERRRR");
                CanInteract = true;

                Show();
                this.baseCounterSelected = baseCounter;
            }
          
        }
        else
        {
            CanInteract = false;

           Hide(); 
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

                //print("<color=green>SE PUEDE MOVER</color>");
            }



        }
       
    }


    private Vector3 GetMovementDirection()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 movementDirection = new Vector3(inputVector.x,0.0f,inputVector.y);

        return movementDirection;
    }


    public void Show()
    {
        foreach(GameObject visual in visualArray)
        {
            visual.SetActive(true);
        }
    }


    public void Hide()
    {
        foreach (GameObject visual in visualArray)
        {
            visual.SetActive(false);
        }
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
}
