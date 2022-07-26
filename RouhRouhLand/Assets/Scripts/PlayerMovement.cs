using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float timeToDie = 2.5f;

    [SerializeField] Vector2 deathKick = new Vector2(0f, 20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] AudioClip shootingProuih;
    [SerializeField] AudioClip dyingProuih;

    float baseGravity;
    bool isAlive = true;
    bool canJump = false;
    int jumpCount = 0;


    Rigidbody2D rb2d;
    Vector2 moveInput;
    CapsuleCollider2D capsuleCollider; //body
    BoxCollider2D boxCollider; //feet

    Animator playerAnimator;
    

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        baseGravity = rb2d.gravityScale;



    }

    private void Update()
    {
        if(!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    // Called by new unity input system
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || canJump) 
        {
            if (value.isPressed)
            {
                canJump = true;
                jumpCount++;
                if(jumpCount > 1) { canJump = false; jumpCount = 0; }
                rb2d.velocity = new Vector2(moveInput.x, jumpSpeed);
            }

            
        }

    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        AudioSource.PlayClipAtPoint(shootingProuih, Camera.main.transform.position);
        Instantiate(bullet, gun.position, transform.rotation);
    }



    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb2d.velocity.y);
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;


        rb2d.velocity = playerVelocity;
        if (playerHasHorizontalSpeed)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), 1f);
        }
        
    }

    void ClimbLadder()
    {
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) { 
            rb2d.gravityScale = baseGravity;
            playerAnimator.SetBool("isClimbing", false);
            return; 
        } 

            Vector2 climbVelocity = new Vector2(rb2d.velocity.x, moveInput.y * climbSpeed);
            rb2d.velocity = climbVelocity;
            rb2d.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(rb2d.velocity.y) > Mathf.Epsilon;

        playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed);




    }

    void Die() {
        if(rb2d.IsTouchingLayers(LayerMask.GetMask("Ennemies", "Hazards")))
        {
            StartCoroutine(StartDying());
        }
    }

    IEnumerator StartDying()
    {
        AudioSource.PlayClipAtPoint(dyingProuih, Camera.main.transform.position);
        isAlive = false;
        playerAnimator.SetTrigger("Dying");
        rb2d.velocity = deathKick;
        yield return new WaitForSecondsRealtime(timeToDie);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}
