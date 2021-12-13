using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    [System.Serializable]
    public struct LevelSetup
    {
        public string rootScene;
        public string[] additionalScenes;
    }

    [System.Serializable]
    public struct Levels
    {
        public List<LevelSetup> levelSetups;
    }
}