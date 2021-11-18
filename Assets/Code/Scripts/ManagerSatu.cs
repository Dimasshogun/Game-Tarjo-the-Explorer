using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSatu : MonoBehaviour
{
    public GameObject seribu, seratusribu, duapuluhribu, limaribu,seribuhitam, seratusribuhitam, duapuluhribuhitam, limaribuhitam;

    Vector2 seribuInitialPos, seratusribuInitialPos, duapuluhribuInitialPos, limaribuInitialPos;

    public AudioSource source;
    public AudioClip[] correct;
    public AudioClip incorrect;

    bool limaribuCorrect = false;
    // Start is called before the first frame update
    void Start()
    {
        seribuInitialPos = seribu.transform.position;
        seratusribuInitialPos = seratusribu.transform.position;
        duapuluhribuInitialPos = duapuluhribu.transform.position;
        limaribuInitialPos = limaribu.transform.position;
    }

    // Update is called once per frame
    public void Dragseribu()
    {
        seribu.transform.position = Input.mousePosition;
    }
    public void Dragseratusribu()
    {
        seratusribu.transform.position = Input.mousePosition;
    }
    public void Dragduapuluhribu()
    {
        duapuluhribu.transform.position = Input.mousePosition;
    }
    public void Draglimaribu()
    {
        limaribu.transform.position = Input.mousePosition;
    }

    // seribu, seratusribu, duapuluhribu, limaribu
    public void Droplimaribu()
    {
        float Distance = Vector3.Distance(limaribu.transform.position, limaribuhitam.transform.position);
        if (Distance < 50)
        {
            limaribu.transform.position = limaribuhitam.transform.position;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            limaribuCorrect = true;
        }
        else
        {
            limaribu.transform.position = limaribuInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    public void Dropseratusribu()
    {
        float Distance = Vector3.Distance(seratusribu.transform.position, seratusribuhitam.transform.position);
        if (Distance < 50)
        {
            seratusribu.transform.position = seratusribuhitam.transform.position;
            source.clip = correct[Random.Range(0, correct.Length)];
            // source.Play();
            // seratusribuCorrect = true;
        }
        else
        {
            seratusribu.transform.position = seratusribuInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    public void Dropduapuluhribu()
    {
        float Distance = Vector3.Distance(duapuluhribu.transform.position, duapuluhribuhitam.transform.position);
        if (Distance < 50)
        {
            duapuluhribu.transform.position = duapuluhribuhitam.transform.position;
            source.clip = correct[Random.Range(0, correct.Length)];
            // source.Play();
            // duapuluhribuCorrect = true;
        }
        else
        {
            duapuluhribu.transform.position = duapuluhribuInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    public void Dropseribu()
    {
        float Distance = Vector3.Distance(seribu.transform.position, seribuhitam.transform.position);
        if (Distance < 50)
        {
            seribu.transform.position = seribuhitam.transform.position;
            source.clip = correct[Random.Range(0, correct.Length)];
            // source.Play();
            // seribuCorrect = true;
        }
        else
        {
            seribu.transform.position = seribuInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    
    void Update()
    {
        if(limaribuCorrect)
        {
            Debug.Log("You win!");
        }
    }
    // kucing, seratusribu, duapuluhribu, limaribu, kambing
}
