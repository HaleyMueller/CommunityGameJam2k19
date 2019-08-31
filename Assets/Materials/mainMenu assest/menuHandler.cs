using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuHandler : MonoBehaviour
{
    public string theLevel;

    public void StartGame(){
        SceneManager.LoadScene(theLevel);
    }

    public void exitTheGame(){
        Application.Quit();
    }

    public void openOptions(){
        print("options works...");
    }


}
