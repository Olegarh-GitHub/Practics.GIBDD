using System.Collections.Generic;

namespace Practics.GIBDD.VIN.Resources
{
    public static class VINWeights
    {
        public static readonly Dictionary<char, int> WEIGHTS = new Dictionary<char, int>()
        {
            {'A', 1},
            {'B', 2},
            {'C', 3},
            {'D', 4},
            {'E', 5},
            {'F', 6},
            {'G', 7},
            {'H', 8},
            {'J', 1},
            {'K', 2},
            {'L', 3},
            {'M', 4},
            {'N', 5},
            {'P', 7},
            {'R', 9},
            {'S', 2},
            {'T', 3},
            {'U', 4},
            {'V', 5},
            {'W', 6},
            {'X', 7},
            {'Y', 8},
            {'Z', 9}
        };
    }
}