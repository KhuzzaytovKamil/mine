using UnityEngine;
using UnityEngine.UI;

public class AudioSwitch : MonoBehaviour
{
    [SerializeField]
    private Image AudioIcon;
    [SerializeField]
    private Sprite SoundOn;
    [SerializeField]
    private Sprite SoundOff;
    [SerializeField]
    private GameObject AudioListener;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("AudioListener") == 0)
        {
            if (AudioIcon != null)
            {
                AudioIcon.sprite = SoundOff;
            }
            AudioListener.SetActive(false);
        }
        else
        {
            if (AudioIcon != null)
            {
                AudioIcon.sprite = SoundOn;
            }
            AudioListener.SetActive(true);
        }
    }

    public void Switch()
    {
        if (PlayerPrefs.GetInt("AudioListener") == 0)
        {
            AudioIcon.sprite = SoundOn;
            PlayerPrefs.SetInt("AudioListener", 1);
            AudioListener.SetActive(true);
        }
        else
        {
            AudioIcon.sprite = SoundOff;
            PlayerPrefs.SetInt("AudioListener", 0);
            AudioListener.SetActive(false);
        }
    }

    public void OffAudio()
    {
        AudioIcon.sprite = SoundOff;
        PlayerPrefs.SetInt("AudioListener", 0);
        AudioListener.SetActive(false);
    }

    public void OnAudio()
    {
        AudioIcon.sprite = SoundOn;
        PlayerPrefs.SetInt("AudioListener", 1);
        AudioListener.SetActive(true);
    }
}
