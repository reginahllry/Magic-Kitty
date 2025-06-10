using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    private static SFXManager Instance;
    private static AudioSource audioSource;
    private static AudioSource randomPitchAudioSource;
    private static SFXLibrary sfxLibrary;
    [SerializeField] private Slider sfxSlider;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            AudioSource[] audioSources = GetComponents<AudioSource>();
            audioSource = audioSources[0];
            randomPitchAudioSource = audioSources[1];

            sfxLibrary = GetComponent<SFXLibrary>();
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public static void Play(string soundName, bool randomPitch = false, float volume = 1)
    {
        Debug.Log("Playing: " + soundName);
        AudioClip audioClip = sfxLibrary.GetRandomClip(soundName);

        if (audioClip != null)
        {
            if (randomPitch)
            {
                randomPitchAudioSource.pitch = Random.Range(1f, 1.8f);
                randomPitchAudioSource.PlayOneShot(audioClip, volume);
            }

            else
            {
                audioSource.PlayOneShot(audioClip, volume);
            }
        }
    }

    void Start()
    {
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void OnValueChanged()
    {
        SetVolume(sfxSlider.value);
    }
}
