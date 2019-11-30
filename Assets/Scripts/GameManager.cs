using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameCamera))]
public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject blockPrefab;
    public GameObject[] spawnPoints;
    public GameObject[] spawnBlocks;

    public static GameManager instance;
    private GameCamera cam;
    public GameObject playerReference;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
        for (int i = 0; i < spawnBlocks.Length; i++)
        {
            Instantiate(blockPrefab, spawnBlocks[i].transform.position, Quaternion.identity);
        }

    }

    void Start()
    {
        cam = GetComponent<GameCamera>();

        SpawnPlayer();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
        //AudioManager.playSound("EnterLevel");
        playerReference = Instantiate(player, spawnPoints[0].transform.position, Quaternion.identity);
        cam.SetTarget((playerReference).transform);
    }



    public void Death()
    {
        //AudioManager.playSound("Death");
        Time.timeScale = 1f;
        //Invoke("loadActualScene", 4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UIManager.currentUIManager.updateBlockNumber(0);
    }

    public void Win()
    {
        //TODO Show some fancy text (Set WinningText to enabled oder so)
        //Text winningText = FindObjectOfType<Text>();
        //winningText.enabled = true;
        AudioManager.playSound("Winning");
        //Debug.Log("Winning condition");
        Invoke("nextLevel", 4);
        Time.timeScale = 1f;
        UIManager.currentUIManager.updateBlockNumber(0);
    }

    private void nextLevel()
    {
        SceneManager.LoadScene("Level_2");
    }

    /**private void loadActualScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/
}
