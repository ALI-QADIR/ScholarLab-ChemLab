using Assets._Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

public class RayCastSelect : MonoBehaviour
{
    [SerializeField] private LayerMask _grabbableLayer;
    private RaycastHit _raycastHit;

    [SerializeField] private UnityEvent<string> _onRaycastHit;

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState currentState)
    {
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, _grabbableLayer) && Input.GetMouseButtonDown(0))
        {
            _raycastHit = hit;
            _onRaycastHit.Invoke(_raycastHit.collider.tag);
        }
    }
}