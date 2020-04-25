using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteLooper : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Image _image;
    [SerializeField] private float _secondsBetweenFrames;

    public void Play()
    {
        StartCoroutine(nameof(loopThroughSprites));
    }

    public void PlayOnce()
    {
        StartCoroutine(nameof(loopThroughSpritesOnce));
    }

    public void Stop()
    {
        StopCoroutine(nameof(loopThroughSprites));
    }

    IEnumerator loopThroughSprites()
    {
        foreach (Sprite sprite in _sprites)
        {
            _image.sprite = sprite;
            yield return new WaitForSeconds(_secondsBetweenFrames);
        }

        StartCoroutine(nameof(loopThroughSprites));
    }

    IEnumerator loopThroughSpritesOnce()
    {
        foreach (Sprite sprite in _sprites)
        {
            _image.sprite = sprite;
            yield return new WaitForSeconds(_secondsBetweenFrames);
        }
    }
}
