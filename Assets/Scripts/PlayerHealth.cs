using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private RecordCounter _recordCounter;
    [SerializeField] private Animator _healthAnimator, _gameOverAnimator;
    [SerializeField] private int _health, _healPoints;
    private int _maxHealth, _healTimer;
    private bool _isDead;
    private void Start()
    {
        _maxHealth = _health;
    }
    public void Heal()
    {
        if(_health < _maxHealth)
        {
            _healTimer++;
            if (_healTimer >= _healPoints)
            {
                _health++;
                _healTimer = 0;
                _gameUI.UpdateHealthUI(_health, _maxHealth);
                _healthAnimator.SetTrigger("Heal");
            }
        }
    }
    public void TakeDamage()
    {
        _health--;
        _healTimer = 0;
        _gameUI.UpdateHealthUI(_health, _maxHealth);
        _healthAnimator.SetTrigger("Shake");
        AudioManager.Instance.PlayLooseAudio();
        if (_health <= 0 && !_isDead) Death();
    }
    private void Death()
    {
        _recordCounter.SaveRecord();
        _gameManager.EndGame();
        _gameOverAnimator.SetTrigger("Action");
        AudioManager.Instance.PlayDeathAudio();
        _isDead = true;
    }
    public void Revive()
    {
        _health = _maxHealth;
        _gameUI.SwitchGameUI(false);
        _gameUI.UpdateScoreText(0);
        _gameUI.UpdateHealthUI(_health, _maxHealth);
        _gameOverAnimator.SetTrigger("GoIdle");
        _isDead = false;
    }
}
