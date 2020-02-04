using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private DialoguePortrait _dialoguePortrait;

    private Queue<string> sentences;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        sentences = new Queue<string>();

        _dialogueBox.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue, List<Sprite> portraitSprites)
    {
        _dialogueBox.SetActive(true);
        _dialoguePortrait.Init(portraitSprites);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        _dialoguePortrait.Play();
    }

    void EndDialogue()
    {
        _dialogueBox.SetActive(false);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
