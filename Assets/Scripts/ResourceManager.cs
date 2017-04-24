using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int p1Wood = 30;
    public int p1Food = 20;
    public int p1Gold = 10;
    public int p2Wood = 0;
    public int p2Food = 0;
    public int p2Gold = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddResources(int wood, int food, int gold, int player)
    {
        if (player == 1)
        {
            p1Wood += wood;
            p1Food += food;
            p1Gold += gold;
        }
        else if (player == 2)
        {
            p2Wood += wood;
            p2Food += food;
            p2Gold += gold;
        }
    }

    public void RemoveResources(int wood, int food, int gold, int player)
    {
        if (player == 1)
        {
            p1Wood -= wood;
            p1Food -= food;
            p1Gold -= gold;
        }
        else if (player == 2)
        {
            p2Wood -= wood;
            p2Food -= food;
            p2Gold -= gold;
        }
    }

    public bool CheckForResource(int wood, int food, int gold, int player)
    {
        if (player == 1)
        {
            if (p1Wood >= wood && p1Food >= food && p1Gold >= gold)
            {
                return true;
            }
            return false;
        }

        else if (player == 2)
        {
            if (p2Wood >= wood && p2Food >= food && p2Gold >= gold)
            {
                return true;
            }
            return false;
        }
        return false;
    }
}
