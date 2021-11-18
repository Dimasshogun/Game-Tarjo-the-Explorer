using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject kucing, anjing, bebek, monyet, kambing, kucinghitam, anjinghitam, bebekhitam, monyethitam, kambinghitam;

    Vector2 kucingInitialPos, anjingInitialPos, bebekInitialPos, monyetInitialPos, kambingInitialPos;


    public AudioSource source;
    public AudioClip[] correct;
    public AudioClip incorrect;

    bool kucingCorrect, anjingCorrect, bebekCorrect, monyetCorrect, kambingCorrect = false;
    // Start is called before the first frame update
    void Start()
    {
        kucingInitialPos = kucing.transform.position;
        anjingInitialPos = anjing.transform.position;
        bebekInitialPos = bebek.transform.position;
        monyetInitialPos = monyet.transform.position;
        kambingInitialPos = kambing.transform.position;
    }

    // Update is called once per frame
    public void Dragkucing()
    {
        kucing.transform.position = Input.mousePosition;
    }
    public void Draganjing()
    {
        anjing.transform.position = Input.mousePosition;
    }
    public void Dragbebek()
    {
        bebek.transform.position = Input.mousePosition;
    }
    public void Dragmonyet()
    {
        monyet.transform.position = Input.mousePosition;
    }
    public void Dragkambing()
    {
        kambing.transform.position = Input.mousePosition;
    }

    public void Dropkucing()
    {
        float Distance = Vector3.Distance(kucing.transform.position, kucinghitam.transform.position);
        if (Distance < 50)
        {
            kucing.transform.position = kucinghitam.transform.position;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            kucingCorrect = true;
        }
        else
        {
            kucing.transform.position = kucingInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    public void Dropanjing()
    {
        float Distance = Vector3.Distance(anjing.transform.position, anjinghitam.transform.position);
        if (Distance < 50)
        {
            anjing.transform.position = anjinghitam.transform.position;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            anjingCorrect = true;
        }
        else
        {
            anjing.transform.position = anjingInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    public void Dropbebek()
    {
        float Distance = Vector3.Distance(bebek.transform.position, bebekhitam.transform.position);
        if (Distance < 50)
        {
            bebek.transform.position = bebekhitam.transform.position;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            bebekCorrect = true;
        }
        else
        {
            bebek.transform.position = bebekInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    public void Dropmonyet()
    {
        float Distance = Vector3.Distance(monyet.transform.position, monyethitam.transform.position);
        if (Distance < 50)
        {
            monyet.transform.position = monyethitam.transform.position;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            monyetCorrect = true;
        }
        else
        {
            monyet.transform.position = monyetInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    public void Dropkambing()
    {
        float Distance = Vector3.Distance(kambing.transform.position, kambinghitam.transform.position);
        if (Distance < 50)
        {
            kambing.transform.position = kambinghitam.transform.position;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            kambingCorrect = true;
        }
        else
        {
            kambing.transform.position = kambingInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    void Update()
    {
        if(kucingCorrect && anjingCorrect && bebekCorrect && monyetCorrect && kambingCorrect)
        {
            Debug.Log("You win!");
        }
    }
    // kucing, anjing, bebek, monyet, kambing
}
