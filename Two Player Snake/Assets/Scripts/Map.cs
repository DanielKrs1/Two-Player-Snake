using UnityEngine;

public class Map : MonoBehaviour {
    public GameObject redSnake;
    public GameObject blueSnake;
    public GameObject gameCanvas;

    public Vector2Int size;
    public Vector2Int redSnakeSpawn;
    public Vector2Int blueSnakeSpawn;

    private void Start() {
        GameObject newTile = Instantiate(Resources.Load("Square") as GameObject, new Vector3(0, 0), Quaternion.identity);
        newTile.transform.localScale = new Vector3(size.x, size.y, 1);
        Camera.main.orthographicSize = newTile.transform.localScale.y / 2 + 1;

        Instantiate(redSnake, new Vector3(redSnakeSpawn.x, redSnakeSpawn.y), Quaternion.identity);
        Instantiate(blueSnake, new Vector3(blueSnakeSpawn.x, blueSnakeSpawn.y), Quaternion.identity);
        Instantiate(gameCanvas);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(size.x, size.y));

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(redSnakeSpawn.x, redSnakeSpawn.y), 0.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(blueSnakeSpawn.x, blueSnakeSpawn.y), 0.5f);
    }
}