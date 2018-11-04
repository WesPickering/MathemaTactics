using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSoundFX : MonoBehaviour
{

    Button button;
    [SerializeField]
    string fx;

    AudioButtonController controller;

    // Use this for initialization
    void Start()
    {
        button = GetComponent<Button>();
        controller = GameObject.Find("SFXController").GetComponent<AudioButtonController>();
        button.onClick.AddListener(delegate { addFX(fx); });
    }

    void addFX(string name)
    {
        controller.Play(name);
        //Debug.Log("Played effect");
    }
}