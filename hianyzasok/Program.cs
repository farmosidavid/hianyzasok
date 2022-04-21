using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hianyzasok
{
    class Program
    {
        static string hetnapja(int honap, int nap)
        {
            string[] napnev = { "vasarnap", "hetfo", "kedd", "szerda", "csutortok", "pentek", "szombat" };
            int[] napszam = { 1, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 335 };
            int napsorszam = (napszam[honap - 1] + nap) % 7;
            return napnev[napsorszam];

        }
        struct Adatok
        {
            public int honap;
            public int nap;
            public string veznev;
            public string kernev;
            public string orak;
            public string teljesnev;
        }

        static void Main(string[] args)
        {
            //1. feladat
            int db = File.ReadAllLines("naplo.txt").Length;
            StreamReader be = new StreamReader("naplo.txt");
            string[] sor = new string[3];
            int hianyzasokszama = 0;
            for (int i = 0; i < db; i++)
            {
                sor = be.ReadLine().Split(' ');
                if (sor[0]!="#")
                {
                    hianyzasokszama++;
                }
            }
            be.Close();
            Adatok[] tomb = new Adatok[hianyzasokszama];
            be = new StreamReader("naplo.txt");
            int honap = 0;
            int nap = 0;
            int sorszam = 0;
            for (int i = 0; i < db; i++)
            {
                sor = be.ReadLine().Split(' ');
                if (sor[0]=="#")
                {
                    honap = Convert.ToInt32(sor[1]);
                    nap = Convert.ToInt32(sor[2]);
                }
                else
                {
                    tomb[sorszam].honap = honap;
                    tomb[sorszam].nap = nap;
                    tomb[sorszam].veznev = sor[0];
                    tomb[sorszam].kernev = sor[1];
                    tomb[sorszam].orak = sor[2];
                    sorszam++;
                }
            }
            
            
            be.Close();

            //2. feladat
            Console.WriteLine("2. feladat");
            Console.WriteLine("A naplóban {0} bejegyzés van.",hianyzasokszama);

            //3. feladat
            Console.WriteLine("3. feladat");
            int igazolt = 0;
            int igazolatlan = 0;
            for (int i = 0; i < hianyzasokszama; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (tomb[i].orak[j] == 'I')
                    {
                        igazolatlan++;
                    }
                    if (tomb[i].orak[j] == 'X')
                    {
                        igazolt++;
                    }
                }
            }
            Console.WriteLine("Az igazolt hiányzások száma {0}, az igazolatlanoké {1} óra.",igazolt,igazolatlan);

            //5. feladat
            Console.WriteLine("5. feladat");
            Console.Write("A hónap sorszáma=");
            int beho = Convert.ToInt32(Console.ReadLine());
            Console.Write("A nap sorszáma=");
            int benap = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Azon a napon {0} volt.", hetnapja(beho,benap));

            //6. feladat
            Console.WriteLine("6. feladat");
            Console.Write("A nap neve=");
            string benap6 = Console.ReadLine();
            Console.Write("Az óra sorszáma=");
            int beszam = Convert.ToInt32(Console.ReadLine())-1;
            int hiany6 = 0;
            for (int i = 0; i < hianyzasokszama; i++)
            {
                    if (hetnapja(tomb[i].honap,tomb[i].nap)==benap6)
                    {
                            if (tomb[i].orak[beszam]=='X'||tomb[i].orak[beszam]=='I')
                            {
                                hiany6++;
                            }
                    }
            }
            Console.WriteLine("Ekkor összesen {0} óra hiányzás történt.",hiany6);

            //7. feladat
            Console.WriteLine("7. feladat");
            int maxhianyzas = 0;
            int hanyadikszemely = 0;
            int szemelyhianyzas = 0;
            for (int i = 0; i < hianyzasokszama; i++)
            {
                tomb[i].teljesnev = tomb[i].veznev + " " + tomb[i].kernev;
            }
            string[] nevek = tomb.Select(a => a.teljesnev).ToArray().Distinct().ToArray();
            int[] hianyzasuk = new int[nevek.Length];
            for (int i = 0; i < nevek.Length; i++)
            {
                Adatok[] tomb7 = Array.FindAll(tomb, a => a.teljesnev == nevek[i]).ToArray();
                for (int j = 0; j < tomb7.Length; j++)
                {
                    for (int m = 0; m < 7; m++)
                    {
                        if (tomb7[j].orak[m]=='X'|| tomb7[j].orak[m] == 'I')
                        {
                            hianyzasuk[i]++;
                        }
                    }
                }
            }
            Console.Write("A legtöbbet hiányzó tanulók: ");
            for (int i = 0; i < hianyzasuk.Length; i++)
            {
                if (hianyzasuk[i]==hianyzasuk.Max())
                {
                    Console.Write("{0} ", nevek[i]);
                }
            }
            
            Console.ReadKey();
        }
    }
}
