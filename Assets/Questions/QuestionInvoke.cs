using UnityEngine;

public class QuestionInvoke : MonoBehaviour {
    private QuestionsController questionsController; // Objeto referente ao controlador de questoes
    private const string QUESTION_CONTROLLER_OBJ = "QuestionsControll";
    private const string PLAYER_TAG = "Player";

    void Start() {
        questionsController = GameObject.Find(QUESTION_CONTROLLER_OBJ).GetComponent<QuestionsController>();
    }

    // Ao entrar na zona de interacao
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag(PLAYER_TAG)) {
            questionsController.ShowQuestion();
            Destroy(this.gameObject);
        }
    }
}