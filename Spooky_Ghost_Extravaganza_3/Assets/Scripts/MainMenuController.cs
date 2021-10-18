using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject playGameButton;
   
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayerMovementScene");
    }
}
