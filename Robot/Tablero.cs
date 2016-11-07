using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    class Tablero
    {
        private int length;
        private int[,] tablero;

        public int Length {
            get {
                return length;
            }

            set {
                length = value;
            }
        }

        public int getCell(int x, int y){
            return tablero[x, y];
        }

        public Tablero(int max = 18)
        {
            Length = max;
            tablero = new int[max, max];
            for (int i = 0; i < Length; i ++){
                for (int j = 0; j < Length; j++){
                    tablero[i, j] = 0;
                }
            }
        }

        public void clickInTablero(int x, int y, int type) {
            if (type == 0) {
                tablero[x, y] = type;
            } else if (type == 1) {
                if (tablero[x, y] == 1) {
                    tablero[x, y] = 0;
                } else{
                    tablero[x, y] = 1;
                }
            } else if (type == 2) {
                if (tablero[x, y] == 2) {
                    tablero[x, y] = 0;
                } else {
                    tablero[x, y] = 2;
                }
            }
        }

        public void setCell(int x, int y, int type) {
            tablero[x, y] = type;
        }

    }
}
