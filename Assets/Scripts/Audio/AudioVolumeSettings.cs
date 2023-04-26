using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeSettings : MonoBehaviour
{
   private enum VolumeType
    {
        MASTER,
        MUSIC,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = this.GetComponentInChildren<Slider>();
    }
    private void Start()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = LevelManager.Instance.GameSave.MasterSlider;
                Debug.Log(LevelManager.Instance.GameSave.MasterSlider);
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = LevelManager.Instance.GameSave.MusicSlider;
                break;
            case VolumeType.SFX:
                volumeSlider.value = LevelManager.Instance.GameSave.SFXSlider;
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }
    }

    // Set Unity GameObject slider to AudioManager values
    private void Update()
    {
       /* switch (volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = AudioManager.instance.masterVolume;
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = AudioManager.instance.musicVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.instance.sfxVolume;
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }*/
    }

    // Update AudioManager according to slider value
    public void onSliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.instance.masterVolume = volumeSlider.value;
                LevelManager.Instance.GameSave.MasterSlider =  volumeSlider.value;
                break;

            case VolumeType.MUSIC:
                AudioManager.instance.musicVolume = volumeSlider.value;
                LevelManager.Instance.GameSave.MusicSlider = volumeSlider.value;
                break;

            case VolumeType.SFX:
                AudioManager.instance.sfxVolume = volumeSlider.value;
                LevelManager.Instance.GameSave.SFXSlider = volumeSlider.value;
                break;

            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }
    }
    private void OnDestroy()
    {
        GameSave.SaveGameSave(LevelManager.Instance.GameSave);
        Debug.Log("Destroyed boom");
    }
}
