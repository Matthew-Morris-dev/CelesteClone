using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{   

    [Header("Object Reference")]
        public GameObject PauseScreen;
        public PlatformerMovement playerMove;
        public Rigidbody2D playerPhysics;
        public Dash playerDash;

        [Space]

        public GameObject ConfirmMessage;

    [Header("Values")]
        private bool GamePaused;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        playerDash = Player.GetComponent<Dash>();
        playerPhysics = Player.GetComponent<Rigidbody2D>();
        playerMove = Player.GetComponent<PlatformerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        PauseScreen.SetActive(GamePaused);
        playerDash.enabled = !GamePaused;
        playerPhysics.simulated = !GamePaused;
        playerMove.enabled = !GamePaused;

        if (Input.GetButtonDown("Pause")) {
            GamePaused = !GamePaused;
        }
    }

    public void ResumeGame () {
        GamePaused = false;
    }

    public void RestartGame() {
        // Respawn Player
        GamePaused = false;
    }

    public void QuitGame () {
        ConfirmMessage.SetActive(true);
    }

    public void ConfirmQuit () {

    }

    public void DeclineQuit () {

    }
}
