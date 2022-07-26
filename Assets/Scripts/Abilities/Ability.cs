using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Attributes;
using RPG.Core;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "My Ability", menuName = "Abilities/Ability", order = 0)]
    public class Ability : ActionItem
    {
        [SerializeField] TargetingStrategy targetingStrategy;
        [SerializeField] FilterStrategy[] filterStrategies;
        [SerializeField] EffectStrategy[] effectStrategies;
        [SerializeField] float cooldownTime = 2.0f;
        [SerializeField] float manaCost = 0f;

        public override void Use(GameObject user)
        {
            Mana mana = user.GetComponent<Mana>();
            if(mana.GetMana() < manaCost)
            {
                return;
            }

            CooldownStore cooldownStore = user.GetComponent<CooldownStore>();
            if(cooldownStore.GetTimeRemaining(this) > 0)
            {
                return;
            }

            AbilityData data = new AbilityData(user);

            // Cancelling moving/fighting actions.
            ActionScheduler actionScheduler = user.GetComponent<ActionScheduler>();
            actionScheduler.StartAction(data);

            targetingStrategy.StartTargeting(data, 
                () => {
                    TargetAcquired(data);
                });
        }

        private void TargetAcquired(AbilityData data)
        {
            // If we cancel using ability due to death or doing something else
            // while targeting.
            if(data.IsCancelled())
            {
                return;
            }
            Mana mana = data.GetUser().GetComponent<Mana>();
            if(!mana.UseMana(manaCost))
            {
                return;
            }
            CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>();
            cooldownStore.StartCooldown(this, cooldownTime);
            foreach (var filterStrategy in filterStrategies)
            {
                data.SetTargets(filterStrategy.Filter(data.GetTargets()));
            }
            
            foreach (var effect in effectStrategies)
            {
                effect.StartEffect(data, EffectFinished);
            }
        }

        private void EffectFinished()
        {
            
        }
    }
}