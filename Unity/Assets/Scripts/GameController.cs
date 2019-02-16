using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject controlMenu;
    public GameObject optionsMenu;
    public GameObject helpMenu;

    private Button[] controlButtons;
    private Button mergeButton;
    private Button resumeButton;
    private Button pauseButton;
    private Button startButton;
    private Button restartButton;
    private Button helpButton;

    private void Start()
    {
        controlButtons = controlMenu.GetComponentsInChildren<Button>();
        //Debug.Log("Control buttons " + controlButtons.Length);
        pauseButton = controlButtons[0];
        resumeButton = controlButtons[1];
        restartButton = controlButtons[2];
        helpButton = controlButtons[3];

        //Debug.Log("Buttons " + controlButtons);

        ResumeGame();
    }

    public void NavigateTo(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void PauseGame()
    {
        resumeButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        optionsMenu.SetActive(true);
        helpMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void Help()
    {
        resumeButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        optionsMenu.SetActive(false);
        helpMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        resumeButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        optionsMenu.SetActive(false);
        helpMenu.SetActive(false);
        Time.timeScale = 1;
    }

}
