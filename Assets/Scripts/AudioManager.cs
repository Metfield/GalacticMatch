using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    Dictionary<string, AudioClip> sfxClips, bgmClips;
    AudioSource audioSource, musicSource;

    bool canPlayClip;

	// Use this for initialization
	void Start ()
    {
        // Get audio source component
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.priority = 240;

        // Create music audio source
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.8f;

        // Load audio clips
        bgmClips = new Dictionary<string, AudioClip>();
        sfxClips = new Dictionary<string, AudioClip>();

        LoadAudioClips(bgmClips, "Audio/BGM");
        LoadAudioClips(sfxClips, "Audio/SFX");

        canPlayClip = true;
    }

    public void PlayBGM(string name)
    {
        musicSource.clip = bgmClips[name];
        musicSource.Play();
    }

    public void PlaySFX(string clipName)
    {
        // Prevent multiple clips to play at the same time
        if (canPlayClip)
        {
            canPlayClip = false;

            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(sfxClips[clipName]);

            StartCoroutine(ResetCanPlay());
        }
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

    IEnumerator ResetCanPlay()
    {
        yield return new WaitForSeconds(0.2f);
        canPlayClip = true;
    }
}
