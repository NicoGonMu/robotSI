using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaRobot
{
    class Coordenada
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordenada()
        {           
        }

        public Coordenada(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(Coordenada obj)
        {
            return (x = obj.x) && (y == obj.y);
        }
    }
}
