#region Usings

using System;
using System.Collections.Generic;
using System.Text;
using ArenaGame.CharacterProfiles;
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
using ArenaGame.DataTypes.AttackEffects;
using Microsoft.Xna.Framework;

#endif
#endregion

namespace ArenaGame.Entities
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

	public partial class Character : ICharacter
	{
        List<AttackEffectBase> meleeAttackEffects = new List<AttackEffectBase>();
        List<AttackEffectBase> rangedAttackEffects = new List<AttackEffectBase>();
        Vector2 analogDirection;

        public I2DInput Movement { get; set; }
        public IPressableInput MeleeAttack { get; set; }
        public IPressableInput RangedAttack { get; set; }


	    public Direction Direction
        {
            get;
            set;

        }

        /// <summary>
        /// Initialization logic which is execute only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
		private void CustomInitialize()
		{
            // temp!
            this.Health = 100;

            var instantDamage = new InstantDamage {Amount = 10};
            this.meleeAttackEffects.Add(instantDamage);
            this.rangedAttackEffects.Add(instantDamage);

            this.Direction = Entities.Direction.Right;
            this.analogDirection = Vector2.UnitX;
		}

		private void CustomActivity()
		{
            InputActivity();

            AttachableEffectActivity();
		}


        private void AttachableEffectActivity()
        {
            foreach (var item in this.Effects)
            {
                item.ApplyUpdate();
            }
        }

        private void InputActivity()
        {
            this.XVelocity = Movement.X * Speed;
            this.YVelocity = Movement.Y * Speed;

            AssignDirectionFromInput(Movement);

            if(MeleeAttack.WasJustPressed)
            {
                PerformMeleeAttack();
            }
            if(RangedAttack.WasJustPressed)
            {
                PerformRangedAttack();
            }
        }

        private void AssignDirectionFromInput(I2DInput input)
        {
            var inputVelocity = new Vector2(input.X, input.Y);

            bool isMoving = inputVelocity.LengthSquared() > 0;

            // Only attempt to assign a velocity if actually moving, otherwise
            // just keep the old value
            if (isMoving)
            {
                analogDirection = inputVelocity;
                analogDirection.Normalize();
                bool isMovingHorizontally = Math.Abs(this.XVelocity) > Math.Abs(this.YVelocity);

                if (isMovingHorizontally)
                {
                    bool isMovingLeft = this.XVelocity < 0;
                    if (isMovingLeft)
                    {
                        this.Direction = Entities.Direction.Left;
                    }
                    else
                    {
                        this.Direction = Entities.Direction.Right;
                    }
                }
                else
                {
                    bool isMovingDown = this.YVelocity < 0;
                    if (isMovingDown)
                    {
                        this.Direction = Entities.Direction.Down;
                    }
                    else
                    {
                        this.Direction = Entities.Direction.Up;
                    }
                }
            }
        }

        private void PerformMeleeAttack()
        {

            var attackArea = Factories.AttackAreaFactory.CreateNew();
            attackArea.Effects = this.meleeAttackEffects;
            attackArea.Position = this.Position;
            const float attackOffset = 16;

            switch (this.Direction)
            {
                case Direction.Up:
                    attackArea.Y += attackOffset;
                    break;
                case Direction.Down:
                    attackArea.Y -= attackOffset;

                    break;
                case Direction.Left:
                    attackArea.X -= attackOffset;

                    break;
                case Direction.Right:
                    attackArea.X += attackOffset;

                    break;
            }

            const float secondsLasting = .3f;
            attackArea.Call(attackArea.Destroy).After(secondsLasting);

        }

        private void PerformRangedAttack()
        {
            var attackArea = Factories.AttackAreaFactory.CreateNew();
            attackArea.Effects = this.rangedAttackEffects;
            attackArea.Position = this.Position;

            const float projectileSpeed = 400;

            attackArea.Velocity = new Vector3(analogDirection * projectileSpeed, 0);
            const float secondsLasting = 1;
            attackArea.Call(attackArea.Destroy).After(secondsLasting);
        }

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }


        /// <summary>
        /// The list of effects placed on this by other targets, like if the target is on fire, or if it is poisoned
        /// </summary>
        List<AbilityRuntimes.AttachableEffect> effects = new List<AbilityRuntimes.AttachableEffect>();

	    public CharacterProfile Profile { get; set; }

	    public int Health { get; set; }

	    public List<AbilityRuntimes.AttachableEffect> Effects
        {
            get { return effects; }
        }
    }
}
