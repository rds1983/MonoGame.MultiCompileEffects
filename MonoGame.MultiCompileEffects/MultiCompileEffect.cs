using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.MultiCompileEffects
{
    public class MultiCompileEffect
    {
        public const string DefineSeparator = ";";

        private readonly Dictionary<string, Effect> _effects = new Dictionary<string, Effect>();

        public string DefaultVariantKey { get; internal set; }

        public IEnumerable<string> AllKeys
        {
            get { return _effects.Keys; }
        }

        public static string BuildKey(string[] defines)
        {
            return string.Join(DefineSeparator,
                (from d in defines where !string.IsNullOrEmpty(d.Trim()) orderby d select d.ToUpper()));
        }

        internal void AddEffect(string defines, Effect effect)
        {
            _effects[defines] = effect;
        }

        private Effect InternalGetEffect(string key)
        {
            Effect result;
            _effects.TryGetValue(key, out result);

            return result;
        }

        public Effect GetEffect(string[] defines)
        {
            var key = BuildKey(defines);
            return InternalGetEffect(key);
        }

        public Effect GetDefaultEffect()
        {
            return InternalGetEffect(DefaultVariantKey);
        }
    }
}
