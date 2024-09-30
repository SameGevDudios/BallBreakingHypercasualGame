using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private RecordCounter _recordCounter;
    [SerializeField] private GameObject[] _skin;
    [SerializeField] private ParticleSystem _unlockParticles;
    [SerializeField] private Animator _unlockAnimator;
    private int _currentSkin, _unlockedSkins, _skinProgression, _xpToProgress = 100;
    private void Start()
    {
        LoadCurrentSkin();
        LoadSkins();
        ChooseSkin(_currentSkin);
        _gameUI.UnlockSkinButtons(_unlockedSkins);
        _gameUI.UpdateSkinsBar(_skinProgression);
    }
    public void ChooseSkin(int index)
    {
        _skin[_currentSkin].SetActive(false);
        _currentSkin = index;
        _skin[_currentSkin].SetActive(true);
        SaveCurrentSkin();
        _gameManager.SkinChanged(_skin[_currentSkin]);
    }
    public void GameEnded()
    {
        _gameUI.UpdateSkinsBar(_skinProgression / (float)_xpToProgress);
        _gameUI.UpdateSkinSprite(_unlockedSkins);
    }
    public void ProgressSkins()
    {
        _skinProgression += _recordCounter.GetScore();
        _gameUI.ProgressSkinBar(_skinProgression / (float)_xpToProgress);

        SaveSkins();
        Invoke("UnlockSkin", 1.1f);
    }
    private void UnlockSkin()
    {
        if (_skinProgression >= _xpToProgress)
        {
            _unlockedSkins++;
            _skinProgression = 0;
            SaveSkins();
            _gameUI.UnlockSkinButtons(_unlockedSkins);
            _unlockParticles.Play();
            _unlockAnimator.SetTrigger("SkinUnlocked");
        }
    }
    private void SaveCurrentSkin() => PlayerPrefs.SetInt("CurrentSkin", _currentSkin);
    private void LoadCurrentSkin() => _currentSkin = PlayerPrefs.GetInt("CurrentSkin", 0);
    private void SaveSkins()
    {
        PlayerPrefs.SetInt("UnlockedSkins", _unlockedSkins);
        PlayerPrefs.SetInt("SkinsProgression", _skinProgression);
    }
    private void LoadSkins()
    {
        _unlockedSkins = PlayerPrefs.GetInt("UnlockedSkins", 0);
        _skinProgression = PlayerPrefs.GetInt("SkinsProgression", 0);
    }
}
