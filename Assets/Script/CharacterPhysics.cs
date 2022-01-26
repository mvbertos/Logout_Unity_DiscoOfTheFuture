using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterPhysics : MonoBehaviour
{
    //Physics
    [SerializeField] private Collider2D objectCollider;
    [SerializeField] private Rigidbody2D rb;
    public Collider2D ObjectCollider { get { return objectCollider; } }
    private float defaultGravity;

    //Callbacks
    public delegate void CollisionCallback(Collider2D other);
    public CollisionCallback OnTriggerBegin;
    public CollisionCallback OnTriggerEnd;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
    }

    private void Update()
    {

        //If player bellow 0
        //invert gravity
        IsGravityInverted();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerBegin?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerEnd?.Invoke(other);
    }

    public void OnTriggerEnd_ResetIsTriggerValue(Collider2D other)
    {
        ObjectCollider.isTrigger = false;
        OnTriggerEnd -= OnTriggerEnd_ResetIsTriggerValue;
    }

    public Vector2 GetVerticalVelocity(float movement)
    {
        Vector2 newVelocity = new Vector2();

        //If gravity is inverted player jump will be reversed
        if (IsGravityInverted())
        {
            newVelocity = new Vector2(rb.velocity.x, -movement);
        }
        else
        {
            newVelocity = new Vector2(rb.velocity.x, movement);
        }

        return newVelocity;
    }

    public bool IsGravityInverted()
    {
        bool _invertedGravity;
        if (this.transform.position.y < 0)
        {
            rb.gravityScale = -defaultGravity;
            _invertedGravity = true;
        }
        else
        {
            _invertedGravity = false;
            rb.gravityScale = defaultGravity;
        }
        return _invertedGravity;
    }
}
