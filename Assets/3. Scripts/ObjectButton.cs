using System;
using UnityEngine;

namespace _3._Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class ObjectButton : MonoBehaviour
    {
        public event Action OnClick;
        private void OnMouseDown()
        {
            OnClick?.Invoke();
        }
    }
}