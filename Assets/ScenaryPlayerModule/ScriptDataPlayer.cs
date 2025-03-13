using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public struct CharacterStruct
{
    public string key;
    public CharacterScriptObj Character;
}
public class ScriptDataPlayer : MonoBehaviour
{
    [SerializeField]
    private List<CharacterStruct> characterObjs;
    private int ScriptLineCount;
    [SerializeField]
    private SceneScripteData sceneScript;
    private CharacterScriptObj FindTagOnList(string teg)
    {
        foreach (CharacterStruct character in characterObjs)
        {
            if (character.key == teg)
                return character.Character;
        }
        return null;
    }
    public void Play()
    {
        print("Start");
        StartCoroutine(play());
    }
    private IEnumerator play(){
        SceneScriptLine characterObj;
        for (int i = 0; i < ScriptLineCount; i++)
        {
            characterObj = sceneScript.GetNextLine();
            ActualScriptInFo.Actual = FindTagOnList(characterObj.characterTeg).gameObject;
            FindTagOnList(characterObj.characterTeg).PlayAudio(characterObj.audio);
            yield return new WaitForSeconds(characterObj.audio.length * (characterObj.delayAfterAudio + 1));
        }
    }

    void Start()
    {
        ScriptLineCount = sceneScript.GetScriptLineCount();
    }
}
