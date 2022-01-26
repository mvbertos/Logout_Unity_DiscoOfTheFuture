using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerSwitchSide : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] KeyCode down = KeyCode.S;

    //Physics
    [Header("Physics")]
    [SerializeField] private CharacterPhysics characterPhysics;
    [SerializeField] private PlayerMovement playerMovement;
    private Rigidbody2D rb;

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
        if (Input.GetKeyDown(down))
        {
            OnChangeSide();
        }
    }

    public void OnChangeSide()
    {
        if (!playerMovement.CanPlayerJump())
        {
            return;
        }

        characterPhysics.ObjectCollider.isTrigger = true;
        playerMovement.OnJump(-1.5f);
        characterPhysics.NewGravity(-rb.gravityScale);

        //TODO: find better way to return trigger value to the object after receive flip input
        characterPhysics.OnTriggerEnd += characterPhysics.OnTriggerEnd_ResetIsTriggerValue;
    }

}
