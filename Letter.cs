using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt
{
    class Letter
    {
        private int num;
        private char nom;
        public Letter(int n, char c)
        {
            num = n;
            nom = c;
        }

        public int Num
        {
            get
            {
                return num;
            }
            set
            {
                num = value;
            }
        }

        public char Nom
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
            }
        }
    }
}
