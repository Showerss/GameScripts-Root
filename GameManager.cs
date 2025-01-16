using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
        public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    /// <summary>
    /// Methods to show or hide UI elements based on the game state
    /// </summary>

        private void ShowMainMenu()
    {
        //show main menu UI
        //play main menu music
        //hide pause menu
        //hide game over menu
    }

    private void ShowPauseMenu()
    {
        //show pause menu UI
        //play pause menu music
        //hide main menu
        //hide game over menu
    }

    private void ShowGameOverMenu()
    {
        //show game over menu UI
        //play game over music
        //hide main menu
        //hide pause menu
    }

    private void ShowGameUI()
    {
        //show game UI
        //play game music
        //hide main menu
        //hide pause menu
        //hide game over menu
    }


/// <summary>
/// 
/// 
/// Beginning of Game Logic Portion
/// 
/// 
/// 
/// </summary>

    public GameState gameState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameState.MainMenu;
        ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        //if keypress Esc, show pause menu
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameState == GameState.Playing)
            {
                gameState = GameState.Paused;
                ShowPauseMenu();
            }
            else if(gameState == GameState.Paused)
            {
                gameState = GameState.Playing;
                ShowGameUI();
            }
        }
    }
}


//TODO: Add a tracker of the game state, such as paused, playing, game over, etc.
// Handle loading or unloading of scenes
// Handle score or presistient data between scenes 
// show pause menu when game is pause
// show game over menu when game is over
// telling audiomanager which music to play
// telling uimanager which UI to show or hide


