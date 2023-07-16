using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace ArcadeMachine {

    // [ExecuteAlways]
    public class ScreenLight : MonoBehaviour {
        
        [SerializeField] private Light _light;
        [SerializeField] private RenderTexture _renderTexture;
        
        private Texture2D _screen;

        private void Awake() {
            _screen = new Texture2D(_renderTexture.width, _renderTexture.height, TextureFormat.RGB24, false);
        }

        private void Update() {
            RenderTexture.active = _renderTexture;
            // Debug.Log($"_renderTexture.width: {_renderTexture.width}, _renderTexture.height: {_renderTexture.height}, screen: {_screen}");
            _screen.ReadPixels(new Rect(0, 0, _renderTexture.width, _renderTexture.height), 0, 0);
            _screen.Apply();
            RenderTexture.active = null;
         
            _light.color = CalculateAverageColor(_screen);
            
            // _light.cookie = _screen;
        }
        
        private Color CalculateAverageColor(Texture2D texture, int step = 1) {
            Color[] pixels = texture.GetPixels();
            Color averageColor = Color.black;
            int count = 0;
            for (int i = 0; i < pixels.Length; i += step) {
                averageColor += pixels[i];
                count++;
            }
            averageColor /= count;
            return averageColor;
        }
    }

}