using UnityEngine;

public class Freeze : PowerUp {
    [SerializeField] private Snake red;
    [SerializeField] private Snake blue;

    public override void OnCollect(Snake snake) {
        red = GameObject.FindWithTag("RedSnake").GetComponent<Snake>();
        blue = GameObject.FindWithTag("BlueSnake").GetComponent<Snake>();

        if(snake.gameObject.name == "Red Snake(Clone)"){
            blue.Freeze();
        }else{
            red.Freeze();
        }
    }
}
