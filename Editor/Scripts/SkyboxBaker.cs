using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace RaiTools.Editor
{
    class SkyboxBaker : EditorWindow
    {
        [MenuItem("RaiTools/Bake Skybox")]
        public static void BakeSkybox()
        {
            GameObject go = new GameObject("CubemapCamera");
            Cubemap cubemap = new Cubemap(512, TextureFormat.RGB24, false);

            go.AddComponent<Camera>();
            go.transform.position = new Vector3(0, 0, 0);
            go.transform.rotation = Quaternion.identity;
            go.GetComponent<Camera>().cullingMask = 0;
            go.GetComponent<Camera>().RenderToCubemap(cubemap);

            AssetDatabase.CreateAsset(cubemap, "Assets/BakedSkybox_" + EditorSceneManager.GetActiveScene().name + ".cubemap");

            DestroyImmediate(go);
            AssetDatabase.Refresh();
        }
    }
}
