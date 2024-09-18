using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [Tooltip("Sequence: Red, Blue, Yellow, Green")]
    [SerializeField] private GameObject[] _objectPrimary, _objectSecondary;
    private int _previousColorPrimary, _previousColorSecondary,
        _currentColorPrimary, _currentColorSecondary,
        _lastColor;
    [SerializeField] bool _primarySide = true;
    public void Change()
    {
        while (_previousColorPrimary == _currentColorPrimary
            || _previousColorSecondary == _currentColorSecondary
            || _currentColorPrimary == _currentColorSecondary)
            RandomizeColor();
        if (_primarySide)
        {
            DeactivatePrimaryObjects();
            _objectPrimary[_currentColorPrimary].SetActive(true);
            _previousColorPrimary = _currentColorPrimary;
            _lastColor = _currentColorPrimary;
        }
        else
        {
            DeactivateSecondaryObjects();
            _objectSecondary[_currentColorSecondary].SetActive(true);
            _previousColorSecondary = _currentColorSecondary;
            _lastColor = _currentColorSecondary;
        }
        _primarySide = !_primarySide;
    }
    public void Change(int newColor)
    {
        if (_primarySide)
        {
            DeactivatePrimaryObjects();
            _objectPrimary[newColor].SetActive(true);
        }
        else
        {
            DeactivateSecondaryObjects();
            _objectSecondary[newColor].SetActive(true);
        }
        _primarySide = !_primarySide;
    }
    private void RandomizeColor()
    {
        _currentColorPrimary = Random.Range(0, 4);
        _currentColorSecondary = Random.Range(0, 4);
    }
    private void DeactivatePrimaryObjects()
    {
        foreach (GameObject item in _objectPrimary)
            item.SetActive(false);
    }
    private void DeactivateSecondaryObjects()
    {
        foreach (GameObject item in _objectSecondary)
            item.SetActive(false);
    }
    public int GetPrimaryColor => _currentColorPrimary;
    public int GetSecondaryColor => _currentColorSecondary;
    public int GetLastColor => _lastColor;
}
