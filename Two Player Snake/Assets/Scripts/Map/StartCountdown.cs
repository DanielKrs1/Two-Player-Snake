using System.Collections;
using UnityEngine;
using TMPro;

public class StartCountdown : MonoBehaviour {
    [SerializeField] private GameObject countdownScreen;
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start() {
        Time.timeScale = 0f;
        StartCoroutine(Count());
    }

    private IEnumerator Count() {
        int i = 3;

        while (i > 0) {
            countdownText.text = i.ToString();
            i--;
            yield return new WaitForSecondsRealtime(1f);
        }

        countdownScreen.SetActive(false);
        Time.timeScale = 1f;
    }
}