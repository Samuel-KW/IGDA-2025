using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    //public Flip TimerObject;

    List<AudioSource> music = new List<AudioSource>();
    List<AudioSource> fade_out = new List<AudioSource>();
    List<AudioSource> fade_in = new List<AudioSource>();
    List<AudioSource> queue_add = new List<AudioSource>();
    List<AudioSource> queue_remove = new List<AudioSource>();

    const float FADE_SPEED = 0.5f;
    public AudioSource tracker;

    float prev_progress = 0.0f;
    float timer = 0.0f;
    float timer_multiplier = 1.0f;

    int prev_power = 0;

 
    void Awake() {
        // Populate the list
        music.AddRange(GetComponentsInChildren<AudioSource>());
    }


    // Start is called before the first frame update
    void Start()
    {
        /*foreach (Transform child in transform)
        {
            AudioSource audioSource = child.GetComponent<AudioSource>();
            music.Add(audioSource);
        }*/


        //SetPitch(1.0f);
        MuteAll();
        //PlayAll();
        //SetMusicPowerNum(101.0f);
        //StartCoroutine(mus());
    }

    public void SetMusicPowerNum(float val){ // SO SCUFFED
        int power = 0;
        int[] thresholds = {3, 5, 7, 13, 20, 30, 45, 57, 80, 100};
        for (int i = 0; i < thresholds.Length; i++)
        {
            if (val >= thresholds[i]) power += 1;
        }

        if (power != prev_power)
        {
            //MuteAll();
            switch (power)
            {
                case 0:
                    Fade("TinTapper", true);
                    Queue("SmolSnare", false);
                    Queue("BubbleBell", false);
                    Queue("WompWomp", false);
                    Queue("ChipperChirp", false);
                    Queue("PontificatingPiccolo", false);
                    Queue("SizableSnare", false);
                    Queue("TimelyTriangle", false);
                    Queue("PrettyPluck", false);
                    Queue("TuhTimpani", false);
                    Queue("VictoryVells", false);
                    break;
                case 1:
                    Queue("TinTapper", true);
                    Queue("SmolSnare", false);
                    Queue("BubbleBell", true);
                    Queue("WompWomp", false);
                    Queue("ChipperChirp", false);
                    Queue("PontificatingPiccolo", false);
                    Queue("SizableSnare", false);
                    Queue("TimelyTriangle", false);
                    Queue("PrettyPluck", false);
                    Queue("TuhTimpani", false);
                    Queue("VictoryVells", false);
                    break;
                case 2:
                    Queue("TinTapper", true);
                    Queue("SmolSnare", true);
                    Queue("BubbleBell", true);
                    Queue("WompWomp", false);
                    Queue("ChipperChirp", false);
                    Queue("PontificatingPiccolo", false);
                    Queue("SizableSnare", false);
                    Queue("TimelyTriangle", false);
                    Queue("PrettyPluck", false);
                    Queue("TuhTimpani", false);
                    Queue("VictoryVells", false);
                    break;
                case 3:
                    Queue("TinTapper", true);
                    Queue("SmolSnare", true);
                    Queue("BubbleBell", true);
                    Queue("WompWomp", true);
                    Queue("ChipperChirp", false);
                    Queue("PontificatingPiccolo", false);
                    Queue("SizableSnare", false);
                    Queue("TimelyTriangle", false);
                    Queue("PrettyPluck", false);
                    Queue("TuhTimpani", false);
                    Queue("VictoryVells", false);
                    break;
                case 4:
                    Queue("TinTapper", true);
                    Queue("SmolSnare", true);
                    Queue("BubbleBell", true);
                    Queue("WompWomp", true);
                    Queue("ChipperChirp", true);
                    Queue("PontificatingPiccolo", false);
                    Queue("SizableSnare", false);
                    Queue("TimelyTriangle", false);
                    Queue("PrettyPluck", false);
                    Queue("TuhTimpani", false);
                    Queue("VictoryVells", false);
                    break;
                case 5:
                    Queue("TinTapper", true);
                    Queue("SmolSnare", true);
                    Queue("BubbleBell", true);
                    Queue("WompWomp", true);
                    Queue("ChipperChirp", true);
                    Queue("PontificatingPiccolo", true);
                    Queue("SizableSnare", false);
                    Queue("TimelyTriangle", false);
                    Queue("PrettyPluck", false);
                    Queue("TuhTimpani", false);
                    Queue("VictoryVells", false);
                    break;
                case 6:
                    Queue("TinTapper", true);
                    Queue("SmolSnare", true);
                    Queue("BubbleBell", true);
                    Queue("WompWomp", true);
                    Queue("ChipperChirp", true);
                    Queue("PontificatingPiccolo", true);
                    Queue("SizableSnare", true);
                    Queue("TimelyTriangle", false);
                    Queue("PrettyPluck", false);
                    Queue("TuhTimpani", false);
                    Queue("VictoryVells", false);
                    break;
                case 7:
                    Queue("TinTapper", true);
                    Queue("SmolSnare", true);
                    Queue("BubbleBell", true);
                    Queue("WompWomp", true);
                    Queue("ChipperChirp", true);
                    Queue("PontificatingPiccolo", true);
                    Queue("SizableSnare", true);
                    Queue("TimelyTriangle", true);
                    Queue("PrettyPluck", true);
                    Queue("TuhTimpani", false);
                    Queue("VictoryVells", false);
                    break;
                case 8:
                    Queue("TinTapper", true);
                    Queue("SmolSnare", true);
                    Queue("BubbleBell", true);
                    Queue("WompWomp", true);
                    Queue("ChipperChirp", true);
                    Queue("PontificatingPiccolo", false);
                    Queue("SizableSnare", true);
                    Queue("TimelyTriangle", true);
                    Queue("PrettyPluck", true);
                    Queue("TuhTimpani", true);
                    Queue("VictoryVells", false);
                    break;
                case 9:
                    Queue("TinTapper", true);
                    Queue("SmolSnare", true);
                    Queue("BubbleBell", true);
                    Queue("WompWomp", true);
                    Queue("ChipperChirp", true);
                    Queue("PontificatingPiccolo", true);
                    Queue("SizableSnare", true);
                    Queue("TimelyTriangle", true);
                    Queue("PrettyPluck", true);
                    Queue("TuhTimpani", true);
                    Queue("VictoryVells", false);
                    break;
                case 10:
                    Queue("TinTapper", true);
                    Queue("SmolSnare", true);
                    Queue("BubbleBell", true);
                    Queue("WompWomp", true);
                    Queue("ChipperChirp", true);
                    Queue("PontificatingPiccolo", true);
                    Queue("SizableSnare", true);
                    Queue("TimelyTriangle", true);
                    Queue("PrettyPluck", true);
                    Queue("TuhTimpani", true);
                    Queue("VictoryVells", true);
                    break;
                default:
                    break;
            }
        }
        prev_power = power;
        
    }

    //IEnumerator mus()
    //{
    //    Fade("TinTapper", true);
    //    Fade("SmolSnare", true);
    //    Fade("BubbleBell", true);
    //    yield return new WaitForSeconds(5);
    //    //TimerObject.Play();
    //    Queue("WompWomp", true);
    //    Queue("ChipperChirp", true);
    //    Queue("PontificatingPiccolo", true);
    //    Queue("SizableSnare", true);
    //    Queue("TimelyTriangle", true);
    //    Queue("PrettyPluck", true);
    //    Queue("TuhTimpani", true);
    //    Queue("VictoryVells", true);
    //    Queue("PontificatingPiccolo", true);
    //    yield return new WaitForSeconds(16);


    //    Queue("WompWomp", true);
    //    yield return new WaitForSeconds(16);
    //    Queue("ChipperChirp", true);
    //    yield return new WaitForSeconds(16);
    //    Queue("PontificatingPiccolo", true);
    //    yield return new WaitForSeconds(16);
    //    Queue("SizableSnare", true);
    //    yield return new WaitForSeconds(16);
    //    Queue("TimelyTriangle", true);
    //    yield return new WaitForSeconds(16);
    //    Queue("PrettyPluck", true);
    //    yield return new WaitForSeconds(16);
    //    Queue("TuhTimpani", true);
    //    Queue("PontificatingPiccolo", false);
    //    yield return new WaitForSeconds(16);
    //    Queue("VictoryVells", true);
    //    Queue("PontificatingPiccolo", true);
    //    yield return new WaitForSeconds(16);
    //    //Fade("MainSynth", true);
    //}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * timer_multiplier;
        if ((int)timer % 16 == 15 && (queue_add.Count != 0 || queue_remove.Count != 0))
        {
            // Create copies to iterate over
            var queueAddCopy = new List<AudioSource>(queue_add);
            var queueRemoveCopy = new List<AudioSource>(queue_remove);

            foreach (AudioSource q in queueAddCopy)
            {
                q.volume = q.reverbZoneMix;
                queue_add.Remove(q);
            }
            foreach (AudioSource q in queueRemoveCopy)
            {
                q.volume = 0.0f;
                queue_remove.Remove(q);
            }
        }


        if (fade_in.Count > 0)
        {
            foreach (AudioSource fade in fade_in.ToArray())
            {
                if (fade.volume < fade.reverbZoneMix)
                {
                    //fade.volume += Time.deltaTime * FADE_SPEED;
                    fade.volume = fade.reverbZoneMix;
                }
                else
                {
                    fade.volume = fade.reverbZoneMix;
                    fade_in.Remove(fade);
                }
            }
        }

        if (fade_out.Count > 0)
        {
            foreach (AudioSource fade in fade_out)
            {
                if (fade.volume > 0.0f)
                {
                    fade.volume -= Time.deltaTime * FADE_SPEED;
                }
                else
                {
                    fade.volume = 0.0f;
                    fade_out.Remove(fade);
                }
            }
        }

        if (tracker.time < prev_progress)
        {
            timer = Time.deltaTime;
        }
        prev_progress = tracker.time;
    }

    private void PrintMusicSourceNames()
    {
        UnityEngine.Debug.LogWarning("Music :" + music.Count);
        foreach (AudioSource song in music)
        {
            UnityEngine.Debug.Log("Available Music Source: " + song.name);
        }
    }

    private AudioSource Find(string id)
    {
        foreach (AudioSource song in music)
        {
            if (song.name.Contains(id))
            {
                return song;
            }
        }
        UnityEngine.Debug.LogWarning("Song Not Found: " + id);
        PrintMusicSourceNames();
        return null;
    }

    public void Fade(string id, bool on)
    {
        AudioSource source = Find(id);
        if (on)
        {
            if (fade_out.Contains(source)) { fade_out.Remove(source); }
            fade_in.Add(source);
        }
        else
        {
            if (fade_in.Contains(source)) { fade_in.Remove(source); }
            fade_out.Add(source);
        }
    }
    public void Queue(string id, bool on)
    {
        AudioSource source = Find(id);
        if (on)
        {
            if (queue_remove.Contains(source)) { queue_remove.Remove(source); }
            queue_add.Add(source);
            source.Play();
        }
        else
        {
            if (queue_add.Contains(source)) { queue_add.Remove(source); }
            queue_remove.Add(source);
            source.Stop();
        }
    }

    public void PlayRandom(List<string> from_ids)
    {
        Queue(from_ids[UnityEngine.Random.Range(0, from_ids.Count)], true);
    }

    public void MuteAll()
    {
        foreach (AudioSource song in music)
        {
            song.volume = 0.0f;
        }
        fade_out = new List<AudioSource>();
        queue_add = new List<AudioSource>();
        queue_remove = new List<AudioSource>();

    }

    public void PlayAll()
    {
        foreach (AudioSource song in music)
        {
            song.Play();
        }

    }

    public void SetPitch(float pitch)
    {
        foreach (AudioSource song in music)
        {
            song.pitch = pitch;
        }
        timer_multiplier = pitch;
    }


}