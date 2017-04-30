using Assets.Scripts.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;
    public ResourceManager resourceManager;
    public GridManager gridManager;
    public Selector selector;
    public Unit1 unit1;
    public Unit2 unit2;
    public int currentPlayer = 1;
    public Text text;
    private bool allowedToSpawn;
    public GameObject canvas;
    public GameObject currentSelectedTile;
    public GameObject currentSelectedUnit;
    public Unit unitIns;
    public int numberOfUnit1;
    public int numberOfUnit2;
    public bool spawn1Ready = false;
    public bool spawn2Ready = false;
    public List<Tile> selectedTiles;
    public List<Unit> listOfUnits;
    public List<Tile> movementRange;
    public List<Tile> attackRange;


    void Awake()
    {
        gameManager = this;
    }

    // Use this for initialization
    void Start()
    {
        selectedTiles = new List<Tile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Tile tileHit = null;
            tileHit = selector.RaycastToTiles();

            if (tileHit != null)
            {
                
                if (spawn1Ready || spawn2Ready)
                {
                    if (tileHit.spawning && !tileHit.currentlyOccupied)
                    {
                        if (currentPlayer == 1 && (tileHit.name.Contains("Tile_0") || tileHit.name.Contains("Tile_1")))
                        {
                            SpawnUnit(tileHit);
                            tileHit.currentlyOccupied = true;
                            tileHit.spawning = false;
                        }
                        else if (currentPlayer == 2 && (tileHit.name.Contains("Tile_6") || tileHit.name.Contains("Tile_7")))
                        {
                            SpawnUnit(tileHit);
                            tileHit.currentlyOccupied = true;
                            tileHit.spawning = false;
                        }

                    }
                    else
                    {
                        tileHit.spawning = false;
                        spawn1Ready = false;
                        spawn2Ready = false;
                    }
                }

                else if (currentSelectedUnit != null && currentSelectedUnit.GetComponent<Unit>().moving && currentSelectedUnit.GetComponent<Unit>().allowedToMove)
                {
                    if (movementRange.Contains(tileHit) && !tileHit.currentlyOccupied)
                    {
                        currentSelectedTile.GetComponent<Tile>().currentlyOccupied = false;
                        tileHit.unit = currentSelectedTile.GetComponent<Tile>().unit;

                        currentSelectedUnit.GetComponent<Unit>().Move(tileHit.transform.position);

                        tileHit.currentlyOccupied = true;
                        currentSelectedTile.GetComponent<Tile>().unit = null;

                        UnselectAll();
                    }

                    
                }

                if (currentSelectedUnit != null && currentSelectedUnit.GetComponent<Unit>().attacking && currentSelectedUnit.GetComponent<Unit>().allowedToAttack)
                {
                    attackRange = currentSelectedUnit.GetComponent<Unit>().calculateAttackRange();
                    if (attackRange.Contains(tileHit) && tileHit.currentlyOccupied && tileHit.unit.player != currentPlayer)
                    {
                        Debug.Log("Pang");
                        currentSelectedUnit.GetComponent<Unit>().attacking = false;
                        currentSelectedUnit.GetComponent<Unit>().allowedToAttack = false;
                        Destroy(tileHit.unit.gameObject);
                        listOfUnits.Remove(tileHit.unit);
                        tileHit.currentlyOccupied = false;

                        UnselectAll();
                    }
                }

                if (currentSelectedTile != null)
                {
                    currentSelectedTile.GetComponent<Tile>().UnSelect();
                    selectedTiles.Remove(currentSelectedTile.GetComponent<Tile>());
                    currentSelectedTile = null;
                    currentSelectedUnit = null;
                    UnselectAll();
                }


                if (tileHit.unit != null)
                {
                    tileHit.unit.SelectUnit();
                    tileHit.Select();
                }
                else
                {
                    currentSelectedTile = tileHit.gameObject;
                    currentSelectedTile.GetComponent<Tile>().Select();
                }
            }
            else
            {
                UnselectAll();
            }


        }

        if (Input.GetMouseButtonDown(1))
        {
            GameManager.gameManager.UnselectAll();
        }
        /*if(canvas.active == true)
        {
            canvas.transform.position = Input.mousePosition;
        }*/
    }

    public void ChangeTurn()
    {
        UnselectAll();
        if (currentPlayer == 1)
        {
            currentPlayer = 2;
            foreach (Unit u in listOfUnits)
            {
                if (u.player == 2)
                {
                    u.allowedToMove = true;
                    u.allowedToAttack = true;
                }
                else
                {
                    u.allowedToMove = false;
                    u.allowedToAttack = false;
                }
            }
            text.text = "Turn: Player 2";
        }
        else
        {
            currentPlayer = 1;
            foreach (Unit u in listOfUnits)
            {
                if (u.player == 1)
                {
                    u.allowedToMove = true;
                    u.allowedToAttack = true;
                }
                else
                {
                    u.allowedToMove = false;
                    u.allowedToAttack = false;
                }
            }
            text.text = "Turn: Player 1";
        }
        resourceManager.AddResources(30, 20, 10, currentPlayer);
    }

    public void SpawnCheck(Unit unit)
    {
        allowedToSpawn = resourceManager.CheckForResource(unit.wood, unit.food, unit.gold, currentPlayer);

        if (allowedToSpawn)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int i1 = 0; i1 < 8; i1++)
                {
                    if ((gridManager.tileList[i, i1] == gridManager.tileList[0, i1] || gridManager.tileList[i, i1] == gridManager.tileList[1, i1]) && currentPlayer == 1)
                    {
                        selectedTiles.Add(gridManager.tileList[i, i1].GetComponent<Tile>());
                        gridManager.tileList[i, i1].GetComponent<Tile>().SpawnSelect();
                    }
                    else if ((gridManager.tileList[i, i1] == gridManager.tileList[6, i1] || gridManager.tileList[i, i1] == gridManager.tileList[7, i1]) && currentPlayer == 2)
                    {
                        selectedTiles.Add(gridManager.tileList[i, i1].GetComponent<Tile>());
                        gridManager.tileList[i, i1].GetComponent<Tile>().SpawnSelect();
                    }
                }
            }
        }
    }
    public void SpawnUnit(Tile tile)
    {
        if (spawn1Ready)
        {
            UnselectAll();
            unitIns = Instantiate(unit1, tile.transform.position, Quaternion.identity);
            tile.unit = unitIns;
            tile.unit.x = tile.x;
            tile.unit.z = tile.y;
            unitIns.name = "unit1_" + numberOfUnit1++;
            unitIns.player = currentPlayer;
            listOfUnits.Add(unitIns);
            resourceManager.RemoveResources(unit1.wood, unit1.food, unit1.gold, currentPlayer);
            spawn1Ready = false;
        }
        else if (spawn2Ready)
        {
            UnselectAll();
            unitIns = Instantiate(unit2, tile.transform.position, Quaternion.identity);
            tile.unit = unitIns;
            tile.unit.x = tile.x;
            tile.unit.z = tile.y;
            unitIns.name = "unit2_" + numberOfUnit2++;
            unitIns.player = currentPlayer;
            listOfUnits.Add(unitIns);
            resourceManager.RemoveResources(unit2.wood, unit2.food, unit2.gold, currentPlayer);
            spawn2Ready = false;
        }
        UnselectAll();
    }

    public void CreateInfoPanel()
    {
        //canvas.SetActive(true);
    }

    public void DestroyInfoPanel()
    {
        //canvas.SetActive(false);
    }

    public void SelectTile(Tile tile)
    {
        if (currentSelectedTile != null)
        {
            UnSelectTile();
        }
        currentSelectedTile = tile.gameObject;
        if (!selectedTiles.Contains(tile))
        {
            selectedTiles.Add(tile);
        }
    }

    public void UnSelectTile()
    {
        if (currentSelectedTile.GetComponent<Tile>() is Tile)
        {
            currentSelectedTile.GetComponent<Tile>().UnSelect();
            selectedTiles.Remove(currentSelectedTile.GetComponent<Tile>());
        }
        currentSelectedTile = null;
    }

    public void SelectUnit(Unit unit)
    {
        if (unit.player == currentPlayer)
        {
            currentSelectedUnit = unit.gameObject;

            if (unit.allowedToAttack)
            {
                attackRange = unit.calculateAttackRange();
                unit.attacking = true;
                foreach (Tile t in attackRange)
                {
                    if (t != null)
                    {
                        selectedTiles.Add(t.GetComponent<Tile>());
                        t.GetComponent<Tile>().HighlightAttackTile();
                    }
                }
            }

            if (unit.allowedToMove)
            {
                movementRange = unit.calculateMovementRange();
                unit.moving = true;
                foreach (Tile t in movementRange)
                {
                    if (t != null)
                    {
                        selectedTiles.Add(t.GetComponent<Tile>());
                        t.GetComponent<Tile>().HighlightTile();
                    }
                }
            }
        }
        //highlight Unit
    }

    public void UnSelectUnit()
    {
        currentSelectedUnit = null;
        //remove Unit highlight
    }

    public void UnselectAll()
    {
        foreach (GameObject t in gridManager.tileList)
        {
            Tile tile = t.GetComponent<Tile>();
            tile.DoneMoving();
            tile.DoneSpawning();
            tile.UnSelect();
        }
        selectedTiles.Clear();
        currentSelectedTile = null;
        currentSelectedUnit = null;
        spawn1Ready = false;
        spawn2Ready = false;
        movementRange.Clear();
    }
}