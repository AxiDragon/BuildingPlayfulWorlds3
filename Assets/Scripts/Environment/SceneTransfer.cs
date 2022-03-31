using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    BlackFade black;
    delegate void NextScene();
    //with using System;
    Action nextSceneAction; // delegate void nextSceneAction(); (no parameters)
    Action<int, float> veryCoolAction; //

    Func<bool> veryCoolFunction;
    Func<bool, int, float> veryCoolBoolIntFloatFunction; //returns float - takes bool and int as parameters

    NextScene nextSceneFunction;

    void Start()
    {
        //nextSceneFunction = new NextScene(GoToNextScene);
        //or
        //nextSceneFunction = GoToNextScene;
        //or 
        //nextSceneFunction = delegate () { SceneManager.LoadScene(1); };
        //or
        //nextSceneFunction = () => { SceneManager.LoadScene(1); };
        //you can als += () => { print("well hey that's cool"); };
        //veryCoolAction = (int i, float f) => { print(i); print(f); }; 
        //veryCoolFunction = () => false;

        nextSceneAction = () => { SceneManager.LoadScene(1); };

        black = FindObjectOfType<BlackFade>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Door.doorsLocked = true;
            StartCoroutine(black.Fade(false, 2f, nextSceneAction));
        }
    }
}
