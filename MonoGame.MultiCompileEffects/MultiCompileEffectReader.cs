using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.MultiCompileEffects
{
	public class MultiCompileEffectReader : ContentTypeReader<MultiCompileEffect>
	{
		protected override MultiCompileEffect Read(ContentReader input, MultiCompileEffect existingInstance)
		{
			var graphicsDeviceService = input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
			if (graphicsDeviceService == null)
			{
				throw new InvalidOperationException("No Graphics Device Service");
			}

			return MultiCompileEffect.CreateFromReader(graphicsDeviceService.GraphicsDevice, input);
		}
	}
}
