using System;
using System.Threading.Tasks;
using Assets._Scripts.Managers;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Welcome:
                break;

            case GameState.Experiment1:
                break;

            case GameState.Experiment1End:
                PlayExcitedAnimation();
                break;

            case GameState.Experiment2:
                break;

            case GameState.Experiment2End:
                PlayIrritatedAnimation();
                break;

            case GameState.End:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
    }

    private async void PlayExcitedAnimation()
    {
        await Task.Delay(800);
        _animator.SetTrigger("Excited");
    }

    private async void PlayIrritatedAnimation()
    {
        await Task.Delay(800);
        _animator.SetTrigger("Irritated");
    }
}