using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRangeX;
    public float spawnRangeY;

    protected PowerUp(GameObject prefab, float xRange, float yRange){
        this.prefab = prefab;
        this.spawnRangeX = xRange;
        this.spawnRangeY = yRange;
    }
    
    abstract public void Effect();

    public void SpawnNewPowerUp(){
        Instantiate(prefab, new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), 0), Quaternion.identity);
    }



}
