using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public enum AudioList
    {
        JinglesSax,
        BeltHandle,
        BookFlip,
        Chop,
        ClothBelt,
        ClothBelt2
    }

    [SerializeField] private List<AudioClip> clips;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    [Header("Mixer")]
    [SerializeField] private AudioMixer _mixer;

    [Header("Testing")]
    [SerializeField] private Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(PlayWithButton);
    }

    private void PlayWithButton()
    {
        PlayRandomSound(AudioList.Chop);
    }

    public void PlaySound(AudioList audioName, bool oneShot = true)
    {
        AudioClip clip = clips[(int) audioName];
        if (clip)
        {
            soundSource.pitch = 1;
            soundSource.clip = clip;

            if (oneShot)
                soundSource.PlayOneShot(clip);
            else
                soundSource.Play();
        }
    }

    public void PlayRandomSound(AudioList audioName, float rangeLimit = 0.1f)
    {
        AudioClip clip = clips[(int) audioName];
        if (clip)
        {
            soundSource.pitch = 1 + Random.Range(-rangeLimit,rangeLimit);
            soundSource.PlayOneShot(clip);
        }
    }


    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(PlayWithButton);
    }

    //AudioManager.Instance.PlaySound(AudioList.JinglesSax);
}
