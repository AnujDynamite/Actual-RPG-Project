using System.Collections;
using System.Collections.Generic;
using RPG.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.MainMenu
{
    public class SaveLoadUI : MonoBehaviour
    {
        [SerializeField] Transform contentRoot;
        [SerializeField] GameObject buttonPrefab;

        // OnEnable is triggered when the parent gameObject is re-activated.
        // The switcher gameObject de-activates this component so OnEnable is required.
        private void OnEnable()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            if (savingWrapper == null) return;

            // Clearing potential leftover content.
            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);
            }

            foreach(string save in savingWrapper.ListSaves())
            {
                GameObject buttonInstance = Instantiate(buttonPrefab, contentRoot);
                TMP_Text textComponent = buttonInstance.GetComponentInChildren<TMP_Text>();
                textComponent.text = save;
                Button button = buttonInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    savingWrapper.LoadGame(save);
                });
            }
        }
    }
}
