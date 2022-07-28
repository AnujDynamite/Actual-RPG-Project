using System.Collections;
using System.Collections.Generic;
using TMPro;
using RPG.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Traits
{
    public class TraitUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI unassignedPointsText;
        [SerializeField] Button commitButton;

        TraitStore playerTraitStore = null;

        private void Start()
        {
            playerTraitStore = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitStore>();
            // No lambda function needed in this case as no argument is being passed in Commit.
            commitButton.onClick.AddListener(playerTraitStore.Commit);
        }

        private void Update()
        {
            unassignedPointsText.text = playerTraitStore.GetUnassignedPoints().ToString();
        }
    }
}
