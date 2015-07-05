using System.Collections.Generic;

namespace MonoGame.MultiCompileEffects
{
    public class MultiCompileEffect
    {
        private readonly Dictionary<string, byte[]> _variants = new Dictionary<string, byte[]>();

        public Dictionary<string, byte[]> Variants
        {
            get { return _variants; }
        }

        public static string BuildKey(string[] defines)
        {
            return string.Join(",", defines);
        }

        public void AddVariant(string defines, byte[] effectCode)
        {
            _variants[defines] = effectCode;
        }

        public byte[] GetVariant(string defines)
        {
            byte[] result;
            _variants.TryGetValue(defines, out result);

            return result;
        }
    }
}
