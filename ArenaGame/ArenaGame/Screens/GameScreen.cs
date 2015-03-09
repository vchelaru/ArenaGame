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
            this.TestCharacter1.Movement = InputManager.Keyboard.Get2DInput(
                Keys.A, Keys.D, Keys.W, Keys.S);

            this.TestCharacter1.RangedAttack = InputManager.Keyboard.GetKey(Keys.R);
            this.TestCharacter1.MeleeAttack = InputManager.Keyboard.GetKey(Keys.Space);

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
        }

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
