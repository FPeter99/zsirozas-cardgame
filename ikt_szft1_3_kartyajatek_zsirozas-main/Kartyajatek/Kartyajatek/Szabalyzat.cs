namespace Zsirozas
{
    class Szabalyzat
    {
        public void SZ_Megjelenites()
        {
            Console.Clear();
            Console.WriteLine("Szabályzat\n");

            // Szabályzat leírása
            string[] szabalyzat =
            {
                "A játék célja",
                "A játékban a 10-esek és az ász lapok érnek 10-10 pontot, azokat kell gyűjtenünk az ütésekkel. A begyűjtésükkel összesen 80 pont érhető el. A cél, hogy legalább 50-et megszerezzünk amivel megnyerjük a partit.",
                "Osztás és játékmenet",
                "A játékot 2 fő játszhatja, az osztás felváltva történik. Az osztó 4 - 4 lapot oszt mindkettőjüknek, a megmaradt paklit hátlappal lefele lehelyezi, azok lesznek a húzó lapok. A játékot az osztó játékostársa kezdi ő tesz ki középre 1 lapot.",
                "Ütések, húzások és játékmenet",
                "Két dolgot tehetünk, vagy ütő lapot teszünk vagy nem ütő lapot teszünk a kitett lapra.",
                "Ütéskor: A középre kitett lapot ütni lehet ugyanolyan értékű lappal vagy bármelyik hetessel (minden 7-es lap ütőlapnak számít a játékban). Pl. ha makk alsót tett a partnerünk, akkor bármelyik másik alsót rátéve ütjük azt a lapot. Az ütés azt jelenti, hogy mi visszük a két lapot és azt magunk elé helyezzük hátlappal lefele. Ebben a játékban mindkét játékos gyűjtögeti ütésekkel a lapokat, de pontot csak a tízesek és ászok érnek.",
                "Ha nem ütjük a letett lapot: Ütni csak ugyanolyan értékű lappal vagy hetessel lehet, tehát ha mást teszünk akkor a partnerünk (a lapot kitevő játékos) viszi a lapokat. A példában a makk alsóra ha ászt, ha felsőt vagy ha nyolcast teszünk akkor is viszi a két letett lapot a partnerünk.",
                "Továbbütésre is van lehetőség partnerünknek ami azt jelenti, hogy a kitett második lapra amit mi tettünk arra még 'válaszolhat', ha van neki ütőlapja (a példában alsó) akkor azt ráteheti. Vagy még hetest is mert azzal minden lapot lehet ütni. Harmadik lapként már csak ütőlapot lehet tenni más lapot nem. Ha tovább üti akkor nekünk is lapot kell tenni ami lehet ütőlap vagy nem ütőlap. Középen mindig páros számú lapoknak kell lennie amit valaki visz. 2,4,6 vagy 8 lap. Az a játékos viheti el a lapokat aki utoljára ütőlapot tett. Összesen mind a 4 lapot kijátszhatjuk a kezünkből.",
                "Laphúzás a pakliból: Miután valaki viszi a lapokat a húzópakliból annyi lapot húzunk, hogy újra 4 lap legyen a kezünkben. Elsőnek az a játékos húz aki vitte a lapokat.",
                "Miután valaki vitte az asztalon lévő lapokat ő lesz a kitevő játékos. Egy parti addig tart amíg minden lap elfogy a húzópakliból és a kezünkből is. Olyankor számoljuk össze az elvitt 10-eseket és ászokat, hogy ki nyert.",
                "A játékban nincs döntetlen, 40-40 pont esetén az a játékos nyer aki a legutolsó ütést vitte.",
                "Szabály még, hogy utolsó lapnak nem tarthatunk meg hetes lapot, mert akkor elveszítjük a játékot pontoktól függetlenül.",
            };

            foreach (string paragrafus in szabalyzat)
            {
                Console.WriteLine(paragrafus);
                Console.WriteLine();
            }

            Console.WriteLine("Nyomj meg egy gombot a visszatéréshez...");
            Console.ReadKey();
            Menu menu = new Menu();
            menu.M_Megjelenites();
        }
    }
}