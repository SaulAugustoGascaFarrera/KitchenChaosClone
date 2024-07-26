using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private cuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(PlayerManager player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    KitchenObject kitchenObject = Instantiate(player.GetKitchenObject(), GetKitchenObjectFollowTransform());
                    var kitchenObjectComp = kitchenObject.GetComponent<KitchenObject>();
                    kitchenObjectComp.SetKitchenObjectParemt(this);

                    player.GetKitchenObject().DestroySelf();
                }




            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject kitchenObject = Instantiate(GetKitchenObject(), player.GetKitchenObjectFollowTransform());
                var kitchenObjectComp = kitchenObject.GetComponent<KitchenObject>();

                kitchenObjectComp.SetKitchenObjectParemt(player);

                GetKitchenObject().DestroySelf();
            }
        }
        
    }


    public override void InteractAlternate(PlayerManager player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {

            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);

           
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (cuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return true;
            }
        }

        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(cuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }

        return null;
    }
}
