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
	public class CoinBomb: Entity
	{
		float xSpeed, ySpeed;
		float gravity = 0.1f;
		float maxSpeed = 6.0f;
		float bounciness = 0.8f;
		float falloff = 0.995f;
		
		public int bombTimer = 60;
		
		Vector2 hitBox = new Vector2(8.0f, 8.0f);
		Vector2 hitBoxOffset = new Vector2(0.0f, 0.0f);
		
		String returnType = "";

		public CoinBomb (float _x, float _y)
		{
			texInfo = new TextureInfo ("/Application/textures/BouncyBomb.png");
			sprite = new SpriteUV (texInfo);
			
			sprite.Quad.S = texInfo.TextureSizef;
			sprite.Position = new Vector2 (_x,_y);
			
			Random rng = new Random((int)(sprite.Position.X + sprite.Position.Y));
			xSpeed = rng.Next ((int)(0-maxSpeed), (int)maxSpeed);
			ySpeed = rng.Next ((int)(0-maxSpeed), (int)maxSpeed);
			
			bombTimer += rng.Next (120);
		}
		
		override public void Update(float deltaTime, int[,] levelData)
		{
			bombTimer --;
			if(bombTimer <= 0)
			{
				returnType = "Bomb";
			}
			
			xSpeed *= falloff;
			ySpeed *= falloff;
			
			ySpeed -= gravity;
			
			CheckCollisions(levelData);
			
			sprite.Position = new Vector2(sprite.Position.X+ xSpeed, sprite.Position.Y + ySpeed);
		}
		
		private void CheckCollisions(int[,] _levelData)
		{
			//These are the collision boundaries for the sprite.
			//Each is checked separately since different functions will happen 
			//depending on the direction of the collision.
			
			Vector2 topLeft = 		new Vector2(sprite.Position.X + hitBoxOffset.X, 
			                                    sprite.Position.Y + hitBoxOffset.Y + hitBox.Y);
			
			Vector2 bottomLeft =	new Vector2(sprite.Position.X + hitBoxOffset.X,
			                                 	sprite.Position.Y + hitBoxOffset.Y);
			
			Vector2 bottomRight =	new Vector2(sprite.Position.X + hitBoxOffset.X + hitBox.X,
			                                  	sprite.Position.Y + hitBoxOffset.Y);


			//Ground Collision
			if(CollisionHandler.PointCollision(_levelData, bottomLeft))
			{
				sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y+2);
				ySpeed *= -bounciness;
			}
			
			//Right Collision
			if(CollisionHandler.PointCollision (_levelData, bottomRight))
			{
				xSpeed *= -bounciness;
				sprite.Position = new Vector2(sprite.Position.X - 4, sprite.Position.Y);
			}
			
			//Left Collision
			if(CollisionHandler.PointCollision(_levelData, bottomLeft))
			{
				xSpeed *= -bounciness;
				sprite.Position = new Vector2(sprite.Position.X + 4, sprite.Position.Y);
			}
			
			//Top Collision
			if(CollisionHandler.PointCollision(_levelData, topLeft))
			{
				sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y-4);
				ySpeed *= -bounciness;
			}
		}
		override public string ReturnType()
		{
			return returnType;
		}
		
	}
}

