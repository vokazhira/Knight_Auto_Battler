using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    [SerializeField] private PlayerEntity _playerPrefab;
    [SerializeField] private EnemyEntity _enemyPrefab;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private Transform[] _enemyPositions;

    public PlayerEntity Player { get; private set; }
    
    private List<EnemyEntity> _activeEnemies = new List<EnemyEntity>();
    
    private LevelInfo _currentLevel;
    private int _currentBattleIndex;
    
    private GameManager _gameManager;

    private void OnEnable()
    {
        EventManager.Subscribe(GameEvents.EnemyDeath, CheckBattleWin);
        EventManager.Subscribe(GameEvents.StartLevel, OnStartLevel);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(GameEvents.EnemyDeath, CheckBattleWin);
        EventManager.Unsubscribe(GameEvents.StartLevel, OnStartLevel);
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }
    
    private void OnStartLevel(object levelObj)
    {
        if (levelObj is LevelInfo levelInfo)
        {
            StartLevel(levelInfo);
        }
    }
    
    public void StartLevel(LevelInfo level)
    {
        _currentLevel = level;
        _currentBattleIndex = 0;
        
        StartBattle(level.Battles[0]);
    }

    public void StartBattle(BattleInfo battleInfo)
    {
        ClearBattle();
        SpawnEntities(battleInfo);
    }

    public static EnemyEntity GetRandomAliveEnemy()
    {
        List<EnemyEntity> activeEnemies = new List<EnemyEntity>();
        
        for (int i = 0; i < Instance._activeEnemies.Count; i++)
        {
            if (Instance._activeEnemies[i] != null && Instance._activeEnemies[i].IsAlive())
            {
                activeEnemies.Add(Instance._activeEnemies[i]);
            }
        }

        if (activeEnemies.Count == 0)
        {
            return null;
        }
        return activeEnemies[Random.Range(0, activeEnemies.Count)];
    }
    
    private void SpawnEntities(BattleInfo battleInfo)
    {
        if (Player == null)
        {
            Player = LeanPool.Spawn(_playerPrefab, _playerPosition.position, Quaternion.identity);
            Player.Init(_gameManager.PlayerData);
        }
        
        HashSet<int> usedSpawnPoints = new HashSet<int>();

        for (int i = 0; i < battleInfo.Enemies.Length; i++)
        {
            int posIndex;
            do
            {
                posIndex = Random.Range(0, _enemyPositions.Length);
            }
            while (usedSpawnPoints.Contains(posIndex));
            
            usedSpawnPoints.Add(posIndex);
            
            EnemyEntity enemy = LeanPool.Spawn(_enemyPrefab, _enemyPositions[posIndex].position, Quaternion.identity);
            enemy.Init(battleInfo.Enemies[i]);
            enemy.SetPlayerAsTarget(Player);
            
            _activeEnemies.Add(enemy);
        }
    }

    private void ClearBattle()
    {
        foreach (EnemyEntity enemy in _activeEnemies)
        {
            LeanPool.Despawn(enemy);
        }
        _activeEnemies.Clear();
    }

    private void CheckBattleWin(object obj)
    {
        if (AreAllEnemiesDead())
        {
            OnBattleWin();
        }
    }
    
    private void OnBattleWin()
    {
        _currentBattleIndex++;

        if (_currentLevel != null && _currentBattleIndex < _currentLevel.Battles.Length)
        {
            StartCoroutine("NextBattleAfterDelay");
        }
        else
        {
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        EventManager.Invoke(GameEvents.LevelCompleted);
    }

    private bool AreAllEnemiesDead()
    {
        _activeEnemies.RemoveAll(enemy => enemy == null || !enemy.IsAlive());
        return _activeEnemies.Count == 0;
    }
    
    private IEnumerator NextBattleAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        StartBattle(_currentLevel.Battles[_currentBattleIndex]);
    }
}
