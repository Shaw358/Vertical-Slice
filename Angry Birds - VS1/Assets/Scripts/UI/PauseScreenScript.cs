using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenScript : MonoBehaviour {
    public GameObject pauseScreen;
    public GameObject pauseButton;
    public Transform canvas;
    public AudioListener audioListener;
    public List<AudioSource> audioSources;
    private bool opened = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            OpenPauseScreen();
            opened = true;
        } else if (opened == true) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                ClosePauseScreen();
            }
        }
    }

    public void OpenPauseScreen() {
        pauseScreen.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ClosePauseScreen() {
        pauseScreen.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void ResetGame() {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1.0f;
    }

    public void MuteSounds() {
        foreach (AudioSource audioSorce in audioSources) {
            audioSorce.Stop();
        }
    }
}

