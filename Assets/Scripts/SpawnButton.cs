using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour {
    private Player currentPlayer;
    public GameObject unit;

	// Use this for initialization
	void Start () {
        currentPlayer = GameController.maincontroller.currentPlayer;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn()
    {
        if(currentPlayer.resource1 >= 100)
        {
            Instantiate(unit);
        }
        Debug.Log("not enough");
    }
}
