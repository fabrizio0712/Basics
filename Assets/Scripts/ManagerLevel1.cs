using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerLevel1 : MonoBehaviour
{
    public List<GameObject> CheckPoints;
    public List<GameObject> Cherries;
    public GameObject ActualCheckPoint;
    public GameObject Player;
    private int cherries;
    private int lives;
    [SerializeField] private Text CherriesCollect;
    [SerializeField] private Text Lives;
    public delegate void _OnAllCollected();
    public static event _OnAllCollected OnAllCollected;

    // Start is called before the first frame update
    void Start()
    {
        cherries = 0;      
        lives = 3;
        Respawn();
    }
    // Update is called once per frame
    void Update()
    {
        CherriesCollect.text = cherries.ToString();
        Lives.text = lives.ToString();
        if (cherries == Cherries.Count)
        {
            OnAllCollected();
        }
    }
    private void OnEnable()
    {
        CheckPoint.OnSetCheckPoint += OnSetCheckPointHandler;
        HouseScript.OnlevelComplete += OnlevelCompleteHandler;
        PlayerController.OnCherryCollected += OnCherryCollectedHandler;        
        PlayerController.OnTakenDamage += OnTakenDamageHandler;
    }
    private void OnDisable()
    {
        CheckPoint.OnSetCheckPoint -= OnSetCheckPointHandler;
        HouseScript.OnlevelComplete -= OnlevelCompleteHandler;
        PlayerController.OnCherryCollected -= OnCherryCollectedHandler;
        PlayerController.OnTakenDamage -= OnTakenDamageHandler;
    }
    public void Respawn()
    {
        Player.transform.position = ActualCheckPoint.transform.position + new Vector3(0, 2, 0);
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
