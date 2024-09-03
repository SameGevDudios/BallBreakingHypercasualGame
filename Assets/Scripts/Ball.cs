using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Tooltip("Sequence: Red, Blue, Yellow, Green")]
    [SerializeField] private GameObject[] _frontBall, _backBall;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationCurve _verticalVelocity,
        _horizontalScale, _verticalScale;
    [SerializeField] private float _jumpSpeed = 1, _jumpHeight = 1;
    private int _colorIndex;
    private int _previousColorIndex;
    private bool _frontSide;
    private Vector3 _startPosition;
    private void Start()
    {
        DeactivateFrontBalls();
        DeactivateBackBalls();
        ChangeColor();
        _startPosition = transform.position;
        StartCoroutine(MovingVertically());
        StartCoroutine(ScalingBall());
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeColor();
        }
#endif
    }
    private void ChangeColor()
    {
        while(_colorIndex == _previousColorIndex)
        {
            _colorIndex = Random.Range(0, 4); // 0 - red, 1 - blue, 2 - yellow, 3 - green
        }
        if (_frontSide)
        {
            DeactivateFrontBalls();
            _frontBall[_colorIndex].SetActive(true);
        }
        else
        {
            DeactivateBackBalls();
            _backBall[_colorIndex].SetActive(true);
        }
        _frontSide = !_frontSide;
        _previousColorIndex = _colorIndex;
        Invoke("ChangeColor", _verticalVelocity.keys[_verticalVelocity.keys.Length - 1].time);
        _animator.SetTrigger("ChangeSide");
    }
    public void ChangeColor(int colorIndex)
    {
        if (_frontSide)
        {
            DeactivateFrontBalls();
            _frontBall[colorIndex].SetActive(true);
        }
        else
        {
            DeactivateBackBalls();
            _backBall[colorIndex].SetActive(true);
        }
        _frontSide = !_frontSide;
    }
    private void DeactivateFrontBalls()
    {
        foreach (GameObject ball in _frontBall)
            ball.SetActive(false);
    }
    private void DeactivateBackBalls()
    {
        foreach (GameObject ball in _backBall)
            ball.SetActive(false);
    }
    private IEnumerator MovingVertically()
    {
        float currentTime = 0;
        float totalTime = _verticalVelocity.keys[_verticalVelocity.keys.Length - 1].time;
        while (currentTime < totalTime)
        {
            transform.position = _startPosition + Vector3.up * _verticalVelocity.Evaluate(currentTime) * _jumpHeight;
            currentTime += Time.deltaTime * _jumpSpeed;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(MovingVertically());
    }
    private IEnumerator ScalingBall()
    {
        float currentTime = 0;
        float totalTime = _horizontalScale.keys[_horizontalScale.keys.Length - 1].time;
        while(currentTime < totalTime)
        {
            float xScale = _horizontalScale.Evaluate(currentTime);
            float yScale = _verticalScale.Evaluate(currentTime);
            transform.localScale = Vector3.forward + Vector3.right * xScale + Vector3.up * yScale;
            currentTime += Time.deltaTime * _jumpSpeed;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(ScalingBall());
    }
}
