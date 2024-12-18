using UnityEditor;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Singletons
{
    /// <summary>
    /// Scriptable object singleton, mostly used for settings where only ONE instance should be present in game.
    /// </summary>
    public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    LoadOrCreateInstance();
                }

                return _instance;
            }
        }

        private static void LoadOrCreateInstance()
        {
            _instance = Resources.Load<T>(typeof(T).Name);
            if (_instance == null)
            {
#if UNITY_EDITOR
                CreateAndSaveInstance();
#else
            Debug.LogErrorFormat("{0} - Not found setting file in Resources folder.", typeof(T));
            _instance = CreateInstance<T>();
#endif
            }
        }

#if UNITY_EDITOR
        private static void CreateAndSaveInstance()
        {
            _instance = CreateInstance<T>();
            string assetPath = AssetDatabase.GenerateUniqueAssetPath($"Assets/Resources/{typeof(T).Name}.asset");
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");

            Debug.LogFormat("Creating {0} asset at {1}", typeof(T).Name, assetPath);
            AssetDatabase.CreateAsset(_instance, assetPath);
            AssetDatabase.SaveAssets();
        }

        protected void OpenAssetsFile()
        {
            Selection.activeObject = this;
        }
#endif
    }
}