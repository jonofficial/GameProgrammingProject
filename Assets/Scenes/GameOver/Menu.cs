using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public void ReturnToMainMenu() {
        SceneManager.LoadSceneAsync(1);
    }
}