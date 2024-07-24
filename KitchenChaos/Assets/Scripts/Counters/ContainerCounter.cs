using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(PlayerManager player)
    {
        if(!player.HasKitchenObject())
        {
            Transform transformObject = Instantiate(kitchenObjectSO.kitchenObject,player.GetKitchenObjectFollowTransform());
            KitchenObject kitchenObject = transformObject.GetComponent<KitchenObject>();

            kitchenObject.SetKitchenObjectParemt(player);
        }
    }
}
