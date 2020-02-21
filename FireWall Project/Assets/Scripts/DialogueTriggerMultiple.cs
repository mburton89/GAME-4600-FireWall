using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerMultiple : MonoBehaviour
{
    public Dialogue diaglogue;
    public Dialogue diaglouge2;
    public List<Sprite> portraitSprites;
    public List<Sprite> portraitSprites2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(diaglogue, portraitSprites);
    }
}
