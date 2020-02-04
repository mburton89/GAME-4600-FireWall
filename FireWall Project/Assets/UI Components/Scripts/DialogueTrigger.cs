using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue diaglogue;
    public List<Sprite> portraitSprites;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(diaglogue, portraitSprites);
    }
}
