using System;
using System.Linq;
using _3._Scripts.Characters;
using _3._Scripts.Config;
using _3._Scripts.Environment;
using _3._Scripts.Pets;
using _3._Scripts.Singleton;
using _3._Scripts.Trails;
using _3._Scripts.Upgrades;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.Player
{
    public class Player : Singleton<Player>
    {
        [SerializeField] private TrailRenderer trail;

        public PetsHandler PetsHandler { get; private set; }
        public TrailHandler TrailHandler { get; private set; }
        public CharacterHandler CharacterHandler { get; private set; }
        public UpgradeHandler UpgradeHandler { get; private set; }
        public PlayerAnimator Animator { get; private set; }

        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            Animator = GetComponent<PlayerAnimator>();
            PetsHandler = new PetsHandler();
            CharacterHandler = new CharacterHandler();
            UpgradeHandler = new UpgradeHandler();
            TrailHandler = new TrailHandler(GetComponent<PlayerMovement>(), trail);
        }

        private void Start()
        {
            InitializeCharacter();
            InitializeTrail();
            InitializePets();
            SetUpgrade();
            Teleport(StageController.Instance.GetSpawnPoint(GBGames.saves.stageID + 1));
        }


        public void Teleport(Vector3 position)
        {
            _characterController.enabled = false;
            transform.position = position;
            _characterController.enabled = true;
        }

        private void InitializeCharacter()
        {
            var id = GBGames.saves.characterSaves.current;
            CharacterHandler.SetCharacter(id, transform);
        }    
        public void SetUpgrade()
        {
            var id = GBGames.saves.upgradeSaves.current;
            var list = GetComponentsInChildren<GlovePoint>();
            UpgradeHandler.SetUpgrade(id, list);
        }

        private void InitializeTrail()
        {
            var id = GBGames.saves.trailSaves.current;
            TrailHandler.SetTrail(id);
        }

        private void InitializePets()
        {
            var player = transform;
            var position = player.position + player.right * 2;
            foreach (var data in GBGames.saves.petSaves.current.Select(id =>
                Configuration.Instance.AllPets.FirstOrDefault(p => p.ID == id)))
            {
                PetsHandler.CreatePet(data, position);
            }
        }
    }
}