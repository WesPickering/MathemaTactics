using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class MainMenuScript : MonoBehaviour
{
   #region Button Methods
   public void StartGame()
   {
      SceneManager.LoadScene("JennScene");
   }

   // Doesn't do anything but exists incase Wes wants to use
   public void Credits() {
        SceneManager.LoadScene("Credits");
   }
   #endregion
}
