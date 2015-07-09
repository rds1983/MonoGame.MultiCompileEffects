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


            var count = input.ReadInt32();

            var result = new MultiCompileEffect();

            for (var i = 0; i < count; ++i)
            {
                var key = input.ReadString();

                var effectCodeLength = input.ReadInt32();
                var effectCode = input.ReadBytes(effectCodeLength);

                var effect = new Effect(graphicsDeviceService.GraphicsDevice, effectCode);

                result.AddEffect(key, effect);
            }

            result.DefaultVariantKey = input.ReadString();

             return result;
        }
    }
}
