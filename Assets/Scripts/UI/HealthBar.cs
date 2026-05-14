using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private Entity _entity;
    
    private void OnEnable()
    {
        _entity.HealthChanged += OnHealthChanged;
    }
    
    private void OnDisable()
    {
        _entity.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float valueAsPercentage)
    {
        _healthBarFilling.fillAmount = valueAsPercentage;
    }
}
