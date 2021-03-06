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
	public class ExitTile : Entity
	{

		public ExitTile (float _x, float _y)
		{
			texInfo = new TextureInfo ("/Application/textures/Exit.png");
			sprite = new SpriteUV (texInfo);
			
			sprite.Quad.S = texInfo.TextureSizef;
			sprite.Position = new Vector2 (_x,_y);
		}
		
		override public string ReturnType()
		{
			return "Exit";
		}
		
	}
}

