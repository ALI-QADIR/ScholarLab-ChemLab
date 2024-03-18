using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class LabManager : MonoBehaviour
    {
        [SerializeField] private Outline _flaskA;
        [SerializeField] private Outline _flaskB;
        [SerializeField] private Outline _testTube;

        [SerializeField] private Material _flaskAMaterial;
        [SerializeField] private Material _flaskBMaterial;
        [SerializeField] private Material _testTubeMaterial;

        private Collider _flaskACollider;
        private Collider _flaskBCollider;
        private Collider _testTubeCollider;

        [SerializeField] private LayerMask _selectableLayerMask;

        private void Start()
        {
            _flaskA.enabled = false;
            _flaskB.enabled = false;
            _testTube.enabled = false;

            _flaskACollider = _flaskA.GetComponent<Collider>();
            _flaskBCollider = _flaskB.GetComponent<Collider>();
            _testTubeCollider = _testTube.GetComponent<Collider>();

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
                    break;

                case GameState.Experiment1:
                    StartExperiment(_flaskACollider, _flaskA);
                    break;

                case GameState.Experiment1End:
                    _flaskA.enabled = false;
                    break;

                case GameState.Experiment2:
                    StartExperiment(_flaskBCollider, _flaskB);
                    break;

                case GameState.Experiment2End:
                    _flaskB.enabled = false;
                    break;

                case GameState.End:
                    break;
            }
        }

        private void StartExperiment(Collider flaskCollider, Outline flask)
        {
            flask.enabled = true;
            flaskCollider.enabled = true;
        }

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

        private void Update()
        {
        }
    }
}