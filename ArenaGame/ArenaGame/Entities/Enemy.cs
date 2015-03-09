#region Usings

using System;
using System.Linq;
using System.Collections.Generic;
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
using Microsoft.Xna.Framework;

#endif
#endregion

namespace ArenaGame.Entities
{
	public partial class Enemy : ICharacter
	{
        public int health;

        ICharacter target;

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;

                ReactToHealthChanged();
            }
        }


        /// <summary>
        /// Initialization logic which is execute only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
		private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{
            MovementActivity();

            AttachableEffectActivity();

		}

        private void MovementActivity()
        {
            if(target != null)
            {
                var directionVector = new Vector2(this.target.X - this.X, this.target.Y - this.Y);
                directionVector.Normalize();

                this.Velocity = new Vector3(directionVector * this.EnemyInfo.Speed, 0);
            }
        }

        private void AttachableEffectActivity()
        {
            foreach(var item in this.Effects)
            {
                item.ApplyUpdate();
            }
        }

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public void AcquireTarget(IEnumerable<ICharacter> possibleTargets)
        {
            target = possibleTargets.FirstOrDefault();
        }

        private void ReactToHealthChanged()
        {
            if(this.Health <= 0)
            {
                this.Destroy();
            }
        }



        /// <summary>
        /// The list of effects placed on this by other targets, like if the target is on fire, or if it is poisoned
        /// </summary>
        List<AbilityRuntimes.AttachableEffect> effects = new List<AbilityRuntimes.AttachableEffect>();
        public List<AbilityRuntimes.AttachableEffect> Effects
        {
            get { return effects; }
        }
    }
}
