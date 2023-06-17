using UnityEngine;

public class Map : MonoBehaviour {
    public Vector2Int size;
    public Vector2Int player1Spawn;
    public Vector2Int player2Spawn;

    private void Start() {
        GameObject newTile = Instantiate(Resources.Load("Square") as GameObject, new Vector3(0, 0), Quaternion.identity);
        newTile.transform.localScale = new Vector3(size.x, size.y, 1);
        Camera.main.orthographicSize = newTile.transform.localScale.y / 2 + 1;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(size.x, size.y));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(player1Spawn.x, player1Spawn.y), 0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(player2Spawn.x, player2Spawn.y), 0.5f);
    }
}