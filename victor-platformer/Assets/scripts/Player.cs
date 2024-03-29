using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Rigidbody2D playerCharacter;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;
    Animator playerAnimator;
    float gravityScaleAtStart;
    bool isAlive = true;
    private bool hasJumpedFromWall = false;
    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float climbSpeed = 5.0f;
    [SerializeField] private Vector2 deathSeq = new Vector2(25f, 25f);
    [SerializeField] private float slideSlowdownFactor = 0.5f; // Adjust as needed

    // Start is called before the first frame update
    private void Start()
    {
        playerCharacter = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = playerCharacter.gravityScale;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        Jump();
        FlipSprite();
        Climb();
        Die();

        // Check for wall collision and slow down if necessary
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Walls")))
        {
            SlowDownSlide();
            if (!hasJumpedFromWall && Input.GetButtonDown("Jump"))
            {
                Jump();
                hasJumpedFromWall = true;
            }
        }
        else
        {
            // Reset the flag when the player is not touching the wall anymore
            hasJumpedFromWall = false;
        }
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
        if (hMovement)
        {
            // Reverse the current scaling of the X Axis
            transform.localScale = new Vector2
            (Mathf.Sign(playerCharacter.velocity.x), 1f);

        }
    }

    private void Jump()
    {
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Walls")))
        {
            // Will stop this function unless true
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            // Get new Y velocity based on a controllable variable
            Vector2 jumpVelocity = new Vector2(0.0f, jumpSpeed);
            playerCharacter.velocity += jumpVelocity;
        }
    }

    private void Climb()
    {
        if
        (!playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerAnimator.SetBool("climb", false);
            return;
        }
        // "Vertical" from Input Axes
        float vMovement = Input.GetAxis("Vertical");
        // X needs to remain the same and we need to change Y
        Vector2 climbVelocity = new
        Vector2(playerCharacter.velocity.x,
        vMovement * climbSpeed);
        playerCharacter.velocity = climbVelocity;
        bool vSpeed = Mathf.Abs(playerCharacter.velocity.y) >
        Mathf.Epsilon;
        playerAnimator.SetBool("climb", vSpeed);
    }
    private void SlowDownSlide()
    {
        // Reduce player's horizontal velocity gradually
        playerCharacter.velocity *= slideSlowdownFactor;
    }
    private void Die()
    {
        if (playerBodyCollider.IsTouchingLayers(
        LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            playerAnimator.SetTrigger("die");
            GetComponent<Rigidbody2D>().velocity = deathSeq;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }


}
