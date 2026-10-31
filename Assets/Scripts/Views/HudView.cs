using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudView : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text enemiesText;

    public void SetHealthNormalized(float v) => healthBar.value = Mathf.Clamp01(v);
    public void SetTimer(float t)            => timerText.text = $"Time: {t:F1}";
    public void SetEnemies(int n)            => enemiesText.text = $"Enemies: {n}";
}