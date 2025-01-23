using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    enum AudioType
    {
        Music,
        SFX,
        Voice
    }

    enum AudioState
    {
        Playing,
        Paused,
        Stopped
    }


    //-------------------------------
    //Instance Variables
    //-------------------------------

    [SerializeField] private AudioType audioType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //initialize audio
        //load audio files
        //start background music
    }

    // Update is called once per frame
    void Update()
    {
        //Monitor game state to pause/ OnApplicationResume or stop audio
    }


    void OutofStaminaAudio()
    {
        //play out of stamina audio
    }
}
