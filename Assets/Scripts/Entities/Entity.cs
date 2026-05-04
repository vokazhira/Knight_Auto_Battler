using UnityEngine;
using Lean.Pool;

public class Entity : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;
    
    private Entity[] _targets;
    private float _timeDelay;

    private void Start()
    {
        _timeDelay = 0f;
    }
    
    protected virtual void Update()
    {
        _timeDelay += Time.deltaTime;
        
        if (_timeDelay >= _attackDelay)
        {
            ApplyDamage();
            _timeDelay = 0f;
        }
    }

    protected virtual void ApplyDamage()
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            _targets[i].TakeDamage(_damage);
        }
    }
    
    protected virtual void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            LeanPool.Despawn(this);
        }
    }

    public void SetTarget(Entity[] target)
    {
        _targets = target;
    }

    public void SetBattlePosition(Transform battlePosition)
    {
        transform.position = battlePosition.position;
    }

    public void Initialize(Sprite sprite, int health, int damage, int attackDelay)
    {
        
    }
}
