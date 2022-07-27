using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : MonoBehaviour
{
    [SerializeField] private float LeftLimit;
    [SerializeField] private float RightLimit;

    private bool facingleft = true;
    private bool moveEnable = true;
    private float speedX = 5;

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
        if (moveEnable) 
        {
            if (facingleft)
            {
                rb.velocity = new Vector2(-speedX, 0);
            }
            else 
            {
                rb.velocity = new Vector2(speedX, 0);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll) 
    {
        if (coll.gameObject.tag == "Player")
        {
            if (coll.gameObject.transform.position.y > transform.position.y + 0.7f)
            {
                anim.SetTrigger("Death");
                moveEnable = false;
                rb.velocity = new Vector2(0, 0);
            }
        }
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
