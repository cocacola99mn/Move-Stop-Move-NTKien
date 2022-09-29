using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioName 
{ 
    ButtonClick,
    Die,
    Hit,
    Projectile,
    Fail,
    Victory,
    SizeUp
}

public class AudioManager : Singleton<AudioManager>
{
    public AudioMixer audioMixer;
    public AudioSource audioSource;

    [System.Serializable]
    public class AudioData
    {
        public AudioName audioName;
        public AudioClip audioClip;
    }

    public List<AudioData> audioDatas = new List<AudioData>();
    public Dictionary<AudioName, AudioClip> audioDict = new Dictionary<AudioName, AudioClip>();

    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        foreach (var item in audioDatas)
        {
            audioDict.Add(item.audioName, item.audioClip);
        }
    }

    public void PlayAudio(AudioName name)
    {
        audioSource.PlayOneShot(audioDict[name]);
    }

    public void SetAudio(bool state)
    {
        int soundPref = (state) ? 1 : 0;
        PlayerPrefs.SetInt(GameConstant.PREF_SOUND, soundPref);

        float mixerValue = (state) ? 0 : -80;
        audioMixer.SetFloat(GameConstant.MIXER_MASTER, mixerValue);
    }
}
