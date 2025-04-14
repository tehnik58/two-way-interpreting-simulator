using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class CharacterScriptObj : MonoBehaviour
{
    [SerializeField]
    private string _name;
    private Animator _animator;
    [SerializeField]
    private float Speak;
    public void PlayAudio(AudioClip _clip)
    {
        GetComponent<AudioSource>().clip = _clip;
        print($"Play: {_name}");
        GetComponent<AudioSource>().Play();
        
        TargetStaticController.SetTarget(gameObject);
    }
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float[] spectrum = new float[256]; 

        GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.Rectangular );

        //bool fl = true;
        //float max = 0f;
        float dAudio = 0f;
        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            dAudio += spectrum[i];
        }
        dAudio = dAudio/spectrum.Length;
        Speak = dAudio;

        _animator.SetFloat("Blend1", Speak / 0.001f);
    }
}
