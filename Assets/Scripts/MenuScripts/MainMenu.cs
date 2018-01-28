using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    public void StartSinglePlayer() {

        //SceneManager.LoadScene("City3");

    }

    public void StartMultiplayer () {

        SceneManager.LoadScene("City3");

    }

    public void Quit () {
        Application.Quit();
    }
}
