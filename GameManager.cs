using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;

public class GameManager : MonoBehaviour
{

        public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }






    public GameState gameState;
    private UIManager uiManager;







    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        gameState = GameState.MainMenu;
        uiManager.ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
       //switch for either if ESC is pressed or isPlayerAlive == false to either show pausemenu or gameovermenu
       switch(gameState)
       {
        // case GameState.GameOver:
        //     if(PlayerController.isDead)
        //     {
        //         gameState = GameState.GameOver;
        //         ShowGameOverMenu();
        //     }
        //     break;

        case GameState.Playing:
            if(Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                gameState = GameState.Paused;
                uiManager.ShowPauseMenu();
            }
            break;

        case GameState.Paused:
            if(Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                gameState = GameState.Paused;
                uiManager.ShowGameUI();
            }
            break;
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


