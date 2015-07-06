using System.Collections.Generic;
using System.Linq;

namespace MonoGame.MultiCompileEffects
{
    public class MultiCompileEffect
    {
        private readonly Dictionary<string, byte[]> _variants = new Dictionary<string, byte[]>();

        public string DefaultVariantKey { get; internal set; }

        public Dictionary<string, byte[]> Variants
        {
            get { return _variants; }
        }

        public static string BuildKey(string[] defines)
        {
            return string.Join(";", 
                (from d in defines where !string.IsNullOrEmpty(d) select d));
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

		public byte[] GetDefaultVariant()
		{
		    return GetVariant(DefaultVariantKey);
		}
    }
}
