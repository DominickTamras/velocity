using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


// DEV NOTE: PLEASE CHECK FOOTSTEP SCRIPT FOR FOOTSTEP SOUNDS, THEY ARE NOT CONNECTED TO THE AUDIOMANAGER AT ALL.
public class AudioManager : MonoBehaviour
{
    
    public Sound[] sounds;      // store all our sounds
    public Sound[] playlist;    // store all our music

    private int currentPlayingIndex = 999; // set high to signify no song playing

    // a play music flag so we can stop playing music during cutscenes etc
    private bool shouldPlayMusic = false;

    public static AudioManager instance; // will hold a reference to the first AudioManager created

    private float mvol; // Global music volume
    private float evol; // Global effects volume

    private void Start()
    {
        if(SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "Credits")
        {
            Debug.Log("BG music Playing");
            PlaySound("BG");

        }
        //start the music
        PlayMusic();
    }


    private void Awake()
    {

        if (instance == null)
        {     // if the instance var is null this is first AudioManager
            instance = this;        //save this AudioManager in instance 
        }
        else
        {
            Destroy(gameObject);    // this isnt the first so destroy it
            return;                 // since this isn't the first return so no other code is run
        }

        DontDestroyOnLoad(gameObject); // do not destroy me when a new scene loads

        // get preferences
        mvol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        evol = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);

        createAudioSources(sounds, evol);     // create sources for effects
        createAudioSources(playlist, mvol);   // create sources for music

    }

    // create sources
    private void createAudioSources(Sound[] sounds, float volume)
    {
        foreach (Sound s in sounds)
        {   // loop through each music/effect
            s.source = gameObject.AddComponent<AudioSource>(); // create anew audio source(where the sound splays from in the world)
            s.source.clip = s.clip;     // the actual music/effect clip
            s.source.volume = s.volume * volume; // set volume based on parameter
            s.source.pitch = s.pitch;   // set the pitch
            s.source.loop = s.loop;     // should it loop
            s.source.spatialBlend = s.spatialBlend; //should it be 2d or 3d
            s.source.outputAudioMixerGroup = s.audioGroupAdd;
        }
    }

    public void PlaySound(string name)
    {
        // here we get the Sound from our array with the name passed in the methods parameters
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            //Debug.LogError("Unable to play sound " + name);
            return;
        }
        //s.source.pitch = UnityEngine.Random.Range(0.7f, 1.1f);
        s.source.Play(); // play the sound
    }

    public void PauseSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Unable to pause sound " + name);
            return;
        }
        s.source.Pause();
    }

    public bool SoundIsPaused(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Unable to pause sound " + name);
            return false;
        }
        return true;
    }

    public void UnpauseSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Unable to unpause sound " + name);
            return;
        }
        s.source.UnPause();
    }

    public void StopSound(string name)
    {
       
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Unable to stop sound " + name);
            return;
        }
        s.source.Stop();
    }

    public bool SoundIsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Unable to find sound " + name);
            return false;
        }
        return s.source.isPlaying;
    }

    public void PlayMusic()
    {
        //Must have a song ready in the playlist to do anything
        if (shouldPlayMusic == false && playlist.Length > 0)
        {
            shouldPlayMusic = true;
            // pick a random song from our playlist
            currentPlayingIndex = UnityEngine.Random.Range(0, playlist.Length - 1);
            playlist[currentPlayingIndex].source.volume = playlist[0].volume * mvol; // set the volume
            playlist[currentPlayingIndex].source.Play(); // play it
        }

    }

    // stop music
    public void StopMusic()
    {
        if (shouldPlayMusic == true)
        {
            shouldPlayMusic = false;
            currentPlayingIndex = 999; // reset playlist counter
        }
    }

    public IEnumerator FadeOut(string name, float FadeTime)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Unable to fade out sound " + name);
            yield return null;
        }
        float startVolume = s.source.volume;

        while (s.source.volume > 0)
        {
            s.source.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        s.source.Stop(); //Change back to Pause() if janky
        startVolume = s.source.volume;
        
      
    }

    public IEnumerator FadeIn(string name, float FadeTime)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Unable to fade out sound " + name);
            yield return null;
        }
        s.source.Play();
        float startVolume = s.source.volume;
        s.source.volume = 0;

        while (s.source.volume < startVolume)
        {
            //Debug.Log("Fade in volume: " + s.source.name + s.source.volume);
            s.source.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

    }

    public IEnumerator FadeInUnpause(string name, float FadeTime)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Unable to fade out sound " + name);
            yield return null;
        }
        s.source.UnPause();
        float startVolume = s.source.volume;
        s.source.volume = 0;

        while (s.source.volume < startVolume)
        {
            s.source.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

    }
    void Update()
    {
        // if we are playing a track from the playlist && it has stopped playing
        /*if (currentPlayingIndex != 999 && !playlist[currentPlayingIndex].source.isPlaying)
        {
            currentPlayingIndex++; // set next index
            if (currentPlayingIndex >= playlist.Length)
            { //have we went too high
                currentPlayingIndex = 0; // reset list when max reached
            }
            playlist[currentPlayingIndex].source.Play(); // play that funky music
        }*/
    }

    // get the song name
    public String getSongName()
    {
        return playlist[currentPlayingIndex].name;
    }

    // if the music volume change update all the audio sources
    public void musicVolumeChanged()
    {
        foreach (Sound m in playlist)
        {
            mvol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            m.source.volume = playlist[0].volume * mvol;
        }
    }

    //if the effects volume changed update the audio sources
    public void effectVolumeChanged()
    {
        evol = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * evol;
        }
        sounds[0].source.Play(); // play an effect so user can her effect volume
    }
}