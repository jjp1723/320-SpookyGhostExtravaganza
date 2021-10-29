using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject playGameButton;
    private AudioManager gameAudio;
    public void Start()
    {
        gameAudio = FindObjectOfType<AudioManager>();
    }

    public void PlayGame()
    {
        if (gameAudio != null)
        {
            gameAudio.Stop("TitleIntro");
            gameAudio.Play("Gameloop");
        }
        SceneManager.LoadScene("PlayerMovementScene");
    }
}
