using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Game;

public class AudioManagerScript : MonoBehaviour {
    public List<AudioClip> musicClips;
    public List<AudioClip> soundClips;
    List<AudioSource> audioSources;
    public static AudioManagerScript instance;
    //public AudioSource musicAudioSource;
    //public AudioSource soundAudioSource;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    void Start() {
        audioSources= new List<AudioSource>();
        AudioSource audioSource =gameObject.AddComponent<AudioSource>();
        audioSource.loop=true;
        audioSource.volume = 0.05f;
        audioSources.Add(audioSource);
        
        audioSources.Add(gameObject.AddComponent<AudioSource>());
        audioSources.Add(gameObject.AddComponent<AudioSource>());
        audioSources.Add(gameObject.AddComponent<AudioSource>());

    }

    public void PlayMusic(Music id) {
        audioSources[0].Stop();
        audioSources[0].clip = musicClips[(int)id];
        audioSources[0].Play();
    }
    public void PlaySound(Sound id) {

        //audioSources[1].PlayOneShot(soundClips[(int)id]);
        switch (id) {
            case Sound.PlayerShot:
                audioSources[1].Stop();
                audioSources[1].clip = soundClips[(int)id];
                audioSources[1].Play();
                break;
            case Sound.Explosion:
                audioSources[2].Stop();
                audioSources[2].clip = soundClips[(int)id];
                audioSources[2].Play();
                break;
            case Sound.Coin:
                audioSources[3].Stop();
                audioSources[3].clip = soundClips[(int)id];
                audioSources[3].Play();
                break;
        }

    }

}
