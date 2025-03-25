using UnityEngine;
[RequireComponent(typeof(Collider2D))]

public class DialogInvoke : MonoBehaviour {
    private protected const string INTERACTIVE_BUTTON_NAME = "Fire1"; // Button used to start the dialog

    [SerializeField] private DialogControll dialogController; // Reference to the dialog controller object
    [SerializeField] private protected int[] dialogLine; // Line referencing the dialog

    [SerializeField] private GameObject interactiveIcon; // Interaction icon

    private protected byte stay = 0; // If the player is inside the interaction area
    private protected int dialogCount = 0; // Number referencing the next dialog in the array 

    // Called every frame
    private void Update() {
        // If the player is inside the area and presses "Fire1"
        if(Input.GetButtonDown(INTERACTIVE_BUTTON_NAME) && stay == 1) {
            // Corrects the dialog number if it exceeds the number of items in the array
            if(dialogCount > dialogLine.Length - 1) dialogCount = dialogLine.Length - 1;

            dialogController.StartDialog(dialogLine[dialogCount]); // Starts the dialog
            interactiveIcon.SetActive(false); // Hides the interaction icon

            dialogCount++; // Moves to the next item in the array
            stay = 0; // The player won't be able to trigger the dialog again
        }
    }

    // When entering the interaction zone
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            interactiveIcon.SetActive(true); // Shows the interaction icon
            stay = 1; // Player can interact and trigger dialog
        }
    }

    // When leaving the interaction zone
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            interactiveIcon.SetActive(false); // Hides the interaction icon
            stay = 0; // Player cannot interact and trigger dialog
        }
    }
}
