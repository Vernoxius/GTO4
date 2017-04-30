using System;
using System.Collections;
using UnityEngine;
using Priority_Queue;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public int wood;
    public int food;
    public int gold;
    public int player;
    public GameObject unit;
    public ResourceManager resourceManager;
    public int movementRange;
    public int attackRange;
    public float x;
    public float y;
    public float z;
    public int iterationCounter = 0;
    public LayerMask layerMask;
    public bool allowedToMove = true;
    public bool allowedToAttack = true;
    public bool moving = false;
    public bool attacking = false;
    public bool startMoving = false;
    public bool doneMoving = false;
    public Vector3 newPosition;
    public float pos = 0;


    void Update()
    {
        
    }

    public void SelectUnit()
    {
        GameManager.gameManager.SelectUnit(this);
    }

    public void Move(Vector3 newPosition)
    {
        pos = 0;
        startMoving = true;
        this.newPosition = newPosition;
        this.moving = false;
        this.attacking = false;
        this.allowedToMove = false;
    }

    public List<Tile> calculateMovementRange()
    {
        iterationCounter = 0;
        List<Tile> result = new List<Tile>();
        Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position, movementRange, layerMask);
        foreach(Collider c in colliders)
        {
            result.Add(c.gameObject.GetComponent<Tile>());
            GameManager.gameManager.selectedTiles.Add(c.gameObject.GetComponent<Tile>());
        }

        return result;
    }

    public List<Tile> calculateAttackRange()
    {
        Collider[] colliders;
        iterationCounter = 0;
        List<Tile> result = new List<Tile>();
        if (allowedToMove)
        {
            colliders = Physics.OverlapSphere(this.gameObject.transform.position, movementRange + attackRange, layerMask);
        }
        else
        {
            colliders = Physics.OverlapSphere(this.gameObject.transform.position, attackRange, layerMask);
        }
        foreach (Collider c in colliders)
        {
            result.Add(c.gameObject.GetComponent<Tile>());
            GameManager.gameManager.selectedTiles.Add(c.gameObject.GetComponent<Tile>());
        }

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, movementRange);
    }
}
