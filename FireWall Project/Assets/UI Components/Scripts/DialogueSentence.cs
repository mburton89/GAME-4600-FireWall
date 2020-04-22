using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueSentence
{
    public enum FireWallCharacter
    {
        Vlad,
        SALLI,
        Det,
        HectorA,
        SpiderQueen
    }

    public FireWallCharacter characterSpeaking;

    [TextArea(3, 10)]
    public string sentence;
}
