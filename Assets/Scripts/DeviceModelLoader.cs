using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class DeviceModelLoader : MonoBehaviour
{
    void Start()
    {
        // Parse data received from React Native
        // string jsonData = GetParameterFromURL("data");
        string jsonData = @"
        {
        ""deviceName"": ""Device Model XYZ"",
        ""dimensions"": {
            ""length"": 1,
            ""width"": 1.5,
            ""height"": 1.7340
        },
        ""textures"": {
            ""front"": ""https://i.ibb.co/LpdnT7h/TALL-1.png"",
            ""back"": ""https://i.ibb.co/s3Vjzrw/TALL-7.png"",
            ""left"": ""https://i.ibb.co/s3Vjzrw/TALL-7.png"",
            ""right"": ""https://i.ibb.co/s3Vjzrw/TALL-7.png"",
            ""top"": ""https://i.ibb.co/s3Vjzrw/TALL-7.png"",
            ""bottom"": ""https://i.ibb.co/s3Vjzrw/TALL-7.png""
        }
        }";
        if (!string.IsNullOrEmpty(jsonData))
        {
            DeviceData deviceData = JsonUtility.FromJson<DeviceData>(jsonData);
            SetCubeDimensions(deviceData.dimensions);
            StartCoroutine(ApplyTextures(deviceData.textures));
        }
    }

    string GetParameterFromURL(string param)
    {
        // Extract parameter value from URL
        // Implementation depends on how data is passed from React Native
        return ""; // Placeholder for actual implementation
    }

    void SetCubeDimensions(Dimensions dimensions)
    {
        // Set the cube's scale based on the received dimensions
        transform.localScale = new Vector3(dimensions.width, dimensions.height, dimensions.length);
    }

    IEnumerator ApplyTextures(TextureUrls urls)
    {
        // Assuming the cube has 6 materials, one for each face
        Material[] materials = GetComponent<Renderer>().materials;

        // A dictionary to map face names to material indices
        Dictionary<string, int> faceToMaterialIndex = new Dictionary<string, int>
        {
            { "front", 0 },
            { "back", 1 },
            { "left", 2 },
            { "right", 3 },
            { "top", 4 },
            { "bottom", 5 }
        };

        foreach (var urlField in urls.GetType().GetFields())
        {
            string faceName = urlField.Name;
            string textureUrl = (string)urlField.GetValue(urls);

            UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(textureUrl);
            yield return textureRequest.SendWebRequest();

            if (textureRequest.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
                int materialIndex = faceToMaterialIndex[faceName];
                materials[materialIndex].mainTexture = texture;
            }
            else
            {
                Debug.LogError("Failed to load texture: " + faceName);
            }
        }

        GetComponent<Renderer>().materials = materials;
    }

}

[System.Serializable]
public class DeviceData
{
    public string deviceName;
    public Dimensions dimensions;
    public TextureUrls textures;
}

[System.Serializable]
public class Dimensions
{
    public float length;
    public float width;
    public float height;
}

[System.Serializable]
public class TextureUrls
{
    public string front;
    public string back;
    public string left;
    public string right;
    public string top;
    public string bottom;
}