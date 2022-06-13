using System.Collections.ObjectModel;
using System.Data;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace LatticeViz.Model
{
    public class Lattice
    {
        public ObservableCollection<LatticePoint> lattice = new ObservableCollection<LatticePoint>();
        public LatticePoint baseX { get; set; }
        public LatticePoint baseY { get; set; }

        public Lattice(LatticePoint x, LatticePoint y)
        {
            this.baseX = x;
            this.baseY = y;
        }

        public ObservableCollection<LatticePoint> ReturnLatticePoints()
        {
            for (int i = -30; i <= 30; i++)
            {
                for (int j = -30; j <= 30; j++)
                {
                    lattice.Add(new LatticePoint(j*baseX.x+i*baseY.x,j*baseX.y+i*baseY.y));
                }
            }
            return lattice;
        }

        public void BasisReductionCheck()
        {
            if (!((baseX.x == 0 && baseX.y == 0) || (baseY.x == 0 && baseY.y == 0)))
            {
                int k1 = (int)Math.Round(((double)baseX.x * baseY.x + baseX.y * baseY.y) /
                                         (baseX.x * baseX.x + baseX.y * baseX.y),MidpointRounding.AwayFromZero);
                int k2 = (int)Math.Round(((double)baseX.x * baseY.x + baseX.y * baseY.y) /
                                         (baseY.x * baseY.x + baseY.y * baseY.y),MidpointRounding.ToZero);
                Console.WriteLine("--------");
                Console.WriteLine(k1);
                Console.WriteLine(k2);
                Console.WriteLine("----X----");
                Console.WriteLine(baseX.x);
                Console.WriteLine(baseX.y);
                Console.WriteLine("----Y----");
                Console.WriteLine(baseY.x);
                Console.WriteLine(baseY.y);
                if (k2 != 0 && Math.Abs(k2) >= Math.Abs(k1))
                {
                    baseX.x = baseX.x - k2 * baseY.x;
                    baseX.y = baseX.y - k2 * baseY.y;
                    if (!(baseX.x == 0 && baseX.y == 0))
                        BasisReductionCheck();
                }
                else if (k1 != 0 && Math.Abs(k1) >= Math.Abs(k2))
                {
                    baseY.x = baseY.x - k1 * baseX.x;
                    baseY.y = baseY.y - k1 * baseX.y;
                    if (!(baseY.x == 0 && baseY.y == 0))
                        BasisReductionCheck();
                }
            }
        }
    }
}