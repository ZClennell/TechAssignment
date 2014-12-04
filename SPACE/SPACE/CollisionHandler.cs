using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;

namespace SPACE
{
	public static class CollisionHandler
	{
		public static bool PointCollision(int[,] _levelData, Vector2 _point)
		{
			int levelHeight = 17;
			int levelWidth = 30;
			int viewHeight = 544;
			int viewWidth = 960;
			//int tileSize = 32;
			
			//Find the tile closest to the point
			int tileX = (int)FMath.Floor( (_point.X / viewHeight)	*levelHeight);
			int tileY = (int)FMath.Floor( (_point.Y / viewWidth)	*levelWidth);
			
			tileX = LimitToRange(tileX, 0, levelWidth);
			tileY = LimitToRange(tileY, 0, levelHeight);
			
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
		
		
		
	}
}

