using UnityEngine;
using System.Collections;

public class Reverse : PowerUp {
    [SerializeField] private Snake red;
    [SerializeField] private Snake blue;
    private Snake snake;

    public override void OnCollect(Snake snake) {
        red = Map.instance.redSnake;
        blue = Map.instance.blueSnake;
        this.snake = snake;
        if(Random.Range(0, 99)>24){
            if(snake.Equals(red)){
                blue.ReverseControls();
                snake = blue;
                Invoke(nameof(PowerUp), 3);
            }else{
                red.ReverseControls();
                snake = red;
                Invoke(nameof(PowerUp), 3);
            }
        }else{
            snake.ReverseControls();
            Invoke(nameof(PowerUp), 3);
        }
    }

    public void PowerUp(){
        snake.ReverseControls();
    }
}
