using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Entity _player;
    [SerializeField] private Transform[] _enemyPositions;
    
    
    private int _enemiesCountToSpawn;

    private void Start()
    {
        CreateEnemy();
    }

    public void CreateEnemy()
    {
        HashSet<int> usedIndexes = new HashSet<int>();

        for (int i = 0; i < _enemiesCountToSpawn; i++)
        {
            int index;
            do
            {
                index = Random.Range(0, _enemyPositions.Length);
            }
            while(usedIndexes.Contains(index));
            
            usedIndexes.Add(index);
            Transform spawnPoint = _enemyPositions[index];
            /*GameObject enemy = Instantiate(_enemyPrefab, spawnPoint.position, spawnPoint.rotation);*/
        }
    }
}
