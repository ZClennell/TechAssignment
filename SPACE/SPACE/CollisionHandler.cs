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
		//private static int cTileSize = 32;
		
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
		
		public static bool BoxCollision(SpriteUV _sprite1, SpriteUV _sprite2)
		{
			//Box1
			Bounds2 b1 = _sprite1.Quad.Bounds2();
			float width = b1.Point10.X;
			float height = b1.Point01.Y;
			
			Vector2 sprite1p1 = new Vector2(_sprite1.Position.X, 
			                                _sprite1.Position.Y);
			
			Vector2 sprite1p2 = new Vector2(_sprite1.Position.X + width, 
			                               	_sprite1.Position.Y + height);
			
			//Box 2
			Bounds2 b2 = _sprite2.Quad.Bounds2();
			width = b2.Point10.X;
			height = b2.Point01.Y;
			
			Vector2 sprite2p1 = new Vector2(_sprite2.Position.X, 
			                                _sprite2.Position.Y);
			
			Vector2 sprite2p2 = new Vector2(_sprite2.Position.X + width, 
			                               	_sprite2.Position.Y + height);
			
			if(	!(	sprite1p1.X > sprite2p2.X	|| sprite1p2.X < sprite2p1.X	||
			   		sprite1p1.Y > sprite2p2.Y	|| sprite1p2.Y < sprite2p1.Y	))
			{
				return true;
			}
			
			return false;
		}
		
		public static bool LineCollision(int[,] _levelData, Vector2 _point1, Vector2 _point2)
		{
			if(PointCollision (_levelData, _point1))
			{
				return true;
			}
			else 
			if (PointCollision (_levelData, _point2))
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
		
		
	}
}

