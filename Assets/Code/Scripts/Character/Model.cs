using UnityEngine;

namespace Code.Scripts.Character
{
    public class Model : MonoBehaviour
    {
        [SerializeField] private ModelPart[] parts;

        private void Start()
        {
            foreach (var part in parts)
            {
                var partObject = Instantiate(part.PartObject, transform);
                PickPart(partObject, part);
            }
        }

        private void PickPart(GameObject partObject, ModelPart part)
        {
            var childCount = partObject.transform.childCount;
            var maxIndex = part.Nullable ? childCount : childCount - 1;
            var selectedIndex = part.ActivePartIndex == -1 ? Random.Range(0, maxIndex) : part.ActivePartIndex;
            
            for (var i = 0; i < childCount; i++)
            {
                if (i == selectedIndex)
                {
                    continue;
                }

                partObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}