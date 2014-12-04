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
	public class Player : Entity
	{
		private enum DirState { Still, Left, Right };
		private enum ActState { Ground, Jump, Fall };
		
		private DirState dirState;
		private ActState actState;
		
		private float ySpeed;
		private float xSpeed;
		
		private float walkSpeed;
		
		private float jumpHeight;
		private float jumpSpeed;
		private float gravity;
		private float lastGroundPos;
		
		////*****Hardcoded values
		private const int cLevelWidth = 30;
		private const int cLevelHeight = 17;
		private const int cViewWidth = 960;
		private const int cViewHeight = 544;
		private const int cTileSize = 32;
		
		public Player ()
		{
			texInfo = new TextureInfo ("/Application/textures/SpacebroSide.png");
			sprite = new SpriteUV (texInfo);
			
			sprite.Quad.S = texInfo.TextureSizef;
			sprite.Position = new Vector2 (64.0f,64.0f);
			
			dirState = DirState.Still;
			actState = ActState.Fall;
			
			xSpeed = 0.0f;
			ySpeed = 0.0f;
			
			walkSpeed = 3.0f;
			
			jumpHeight = 36.0f;
			jumpSpeed = 1.0f;
			gravity = 0.5f;
			lastGroundPos = 0.0f;
		}
		
		override public void Update(float _deltaTime, int[,] _levelData)
		{
			GetInput ();
			CheckCollisions(_levelData);
			CheckBoundaries();
			CheckStates ();
			
			sprite.Position = new Vector2(sprite.Position.X + xSpeed, sprite.Position.Y + ySpeed);
		}
		
		private void CheckCollisions(int[,] _levelData)
		{
			Vector2 bottomPoint = 	new Vector2(sprite.Position.X + (cTileSize/2), sprite.Position.Y);
			Vector2 topPoint = 		new Vector2(sprite.Position.X + (cTileSize/2), sprite.Position.Y+cTileSize);
			
			Vector2 leftPoint =		new Vector2(sprite.Position.X				, sprite.Position.Y+(cTileSize/2));
			Vector2 rightPoint = 	new Vector2(sprite.Position.X + cTileSize	, sprite.Position.Y+(cTileSize/2));
			
			if(CollisionHandler.PointCollision(_levelData, bottomPoint))
			{
				float newPos = FMath.Floor((sprite.Position.Y/ cViewHeight)	*cLevelHeight)	*cTileSize + cTileSize;
					
				sprite.Position = new Vector2(sprite.Position.X, newPos);
				
				actState = ActState.Ground;
			}
			else
			{
				if(actState != ActState.Jump)
				{
					actState = ActState.Fall;
				}
			}
			
			if(CollisionHandler.PointCollision(_levelData, rightPoint))
			{
				float newPos = FMath.Floor((sprite.Position.X/ cViewWidth)	*cLevelWidth)	*cTileSize - 1;
					
				sprite.Position = new Vector2(newPos, sprite.Position.Y);
				
				dirState = DirState.Still;
			}
			
			if(CollisionHandler.PointCollision(_levelData, leftPoint))
			{
				float newPos = FMath.Floor((sprite.Position.X/ cViewWidth)	*cLevelWidth)	*cTileSize + cTileSize;
					
				sprite.Position = new Vector2(newPos, sprite.Position.Y);
				
				dirState = DirState.Still;
			}
			
			if(CollisionHandler.PointCollision(_levelData, topPoint))
			{
				float newPos = FMath.Floor((sprite.Position.Y/ cViewHeight)	*cLevelHeight)	*cTileSize - 1;
					
				sprite.Position = new Vector2(sprite.Position.X, newPos);
				
				actState = ActState.Fall;
			}
		}
		
		private void CheckStates()
		{
			switch(dirState)
			{
			case DirState.Still:
				xSpeed = 0;
				break;
				
			case DirState.Right:
				
				sprite.FlipU = false;
				xSpeed = walkSpeed;
				
				break;
				
			case DirState.Left:
				
				sprite.FlipU = true;
				xSpeed = 0 - walkSpeed;
				
				break;
				
			}
			
			switch(actState)
			{
				
			case ActState.Ground:
				ySpeed = 0.0f;
				lastGroundPos = sprite.Position.Y;
				break;
			
			case ActState.Jump:
				Jump();
				break;
			
			case ActState.Fall:
				Fall ();
				break;
			}
		}
		
		private void CheckBoundaries()
		{
			if(ySpeed < 0.1f && ySpeed > -0.1f)
			{
				ySpeed = 0.0f;
			}
			
			if(sprite.Position.X < 0.0f)
			{
				dirState = DirState.Still;
				xSpeed = 0.0f;
				sprite.Position = new Vector2(0.0f, sprite.Position.Y);
			}
			
			if(sprite.Position.Y < 0.0f)
			{
				actState = ActState.Ground;
				ySpeed = 0.0f;
				sprite.Position = new Vector2(sprite.Position.X, 0.0f);
			}
		}
		

		
		private void Jump()
		{
			if(sprite.Position.Y < lastGroundPos + jumpHeight)
			{
				ySpeed += jumpSpeed;
			}
			else
			{
				actState = ActState.Fall;
			}
		}
		
		private void Fall()
		{
			ySpeed -= gravity;
		}
		
		
		
		private void GetInput()
		{
			dirState = DirState.Still;
			bool walkLeft = false;
			bool walkRight = false;
			
			if(InputHandler.KeyPressed (InputHandler.Key.Right))
			{
				walkRight = true;
				dirState = DirState.Right;
			}
			if(InputHandler.KeyPressed (InputHandler.Key.Left))
			{
				walkLeft = true;
				dirState = DirState.Left;
			}
			
			if(walkLeft && walkRight)
			{
				Console.WriteLine ("rawr");
				dirState = DirState.Still;
			}
			
			if(InputHandler.KeyPressed (InputHandler.Key.Up))
			{
				if(actState == ActState.Ground)
				{
					actState = ActState.Jump;
				}
			}
		}
		
	}
}

