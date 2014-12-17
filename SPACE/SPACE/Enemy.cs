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
	public class Enemy : Entity
	{
		private int mode;
		
		float spawnX, spawnY;
		float moveDist = 128.0f;
		float moveSpeed = 2.0f;
		bool moveSwitch = true;
		
		
		public Enemy (Vector2 _pos, String _fileName, int _mode)
		{
			texInfo = new TextureInfo ("/Application/textures/" + _fileName + ".png");
			sprite = new SpriteUV (texInfo);
			sprite.Quad.S = texInfo.TextureSizef;
			sprite.Position = new Vector2 (_pos.X, _pos.Y);
			
			spawnX = _pos.X;
			spawnY = _pos.Y;
			mode = _mode;
		}
		
		
		override public void Update(float _deltaTime, int[,] _levelData)
		{
			switch(mode)
			{
			case 1:
				MoveHorizontal();
				break;
			case 2:
				MoveVertical();
				break;
			}
		}
		
		private void MoveHorizontal()
		{
			if(moveSwitch)
			{
				if(sprite.Position.X < spawnX + moveDist)
				{
					sprite.Position = new Vector2(sprite.Position.X + moveSpeed, sprite.Position.Y);
				}
				else
				{
					moveSwitch = !moveSwitch;
				}
			}
			else
			{
				if(sprite.Position.X > spawnX)
				{
					sprite.Position = new Vector2(sprite.Position.X - moveSpeed, sprite.Position.Y);
				}
				else
				{
					moveSwitch = !moveSwitch;
				}
			}
		}
		
		private void MoveVertical()
		{
			if(moveSwitch)
			{
				if(sprite.Position.Y < spawnY + moveDist)
				{
					sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y + moveSpeed);
				}
				else
				{
					moveSwitch = !moveSwitch;
				}
			}
			else
			{
				if(sprite.Position.Y > spawnY)
				{
					sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y - moveSpeed);
				}
				else
				{
					moveSwitch = !moveSwitch;
				}
			}
		}
		
		public Vector2 Pos()
		{
			Vector2 Pos = new Vector2(sprite.Position.X, sprite.Position.Y);
			return Pos;
		}
				
		override public string ReturnType()
		{
			return "Enemy";
		}
	}
}

