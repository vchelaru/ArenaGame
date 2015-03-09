using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArenaGame.DataTypes.AttackEffects;
using ArenaGame.Entities.Interfaces;
using FlatRedBall;

namespace ArenaGame.AbilityRuntimes
{



    public class AttachableEffect
    {
        bool hasBeenApplied = false;
        public uint AreaId { get; set; }

        public ICharacter Source
        {
            get;
            set;
        }

        public ICharacter Target
        {
            get;
            private set;
        }

        public AttackEffectBase Effect
        {
            get;
            set;
        }


        public AttachableEffect()
        {
        }

        public void AttachTo(ICharacter target)
        {
            this.Target = target;
            Target.Effects.Add(this);
        }

        public void ApplyUpdate()
        {
            Effect.ApplyUpdate(Target, hasBeenApplied);

            if(Effect.ShouldBeRemoved(Source, Target))
            {
                Target.Effects.Remove(this);
            }

            hasBeenApplied = true;
        }
    }
}
