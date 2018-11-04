using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * To play a sound, simply attach this script to any GameObject in the scene, and call SoundManager.PlaySound(fileName)
 *   This is a static method that can be called from anywhere without reference
 * This SoundManager will spawn and reuse AudioSources independantly
 */
public class SoundManager : MonoBehaviour {
    private static SoundManager instance;
    private Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();
    private Queue<AudioSource> readyAudioSources = new Queue<AudioSource>();
    private List<AudioSource> busyAudioSources = new List<AudioSource>();

    void Start() {
        Initialize();
    }
    void Update() {
        CheckBusyAudioSources();
    }

    private void Initialize() {
        instance = this;
    }
    private void CheckBusyAudioSources() {
        for (int i = busyAudioSources.Count - 1; i >= 0; i--) {
            if (!busyAudioSources[i].isPlaying) {
                AudioSource ready = busyAudioSources[i];
                busyAudioSources.RemoveAt(i);
                readyAudioSources.Enqueue(ready);
            }
        }
    }

    public static void PlaySound(string fileName) {
		if (instance == null) {
			Debug.Log("Trying to play sound before SoundManager can run Start()");
			return;
		}
        instance.Instance_PlaySound(fileName);
    }
    private void Instance_PlaySound(string fileName) {
        LoadSound(fileName);
        AudioSource audioSource = GetAudioSource();
        audioSource.PlayOneShot(audioDictionary[fileName]);
        busyAudioSources.Add(audioSource);
    }
    private AudioSource GetAudioSource() {
        if (readyAudioSources.Count <= 0) {
            return gameObject.AddComponent<AudioSource>();
        }
        return readyAudioSources.Dequeue();
    }
    private void LoadSound(string fileName) {
        if (audioDictionary.ContainsKey(fileName)) {
            return;
        }
        string path = "Sounds/" + fileName;
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        if (audioClip == null) {
            Debug.LogError("AudioClip not found, path = " + path);
        }
        audioDictionary[fileName] = audioClip;
    }
}
