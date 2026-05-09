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

    public void StartLevel(LevelInfo level)
    {
        _currentLevel = level;
        _currentBattleIndex = 0;
        EventManager.EnemyDied += CheckBattleWin;
        StartBattle(level.Battles[0]);
    }

    public void StartBattle(BattleInfo battleInfo)
    {
        ClearBattle();
        SpawnEntities(battleInfo);
    }

    public EnemyEntity GetRandomAliveEnemy()
    {
        List<EnemyEntity> activeEnemies = new List<EnemyEntity>();
        
        for (int i = 0; i < _activeEnemies.Count; i++)
        {
            if (_activeEnemies[i] != null && _activeEnemies[i].IsAlive())
            {
                activeEnemies.Add(_activeEnemies[i]);
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
            Player.Init(GameManager.Instance.PlayerData);
            
            //TODO: инициализировать хелфбар player
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
            enemy.SetPlayerAsTarget(Player );
            //TODO: инициализировать хелфбар enemy
            
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

    /*private void OnEnemyDied()
    {
        CheckBattleWin(int expAmount);
    }*/

    private void CheckBattleWin(int expAmount)
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
            //TODO:Класс levelSelectionUI показывает окно итогов боя и возвращает на выбор уровня
        }
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
