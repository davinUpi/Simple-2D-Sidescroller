using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="PlayerData", menuName ="ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    const float MIN_HEALTH = 0f;

    [Header("Default values")]
    [SerializeField] private float maxHealth = 5f;

    [Header("event Channels")]
    [SerializeField] private FloatEventChannel playerHealthPercentageChannel;
    [SerializeField] private EventChannel playerIsDeadChannel;

    [SerializeField] private IntEventChannel ScoreChanged;

    private float _currentHealth;
    public float CurrentHealth
    {
        private set
        {
            if(value != _currentHealth) 
            { 
                _currentHealth = Mathf.Clamp(value, MIN_HEALTH, maxHealth);
                if (_currentHealth <= MIN_HEALTH) PublishPlayerIsDead();

                PublishPlayerHealthPercentage();

            }
        }
        get => _currentHealth;
    }

    private int _playerScore;
    public int PlayerScore
    {
        private set
        {
            if(value != _playerScore)
            {
                _playerScore = value;
                if(ScoreChanged != null)
                    ScoreChanged.Invoke(value);
            }
        }
        get => _playerScore;
    }

    private void PublishPlayerHealthPercentage()
    {
        if(playerHealthPercentageChannel != null)
            playerHealthPercentageChannel.Invoke(CurrentHealth / maxHealth);
    }

    private void PublishPlayerIsDead()
    {
        if(playerIsDeadChannel != null)
        {
            Empty temp;
            playerIsDeadChannel?.Invoke(temp);
        }
       
    }

    public void ResetHealth() => CurrentHealth = maxHealth;

    private void OnEnable()
    {
        ResetHealth();
        PlayerScore = 0;
    }

    public void AddHealth(float delta) => CurrentHealth += delta;

    public void AddScore(int delta) => PlayerScore += delta;
}
