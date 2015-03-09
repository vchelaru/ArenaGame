using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArenaGame.AbilityRuntimes;

namespace ArenaGame.Entities.Interfaces
{
    public interface ICharacter
    {
        int Health { get; set; }
        List<AttachableEffect> Effects { get; }


    }
}
