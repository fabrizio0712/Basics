using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public List<GameObject> CheckPoints;
    public List<GameObject> Jewels;
    public List<GameObject> Cherries;
    public GameObject ActualCheckPoint;
    public GameObject Player;
    public GameObject House;
    private int cherries;
    private int jewels;
    private int lives;
    [SerializeField] private Text CherriesCollect;
    [SerializeField] private Text JewelsCollect;
    [SerializeField] private Text Lives;
    

    // Start is called before the first frame update
    void Start()
    {
        cherries = 0;
        jewels = 0;
        lives = 3;
        Respawn();
    }
    // Update is called once per frame
    void Update()
    {
        CherriesCollect.text = cherries.ToString();
        JewelsCollect.text = jewels.ToString();
        Lives.text = lives.ToString();
        if (cherries == Cherries.Count && jewels == Jewels.Count) 
        {
            House.SetActive(true);
        }
    }
    private void OnEnable() 
    {
        CheckPoint.OnSetCheckPoint += OnSetCheckPointHandler;
        HouseScript.OnlevelComplete += OnlevelCompleteHandler;
        PlayerController.OnCherryCollected += OnCherryCollectedHandler;
        PlayerController.OnJewelCollected += OnJewelCollectedHandler;
        PlayerController.OnTakenDamage += OnTakenDamageHandler;
    }
    private void OnDisable()
    {
        CheckPoint.OnSetCheckPoint -= OnSetCheckPointHandler;
        HouseScript.OnlevelComplete -= OnlevelCompleteHandler;
        PlayerController.OnCherryCollected -= OnCherryCollectedHandler;
        PlayerController.OnJewelCollected -= OnJewelCollectedHandler;
        PlayerController.OnTakenDamage -= OnTakenDamageHandler;
    }
    public void Respawn() 
    {
        Player.transform.position = ActualCheckPoint.transform.position + new Vector3(0,2,0);
    }
    void OnSetCheckPointHandler(GameObject go) 
    {
        ActualCheckPoint = go;
    }
    void OnlevelCompleteHandler() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void OnCherryCollectedHandler() 
    {
        cherries++;
    }
    void OnJewelCollectedHandler() 
    {
        jewels++;
    }
    void OnTakenDamageHandler() 
    {
        lives--;
        if (lives <= 0) 
        {
            SceneManager.LoadScene("MainMenu");
        }
        Respawn();
    }
}
