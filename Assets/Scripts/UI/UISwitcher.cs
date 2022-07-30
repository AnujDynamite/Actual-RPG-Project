using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class UISwitcher : MonoBehaviour
    {
        [SerializeField] GameObject entryPoint;

        private void Start()
        {
            if (entryPoint == null) return;
            SwitchTo(entryPoint);
        }

        public void SwitchTo(GameObject toDisplay)
        {
            // If the UISwitcher is not on the parent
            if (toDisplay.transform.parent != transform) return;
            foreach(Transform child in transform)
            {
                // Only displays objects that has toDisplay marked active.
                child.gameObject.SetActive(child.gameObject == toDisplay);
            }
        }
    }
}