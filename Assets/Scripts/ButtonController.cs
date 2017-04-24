using Assets.Scripts.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {
    public Unit1 unit1;
    public Unit2 unit2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnUnit1()
    {
        GameManager.gameManager.spawn1Ready = true;
        GameManager.gameManager.SpawnCheck(unit1);
    }

    public void SpawnUnit2()
    {
        GameManager.gameManager.spawn2Ready = true;
        GameManager.gameManager.SpawnCheck(unit2);
    }
}
