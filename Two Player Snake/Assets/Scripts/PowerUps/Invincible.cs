using UnityEngine;
using System.Collections;

public class Invincible : PowerUp {

    public override void OnCollect(Snake snake) {
        StartCoroutine(powerUp(snake));
    }

    IEnumerator powerUp(Snake snake){
        snake.SetInvincible();
        yield return new WaitForSeconds(3);
        snake.SetInvincible();
    }
}
