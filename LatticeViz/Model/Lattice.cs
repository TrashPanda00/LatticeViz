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
            for (int i = -60; i <= 60; i++)
            {
                for (int j = -60; j <= 60; j++)
                {
                    lattice.Add(new LatticePoint(j*baseX.x+i*baseY.x,j*baseX.y+i*baseY.y));
                }
            }
            return lattice;
        }

        public void BasisReductionCheck()
        {
            int k1 = (int) Math.Round(((double) baseX.x * baseY.x + baseX.y * baseY.y) / (baseX.x * baseX.x + baseX.y * baseX.y));
            int k2 = (int) Math.Round(((double) baseX.x * baseY.x + baseX.y * baseY.y) / (baseY.x * baseY.x + baseY.y * baseY.y));
            if (k2 > 0)
            {
                baseX.x = baseX.x - k2 * baseY.x;
                baseX.y = baseX.y - k2 * baseY.y;
                BasisReductionCheck();
            }
            else if (k1 > 0)
            {
                baseY.x = baseY.x - k1 * baseX.x;
                baseY.y = baseY.y - k1 * baseX.y;
                BasisReductionCheck();
            }
            else if (k1 == k2)
            {
                //Do nothing
            }
            else
            {
                BasisReductionCheck();
            }
        }
    }
}