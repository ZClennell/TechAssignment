using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace SPACE
{
	public class BackgroundLoader
	{	
		//Private variables.
		private SpriteUV[] 	sprites;
		private TextureInfo	textureInfo;
		
		//Public functions.
		public BackgroundLoader (Scene scene)
		{
			sprites	= new SpriteUV[1];
			
			textureInfo = new TextureInfo("/Application/textures/Background.jpg");
		}	
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update()
		{			
		}
	}
}

