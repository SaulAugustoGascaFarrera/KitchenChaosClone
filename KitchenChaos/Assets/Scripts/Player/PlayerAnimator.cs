using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private const string IS_WALKING = "IsWalking";

    private Player player;

    private Animator animator;  

    private void Awake()
    {
        player = GetComponent<Player>();  
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        //animator.SetBool(IS_WALKING, player.IsWalking()); 
    }
}
