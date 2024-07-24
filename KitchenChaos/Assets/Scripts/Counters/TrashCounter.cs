using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(PlayerManager player)
    {
        if(player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            //print("SE DEBE BORRARRRRRRRRR");
        }
    }
}
