using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathLayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Destroy(collision.gameObject);
            SceneMover.Instance.RestartScene();
        }

        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
