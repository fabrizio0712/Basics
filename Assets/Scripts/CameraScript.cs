using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Rigidbody2D PlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(PlayerPos.transform.position.x, PlayerPos.transform.position.y + 1.5F, gameObject.transform.position.z);
    }
}
