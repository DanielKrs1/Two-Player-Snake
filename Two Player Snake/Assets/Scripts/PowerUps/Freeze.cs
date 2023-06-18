using UnityEngine;
using System.Collections;

public class Freeze : PowerUp {
    [SerializeField] private Snake red;
    [SerializeField] private Snake blue;
    private Snake snake;
    float speed;

    public override void OnCollect(Snake snake) {
        red = GameObject.FindWithTag("RedSnake").GetComponent<Snake>();
        blue = GameObject.FindWithTag("BlueSnake").GetComponent<Snake>();
        this.snake = snake;
        if(snake.gameObject.name == "Red Snake(Clone)"){
            blue.Freeze();
        }else{
            red.Freeze();
        }
    }
}
