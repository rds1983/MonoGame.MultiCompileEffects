using Microsoft.Xna.Framework.Content;

namespace MonoGame.MultiCompileEffects
{
    public class MultiCompileEffectReader : ContentTypeReader<MultiCompileEffect>
    {
        protected override MultiCompileEffect Read(ContentReader input, MultiCompileEffect existingInstance)
        {
            var count = input.ReadInt32();

            var result = new MultiCompileEffect();

            for (var i = 0; i < count; ++i)
            {
                var key = input.ReadString();

                var effectCodeLength = input.ReadInt32();
                var effectCode = input.ReadBytes(effectCodeLength);

                result.AddVariant(key, effectCode);

                // First variant is default
                if (i == 0)
                {
                    result.DefaultVariantKey = key;
                }
            }

            return result;
        }
    }
}
