using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    [SerializeField] float jumpForce;
    AnimatorStateInfo stateInfo;
    public Animator anim;
    float direction;
    public PlayerStatus status;
    public KeyCode jumpInput;
    public KeyCode attackInput;
    public KeyCode runInput;
    public bool hitting;
    public Rigidbody2D rb;
    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        status = PlayerStatus.onAir;
    }

    // Update is called once per frame
    void Update()
    {
        //animations
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("HammerGround"))
        {
            hitting = true;
        }
        else
        {
            hitting = false;
        }
        if (!dead)
        {
            if (!hitting)
            {
                if (direction != 0)
                {
                    anim.SetBool("Walking", true);
                }
                else
                {
                    anim.SetBool("Walking", false);
                }
                if (status == PlayerStatus.onAir)
                {
                    anim.SetBool("Jumping", true);
                }
                else
                {
                    anim.SetBool("Jumping", false);
                }
                if (Input.GetKeyDown(attackInput))
                {
                    anim.SetTrigger("Bonk");
                }

                if (direction > 0)
                {
                    gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
                }
                else if (direction < 0)
                {
                    gameObject.transform.localScale = new Vector2(-0.5f, 0.5f);
                }
            }


            direction = Input.GetAxisRaw("Horizontal");
            if (Input.GetKeyDown(jumpInput) && status != PlayerStatus.onAir)
            {
                // apply a force upward to make the character jump
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                status = PlayerStatus.onAir;
            }
            if (Input.GetKey(runInput))
            {
                speed = 7;
                jumpForce = 15;
                anim.SetBool("Running", true);
            }
            else
            {
                speed = 5;
                jumpForce = 10;
                anim.SetBool("Running", false);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        //constant Gravity
        if (status == PlayerStatus.onAir)
        {
            rb.gravityScale = 6;
            if(rb.velocity.y == 0)
            {
                rb.velocity = new Vector2(0,-5);
            }

        }
        else 
        {
            rb.gravityScale = 0;
        }
        //movement
        if (!stateInfo.IsName("HammerGround"))
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
    }
}
