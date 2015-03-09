#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using ArenaGame.Entities.Interfaces;
using ArenaGame.AbilityRuntimes;
using ArenaGame.DataTypes.AttackEffects;

#endif
#endregion

namespace ArenaGame.Entities
{
	public partial class AttackArea
	{

        static uint nextId = 0;

        uint id;

        public IEnumerable<AttackEffectBase> Effects { get; set; }

        public ICharacter Source { get; set; }


        /// <summary>
        /// Initialization logic which is execute only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
		private void CustomInitialize()
		{
            id = nextId;
            nextId++;
		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public void HandleCollideWith(ICharacter target)
        {
            foreach (var effect in this.Effects)
            {
                bool isProtected = target.Effects.Any(item => item.AreaId == this.id);

                if (!isProtected)
                {
                    var attachableEffect = new AttachableEffect();
                    attachableEffect.AreaId = this.id;
                    attachableEffect.Effect = effect;
                    attachableEffect.Source = Source;
                    attachableEffect.AttachTo(target);
                }
            }
        }

	}
}
