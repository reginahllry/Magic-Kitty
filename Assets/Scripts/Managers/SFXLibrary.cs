using System.Collections.Generic;
using UnityEngine;

public class SFXLibrary : MonoBehaviour
{
    [SerializeField] private SFXGroup[] sfxGroups;
    private Dictionary<string, List<AudioClip>> soundDictionary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        soundDictionary = new Dictionary<string, List<AudioClip>>();

        foreach (SFXGroup sfxGroup in sfxGroups)
        {
            soundDictionary[sfxGroup.name] = sfxGroup.audioClips; 
        }
    }

    public AudioClip GetRandomClip(string name)
    {
        if (soundDictionary.ContainsKey(name))
        {
            List<AudioClip> audioClips = soundDictionary[name];

            if (audioClips.Count > 0)
            {
                return audioClips[Random.Range(0, audioClips.Count)];
            }
        }

        Debug.Log("Null?");
        return null;
    }

}

[System.Serializable]
public struct SFXGroup
{
    public string name;
    public List<AudioClip> audioClips;
}