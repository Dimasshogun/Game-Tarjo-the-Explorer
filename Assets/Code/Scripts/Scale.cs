using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scale : MonoBehaviour
{
//agar tombolnya ada efek scalenya
    public void scale(float scale){
        transform.localScale = new Vector2(1/scale,1*scale);
    }

    public void scene(string scene){
        SceneManager.LoadScene(scene);
    }
    public void exit() {
        Application.Quit();
    }
    public void sound_volume(float volume){
        PlayerPrefs.SetFloat("volume",volume);
    }
}
