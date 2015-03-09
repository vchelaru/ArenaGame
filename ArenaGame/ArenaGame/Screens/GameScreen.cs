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

            Factories.EnemyFactory.EntitySpawned = HandleEnemySpawned;

            InitializeLevel("Level1");
        }

        private void HandleEnemySpawned(Entities.Enemy newEnemy)
        {
            newEnemy.AcquireTarget(this.CharacterList);
        }

        private void DebugInitialize()
        {
            const bool useKeyboard = true;

            if (useKeyboard)
            {
                this.TestCharacter1.Movement = InputManager.Keyboard.Get2DInput(
                    Keys.A, Keys.D, Keys.W, Keys.S);

                this.TestCharacter2.Movement = InputManager.Keyboard.Get2DInput(Keys.Left, Keys.Right, Keys.Up,
                    Keys.Down);

                this.TestCharacter1.RangedAttack = InputManager.Keyboard.GetKey(Keys.R);
                this.TestCharacter1.MeleeAttack = InputManager.Keyboard.GetKey(Keys.Space);

                this.TestCharacter2.RangedAttack = InputManager.Keyboard.GetKey(Keys.P);
                this.TestCharacter2.MeleeAttack = InputManager.Keyboard.GetKey(Keys.O);
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


            }
        }

		void CustomActivity(bool firstTimeCalled)
		{
            PerformCollisisionActivity();

		}

        private void PerformCollisisionActivity()
        {
            EnemyList.SortXInsertionAscending();
            var maxXSeparation = 0f;
            if (EnemyList.Count > 0)
            {
                maxXSeparation = EnemyList[0].CollisionRadius*2;
            }

            for (int index = 0; index < this.AttackAreaList.Count; index++)
            {
                var area = this.AttackAreaList[index];
                for (int i = 0; i < this.EnemyList.Count; i++)
                {
                    var enemy = this.EnemyList[i];
                    if (area.CollideAgainst(enemy))
                    {
                        area.HandleCollideWith(enemy);
                    }
                }
            }

            var collisionCount = 0;
            var enemyListCount = this.EnemyList.Count;
            for (int index = 0; index < enemyListCount - 1; index++)
            {
                var enemy = this.EnemyList[index];
                for (int i = index + 1; i < enemyListCount; i++)
                {
                    var otherEnemy = this.EnemyList[i];

                    if (otherEnemy.X - enemy.X < maxXSeparation)
                    {
                        collisionCount++;
                        enemy.CollideAgainstMove(otherEnemy, 1, 1);
                    }
                    else
                    {
                        break;
                    }
                }

                for (int i = 0; i < this.CharacterList.Count; i++)
                {
                    var character = this.CharacterList[i];
                    if (character.CollideAgainst(enemy))
                    {
                        // kill them?
                    }
                }
            }

            FlatRedBall.Debugging.Debugger.Write(collisionCount);


            for (int index = 0; index < this.CharacterList.Count; index++)
            {
                var character = this.CharacterList[index];
                for (int i = 0; i < this.CharacterList.Count; i++)
                {
                    var otherCharacter = this.CharacterList[i];
                    if (character != otherCharacter)
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
