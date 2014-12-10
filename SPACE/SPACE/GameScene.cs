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
		
		private List<Entity> entityList = new List<Entity>();

		private int[,] 	  currentLevel;
		private const int cLevelWidth = 30;
		private const int cLevelHeight = 17;
		private const int cTileSize = 32;
		
		
		public GameScene()
		{
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);	// Tells the director that this "node" requires to be updated
			
			scenePaused = false;
			swapScene = false;;
			
			currentLevel = LevelLoader.GetLevel();
			
			CreateLevel ();
			
			AddToScene();
		}
		
		private void AddToScene()
		{
			foreach (var entity in entityList)
			{
            	this.AddChild (entity.Sprite);
			}
		}
		
		public override void Update(float deltaTime)
		{
			if(!scenePaused)
			{
				foreach (var entity in entityList)
				{
            		entity.Update (deltaTime, currentLevel);
				}
			}
		}
		
		public void ResetScene()
		{
		}
		
		
		

		
		private void CreateLevel()
		{
			for(int y = 0; y < cLevelHeight; y++)
			{
				for(int x = 0; x < cLevelWidth; x++)
				{
					AddElement(currentLevel, x, y);
				}
			}
		}
		
		private void AddElement(int[,] _level, int _x, int _y)
		{
			float tileX = _x*cTileSize;	
			float tileY = _y*cTileSize;
			
			TextureInfo texInfo1 = new TextureInfo ("/Application/textures/lego.png");
			TextureInfo texInfo2 = new TextureInfo ("/Application/textures/lava.png");
			
			switch(_level[_y,_x])
			{
				case 1:
					SpriteUV sprite = new SpriteUV(texInfo1);
					sprite.Quad.S = texInfo1.TextureSizef;
					sprite.Position = new Vector2 (tileX, tileY);
					this.AddChild (sprite);
				break;
					
				case 2:
					entityList.Add(new Enemy(new Vector2(tileX, tileY), "Enemy3",false,false));
				break;
					
				case 3:
					entityList.Add(new Enemy(new Vector2(tileX, tileY), "Enemy4",false,false));
				break;
					
				case 4:
					Player player = new Player();
					player.SetPosition (new Vector2(tileX, tileY));
					entityList.Add(player);
				break;
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