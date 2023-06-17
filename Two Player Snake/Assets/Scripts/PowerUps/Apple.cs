using UnityEngine;

public class Apple : PowerUp {
    [SerializeField] private int growAmount;

    public override void OnCollect(Snake snake) {
        snake.Grow(growAmount);
        SpawnNewPowerUp();   
    }
}
