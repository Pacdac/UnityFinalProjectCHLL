using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private Sound[] sounds;
    [SerializeField] private AudioMixerGroup audioMixer;
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
            sound.source.outputAudioMixerGroup = audioMixer;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name, float delay = 0)
    {
        Sound sound = SearchSound(name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return;
        }
        sound.source.PlayDelayed(delay);
    }

    public void Stop(string name)
    {
        Sound sound = SearchSound(name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return;
        }
        sound.source.Stop();
    }

    public void Pause(string name)
    {
        Sound sound = SearchSound(name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return;
        }
        sound.source.Pause();
    }

    public Sound SearchSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }
}
