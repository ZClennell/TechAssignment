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
		////*****Hardcoded values
		private const int cLevelWidth = 30;
		private const int cLevelHeight = 17;
		private const int cViewWidth = 960;
		private const int cViewHeight = 544;
		private const int cTileSize = 32;
		
		
		private enum AnimState { Still, Left, Right };
		private enum ActionState { Ground, Jump, Fall };
		
		private ActionState actState;
		private AnimState aniState;
		
		private bool canMoveLeft;
		private bool canMoveRight;
		private bool canJump;
		
		
		private float ySpeed;
		private float xSpeed;
		
		private float walkSpeed;
		
		private float jumpHeight;
		private float jumpSpeed;
		private float gravity;
		private float lastGroundPos;
		
		
		
		public Player ()
		{
			texInfo = new TextureInfo ("/Application/textures/SpacebroSide.png");
			sprite = new SpriteUV (texInfo);
			
			sprite.Quad.S = texInfo.TextureSizef;
			sprite.Position = new Vector2 (64.0f,64.0f);
			
			aniState = AnimState.Still;
			actState = ActionState.Fall;
			
			canMoveLeft = true;
			canMoveRight = true;
			canJump = true;
			
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
			CheckCollisions(_levelData);
			GetInput ();
			CheckBoundaries();
			CheckStates ();
			
			sprite.Position = new Vector2(sprite.Position.X + xSpeed, sprite.Position.Y + ySpeed);
		}
		
		private void CheckCollisions(int[,] _levelData)
		{
			bool [] colPoint = CollisionHandler.DetailedCollision(_levelData, sprite);
			
			//Ground Collision
			if(colPoint[0])
			{
				actState = ActionState.Ground;
			}
			else
			{
				if(actState != ActionState.Jump)
				{
					actState = ActionState.Fall;
				}
			}
			
			//Right Collision
			if(colPoint[1])
			{
				canMoveRight = false;
				//float newPos = FMath.Floor((sprite.Position.X/ cViewWidth)	*cLevelWidth)	*cTileSize - 1;
				//sprite.Position = new Vector2(newPos, sprite.Position.Y);
			}
			else
			{	
				canMoveRight = true;
			}
			
			//Left Collision
			if(colPoint[2])
			{
				canMoveLeft = false;
				//float newPos = FMath.Floor((sprite.Position.X/ cViewWidth)	*cLevelWidth)	*cTileSize + cTileSize;
				//sprite.Position = new Vector2(newPos, sprite.Position.Y);
			}
			else
			{
				canMoveLeft = true;
			}
			
			//Top Collision
			if(colPoint[3])
			{
				float newPos = (FMath.Floor((sprite.Position.Y/ cViewHeight) *cLevelHeight))*cTileSize;
				sprite.Position = new Vector2(sprite.Position.X, newPos);
				ySpeed = 0;
				actState = ActionState.Fall;
			}
		}
		
		private void CheckStates()
		{
			switch(aniState)
			{
			case AnimState.Still:
				break;
				
			case AnimState.Right:
				sprite.FlipU = false;
				break;
				
			case AnimState.Left:
				sprite.FlipU = true;
				break;
			}
			
			switch(actState)
			{
				
			case ActionState.Ground:
				OnGround ();
				lastGroundPos = sprite.Position.Y;
				break;
			
			case ActionState.Jump:
				Jump();
				break;
			
			case ActionState.Fall:
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
				xSpeed = 0.0f;
				sprite.Position = new Vector2(0.0f, sprite.Position.Y);
			}
			
			if(sprite.Position.Y < 0.0f)
			{
				actState = ActionState.Ground;
				ySpeed = 0.0f;
				sprite.Position = new Vector2(sprite.Position.X, 0.0f);
			}
		}
		
		private void GetInput()
		{
			aniState = AnimState.Still;
			xSpeed = 0.0f;
			
			if(InputHandler.KeyPressed (InputHandler.Key.Right) && canMoveRight)
			{
				WalkRight();
				aniState = AnimState.Right;
			}
			
			if(InputHandler.KeyPressed (InputHandler.Key.Left) && canMoveLeft)
			{
				WalkLeft();
				aniState = AnimState.Left;
			}
			
			if(InputHandler.KeyPressed (InputHandler.Key.Up) && canJump)
			{
				canJump = false;
				actState = ActionState.Jump;
			}
		}

		////Abilities
		
		private void OnGround()
		{
			float newPos = (FMath.Floor( ((sprite.Position.Y-1) / cViewHeight)	*cLevelHeight))*32+32;
			sprite.Position = new Vector2(sprite.Position.X, newPos);
			actState = ActionState.Ground;
			ySpeed = 0.0f;
			canJump = true;
		}
		
		private void Jump()
		{
			if(sprite.Position.Y < lastGroundPos + jumpHeight)
			{
				ySpeed += jumpSpeed;
			}
			else
			{
				actState = ActionState.Fall;
			}
		}
		
		private void Fall()
		{
			canJump = false;
			ySpeed -= gravity;
		}
		
		private void WalkLeft()
		{
			xSpeed = 0-walkSpeed;
		}
		
		private void WalkRight()
		{
			xSpeed = walkSpeed;
		}
	}
}

