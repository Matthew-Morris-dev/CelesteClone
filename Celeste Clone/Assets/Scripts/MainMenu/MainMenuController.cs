using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{

    [Header("Object Reference")]
        public TMP_Text[] btnOptions;
        
        public GameObject ConfirmScreen;
        public GameObject MainMenu;

    [Header("Control")]
        public Color highlightColor;
        [SerializeField] private int selected;
        private float cooldown;
        private float originalFont;

        private bool Quitting;
    // Start is called before the first frame update
    void Start()
    {
        selected = 0;
        originalFont = btnOptions[0].fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Vertical");

        if (Time.time > cooldown){
            if (input < 0) {
                btnOptions[selected].color = Color.white;
                btnOptions[selected].fontSize = originalFont;
                selected--;
                if (selected < 0) {
                    selected = btnOptions.Length - 1;
                }
                cooldown = Time.time + 0.3f;
            } else if (input > 0) {
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

        if (Input.GetButtonDown("Jump")) {
            switch (selected) {
                case 0:
                    SceneManager.LoadSceneAsync(2);
                    break;
                case 1: 
                    QuitButton();
                    break;
            }
        }

        if (Quitting) {
            if (Input.GetButtonDown("Submit")) {
                QuitChoice(true);
            } else if (Input.GetButtonDown("Cancel")) {
                QuitChoice(false);
            }
        }
    }

    public void QuitButton () {
        ConfirmScreen.SetActive(true);
        MainMenu.SetActive(false);
        Quitting = true;
    }

    public void QuitChoice (bool choice) {
        if (choice) {
            Application.Quit();
        } else {
            ConfirmScreen.SetActive(false);
            MainMenu.SetActive(true);
            Quitting = false;
        }
    }
}
