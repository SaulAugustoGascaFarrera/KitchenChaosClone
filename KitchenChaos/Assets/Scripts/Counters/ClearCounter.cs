using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    public override void Interact(PlayerManager player)
    {
        //if (!HasKitchenObject())
        //{
        //    var ObjectKit = Instantiate(prefab,GetKitchenObjectFollowTransform());
        //    ObjectKit.localPosition = Vector3.zero;
        //    ObjectKit.localRotation = Quaternion.identity;  
        //}
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                KitchenObject kitchenObject = Instantiate(player.GetKitchenObject(), GetKitchenObjectFollowTransform());
                KitchenObject kitchenObjectComp = kitchenObject.GetComponent<KitchenObject>();
                kitchenObjectComp.SetKitchenObjectParemt(this);

                player.GetKitchenObject().DestroySelf();
            }
           
        }
        else
        {
            //if (!player.HasKitchenObject())
            //{
            //    KitchenObject kitchenObject = Instantiate(GetKitchenObject(), player.GetKitchenObjectFollowTransform());
            //    KitchenObject kitchenObjectComp = kitchenObject.GetComponent<KitchenObject>();
            //    kitchenObjectComp.SetKitchenObjectParemt(player);

            //    GetKitchenObject().DestroySelf();
            //}
            if(player.HasKitchenObject())
            {
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate ?
                    //PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;

                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
                else
                {
                    //the player is not holding a plate,beceuase the player is holding something else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //counter is holding a plate
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParemt(player);
            }

        }
       
    }

   
}
