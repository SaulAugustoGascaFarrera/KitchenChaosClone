using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;
    
    private KitchenObject kitchenObject;


    public virtual void Interact(Player player)
    {
        Debug.Log("Interact Function Called");
    }

    public virtual void InteractAlternate(Player player)
    {
        Debug.Log("Interact Alternate Function Called");
    }

    public void ClearKithenObject()
    {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint; 
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject; 
    }

    
}
