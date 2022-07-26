using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        // TimeMultiplier is used solely for faster testing.
        [SerializeField] float timeMultiplier = 1.0f;
        PlayerController playerController;

        private void Awake()
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            if (playerController == null) return;
            Time.timeScale = 0;
            playerController.enabled = false;
        }

        private void OnDisable()
        {
            if (playerController == null) return;
            Time.timeScale = timeMultiplier;
            playerController.enabled = true;
        }

        public void Save()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();
        }
        
        public void SaveAndQuit()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();
            savingWrapper.LoadMenu();
        }
    }
}
