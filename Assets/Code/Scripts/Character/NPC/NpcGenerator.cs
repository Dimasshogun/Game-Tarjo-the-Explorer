using System;
using UnityEngine;

namespace Code.Scripts.Character.NPC
{
    public class NpcGenerator : MonoBehaviour
    {
        [SerializeField] private Transform spawnpointContainer;
        [SerializeField] private Transform[] spawnpoints;
        [SerializeField] private GameObject[] npcObjects;

        [SerializeField] private int generateAmount = -1;

        private void Start()
        {
            spawnpoints = spawnpointContainer.GetComponentsInChildren<Transform>();
            generateAmount = generateAmount == -1 ? spawnpoints.Length : generateAmount;

            for (int i = 1; i < generateAmount; i++)
            {
                var randomNpc = UnityEngine.Random.Range(0, npcObjects.Length);
                var randomSpawnPoint = UnityEngine.Random.Range(0, spawnpoints.Length);
                while (spawnpoints[randomSpawnPoint].childCount > 0)
                {
                    randomSpawnPoint = (randomSpawnPoint + 1) % spawnpoints.Length;
                }

                Instantiate(npcObjects[randomNpc], spawnpoints[randomSpawnPoint]);
            }
        }
    }
}