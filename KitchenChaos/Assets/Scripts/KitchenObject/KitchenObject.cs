using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;

    public IKitchenObjectParent KitchenObjectParent;

   public KitchenObjectSO GetKitchenObjectSO() 
    { 
        return KitchenObjectSO; 
    }

    public void SetKitchenObjectParemt(IKitchenObjectParent kitchenObjectParent)
    {
        if(this.KitchenObjectParent != null)
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }

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

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO,IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.kitchenObject);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        kitchenObject.SetKitchenObjectParemt(kitchenObjectParent);
      
        return kitchenObject;
    }


    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;

            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        } 

       
    }
}
