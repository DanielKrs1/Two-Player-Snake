using UnityEngine;

public class Speed : PowerUp {
    [SerializeField] private float speedIncrease;

    public override void OnCollect(Snake snake) {
        snake.SpeedIncrease(speedIncrease);
    }
}
