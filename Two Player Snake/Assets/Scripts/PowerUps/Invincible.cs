using UnityEngine;
using System.Collections;

public class Invincible : PowerUp {

    private Snake snake;

    public override void OnCollect(Snake snake) {
        snake.SetInvincible();
    }

}
