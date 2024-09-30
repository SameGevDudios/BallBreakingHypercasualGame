using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordCounter : MonoBehaviour
{
    [SerializeField] GameUI _gameUI;
    private int _score, _record;
    private void Start()
    {
        _record = PlayerPrefs.GetInt("Record", 0);
        _gameUI.UpdateRecordText(_record);
    }
    public void GameStarted() => _score = 0;
    public void AddScore()
    {
        _score++;
        _gameUI.UpdateScoreText(_score);
    }
    public void SaveRecord() => PlayerPrefs.SetInt("Record", Mathf.Max(_score, _record));
    public int GetScore() => _score;
}
