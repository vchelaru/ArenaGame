using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArenaGame.Entities.Interfaces;

namespace ArenaGame.DataTypes.AttackEffects
{
    public abstract class AttackEffectBase
    {
        public abstract void ApplyUpdate(ICharacter target, bool hasAlreadyBeenApplied);

        public abstract bool ShouldBeRemoved(ICharacter source, ICharacter target);

    }
}
