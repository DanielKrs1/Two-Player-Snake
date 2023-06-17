using UnityEngine;
using System.Collections;

public class Teleport : PowerUp {

    public override void OnCollect(Snake snake) {
        snake.Teleport();
    }
}
