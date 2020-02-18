using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private string _currentName;

    [SerializeField] private Button _next;
    [SerializeField] private GameObject _dialogueBox;

    [SerializeField] private UISpriteLooper _salliPortrait;
    [SerializeField] private UISpriteLooper _vladPortrait;
    private UISpriteLooper _currentPortrait;

    private Queue<DialogueSentence> sentences;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        sentences = new Queue<DialogueSentence>();
        _dialogueBox.SetActive(false);
        _next.onClick.AddListener(DisplayNextSentence);
    }

    public void StartDialogue(Dialogue dialogue, List<Sprite> portraitSprites)
    {
        _dialogueBox.SetActive(true);
        sentences.Clear();

        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            sentences.Enqueue(dialogue.sentences[i]);
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

        DialogueSentence sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.sentence));
        EstablishCharacter(sentence.characterSpeaking);
    }

    void EndDialogue()
    {
        _salliPortrait.Stop();
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

    void EstablishCharacter(DialogueSentence.FireWallCharacter characterSpeaking)
    {
        if (_currentPortrait != null)
        {
            _currentPortrait.Stop();
            _currentPortrait.gameObject.SetActive(false);
        }

        if (characterSpeaking == DialogueSentence.FireWallCharacter.SALLI)
        {
            _currentName = "S.A.L.L.I.";
            _currentPortrait = _salliPortrait;
        }
        else if (characterSpeaking == DialogueSentence.FireWallCharacter.Vlad)
        {
            _currentName = "Vlad";
            _currentPortrait = _vladPortrait;
        }

        nameText.text = _currentName;
        _currentPortrait.gameObject.SetActive(true);
        _currentPortrait.Play();
    }
}
