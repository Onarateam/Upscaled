using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
    [SerializeField]
    GameObject menuPause, menuGameOver, menuEnd;
    [SerializeField]
    PlayerController playerController;

    private bool m_pause;
    private float last_pause;

    // Use this for initialization
    void Start () {
        menuPause.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Pause") != 0 && last_pause == 0) {
            m_pause = true;
        } else {
            m_pause = false;
        }
        last_pause = Input.GetAxisRaw("Pause");

        if (m_pause && !menuGameOver.activeSelf && !menuEnd.activeSelf) {
            if(Time.timeScale == 0) {
                Time.timeScale = 1;
                menuPause.SetActive(false);
            } else {
                Time.timeScale = 0;
                menuPause.SetActive(true);
            }
        }
    }

    public void Exit() {
        Application.Quit();
    }

    public void launchDeathMenu() {
        menuGameOver.SetActive(true);
    }

    public void reloadGame() {
        Time.timeScale = 1;
        playerController.restartGame();
        SceneManager.LoadScene("Level");
    }

    public void reloadLevel() {
        Time.timeScale = 1;
        playerController.launchLastLevel();
        SceneManager.LoadScene("Level");
    }
}
