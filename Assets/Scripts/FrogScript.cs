using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    [SerializeField] private float LeftLimit;
    [SerializeField] private float RightLimit;
    [SerializeField] private float jumpforceX = 5;
    [SerializeField] private float jumpforceY = 5;

    private bool facingleft = true;
    private bool moveEnable = true;
    private bool onfloor = false;
    private float temp = 0;

    private enum State { idle, jumping, falling }
    private State state = State.idle;

    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onfloor)
        {
            if (transform.position.x < LeftLimit)
            {
                facingleft = false;
                transform.localScale = new Vector2(-1, 1);
            }
            if (transform.position.x > RightLimit)
            {
                facingleft = true;
                transform.localScale = new Vector2(1, 1);
            }
        }
        if (moveEnable == false)
        {
            temp += Time.deltaTime;
            if (temp >= 1.7f)
            {
                moveEnable = true;
            }
        }
        States();
        anim.SetInteger("State", (int)state);
    }
    void FixedUpdate() 
    {
        if (onfloor && moveEnable) 
        {
            if (facingleft)
            {
                rb.AddForce(new Vector2(-jumpforceX, jumpforceY), ForceMode2D.Impulse);
            }
            else 
            {
                rb.AddForce(new Vector2(jumpforceX, jumpforceY), ForceMode2D.Impulse);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D coll) 
    {
        if(coll.gameObject.tag == "ground") 
        {
            onfloor = true;
            moveEnable = false;
            temp = 0;
            rb.velocity = new Vector2(0, 0);
        }
        if (coll.gameObject.tag == "Player") 
        {
            if(coll.gameObject.transform.position.y > transform.position.y + 0.7f) 
            {
                anim.SetTrigger("Death");
                moveEnable = false;
                rb.velocity = new Vector2(0, 0);
            }
        }
    }
    void OnCollisionExit2D(Collision2D coll) 
    {
        if (coll.gameObject.tag == "ground") 
        {
            onfloor = false;
        }
    }
    private void States() 
    {
        if (onfloor)
        {
            state = State.idle;
        }
        else 
        {
            if (rb.velocity.y > 0)
            {
                state = State.jumping;
            }
            if(rb.velocity.y < 0)
            {
                state = State.falling;
            }
        }
    }
    private void Death() 
    {
        Destroy(this.gameObject);
    }
}
