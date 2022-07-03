using UnityEngine;
using UnityEngine.UI;
public sealed class BoosterPrefab : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Booster _booster;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _cooldownMask;

    private float _timer;

    public void Init(Booster booster)
    {
        _booster = booster;
        _icon.sprite = _booster.Icon;
        _timer = _booster.Cooldown;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            _cooldownMask.fillAmount = _timer / _booster.Cooldown;
        }
        else
        {
            _cooldownMask.enabled = false;
        }
    }

    public void UseBooster()
    {
        if (_timer <= 0)
        {
            _booster.MakeEffect();
            _timer = _booster.Cooldown;
            _cooldownMask.enabled = true;
        }
    }
}
