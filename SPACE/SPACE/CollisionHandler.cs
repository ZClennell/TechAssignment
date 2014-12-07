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
	public static class CollisionHandler
	{
		private static int cLevelHeight = 17;
		private static int cLevelWidth = 30;
		private static int cViewHeight = 544;
		private static int cViewWidth = 960;
		private static int cTileSize = 32;
		
		public static bool PointCollision(int[,] _levelData, Vector2 _point)
		{
			//Find the tile closest to the point
			int tileX = (int)FMath.Floor( (_point.X / cViewHeight)	*cLevelHeight);
			int tileY = (int)FMath.Floor( (_point.Y / cViewWidth)	*cLevelWidth);
			
			tileX = LimitToRange(tileX, 0, cLevelWidth);
			tileY = LimitToRange(tileY, 0, cLevelHeight);
			
			if(_levelData[tileY,tileX] == 1)
			{
				return true;
			}
			
			return false;
			
		}
		
		private static int LimitToRange(int _value, int _min, int _max)
    	{
	        if (_value < _min) { return _min; }
	        if (_value > _max) { return _max; }
	        return _value;
    	}
		
		public static bool[] DetailedCollision(int[,] _levelData, SpriteUV _sprite)
		{
			Vector2 bottomPoint = 	new Vector2(_sprite.Position.X + (cTileSize/2), _sprite.Position.Y - 1);
			Vector2 topPoint = 		new Vector2(_sprite.Position.X + (cTileSize/2), _sprite.Position.Y+cTileSize);
			Vector2 leftPoint =		new Vector2(_sprite.Position.X				, 	_sprite.Position.Y+(cTileSize/2));
			Vector2 rightPoint = 	new Vector2(_sprite.Position.X + cTileSize	, 	_sprite.Position.Y+(cTileSize/2));
			
			bool[] colPoint = new bool[4];
			
			for(int a = 0; a < colPoint.Length; a++)
			{
				colPoint[a] = false;
			}
			
			//Ground Collision
			if(CollisionHandler.PointCollision(_levelData, bottomPoint))
			{
				colPoint[0] = true;
			}
			
			//Right Collision
			if(CollisionHandler.PointCollision(_levelData, rightPoint))
			{
				colPoint[1] = true;
			}
			
			//Left Collision
			if(CollisionHandler.PointCollision(_levelData, leftPoint))
			{
				colPoint[2] = true;
			}
			
			//Top Collision
			if(CollisionHandler.PointCollision(_levelData, topPoint))
			{
				colPoint[3] = true;
			}
			
			return colPoint;
		}
		
	}
}

