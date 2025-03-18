namespace Zsirozas
{
    public class Jatek
    {
        private Random r = new Random();

        // Játékhoz szükséges listák, amikben a lapokat tároljuk a játék során
        private List<Kartyak> pakli;
        private List<Kartyak> gepkez = new List<Kartyak>();
        private List<Kartyak> jatekoskez = new List<Kartyak>();
        private List<Kartyak> asztal = new List<Kartyak>();

        // Játékhoz szükséges változók
        private int jatekospont = 0;
        private int geppont = 0;
        private bool nemkell = false;
        private bool vittehiba = false;
        private bool hetesu = false;
        private bool jatekoskezd;

        public void J_Megjelenites()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Sok sikert!\n");

            pakli = paklikeveres(pakligeneralas());

            // Játék előtt a paklikiosztás a játékosnak és a gépnek
            huzas(gepkez);
            huzas(jatekoskez);

            // Kezdő játékos megállapítása
            jatekoskezd = penzfeldobas();
            // Kezdő játékos megállapítása után elindul a játék
            jatekkezdet(jatekoskezd);

            // Míg nem fogy el az összes lap a pakliból, vagy a játékos nem tette le az összes lapját,
            // vagy esetleg nem a 7-es lap az utolsó ami maradt a két játékos kezében,
            // akkor addig amíg egyik feltétel sem teljesül fusson a játék
            while ((pakli.Count != 0 || jatekoskez.Count != 0) && !hetesu)
            {
                jatek(jatekoskezd);
            }

            // Tájékoztatja a felhasználót a játék végkimeneteléről
            if (geppont < jatekospont)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nNyertél! Gratulálunk");
            }
            else if (geppont == jatekospont)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nDöntetlen! Milyen szoros játszma!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nVesztettél! Majd legközelebb");
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Nyomj meg egy gombot a visszatéréshez...");
            Console.ReadKey();
            Menu menu = new Menu();
            menu.M_Megjelenites();
            Console.ResetColor();
        }

        // Megállapítja, hogy van-e nyertese a játéknak
        private bool vanenyertes()
        {
            int db = asztal.Count - 1;
            if (asztal.Count < 2)
                return false;
            else if (asztal[db].ertek == asztal[db - 1].ertek || asztal[db].ertek == 7)
            {
                return true;
            }
            return false;
        }

        // Pénzfeldobás után tájékoztatjuk a felhasználót arról, hogy végül ki kezdi a játékot
        private void jatekkezdet(bool jatekoskezd)
        {
            if (jatekoskezd)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Nyertél! Te kezdesz!\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Vesztettél! A gép kezd!\n");
            }
        }

        // A kezdés után elkezdődik a valódi játék és tájékoztatjuk a felhasználót az aktuális lapokról
        private void jatekeleje(bool jatekoskezd)
        {
            hetesutolso();
            if (hetesu)
            {
                return;
            }
            else if (jatekoskezd)
            {
                jatekos_letetel();
                gepletesz();
                asztal_kartya_megjelenites();
            }
            else
            {
                gepletesz();
                asztal_kartya_megjelenites();
                jatekos_letetel();
            }
        }

        // Játék fő része
        private void jatek(bool jatekoskezd)
        {
            jatekeleje(jatekoskezd);
            // Míg nincs nyertes és nem szeg szabályt egyik játékos sem, addig fusson a játék
            while (vanenyertes() && !vittehiba)
            {
                if (jatekoskezd)
                {
                    jatekosutes();
                }
                else
                {
                    geputes();
                }
                if (nemkell)
                {
                    break;
                }
            }
            if (!hetesu)
            {
                korvege();
            }
        }
        // Játékos lapleütésének a mechanikája
        private void jatekosutes()
        {
            string szeretne_e_utni_valasz;
            bool helyes_bemenet = false;

            while (!helyes_bemenet)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Szeretnél-e ütni? (igen / nem): ");
                Console.ResetColor();
                szeretne_e_utni_valasz = Console.ReadLine()!.ToLower();
                Console.WriteLine();

                if (szeretne_e_utni_valasz == "igen")
                {
                    hetesutolso();
                    if (hetesu)
                    {
                        return;
                    }
                    else
                    {
                        jatekos_letetel();
                        if (!vanenyertes())
                        {
                            vittehiba = true;
                        }
                        gepletesz();
                        asztal_kartya_megjelenites();
                    }
                    helyes_bemenet = true;
                }
                else if (szeretne_e_utni_valasz == "nem")
                {
                    nemkell = true;
                    helyes_bemenet = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Érvénytelen választás! Kérlek, 'igen' vagy 'nem' szót adj meg.");
                    Console.ResetColor();
                }
            }
        }

            // Gép lapleütésének a mechanikája
            private void geputes()
        {
            bool vanepont = false;
            if (vanenyertes())
            {
                for (int i = 0; i < asztal.Count - 1; i++)
                {
                    if (asztal[i].ertek == 10 || asztal[i].ertek == 14)
                    {
                        vanepont = true;
                    }
                }
            }
            if (vanepont)
            {
                hetesutolso();
                if (hetesu)
                {
                    return;
                }
                else
                {
                    gepletesz();
                    asztal_kartya_megjelenites();
                    if (!vanenyertes())
                    {
                        vittehiba = true;
                    }
                    jatekos_letetel();
                }
            }
            else
            {
                nemkell = true;
            }
        }

        // Az utolsó lapról megállapítja, hogy 7-es vagy 7-estől különböző lap-e
        private void hetesutolso()
        {
            if (jatekoskez.Count == 1 && jatekoskez[0].ertek == 7)
            {
                asztal.Clear();
                pakli.Clear();
                jatekospont = 0;
                geppont = 80;
                hetesu = true;
                Console.WriteLine("7-es lap maradt a kezedben utoljára, ami szabályellenes, ezért automatikusan vesztettél! A játéknak vége");
            }
        }

        // A körök végén tájékoztatja a felhasználót a kör végkimeneteléről
        private void korvege()
        {
            huzas(gepkez);
            huzas(jatekoskez);
            int pontok = 0;
            int db = asztal.Count - 1;
            for (int i = 0; i <= db; i++)
            {
                if (asztal[i].ertek == 10 || asztal[i].ertek == 14)
                {
                    pontok += 10;
                }
            }
            if ((vanenyertes() && !jatekoskezd) || (!vanenyertes() && jatekoskezd) || (vittehiba == true && !jatekoskezd))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Te vitted a lapokat");
                jatekospont += pontok;
                jatekoskezd = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("A gép vitte a lapokat");
                geppont += pontok;
                jatekoskezd = false;
            }
            nemkell = false;
            vittehiba = false;
            asztal.Clear();
            Console.WriteLine("--------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Pontod: {jatekospont} ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" Ellenfél pont: {geppont}");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("--------------------------------------");
            Console.ForegroundColor = ConsoleColor.Magenta;
            if (jatekospont > geppont)
            {
                Console.WriteLine("Így tovább, jó úton haladsz a nyerés felé\n");
            }
            else if (jatekospont < geppont)
            {
                Console.WriteLine("Nocsak nocsak nocsak, még egy gép ellen se tudsz nyerni? Jó úton haladsz a vereség felé\n");
            }
            else
            {
                Console.WriteLine("Fej-fej mellett az állás\n");
            }
            Console.ResetColor();
        }

        // Pénzfeldobás (kezdő játékos megállapítása)
        private bool penzfeldobas()
        {
            bool rosszertek = true;
            while (rosszertek)
            {

                Console.Write("Fej vagy írás a kezdésért? ");
                Console.ResetColor();
                string fejvagyiras = Console.ReadLine()!.ToLower();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (fejvagyiras == "fej" || fejvagyiras == "írás")
                {
                    rosszertek = false;
                    int penzfeldobas = r.Next(0, 2);
                    string eredmeny = penzfeldobas == 0 ? "fej" : "írás";
                    if (fejvagyiras == eredmeny)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Érvénytelen választás! Kérlek, 'fej' vagy 'írás' szót adj meg.");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
            }
            return false;
        }

        // Asztal kártyáinak a megjelenítése
        private void asztal_kartya_megjelenites()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Az asztalon lévő lapok: ");
            Console.ResetColor();
            lapkiiras(asztal, true);
            Console.WriteLine();
        }



        // Lap kiírása
        private void lapkiiras(List<Kartyak> list, bool asztal)
        {
            int hossz = list.Count;

            string[,] tulajdonsagok = new string[hossz, 2];

            for (int i = 0; i < hossz; i++)
            {
                tulajdonsagok[i, 0] = list[i].tipus;
                tulajdonsagok[i, 1] = list[i].ertek.ToString();
            }

            string[] sablonok = new string[5]
            {
        "┌─────┐",
        "│{0}   │",  // {0} helyére az érték kerül
        "│  {1}  │",  // {1} helyére a szimbólum kerül
        "│   {0}│",
        "└─────┘"
            };

            for (int i = 0; i < sablonok.Length; i++)
            {
                for (int j = 0; j < hossz; j++)
                {
                    string symbol = "";
                    string tipusok = tulajdonsagok[j, 0];
                    string ertek = tulajdonsagok[j, 1];

                    // Szín és szimbólum beállítása
                    switch (tipusok)
                    {
                        case "Piros":
                            Console.ForegroundColor = ConsoleColor.Red;
                            symbol = "♥";
                            break;
                        case "Tök":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            symbol = "♦";
                            break;
                        case "Zöld":
                            Console.ForegroundColor = ConsoleColor.Green;
                            symbol = "♣";
                            break;
                        case "Makk":
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            symbol = "♠";
                            break;
                        default:
                            symbol = "L";
                            break;
                    }

                    // Aktuális sor kiírása
                    string formattedLine = "";
                    if (i == 1)
                    {
                        formattedLine = string.Format(sablonok[i], ertek.Length == 1 ? ertek + " " : ertek, symbol);
                    }
                    else if (i == 2)
                    {
                        formattedLine = string.Format(sablonok[i], ertek, symbol);
                    }
                    else if (i == 3)
                    {
                        formattedLine = string.Format(sablonok[i], ertek.Length == 1 ? " " + ertek : ertek, symbol);
                    }
                    else
                    {
                        formattedLine = sablonok[i];
                    }

                    Console.Write($"{formattedLine} ");
                }
                Console.WriteLine();
            }
            Console.ResetColor(); // Visszaállítja az alapértelmezett színt
            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (!asztal){
                Console.Write("   ");
                for (int szamozas = 1; szamozas < hossz + 1; szamozas++)
                {
                    Console.Write($"{szamozas}".PadRight(8));
                }
                Console.WriteLine();
            }
            else{
                for (int szamozas = 0; szamozas < hossz-1; szamozas++){
                    Console.Write("        ");
                }
                
                Console.Write($"(felső)");
                Console.WriteLine();
            }
        }




        // Játékos lapletételének a mechanikája
        private void jatekos_letetel()
        {
            bool valasztashiba = true;
            while (valasztashiba)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Játékos keze: ");
                lapkiiras(jatekoskez, false);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"Melyik kártyát szeretnéd kijátszani? (1-{jatekoskez.Count}): ");
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out int valasztas))
                {
                    valasztas -= 1;
                    if (valasztas >= 0 && valasztas < jatekoskez.Count)
                    {
                        asztal.Add(lapletetel(valasztas, jatekoskez));
                        valasztashiba = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Hibás választás! Próbáld újra");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Érvénytelen bemenet! Számot adj meg");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }
        // Gép lapletételének a mechanikája
        private void gepletesz()
        {
            asztal.Add(lapletetel(gepvalaszt(), gepkez));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("A gép letett egy lapot.");
            Console.ResetColor();
        }

        // Gép lapletételénél a lapkiválasztásának a mechanikája
        private int gepvalaszt()
        {
            int db = asztal.Count - 1;
            int ennyidb7es = 0;

            foreach (var lap in gepkez)
            {
                if (lap.ertek == 7)
                {
                    ennyidb7es++;
                }
            }

            if (db == ennyidb7es)
            {
                for (int seged = 0; seged < gepkez.Count; seged++)
                {
                    if (gepkez[seged].ertek == 7)
                    {
                        return seged;
                    }
                }
            }

            if (asztal.Count == 0)
            {
                for (int seged = 0; seged < gepkez.Count; seged++)
                {
                    if (gepkez[seged].ertek != 7 && gepkez[seged].ertek != 14 && gepkez[seged].ertek != 10)
                    {
                        return seged;
                    }
                }

                for (int seged = 0; seged < gepkez.Count; seged++)
                {
                    if (gepkez[seged].ertek == 14 || gepkez[seged].ertek == 10)
                    {
                        return seged;
                    }
                }
            }

            if (asztal[db].ertek == 10 || asztal[db].ertek == 14)
            {
                for (int seged = 0; seged < gepkez.Count; seged++)
                {
                    if (gepkez[seged].ertek == asztal[db].ertek)
                    {
                        return seged;
                    }
                }

                for (int seged = 0; seged < gepkez.Count; seged++)
                {
                    if (gepkez[seged].ertek == 7)
                    {
                        return seged;
                    }
                }
            }
            else
            {
                for (int seged = 0; seged < gepkez.Count; seged++)
                {
                    if (gepkez[seged].ertek != 7 && gepkez[seged].ertek == asztal[db].ertek)
                    {
                        return seged;
                    }
                }
            }

            return r.Next(gepkez.Count);
        }


        // Lapletétel mechanikája
        private Kartyak lapletetel(int index, List<Kartyak> kez)
        {
            Kartyak kartya = kez[index];
            kez.RemoveAt(index);
            return kartya;
        }

        // Laphúzás mechanikája
        private void huzas(List<Kartyak> kez)
        {
            while (kez.Count < 4 && pakli.Count > 0)
            {
                kez.Add(pakli[0]);
                pakli.RemoveAt(0);
            }
        }

        // Pakli generálása
        private List<Kartyak> pakligeneralas()
        {
            string[] tipusok = { "Piros", "Tök", "Zöld", "Makk" };
            List<Kartyak> pakli = new List<Kartyak>();
            foreach (string tipus in tipusok)
            {
                for (int ertek = 7; ertek <= 14; ertek++)
                {
                    pakli.Add(new Kartyak(ertek, tipus));
                }
            }
            return pakli;
        }

        // Pakli keverése
        private List<Kartyak> paklikeveres(List<Kartyak> pakli)
        {
            return pakli.OrderBy(c => r.Next()).ToList();
        }

        // Kártyák osztály
        public record Kartyak(int ertek, string tipus);
    }
}
// Készítette: Andris, Bálint, Peti