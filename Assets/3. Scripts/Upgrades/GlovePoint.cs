using _3._Scripts.Upgrades.Enum;
using DG.Tweening;
using UnityEngine;
using VInspector;

namespace _3._Scripts.Upgrades
{
    public class GlovePoint : MonoBehaviour
    {
        [SerializeField] private Transform prefab;
        [Tab("Transform")]
        [SerializeField] private Vector3 position;
        [SerializeField] private Vector3 rotation;
        [SerializeField] private Vector3 scale;
        
        

        public void Initialize(Color color)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            var t = Instantiate(prefab, transform);
            var renderer = t.GetComponent<MeshRenderer>();
            
            renderer.materials[0].DOColor(color, 0.1f);
            t.localPosition = position;
            t.localEulerAngles = rotation;
            t.localScale = scale;
        }
    }
}