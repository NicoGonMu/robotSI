using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    class Robot
    {
        private enum coord { N, E, S, W }
        int[] sensors = new int[4];
        int[] memoria = new int[4];
        int x;
        int y;

        // Indica la orientacio del N del robot respecte al tauler
        coord direccio;

        public Robot(int x, int y, Tablero t)
        {
            this.x = x;
            this.y = y;
            direccio = coord.N;

            sensors[(int)coord.N] = t.getCell(x , y - 1);
            sensors[(int)coord.E] = t.getCell(x + 1, y);
            sensors[(int)coord.S] = t.getCell(x, y + 1);
            sensors[(int)coord.W] = t.getCell(x - 1, y);

            var coords = Enum.GetValues(typeof(coord));
            foreach (coord c in coords)
            {
                memoria[(int)c] = 0;
            }
        }

        public void move(Tablero t)
        {
            //Actualitzacio dels sensors i la memoria
            update(t);

            //Obtencio de les coordenades relatives al robot
            int W = ((int)direccio + 3) % 4;
            int N = (int)direccio;

            if (sensors[W] != 0) 
            {
                if(sensors[N] == 0)
                {
                    //Avanzar
                } else
                {
                    //Rotar hacia el este
                }
            } else
            {
                if (memoria[W] == 0 && sensors[N] == 0)
                {
                    //Avanzar
                } else
                {
                    //Rotar hacia el oeste
                }
            }
        }

        private void update(Tablero t)
        {
            var coords = Enum.GetValues(typeof(coord));
            foreach (coord c in coords)
            {
                int i = (int)c;
                memoria[i] = sensors[i];
            }

            sensors[(int)coord.N] = t.getCell(x, y - 1);
            sensors[(int)coord.E] = t.getCell(x + 1, y);
            sensors[(int)coord.S] = t.getCell(x, y + 1);
            sensors[(int)coord.W] = t.getCell(x - 1, y);            
        }
    }
}
