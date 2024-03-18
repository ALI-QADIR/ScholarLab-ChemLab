using System.Threading.Tasks;
using UnityEngine;

public class LiquidColorController : MonoBehaviour
{
    [SerializeField] private Material _liquidMaterial;
    [SerializeField] private Color _color;

    private Color _initialColor;
    private Color _currentColor;
    private Color _targetColor;
    private float _elapsedTime;
    private const float _DURATION_F = 0.5f;
    private const string _MATERIAL_PROPERTY_NAME_S = "_Tint";

    private void Awake()
    {
        _initialColor = _liquidMaterial.GetColor(_MATERIAL_PROPERTY_NAME_S);
        _currentColor = _initialColor;
        _targetColor = _initialColor;
    }

    private void Update()
    {
        // gradually change the liquid color
        _elapsedTime += Time.deltaTime;
        float delta = _elapsedTime / _DURATION_F;
        _liquidMaterial.SetColor(_MATERIAL_PROPERTY_NAME_S, Color.Lerp(_currentColor, _targetColor, delta));
    }

    /// <summary>
    /// Changes the liquid color to the given color.
    /// </summary>
    public async void ChangeLiquidColor()
    {
        await Task.Delay(600);
        _elapsedTime = 0;
        _currentColor = _liquidMaterial.GetColor(_MATERIAL_PROPERTY_NAME_S);
        _targetColor = _color;
    }

    /// <summary>
    /// Resets the liquid Color to its initial value.
    /// </summary>
    public void ResetLiquidColor()
    {
        _currentColor = _initialColor;
        _targetColor = _initialColor;
        _liquidMaterial.SetColor(_MATERIAL_PROPERTY_NAME_S, _initialColor);
    }

    private void OnDisable()
    {
        _liquidMaterial.SetColor(_MATERIAL_PROPERTY_NAME_S, _initialColor);
    }
}