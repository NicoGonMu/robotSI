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

        public void clickInTablero(int x, int y, bool robot){
            if (tablero[x, y] == 0 & !robot){
                addWall(x, y);
            }else if (tablero[x, y] == 0){
                addRobot(x, y);
            }else{
                removeCell(x, y);
            }
        }

        private void addWall(int x, int y){
            tablero[x, y] = 1;
        }

        private void addRobot(int x, int y){
            tablero[x, y] = 2;
        }

        private void removeCell(int x, int y){
            tablero[x, y] = 0;
        }

    }
}
