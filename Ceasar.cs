using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt
{

    class Ceasar
    {
        
        private List<Letter> lLettre = null;
        private char[] t ={'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};

        Ceasar()
        {
            chargeList();
        }
        private void chargeList() {
            lLettre = new List<Letter>(26);
            for (int i = 0; i < 26; i++)
            {
                Letter temp = new Letter(i + 1, t[i]);
                lLettre.Add(temp);
            }
        }
        private void declareList() {
            char tmp = 'a';
            for (int i = 0; i < 26; i++)
            {

                if (i == 0)
                {
                    tmp = lLettre[i].Nom;
                    lLettre[i].Nom = lLettre[i + 1].Nom;
                }
                else
                    if (i < 25)
                        lLettre[i].Nom = lLettre[i + 1].Nom;
                    else
                        lLettre[i].Nom = tmp;
            }
        }
        private void crypt() {
            char c;
            int n = 0;
            Stream fStream1 = new BufferedStream(new FileStream("C:\\test\\fileCCrypt.txt", FileMode.OpenOrCreate));
            BinaryWriter bw = new BinaryWriter(fStream1);
            byte[] buff = File.ReadAllBytes("C:\\test\\ceasarF.txt");
            foreach (byte b in buff) {
                //Console.WriteLine((char)b);
                c = (char)b;
                c = char.ToUpper(c);
                if (c >= 'A' && c <= 'B' || c == ' ')
                {
                    if (c == ' ')
                    {

                        Console.WriteLine(c);
                        //bw.Write(c);
                    }else {
                        Console.WriteLine((char)b);
                        for (int i = 0; i < 26; i++) {
                            if (char.ToLower(c) == t[i]) {
                                n = i;
                                break;
                            }
                            Console.WriteLine(lLettre[n].Nom);
                            //bw.Write(lLettre[n].Nom);
                        }
                    }
                }
            }
            Console.ReadKey();
        }
        static void Main() {
            Ceasar cs = new Ceasar();
            cs.chargeList();
            cs.declareList();
            cs.crypt();
        }
    }
}
