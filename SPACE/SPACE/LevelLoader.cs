using System;
using System.IO;
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
	public static class LevelLoader
	{
		private const int cLevelWidth = 30;
		private const int cLevelHeight = 17;
		
		public static int[,] GetLevel(int _levelNum)
		{
			switch(_levelNum)
			{
				case 1:
					return GetLevel1 ();
				
				case 2:
					return GetLevel2 ();
				
				case 3:
					return GetLevel3 ();
				
				default:
					return GetBlankLevel ();
			}
		}
		
		public static int[,] GetLevel1()
		{
			int[,] levelData = new int[,]{
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			  	{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			  	{1,1,1,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,0,0,0,0,1},
				{1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,6,0,0,0,0,0,1,1,1,1},
			 	{1,0,0,0,1,0,6,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1},
				{1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,1,1,0,0,1,1,1,1,1,1,1},
			 	{1,5,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
				{1,1,1,1,1,0,3,0,0,0,0,0,0,0,0,0,6,0,2,0,0,0,0,0,0,0,0,0,0,1},
			  	{1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1},
				{1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,1},
			  	{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
				{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1},
			  	{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,1,1,1,1,1,1},
				{1,0,0,0,0,4,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			};
			
			return FlipLevel(levelData);
		}
		
		public static int[,] GetLevel2()
		{
			int[,] levelData = new int[,]{
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,1},
			  	{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,1,0,5,0,0,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,1},
			  	{1,0,0,1,1,1,0,1,1,1,1,1,1,0,1,0,1,0,1,0,1,1,0,0,0,0,0,6,0,1},
				{1,0,0,0,0,0,0,1,0,1,0,1,2,0,7,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
			 	{1,0,0,0,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,2,0,0,0,0,0,0,1,1},
				{1,1,0,0,0,0,0,0,0,1,0,1,0,2,0,0,0,0,0,0,1,0,1,1,1,1,1,1,1,1},
			  	{1,0,0,0,0,0,0,0,0,1,0,1,1,0,1,0,1,0,1,0,1,0,0,0,0,0,0,1,0,1},
				{1,0,2,0,0,0,0,0,0,1,0,1,2,0,0,0,0,0,0,6,1,0,0,0,0,0,0,0,1,1},
			  	{1,0,0,0,1,0,0,6,0,1,0,1,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,0,1,1,0,1,1,0,3,0,0,0,0,1},
			  	{1,0,0,0,0,0,0,1,1,1,1,1,0,1,1,1,1,1,1,0,0,0,1,1,1,1,0,0,0,1},
				{1,0,0,0,0,0,0,1,0,1,0,1,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			  	{1,0,0,0,4,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
				{1,0,0,0,1,0,0,1,0,1,0,1,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			};
			
			return FlipLevel(levelData);
		}
		
		public static int[,] GetLevel3()
		{
			int[,] levelData = new int[,]{
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			  	{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			  	{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			  	{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			 	{1,0,4,0,0,0,0,3,0,0,0,3,0,0,0,3,0,0,0,3,0,0,0,0,0,0,0,5,0,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			  	{1,0,1,0,0,1,0,0,1,0,0,1,0,0,1,1,0,0,1,0,0,1,0,0,1,0,0,1,0,1},
				{1,1,0,0,1,0,0,0,1,0,0,1,0,0,1,1,0,0,1,0,0,1,0,0,0,1,0,0,1,1},
	  			{1,0,0,1,0,0,0,0,1,0,0,1,0,0,1,1,0,0,1,0,6,1,0,0,0,0,1,0,0,1},
				{1,0,1,0,0,0,0,0,1,0,0,1,0,0,1,1,0,0,1,0,0,1,0,0,0,0,0,1,0,1},
	  			{1,1,0,0,0,0,0,0,1,0,0,1,0,0,1,1,0,0,1,0,0,1,0,0,0,0,0,0,1,1},
				{1,0,0,0,0,0,0,0,1,0,0,1,0,0,1,1,0,0,1,0,0,1,0,0,0,0,0,0,0,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			};
			
			return FlipLevel(levelData);
		}
		
		public static int[,] GetBlankLevel()
		{
			int[,] levelData = new int[,]{
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			  	{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,1,1,1,0,1,1,0,0,1,0,1,1,0,0,0,0,0,0,0,0,1},
			  	{1,0,0,0,0,0,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,1},
			  	{1,0,0,0,0,0,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,1,1,1,0,1,0,0,1,1,0,1,1,0,0,0,0,0,0,0,0,1},
			 	{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			  	{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	  			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
	  			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
				{1,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			};
			
			return FlipLevel(levelData);
		}
		
		private static int[,] FlipLevel(int[,] _levelData)
		{
			int[,] flippedLevel = new int[cLevelHeight,cLevelWidth];
			
			for(int y = 0; y < cLevelHeight; y++)
			{
				for(int x = 0; x < cLevelWidth; x++)
				{
					flippedLevel[y,x] = _levelData[cLevelHeight-y-1,x];
				}
			}
			
			return flippedLevel;
		}
		
		
		private static void ReadLevelFile()
		{
//			String input = File.ReadAllText("/Application/levels/Level2.txt");
//			
//			int i = 0, j = 0;
//			int[,] levelData = new int[cLevelHeight, cLevelWidth];
//			
//			foreach (var row in input.Split('\n'))
//			{
//			    j = 0;
//			    foreach (var col in row.Trim().Split(' '))
//			    {
//			        levelData[i, j] = int.Parse(col.Trim());
//			        j++;
//			    }
//			    i++;
//			}
		}
		
	}
}





