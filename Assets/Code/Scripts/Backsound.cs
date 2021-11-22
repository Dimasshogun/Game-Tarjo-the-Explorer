using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backsound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("backsound on")==null){
            DontDestroyOnLoad(gameObject); //pindah scane nanti audionya tidak kehpus
            GetComponent<AudioSource>().Play(); //play musik di scane pertama
            gameObject.name = "backsound on";
        }
    }

    public void SoundVolume(float volume){
        // update volume sesuai parameternya float
        GetComponent<AudioSource>().volume = volume; 
    }
    
}
