using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private V3PlayerCharacterControler _player;
    private Rigidbody2D _playerRigidBody;
    private float _initialPlayerGravity;
    private float _teleportDelay;
    private bool _canTeleport;
    public float distanceToTeleport;

    void Awake()
    {
        _player = FindObjectOfType<V3PlayerCharacterControler>();
        _playerRigidBody = _player.GetComponent<Rigidbody2D>();
        _initialPlayerGravity = _playerRigidBody.gravityScale;
        _teleportDelay = 0.5f;
        _canTeleport = true;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Keypad3)) && _canTeleport)
        {
            StartCoroutine(delayTeleport());
        }
    }

    private IEnumerator delayTeleport()
    {
        _canTeleport = false;
        _player.controller.setAirControl(false);
        _playerRigidBody.velocity = _playerRigidBody.velocity / 10;
        _playerRigidBody.gravityScale = -.1f;
        _player.characterAnimator.PlayTeleportAnimation();
        _player.soundManager.PlayTeleportStartSound();
        yield return new WaitForSeconds(_teleportDelay);

        float teleportDistance = distanceToTeleport;

        if (!_player.controller.getIsFacingRight())
        {
            teleportDistance = -distanceToTeleport;
        }

        Vector3 teleportPosition = new Vector3(_player.transform.position.x + teleportDistance, _player.transform.position.y, 0);
        _player.transform.position = teleportPosition;
        _playerRigidBody.gravityScale = _initialPlayerGravity;
        _player.controller.setAirControl(true);
        _canTeleport = true;

        _player.soundManager.PlayTeleportEndSound();
    }
}
