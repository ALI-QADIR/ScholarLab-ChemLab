using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class LabManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fumes;

        [Header("Flask Outline Controller")]
        [SerializeField] private Outline _flaskA;

        [SerializeField] private Outline _flaskB;
        [SerializeField] private Outline _testTube;

        private LiquidLevelController _flaskALevelController;
        private LiquidLevelController _flaskBLevelController;
        private LiquidLevelController _testTubeLevelController;

        private LiquidColorController _flaskAColorController;

        private Collider _flaskACollider;
        private Collider _flaskBCollider;
        private Collider _testTubeCollider;

        private void Start()
        {
            _flaskA.enabled = false;
            _flaskB.enabled = false;
            _testTube.enabled = false;

            _flaskACollider = _flaskA.GetComponent<Collider>();
            _flaskBCollider = _flaskB.GetComponent<Collider>();
            _testTubeCollider = _testTube.GetComponent<Collider>();

            _flaskALevelController = _flaskA.GetComponent<LiquidLevelController>();
            _flaskBLevelController = _flaskB.GetComponent<LiquidLevelController>();
            _testTubeLevelController = _testTube.GetComponent<LiquidLevelController>();

            _flaskAColorController = _flaskA.GetComponent<LiquidColorController>();

            _flaskACollider.enabled = false;
            _flaskBCollider.enabled = false;
            _testTubeCollider.enabled = false;

            GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState currentState)
        {
            switch (currentState)
            {
                case GameState.Welcome:
                    ResetFlasks();
                    break;

                case GameState.Experiment1:
                    StartExperiment(_flaskACollider, _flaskA);
                    break;

                case GameState.Experiment1End:
                    Mix(ref _flaskALevelController);
                    _flaskAColorController.ChangeLiquidColor();
                    _flaskA.enabled = false;
                    break;

                case GameState.Experiment2:
                    StartExperiment(_flaskBCollider, _flaskB);
                    break;

                case GameState.Experiment2End:
                    Mix(ref _flaskBLevelController);
                    _fumes.Play();
                    _flaskB.enabled = false;
                    break;

                case GameState.End:
                    _fumes.Stop();
                    break;
            }
        }

        private void ResetFlasks()
        {
            _flaskALevelController.ResetLiquidLevel();
            _flaskBLevelController.ResetLiquidLevel();
            _testTubeLevelController.ResetLiquidLevel();

            _flaskAColorController.ResetLiquidColor();
        }

        private void StartExperiment(Collider flaskCollider, Outline flask)
        {
            flask.enabled = true;
            flaskCollider.enabled = true;
        }

        /// <summary>
        /// Handles the selection of objects in the lab.
        /// </summary>
        /// <param name="objectTag">The tag of the selected object.</param>
        public void OnObjectSelected(string objectTag)
        {
            if (objectTag == "Flask")
            {
                _flaskA.enabled = false;
                _flaskACollider.enabled = false;
                _flaskB.enabled = false;
                _flaskBCollider.enabled = false;
                _testTube.enabled = true;
                _testTubeCollider.enabled = true;
            }
            else if (objectTag == "TestTube")
            {
                _testTube.enabled = false;
                _testTubeCollider.enabled = false;
            }
        }

        private void Mix(ref LiquidLevelController flaskLevelController)
        {
            flaskLevelController.ChangeLiquidLevel(false);
            _testTubeLevelController.ChangeLiquidLevel(true);
        }
    }
}