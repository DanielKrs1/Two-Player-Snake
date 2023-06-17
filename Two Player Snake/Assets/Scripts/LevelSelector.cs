using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {
    public void LevelSelect(string toLevel) {
        SceneManager.LoadScene(toLevel);
    }
}