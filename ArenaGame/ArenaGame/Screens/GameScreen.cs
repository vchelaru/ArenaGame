#region Usings

using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif
#endregion

namespace ArenaGame.Screens
{
	public partial class GameScreen
	{

		void CustomInitialize()
		{
            DebugInitialize();


        }

        private void DebugInitialize()
        {
            const bool useKeyboard = false;

            if (useKeyboard)
            {

                this.TestCharacter1.Movement = InputManager.Keyboard.Get2DInput(
                    Keys.A, Keys.D, Keys.W, Keys.S);

                this.TestCharacter1.RangedAttack = InputManager.Keyboard.GetKey(Keys.R);
                this.TestCharacter1.MeleeAttack = InputManager.Keyboard.GetKey(Keys.Space);

                this.TestEnemy1.AcquireTarget(this.CharacterList);
            }
            else
            {
                var first = InputManager.Xbox360GamePads[0];
                this.TestCharacter1.Movement = first.LeftStick;
                this.TestCharacter1.RangedAttack = first.GetButton(Xbox360GamePad.Button.X);
                this.TestCharacter1.MeleeAttack = first.GetButton(Xbox360GamePad.Button.A);


                var second = InputManager.Xbox360GamePads[1];
                this.TestCharacter2.Movement = second.LeftStick;
                this.TestCharacter2.RangedAttack = second.GetButton(Xbox360GamePad.Button.X);
                this.TestCharacter2.MeleeAttack = second.GetButton(Xbox360GamePad.Button.A);


                this.TestEnemy1.AcquireTarget(this.CharacterList);
            }
        }

		void CustomActivity(bool firstTimeCalled)
		{
            PerformCollisisionActivity();

		}

        private void PerformCollisisionActivity()
        {
            foreach(var area in this.AttackAreaList)
            {
                foreach(var enemy in this.EnemyList)
                {
                    if(area.CollideAgainst(enemy))
                    {
                        area.HandleCollideWith(enemy);
                    }
                }
            }

            foreach(var character in this.CharacterList)
            {
                foreach(var otherCharacter in this.CharacterList)
                {
                    if(character != otherCharacter)
                    {
                        character.CollideAgainstMove(otherCharacter, 1, 1);
                    }
                }
            }
        }

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
