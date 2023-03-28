// Main editor window for the Stable Diffusion Unity plugin.

namespace StableDiffusionUnity
{
    using UnityEditor;
    using UnityEngine;
    [CreateAssetMenu(fileName = "StableDiffusionSetting", menuName = "Stable Diffusion Setting", order = 0)]
    public class StableDiffusionSetting : ScriptableSingleton<StableDiffusionSetting>
    {
        public string ServerUrl = "http://127.0.0.1:7860";
        public string TextToImageAPI = "/sdapi/v1/txt2img";


        [Header("Target Directory")]
        public string TargetDirectory = "Assets/Textures/StableDiffusion";
    }
}