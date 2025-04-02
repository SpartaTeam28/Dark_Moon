using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{


    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    public float SoundEffectVolume => soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;

    [SerializeField][Range(0f, 1f)] private float musicVolume;

    public float MusicVolume => musicVolume;


    private AudioSource musicAudioSource;
    public AudioSource MusicAudioSource => musicAudioSource;
    public AudioClip musicClip;


    private string MusicVolumKey = "MusicVolumeKey";
    private string SoundEffectVolumeKey = "SoundEffectVolume";

    public SoundSource soundSourcePrefabs;

    private static SoundManager _instance;
    public static SoundManager instance
    {
        get
        {
            if (null == _instance)
            {
                return null;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        musicVolume = PlayerPrefs.GetFloat(MusicVolumKey, 0);
        soundEffectVolume = PlayerPrefs.GetFloat(SoundEffectVolumeKey, 0);

        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;


        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (_instance != this)
                Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        ChangeBackGroundMusic(musicClip);
    }

    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("재생할 AudioClip이 null입니다! AudioManager에서 확인하세요.");
            return;
        }
        SoundSource obj = Instantiate(instance.soundSourcePrefabs);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }

    public void SetSoundEffectVolume(float _soundEffectVolule)
    {
        soundEffectVolume = _soundEffectVolule;
        PlayerPrefs.SetFloat(SoundEffectVolumeKey, _soundEffectVolule);
    }

    public void SetMusicVolumeSave(float value)
    {
        PlayerPrefs.SetFloat(MusicVolumKey, value);
    }




}
