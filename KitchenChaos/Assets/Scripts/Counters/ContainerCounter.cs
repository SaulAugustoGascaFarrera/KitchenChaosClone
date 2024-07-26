using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(PlayerManager player)
    {
        if(!player.HasKitchenObject())
        {

            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

           // Transform transformObject = Instantiate(kitchenObjectSO.kitchenObject,player.GetKitchenObjectFollowTransform());
           // KitchenObject kitchenObject = transformObject.GetComponent<KitchenObject>();

           // //transformObject.GetComponent<KitchenObject>();

           //kitchenObject.SetKitchenObjectParemt(player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
