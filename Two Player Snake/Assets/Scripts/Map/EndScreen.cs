using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour {
    public static EndScreen instance;
    
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI winnerText;

    private List<Snake> deadSnakes = new List<Snake>();

    private void Awake() {
        instance = this;
        endScreen.SetActive(false);
    }


    public void GameOver(Snake loser) {
        deadSnakes.Add(loser);
    }

    private void LateUpdate() {
        if (deadSnakes.Count == 0)
            return;

        if (deadSnakes.Count == 1) {
            Debug.Log(deadSnakes[0].gameObject.name);
            if (deadSnakes[0].gameObject.name.ToLower().Contains("red"))
            {
                winnerText.text = "Blue won!";
            }
            else
            {
                winnerText.text = "Red won";
            }
        } else {
            winnerText.text = "Tie";
        }

        endScreen.SetActive(true);
        Time.timeScale = 0f;
        enabled = false;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackTomapSelect() {
        SceneManager.LoadScene("MapSelector");
    }
}