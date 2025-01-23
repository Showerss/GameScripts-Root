using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{

    public void ShowMainMenu()
    {
        return;
        //show main menu UI
        //play main menu music
        //hide pause menu
        //hide game over menu
    }

    public void ShowPauseMenu()
    {
        return;
        //show pause menu UI
        //play pause menu music
        //hide main menu
        //hide game over menu
    }

    public void ShowGameOverMenu()
    {
        return;
        //show game over menu UI
        //play game over music
        //hide main menu
        //hide pause menu
    }

    public void ShowGameUI()
    {
        return;
        //show game UI
        //play game music
        //hide main menu
        //hide pause menu
        //hide game over menu
    }

    public enum UIState
    {
        MainMenu,
        OptionsMenu,
        PauseMenu,
        GameOverMenu,
        InGameUI
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
