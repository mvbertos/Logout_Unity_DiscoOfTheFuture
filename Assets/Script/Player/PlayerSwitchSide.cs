using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerSwitchSide : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private KeyCode down = KeyCode.S;
    [SerializeField] private KeyCode downNegative = KeyCode.W;

    //Physics
    [Header("Physics")]
    [SerializeField] private CharacterPhysics characterPhysics;
    [SerializeField] private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    //Switch side 
    [SerializeField] private float cooldown = 1f;
    private float cooldownTime;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Make player flip side when input is executed
        ChangeSide();
    }

    private void ChangeSide()
    {
        if (characterPhysics.IsGravityInverted())
        {
            if (Input.GetKeyDown(downNegative))
            {
                OnChangeSide();
            }
        }
        else
        {
            if (Input.GetKeyDown(down))
            {
                OnChangeSide();
            }
        }
    }

    public void OnChangeSide()
    {
        if (!playerMovement.CanPlayerJump())
        {
            return;
        }

        //Begin timerevent 
        //if player don´t touch anything in time 
        //On Change will be called back
        EnableAntiGravity();

        TimerEvent.Create(CancelOnChangeSide, 0.5f, "CancelOnChangeSide");

        characterPhysics.OnTriggerEnd += characterPhysics.OnTriggerEnd_ResetIsTriggerValue;
    }

    private void CancelOnChangeSide()
    {
        if (characterPhysics && characterPhysics.ObjectCollider.isTrigger) DisableAntiGravity();
    }

    private void EnableAntiGravity()
    {
        characterPhysics.ObjectCollider.isTrigger = true;
        playerMovement.OnJump(-playerMovement.JumpPower / 2);
        characterPhysics.NewGravity(-rb.gravityScale);
    }
    private void DisableAntiGravity()
    {
        characterPhysics.ObjectCollider.isTrigger = false;
        characterPhysics.NewGravity(-rb.gravityScale);

    }
}
