using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitAndFallback : MonoBehaviour
{
    GameObject player;
    Action reloadScene;
    BlackFade fade;
    bool isReloading = false;

    void Start()
    {
        fade = FindObjectOfType<BlackFade>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Collider>().gameObject;
        reloadScene = () => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); };
    }

    void Update()
    {
        if (player.transform.position.y < -4f && !isReloading)
        {
            StartCoroutine(fade.Fade(false, 1f, reloadScene));
            isReloading = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
