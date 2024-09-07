using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColor : MonoBehaviour
{
    [Tooltip("Sequence: Red, Blue, Yellow, Green")]
    [SerializeField] private GameObject[] _leftPlatform, _rightPlatform;
    private int _previousColorLeft, _previousColorRight;
    private int _currentColorLeft, _currentColorRight;

    private void ChangeColor()
    {
        while (_previousColorLeft == _currentColorLeft 
            || _previousColorRight == _currentColorRight 
            || _currentColorLeft == _currentColorRight) 
            RandomizeColor();


    }
    private void RandomizeColor()
    {
        _currentColorLeft = Random.Range(0, 4);
        _currentColorRight = Random.Range(0, 4);
    }
}
