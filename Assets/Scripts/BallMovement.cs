using System.Collections;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Tooltip("Sequence: Red, Blue, Yellow, Green")]
    [SerializeField] private AnimationCurve _verticalVelocity,
        _horizontalScale, _verticalScale;
    [SerializeField] private float _jumpSpeed = 1, _jumpHeight = 1;
    private Vector3 _startPosition;
    private void Start()
    {
        _startPosition = transform.position;
        StartCoroutine(MovingVertically());
        StartCoroutine(ScalingBall());
    }
    public void ChangeSpeed(float newSpeed) => _jumpSpeed = newSpeed;
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
