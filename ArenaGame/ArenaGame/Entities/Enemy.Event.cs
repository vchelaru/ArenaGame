using System;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using FlatRedBall.Audio;
using FlatRedBall.Screens;
using ArenaGame.Entities;
using ArenaGame.Screens;
namespace ArenaGame.Entities
{
	public partial class Enemy
	{
        void OnAfterEnemyInfoSet (object sender, EventArgs e)
        {
            this.Health = this.EnemyInfo.Health;
        }
		
	}
}
