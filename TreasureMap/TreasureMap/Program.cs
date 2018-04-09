using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = null;
            Map map = new Map();
            Game game = new Game();
            File file = new File();

            if (args.Length > 0)
                path = args[0];
            else
            {
                Console.WriteLine("The program must be launch with the path of the file.\nTreasureMap.exe your_path\nPress Enter to exit.");
                Console.ReadLine();
                Environment.Exit(-1);
            }
            map.createMap(file, path);
            game.start(map, file);
        }

    }
}
