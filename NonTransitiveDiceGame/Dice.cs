using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonTransitiveDiceGame
{
    public record Dice(IReadOnlyList<int> Faces)
    {
        public override string ToString()
        {
            return $"[{string.Join(",", Faces)}]";
        }
    }
}
