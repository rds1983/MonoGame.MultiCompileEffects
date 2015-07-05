using Microsoft.Xna.Framework.Content.Pipeline;

namespace MonoGame.MultiCompileEffects.Content.Pipeline
{
    public class MultiCompileEffectContent: ContentItem
    {
        private readonly MultiCompileEffect _multiCompileEffect = new MultiCompileEffect();

        public MultiCompileEffect MultiCompileEffect
        {
            get { return _multiCompileEffect; }
        }
    }
}
