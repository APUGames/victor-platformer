using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D playerCharacter;
    Collider2D playerCollider;
    Animator playerAnimator;

    float gravityScaleAtStart;

    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float climbSpeed = 5.0f;

    // Start is called before the first frame update
    private void Start()
    {
        playerCharacter = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        gravityScaleAtStart = playerCharacter.gravityScale;
    }

    // Update is called once per frame
    private void Update()
    {
        Run();
        Jump();
        Climb();
        FlipSprite();
    }

    private void Run()
    {
        // Value between -1 to +1
        float hMovement = Input.GetAxis("Horizontal");
        Vector2 runVelocity = new Vector2(hMovement * runSpeed, playerCharacter.velocity.y);
        playerCharacter.velocity = runVelocity;
        playerAnimator.SetBool("run", true);
        bool hSpeed = Mathf.Abs(playerCharacter.velocity.x) >
        Mathf.Epsilon;
        playerAnimator.SetBool("run", hSpeed);
    }

    private void FlipSprite()
    {
        // If the player is moving horizontally
        bool hMovement = Mathf.Abs(playerCharacter.velocity.x) >
        Mathf.Epsilon;
        if(hMovement)
            {
            // Reverse the current scaling of the X Axis
            transform.localScale = new Vector2
            (Mathf.Sign(playerCharacter.velocity.x), 1f);

            }
    }

    private void Jump()
    {
        if(!playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            // Will stop this function unless true
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            // Get new Y velocity based on a controllable variable
            Vector2 jumpVelocity = new Vector2(0.0f,jumpSpeed);
            playerCharacter.velocity += jumpVelocity;
        }
    }

    private void Climb()
    {
        if(!playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerAnimator.SetBool("climb", false);
            playerCharacter.gravityScale = gravityScaleAtStart;
            return;
        }
        // "Vertical" from Input Axes
        float vMovement = Input.GetAxis("Vertical");
        // X needs to remain the same and we need to change Y
        Vector2 climbVelocity = new
        Vector2(playerCharacter.velocity.x,
        vMovement * climbSpeed);
        playerCharacter.velocity = climbVelocity;
        playerCharacter.gravityScale = 0.0f;
        bool vSpeed = Mathf.Abs(playerCharacter.velocity.y) >
        Mathf.Epsilon;
        playerAnimator.SetBool("climb", vSpeed);
    }
    
}