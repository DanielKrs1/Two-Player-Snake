using UnityEngine;

public class MapCreate : MonoBehaviour {
    public int width;
    public int height;

    private void Start() {
        GameObject newTile = Instantiate(Resources.Load("Square") as GameObject, new Vector3(0, 0), Quaternion.identity);

        newTile.transform.localScale = new Vector3(width, height, 1);

        Camera.main.orthographicSize = newTile.transform.localScale.y / 2 + 1;
    }
}