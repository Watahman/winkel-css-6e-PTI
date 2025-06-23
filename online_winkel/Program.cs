using System.Net.Mime;

namespace online_winkel
{
    class Product
    {
        string naam;
        double prijs;
        int voorraad;
        public Product(string naam, double prijs, int voorraad)
        {
            this.naam = naam;
            this.prijs = prijs;
            this.voorraad = voorraad;
        }
        public string info()
        {
            return "dit is een " + naam + ", heeft een prijs van €" + prijs + ", en er zijn " + voorraad + " op voorraad";
        }
        public string name()
        {
            return naam;
        }
        public double price()
        {
            return prijs;
        }
    }
    class Etenswaar : Product
    {
        int houdbaarheidsdatum;
        public Etenswaar(int houdbaarheidsdatum, string naam, double prijs, int voorraad) : base(naam, prijs, voorraad)
        {
            this.houdbaarheidsdatum = houdbaarheidsdatum;
        }
        public string datumcheck(Dictionary<Product, int> lijst)
        {
            Etenswaar food;
            foreach (KeyValuePair<Product, int> eten in lijst)
            {
                if (eten.Key is Etenswaar)
                {
                    food = eten.Key as Etenswaar;
                    if (food.houdbaarheidsdatum <= 7)
                    {
                        lijst.Remove(eten.Key);
                        return food.name() + " werd verwijderd uit je boodschappen.";
                    }
                }
            }
            Console.WriteLine("dit zit nog in je boodschappen.");
            foreach (KeyValuePair<Product, int> items in lijst)
            {
                return items.Key.name();
            }
            return "";
        }
    }
    class Kledij : Product
    {
        string merk;
        int voorraad;
        public Kledij(string merk, string naam, double prijs, int voorraad) : base(naam, prijs, voorraad)
        {
            this.merk = merk;
            this.voorraad = voorraad;
        }
        public string kopen(Dictionary<Product, int> lijst, Kledij kleding, int aantal)
        {
            if (kleding.voorraad - aantal > 1)
            {
                lijst.Add(kleding, aantal);
                return "kleding is toegevoegd aan winkelmand";
            }
            else
            {
                return "er was niet genoeg in voorraad";
            }
        }
    }
    class Elektronica : Product
    {
        bool garantieperiode_geldigheid;
        double prijs;
        int voorraad;
        string naam;
        public Elektronica(string naam, double prijs, int voorraad, bool garantieperiode_geldigheid) : base(naam, prijs, voorraad)
        {
            this.garantieperiode_geldigheid = garantieperiode_geldigheid;
            this.prijs = prijs;
            this.voorraad = voorraad;
            this.naam = naam;
        }
        public (double, Dictionary<Product, int>) terugbrengen(double geld, Dictionary<Product, int> lijst)
        {
            if (garantieperiode_geldigheid == true)
            {
                geld += prijs;
                foreach(KeyValuePair<Product,int> item in lijst)
                {
                    if(item.Key.name() == naam)
                    {
                        lijst.Remove(item.Key);
                    }
                }
                voorraad++;
                Console.WriteLine("item teruggebracht");
                return (geld, lijst);
            }
            else
            {
                Console.WriteLine("garantieperiode niet geldig");
                return (geld, lijst);
            }
        }
    }
    class Bestelling
    {
        klant nieuwklant;
        winkelwagen wagon;
        double totaalbedrag;
        bool status;
        public Bestelling(klant nieuwklant, winkelwagen wagon, double totaalbedrag, bool status)
        {
            this.nieuwklant = nieuwklant;
            this.wagon = wagon;
            this.totaalbedrag = totaalbedrag;
            this.status = status;
        }
        public string info(klant jean_pol_pièrre, winkelwagen wagonnie)
        {
            nieuwklant = jean_pol_pièrre;
            wagon = wagonnie;
            return "klantinfo: " + nieuwklant.info() + " winkelwageninfo: " + wagon.alle_producten() + " en de status is: " + status;
        }
    }
    class klant
    {
        string naam;
        string email;
        double budget;
        winkelwagen wagon;
        public klant(string naam, string email, double budget, winkelwagen wagon)
        {
            this.naam = naam;
            this.email = email;
            this.budget = budget;
            this.wagon = wagon;
        }
        public string info()
        {
            return "De naam van de klant is: " + naam + ", zijn email is: " + email + ", en hij heeft nog " + budget + "euro. ";
        }
        public double bugeto()
        {
            return budget;
        }
    }
    class winkelwagen
    {
        Dictionary<Product, int> wageninhoud;
        double totaalprijs;
        public winkelwagen(Dictionary<Product, int> wageninhoud, double totaalprijs)
        {
            this.wageninhoud = wageninhoud;
            this.totaalprijs = totaalprijs;
        }
        public string itemtoev(Product item, int hoeveelheid)
        {
            wageninhoud.Add(item, hoeveelheid);
            totaalprijs += item.price();
            return "item is toegevoegd aan winkelwagen";
        }
        public string itemverw(Product item)
        {
            wageninhoud.Remove(item);
            totaalprijs -= item.price();
            return "item is verwijderd uit winkelwagen";
        }
        public double totale_prijs()
        {
            return totaalprijs;
        }
        public string alle_producten()
        {
            foreach (KeyValuePair<Product, int> item in wageninhoud)
            {
                return "Er zitten " + item.Value + " " + item.Key + " in ";
            }
            return "";
        }
    }
    class Onlineplatform
    {
        string naam;
        List<Product> producten;
        List<klant> klanten;
        List<Bestelling> bestellingen;
        public Onlineplatform(string naam, List<Product> producten, List<klant> klanten, List<Bestelling> bestellingen)
        {
            this.naam = naam;
            this.producten = producten;
            this.klanten = klanten;
            this.bestellingen = bestellingen;
        }
        public void product_toev(Product productos)
        {
            producten.Add(productos);
        }
        public void klant_toev(klant klanto)
        {
            klanten.Add(klanto);
        }
        public void best_toev(Bestelling bestellingo)
        {
            bestellingen.Add(bestellingo);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> productenn = new List<Product>();
            List<klant> klantenn = new List<klant>();
            List<Bestelling> bestellingenn = new List<Bestelling>();
            Onlineplatform platform = new Onlineplatform("alibaba", productenn, klantenn, bestellingenn);

            Product schroef = new Product("schroef", 2.33, 500);
            Kledij trui = new Kledij("levis", "driller",45.77,4);
            Etenswaar worst = new Etenswaar(7, "worst", 3.90, 88);
            Elektronica lamp = new Elektronica("lamp", 12.86, 5, true);

            platform.product_toev(schroef);
            platform.product_toev(trui);
            platform.product_toev(worst);
            platform.product_toev(lamp);

            Dictionary<Product, int> inhoud1 = new Dictionary<Product, int>();
            Dictionary<Product, int> inhoud2 = new Dictionary<Product, int>();

            winkelwagen wagen1 = new winkelwagen(inhoud1, 0);
            winkelwagen wagen2 = new winkelwagen(inhoud2, 0);

            klant emile = new klant("emile", "emile@gmail", 121.77,wagen1);
            klant arjen = new klant("arjen", "arjen@gmail", 1119.90,wagen2);

            platform.klant_toev(emile);
            platform.klant_toev(arjen);

            wagen1.itemtoev(lamp, 2);
            wagen2.itemtoev(worst, 34);

            Bestelling best1 = new Bestelling(emile, wagen1, wagen1.totale_prijs(), false);
            Bestelling best2 = new Bestelling(arjen, wagen2, wagen2.totale_prijs(), false);

            Console.WriteLine(best1.info(emile,wagen1));
            Console.WriteLine(best1.info(arjen,wagen2));

            lamp.terugbrengen(emile.bugeto(), inhoud1);

        }
    }
}