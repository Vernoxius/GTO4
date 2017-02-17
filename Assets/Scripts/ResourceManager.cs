using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int p1Resource1 = 30;
    public int p1Resource2 = 20;
    public int p1Resource3 = 10;
    public int p2Resource1 = 0;
    public int p2Resource2 = 0;
    public int p2Resource3 = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddResources(int resource1, int resource2, int resource3, int player)
    {
        if (player == 1)
        {
            p1Resource1 += resource1;
            p1Resource2 += resource2;
            p1Resource3 += resource3;
        }
        else if (player == 2)
        {
            p2Resource1 += resource1;
            p2Resource2 += resource2;
            p2Resource3 += resource3;
        }
    }

    public void RemoveResources(int resource1, int resource2, int resource3, int player)
    {
        if (player == 1)
        {
            p1Resource1 -= resource1;
            p1Resource2 -= resource2;
            p1Resource3 -= resource3;
        }
        else if (player == 2)
        {
            p2Resource1 -= resource1;
            p2Resource2 -= resource2;
            p2Resource3 -= resource3;
        }
    }

    public bool CheckForResource(int resourceType, int player, int amount)
    {
        if(player == 1)
        {
            switch (resourceType)
            {
                case 1:
                    if(p1Resource1 >= amount)
                    {
                        return true;
                    }
                    return false;
                case 2:
                    if(p1Resource2 >= amount)
                    {
                        return true;
                    }
                    return false;
                case 3:
                    if(p1Resource3 >= amount)
                    {
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
        else if (player == 2)
        {
            switch (resourceType)
            {
                case 1:
                    if (p2Resource1 >= amount)
                    {
                        return true;
                    }
                    return false;
                case 2:
                    if (p2Resource2 >= amount)
                    {
                        return true;
                    }
                    return false;
                case 3:
                    if (p2Resource3 >= amount)
                    {
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
        return false;
    }    
}
