using System;
using System.Collections;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.Environment
{
    public class FirstPortalEnabler : MonoBehaviour
    {
        [SerializeField] private Portal portal;

        private void Start()
        {
            portal.gameObject.SetActive(GBGames.saves.stageID >= 0);
            StartCoroutine(Check());
        }

        private IEnumerator Check()
        {
            while (GBGames.saves.stageID < 0)
            {
                yield return new WaitForSeconds(5f);
            }
            
            portal.gameObject.SetActive(true);        
        }
    }
}