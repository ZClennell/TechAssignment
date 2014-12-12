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
		
		private int levelNum = 1;
		TextureInfo basicTile;
		
		
		public GameScene()
		{
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);	// Tells the director that this "node" requires to be updated
			
			scenePaused = false;
			swapScene = false;
			
			LoadLevel(levelNum);
		}
		
		private void LoadLevel(int _levelNum)
		{
			levelNum = _levelNum;
			currentLevel = LevelLoader.GetLevel (_levelNum);
			
			CleanupLevel();
			
			DrawLevel ();
			DrawEntities();
		}
		
		private void CleanupLevel()
		{
			this.RemoveAllChildren (true);
			entityList.Clear();
		}
		
		private void DrawEntities()
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
				foreach (var entity in entityList.ToArray())
				{
            		entity.Update (deltaTime, currentLevel);
					
					if(entity.ReturnType().Equals("Player"))
					{
						CheckEntityCollisions(entity);
					}
				}
				
				if(InputHandler.KeyPressed (InputHandler.Key.Enter))
				{
					LoadLevel (levelNum+1);
				}
			}
		}

		private void DrawLevel()
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
			
			switch(levelNum)
			{
				case 1:
					basicTile = new TextureInfo ("/Application/textures/lego.png");
				break;
				case 2:
					basicTile = new TextureInfo ("/Application/textures/wavey.png");
				break;
				case 3:
					basicTile = new TextureInfo ("/Application/textures/lava.png");
				break;
			}
			
			switch(_level[_y,_x])
			{
				case 1:
					SpriteUV sprite = new SpriteUV(basicTile);
					sprite.Quad.S = basicTile.TextureSizef;
					sprite.Position = new Vector2 (tileX, tileY);
					this.AddChild (sprite);
				break;
					
				case 2:
					entityList.Add(new Enemy(new Vector2(tileX, tileY), "Enemy4",false,false));
				break;
					
				case 3:
					entityList.Add(new Enemy(new Vector2(tileX, tileY), "Enemy3",true,false));
				break;
					
				case 4:
					Player player = new Player();
					player.SetPosition (new Vector2(tileX, tileY));
					entityList.Add(player);
				break;
				
				case 5:
					entityList.Add(new ExitTile(tileX, tileY));
				break;
			}
		}
		
		private void CheckEntityCollisions(Entity _entity1)
		{
			foreach (var entity2 in entityList.ToArray())
			{
				if(CollisionHandler.BoxCollision(_entity1.Sprite, entity2.Sprite))
				{
					if(entity2.ReturnType().Equals ("Enemy"))
					{
						LoadLevel (levelNum);
					}
					if(entity2.ReturnType().Equals ("Exit"))
					{
						LoadLevel (levelNum +1);
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