using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    public int width;
    public int height;
    public Camera mainCamera;

    //public GameObject tileSprite;

    // Start is called before the first frame update.
    void Start()
    {
        
        Map();
    }

    void Map()
    {
        GameObject newTile = Instantiate(Resources.Load("Square") as GameObject, new Vector3(0,0), Quaternion.identity);

        newTile.transform.localScale = new Vector3(width, height, 1);

        mainCamera.orthographicSize = newTile.transform.localScale.y/2 + 1;
    }
}
