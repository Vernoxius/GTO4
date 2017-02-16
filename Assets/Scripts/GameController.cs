using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController maincontroller;
    public GameObject Player1;
    public GameObject Player2;
    public Player player1;
    public Player player2;
    public Player currentPlayer;
    public bool switchTurn = false;
    public bool turnStart = true;
    public Text text;
    


    void Awake()
    {
        maincontroller = this;
    }

    // Use this for initialization
    void Start () {
        player1 = Player1.GetComponent<Player>();
        player2 = Player2.GetComponent<Player>();
        currentPlayer = player1;
    }
	
	// Update is called once per frame
	void Update () {
		if(currentPlayer == player1)
        {
            if (turnStart)
            {
                text.text = "Turn: Player 1";
                player1.resource1 += 30;
                player1.resource2 += 20;
                player1.resource3 += 10;
                turnStart = false;
            }

            if (switchTurn)
            {
                currentPlayer = player2;
                switchTurn = false;
                turnStart = true;
            }
        }

        else if(currentPlayer == player2)
        {
            if (turnStart)
            {
                text.text = "Turn: Player 2";
                player2.resource1 += 30;
                player2.resource2 += 20;
                player2.resource3 += 10;
                turnStart = false;
            }

            if (switchTurn)
            {
                currentPlayer = player1;
                switchTurn = false;
                turnStart = true;
            }
        }
	}
}
