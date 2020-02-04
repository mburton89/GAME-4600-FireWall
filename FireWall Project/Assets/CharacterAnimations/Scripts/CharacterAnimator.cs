using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public float idleFrameRate;
    public float runFrameRate;
    public float jumpFrameRate;
    private float _activeFrameRate;

    public List<Sprite> idleSprites;
    public List<Sprite> runSprites;
    public List<Sprite> jumpSprites;
    private List<Sprite> _activeSpriteList;

    public SpriteRenderer activeFrame;
    private int _frameIndex;

    public enum CharacterAnimationState {idle, run, jump};
    [HideInInspector] public CharacterAnimationState activeAnimationState;

    private void Start()
    {
        PlayIdleAnimation();
    }

    public void PlayIdleAnimation()
    {
        StopCoroutine(nameof(playAnimationCo));
        _activeFrameRate = idleFrameRate;
        _activeSpriteList = idleSprites;
        _frameIndex = 0;
        StartCoroutine(nameof(playAnimationCo));
        activeAnimationState = CharacterAnimationState.idle;
    }

    public void PlayRunAnimation()
    {
        StopCoroutine(nameof(playAnimationCo));
        _activeFrameRate = runFrameRate;
        _activeSpriteList = runSprites;
        _frameIndex = 0;
        StartCoroutine(nameof(playAnimationCo));
        activeAnimationState = CharacterAnimationState.run;
    }

    public void PlayJumpAnimation()
    {
        StopCoroutine(nameof(playAnimationCo));
        _activeFrameRate = jumpFrameRate;
        _activeSpriteList = jumpSprites;
        _frameIndex = 0;
        StartCoroutine(nameof(playAnimationCo));
        activeAnimationState = CharacterAnimationState.jump;
    }

    private IEnumerator playAnimationCo()
    {
        ShowNextFrame();
        yield return new WaitForSeconds(_activeFrameRate);
        StartCoroutine(nameof(playAnimationCo));
    }

    void ShowNextFrame()
    {
        if (_frameIndex >= _activeSpriteList.Count)
        {
            _frameIndex = 0;
        }

        activeFrame.sprite = _activeSpriteList[_frameIndex];
        _frameIndex++;
    }

    public void Animate(bool isGrounded, float horizontalMove)
    {
        if (isGrounded)
        {
            if (activeAnimationState != CharacterAnimationState.idle && horizontalMove == 0)
            {
                PlayIdleAnimation();
            }
            else if (activeAnimationState != CharacterAnimationState.run && horizontalMove != 0)
            {
                PlayRunAnimation();
            }
        }
        else
        {
            if (activeAnimationState != CharacterAnimationState.jump)
            {
                PlayJumpAnimation();
            }
        }
    }
}
