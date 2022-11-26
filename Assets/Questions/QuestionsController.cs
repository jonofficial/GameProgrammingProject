using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsController : MonoBehaviour  {
    [SerializeField] private GameObject questionInterface; // interface da questao
    [SerializeField] private PlayerMove playerMovementScript; // script do player

    // arquivo de questoes
    private protected string dialogFile;
    private protected const string FILE_LOCATE = "/Questions/pastadeteste.csv";

    private protected string[] lines, colluns; // line: linha completa / colluns: celulas

    [SerializeField] private protected Text questionText; // objetos de textos da unity
    [SerializeField] private protected Dropdown optionsDrop; // objetos de textos da unity

    private protected string[] optionValues; // atuais valores das respostas

    // Chamado quando o objeto aparece na cena
    private void Start() {
        dialogFile = Application.streamingAssetsPath + FILE_LOCATE; // define local do arquivo de dialogos
        StreamReader stream = new StreamReader(dialogFile); // coleta dados do arquivo de dialogo
        lines = stream.ReadToEnd().Split('/'); // separa as linhas do arquivo de dialogo e aramazena no array
    }

    // inicia o dialogo por meio do id referente a linha de fala
    public void ShowQuestion() {
        questionInterface.SetActive(true);
        int dialogID = Random.Range(0, lines.Length);
        playerMovementScript.canMov = false; // faz o player não conseguir se movimentar
        
        colluns = lines[dialogID].Split(';'); // separa as celulas de dialogo
        questionText.text = colluns[0]; // escreve a pergunta

        string[] options = new string[5]; // define as opcoes
        optionValues = new string[5]; // define qual resposta está certa

        for(int i = 1; i < colluns.Length; i++) {
            if(i <= options.Length) options[i - 1] = colluns[i];
            else optionValues[i - options.Length - 1] = colluns[i];
        }

        foreach(string c in options) {
            optionsDrop.options.Add(new Dropdown.OptionData() { text = c });
        }
    }


    // Referente ao Dropdown de respostas do usuario
    public void SelectOption() {
        // verifica resposta do jogador
        switch(optionValues[optionsDrop.value - 1]) {
            // resposta certa
            case "1":
                Debug.Log("certa resposta!!");
                break;

            // resposta quase certa
            case "0,5":
                Debug.Log("voce esta quase certo");
                break;

            // resposta errada
            case "0":
                Debug.Log("resposta errada");
                break;

            default: Debug.LogError(optionValues[optionsDrop.value - 1] + " is a invalid option in"); break;
        }

        EndQuestion();
    }

    private void EndQuestion() {
        questionText.text = string.Empty;
        optionsDrop.ClearOptions(); // limpa o dropdown
        optionsDrop.value = 0;

        questionInterface.SetActive(false);
        playerMovementScript.canMov = true; // faz o player voltar a se movimentar
    }
}