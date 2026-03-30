using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject DaChangeMusic;
    [SerializeField] private AudioSource DaChangeMusicAS;

    [SerializeField] private AudioSource[] Musics;


    public void PlayMusic(int index)
    {
        for (int i = 0; i < Musics.Length; i++)
        {
            AudioSource music = Musics[i];
            if (index == i)
            {
                if (!music.isPlaying)
                    music.Play();
            }
            else
            {
                if (music.isPlaying)
                    music.Stop();
            }
        }
    }

    public void StopMusic()
    {
        PlayMusic(-1);
    }


    bool mute = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mute = !mute;

            for (int i = 0; i < Musics.Length; i++)
            {
                AudioSource music = Musics[i];
                music.mute = mute;
            }
        }
    }


    public void OnClick()
    {
        for (int i = 0; i < Musics.Length; i++)
        {
            AudioSource music = Musics[i];
            music.mute = true;
        }
        DaChangeMusic.SetActive(true);
    }
    public void OnRelease()
    {
        if (!mute)
        {
            for (int i = 0; i < Musics.Length; i++)
            {
                AudioSource music = Musics[i];
                music.mute = false;
            }
        }
        
        DaChangeMusic.SetActive(false);
    }
    public void ValChange()
    {
        float VolumeTo = PositionQuadCollScr.YouAreMusic.CurrentValue;

        DaChangeMusicAS.volume = VolumeTo;

        for (int i = 0; i < Musics.Length; i++)
        {
            AudioSource music = Musics[i];
            music.volume = VolumeTo;
        }
    }
}
