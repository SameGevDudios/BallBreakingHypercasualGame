using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private SkinsManager _skinsManager;
    private int _coins;
    [SerializeField] private int _skinPrice;
    #region Singleton
    public static CoinsManager Instance;
    private void Awake() => Instance = this;
    #endregion
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        LoadCoins();
        _gameUI.UpdateCoinsText(_coins);
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) GetCoins(1000);  
    }
#endif
    private void LoadCoins() => _coins = PlayerPrefs.GetInt("Coins", 0);
    private void SaveCoins() => PlayerPrefs.SetInt("Coins", _coins);
    public void GetCoins(int amount)
    {
        _coins += amount;
        SaveCoins();
        _gameUI.UpdateCoinsText(_coins);
    }
    public void BuyNewSkin()
    {
        if(_coins >= _skinPrice)
        {
            _coins -= _skinPrice;
            SaveCoins();
            _skinsManager.UnlockNewSkin();
            _gameUI.UpdateCoinsText(_coins);
        }
    }
}
