using UnityEngine;

public class LiquidLevelController : MonoBehaviour
{
    [SerializeField] private Material _liquidMaterial;

    private float _initialFillAmount;
    private float _currentFillAmount;
    private float _targetAmount;
    private const string _MATERIAL_PROPERTY_NAME_S = "_FillAmount";

    private float _elapsedTime;
    private const float _DURATION_F = 1f;

    private void Awake()
    {
        _initialFillAmount = _liquidMaterial.GetFloat(_MATERIAL_PROPERTY_NAME_S);
        _currentFillAmount = _initialFillAmount;
        _targetAmount = _initialFillAmount;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        float delta = _elapsedTime / _DURATION_F;
        _liquidMaterial.SetFloat(_MATERIAL_PROPERTY_NAME_S, Mathf.Lerp(_currentFillAmount, _targetAmount, delta));
    }

    /// <summary>
    /// Changes the liquid level based on the given boolean value.
    /// </summary>
    /// <param name="b">Pass true to decrease the value, false to increase</param>
    public void ChangeLiquidLevel(bool b)
    {
        _elapsedTime = 0;
        _currentFillAmount = _liquidMaterial.GetFloat(_MATERIAL_PROPERTY_NAME_S);
        if (b)
        {
            _targetAmount += 0.15f;
        }
        else
        {
            _targetAmount -= 0.1f;
        }
    }

    /// <summary>
    /// Resets the liquid level to its initial value.
    /// </summary>
    public void ResetLiquidLevel()
    {
        _currentFillAmount = _initialFillAmount;
        _targetAmount = _initialFillAmount;
    }

    private void OnDisable()
    {
        _liquidMaterial.SetFloat(_MATERIAL_PROPERTY_NAME_S, _initialFillAmount);
    }
}