using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(ButtonBorderDisplayer))]
public class HUDManager : MonoBehaviour
{
    //Exposed variables
    [SerializeField] private GameObject HUDGroup;
    [SerializeField] private Text money;
    [SerializeField] private Text day;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject[] livesHUDIcons;
    private ButtonBorderDisplayer borderDisplyer;

    private void Awake()
    {
        //Reference
        borderDisplyer = GetComponent<ButtonBorderDisplayer>();
    }

    #region Public
    public void UpdateMoneyCount(int amount)
    {
        //Update the text that needs to displayed
        money.text = amount.ToString();
    }

    public void UpdateDay(int value)
    {
        day.text = value.ToString();
    }

    public void UpdateLivesDisplay(int lives)
    {
        //Reveal heart icons corresponding the lives left
        for (int i = 0; i < livesHUDIcons.Length; i++)
        {
            livesHUDIcons[i].SetActive(lives > i ? true : false);
        }
    }

    public void DisplayDebugText(string text)
    {
        //Erase debug text's text box
        descriptionText.text = text;
    }


    public void HideDebugText ()
    {
        DisplayDebugText("");
    }
    #endregion
}

/*
     public void EnterPlacementMode(TowerTypes mode)
    {
        //Display a certain border
        borderDisplyer.EnterPlacementMode(mode);
        DisplayDebugText("Click on a platform to spawn tower!");
    }

    public void ExitPlacementMode ()
    {
        //Hide current border
        borderDisplyer.ExitPlacementMode();
    }

 */