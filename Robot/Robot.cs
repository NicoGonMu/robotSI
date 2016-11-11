using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaRobot
{
    class Robot
    {
        public enum coord { N, E, S, W }
        int[] sensors = new int[4];
        int[] memoria = new int[4];
        int x;
        int y;

        // Indica la orientacio del N del robot respecte al tauler
        public coord direccio;
        public coord direccioAnt;

        public Robot(int x, int y, ref Tablero t)
        {
            this.x = x;
            this.y = y;
            direccio = coord.N;

            sensors[(int)coord.N] = t.getCell(x, y - 1);
            sensors[(int)coord.E] = t.getCell(x + 1, y);
            sensors[(int)coord.S] = t.getCell(x, y + 1);
            sensors[(int)coord.W] = t.getCell(x - 1, y);

            var coords = Enum.GetValues(typeof(coord));
            foreach (coord c in coords)
            {
                memoria[(int)c] = 0;
            }

        }

        public int X
        {
            get { return x; }
            set { }
        }

        public int Y
        {
            get { return y; }
            set { }
        }

        public int[] Sensors
        {
            get { return sensors; }
            set { }
        }

        public int[] Memoria
        {
            get { return memoria; }
            set { }
        }



        public void move(Tablero t)
        {

            //Actualitzacio dels sensors 
            updateSensors(t);

            //Obtencio de les coordenades relatives al robot
            int W = ((int)direccio + 3) % 4;
            int N = (int)direccio;

            if (sensors[N] != 0 && (sensors[W] != 0 || memoria[W] == 0))
            {
                direccio = (coord)(((int)direccio + 1) % 4);
            }
            else if (sensors[W] == 0 && memoria[W] != 0)
            {
                direccio = (coord)(((int)direccio + 3) % 4);
            }
            else
            {
                //Avançam
                t.setCell(x, y, 0);
                calculaAvance(ref x, ref y);
                t.setCell(x, y, 2);
            }
            //Actualitzacio dels sensors i la memoria
            update(t);
        }

        private void updateSensors(Tablero t) {
            sensors[(int)coord.N] = t.getCell(x, y - 1);
            sensors[(int)coord.E] = t.getCell(x + 1, y);
            sensors[(int)coord.S] = t.getCell(x, y + 1);
            sensors[(int)coord.W] = t.getCell(x - 1, y);
        }

        private void update(Tablero t)
        {
            var coords = Enum.GetValues(typeof(coord));
            direccioAnt = direccio;
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

        private void calculaAvance(ref int x, ref int y)
        {
            switch (direccio)
            {
                case coord.N:
                    y -= 1;
                    break;
                case coord.E:
                    x += 1;
                    break;
                case coord.S:
                    y += 1;
                    break;
                case coord.W:
                    x -= 1;
                    break;
            }
        }

        public override string ToString()
        {
            string sensorsActius = "";
            var coords = Enum.GetValues(typeof(coord));
            foreach (coord c in coords)
            {
                if (sensors[(int)c] != 0)
                {
                    sensorsActius += c.ToString() + " ";
                }
            }


            string memoriaAciva = "";
            foreach (coord c in coords)
            {
                if (sensors[(int)c] != 0)
                {
                    memoriaAciva += c.ToString() + " ";
                }
            }

            return string.Format("Robot en posicio ({0}, {1}): \n\tDireccio: {2} \n\tSensors actius: {3} \n\tSensors actius memoria: {4}. \n\n", x, y, direccio.ToString(), sensorsActius, memoriaAciva);
        }
    }
}
