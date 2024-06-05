using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
  public Sound[] Sounds;

  public override void Awake()
  {
    foreach (Sound s in Sounds)
    {
      s.Source = gameObject.AddComponent<AudioSource>();
      s.Source.clip = s.Clip;
      s.Source.volume = s.Volume;
      s.Source.pitch = s.Pitch;
    }
  }

  void Start()
  {
    Play("Theme");
  }

  public void Play(string name)
  {
    Sound s = Array.Find(Sounds, sound => sound.Name == name);
    if (s == null)
    {
      Debug.LogWarning("Sound: " + name + " not found!");
      return;
    }
    Debug.Log(s.Source);

    // debug why i cant hear the sound
    Debug.Log(s.Source.clip);
    Debug.Log(s.Source.volume);
    Debug.Log(s.Source.pitch);
    Debug.Log(s.Source.isPlaying);

    s.Source.Play();

    Debug.Log(s.Source.isPlaying);
  }
}
