using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameSceneManager : MonoBehaviour {

    public void continueGame() {
        SceneManager.LoadScene("WesScene");

    }

    public void backToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void toCredits() {
        SceneManager.LoadScene("Credits");
    }
}
