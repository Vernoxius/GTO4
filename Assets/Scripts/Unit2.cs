using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit2 : Unit {

    // Use this for initialization
    void Start ()
    {
        movementRange = 1;
        attackRange = 2;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (startMoving)
        {
            if (pos >= 1)
            {
                startMoving = false;
                doneMoving = true;
                x = newPosition.x;
                y = newPosition.y;
                z = newPosition.z;
            }
            this.transform.position = Vector3.Lerp(new Vector3(x, y, z), newPosition, pos);
            pos += Time.deltaTime;
        }
        else if (doneMoving)
        {
            GameManager.gameManager.UnselectAll();
            GameManager.gameManager.SelectUnit(this);
            doneMoving = false;
        }
    }
}
