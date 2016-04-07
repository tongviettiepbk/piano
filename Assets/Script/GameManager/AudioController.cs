using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{

    public static AudioController instance;

	public AudioClip buttonClick;
	public bool isBgMusicOn; 
    public bool isEfxSoundOn;

    public AudioSource adsEfxSound;
    public AudioSource adsBgMusic;

    public AudioClip[] bgMusic;
    public AudioClip[] efxSound;

    

    public Dictionary<string, AudioClip> efxClips = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> bgMusicClips = new Dictionary<string, AudioClip>();
    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
//		if(!instance)
        instance = this;
        DontDestroyOnLoad(gameObject);

        CheckMusicSetting();

        foreach (AudioClip ad in bgMusic)
            bgMusicClips.Add(ad.name, ad); 

        foreach (AudioClip ad in efxSound)
            efxClips.Add(ad.name, ad);


    }

    // Use this for initialization
    void Start()
    {
		
    }

    public void PlayEfxClip(string clip)
    {
        if (isEfxSoundOn)
        {
            adsEfxSound.PlayOneShot(efxClips[clip]);
        }
    }

    public void Click()
    {
        if (isEfxSoundOn)
            adsEfxSound.PlayOneShot(buttonClick);
    }

    public void PlayMusic(string musicName)
    {
        //Debug.Log(musicName+ "  " + isBgMusicOn);
        adsBgMusic.clip = bgMusicClips[musicName];

        if (isBgMusicOn)
        {
            adsBgMusic.Play();
        }
    }

    public void PauseMusic()
    {
        adsBgMusic.Pause();
        //adsBgMusic.Play();
    }

    public void ResumeMusic()
    {
        if (isBgMusicOn)
        {
            adsBgMusic.Play();
        }
    }

    public void SetVolumeEfxSound(float volume)
    {
        adsEfxSound.volume = volume;
    }

    public void SetVolumeMusic(float volume)
    {
        adsBgMusic.volume = volume;
    }

    public void CheckMusicSetting()
    {
		// O la mo, 1 la tat
        isEfxSoundOn = PlayerPrefs.GetInt("isEfxSoundOn") == 0;
        isBgMusicOn = PlayerPrefs.GetInt("isBgMusicOn") == 0;

    }

    public void SetMusicOn(bool isOn)
    {
        if (isBgMusicOn == isOn) return;
        isBgMusicOn = isOn;
        if (!isBgMusicOn)
        {
            adsBgMusic.Stop();
            PlayerPrefs.SetInt("isBgMusicOn", 1);
        }
        else
        {
            adsBgMusic.Play();
            PlayerPrefs.SetInt("isBgMusicOn", 0);
        }
        PlayerPrefs.Save();
    }

    public void SetEfxSoundOn(bool isOn)
    {
        if (isEfxSoundOn == isOn) return;

        isEfxSoundOn = isOn;
        if (!isEfxSoundOn)
        {
            adsEfxSound.Stop();
            PlayerPrefs.SetInt("isEfxSoundOn", 1);
        }
        else
            PlayerPrefs.SetInt("isEfxSoundOn", 0);

        PlayerPrefs.Save();
    }

    public void SetAllMusic(bool isOn)
    {
        SetMusicOn(isOn);
        SetEfxSoundOn(isOn);
    }
}
