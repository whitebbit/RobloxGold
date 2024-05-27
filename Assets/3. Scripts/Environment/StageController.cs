using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Singleton;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.Environment
{
    public class StageController : Singleton<StageController>
    {
        [SerializeField] private float viewDistance = 100;
        
        private List<Stage> _stages = new();

        
        protected override void OnAwake()
        {
            base.OnAwake();
            _stages = GetComponentsInChildren<Stage>().ToList();
        }

        private void Start()
        {
            foreach (var stage in _stages)
            {
                stage.Initialize();
            }
        }


        private void Update()
        {
            SetStagesVisibility();
        }

        private void SetStagesVisibility()
        {
            var stages = _stages.Where(s => s.Unlocked);
            foreach (var stage in stages)
            {
                var distance = Vector3.Distance(Player.Player.Instance.transform.position, stage.transform.position);
                
                stage.SetVisibility(distance <= viewDistance);
            }
        }
        
        public void UnlockStage(int id)
        {
            _stages.FirstOrDefault(s=>s.ID == id)?.SetVisibility(true);
        }
        
        public Vector3 GetSpawnPoint(int id)
        {
            var first = _stages.FirstOrDefault(s => s.ID == id);

            return first == null ? Vector3.zero : first.SpawnPoint.position;
        }
    }
}