using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Transform _platformSpawnPoint;
    [SerializeField] private BallMovement _ballMovement;
    [SerializeField] private ChangeColor _ball;
    private ChangeColor _currentPlatform;
    [SerializeField] private Animator _ballAnimator;
    private Animator _platformAnimator, _flyAwayAnimator;
    [SerializeField] private float _speed = 1f, _moveDistance = 15f;
    private float _timer;
    private int _platformChosen;
    private int _bounces;
    private bool _rightToWin;

    private void Start()
    {
        _timer = _speed / 2f;
        _currentPlatform = PoolManager.Instance.InstantiateFromPool("Platform", _platformSpawnPoint.position, Quaternion.identity).GetComponent<ChangeColor>();
        GetNewPlatform();
    }
    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1 / _speed)
        {
            CheckColors();
            Invoke("GetNewPlatform", .1f / _speed);
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
    private void GetNewPlatform()
    {
        _currentPlatform.gameObject.SetActive(false);
        _currentPlatform = PoolManager.Instance.InstantiateFromPool("Platform", _platformSpawnPoint.position, Quaternion.identity).GetComponent<ChangeColor>();
        _platformAnimator = _currentPlatform.GetComponent<Platform>().GetPlatformAnimator();
        _flyAwayAnimator = _currentPlatform.GetComponent<Platform>().GetFlyAwayAnimator();
        SetGameSpeed();
        _currentPlatform.gameObject.SetActive(true);
        StartCoroutine(MoveCurrentPlatform());
        SetColors();
        _ballAnimator.SetTrigger("ChangeSide");
    }
    public void ChooseRightPlaftorm(bool rightPlatform)
    {
        _platformChosen = rightPlatform ? 1 : 0;
        _currentPlatform.transform.eulerAngles = Vector3.up * (rightPlatform ? 89.99f : -89.99f);
        _platformAnimator.SetTrigger(rightPlatform ? "SpinRight" : "SpinLeft");
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
        _flyAwayAnimator.SetTrigger("FlyAway");
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
        _playerHealth.Heal();

    }
    private void Loose()
    {
        // "You loose UI or smth"
        print("Player lost, nay >:(");
        _playerHealth.TakeDamage();
    }
    private IEnumerator MoveCurrentPlatform()
    {
        float moveDelta = 20f;
        Transform platform = _currentPlatform.gameObject.transform;
        // first platform should be half way up, because game starts from "half tact"
        platform.position = new Vector3(platform.position.x, (_bounces == 0 ? -_moveDistance / 2 : -_moveDistance), platform.position.z);
        while (platform.position.y < 0)
        {
            platform.position += Vector3.up * _speed * moveDelta * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        platform.position = new Vector3(platform.position.x, 0, platform.position.z);
    }
}
