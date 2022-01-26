using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] bool autoRun = false;
    [SerializeField] KeyCode up = KeyCode.W;
    //[SerializeField] KeyCode down = KeyCode.S;
    [SerializeField] KeyCode left = KeyCode.A;
    [SerializeField] KeyCode right = KeyCode.D;

    [Header("Movement values")]
    [SerializeField] float movementSpeed = 5;
    [SerializeField] float jumpPower = 5;

    [Header("Physics")]
    [SerializeField] float maxFallSpeed = 5;
    [SerializeField] float groundCheckDistance = 5;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private CharacterPhysics characterPhysics;

    //physics
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Handle movement inputs
        //from player
        Move();

        //Handle jump inputs
        //from player
        Jump();
    }

    private void Jump()
    {
        //Handle Inputs
        if (Input.GetKey(up))
        {
            OnJump(1);
        }
    }

    #region MovementationMethods

    private void Move()
    {
        float inputValue = 0;

        if (!autoRun)
        {
            if (Input.GetKey(right))
            {
                inputValue = 1;
            }
            if (Input.GetKey(left))
            {
                inputValue = -1;
            }
        }
        else
        {
            inputValue = 1;
        }

        OnMove(inputValue);

    }

    public void OnMove(float horizontalDirection)
    {
        //convert direction to movement 
        float movement = horizontalDirection * movementSpeed;

        //execute velocity
        rb.velocity = new Vector2(movement, rb.velocity.y);
    }

    #endregion
    #region JumpMethods
    public bool CanPlayerJump()
    {
        RaycastHit2D hit;

        if (characterPhysics.IsGravityInverted())
        {
            hit = Physics2D.Raycast(this.transform.position, this.transform.up, groundCheckDistance, whatIsGround);
            Debug.DrawRay(this.transform.position, this.transform.up * groundCheckDistance);
        }
        else
        {
            hit = Physics2D.Raycast(this.transform.position, -this.transform.up, groundCheckDistance, whatIsGround);
            Debug.DrawRay(this.transform.position, -this.transform.up * groundCheckDistance);
        }

        if (hit)
        {
            Debug.Log("Hit ground");
            return true;
        }
        else
        {
            return false;
        }
    }
    public void OnJump(float VerticalDirection)
    {
        if (CanPlayerJump())
        {
            rb.velocity = characterPhysics.GetVerticalVelocity(VerticalDirection * jumpPower);
        }
    }
    #endregion
}