using UnityEngine;
using UnityEngine.Audio;

public sealed class SettingsMenu : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private AudioMixer _soundsMixer;
    [SerializeField] private AudioMixer _musicMixer;

    public void AddSoundsVolume()
    {
        _soundsMixer.SetFloat("Volume", 0f);
        
    }

    public void ReduceSoundsVolume()
    {

    }

    public void AddMusicVolume()
    {

    }
    
    public void ReduceMusicVolume()
    {

    }
}
