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
        [SerializeField] private List<Stage> stages = new();
        [SerializeField] private float viewDistance = 100;
        
        private void Start()
        {
            foreach (var stage in stages)
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
            var s = stages.Where(stage => stage.Unlocked);
            foreach (var stage in s)
            {
                var distance = Vector3.Distance(Player.Player.Instance.transform.position, stage.transform.position);

                stage.SetVisibility(distance <= viewDistance);
            }
        }

        public float GetStageMultiplier(int id)
        {
            var first = stages.FirstOrDefault(s => s.ID == id);

            return first == null ? 1 : first.Multiplier;
        }

        public void UnlockStage(int id)
        {
            stages.FirstOrDefault(s => s.ID == id)?.SetVisibility(true);
        }

        public Vector3 GetSpawnPoint(int id)
        {
            var first = stages.FirstOrDefault(s => s.ID == id);

            return first == null ? Vector3.zero : first.SpawnPoint.position;
        }
    }
}