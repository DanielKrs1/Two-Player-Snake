using UnityEngine;

public class Map : MonoBehaviour {
    public static Map instance;

    public new Camera camera;
    public Snake redSnake;
    public Snake blueSnake;
    public Apple app;
    public Canvas gameCanvas;

    public PowerUp[] powerUps;
    public int powerUpCount;
    public int maxPowerUps;
    public int spawnChance = 500;

    public Vector2Int size;
    public Vector2Int redSnakeSpawn;
    public Vector2Int blueSnakeSpawn;

    private void Awake() {
        instance = this;
    }

    private void Start() {       
        Camera mainCamera = Instantiate(camera);
        GameObject newTile = Instantiate(Resources.Load("Square") as GameObject, new Vector3(0, 0), Quaternion.identity);
        SpriteRenderer sp1 = newTile.GetComponent<SpriteRenderer>() as SpriteRenderer;
        sp1.size = new Vector2(size.x, size.y);
        //mainCamera.orthographicSize = newTile.transform.localScale.y / 2 + 1;
        mainCamera.orthographicSize = size.y/2;

        Instantiate(redSnake, new Vector3(redSnakeSpawn.x, redSnakeSpawn.y), Quaternion.identity);
        Instantiate(blueSnake, new Vector3(blueSnakeSpawn.x, blueSnakeSpawn.y), Quaternion.identity);
        Instantiate(gameCanvas);
        app.SpawnNewPowerUp();
    }

    private void FixedUpdate(){
        if(Random.Range(0, spawnChance)==0&&powerUpCount<10){
            powerUps[Random.Range(0, powerUps.Length)].SpawnNewPowerUp();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(size.x, size.y));

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(redSnakeSpawn.x, redSnakeSpawn.y), 0.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(blueSnakeSpawn.x, blueSnakeSpawn.y), 0.5f);
    }

    public Vector2Int GetRandomPosition() {
        int width = (size.x - 1) / 2;
        int height = (size.y - 1) / 2;
        return new Vector2Int(Random.Range(-width, width + 1), Random.Range(-height, height + 1));
    }
}