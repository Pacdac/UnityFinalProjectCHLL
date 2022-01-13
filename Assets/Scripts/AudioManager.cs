using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null) // to avoid having more than AudioManager when switching scenes
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;

        }
        DontDestroyOnLoad(gameObject); // to make AudioManager common to all scenes, so music doesn't restart 

        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return;
        }
        //Debug.Log("Sound " + name + " playing");
        sound.source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return;
        }
        sound.source.Stop();
    }
}
