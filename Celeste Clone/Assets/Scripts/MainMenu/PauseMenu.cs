using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{   

    [Header("Object Reference")]
        public GameObject PauseScreen;
        public PlatformerMovement playerMove;
        public Rigidbody2D playerPhysics;
        public Dash playerDash;

        [Space]

        public GameObject ConfirmMessage;
        public GameObject PauseMenuScreen;
        public GameObject QuittingScreen;
        public TMPro.TMP_Text[] btnOptions;

    [Header("Values")]
        public Color highlightColor;
        private bool GamePaused;
        private int selected;
        private float cooldown;
        private float originalFont;
        private bool Quitting;


    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        playerDash = Player.GetComponent<Dash>();
        playerPhysics = Player.GetComponent<Rigidbody2D>();
        playerMove = Player.GetComponent<PlatformerMovement>();
        selected = 0;
        originalFont = btnOptions[0].fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        PauseScreen.SetActive(GamePaused);
        playerDash.enabled = !GamePaused;
        playerPhysics.simulated = !GamePaused;
        playerMove.enabled = !GamePaused;

        if (Input.GetButtonDown("Pause") && !Quitting) {
            GamePaused = !GamePaused;
        }

        if (GamePaused) {
            float input = Input.GetAxis("Vertical");

            if (Time.time > cooldown){
                if (input > 0) {
                    btnOptions[selected].color = Color.white;
                    btnOptions[selected].fontSize = originalFont;
                    selected--;
                    if (selected < 0) {
                        selected = btnOptions.Length - 1;
                    }
                    cooldown = Time.time + 0.3f;
                } else if (input < 0) {
                    btnOptions[selected].color = Color.white;
                    btnOptions[selected].fontSize = originalFont;
                    selected++;
                    if (selected == btnOptions.Length) {
                        selected = 0;
                    }
                    cooldown = Time.time + 0.3f;
                }
                btnOptions[selected].fontSize = originalFont * 1.2f;
                btnOptions[selected].color = highlightColor;
            }

            if (Input.GetButtonDown("Jump") && !Quitting) {
                switch (selected) {
                    case 0:
                        ResumeGame();
                        break;
                    case 1:
                        RestartGame();
                        break;
                    case 2:
                        QuitGame();
                        break;
                }
                cooldown = Time.time + 0.3f;
            }

            if (Quitting && Time.time > cooldown) {
                if (Input.GetButtonDown("Submit")) {
                    DeclineQuit();
                } else if (Input.GetButtonDown("Cancel")){
                    ConfirmQuit();
                    Debug.Log ("Here Again");
                }
            }
        }
    }

    public void ResumeGame () {
        GamePaused = false;
    }

    public void RestartGame() {
        GamePaused = false;
        SceneManager.LoadScene("Stash's Level Testing");
    }

    public void QuitGame () {
        Quitting = true;
        ConfirmMessage.SetActive(true);
        PauseMenuScreen.SetActive(false);
    }

    public void ConfirmQuit () {
        ConfirmMessage.SetActive(false);
        QuittingScreen.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void DeclineQuit () {
        Quitting = false;
        ConfirmMessage.SetActive(false);
        PauseMenuScreen.SetActive(true);
    }
}
