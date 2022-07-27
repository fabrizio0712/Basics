using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audioSource;
    public GameObject manager;
    private bool onfloor = false;
    private bool moveEnable = true;
    private float temporizador = 0;
    public delegate void _OnCherryCollected();
    public static event _OnCherryCollected OnCherryCollected;
    public delegate void _OnJewelCollected();
    public static event _OnJewelCollected OnJewelCollected;
    public delegate void _OnTakenDamage();
    public static event _OnTakenDamage OnTakenDamage;

    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpforce = 10;
    [SerializeField] private float hurtforce = 10;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (moveEnable == false) 
        {
            temporizador += Time.deltaTime;
            if (temporizador > 0.7f)
            {
                temporizador = 0;
                moveEnable = true;
            }
        }
        if(moveEnable) 
        {
            Movement();
            States();
        }
        anim.SetInteger("State", (int)state);
        
    }
    void OnCollisionStay2D(Collision2D collision) 
    {
        if (rb.velocity.y == 0 && !onfloor) 
        {
            onfloor = true;
        }
    }
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "ground" && transform.position.y > collision.GetContact(0).point.y + 0.9f)
        {
            onfloor = true;
        }
        else 
        {
            rb.AddForce(new Vector2(collision.GetContact(0).normal.x*50,50) , ForceMode2D.Impulse);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (state == State.falling && collision.gameObject.transform.position.y + 0.7f < transform.position.y)
            {
                rb.AddForce(new Vector2(0, hurtforce), ForceMode2D.Impulse);
            }
            else 
            {
                state = State.hurt;
                moveEnable = false;
                if (collision.gameObject.transform.position.x > rb.transform.position.x)
                {
                    // enemigo a la derecha, recibir daño y empuje hacia la izquierda
                    rb.AddForce(new Vector2(-hurtforce, hurtforce), ForceMode2D.Impulse);
                }
                else 
                {
                    // enemigo a la izquierda, recibir daño y empuje hacia la derecha
                    rb.AddForce(new Vector2(hurtforce, hurtforce), ForceMode2D.Impulse);
                }
            }
        }
    }
    
    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "DeathZone") 
        {
            state = State.hurt;
            moveEnable = false;
            rb.AddForce(new Vector2(0, hurtforce), ForceMode2D.Impulse);
        }
        if(collider.tag == "Cherry") 
        {
            OnCherryCollected();
        }
        if (collider.tag == "Jewel") 
        {
            OnJewelCollected();
        }
    }
    private void States()
    { 
        if (onfloor) 
        {
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                state = State.idle;
            }
            else 
            {
                state = State.running;
            }
        }
        else
        {
            if (rb.velocity.y > 0f)
            {
                state = State.jumping;
            }
            else if (rb.velocity.y < 0f)
            {
                state = State.falling;
            }
        }
    }
    private void Movement() 
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        if (Input.GetButtonDown("Jump") && onfloor)
        {
            rb.AddForce(new Vector2(0,jumpforce),ForceMode2D.Impulse);
            onfloor = false;
        }
    }
    void Damaged() 
    {
        OnTakenDamage();
    }
    void PlaySound() 
    {
        if (!audioSource.isPlaying) 
        {
            audioSource.Play();
        }
    }
}
