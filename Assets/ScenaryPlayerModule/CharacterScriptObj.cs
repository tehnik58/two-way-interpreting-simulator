using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScriptObj : MonoBehaviour
{
    [SerializeField]
    private string _name;
    private Animator _animator;
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

        // Цикл по заполненному массиву
        // Начинаем цикл с 1 и до 1 меньше длины, чтобы цикл мог рисовать линии между соседними ячейками.
        bool fl = true;
        float max = 0f;
        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            if(  new Vector3 ( Mathf.Log (i), Mathf.Log (spectrum[i]), 3).magnitude < new Vector3 ( Mathf.Log (i), spectrum[i] - 10, 1).magnitude / 2 )
            {
                if(new Vector3 ( Mathf.Log (i), spectrum[i] - 10, 1).magnitude / new Vector3 ( Mathf.Log (i), Mathf.Log (spectrum[i]), 3).magnitude > max)
                    max = new Vector3 ( Mathf.Log (i), spectrum[i] - 10, 1).magnitude / new Vector3 ( Mathf.Log (i), Mathf.Log (spectrum[i]), 3).magnitude;
                fl = false;
            }
            
            if(fl)
                GetComponent<Animator>().SetFloat("Blend", 0f);
            else
                _animator.SetFloat("Blend", max);
            Debug.DrawLine (new Vector3 (i - 1, spectrum[i] + 10, 0), new Vector3 (i, spectrum[i + 1] + 10, 0), Color.red );
            Debug.DrawLine (new Vector3 (i - 1, Mathf.Log (spectrum[i - 1]) + 10, 2), new Vector3 (i, Mathf.Log (spectrum[i]) + 10, 2), Color.cyan );
            Debug.DrawLine (new Vector3 ( Mathf.Log (i - 1), spectrum[i - 1] - 10, 1), new Vector3 ( Mathf.Log (i), spectrum[i] - 10, 1), Color.green );
            Debug.DrawLine (new Vector3 ( Mathf.Log (i - 1), Mathf.Log (spectrum[i - 1]), 3), new Vector3 ( Mathf.Log (i), Mathf.Log (spectrum[i]), 3), Color.blue );
        }
    }
}
