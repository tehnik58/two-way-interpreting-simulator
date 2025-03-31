using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Text;
using Unity.VisualScripting;

[Serializable]
public struct SceneScriptLine{
    public AudioClip audio;
    public string characterTeg;
    public float delayAfterAudio;
    public Material material;

    public SceneScriptLine(AudioClip audio, string characterTeg, float delayAfterAudio)
    {
        this.audio = audio;
        this.characterTeg = characterTeg;
        this.delayAfterAudio = delayAfterAudio;
        material = null;
    }
}
public class SceneScripteData : MonoBehaviour
{
    [SerializeField]
    public string SneneName;
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

    private void Start()
    {
        string BaseUrl = "C:\\Scenes";
        if (!File.Exists(BaseUrl))
            Directory.CreateDirectory(BaseUrl);
        if (!File.Exists($"{BaseUrl}\\{SneneName}"))
            Directory.CreateDirectory($"{BaseUrl}\\{SneneName}");
        Debug.Log($"{BaseUrl}\\{SneneName}.txt");
        using (FileStream fstream = new FileStream($"{BaseUrl}\\{SneneName}\\{SneneName}.txt", FileMode.OpenOrCreate))
        {
            float scale = 1.0f;
            byte[] buffer = new byte[fstream.Length];
            fstream.Read(buffer, 0, buffer.Length);
            string text = Encoding.UTF8.GetString(buffer);
            Debug.Log(text);

            string[] lines = text.Split('\n');
            Debug.Log(lines[0].Split(':')[1].Split(',')[1]);
            scale = float.Parse(lines[0].Split(':')[1].Split(',')[1], CultureInfo.InvariantCulture.NumberFormat);

            for (int i = 1; i < lines.Length-1; i++)
            {
                using (WWW www = new WWW($"file://C:\\{lines[i].Split(':')[1]}"))
                {
                    Debug.Log(lines[i]);
                    Debug.Log($"file://C:\\{lines[i].Split(':')[1]}\t{lines[i].Split(':')[0]}\t{scale}");
                    AudioClip audio = www.GetAudioClip();
                    audio.name = lines[i].Split(':')[1];
                    Script.Add(new SceneScriptLine(audio, lines[i].Split(':')[0], scale));
                }
            }
        }
    }
}
