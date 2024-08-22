using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    //public class OnProgressChangedEventArgs : EventArgs
    //{
    //    public float progressNormalized;
    //}

    public event EventHandler OnCut;
    

    [SerializeField] private cuttingRecipeSO[] cuttingRecipeSOArray;


    private int cuttingProgress;
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

                    cuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    cuttingProgress = 0;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }




            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                //KitchenObject kitchenObject = Instantiate(GetKitchenObject(), player.GetKitchenObjectFollowTransform());
                //var kitchenObjectComp = kitchenObject.GetComponent<KitchenObject>();

                //kitchenObjectComp.SetKitchenObjectParemt(player);

                //GetKitchenObject().DestroySelf();
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParemt(player);
            }
        }
        
    }


    public override void InteractAlternate(PlayerManager player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;


            cuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());


            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            OnCut?.Invoke(this,EventArgs.Empty);

            if (cuttingProgress >=  cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());


                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
            
    
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        cuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;

        //foreach (cuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        //{
        //    if(cuttingRecipeSO.input == inputKitchenObjectSO)
        //    {
        //        return true;
        //    }
        //}

        //return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {

        cuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        if(cuttingRecipeSO)
        {
           return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }

        //foreach(cuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        //{
        //    if(cuttingRecipeSO.input == inputKitchenObjectSO)
        //    {
        //        return cuttingRecipeSO.output;
        //    }
        //}

       
    }

    private cuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(cuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}
