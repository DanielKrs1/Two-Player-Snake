using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour {
    public static EndScreen instance;
    
    [SerializeField] private TextMeshProUGUI winnerText;

    private void Awake() {
        instance = this;
        gameObject.SetActive(false);
    }

    public void GameOver(Snake loser) {
        winnerText.text = loser.gameObject.name + " lost!";
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}