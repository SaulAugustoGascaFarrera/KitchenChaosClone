using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    private KitchenObjectSO KitchenObjectSO;

    public IKitchenObjectParent KitchenObjectParent;

   public KitchenObjectSO GetKitchenObjectSO() 
    { 
        return KitchenObjectSO; 
    }

    public void SetKitchenObjectParemt(IKitchenObjectParent kitchenObjectParent)
    {
        this.KitchenObjectParent = kitchenObjectParent;

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();

        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParemt()
    {
        return KitchenObjectParent;
    }

    public void DestroySelf()
    {
        KitchenObjectParent.ClearKitchenObject();   

        Destroy(this.gameObject);
    }
}
