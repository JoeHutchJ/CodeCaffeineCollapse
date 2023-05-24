using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public List<CustomAudioClip> clips;

    public List<AudioSource> sources;

    public bool playAutomatically;

    public float everyXseconds;

    public float randomRange;

    float timeElapsed;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] array = GetComponents<AudioSource>();
        foreach (AudioSource src in array) {
            sources.Add(src);
            src.playOnAwake = false;
        }

        if (playAutomatically && everyXseconds == 0.0f) {
            Play();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (playAutomatically && !ClipLoop()) {
            
            if (timeElapsed + Time.deltaTime >= (everyXseconds + (UnityEngine.Random.Range(-1.0f, 1.0f)) * randomRange)) {
                timeElapsed = 0;
                if (clips.Count > 1) {
                    PlayRandom();
                } else {
                Play();
            }
            }
            timeElapsed += Time.deltaTime;
        
    }

    }

    public void Play() {
        CustomAudioClip clip = clips[0];
        if (clip != null) {
                AudioSource src = setAudioSource(clip);
                src.Play();
        }


    }

    public void PlayRandom() {
        CustomAudioClip clip = clips[UnityEngine.Random.Range(0,clips.Count)];
        if (clip != null) {
                //Debug.Log(clip.name);
                AudioSource src = setAudioSource(clip);
                src.Play();
        }


    }

    public void Play(int index) {
        CustomAudioClip clip = clips[index];
        if (clip != null) {

                AudioSource src = setAudioSource(clip);
                src.Play();
            }
        }



    public void Play(string name) {
        foreach (CustomAudioClip clip in clips) {
            if (clip.name == name) {
                AudioSource src = setAudioSource(clip);
                src.Play();
            }
        }


    }

    public AudioSource GetFreeSource() {
        foreach(AudioSource src in sources) {
            if (!src.isPlaying) {
                return src;
            }
        }
        return null;

    }

    public AudioSource setAudioSource(CustomAudioClip clip) {
        AudioSource src = GetFreeSource();
        if (src != null) {

        } else {
            src = newSource();
        }
        src.clip = clip.clip;
        src.volume = clip.relativeVolume * Global.volume;
        src.loop = clip.loop;
        if (clip.spatial) {
        src.spatialBlend = 1;
        src.rolloffMode = AudioRolloffMode.Linear;
        src.maxDistance = clip.range;
        src.dopplerLevel = 0;
        }
        

        return src;
        


    }

    public bool ClipLoop() {

        foreach(CustomAudioClip clip in clips) {
            if (clip.loop) {
                return true;
            }
        }
        return false;
    }

    public AudioSource newSource() {
            AudioSource newSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
            newSource.playOnAwake = false;
            sources.Add(newSource);
            return newSource;
    }

    public void StopAll() {

        foreach(AudioSource src in sources) {
            src.Stop();
        }
    }
}
