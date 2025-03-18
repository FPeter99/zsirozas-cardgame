namespace Zsirozas
{
    class Menu
    {
        private string[] menupontok = { "Új játék", "Szabályzat", "Kilépés" }; // Menüpontokat eltároljuk egy tömbben
        private int aktiv_menupont = 0; // Az éppen aktív menüpontot számláló egész szám, minimum értéke = 0, max értéke = menupontok.Length-1

        public void M_Megjelenites()
        {
            Console.Clear();
            Console.WriteLine(KozepreIgazitas("ZSÍROZÁS\n"));

            while (true)
            {
                M_Menupontok_Kiiras();
                ConsoleKeyInfo billentyu = Console.ReadKey(true);

                // Felfelé billentyűnél fellépünk egyet a menüpontok közt, a menüpontokat számláló indexet csökkentjük
                if (billentyu.Key == ConsoleKey.UpArrow)
                {
                    aktiv_menupont--;
                    // Ha a legfelső menüponthoz ér, akkor a legalsóhoz ugrik
                    if (aktiv_menupont < 0)
                    {
                        aktiv_menupont = menupontok.Length - 1;
                    }
                }
                // Lefelé billentyűnél lelépünk egyet a menüpontok közt, a menüpontokat számláló indexet növeljük
                else if (billentyu.Key == ConsoleKey.DownArrow)
                {
                    aktiv_menupont++;
                    // Ha a legalsó menüponthoz ér, akkor a legfelsőhöz ugrik
                    if (aktiv_menupont >= menupontok.Length)
                    {
                        aktiv_menupont = 0;
                    }
                }
                // Enternél a kiválasztott menüpontot futtatjuk
                else if (billentyu.Key == ConsoleKey.Enter)
                {
                    M_Futtatas();
                }
            }
        }

        // Menüpontok kiírása a konzolra
        private void M_Menupontok_Kiiras()
        {
            Console.Clear();
            Console.WriteLine(KozepreIgazitas("ZSÍROZÁS\n"));

            for (int i = 0; i < menupontok.Length; i++)
            {
                if (i == aktiv_menupont)
                {
                    Console.WriteLine(KozepreIgazitas($">> {menupontok[i]} <<"));
                }
                else
                {
                    Console.WriteLine(KozepreIgazitas(menupontok[i]));
                }
            }
        }

        // Aktív menüpont alapján futtatjuk a megfelelő menüpontot
        private void M_Futtatas()
        {
            switch (aktiv_menupont)
            {
                case 0:
                    Console.Clear();
                    Jatek jatek = new Jatek();
                    jatek.J_Megjelenites();
                    break;
                case 1:
                    Szabalyzat szabalyzat = new Szabalyzat();
                    szabalyzat.SZ_Megjelenites();
                    break;
                case 2:
                    Kilepes kilepes = new Kilepes();
                    kilepes.K_Megjelenites();
                    break;
            }
        }

        // Szöveg középre igazítása a konzol ablak méretétől függően
        private string KozepreIgazitas(string szoveg)
        {
            int konzolSzelesseg = Console.WindowWidth;
            int padding = (konzolSzelesseg - szoveg.Length) / 2;
            return szoveg.PadLeft(padding + szoveg.Length).PadRight(konzolSzelesseg);
        }
    }
}
// Végleges menü