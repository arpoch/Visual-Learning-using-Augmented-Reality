using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public int sceneNumber;
    public void enterMenu()
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void exit()
    {
        Application.Quit();
    }
}
