using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO_Monster
{
    internal class Entity
    {
        public string Type { get; private set; }
        public int HP { get; private set; }
        public int MP { get; private set; }
        public int AP { get; private set; }
        public int DEF { get; private set; }
        public bool Dead { get; private set; }

        public Entity(string type, int hp, int mp, int ap, int def) 
        {
            Type = type;
            HP = hp;
            MP = mp;
            AP = ap; 
            DEF = def;
            Dead = false;


        }

        public void Kill()
        {
            Dead = true;
        }
        public override string ToString()
        {
            return $"{Type} {HP} {MP} {AP} {DEF}";
        }
    }
}
