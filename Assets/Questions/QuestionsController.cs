using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsController : MonoBehaviour  {
    [SerializeField] private GameObject questionInterface; // Question interface
    [SerializeField] private PlayerMove playerMovementScript; // Player movement script

    // Question file
    private protected string dialogFile;
    private protected const string FILE_LOCATE = "/Questions/qns.csv";

    private protected string[] lines, colluns; // line: complete line / colluns: individual cells

    [SerializeField] private protected Text questionText; // Unity text objects
    [SerializeField] private protected Text[] optionsText; // Texts for answer buttons

    private protected string[] optionValues; // Current answer values

    // Called when the object appears in the scene
    private void Start() {
        dialogFile = Application.streamingAssetsPath + FILE_LOCATE; // Defines the location of the dialog file
        StreamReader stream = new StreamReader(dialogFile); // Reads data from the dialog file
        lines = stream.ReadToEnd().Split('/'); // Splits the dialog file into lines and stores them in an array
    }

    // Starts the question based on the ID referring to the line
    public void ShowQuestion() {
        questionInterface.SetActive(true);
        int dialogID = Random.Range(0, lines.Length);
        Time.timeScale = 0; // Prevents the player from moving

        colluns = lines[dialogID].Split(';'); // Splits the dialog cells
        questionText.text = colluns[0]; // Displays the question

        string[] options = new string[5]; // Defines the answer options
        optionValues = new string[5]; // Defines which answer is correct

        for(int i = 1; i < colluns.Length; i++) {
            if(i <= options.Length) options[i - 1] = colluns[i];
            else optionValues[i - options.Length - 1] = colluns[i];
        }

        for(int i = 0; i < options.Length; i++) optionsText[i].text = options[i];
    }

    // Handles the user's answer selection
    public void SelectOption(int idButton) {
        // Checks the player's answer
        switch(optionValues[idButton]) {
            // Correct answer
            case "1":
                playerMovementScript.LifeManager(+500);
                break;

            // Almost correct answer
            case "0,5":
                playerMovementScript.LifeManager(-500);
                break;

            // Wrong answer
            case "0":
                playerMovementScript.LifeManager(-1000);
                break;

            default: 
                Debug.LogError(optionValues[idButton] + " is an invalid option"); 
                break;
        }

        EndQuestion();
    }

    private void EndQuestion() {
        questionText.text = string.Empty;
        for (int i = 0; i < optionsText.Length; i++) optionsText[i].text = string.Empty;

        questionInterface.SetActive(false);
        Time.timeScale = 1; // Allows the player to move again
    }
}
