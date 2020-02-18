﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue diaglogue;
    public List<Sprite> portraitSprites;
    [HideInInspector] public bool canTrigger;

    private void Awake()
    {
        canTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canTrigger)
        {
            TriggerDialogue();
            canTrigger = false;
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(diaglogue, portraitSprites);
    }
}
