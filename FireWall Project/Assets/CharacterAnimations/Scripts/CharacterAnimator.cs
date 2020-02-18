using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public float idleFrameRate;
    public float runFrameRate;
    public float jumpFrameRate;
    public float punchFrameRate;
    public float teleportFrameRate;
    private float _activeFrameRate;

    public List<Sprite> idleSprites;
    public List<Sprite> runSprites;
    public List<Sprite> jumpSprites;
    public List<Sprite> punchSprites;
    public List<Sprite> teleportSprites;
    private List<Sprite> _activeSpriteList;

    public SpriteRenderer activeFrame;
    private int _frameIndex;

    public enum CharacterAnimationState {idle, run, jump, punch, teleport};
    [HideInInspector] public CharacterAnimationState activeAnimationState;

    [HideInInspector] bool canLoop;

    private void Awake()
    {
        canLoop = true;
    }

    private void Start()
    {
        PlayIdleAnimation();
    }

    public void PlayIdleAnimation()
    {
        SetUpAnimation(idleFrameRate, idleSprites, CharacterAnimationState.idle);
    }

    public void PlayRunAnimation()
    {
        SetUpAnimation(runFrameRate, runSprites, CharacterAnimationState.run);
    }

    public void PlayJumpAnimation()
    {
        SetUpAnimation(jumpFrameRate, jumpSprites, CharacterAnimationState.jump);
    }

    public void PlayPunchAnimation()
    {
        StartCoroutine(startLoopBuffer(punchFrameRate, punchSprites.Count));
        SetUpAnimation(punchFrameRate, punchSprites, CharacterAnimationState.punch);
    }

    public void PlayTeleportAnimation()
    {
        StartCoroutine(startLoopBuffer(teleportFrameRate, teleportSprites.Count));
        SetUpAnimation(teleportFrameRate, teleportSprites, CharacterAnimationState.teleport);
    }

    void SetUpAnimation(float frameRate, List<Sprite> sprites, CharacterAnimationState animationState)
    {
        StopCoroutine(nameof(playAnimationCo));
        _activeFrameRate = frameRate;
        _activeSpriteList = sprites;
        _frameIndex = 0;
        StartCoroutine(nameof(playAnimationCo));
        activeAnimationState = animationState;
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
        if (canLoop)
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

    private IEnumerator startLoopBuffer(float frameRate, int numberOfSprites)
    {
        canLoop = false;
        yield return new WaitForSeconds(frameRate * numberOfSprites);
        canLoop = true;
    }
}
