using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Intro
{
    public class SwipeControl : MonoBehaviour
    {
        public GameObject scrollbar; //biar bisa akses value dari scrol bar
        private float _scrollPos = 0;
        private float[] _pos; //butuh array untuk menyimpan posisinya dari tiap2 object
        private int _posisi = 0; //navigasi posisi kanan kiri\

        public GameObject buttonLanjutkan;
        public GameObject buttonKiri;
        [SerializeField] private AudioClip[] audioClip;
        public AudioSource audioSource;

        // Start is called before the first frame update
        private void Start()
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
            if (_posisi < _pos.Length - 1)
            {
                _posisi += 1;
                _scrollPos = _pos[_posisi];
                if (audioClip[_posisi] != null)
                {
                    audioSource.clip = audioClip[_posisi];
                    audioSource.Play();
                }
            }
            if (_posisi == _pos.Length - 1)
            {
                buttonLanjutkan.SetActive(true);
                buttonKiri.SetActive(true);
            }
        }

        // untuk tombol previous atau sebelumnya
        public void prev()
        {
            if (_posisi > 0)
            {
                _posisi -= 1;
                _scrollPos = _pos[_posisi];

            }
        }

        // Update is called once per frame
        void Update()
        {
            //deklarasikan posnya dulu
            _pos = new float[transform.childCount];
            //mengambil dari jarak antar object
            float distance = 1f / (_pos.Length - 1f);
            //memasukan jarak pos2nya ke suatu object
            for (int i = 0; i < _pos.Length; i++)
            {
                _pos[i] = distance * i;
            }

            //funsi scroll ketika menggerakan objectnya
            if (Input.GetMouseButton(0))
            {
                //nilai dari value dari posisi object di masukan ke variable scrol pos
                _scrollPos = scrollbar.GetComponent<Scrollbar>().value;
            }
            else
            {
                for (int i = 0; i < _pos.Length; i++)
                {
                    if (_scrollPos < _pos[i] + (distance / 2) && _scrollPos > _pos[i] - (distance / 2))
                    {
                        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, _pos[i], 0.15f);
                        _posisi = i; //agar kalau swipe posisinya selalu update
                    }
                }
            }
        }
    }
}
