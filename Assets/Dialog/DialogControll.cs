using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DialogControll : MonoBehaviour {
    [SerializeField] private GameObject dialogInterface; // dialog interface
    [SerializeField] private PlayerMove playerMovementScript; // player script

    // dialog file
    private protected string dialogFile; 
    private protected const string FILE_LOCATE = "/Dialog/welcome.csv";

    private protected string[] lines, colluns; // line: complete dialog line / colluns: dialog cells
    [SerializeField] private protected Text dialogText, npcName; // Unity text objects

    private int currentCollun = 0; // current dialog cell
    private int currentChar = 0; // number of typed characters

    private byte write = 0; // defines when text writing will start

    // speeds
    private protected const float DIALOG_SPEED_NORMAL = 0; 
    private protected const float DIALOG_SPEED_FAST = -50;

    // dialog writing timer
    private float timeToShow;
    private float timer;

    // Called when the object appears in the scene
    private void Awake() {
        dialogFile = Application.streamingAssetsPath + FILE_LOCATE; // defines dialog file location
        StreamReader stream = new StreamReader(dialogFile); // fetches data from the dialog file
        lines = stream.ReadToEnd().Split('/'); // separates the lines of the dialog file and stores them in the array
        PrintAllDialogs();
        EndDialog(); // normalizes all variables
    }

    private void PrintAllDialogs() {
        Debug.Log("Printing all dialogs:");

        for (int i = 0; i < lines.Length; i++) {
            string[] colluns = lines[i].Split(';');
            Debug.Log($"Dialog {i}: {string.Join(" | ", colluns)}");
        }
    }

    // called every frame
    private void Update() {
        // if the write command starts and the number of characters is within the character index
        if(write == 1 && currentChar < colluns[currentCollun].Length) {
            if(timer < timeToShow) timer += Time.deltaTime; // timer
            else {
                dialogText.text += colluns[currentCollun][currentChar]; // adds a character
                currentChar++; // updates the current number of characters
                timer = 0; // resets the timer
            }
        }
        else write = 0; // ends the writing loop command when the number of characters reaches the end
    }

    // starts the dialog using the ID referring to the speech line
    public void StartDialog(int dialogID) {
        Time.timeScale = 0; // prevents the player from moving
        dialogInterface.SetActive(true); // activates the interface

        colluns = lines[dialogID].Split(';'); // separates the dialog cells |0| = NPC name 
        npcName.text = colluns[0]; // writes the NPC's name on the screen
        WriteDialog(); // starts dialog writing
    }

    // controls the writing or completion of the dialog
    private void WriteDialog() {
        if(currentCollun < colluns.Length) {
            timeToShow = DIALOG_SPEED_NORMAL; // sets speed
            
            // resets variables
            currentChar = 0;
            timer = 0;
            write = 1;
        }
        else EndDialog(); // ends the dialog
    }

    // ends the dialog by normalizing variables
    private void EndDialog() {
        dialogText.text = String.Empty; // clears the dialog
        npcName.text = String.Empty; // clears the name

        // resets variables
        colluns = new string[0];
        currentCollun = 1;
        write = 0;

        Time.timeScale = 1; // allows the player to move again
        dialogInterface.SetActive(false); // deactivates the interface
    }

    // linked to the button for proceeding with the dialog in Unity
    public void NextPharse() {
        // checks if the phrase is still being written
        if(currentChar < colluns[currentCollun].Length) timeToShow = DIALOG_SPEED_FAST; // increases speed
        else {
            dialogText.text = String.Empty; // clears the previous dialog
            
            // moves to the next dialog
            currentCollun++; 
            WriteDialog();
        }
    }
}
