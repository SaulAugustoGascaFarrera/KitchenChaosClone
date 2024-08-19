using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{

    public event EventHandler<OnStateChangeEventArgs> OnStateChanged;
    public class OnStateChangeEventArgs : EventArgs
    {
        public State state;
       
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;


    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {

        if(HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    FryingAction();
                    break;
                case State.Fried:
                    BurningAction();
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(PlayerManager player)
    {
        if (!HasKitchenObject())
        {
            if(player.HasKitchenObject())
            {
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParemt(this);

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;

                    fryingTimer = 0;

                    //burningTimer = 0;   

                    OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                    {
                        state = state
                    });
                }
            }
        }
        else
        {
            if(player.HasKitchenObject())
            {
                return;
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParemt(player);

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = state
                });
            }
        }

    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if(fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }

        
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if(fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }

        return null;
        
    }


    private void FryingAction()
    {
        fryingTimer += Time.deltaTime;
        if(fryingTimer > fryingRecipeSO.fryingTimerMax)
        {
            //fryingTimer = 0;

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

            state = State.Fried;

            burningTimer = 0;

            burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
            {
                state = state,
               
            });

        }
    }


    private void BurningAction()
    {
        burningTimer += Time.deltaTime;
        if(burningTimer > burningRecipeSO.burningTimerMax)
        {
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

            state = State.Burned;

            OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
            {
                state = state
            });
        }
    }
}
