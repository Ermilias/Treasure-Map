using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TreasureMap
{
    class Map
    {
        private Utils utils;
        private File file;
        private Adventurer[] hero;
        private Tile[,] map;
        private int mapX;
        private int mapY;
        private int nbHero;

        public Map()
        {
            utils = new Utils();
        }

        public void createMap(File p_file, string path)
        {
            file = p_file;
            file.readFile(path);

            hero = new Adventurer[file.LineNb];
            createBaseMap();

            //fill the map
            for (int i = 0; i < file.LineNb; i++)
            {
                string[] mapInfo = utils.splitString(file.FileInfo[i]);
                if (file.FileInfo[i][0] == 'M')
                    fillMontains(mapInfo);
                if (file.FileInfo[i][0] == 'T')
                    fillTreasure(mapInfo);
                if (file.FileInfo[i][0] == 'A')
                    setAdventurers(mapInfo);
            }
            drawMap();
        }

        public void createBaseMap()
        {
            bool hasMap = false;
            for (int i = 0; i < file.LineNb; i++)
            {
                if (file.FileInfo[i][0] == 'C')
                {
                    string[] mapInfo = utils.splitString(file.FileInfo[i]);
                    mapX = int.Parse(mapInfo[1]);
                    mapY = int.Parse(mapInfo[2]);

                    map = new Tile[mapY, mapX];
                    for (int x = 0; x < mapX; ++x)
                    {
                           for (int y = 0; y < mapY; ++y)
                           {
                              map[y, x] = new Tile();
                           }
                    }
                    hasMap = true;
                }
            }
            if (hasMap == false)
            {
                Console.WriteLine("Your file doesn't include a map.\nYou need a line like this in your file to start the game : C-X-Y\nPress Enter to exit");
                Console.ReadLine();
                Environment.Exit(-1);
            }       
        }

        public void fillMontains(string[] mapInfo)
        {
            if (mapInfo.Length == 3)
            {
                int posX = int.Parse(mapInfo[1]);
                int posY = int.Parse(mapInfo[2]);
                map[posY, posX].Draw = "M";
                map[posY, posX].IsTraversable = false;
            }
            else
            {
                Console.WriteLine("Montains has to follow this model (3 input) : M-X-Y\nLine ignored.");
                return;
            }
        }

        public void fillTreasure(string[] mapInfo)
        {     
            if (mapInfo.Length == 4)
            {
                int posX = int.Parse(mapInfo[1]);
                int posY = int.Parse(mapInfo[2]);
                map[posY, posX].Treasure = int.Parse(mapInfo[3]);
                map[posY, posX].Draw = "T(" + map[posY, posX].Treasure + ")";
            }
            else
            {
                Console.WriteLine("Treasure has to follow this model (4 inputs) : T-X-Y-nb treasure\nLine ignored.");
                return;
            }
        }

        public void setAdventurers(string[] mapInfo)
        {
            if (mapInfo.Length == 6)
            {
                int posX = int.Parse(mapInfo[2]);
                int posY = int.Parse(mapInfo[3]);
                if (posX >= map.GetLength(1) || posY >= map.GetLength(0) || posY < 0 || posX < 0)
                {
                    Console.WriteLine("Your Adventurer is out of the map.\nThe new Hero "+mapInfo[1]+" was not created.");
                    return;
                }
                    if (map[posY, posX].IsTraversable == true)
                {
                    hero[nbHero] = new Adventurer(nbHero, posX, posY, mapInfo[1], mapInfo[5], mapInfo[4]);
                    map[posY, posX].Draw = "A(" + utils.adjustNameLenght(hero[nbHero].Name) + ")";
                    map[posY, posX].IsTraversable = false;
                    nbHero++;
                }
                else
                {
                    Console.WriteLine("Your Hero can't spwan on another hero or mountain.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Adventurers has to follow this model (6 inputs) : A-Name-X-Y-Orientation-Movement Sequence\nLine ignored.");
                return;
            }
            
        }

        public void drawMap()
        {
            for (int i = 0; i < mapY; i++)
            {
                for (int j = 0; j < mapX; j++)
                    Console.Write(map[i, j].Draw + "\t");
                Console.Write("\n");
            }
            Console.Write("\n");
        }
        
        public Adventurer[] Hero
        {
            get { return hero; }
            set { hero = value; }
        }
        public Tile[,] Matrice
        {
            get { return map; }
            set { map = value; }
        }
        public int MapX
        {
            get { return mapX; }
            set { mapX = value; }
        }
        public int MapY
        {
            get { return mapY; }
            set { mapY = value; }
        }
        public int NbHero
        {
            get { return nbHero; }
            set { nbHero = value; }
        }
    }
}
