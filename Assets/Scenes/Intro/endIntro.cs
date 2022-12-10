using UnityEngine;
using UnityEngine.SceneManagement;

public class endIntro : MonoBehaviour {
    public bool end = false;
    void Update() {
        if(end == true && Input.anyKeyDown) SceneManager.LoadSceneAsync(1);
    }
}
