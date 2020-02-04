using UnityEngine;

public class CharacterAnimationTester : MonoBehaviour
{
    private CharacterAnimator _characterAnimator;

    private void Awake()
    {
        _characterAnimator = FindObjectOfType<CharacterAnimator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _characterAnimator.PlayIdleAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            _characterAnimator.PlayRunAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            _characterAnimator.PlayJumpAnimation();
        }
    }
}
