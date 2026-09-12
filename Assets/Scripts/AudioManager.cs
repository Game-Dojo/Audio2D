using System.Collections;
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
        ClothBelt2,
        Footstep
    }

    [SerializeField] private List<AudioClip> clips;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    [Header("Mixer")]
    [SerializeField] private AudioMixer mixer;

    [Header("Testing")]
    [SerializeField] private Button playButton;

    private static AudioManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("AudioManager is a Singleton: An instance already exists");
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        playButton.onClick.AddListener(PlayWithButton);
    }

    private void PlayWithButton()
    {
        PlayRandomSound(AudioList.Footstep);
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

    public void PlaySoundAt(AudioList audioName, Vector3 position, bool randomSound = false)
    {
        AudioClip clip = clips[(int) audioName];
        if (clip)
        {
            GameObject go = new GameObject();
            go.transform.position = position;

            AudioSource source = go.AddComponent<AudioSource>();
            source.spatialBlend = 1.0f;
            source.rolloffMode = AudioRolloffMode.Linear;
            
            source.minDistance = 2.0f;
            source.maxDistance = 5.0f;

            source.clip = clip;
            source.pitch = 1 + Random.Range(-.5f,.5f);

            source.PlayOneShot(clip);

            Destroy(go, source.clip.length / source.pitch);            
        }
    }
    
    public void PlayRandomSound(AudioList audioName, float rangeLimit = 0.1f)
    {
        AudioClip clip = clips[(int) audioName];
        if (clip)
        {
            soundSource.pitch = 1 + Random.Range(-rangeLimit,rangeLimit);
            soundSource.clip = clip;
            soundSource.PlayOneShot(clip);
        }
    }

    private IEnumerator FadeGroup()
    {
        mixer.GetFloat("MusicVolume", out float musicVolume);
        var waitTime = new WaitForSeconds(0.1f);
        while (musicVolume > -80)
        {
            mixer.SetFloat("MusicVolume", musicVolume -= 2f);
            yield return waitTime;
        }
    }
    
    private IEnumerator FadeMixerGroup(float duration, float targetDb)
    {
        float currentTime = 0;
        
        mixer.GetFloat("Music", out float startDb);

        float startLinear = Mathf.Pow(10f, startDb / 20f);
        float targetLinear = Mathf.Pow(10f, targetDb / 20f);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;

            float currentLinear = Mathf.Lerp(startLinear, targetLinear, t);

            float newDb = Mathf.Log10(Mathf.Max(currentLinear, 0.0001f)) * 20f;
            
            mixer.SetFloat("Music", newDb);
            yield return null;
        }

        mixer.SetFloat("Music", targetDb);
    }
    
    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(PlayWithButton);
    }

    public static AudioManager Instance => _instance;
    
    //AudioManager.Instance.PlaySound(AudioList.JinglesSax);
}
