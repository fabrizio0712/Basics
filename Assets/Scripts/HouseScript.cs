using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseScript : MonoBehaviour
{
    public GameObject collider;
    public delegate void _OnlevelComplete();
    public static event _OnlevelComplete OnlevelComplete;

    // Start is called before the first frame update
    void Start()
    {

        collider.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player" && collider.active) 
        {
            OnlevelComplete();
        }
    }
    
    private void OnAllCollectedHandler() 
    {
        collider.SetActive(true);
    }
}
