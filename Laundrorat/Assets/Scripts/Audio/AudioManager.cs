using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    
    public static AudioManager instance;

    public Sound[] sounds;

    void Awake() {

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds) {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.shouldLoop;
        }
    }

    public void PlaySound(string soundName) {
        Sound sound = Array.Find(sounds, sound => sound.name == soundName);
        if (sound == null) {
            Debug.Log("Sound: " + soundName + " not found!");
            return;
        }
        sound.audioSource.Play();
    }

}
