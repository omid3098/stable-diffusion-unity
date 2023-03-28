using System.Collections;
using System.Text;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Plastic.Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace StableDiffusionUnity
{
    public class TextToImageComponent : MonoBehaviour
    {
        [SerializeField] string m_Prompt = "a cute dog";
        [SerializeField] TextureSize m_textureSize = TextureSize._256;
        [SerializeField] bool m_Tiling = false;
        [SerializeField] int m_Seed = -1;
        [SerializeField] int m_Steps = 20;
        Texture2D m_Texture;

        [Button]
        void Generate()
        {
            StartCoroutine(DownloadTexture(StableDiffusionSetting.instance.ServerUrl + StableDiffusionSetting.instance.TextToImageAPI));
        }

        // downaload texture from server using coroutine
        private IEnumerator DownloadTexture(string url)
        {
            var prompt = new TextToImageMessage
            {
                prompt = m_Prompt,
                width = (int)m_textureSize,
                height = (int)m_textureSize,
                tiling = m_Tiling,
                seed = m_Seed,
                steps = m_Steps
            };

            var request = new UnityWebRequest(url, "POST");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            byte[] bodyRaw = Encoding.UTF8.GetBytes(prompt.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerTexture();

            Debug.Log("Downloading: \n" + prompt.ToString());

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                yield break;
            }

            // Deserialize the JSON response.
            JObject jsonResponse = JObject.Parse(request.downloadHandler.text);
            if (jsonResponse["images"] is not JArray imageArray || imageArray.Count == 0)
            {
                Debug.LogError("JSON response 'images' array is empty or invalid.");
                yield break;
            }
            string base64Image = imageArray[0].Value<string>();
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            m_Texture = SaveTexture(imageBytes);

            DetectGameobjectType();

            Debug.Log("Downloaded");
        }

        private void DetectGameobjectType()
        {
            if (GetComponent<UnityEngine.UI.RawImage>() != null)
            {
                AssignTextureToRawImage(m_Texture);
            }
            else if (GetComponent<Renderer>() != null)
            {
                AssignTextureMaterial(m_Texture);
            }
            else
            {
                Debug.LogError("No RawImage or Renderer component found.");
            }
        }

        private void AssignTextureToRawImage(Texture2D texture)
        {
            var rawImage = GetComponent<UnityEngine.UI.RawImage>();
            rawImage.texture = texture;
        }

        private void AssignTextureMaterial(Texture2D texture)
        {
            var material = GetComponent<Renderer>().sharedMaterial;
            material.mainTexture = texture;
        }

        private Texture2D SaveTexture(byte[] imageBytes)
        {
            ValidateTextureDirectory();
            string path = $"{StableDiffusionSetting.instance.TargetDirectory}/{gameObject.name}.png";
            File.WriteAllBytes(path, imageBytes);
            AssetDatabase.Refresh();
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            return texture;
        }

        private void ValidateTextureDirectory()
        {
            if (!AssetDatabase.IsValidFolder(StableDiffusionSetting.instance.TargetDirectory))
            {
                // Create the directory if it doesn't exist.
                Directory.CreateDirectory(StableDiffusionSetting.instance.TargetDirectory);
            }
        }
    }
}
