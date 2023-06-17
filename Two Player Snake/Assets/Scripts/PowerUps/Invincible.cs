using UnityEngine;
using System.Collections;

public class Invincible : PowerUp {

    private Snake snake;

    public override void OnCollect(Snake snake) {
        snake.SetInvincible();
        this.snake = snake;
        Invoke(nameof(PowerUp), 3);
    }

    public void PowerUp(){
        snake.SetInvincible();
    }
}
