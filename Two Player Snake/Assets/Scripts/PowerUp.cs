using UnityEngine;

public abstract class PowerUp : MonoBehaviour {
    public GameObject prefab;

    public abstract void OnCollisionEnter(Collision collision);

    public static void SpawnNewPowerUp() {
        Vector2 spawnPosition = Map.instance.GetRandomPosition();

        while (Physics2D.OverlapPoint(spawnPosition) != null) {
            spawnPosition = Map.instance.GetRandomPosition();
        }

        //do something to spawn powerup, maybe create probabilities so one is more common than another
    }
}