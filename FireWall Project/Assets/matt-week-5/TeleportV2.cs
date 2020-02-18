using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportV2 : MonoBehaviour
{
    private V3PlayerCharacterControler _player;
    private float _teleportDelay;
    public float distanceToTeleport;

    void Awake()
    {
        _player = FindObjectOfType<V3PlayerCharacterControler>();
        _teleportDelay = 0.5f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(delayTeleport());
        }
    }

    private IEnumerator delayTeleport()
    {
        _player.characterAnimator.PlayTeleportAnimation();
        yield return new WaitForSeconds(_teleportDelay);
        Vector3 teleportPosition = new Vector3(_player.transform.position.x + distanceToTeleport, _player.transform.position.y, 0);
        _player.transform.position = teleportPosition;
    }
}
