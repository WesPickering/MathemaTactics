using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class TutorialScript : MonoBehaviour
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("All of the pages in order.")]
   private RectTransform[] m_Pages;

   [SerializeField]
   [Tooltip("In this array, place every game object that should start " +
      "disabled as the player flips through the tutorial.")]
   private GameObject[] m_ObjectsToDisableAtStart;
   #endregion

   #region Private Variables
   // The index for the current page
   private int m_CurrentPage;
   #endregion

   #region Initialization
   private void Awake()
   {
      m_CurrentPage = 0;
   }

   private void Start()
   {
      foreach (RectTransform page in m_Pages)
         page.gameObject.SetActive(false);
      m_Pages[0].gameObject.SetActive(true);
      foreach (GameObject go in m_ObjectsToDisableAtStart)
         go.SetActive(false);
   }
   #endregion

   #region Button Methods
   public void Next()
   {
      if (m_CurrentPage == m_Pages.Length - 1)
         return;
      m_Pages[m_CurrentPage++].gameObject.SetActive(false);
      m_Pages[m_CurrentPage].gameObject.SetActive(true);
   }

   public void Back()
   {
      if (m_CurrentPage == 0)
         return;
      m_Pages[m_CurrentPage--].gameObject.SetActive(false);
      m_Pages[m_CurrentPage].gameObject.SetActive(true);
   }

   public void Skip()
   {
      m_Pages[m_CurrentPage].gameObject.SetActive(false);
      foreach (GameObject go in m_ObjectsToDisableAtStart)
         go.SetActive(true);
   }
   #endregion
}
