using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

[Serializable]
public struct SceneScriptLine{
    public AudioClip audio;
    public string characterTeg;
    public float delayAfterAudio;
}
public class SceneScripteData : MonoBehaviour
{
    [SerializeField]
    private List<SceneScriptLine> Script;
    private int indexLineScript = 0;

    public int GetScriptLineCount(){
        return Script.Count;
    }

    public string[] GetCharactersTags(){
        return (from line in Script.DistinctBy(line => line.characterTeg) select line.characterTeg).ToArray();
    }

    public SceneScriptLine GetNextLine(){
        if (!(indexLineScript < Script.Count))
            throw new Exception($"Script List Count: {Script.Count}, i: {indexLineScript}");
        indexLineScript++;
        return Script[indexLineScript-1];
    }
}
