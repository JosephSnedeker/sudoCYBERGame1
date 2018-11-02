using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

public enum SoundEffect
{
    Jump,
    wallJump,
}
public class SoundManager
{
    static SoundManager instance;
    private Dictionary<SoundEffect, AudioClip> SoundEffects
    { get; set; }
    private AudioSource SoundEffectSource
    { get; set; }
    private AudioSource BGMSource
    { get; set; }

    public static SoundManager Instance
    { get { return instance ?? (instance = new SoundManager()); } }
    private SoundManager()
    {
        SoundEffects = Resources.LoadAll<AudioClip>("")
            .ToDictionary(t => (SoundEffect)Enum.Parse(typeof(SoundEffect), t.name, true));
        SoundEffectSource = new GameObject("SoundEffectSource", typeof(AudioSource)).GetComponent<AudioSource>();
        Object.DontDestroyOnLoad(SoundEffectSource.gameObject);

        BGMSource = new GameObject("BGMSource", typeof(AudioSource)).GetComponent<AudioSource>();
        BGMSource.volume = .5f;
        BGMSource.loop = true;
        Object.DontDestroyOnLoad(BGMSource.gameObject);

        //ChangeBGM(Resources.Load<AudioClip>("Sound/Music/DancingMidgets"));
    }
    public void PlayOneShot(SoundEffect sound, float volumeScale = 1)
    {
        SoundEffectSource.PlayOneShot(SoundEffects[sound], volumeScale);

    }

    public void ChangeBGM(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    
    }


}

