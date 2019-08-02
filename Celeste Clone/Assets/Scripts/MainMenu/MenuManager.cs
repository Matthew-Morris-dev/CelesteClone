using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private AsyncOperation MenuScene;
    private void Start() {
        MenuScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        MenuScene.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            MenuScene.allowSceneActivation = true;
        }   
    }
}
