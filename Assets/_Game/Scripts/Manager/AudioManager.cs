using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioName 
{ 
    ButtonClick = 0,
    Die = 1,
    Hit = 2,
    Projectile = 3,
    Fail = 4,
    Victory = 5,
    SizeUp = 6
}

public class AudioManager : Singleton<AudioManager>
{
    DataManager dataIns;

    public AudioMixer audioMixer;
    public AudioSource audioSource;
    bool startAudio;

    public List<AudioClip> audioList = new List<AudioClip>();

    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        dataIns = DataManager.Ins;
        startAudio = (dataIns.playerDataSO.Sound) ? true : false;
        SetAudio(startAudio);
    }

    public void PlayAudio(AudioName name)
    {
        audioSource.PlayOneShot(audioList[(int)name]);
    }

    public void SetAudio(bool state)
    {
        dataIns.SetBoolData(GameConstant.PREF_SOUND, ref dataIns.playerDataSO.Sound, state);

        float mixerValue = (state) ? 0 : -80;
        audioMixer.SetFloat(GameConstant.MIXER_MASTER, mixerValue);
    }
}
