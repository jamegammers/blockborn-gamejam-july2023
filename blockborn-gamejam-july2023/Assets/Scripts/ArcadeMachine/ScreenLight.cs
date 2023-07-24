using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace ArcadeMachine {

    // [ExecuteAlways]
    public class ScreenLight : MonoBehaviour {
        
        [SerializeField] private Light _light;
        [SerializeField] private RenderTexture _renderTexture;
        [SerializeField] private int _calculationStep = 1;
        [SerializeField] private int _calculateEveryFrames = 2;
        
        private Texture2D _screen;
        private Rect _rect;

        private void Awake() {
            _screen = new Texture2D(_renderTexture.width, _renderTexture.height, TextureFormat.RGB24, false);
            _rect = new Rect(0, 0, _renderTexture.width, _renderTexture.height);
        }

        private void Update() {
            if (Time.frameCount % _calculateEveryFrames != 0) return;

            RenderTexture.active = _renderTexture;
            // Debug.Log($"_renderTexture.width: {_renderTexture.width}, _renderTexture.height: {_renderTexture.height}, screen: {_screen}");
            _screen.ReadPixels(_rect, 0, 0);
            _screen.Apply();
            RenderTexture.active = null;

            _light.color = CalculateAverageColor(_screen, _calculationStep);
            // _light.cookie = _screen;
        }
        
        private static Color CalculateAverageColor(Texture2D texture, int step = 1) {
            Color averageColor = Color.black;
            int count = texture.width * texture.height;

            for (int x = 0; x < texture.width; x += step) {
                for (int y = 0; y < texture.height; y += step)
                    averageColor += texture.GetPixel(x, y);
            }

            averageColor /= count;
            return averageColor;
        }
    }

}