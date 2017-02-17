using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static List<string> LoadedItems;
    public bool DestroyOnEndGame;

    // Use this for initialization
    void Awake()
    {
        if (LoadedItems == null)
            LoadedItems = new List<string>();

        if (LoadedItems.Contains(gameObject.name))
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(transform.gameObject);
            LoadedItems.Add(gameObject.name);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
