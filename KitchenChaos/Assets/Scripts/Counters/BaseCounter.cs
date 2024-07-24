using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
    
    [SerializeField] private Transform counterToPoint;
    private KitchenObject KitchenObject;


    public virtual void Interact(PlayerManager player)
    {
        Debug.LogError("Base Counter Interact");

    }   
    public void ClearKitchenObject()
    {
        KitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return KitchenObject;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterToPoint;
    }

    public bool HasKitchenObject()
    {
        return KitchenObject != null;
    }

   

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.KitchenObject = kitchenObject;
    }

    
}
