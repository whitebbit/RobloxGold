﻿using System;
using System.Linq;
using _3._Scripts.Boosters;
using _3._Scripts.Config;
using _3._Scripts.Detectors;
using _3._Scripts.Inputs;
using _3._Scripts.Inputs.Interfaces;
using _3._Scripts.Interactive.Interfaces;
using _3._Scripts.Sounds;
using _3._Scripts.UI;
using _3._Scripts.UI.Scriptable.Shop;
using GBGamesPlugin;
using UnityEngine;
using VInspector;

namespace _3._Scripts.Player
{
    public class PlayerAction : MonoBehaviour
    {
        [SerializeField] private float baseCooldownTime;
        [SerializeField] private AnimationClip actionAnimation;
        [Tab("Detectors")] [SerializeField] private BaseDetector<IInteractive> detector;


        private IInput _input;
        private bool _isOnCooldown;
        private float _cooldownTimer;
        private PlayerAnimator _animator;

        private IInteractive _interactive;

        private void Awake()
        {
            _animator = GetComponent<PlayerAnimator>();
        }

        private void Start()
        {
            _input = InputHandler.Instance.Input;
            detector.OnFound += TryGetInteractive;
            _animator.Event += AnimatorAction;
        }

        private void Update()
        {
            if ((_input.GetAction() || BoostersHandler.Instance.UseAutoClicker) &&
                !UIManager.Instance.Active) DoAction();
            Cooldown();
        }

        private void DoAction()
        {
            if (_isOnCooldown) return;
            _isOnCooldown = true;
            _cooldownTimer = GetCooldown();
            _animator.DoAction(GetActionSpeed());
        }

        private void AnimatorAction(string id)
        {
            if (id != "Action") return;
            SoundManager.Instance.PlayOneShot("action");
            _interactive?.Interact();
        }

        private void TryGetInteractive(IInteractive interactive)
        {
            _interactive = interactive;
        }

        private void Cooldown()
        {
            if (!_isOnCooldown) return;
            _cooldownTimer -= Time.deltaTime;
            if (!(_cooldownTimer <= 0f)) return;
            _isOnCooldown = false;
        }

        private float GetActionSpeed()
        {
            return actionAnimation.length / GetCooldown();
        }

        private float GetCooldown()
        {
            var first = Configuration.Instance.AllUpgrades.FirstOrDefault(u => GBGames.saves.upgradeSaves.IsCurrent(u.ID));

            var booster = first == null ? 1 : first.Booster;
            return Mathf.Clamp(baseCooldownTime * booster, 0.25f, 10);
        }
    }
}