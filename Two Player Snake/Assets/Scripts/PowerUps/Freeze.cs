using UnityEngine;
using System.Collections;

public class Freeze : PowerUp {
    [SerializeField] private Snake red;
    [SerializeField] private Snake blue;
    private Snake snake;
    float speed;

    public override void OnCollect(Snake snake) {
        red = Map.instance.redSnake;
        blue = Map.instance.blueSnake;
        this.snake = snake;
        if(snake.Equals(red)){
            Debug.Log("red");
            blue.Freeze();
            snake = blue;
            Invoke(nameof(PowerUp), 3);
        }else{
            
            Debug.Log("blue");
            red.Freeze();
            snake = red;
            Invoke(nameof(PowerUp), 3);
        }
    }

    void PowerUp(){
        Debug.Log("here2");
        snake.Freeze();
    }
}
