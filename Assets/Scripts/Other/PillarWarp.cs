using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PillarWarp : MonoBehaviour
{
    Action pillarAction;
    Action sceneTransfer;

    BlackFade fade;

    void Start()
    {
        pillarAction = () => { };
        sceneTransfer = () => { SceneManager.LoadScene(0); };
        fade = FindObjectOfType<BlackFade>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            pillarAction();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pillarAction = () => { StartCoroutine(fade.Fade(false, 4f, sceneTransfer)); };
            UIPrompts.UIFade(true, "E - Return");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pillarAction = () => { };
            UIPrompts.UIFade(false, null);
        }
    }
}
