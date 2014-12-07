using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Audio;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;


namespace SPACE
{
	public class GameScene : Scene
	{

		private bool		scenePaused;
		public bool			swapScene {get; set;}
		
		private Entity	  player;

		private int[,] 	  currentLevel;
		private const int levelWidth = 30;
		private const int levelHeight = 17;
		private const int tileSize = 32;
		
		
		public GameScene()
		{
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);	// Tells the director that this "node" requires to be updated
			
			currentLevel = LevelLoader.GetLevel();
			DrawLevel ();
			
			scenePaused = false;
			swapScene = false;;
			
			player = new Player();
			this.AddChild(player.Sprite);
			
		}
		
		public override void Update(float deltaTime)
		{
			if(!scenePaused)
			{
				player.Update (deltaTime, currentLevel);
			}
		}
		
		public void ResetScene()
		{
		}
		
		
		

		
		private void DrawLevel()
		{
			TextureInfo texInfo1 = new TextureInfo ("/Application/textures/lego.png");
			TextureInfo texInfo2 = new TextureInfo ("/Application/textures/lava.png");
			
			for(int y = 0; y < levelHeight; y++)
			{
				for(int x = 0; x < levelWidth; x++)
				{
					if(currentLevel[y,x] == 1)
					{
						SpriteUV sprite = new SpriteUV(texInfo1);
						sprite.Quad.S = texInfo1.TextureSizef;
						sprite.Position = new Vector2 (x*tileSize, y*tileSize);
						this.AddChild (sprite);
					}
					if(currentLevel[y,x] == 2)
					{
						SpriteUV sprite = new SpriteUV(texInfo2);
						sprite.Quad.S = texInfo2.TextureSizef;
						sprite.Position = new Vector2 (x*tileSize, y*tileSize);
						this.AddChild (sprite);
					}
				}
			}
		}
		
	}
}




//blankLevelData = new int{
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//	{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
//};