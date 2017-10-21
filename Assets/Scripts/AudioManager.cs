using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    Dictionary<string, AudioClip> sfxClips, bgmClips;
    AudioSource audioSource, musicSource;

	// Use this for initialization
	void Start ()
    {
        // Get audio source component
        audioSource = gameObject.AddComponent<AudioSource>();

        // Create music audio source
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.8f;

        // Load audio clips
        bgmClips = new Dictionary<string, AudioClip>();
        sfxClips = new Dictionary<string, AudioClip>();

        LoadAudioClips(bgmClips, "Audio/BGM");
        LoadAudioClips(sfxClips, "Audio/SFX");
    }

    public void PlayBGM(string name)
    {
        musicSource.clip = bgmClips[name];
        musicSource.Play();
    }

    public void PlaySFX(string clipName)
    {
        audioSource.PlayOneShot(sfxClips[clipName]);
    }

    public void SetBGMVolume(float volume)
    {
        musicSource.volume = volume;
    }

    void LoadAudioClips(Dictionary<string, AudioClip> clipsDictionary, string folderPath)
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>(folderPath);
        foreach (AudioClip clip in clips)
        {
            clipsDictionary.Add(clip.name, clip);
            clip.LoadAudioData();
        }
    }
}
