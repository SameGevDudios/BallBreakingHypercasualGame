using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _heart, _menuElements, _gameElements, _skinLock;
    [SerializeField] private GameObject _skinsPanel;
    [SerializeField] private TMP_Text _scoreText, _recordText;
    [SerializeField] private Button[] _skinButton;
    [SerializeField] private Image _skinsBar;
    [SerializeField] private Sprite[] _skinSprite;
    [SerializeField] private Animator _scoreAnimator;
    public void UpdateHealthUI(int health, int maxHealth)
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < health) _heart[i].SetActive(true);
            else _heart[i].SetActive(false);
        }
    }
    public void SwitchGameUI(bool gameStarted)
    {
        foreach (GameObject element in _menuElements) element.SetActive(!gameStarted);
        foreach (GameObject element in _gameElements) element.SetActive(gameStarted);
    }
    public void UpdateScoreText(int score)
    {
        _scoreText.text = score.ToString();
        _scoreAnimator.SetTrigger("Action");
    }
    public void UpdateRecordText(int record) => _recordText.text = "Record: " + record.ToString();
    public void SwitchSkinsPanel() => _skinsPanel.SetActive(!_skinsPanel.activeSelf);
    public void UnlockSkinButtons(int skinsUnlocked)
    {
        for (int i = 0; i < _skinButton.Length; i++)
        {
            _skinLock[i].SetActive(skinsUnlocked <= i);
            _skinButton[i].interactable = skinsUnlocked > i;
        }
    }
    public void UpdateSkinSprite(int index) => _skinsBar.sprite = _skinSprite[index];
    public void UpdateSkinsBar(float fillAmount) => _skinsBar.fillAmount = fillAmount;
    public void ProgressSkinBar(float xp) => StartCoroutine(ProgressingSkinsBar(xp));
    private IEnumerator ProgressingSkinsBar(float xp)
    {
        float fillSpeed = 0.8f;
        while (_skinsBar.fillAmount / xp < 0.99f)
        {
            UpdateSkinsBar(_skinsBar.fillAmount + fillSpeed * Time.deltaTime);
            print("Moving...");
            //UpdateSkinsBar(Mathf.Lerp(_skinsBar.fillAmount, xp, fillDelta * Time.deltaTime));
            //print("Current lerp: " + Mathf.Lerp(_skinsBar.fillAmount, xp, fillDelta * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
        _skinsBar.fillAmount = xp;
    }
}
