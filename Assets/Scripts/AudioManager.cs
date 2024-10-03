using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private GameUI _gameUI;
    private bool _audioOn = true;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickAudio, _choosePlatformAudio, _winAudio, _looseAudio, _deathAudio, _skinUnlockAudio;

    #region Singleton
    public static AudioManager Instance;
    private void Awake() => Instance = this;
    #endregion
    private void Start()
    {
        LoadAudio();
        SetVolume();
    }
    private void LoadAudio() => _audioOn = PlayerPrefs.GetInt("Audio", 1) == 1;
    private void SaveAudio() => PlayerPrefs.SetInt("Audio", _audioOn ? 1 : 0);
    public void SwitchSound()
    {
        _audioOn = !_audioOn;
        SetVolume();
        SaveAudio();
    }
    private void SetVolume()
    {
        _audioMixer.SetFloat("Volume", _audioOn ? 0 : -80);
        _gameUI.ActivateSoundStripe(_audioOn);
    }
    public void PlayClickAudio() => _audioSource.PlayOneShot(_clickAudio);
    public void PlayChoosePlatformAudio() => _audioSource.PlayOneShot(_choosePlatformAudio);
    public void PlayWinAudio() => _audioSource.PlayOneShot(_winAudio);
    public void PlayLooseAudio() => _audioSource.PlayOneShot(_looseAudio);
    public void PlayDeathAudio() => _audioSource.PlayOneShot(_deathAudio);
    public void PlaySkinUnlockAudio() => _audioSource.PlayOneShot(_skinUnlockAudio);

}
