using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void scene(string scene){
        SceneManager.LoadScene(scene);
    }
    public void exit() {
        Application.Quit();
    }
}
