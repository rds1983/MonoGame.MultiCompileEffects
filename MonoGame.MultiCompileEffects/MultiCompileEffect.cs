using System.Collections.Generic;
using System.Linq;

namespace MonoGame.MultiCompileEffects
{
    public class MultiCompileEffect
    {
        public const string DefineSeparator = ";";

        private readonly Dictionary<string, byte[]> _allEffectCodes = new Dictionary<string, byte[]>();

        public string DefaultVariantKey { get; internal set; }

        public IEnumerable<string> AllKeys
        {
            get { return _allEffectCodes.Keys; }
        }

        public static string BuildKey(string[] defines)
        {
            return string.Join(DefineSeparator,
                (from d in defines where !string.IsNullOrEmpty(d.Trim()) orderby d select d.ToUpper()));
        }

        internal void AddEffectCode(string defines, byte[] effectCode)
        {
            _allEffectCodes[defines] = effectCode;
        }

        private byte[] InternalGetEffectCode(string key)
        {
            byte[] result;
            _allEffectCodes.TryGetValue(key, out result);

            return result;
        }

        public byte[] GetEffectCode(string[] defines)
        {
            var key = BuildKey(defines);
            return InternalGetEffectCode(key);
        }

        public byte[] GetDefaultEffectCode()
        {
            return InternalGetEffectCode(DefaultVariantKey);
        }
    }
}
