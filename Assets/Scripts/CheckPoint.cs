using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public delegate void _OnSetCheckPoint(GameObject go);
    public static event _OnSetCheckPoint OnSetCheckPoint;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnSetCheckPoint(gameObject);
        }
    }
}
