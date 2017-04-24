using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Renderer _renderer;
    public bool currentlyOccupied = false;
    public bool currentlySelected = false;
    public bool spawning = false;
    public Unit unit;
    Color unselectedColor = new Color(0, 0, 0);
    Color attackingColor = Color.red;
    public int x;
    public int y;
    public bool moving = false;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentlySelected)
        {
            GameManager.gameManager.SelectTile(this);
            currentlySelected = true;
            _renderer.material.color = new Color(1, 1, 1);
        }
    }

    void OnMouseEnter()
    {
            _renderer.material.color = new Color(1, 1, 1);
            GameManager.gameManager.CreateInfoPanel();
    }

    void OnMouseExit()
    {
        if (!GameManager.gameManager.selectedTiles.Contains(this))
        {
            _renderer.material.color = unselectedColor;
            GameManager.gameManager.DestroyInfoPanel();
        }
        else if (!GameManager.gameManager.movementRange.Contains(this) && GameManager.gameManager.attackRange.Contains(this))
        {
            _renderer.material.color = attackingColor;
        }
    }

    public void UnSelect()
    {
        currentlySelected = false;
        moving = false;
        _renderer.material.color = new Color(0, 0, 0);
    }

    public void Select()
    {
            currentlySelected = true;
            _renderer.material.color = new Color(1, 1, 1);
    }

    public void DoneSpawning()
    {
        this.spawning = false;
    }

    public void DoneMoving()
    {
        this.moving = false;
    }

    public void SpawnSelect()
    {
        if (!currentlyOccupied)
        {
            _renderer.material.color = new Color(1, 1, 1);
            spawning = true;
            GameManager.gameManager.selectedTiles.Add(this);
        }
    }

    public void HighlightTile()
    {
        _renderer.material.color = new Color(1, 1, 1);
    }

    public void HighlightAttackTile()
    {
        _renderer.material.color = Color.red;
    }
}
