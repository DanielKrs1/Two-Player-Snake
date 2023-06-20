using UnityEngine;

public class Persist : MonoBehaviour {
    private static Persist instance;

    private void Awake() {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}