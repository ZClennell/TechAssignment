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
	public class Entity
	{
		protected SpriteUV 		sprite;
		protected TextureInfo 	texInfo;
		
		public SpriteUV Sprite {
			get {
				return this.sprite;
			}
			set {
				sprite = value;
			}
		}
		
		public Entity ()
		{
			sprite = null;
			texInfo = null;
		}
		
		public virtual void AddToScene (Scene _scene)
		{
			if(sprite != null)
			{
				_scene.AddChild(sprite);
			}
		}
		
		public void SetPosition(Vector2 _newPos)
		{
			sprite.Position = _newPos;
		}
		
		public virtual void Update(float _deltaTime, int[,] _levelData){}
		public virtual void Update(float _deltaTime){}
		public virtual string ReturnType(){return " ";}
	}
}

