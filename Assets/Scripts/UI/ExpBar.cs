using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image _expFillImage;
    [SerializeField] private TextMeshProUGUI _levelText;

    private void OnEnable()
    {
        EventManager.Subscribe(GameEvents.PlayerStatsChanged, OnStatsChanged);
        EventManager.Subscribe(GameEvents.PlayerLevelUp, OnLevelUp);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(GameEvents.PlayerStatsChanged, OnStatsChanged);
        EventManager.Unsubscribe(GameEvents.PlayerLevelUp, OnLevelUp);
    }

    private void Start()
    {
        UpdateExpUI();
    }

    private void OnStatsChanged(object obj)
    {
        if (obj is PlayerStatsData data)
        {
            UpdateExpUI();
        }
    }

    private void OnLevelUp(object obj)
    {
        UpdateExpUI();
    }

    private void UpdateExpUI()
    {
        PlayerEntity player = BattleController.Instance.Player;
        if (player == null) return;

        float progress = (float)player.CurrentExp / player.MaxExp;

        if (_expFillImage != null) _expFillImage.fillAmount = progress;

        if (_levelText != null) _levelText.text = $"Уровень {player.CurrentLevel}";
    }
}