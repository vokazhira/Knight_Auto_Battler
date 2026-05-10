using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private Entity _playerEntity;
    
    private void Awake()
    {
        EventManager.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float valueAsPercentage)
    {
        _healthBarFilling.fillAmount = valueAsPercentage;
    }
    
    private void OnDestroy()
    {
        EventManager.HealthChanged -= OnHealthChanged;
    }
}
