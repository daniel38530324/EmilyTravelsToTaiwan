using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance{get; private set;}
    public static float MusicValue{get; private set;}
    public static float SoundValue{get; private set;}
    public static float VoiceValue{get; private set;}

    [SerializeField] GameObject soundPrefab;
    [SerializeField] SoundData soundData;
    [SerializeField] GameObject sound_Image;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider[] soundSliders;

    private bool soundIsOpen;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        soundSliders[0].value = MusicValue;
        soundSliders[1].value = SoundValue;
        soundSliders[2].value = VoiceValue;
    }

    public void PlayMusic(string clipNmae)
    {
        Sounds s = Array.Find(soundData.Musics, sound => sound.name == clipNmae);
        if (s == null)
            return;
        AudioSource sp = Instantiate(soundPrefab, Vector3.zero, Quaternion.identity, transform.GetChild(0)).GetComponent<AudioSource>();

        sp.clip = s.clip;
        sp.outputAudioMixerGroup = s.output;
        sp.volume = s.volume;
        sp.pitch = s.pitch;
        sp.loop = s.loop;
        sp.playOnAwake = s.playOnAwake;
        sp.Play();
    }

    public void PlaySound(string clipNmae)
    {
        Sounds s = Array.Find(soundData.Sounds, sound => sound.name == clipNmae);
        if (s == null)
            return;
        AudioSource sp = Instantiate(soundPrefab, Vector3.zero, Quaternion.identity, transform.GetChild(0)).GetComponent<AudioSource>();

        sp.clip = s.clip;
        sp.outputAudioMixerGroup = s.output;
        sp.volume = s.volume;
        sp.pitch = s.pitch;
        sp.loop = s.loop;
        sp.playOnAwake = s.playOnAwake;
        sp.Play();
        Destroy(sp.gameObject, sp.clip.length); 
    }

    public void StopAll()
    {
        AudioSource[] soundPrefabs = transform.GetChild(0).transform.GetComponentsInChildren<AudioSource>();
        foreach(AudioSource item in soundPrefabs)
        {
            Destroy(item.gameObject);
        }
    }

    public void StopSound(string clipNmae)
    {
        Sounds s = Array.Find(soundData.Sounds, sound => sound.name == clipNmae);
        if (s == null)
            return;

        AudioSource[] soundPrefabs = transform.GetChild(0).transform.GetComponentsInChildren<AudioSource>();
        foreach (AudioSource item in soundPrefabs)
        {
            if (item.clip == s.clip)
                Destroy(item.gameObject);
        }
    }

    public void SetMusicVolume()
    {
        MusicValue = soundSliders[0].value;
        audioMixer.SetFloat("Music", MusicValue);
    }

    public void SetSoundVolume()
    {
        SoundValue = soundSliders[1].value;
        audioMixer.SetFloat("Sound", SoundValue);
    }

    public void SetVoiceVolume()
    {
        VoiceValue = soundSliders[2].value;
        audioMixer.SetFloat("Voice", VoiceValue);
    }

    public void Sound(){
        soundIsOpen = !soundIsOpen;
        sound_Image.SetActive(soundIsOpen);
    }
}
