using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private Entity _entity;
    
    private void Start()
    {
        _entity.HealthChanged += OnHealthChanged;
        OnHealthChanged(1f);
    }

    private void OnHealthChanged(float valueAsPercentage)
    {
        _healthBarFilling.fillAmount = valueAsPercentage;
    }
    
    private void OnDestroy()
    {
        _entity.HealthChanged -= OnHealthChanged;
    }
}
