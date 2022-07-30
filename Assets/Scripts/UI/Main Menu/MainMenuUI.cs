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
            savingWrapper.value.ContinueGame();
        }

        public void New()
        {
            savingWrapper.value.NewGame(newGameNameField.text);
        }
    }
}