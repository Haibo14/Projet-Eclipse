    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public string vol;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(vol, Mathf.Log10(sliderValue) * 20);
    }
}
