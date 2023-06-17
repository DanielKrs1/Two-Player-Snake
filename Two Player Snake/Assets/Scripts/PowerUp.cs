using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour
{
    public GameObject prefab;
    public int spawnRangeX;
    public int spawnRangeY;

    abstract public void OnCollisionEnter(Collision collision);

    public void SpawnNewPowerUp(){
        Instantiate(prefab, new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), 0), Quaternion.identity);
    }



}
