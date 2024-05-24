using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Singleton;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.Environment
{
    public class StageController :Singleton<StageController>
    {
        private List<Stage> _stages = new();

        protected override void OnAwake()
        {
            base.OnAwake();
            _stages = GetComponentsInChildren<Stage>().ToList();
        }

        
        public Vector3 GetSpawnPoint(int id)
        {
            return _stages[id].SpawnPoint.position;
        }
    }
}