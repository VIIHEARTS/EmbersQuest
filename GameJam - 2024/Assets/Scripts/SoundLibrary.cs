using UnityEngine;


[System.Serializable]
public struct SoundEffect
{
    public string groupID;
    public AudioClip[] clips;
}
public class SoundLibrary : MonoBehaviour
{
    public SoundEffect[] soundEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip GetClipFromName(string name)
    {
        foreach (var soundEffect in soundEffect)
        {
            if (soundEffect.groupID == name)
            {
                return soundEffect.clips[Random.Range(0, soundEffect.clips.Length)];
            }
        }

        return null;
    }
}
