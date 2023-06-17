using UnityEngine;
using System.Collections;

public class Reverse : PowerUp {
    [SerializeField] private Snake red;
    [SerializeField] private Snake blue;

    public override void OnCollect(Snake snake) {
        if(Random.Range(0, 99)>24){
            if(snake.Equals(red)){
                StartCoroutine(powerUp(blue));
            }else{
                StartCoroutine(powerUp(red));
            }
        }else{
            StartCoroutine(powerUp(snake));
        }
    }

    IEnumerator powerUp(Snake snake){
        snake.reverseControls();
        yield return new WaitForSeconds(10);
        snake.reverseControls();
    }
}
