
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] Slider musicSlider;
    [SerializeField] AudioMixer musicMixer;
    [Header("SFX")]
    [SerializeField] Slider SFXSlider;
    [SerializeField] AudioMixer SFXMixer;
    [Header("Master")]
    [SerializeField] Slider masterSlider;
    [SerializeField] AudioMixer masterMixer;
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        musicMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        SFXMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
}
