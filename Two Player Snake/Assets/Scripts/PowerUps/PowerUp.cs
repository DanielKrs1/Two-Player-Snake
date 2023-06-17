using UnityEngine;

public abstract class PowerUp : MonoBehaviour {
    public GameObject prefab;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Snake snake)) {
            OnCollect(snake);
            Map.instance.powerUpCount-=1;
            Destroy(gameObject);
        }
    }

    public abstract void OnCollect(Snake snake);

    public void SpawnNewPowerUp() {
        Vector2 spawnPosition = Map.instance.GetRandomPosition();
        Map.instance.powerUpCount+=1;

        while (Physics2D.OverlapPoint(spawnPosition) != null) {
            spawnPosition = Map.instance.GetRandomPosition();
        }
        //Vector2 spawnPosition = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(8.0f, 8.0f));
        Instantiate(prefab, new Vector3(spawnPosition.x, spawnPosition.y), Quaternion.identity);
        //do something to spawn powerup, maybe create probabilities so one is more common than another
    }
}