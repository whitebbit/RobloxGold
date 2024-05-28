using System;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts
{
    public class LoadRotator : MonoBehaviour
    {
        private void Start()
        {
            transform.DORotate(new Vector3(0,0 , 360), 1, RotateMode.FastBeyond360).SetRelative(true);
        }
    }
}