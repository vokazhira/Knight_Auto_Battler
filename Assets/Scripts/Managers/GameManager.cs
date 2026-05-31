using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private ActInfo[] _acts;
    [SerializeField] private LevelSelectionUI _levelSelectionUI;
    
    public PlayerEntity Player { get; private set; }
    public PlayerData PlayerData => _playerData;
    public ActInfo[] Acts => _acts;
    public LevelSelectionUI LevelSelectionUI => _levelSelectionUI;

    private int[] _completedLevelsPerAct;
    
    public int CurrentActIndex { get; private set; }
    public int CurrentLevelIndex { get; private set; }

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
        
        CurrentActIndex = 0;
        CurrentLevelIndex = 0;
        
        _completedLevelsPerAct = new int[_acts.Length];
        _completedLevelsPerAct[0] = 0;
    }

    public void SetCurrentActIndex(int actIndex)
    {
        CurrentActIndex = actIndex;
    }
    
    public void SetCurrentLevelIndex(int levelIndex)
    {
        CurrentLevelIndex = levelIndex;
    }
    
    public void CompleteLevel(int actIndex, int levelIndex)
    {
        if (levelIndex >= _completedLevelsPerAct[actIndex])
        {
            _completedLevelsPerAct[actIndex] = levelIndex + 1;

            if (_completedLevelsPerAct[actIndex] >= _acts[actIndex].Levels.Length)
            {
                if (actIndex + 1 < _acts.Length)
                {
                    _completedLevelsPerAct[actIndex + 1] = 0;
                }
            }
            
            EventManager.Invoke(GameEvents.ProgressUpdated);
        }
    }

    public bool IsLevelUnlocked(int actIndex, int levelIndex)
    {
        if (actIndex == 0 && levelIndex == 0) return true;
        
        return levelIndex <= _completedLevelsPerAct[actIndex];
    }

    public bool IsActUnlocked(int actIndex)
    {
        if (actIndex == 0) return true;
        if (actIndex >= _acts.Length) return false;
        
        return _completedLevelsPerAct[actIndex - 1] >= _acts[actIndex - 1].Levels.Length;
    }
    
    public void SetPlayer(PlayerEntity player) => Player = player;
}
