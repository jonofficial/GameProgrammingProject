using UnityEngine;

public class Turtorial : MonoBehaviour {
    [SerializeField] private DialogControll dialogController; // Objeto referente ao controlador de dialogo

    void Start() {
        dialogController.StartDialog(0);
        Destroy(gameObject);
    }
}