using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Character
{
    [CreateAssetMenu(fileName = "ModelPart", menuName = "Project/Model/ModelPart", order = 0)]
    public class ModelPart : ScriptableObject
    {
        [SerializeField] private GameObject partObject;
        [SerializeField] private bool nullable;
        [SerializeField] private int activePartIndex = -1;
        
        public GameObject PartObject => partObject;
        public bool Nullable => nullable;
        public int ActivePartIndex => activePartIndex;
    }
}