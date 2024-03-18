using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Object = System.Object;

namespace Assets._Scripts.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private TextMeshProUGUI _instructionsText;
        [SerializeField] private Transform _startPanel;
        [SerializeField] private Transform _endPanel;
        [SerializeField] private Transform _mixButtonTransform;

        private TextMeshProUGUI _mixButtonText;

        private const string WELCOME_MESSAGE_S = "Welcome to the experiment! Click anywhere to Continue!";
        private const string CLICK_HIGHLIGHTED_FLASK_S = "Click on the highlighted flask to Grab it!";
        private const string CLICK_HIGHLIGHTED_TESTTUBE_S = "Click on the highlighted test tube to grab it!";
        private const string EXPERIMENT1_END_S = "On Mixing TestTube Liquid in the Flask, the Color Changed";
        private const string EXPERIMENT2_END_S = "On Mixing TestTube Liquid in the Flask, the Fumes Formed";

        private void Awake()
        {
            _mixButtonText = _mixButtonTransform.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            OnGameStateChanged(GameManager.Instance.CurrentGameState);

            _startPanel.gameObject.SetActive(true);
            _endPanel.gameObject.SetActive(false);
            _mixButtonTransform.gameObject.SetActive(false);
            _messageText.text =
                "We Saw that mixing liquid from test tube to Flask A liquid resulted in change of color, and mixing the same liquid from test tube to Flask B liquid resulted in formation of fumes";

            GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState currentState)
        {
            switch (currentState)
            {
                case GameState.Welcome:
                    _mixButtonTransform.gameObject.SetActive(false);
                    _startPanel.gameObject.SetActive(true);
                    _endPanel.gameObject.SetActive(false);
                    _instructionsText.text = WELCOME_MESSAGE_S;
                    break;

                case GameState.Experiment1:
                    _instructionsText.text = CLICK_HIGHLIGHTED_FLASK_S;
                    break;

                case GameState.Experiment1End:
                    _instructionsText.text = "Mixing!!";
                    _mixButtonTransform.gameObject.SetActive(false);
                    TempMix();
                    break;

                case GameState.Experiment2:
                    _instructionsText.text = CLICK_HIGHLIGHTED_FLASK_S;
                    _mixButtonTransform.gameObject.SetActive(false);
                    break;

                case GameState.Experiment2End:
                    _instructionsText.text = "Mixing!!";
                    _mixButtonTransform.gameObject.SetActive(false);
                    TempMix();
                    break;

                case GameState.End:
                    _mixButtonText.text = "Restart";
                    _mixButtonTransform.gameObject.SetActive(true);
                    _startPanel.gameObject.SetActive(false);
                    _endPanel.gameObject.SetActive(true);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(currentState), currentState, null);
            }
        }

        public void OnObjectSelected(string objectTag)
        {
            if (objectTag == "Flask")
            {
                Debug.Log("Flask selected");
                _instructionsText.text = CLICK_HIGHLIGHTED_TESTTUBE_S;
            }
            else if (objectTag == "TestTube")
            {
                Debug.Log("TestTube selected");
                _instructionsText.text = "Click on the Mix button to Mix the contents!";
                _mixButtonText.text = "Mix";
                _mixButtonTransform.gameObject.SetActive(true);
            }
        }

        private async void TempMix()
        {
            await Task.Delay(2000);
            string experimentEndText = GameManager.Instance.CurrentGameState == GameState.Experiment1End ? EXPERIMENT1_END_S : EXPERIMENT2_END_S;
            _instructionsText.text = experimentEndText;
            _mixButtonText.text = "Next";
            _mixButtonTransform.gameObject.SetActive(true);
        }
    }
}