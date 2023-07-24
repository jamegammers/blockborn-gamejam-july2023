using UnityEngine;

namespace UI {

    public class StartScreen : MonoBehaviour {

        [SerializeField] private RectTransform _titleParent;
        [SerializeField] private RectTransform _seedInputParent;
        [SerializeField] private CharacterInput _seedInput;
        [SerializeField] private HealthUI _healthUI;

        [SerializeField] private Randomize _randomize;
        [SerializeField] private PlayerMovement _playerMovement;


        private void Awake() {
            _playerMovement.enabled = false;
            _healthUI.gameObject.SetActive(false);
        }

        private void Start() {
            CoinInserter.WaitForCoin(ShowSeedInput);
            _seedInputParent.gameObject.SetActive(false);
        }

        private void ShowSeedInput() {
            _seedInput.OnSubmit += OnInputSubmit;
            _seedInput.StartInput(4);

            _titleParent.gameObject.SetActive(false);
            _seedInputParent.gameObject.SetActive(true);
        }

        private void OnInputSubmit(string input) {
            int seed = StringToSeed(input);
            _randomize.SetLevelSeed(seed);

            _playerMovement.enabled = true;
            _healthUI.gameObject.SetActive(true);

            _seedInput.OnSubmit -= OnInputSubmit;
            Destroy(gameObject);
        }

        private static int StringToSeed(string input) {
            int seed = 0;

            for (int i = 0; i < input.Length; i++)
                seed += input[i] * (int) Mathf.Pow(100, input.Length - i - 1);

            return seed;
        }

    }

}