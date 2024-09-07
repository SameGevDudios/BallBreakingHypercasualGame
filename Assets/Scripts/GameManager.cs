using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BallMovement _ballMovement;
    [SerializeField] private ChangeColor _ball, _currentPlatform;
    [SerializeField] private Animator _ballAnimator, _platformAnimator;
    private float _speed = 1f;
    private float _timer;
    private int _platformChosen;
    private int _bounces;
    private bool _rightToWin;

    private void Start()
    {
        _timer = _speed / 2f;
        SetGameSpeed();
    }
    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1 / _speed)
        {
            CheckColors();
            _timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.A)) ChooseRightPlaftorm(false);
        if (Input.GetKeyDown(KeyCode.D)) ChooseRightPlaftorm(true);
    }
    public void SetColors()
    {
        _rightToWin = Random.Range(0, 2) == 1;
        int winColor = Random.Range(0, 4);
        int looseColor = Random.Range(0, 4);
        while(looseColor == winColor) looseColor = Random.Range(0, 4);
        _ball.Change(winColor);
        if (_rightToWin)
        {
            _currentPlatform.Change(looseColor);
            _currentPlatform.Change(winColor);
        }
        else
        {
            _currentPlatform.Change(winColor);
            _currentPlatform.Change(looseColor);
        }
    }
    public void ChooseRightPlaftorm(bool rightPlatform)
    {
        _platformChosen = rightPlatform ? 1 : 0;
        _currentPlatform.transform.eulerAngles = Vector3.up * (rightPlatform ? 89.99f : -89.99f);
    }
    public void CheckColors()
    {
        switch (_platformChosen) // 0 - left, 1 - right
        {
            case 0:
                if (!_rightToWin) Win();
                else Loose();
                break;
            case 1:
                if (_rightToWin) Win();
                else Loose();
                break;
            default:
                Loose();
                break;
        }
        _platformChosen = -1;
        _currentPlatform.transform.eulerAngles = Vector3.zero;
        SetColors();
        _bounces++;
        print("Bounces: " + _bounces);
        if(_bounces % 10 == 0)
        {
            _speed += 0.1f / (_speed * _speed);
            SetGameSpeed();
        }
    }
    private void SetGameSpeed()
    {
        _ballMovement.ChangeSpeed(_speed);
        _ballAnimator.speed = _speed;
        _platformAnimator.speed = _speed;
    }
    private void Win()
    {
        // Break cuurent platform,
        // Instantiate new platform from pool
        // _currentPlatform == poolPlatform
        // Move new platform
        print("Player won, yay");

    }
    private void Loose()
    {
        // "You loose UI or smth"
        print("Player lost, nay >:(");
    }
}
