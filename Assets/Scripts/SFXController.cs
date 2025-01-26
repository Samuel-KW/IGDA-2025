using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
public class SFXController : MonoBehaviour
{
    List<AudioSource> sfx = new List<AudioSource>();

    System.Random rand = new System.Random();
    string prev_sound;

    List<string> loop_list = new List<string>();
    float loop_time = 1.0f;
    float loop_timer = 0.0f;
    float loop_variation = 0.0f;
    bool is_looping = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in transform)
        {
            AudioSource audioSource = child.GetComponent<AudioSource>();
            sfx.Add(audioSource);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (is_looping)
        {
            loop_timer += Time.deltaTime;
            if (loop_timer > loop_time)
            {
                PlayRandom(loop_list, loop_variation);
                loop_timer -= loop_time;
            }
        }
    }


    public void StartLoop(float loop_length, List<string> loop_sounds, float pitch_variation = 0.0f)
    {
        is_looping = true;
        loop_time = loop_length;
        loop_timer = 0.0f;
        loop_list = new List<string>();
        loop_variation = pitch_variation;
        foreach (string name in loop_sounds)
        {
            loop_list.Add(name);
        }
    
    }

    public void EndLoop()
    {
        is_looping = false;
    }

    public void Play(string sound = "", float pitch_range_rand = 0.0f)
    {
        if (sound.Equals(""))
        {
            float pitch = 1.0f + UnityEngine.Random.Range(-pitch_range_rand, pitch_range_rand);
            sfx[0].pitch = pitch;
            sfx[0].Play();
        } else
        {
            AudioSource chosen = Find(sound);
            float pitch = 1.0f + UnityEngine.Random.Range(-pitch_range_rand, pitch_range_rand);
            chosen.pitch = pitch;
            chosen.Play();
        }
    }




    private AudioSource Find(string id)
    {
        foreach (AudioSource song in sfx)
        {
            if (song.name.Contains(id))
            {
                return song;
            }
        }
        return null;
    }

    public void PlayRandom(List<string> from, float pitch_range_rand = 0.0f, bool exclusive = true)
    {
        string chosen = null;
        if (exclusive)
        {
            int exit = 0;
            chosen = from[rand.Next(0, from.Count)];
            while (prev_sound == chosen || exit == 100)
            {
                chosen = from[rand.Next(0, from.Count)];
                exit++;
            }
        } else
        {
            chosen = from[rand.Next(0, sfx.Count)];
        }

        float pitch = 1.0f + UnityEngine.Random.Range(-pitch_range_rand, pitch_range_rand);

        AudioSource source = Find(chosen);
        source.pitch = pitch;
        source.Play();

        prev_sound = chosen;

    }
}
