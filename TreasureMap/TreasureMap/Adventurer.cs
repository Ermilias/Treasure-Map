using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap
{
    class Adventurer
    {
        private bool done;
        private int id;
        private int posX;
        private int posY;
        private int treasure;
        private int maxMovement;
        private string name;
        private string movement;
        private int orientation;
        private Utils utils;
        private IDictionary<string, int> dict;

        public Adventurer(int p_id, int p_posX, int p_posY, string p_name, string p_movement, string p_orientation)
        {
            utils = new Utils();

            //Change the orientation to int for an easier usage
            dict = new Dictionary<string, int>();
            dict["N"] = 1;
            dict["E"] = 2;
            dict["S"] = 3;
            dict["O"] = 4;
            foreach (KeyValuePair<string, int> kvp in dict)
            {
                if (kvp.Key == p_orientation)
                    orientation = kvp.Value;
            }

            done = false;
            id = p_id;
            posX = p_posX;
            posY = p_posY;
            name = p_name;
            movement = p_movement;
            maxMovement = utils.countMvt(movement) - 1;
        }

        public string revertOrientation(int o)
        {
            string orientation = null;
            foreach (KeyValuePair<string, int> kvp in dict)
            {
                if (kvp.Value == o)
                    orientation = kvp.Key;
            }
            return orientation;
        }

        public bool Done
        {
            get { return done; }
            set { done = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }
        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }
        public int Treasure
        {
            get { return treasure; }
            set { treasure = value; }
        }
        public int MaxMovement
        {
            get { return maxMovement; }
            set { maxMovement = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Movement
        {
            get { return movement; }
            set { movement = value; }
        }
        public int Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }
    }
}
