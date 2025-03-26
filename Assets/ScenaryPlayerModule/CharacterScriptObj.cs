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
            dAudio += spectrum[i];/*
            if(  new Vector3 ( Mathf.Log (i), Mathf.Log (spectrum[i]), 3).magnitude < new Vector3 ( Mathf.Log (i), spectrum[i] - 10, 1).magnitude / 2 )
            {
                if(new Vector3 ( Mathf.Log (i), spectrum[i] - 10, 1).magnitude / new Vector3 ( Mathf.Log (i), Mathf.Log (spectrum[i]), 3).magnitude > max)
                    max = new Vector3 ( Mathf.Log (i), spectrum[i] - 10, 1).magnitude / new Vector3 ( Mathf.Log (i), Mathf.Log (spectrum[i]), 3).magnitude;
                fl = false;
            }
            
            if(fl)
                GetComponent<Animator>().SetFloat("Blend", 0f);
            else
                _animator.SetFloat("Blend", max);*/
        }
        dAudio = dAudio/spectrum.Length;
        //print(dAudio);
        Speak = dAudio;

        _animator.SetFloat("Blend", Speak / 0.002f);
    }
}
