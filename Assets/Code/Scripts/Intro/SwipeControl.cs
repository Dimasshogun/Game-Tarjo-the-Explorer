using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeControl : MonoBehaviour
{
    public GameObject scrollbar; //biar bisa akses value dari scrol bar
    float scroll_pos = 0;
    float[] pos; //butuh array untuk menyimpan posisinya dari tiap2 object
    int posisi = 0; //navigasi posisi kanan kiri\

    public GameObject buttonLanjutkan;
    public GameObject buttonKiri;
    [SerializeField] private AudioClip[] audioClip;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        buttonLanjutkan.SetActive(false);
        buttonKiri.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        if (audioClip[0] != null)
        {
            audioSource.clip = audioClip[0];
            audioSource.Play();
        }
    }

    //fungsi buat tombol nextnya
    public void next()
    {
        if (posisi < pos.Length - 1)
        {
            posisi += 1;
            scroll_pos = pos[posisi];
            if (audioClip[posisi] != null)
            {
                audioSource.clip = audioClip[posisi];
                audioSource.Play();
            }
        }
        if (posisi == pos.Length - 1)
        {
            buttonLanjutkan.SetActive(true);
            buttonKiri.SetActive(true);
        }
    }

    // untuk tombol previous atau sebelumnya
    public void prev()
    {
        if (posisi > 0)
        {
            posisi -= 1;
            scroll_pos = pos[posisi];

        }
    }

    // Update is called once per frame
    void Update()
    {
        //deklarasikan posnya dulu
        pos = new float[transform.childCount];
        //mengambil dari jarak antar object
        float distance = 1f / (pos.Length - 1f);
        //memasukan jarak pos2nya ke suatu object
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        //funsi scroll ketika menggerakan objectnya
        if (Input.GetMouseButton(0))
        {
            //nilai dari value dari posisi object di masukan ke variable scrol pos
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.15f);
                    posisi = i; //agar kalau swipe posisinya selalu update
                }
            }
        }
    }
}
