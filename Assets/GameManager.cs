using UnityEngine;

public class GameManager : MonoBehaviour {
    public void UnPause() {
        Time.timeScale = 1;
    }

    public void Pause() {
        Time.timeScale = 0;
    }
}