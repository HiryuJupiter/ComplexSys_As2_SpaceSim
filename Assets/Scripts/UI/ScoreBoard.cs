﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] Highscore highscore;
    [SerializeField] GameObject anyKeyToQuit;

    CanvasGroup cvs;

    bool waitingToQuit = false;

    #region MonoBehavior
    private void Awake()
    {
        //Hide canvas
        cvs = GetComponent<CanvasGroup>();
        CanvasGroupHelper.InstantHide(cvs);

        //Hide peripherals
        anyKeyToQuit.SetActive(false);
    }

    void Start()
    {
        //Event subscribing
        EventScribing();
    }

    private void Update()
    {
        if (waitingToQuit && Input.anyKey)
        {
            //ToMainMenu();
            waitingToQuit = false;
        }
    }
    #endregion

    #region Canvas visibility
    void RevealCanvas()
    {
        StartCoroutine(CanvasGroupHelper.CanvasFadeIn(cvs, 0.1f));
        //highscore.DisplayHighscore(sceneManager.Coins);
        StartCoroutine(AllowForAnykeyToQuit());
    }

    void HideCanvas()
    {
        StartCoroutine(CanvasGroupHelper.CanvasFadeOut(cvs, 0.1f));
        anyKeyToQuit.SetActive(false);
    }
    #endregion

    #region WaitForKey
    IEnumerator AllowForAnykeyToQuit ()
    {
        yield return new WaitForSeconds(2f);
        anyKeyToQuit.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        waitingToQuit = true;
        
    }
    #endregion

    #region Event subscribing
    void EventScribing()
    {
        //SceneEvents.PlayerDead.Event += RevealCanvas;
        //SceneEvents.GameOverBackToMain.Event += HideCanvas;
    }

    void OnDisable()
    {
        //SceneEvents.PlayerDead.Event -= RevealCanvas;
        //SceneEvents.GameOverBackToMain.Event -= HideCanvas;
    }
    #endregion
}