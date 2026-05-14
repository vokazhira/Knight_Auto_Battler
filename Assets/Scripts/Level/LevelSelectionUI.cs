using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour
{
    [SerializeField] private ActButton[] _actsButtons;
    [SerializeField] private LevelButton[] _levelButtons;
    
    [SerializeField] private GameObject _selectionPanel;

    private GameManager _gameManager;
    private int _currentSelectedAct = 0;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        InitActButtons();
        RefreshLevelButtons();
        
        EventManager.Subscribe(GameEvents.ProgressUpdated, OnProgressUpdated);
        EventManager.Subscribe(GameEvents.LevelCompleted, OnLevelCompletedEvent);
        
        ShowPanel(true);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(GameEvents.ProgressUpdated, OnProgressUpdated);
        EventManager.Unsubscribe(GameEvents.LevelCompleted, OnLevelCompletedEvent);
    }

    private void InitActButtons()
    {
        for (int i = 0; i < _actsButtons.Length; i++)
        {
            int actIndex = i;
            bool isUnlocked = _gameManager.IsActUnlocked(actIndex);
            
            _actsButtons[i].Init(_gameManager.Acts[actIndex], actIndex, isUnlocked, OnActButtonClicked);
        }
    }

    private void OnActButtonClicked(int actIndex)
    {
        if (!_gameManager.IsActUnlocked(actIndex)) return;
        
        _currentSelectedAct = actIndex;
        RefreshLevelButtons();
    }

    private void RefreshLevelButtons()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            int levelIndex = i;
            bool isUnlocked = _gameManager.IsLevelUnlocked(_currentSelectedAct, levelIndex);
            
            _levelButtons[i].Init(_gameManager.Acts[_currentSelectedAct].Levels[levelIndex],
                levelIndex, isUnlocked, OnLevelButtonClicked);
        }
    }

    private void OnLevelButtonClicked(int levelIndex)
    {
        _gameManager.SetCurrentActIndex(_currentSelectedAct);
        _gameManager.SetCurrentLevelIndex(levelIndex);
        
        ShowPanel(false);
        LevelInfo levelToStart = _gameManager.Acts[_currentSelectedAct].Levels[levelIndex];
        EventManager.Invoke(GameEvents.StartLevel, levelToStart);
    }

    private void OnProgressUpdated(object obj)
    {
        InitActButtons();
        RefreshLevelButtons();
    }
    
    private void OnLevelCompletedEvent(object obj)
    {
        OnLevelCompleted();
        ShowPanel(true);
    }

    public void OnLevelCompleted()
    {
        _gameManager.CompleteLevel(_gameManager.CurrentActIndex, _gameManager.CurrentLevelIndex);
    }

    private void ShowPanel(bool isActive)
    {
        _selectionPanel.SetActive(isActive);
    }
}
