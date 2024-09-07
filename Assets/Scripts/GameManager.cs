using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ChangeColor _ball, _currentPlatform;
    private float _speed = 1f;
    private float _timer = 0.5f;
    private int _platformChosen;
    private bool _rightToWin;

    public void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= 1 / _speed)
        {
            CheckColors();
            _timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.A)) ChoosePlaftorm(false);
        if (Input.GetKeyDown(KeyCode.D)) ChoosePlaftorm(true);
    }
    public void SetColors()
    {
        int order = Random.Range(0, 2);
        int winColor = Random.Range(0, 4);
        _ball.Change(winColor);
        if(order == 0)
        {
            _currentPlatform.Change(winColor);
            _currentPlatform.Change();
        }
        else
        {
            _currentPlatform.Change();
            _currentPlatform.Change(winColor);
        }
        _rightToWin = order == 1;
    }
    public void ChoosePlaftorm(bool rightPlatform)
    {
        _platformChosen = rightPlatform ? 1 : 0;
        _currentPlatform.transform.eulerAngles = Vector3.up * (rightPlatform ? 89.99f : -89.99f);
    }
    public void CheckColors()
    {
        if (_platformChosen == 1)
        {
            if (_rightToWin) Win();
            else Loose();
        }
        else if(_platformChosen == 0)
        {
            if (!_rightToWin) Win();
            else Loose();
        }
        else
        {
            Loose();
        }
    }
    private void Win()
    {
        // Break cuurent platform,
        // Instantiate new platform from pool
        // _currentPlatform == poolPlatform
        // Move new platform
        _platformChosen = -1;
        _currentPlatform.transform.eulerAngles = Vector3.zero;
        SetColors();
        print("Player won, yay");

    }
    private void Loose()
    {
        // "You loose UI or smth"
        _platformChosen = -1;
        _currentPlatform.transform.eulerAngles = Vector3.zero;
        SetColors();
        print("Player lost, nay >:(");
    }
}
