using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap
{
    class File
    {
        private StreamWriter resultFile;
        private string[] fileInfo;
        private int lineNb;
        private Map map;
        private Utils utils;

        public File()
        {
            utils = new Utils();
        }

        public void readFile(string path)
        {
            string line;

            if (!System.IO.File.Exists(path))
            {
                Console.WriteLine("Error : File \""+ path +"\" doesn't exist.\nPress enter to exit.");
                Console.ReadLine();
                Environment.Exit(-1);
            }

            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null) { if (line != String.Empty) lineNb++; }
            file.Close();

            file = new StreamReader(path);
            fileInfo = new string[lineNb];
            int counter = 0;
            while (file.EndOfStream == false)
            {
                line = file.ReadLine();
                if (line != String.Empty)
                {
                    line = line.Replace(" ", "");
                    fileInfo[counter] = line;
                    counter++;
                }
            }

            file.Close();
        }

        public void resultToFile(Map p_map)
        {
            map = p_map;
            string path = Directory.GetCurrentDirectory() + "\\TreasureMap result.txt";
            Console.WriteLine("You can find the final map and info in this file : " + path);
            resultFile = new StreamWriter(path);

            resultFile.WriteLine("End game info : ");
            fileInfoToResult();
            resultFile.WriteLine();

            resultFile.WriteLine("End map :");
            for (int i = 0; i < map.MapY; i++)
            {
                for (int j = 0; j < map.MapX; j++)
                    resultFile.Write(map.Matrice[i, j].Draw + "\t");
                resultFile.WriteLine();
            }
            resultFile.WriteLine();
            resultFile.Close();

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        public void fileInfoToResult()
        {
            for (int i = 0; i < lineNb; i++)
            {
                switch (fileInfo[i][0])
                {
                    case 'C':
                        resultFile.WriteLine(fileInfo[i]);
                        break;
                    case 'M':
                        resultFile.WriteLine(fileInfo[i]);
                        break;
                    case 'T':
                        if (map.Matrice[(int)Char.GetNumericValue(fileInfo[i][4]), (int)Char.GetNumericValue(fileInfo[i][2])].Treasure > 0)
                            resultFile.WriteLine("{0}-{1}-{2}-{3}", fileInfo[i][0], fileInfo[i][2],
                                fileInfo[i][4], map.Matrice[(int)Char.GetNumericValue(fileInfo[i][4]), 
                                (int)Char.GetNumericValue(fileInfo[i][2])].Treasure);
                        break;
                    case 'A':
                        writeHeroInfo(i);
                        break;
                }
            }
        }

        public void writeHeroInfo(int i)
        {
            string[] heroInfo = utils.splitString(fileInfo[i]);
            for (int j = 0; j < map.NbHero; j++)
                if (heroInfo[1] == map.Hero[j].Name)
                    resultFile.WriteLine("A-{0}-{1}-{2}-{3}-{4}", map.Hero[j].Name, map.Hero[j].PosX, 
                        map.Hero[j].PosY, map.Hero[j].revertOrientation(map.Hero[j].Orientation), map.Hero[j].Treasure);
        }

        public string[] FileInfo
        {
            get { return fileInfo; }
            set { fileInfo = value; }
        }
        public int LineNb
        {
            get { return lineNb; }
            set { lineNb = value; }
        }
    }
}
