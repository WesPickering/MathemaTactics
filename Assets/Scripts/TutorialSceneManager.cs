﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManager : MonoBehaviour {

    public void startGame() {
        SceneManager.LoadScene("WesScene");
    }
}
