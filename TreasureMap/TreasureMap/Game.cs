using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap
{
    class Game
    {
        private Map map;
        private File file;
        private Utils utils;
        private int maxMvt;

        public Game()
        {
            utils = new Utils();
        }

        public void start(Map p_map, File p_file)
        {
            map = p_map;
            file = p_file;
            Console.WriteLine("Press enter to start the game");
            Console.ReadLine(); //to wait

            map.drawMap();
            getMaxMvt();
            int j = 0;
            for (int i = 0; i < maxMvt; i++)
            {
                while (j < map.NbHero)
                {
                    if (map.Hero[j].Done == true)
                        j++;
                    switch (map.Hero[j].Movement[i])
                    {
                        case 'A':
                            preMove(map.Hero[j]);
                            break;
                        case 'G':
                            changeOrientation("G", map.Hero[j]);
                            break;
                        case 'D':
                            changeOrientation("D", map.Hero[j]);
                            break;
                    }
                    if (map.Hero[j].MaxMovement == i)
                        map.Hero[j].Done = true;
                    j++;
                    
                }
                j = 0;
                map.drawMap();
            }
            end();
        }

        // Here you set the max movement the game will have to do. Since all adventurers can have different movements
        public void getMaxMvt()
        {
            int i = 0;
            for (int j = 0; j < map.NbHero; j++)
            {
                i = utils.countMvt(map.Hero[j].Movement);
                if (maxMvt < i)
                    maxMvt = i;
                i = 0;
            }
        }

        // Here you check which way the hero will move (North, East, South, Ouest) and set the movement
        public void preMove(Adventurer adv)
        {
            switch (map.Hero[adv.Id].Orientation)
            {
                case 1: // North
                    move(map.Hero[adv.Id].PosY, map.Hero[adv.Id].PosX, map.Hero[adv.Id].PosY- 1, map.Hero[adv.Id].PosX, adv);
                    break;
                case 2: // East
                    move(map.Hero[adv.Id].PosY, map.Hero[adv.Id].PosX, map.Hero[adv.Id].PosY, map.Hero[adv.Id].PosX +1, adv);
                    break;
                case 3: // South
                    move(map.Hero[adv.Id].PosY, map.Hero[adv.Id].PosX, map.Hero[adv.Id].PosY + 1, map.Hero[adv.Id].PosX, adv);
                    break;
                case 4: // West
                    move(map.Hero[adv.Id].PosY, map.Hero[adv.Id].PosX, map.Hero[adv.Id].PosY, map.Hero[adv.Id].PosX -1, adv);
                    break;
            }
        }
        
        // Move the hero and set the map with new info, also pick treasure if the hero move to a treasure
        public void move(int oldPosY, int oldPosX, int newPosY, int newPosX, Adventurer adv)
        {
            if (newPosX >= map.Matrice.GetLength(1) || newPosY >= map.Matrice.GetLength(0) || newPosY < 0 || newPosX < 0)
                return;
            if (map.Matrice[newPosY, newPosX].IsTraversable == true)
            {
                map.Matrice[oldPosY, oldPosX].IsTraversable = true;
                if (map.Matrice[oldPosY, oldPosX].Treasure <= 0)
                    map.Matrice[oldPosY, oldPosX].Draw = ".";
                else
                    map.Matrice[oldPosY, oldPosX].Draw = "T(" + map.Matrice[oldPosY, oldPosX].Treasure + ")";
                map.Hero[adv.Id].PosX = newPosX;
                map.Hero[adv.Id].PosY = newPosY;
                map.Matrice[newPosY, newPosX].IsTraversable = false;
                map.Matrice[newPosY, newPosX].Draw = "A(" + utils.adjustNameLenght(map.Hero[adv.Id].Name) + ")";
                if (map.Matrice[newPosY, newPosX].Treasure > 0)
                {
                    map.Hero[adv.Id].Treasure += 1;
                    map.Matrice[newPosY, newPosX].Treasure -= 1;
                }
            }
            else
                return;
        }

        public void changeOrientation(string newOrientation, Adventurer adv)
        {
            if (newOrientation == "G")
            {
                if (map.Hero[adv.Id].Orientation == 1)
                    map.Hero[adv.Id].Orientation = 4;
                else
                    map.Hero[adv.Id].Orientation -= 1;
            }
            if (newOrientation == "D")
            {
                if (map.Hero[adv.Id].Orientation == 4)
                    map.Hero[adv.Id].Orientation = 1;
                else
                    map.Hero[adv.Id].Orientation += 1;
            }
        }

        public void end()
        {
            Console.WriteLine("Congrats, game is done !\n");
            file.resultToFile(map);
        }
    }
}
