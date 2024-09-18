using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health, _healPoints;
    private int _maxHealth, _healTimer;
    private void Start()
    {
        _maxHealth = _health;
    }
    public void Heal()
    {
        _healTimer++;
        if(_healTimer >= _healPoints)
        {
            _health = Mathf.Max(_maxHealth, _health++);
            _healTimer = 0;
        }
    }
    public void TakeDamage()
    {
        _health--;
        if(_health <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        Time.timeScale = 0;
        print("##### Game Over #####");
    }
}
