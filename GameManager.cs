using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
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


//TODO: Add a tracker of the game state, such as paused, playing, game over, etc.
// Handle loading or unloading of scenes
// Handle score or presistient data between scenes 
// show pause menu when game is pause
// show game over menu when game is over
// telling audiomanager which music to play
// telling uimanager which UI to show or hide