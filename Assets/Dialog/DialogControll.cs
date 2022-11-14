using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DialogControll : MonoBehaviour {
    [SerializeField] private GameObject dialogInterface; // interface de dialogo
    [SerializeField] private PlayerMove playerMovementScript; // script do player

    // arquivo de dialogo
    private protected string dialogFile; 
    private protected const string FILE_LOCATE = "/Dialog/pastadeteste.csv";

    private protected string[] lines, colluns; // line: linha completa de dialogo / colluns: celulas de dialogo
    [SerializeField] private protected Text dialogText, npcName; // objetos de textos da unity

    private int currentCollun = 0; // celula de dialogo atualmente
    private int currentChar = 0; // numero de caracteres digitados

    private byte write = 0; // define quando iniciara a escrita do texto

    // velocidades
    private protected const float DIALOG_SPEED_NORMAL = 0; 
    private protected const float DIALOG_SPEED_FAST = -50;

    // timer de escrita do dialogo
    private float timeToShow;
    private float timer;

    // Chamado quando o objeto aparece na cena
    private void Start() {
        dialogFile = Application.dataPath + FILE_LOCATE; // define local do arquivo de dialogos
        StreamReader stream = new StreamReader(dialogFile); // coleta dados do arquivo de dialogo
        lines = stream.ReadToEnd().Split('/'); // separa as linhas do arquivo de dialogo e aramazena no array

        EndDialog(); // normaliza todas variveis
    }

    // chamado a cada freame
    private void Update() {
        // caso o comando write inicie e o numero de caracteres esteja dentro do indice de caracteres 
        if(write == 1 && currentChar < colluns[currentCollun].Length) {
            if(timer < timeToShow) timer += Time.deltaTime; // timer
            else {
                dialogText.text += colluns[currentCollun][currentChar]; // soma um caracter
                currentChar++; // atualiza o numero atual de caracteres
                timer = 0; // reseta o timer
            }
        }
        else write = 0; // finaliza o comando laco de escrita quando o numero de caracteres chega ao fim
    }

    // inicia o dialogo por meio do id referente a linha de fala
    public void StartDialog(int dialogID) {
        playerMovementScript.canMov = false; // faz o player não conseguir se movimentar
        dialogInterface.SetActive(true); // ativa a interface

        colluns = lines[dialogID].Split(';'); // separa as celulas de dialogo |0| = nome do npc 
        npcName.text = colluns[0]; // escreve o nome do npc que fala na tela
        WriteDialog(); // inicia escrita do dialogo
    }

    // controla a escrita ou a finalizacao do dialogo
    private void WriteDialog() {
        if(currentCollun < colluns.Length) {
            timeToShow = DIALOG_SPEED_NORMAL; // define velocid
            
            // zera variaveis
            currentChar = 0;
            timer = 0;
            write = 1;
        }
        else EndDialog(); // finaliza o dialogo
    }

    // finaliza o dialgo noralizando as variaveis
    private void EndDialog() {
        dialogText.text = String.Empty; // apaga dialogo
        npcName.text = String.Empty; // apaga nome

        // zera as variaveis
        colluns = new string[0];
        currentCollun = 1;
        write = 0;

        playerMovementScript.canMov = true; // faz o player voltar a se movimentar
        dialogInterface.SetActive(false); // desativa interface
    }

    // referente ao botao de passar dialogo na unity
    public void NextPharse() {
        // verifica se a frase ainda ta sendo escrita
        if(currentChar < colluns[currentCollun].Length) timeToShow = DIALOG_SPEED_FAST; // aumenta a velocidade
        else {
            dialogText.text = String.Empty; // apaga dialogo anterior
            
            // passa para o proximo dialogo
            currentCollun++; 
            WriteDialog();
        }
    }
}