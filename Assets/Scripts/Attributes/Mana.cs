using System.Collections;
using System.Collections.Generic;
using GameDevTV.Saving;
using GameDevTV.Utils;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Mana : MonoBehaviour, ISaveable
    {
        // At Awake the game may not have fully finished loading the save data
        // which is needed for BaseStats to be populated to know the level of the character
        // and therefore what starting stats they should have.
        // At Start the component should already be ready for use, other componenets could
        // be calling this one.
        // LazyValue allows us to wrap a value as a function that gets initial value.
        LazyValue<float> mana;

        //float mana;

        private void Awake()
        {
            mana = new LazyValue<float>(GetMaxMana);
            //mana = maxMana;
        }

        private void Update()
        {
            if(mana.value < GetMaxMana())
            {
                mana.value += GetRegenRate() * Time.deltaTime;
                if(mana.value > GetMaxMana())
                {
                    mana.value = GetMaxMana();
                }
            }
        }

        public float GetMana()
        {
            return mana.value;
        }

        public float GetMaxMana()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Mana);
        }

        public float GetRegenRate()
        {
            return GetComponent<BaseStats>().GetStat(Stat.ManaRegenRate);
        }

        public bool UseMana(float manaToUse)
        {
            if(manaToUse > mana.value)
            {
                return false;
            }
            mana.value -= manaToUse;
            return true;
        }

        public object CaptureState()
        {
            return mana.value;
        }

        public void RestoreState(object state)
        {
            mana.value = (float)state;
        }
    }
}
