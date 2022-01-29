using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private bool autoRun = false;
    [SerializeField] private KeyCode up = KeyCode.W;
    //[SerializeField] KeyCode down = KeyCode.S;
    [SerializeField] private KeyCode left = KeyCode.A;
    [SerializeField] private KeyCode right = KeyCode.D;

    [Header("Movement values")]
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpPower = 5;

    [Header("Physics")]
    [SerializeField] private float maxFallSpeed = 5;
    [SerializeField] private float groundCheckDistance = 5;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float wallCheckDistance = 1;
    [SerializeField] private LayerMask whatIsObstacle;
    [SerializeField] private CharacterPhysics characterPhysics;

    [Header("Debug")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip debugAudio;
    private float debugTimer;

    //physics
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        debugTimer = Time.time;
    }

    void Update()
    {
        //Timer to get durations of the percurse
        //Debug.Log(Time.time - debugTimer);

        //Check for obstacles
        //from player
        Obstacle();


        //Handle movement inputs
        //from player
        Move();

        //Handle jump inputs
        //from player
        Jump();
    }

    private void Obstacle()
    {
        if (Check_Obstacle())
        {
            GameObject.Destroy(this.gameObject);
        }
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
    public void OnJump(float VerticalDirection)
    {
        if (CanPlayerJump())
        {
            rb.velocity = characterPhysics.GetVerticalVelocity(VerticalDirection * jumpPower);
        }
    }

    public bool Check_Obstacle()
    {
        return Check_GroundLayers(whatIsObstacle) || Check_RaycastHit2D(this.transform.right, wallCheckDistance, whatIsObstacle);
    }

    public bool CanPlayerJump()
    {
        return Check_GroundLayers(whatIsGround, () =>
        {
            Debug.Log("Ground");
        });
    }

    private bool Check_RaycastHit2D(Vector3 direction, float distance, LayerMask layers, out RaycastHit2D hit2D)
    {
        hit2D = Physics2D.Raycast(this.transform.position, direction, distance, layers);
        Debug.DrawRay(this.transform.position, direction * distance);

        return hit2D;
    }
    private bool Check_RaycastHit2D(Vector3 direction, float distance, LayerMask layers)
    {
        RaycastHit2D hit2D;

        hit2D = Physics2D.Raycast(this.transform.position, direction, distance, layers);
        Debug.DrawRay(this.transform.position, direction * distance);

        return hit2D;
    }

    private bool Check_GroundLayers(LayerMask layerMask, Action action = null)
    {
        RaycastHit2D hit2D;

        if (characterPhysics.IsGravityInverted())
        {
            Check_RaycastHit2D(this.transform.up, groundCheckDistance, layerMask, out hit2D);

        }
        else
        {
            Check_RaycastHit2D(-this.transform.up, groundCheckDistance, layerMask, out hit2D);
        }

        if (hit2D)
        {
            action?.Invoke();
            return true;
        }
        return false;
    }

    #endregion
}