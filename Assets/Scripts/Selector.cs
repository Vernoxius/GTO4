using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{

    public LayerMask tileMask;
    public Tile tileHit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool RayCastHit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 5);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return true;
        }
        return false;
    }

    public Tile RaycastToTiles()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 5);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileMask))
        {
            return hit.transform.gameObject.GetComponent<Tile>();
        }
        return null;
    }
}
