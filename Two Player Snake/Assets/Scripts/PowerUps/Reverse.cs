using UnityEngine;

public class Reverse : PowerUp {
    [SerializeField] private Snake red;
    [SerializeField] private Snake blue;
    private Snake snake;

    public override void OnCollect(Snake snake) {
        red = GameObject.Find("Red Snake(Clone)").GetComponent<Snake>();
        blue = GameObject.Find("Blue Snake(Clone)").GetComponent<Snake>();
        this.snake = snake;
        if(Random.Range(0, 99)>24){
            if(snake ==red){
                blue.reverseControls();
            }else{
                red.reverseControls();
            }
        }else{
            snake.reverseControls();
        }
    }
}
