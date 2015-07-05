using System.Collections.Generic;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace MonoGame.MultiCompileEffects.Content.Pipeline
{
    public class MultiCompileEffectContent: ContentItem
    {
        private readonly Dictionary<string, byte[]> _variants = new Dictionary<string, byte[]>();

        public Dictionary<string, byte[]> Variants
        {
            get { return _variants; }
        }

        public string BuildKey(string[] defines)
        {
            return string.Join(",", defines);
        }

        public void AddVariant(string[] defines, byte[] effectCode)
        {
            _variants[BuildKey(defines)] = effectCode;
        }
    }
}
