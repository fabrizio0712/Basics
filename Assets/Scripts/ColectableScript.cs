using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectableScript : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll) 
    {
        if (coll.tag == "Player") 
        {
            anim.SetTrigger("Collect");
        }
    }
    void Disable() 
    {
        gameObject.SetActive(false);
    }
}
