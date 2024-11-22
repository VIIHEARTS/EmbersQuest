using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    
    public static DialogueManager Instance;


    public Image characterIcon;
    public TextMeshProUGUI dialogueArea;

    public RectTransform dialogueBox;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.2f;

    
    private void Start()
    {

        lines = new Queue<DialogueLine>();

        if (Instance == null)
            Instance = this;

        dialogueBox.transform.localScale = Vector3.zero;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;

        dialogueBox.LeanScale(Vector3.one, 0.3f).setEaseInOutExpo();

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();

    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }        
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.LeanScale(Vector3.zero, 0.3f).setEaseInOutExpo();
    }
}
