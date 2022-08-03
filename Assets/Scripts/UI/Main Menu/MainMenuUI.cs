using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Utils;
using RPG.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        LazyValue<SavingWrapper> savingWrapper;
        [SerializeField] TMP_InputField newGameNameField;
        [SerializeField] Button continueButton;

        private void Awake()
        {
            savingWrapper = new LazyValue<SavingWrapper>(GetSavingWrapper);
        }

        private SavingWrapper GetSavingWrapper()
        {
            return FindObjectOfType<SavingWrapper>();
        }

        public void Continue()
        {
            if (savingWrapper.value != null)
            {
                savingWrapper.value.ContinueGame();
            }
        }

        public void New()
        {
            savingWrapper.value.NewGame(newGameNameField.text);
        }

        public void DeleteSave()
        {
            savingWrapper.value.Delete();
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}