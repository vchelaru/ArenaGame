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
        public float CollisionRadius
        {
            get { return this.CircleInstance.Radius; }
        }

        public Direction Direction
        {
            get;
            set;
        }

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
		    AssignDirectionFromMovement();
            SetAnimations(EnemyAnimations);

            AttachableEffectActivity();

		}

        private void AssignDirectionFromMovement()
        {
            bool isMoving = Velocity.LengthSquared() > 0;

            // Only attempt to assign a velocity if actually moving, otherwise
            // just keep the old value
            if (isMoving)
            {
                var analogDirection = Velocity;
                analogDirection.Normalize();
                bool isMovingHorizontally = Math.Abs(XVelocity) > Math.Abs(YVelocity);

                if (isMovingHorizontally)
                {
                    bool isMovingLeft = XVelocity < 0;
                    if (isMovingLeft)
                    {
                        Direction = Entities.Direction.Left;
                    }
                    else
                    {
                        Direction = Entities.Direction.Right;
                    }
                }
                else
                {
                    bool isMovingDown = YVelocity < 0;
                    if (isMovingDown)
                    {
                        Direction = Entities.Direction.Down;
                    }
                    else
                    {
                        Direction = Entities.Direction.Up;
                    }
                }
            }
        }


        public virtual void SetAnimations(AnimationChainList animations)
        {
            if (MainSprite != null)
            {
                string chainToSet = GetChainToSet();

                if (!string.IsNullOrEmpty(chainToSet))
                {
                    bool differs = MainSprite.CurrentChainName == null ||
                        MainSprite.CurrentChainName != chainToSet;

                    if (differs)
                    {
                        MainSprite.SetAnimationChain(animations[chainToSet]);
                    }
                }
            }
        }

        private string GetChainToSet()
        {
            string animationType = "Idle";

            bool isMoving = Velocity.LengthSquared() > 0;
            if (isMoving)
            {
                animationType = "Walk";
            }

            return animationType + Direction;

        }

        private void MovementActivity()
        {
            if(target != null)
            {
                var directionVector = new Vector2(target.X - X, target.Y - Y);

                if (directionVector.LengthSquared() > 0)
                {
                    directionVector.Normalize();

                    Velocity = new Vector3(directionVector * EnemyInfo.Speed, 0);
                }
            }
        }

        private void AttachableEffectActivity()
        {
            foreach(var item in Effects)
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
            if(Health <= 0)
            {
                Destroy();
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
