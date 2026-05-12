using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
    
    [SerializeField] private PlayerData playerDataData;
    [SerializeField] private ActInfo[] _acts;
    
    public PlayerData PlayerData => playerDataData;
    public ActInfo[] Acts => _acts;
}
