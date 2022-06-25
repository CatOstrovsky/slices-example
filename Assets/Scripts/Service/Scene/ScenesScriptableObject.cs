using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Service.Scene
{
    [CreateAssetMenu(fileName = "Scenes config", menuName = "ScriptableObjects/Create Scenes config", order = 1)]
    public class ScenesScriptableObject : ScriptableObject
    {
        [Tooltip("The all list of available scenes in the project")]
        public List<SceneAsset> scenes;
    }
}