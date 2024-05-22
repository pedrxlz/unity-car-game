using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Endless Section Builing PF Variant");
    }

    public void QuitGame()
    {
        Debug.Log("Desisto!");
        Application.Quit();
    }
}
