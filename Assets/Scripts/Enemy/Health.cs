using UnityEngine;

public sealed class Health : MonoBehaviour
{
    private readonly Vector3 _defaultScale = new Vector3(0.2f, 1f, 0.2f);

    private float _maxHP;

    public void Init(float maxHP)
    {
        _maxHP = maxHP;
        UpdateHealth(maxHP);
    }

    public void UpdateHealth(float hp)
    {
        transform.localScale = new Vector3(_defaultScale.x, hp / _maxHP, _defaultScale.z);
    }
}
