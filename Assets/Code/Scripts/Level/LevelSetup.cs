using System.Collections.Generic;

namespace Code.Scripts.Level
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