using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;
    public ResourceManager resourceManager;
    public GameObject unit1;
    public GameObject unit2;
    public int currentPlayer = 1;
    public Text text;
    private bool allowedToSpawn;

    void Awake()
    {
        gameManager = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeTurn()
    {
        if (currentPlayer == 1)
        {
            currentPlayer = 2;
            text.text = "Turn: Player 2";
        }
        else
        {
            currentPlayer = 1;
            text.text = "Turn: Player 1";
        }
        resourceManager.AddResources(30, 20, 10, currentPlayer);
    }

    public void spawnUnit1()
    {
        allowedToSpawn = resourceManager.CheckForResource(1, currentPlayer, 100);

        if (allowedToSpawn)
        {
            Instantiate(unit1);
            resourceManager.RemoveResources(100, 0, 0, currentPlayer);
        }
    }

    public void spawnUnit2()
    {
        allowedToSpawn = resourceManager.CheckForResource(3, currentPlayer, 100);

        if (allowedToSpawn)
        {
            Instantiate(unit2);
            resourceManager.RemoveResources(0, 0, 100, currentPlayer);
        }
        
    }
}
