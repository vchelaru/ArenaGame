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

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using ArenaGame.DataTypes;

#endif
#endregion

namespace ArenaGame.Entities
{
	public partial class EnemySpawner
	{
        double lastSpawn;

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
            TryPerformSpawn();

		}

	    private bool spawn = true;
        private void TryPerformSpawn()
        {
            bool shouldSpawn = spawn && TimeManager.SecondsSince(lastSpawn) > 5;
            if(shouldSpawn)
            {
                spawn = false;
                lastSpawn = TimeManager.CurrentTime;

                SpawnEnemy(GlobalContent.EnemyInfo[EnemyInfo.TestMonster]);
            }
        }

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        void SpawnEnemy(EnemyInfo enemyType)
        {
            var newEnemy = Factories.EnemyFactory.CreateNew(this.LayerProvidedByContainer);
            newEnemy.Position = this.AxisAlignedRectangleInstance.GetRandomPositionInThis();
            newEnemy.Z = 6;

            newEnemy.EnemyInfo = enemyType;

        }

	}
}
