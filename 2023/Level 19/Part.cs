

namespace Level_19
{
    public class Part
    {
        public Part(string x, string m, string a, string s)
        {
            X = long.Parse(x);
            M = long.Parse(m);
            A = long.Parse(a);
            S = long.Parse(s);
        }

        public long X { get; }

        public long M { get; }

        public long A { get; }

        public long S { get; }

        public long Result => X + M + A + S;

        internal bool CheckCondition(string category, string opera, long checkValue)
        {
            var internalCheckValue = X;
            if (category == "m")
            {
                internalCheckValue = M;
            }
            else if (category == "a")
            {
                internalCheckValue = A;
            }
            else if (category == "s")
            {
                internalCheckValue = S;
            }

            switch (opera)
            {
                case ">": return checkValue < internalCheckValue;
                case "<": return checkValue > internalCheckValue;
            }

            return false;
        }
    }
}
