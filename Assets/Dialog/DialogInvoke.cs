using UnityEngine;
[RequireComponent(typeof(Collider2D))]

public class DialogInvoke : MonoBehaviour {
    private protected const string INTERACTIVE_BUTTON_NAME = "Fire1"; // Botao usado para inicar o dialogo

    [SerializeField] private DialogControll dialogController; // Objeto referente ao controlador de dialogo
    [SerializeField] private protected int[] dialogLine; // linha referente ao dialogo do 
    
    [SerializeField] private GameObject interactiveIcon; // icone de interação

    private protected byte stay = 0; // se o jogador esta dentro da area de interação
    private protected int dialogCount = 0; // numero referente ao proximo dialogo no array 

    // chamado a cada freame 
    private void Update() {
        // se o jogador estiver dentro da area e apertar "Fire1"
        if(Input.GetButtonDown(INTERACTIVE_BUTTON_NAME) && stay == 1) {
            // corrige o numero do dialogo caso passe da quantidade de itens do array
            if(dialogCount > dialogLine.Length - 1) dialogCount = dialogLine.Length - 1;

            dialogController.StartDialog(dialogLine[dialogCount]); // incia o dialogo
            interactiveIcon.SetActive(false); // desaparece o icone de interacao
            
            dialogCount++; // passa para proximo item do array
            stay = 0; // o jogador nao podera chamar o dialogo denovo
        }
    }

    // Ao entrar na zona de interacao
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            interactiveIcon.SetActive(true); // aparece o icone de interacao
            stay = 1; // podera interagir e chamar dialogo
        }
    }

    // Ao sair da zona de interacao
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            interactiveIcon.SetActive(false); // desaparece o icone de interacao
            stay = 0; // nao podera interagir e chamar dialogo
        }
    }
}