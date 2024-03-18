using System;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameState CurrentGameState { get; private set; }
        private GameState _nextGameState;

        public event Action<GameState> OnGameStateChanged;

        private RayCastSelect _rayCastSelect;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _rayCastSelect = Camera.main.GetComponent<RayCastSelect>();
            UpdateGameState(GameState.Welcome);
        }

        private void Update()
        {
            if (CurrentGameState == GameState.Welcome && Input.GetMouseButtonDown(0))
                UpdateGameState(GameState.Experiment1);
        }

        /// <summary>
        /// Updates the game state to the specified next state.
        /// </summary>
        /// <param name="nextState">The next state to update to.</param>
        public void UpdateGameState(GameState nextState)
        {
            CurrentGameState = nextState;
            switch (CurrentGameState)
            {
                case GameState.Welcome:
                    OnGameStateChanged?.Invoke(CurrentGameState);
                    _nextGameState = GameState.Experiment1;
                    _rayCastSelect.enabled = false;
                    break;

                case GameState.Experiment1:
                    _rayCastSelect.enabled = true;
                    OnGameStateChanged?.Invoke(CurrentGameState);
                    _nextGameState = GameState.Experiment1End;
                    break;

                case GameState.Experiment1End:
                    _rayCastSelect.enabled = false;
                    OnGameStateChanged?.Invoke(CurrentGameState);
                    _nextGameState = GameState.Experiment2;
                    break;

                case GameState.Experiment2:
                    _rayCastSelect.enabled = true;
                    OnGameStateChanged?.Invoke(CurrentGameState);
                    _nextGameState = GameState.Experiment2End;
                    break;

                case GameState.Experiment2End:
                    _rayCastSelect.enabled = false;
                    OnGameStateChanged?.Invoke(CurrentGameState);
                    _nextGameState = GameState.End;
                    break;

                case GameState.End:
                    OnGameStateChanged?.Invoke(CurrentGameState);
                    _nextGameState = GameState.Welcome;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnClickButton()
        {
            UpdateGameState(_nextGameState);
        }

        public void ApplicationQuit()
        {
            Application.Quit();
            Debug.Log("Quits the Application");
        }
    }

    public enum GameState
    {
        Welcome,
        Experiment1,
        Experiment1End,
        Experiment2,
        Experiment2End,
        End
    }
}