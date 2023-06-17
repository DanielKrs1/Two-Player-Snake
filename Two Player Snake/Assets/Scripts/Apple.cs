using UnityEngine;

public class Apple : PowerUp {
    [SerializeField] private int growAmonut;

    public override void OnCollect(Snake snake) {
        snake.Grow(growAmonut);
        SpawnNewPowerUp();   
    }
}
