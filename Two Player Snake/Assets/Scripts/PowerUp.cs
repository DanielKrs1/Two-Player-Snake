using UnityEngine;

public abstract class PowerUp : MonoBehaviour {
    public GameObject prefab;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent(out Snake snake)) {
            OnCollect(snake);
            Destroy(gameObject);
        }
    }

    public abstract void OnCollect(Snake snake);

    public static void SpawnNewPowerUp() {
        Vector2 spawnPosition = Map.instance.GetRandomPosition();

        while (Physics2D.OverlapPoint(spawnPosition) != null) {
            spawnPosition = Map.instance.GetRandomPosition();
        }

        //do something to spawn powerup, maybe create probabilities so one is more common than another
    }
}