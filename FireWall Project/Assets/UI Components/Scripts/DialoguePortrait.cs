using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePortrait : MonoBehaviour
{
    private List<Sprite> _portraitSprites;
    [SerializeField] private Image _portrait;
    [SerializeField] private float _secondsBetweenFrames;

    public void Init(List<Sprite> portraitSprites)
    {
        _portraitSprites = portraitSprites;
    }

    public void Play()
    {
        Stop();
        StartCoroutine(nameof(loopThroughPortraitSprites));
    }

    public void Stop()
    {
        StopCoroutine(nameof(loopThroughPortraitSprites));
    }

    IEnumerator loopThroughPortraitSprites()
    {
        foreach (Sprite portraitSprite in _portraitSprites)
        {
            _portrait.sprite = portraitSprite;
            yield return new WaitForSeconds(_secondsBetweenFrames);
        }
    }
}
