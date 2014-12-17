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
		
		private SoundPlayer coinSound;
		private SoundPlayer jumpSound;
		private SoundPlayer endSound;
		
		private int levelNum = 1;
		TextureInfo basicTile;
		
		
		public GameScene()
		{
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);	// Tells the director that this "node" requires to be updated
			
			scenePaused = false;
			swapScene = false;
			
			LoadLevel(levelNum);
			
			coinSound = new Sound("/Application/sounds/Pickup_Coin.wav").CreatePlayer();
			jumpSound = new Sound("/Application/sounds/Jump.wav").CreatePlayer();
			endSound = new Sound("/Application/sounds/EndLev.wav").CreatePlayer();
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
					if(entity.ReturnType().Equals("Player"))
					{
						CheckEntityCollisions(entity);
					}
					
					entity.Update (deltaTime, currentLevel);
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
					entityList.Add(new Enemy(new Vector2(tileX, tileY), "Enemy4",1));
				break;
					
				case 3:
					entityList.Add(new Enemy(new Vector2(tileX, tileY), "Enemy3",2));
				break;
					
				case 4:
					Player player = new Player();
					player.SetPosition (new Vector2(tileX, tileY));
					entityList.Add(player);
				break;
				
				case 5:
					entityList.Add(new ExitTile(tileX, tileY));
				break;
				case 6:
					entityList.Add(new Box(tileX, tileY));
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
						if(jumpSound.Status != SoundStatus.Playing)
						{
							jumpSound.Play();
						}
					}
					if(entity2.ReturnType().Equals ("Exit"))
					{
						if(endSound.Status != SoundStatus.Playing)
						{
							endSound.Play();
						}
						LoadLevel (levelNum +1);
					}
					if(entity2.ReturnType().Equals ("Box"))
					{
						this.RemoveChild(entity2.Sprite,true);
						entityList.Remove(entity2);
						
						if(coinSound.Status != SoundStatus.Playing)
						{
							coinSound.Play();
						}
						
						SpawnCoins (new Vector2(entity2.Sprite.Position.X, entity2.Sprite.Position.Y), 12);
					}
				}
			}
		}
		
		private void SpawnCoins(Vector2 _position, int _amount)
		{
			Random rng = new Random();
			
			for(int a = 0; a < _amount; a++)
			{
				float xOffset = rng.Next(8);
				float yOffset = rng.Next(8);
				
				TinyCoin coin = new TinyCoin(_position.X+xOffset, _position.Y+yOffset);
				entityList.Add (coin);
				this.AddChild (coin.Sprite);
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