using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour {
    public static EndScreen instance;
    
    [SerializeField] private TextMeshProUGUI winnerText;

    private void Awake() {
        instance = this;
    }

    public void GameOver(Snake loser) {

    }
}