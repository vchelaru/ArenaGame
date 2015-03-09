using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArenaGame.Entities.Interfaces;

namespace ArenaGame.DataTypes.AttackEffects
{
    public class InstantDamage : AttackEffectBase
    {
        public int Amount { get; set; }


        public override void ApplyUpdate(ICharacter target, bool hasAlreadyBeenApplied)
        {
            if (!hasAlreadyBeenApplied)
            {
                target.Health -= Amount;
            }
        }

        public override bool ShouldBeRemoved(ICharacter source, ICharacter target)
        {
            // InstantDamage applies only once.
            return false;
        }

    }
}
