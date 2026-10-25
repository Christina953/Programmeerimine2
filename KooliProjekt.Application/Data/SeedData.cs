using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public static class SeedData
    {
        // Fikseeritud Seed: iga käivitus annab täpselt samad "juhuslikud" andmed
        private static readonly Random Rnd = new Random(2026);

        // Eesti käibemaksumäär (%)
        private const decimal VatRate = 24m;
        public static void GenerateSeedData(ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return; // Data already seeded
            }
            SeedCarTypes(context);
            SeedCars(context);
            SeedUsers(context);
            SeedBookingsAndInvoices(context);
        }
        // ---------------------------------------------------------------
        // AUTOTÜÜBID (3)
        // ---------------------------------------------------------------
        private static readonly string[] CarTypeNames =
            {
            "Sõiduauto",
            "Väikebuss",
            "Kaubik"
        };

        private static void SeedCarTypes(ApplicationDbContext context)
        { 
            foreach (var name in CarTypeNames)
            {
                context.CarTypes.Add(new CarType { Name = name });
            }

            context.SaveChanges();
            
        }
        // ---------------------------------------------------------------
        // AUTOD (20)
        // mark, mudel, aasta, tüüp, keretüüp, kütus, käigukast, istekohad, hind/min, hind/km
        // ---------------------------------------------------------------
        private static readonly (string Brand, string Model, int Year, string CarType, string BodyType, string FuelType, string Transmission, int Seats, decimal PricePerMinute, decimal PricePerKilometer)[] CarModels =
        {
            // Sõiduautod (14)
            ("Toyota",        "Yaris",                  2023, "Sõiduauto", "Luukpära",   "Hübriid", "Automaat", 5, 0.14m, 0.16m),
            ("Volkswagen",    "Golf",                   2023, "Sõiduauto", "Luukpära",   "Bensiin", "Manuaal",  5, 0.16m, 0.18m),
            ("Škoda",         "Octavia",                2022, "Sõiduauto", "Luukpära",   "Bensiin", "Manuaal",  5, 0.16m, 0.18m),
            ("Toyota",        "Corolla Sedan",          2023, "Sõiduauto", "Sedaan",     "Hübriid", "Automaat", 5, 0.17m, 0.19m),
            ("Škoda",         "Superb",                 2024, "Sõiduauto", "Sedaan",     "Diisel",  "Automaat", 5, 0.23m, 0.24m),
            ("Tesla",         "Model 3",                2024, "Sõiduauto", "Sedaan",     "Elekter", "Automaat", 5, 0.24m, 0.12m),
            ("Škoda",         "Octavia Combi",          2023, "Sõiduauto", "Universaal", "Diisel",  "Automaat", 5, 0.18m, 0.20m),
            ("Toyota",        "Corolla Touring Sports", 2024, "Sõiduauto", "Universaal", "Hübriid", "Automaat", 5, 0.18m, 0.20m),
            ("Volkswagen",    "Passat Variant",         2024, "Sõiduauto", "Universaal", "Diisel",  "Automaat", 5, 0.22m, 0.23m),
            ("Kia",           "Ceed SW",                2022, "Sõiduauto", "Universaal", "Bensiin", "Manuaal",  5, 0.17m, 0.19m),
            ("Toyota",        "RAV4",                   2024, "Sõiduauto", "Maastur",    "Hübriid", "Automaat", 5, 0.25m, 0.26m),
            ("Kia",           "Sportage",               2024, "Sõiduauto", "Maastur",    "Bensiin", "Automaat", 5, 0.22m, 0.23m),
            ("Škoda",         "Kodiaq",                 2024, "Sõiduauto", "Maastur",    "Diisel",  "Automaat", 7, 0.27m, 0.28m),
            ("Volkswagen",    "ID.4",                   2023, "Sõiduauto", "Maastur",    "Elekter", "Automaat", 5, 0.24m, 0.13m),
 
            // Väikebussid (3)
            ("Volkswagen",    "Caravelle",              2023, "Väikebuss", "Minibuss",   "Diisel",  "Automaat", 9, 0.31m, 0.32m),
            ("Mercedes-Benz", "Vito Tourer",            2022, "Väikebuss", "Minibuss",   "Diisel",  "Automaat", 9, 0.30m, 0.31m),
            ("Toyota",        "Proace Verso",           2023, "Väikebuss", "Minibuss",   "Diisel",  "Manuaal",  9, 0.29m, 0.30m),
 
            // Kaubikud (3)
            ("Volkswagen",    "Transporter",            2022, "Kaubik",    "Furgoon",    "Diisel",  "Manuaal",  3, 0.25m, 0.27m),
            ("Ford",          "Transit Custom",         2023, "Kaubik",    "Furgoon",    "Diisel",  "Manuaal",  3, 0.26m, 0.28m),
            ("Renault",       "Master",                 2021, "Kaubik",    "Furgoon",    "Diisel",  "Manuaal",  3, 0.28m, 0.30m)
        };
        private static void SeedCars(ApplicationDbContext context)
        {
            var typeIds = context.CarTypes.ToDictionary(t => t.Name, t => t.Id);
            var usedRegNumbers = new HashSet<string>();

            foreach (var m in CarModels)
            {
                context.Cars.Add(new Car
                {
                    RegNumber = CreateRegNumber(usedRegNumbers),
                    Brand = m.Brand,
                    Model = m.Model,
                    Year = m.Year,
                    BodyType = m.BodyType,
                    FuelType = m.FuelType,
                    Transmission = m.Transmission,
                    Seats = m.Seats,
                    PricePerMinute = m.PricePerMinute,
                    PricePerKilometer = m.PricePerKilometer,
                    CarTypeId = typeIds[m.CarType]
                });
            }

            context.SaveChanges();
        }
        // Eesti numbrimärk kujul "123 ABC"
        private static string CreateRegNumber(HashSet<string> used)
        {
            const string letters = "ABCDEFGHJKLMNPRSTUVXZ";
            string reg;

            do
            {
                reg = $"{Rnd.Next(100, 1000)} " +
                      $"{letters[Rnd.Next(letters.Length)]}" +
                      $"{letters[Rnd.Next(letters.Length)]}" +
                      $"{letters[Rnd.Next(letters.Length)]}";
            }
            while (!used.Add(reg));

            return reg;
        }

        // ---------------------------------------------------------------
        // KASUTAJAD (35) – 2 administraatorit + 33 klienti
        // ---------------------------------------------------------------
        private static readonly string[] FirstNames =
        {
            "Mari", "Jaan", "Kadri", "Andres", "Liis", "Martin", "Kertu", "Rasmus", "Triin", "Margus",
            "Helen", "Siim", "Anu", "Priit", "Kristiina", "Tõnu", "Eveli", "Marko", "Laura", "Indrek",
            "Merike", "Kaspar", "Piret", "Ott", "Grete", "Tanel", "Maarja", "Karl", "Sirje", "Mihkel",
            "Elina", "Rein", "Kätlin", "Hendrik", "Ülle"
        };

        private static readonly string[] LastNames =
        {
            "Tamm", "Saar", "Sepp", "Mägi", "Kask", "Kukk", "Rebane", "Ilves", "Pärn", "Koppel",
            "Lepik", "Kuusk", "Karu", "Oja", "Mets", "Vaher", "Laur", "Lill", "Kallas", "Jõgi",
            "Raudsepp", "Põder", "Org", "Kivi", "Luik", "Tamme", "Vares", "Lõhmus", "Teder", "Kangur",
            "Rand", "Sild", "Paju", "Männik", "Kütt"
        };

        private static readonly (string Street, string City)[] Streets =
        {
            ("Narva mnt", "Tallinn"), ("Pärnu mnt", "Tallinn"), ("Tartu mnt", "Tallinn"),
            ("Mustamäe tee", "Tallinn"), ("Sõpruse pst", "Tallinn"), ("Paldiski mnt", "Tallinn"),
            ("Akadeemia tee", "Tallinn"), ("Riia", "Tartu"), ("Vanemuise", "Tartu"),
            ("Raatuse", "Tartu"), ("Rüütli", "Pärnu"), ("Riia mnt", "Pärnu"),
            ("Pikk", "Rakvere"), ("Tallinna mnt", "Narva"), ("Jaama", "Keila")
        };

        private static readonly string[] EmailDomains = { "gmail.com", "outlook.com" };
        private static void SeedUsers(ApplicationDbContext context)
        {
            for (var i = 0; i < FirstNames.Length; i++)
            {
                var first = FirstNames[i];
                var last = LastNames[i];
                var isAdmin = i < 2;
                var domain = isAdmin ? "autorent.ee" : EmailDomains[Rnd.Next(EmailDomains.Length)];
                var email = $"{ToAscii(first)}.{ToAscii(last)}@{domain}";
                var address = Streets[Rnd.Next(Streets.Length)];

                context.Users.Add(new User
                {
                    FirstName = first,
                    LastName = last,
                    Email = email,
                    Address = $"{address.Street} {Rnd.Next(1, 120)}-{Rnd.Next(1, 60)}, {address.City}",
                    PhoneNumber = $"+372 5{Rnd.Next(100, 1000)} {Rnd.Next(1000, 10000)}",
                    PasswordHash = HashPassword("Parool123!", email),
                    IsAdmin = isAdmin
                });
            }
            context.SaveChanges();
        }

        private static string ToAscii(string s) =>
           s.ToLowerInvariant()
            .Replace("ä", "a").Replace("ö", "o").Replace("ü", "u")
            .Replace("õ", "o").Replace("š", "s").Replace("ž", "z");

        // Näidisräsi (SHA-256).
        private static string HashPassword(string password, string salt) =>
            Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password + salt)));

        // ---------------------------------------------------------------
        // BRONEERINGUD (35), ARVED (35), ARVEREAD (35)
        //  - 35 lõpetatud broneeringut -> igaühele arve ja arverida
        // ---------------------------------------------------------------

        private static void SeedBookingsAndInvoices(ApplicationDbContext context)
        {
            var cars = context.Cars.OrderBy(c => c.Id).ToList();
            var customers = context.Users.Where(u => !u.IsAdmin).OrderBy(u => u.Id).ToList();
        
        // Iga auto läbisõidumõõdiku näit, et km-d järjestikuste broneeringute vahel klapiksid
        var odometer = cars.ToDictionary(c => c.Id, c => Rnd.Next(8000, 120000));

        var start = DateTime.Today.AddDays(-170).AddHours(8);
        var invoiceNo = 1;
 
            for (var i = 0; i< 35; i++)
            {
                var car = cars[Rnd.Next(cars.Count)];
        var user = customers[Rnd.Next(customers.Count)];

        // Enamik rente on lühikesed (25 min – 5 h), osa pikemad (5 h – 2 päeva)
        var isLong = Rnd.Next(4) == 0;
        var minutes = isLong ? Rnd.Next(300, 2880) : Rnd.Next(25, 300);
        var km = isLong
            ? Rnd.Next(60, 600)
            : Math.Max(3, (int)(minutes / 60.0 * Rnd.Next(15, 45)));

        // Klient sisestab algusnäidu ise. Vahepeal võis töötaja autoga
        // sõita (pesu, hooldus, tankimine), seega lisame 0–30 km.
        var startKm = odometer[car.Id] + Rnd.Next(0, 31);
        var endTime = start.AddMinutes(minutes);

        var booking = new Booking
        {
            UserId = user.Id,
            CarId = car.Id,
            StartTime = start,
            EndTime = endTime,
            StartKilometers = startKm,
            EndKilometers = startKm + km
        };
        context.Bookings.Add(booking);
                odometer[car.Id] = startKm + km;
                context.SaveChanges(); // booking.Id on vajalik arvereal
 
                // Summa valemiga: minutid * ajatariif + km * kilomeetritariif
                var lineTotal = Math.Round(minutes * car.PricePerMinute + km * car.PricePerKilometer, 2);
        var vatAmount = Math.Round(lineTotal * VatRate / 100, 2);

        // ~85% arvetest on makstud (kui maksekuupäev pole tulevikus)
        DateTime? paidAt = null;
        var possiblePaidAt = endTime.AddDays(Rnd.Next(1, 14)).AddHours(Rnd.Next(1, 10));
                if (possiblePaidAt<DateTime.Now && Rnd.Next(100) < 85)
                {
                    paidAt = possiblePaidAt;
                }
 
                var invoice = new Invoice
                {
                    InvoiceNumber = $"ARV-{endTime:yyyy}-{invoiceNo++:D4}",
                    InvoiceDate = endTime,
                    DueDate = endTime.Date.AddDays(14),
                    TotalAmount = lineTotal,
                    VatRate = VatRate,
                    VatAmount = vatAmount,
                    GrandTotal = lineTotal + vatAmount,
                    PaidAt = paidAt,
                    UserId = user.Id
                };
                context.Invoices.Add(invoice);
                context.SaveChanges(); // invoice.Id on vajalik arvereal
 
                context.InvoiceLines.Add(new InvoiceLine
                {
        InvoiceId = invoice.Id,
                    BookingId = booking.Id,
                    Description = $"Renditeenus {car.RegNumber}",
                    Quantity = 1,
                    UnitPrice = lineTotal,
                    LineTotal = lineTotal
    });
                context.SaveChanges();
 
                // Järgmine broneering algab ~4 päeva hiljem, nii et rendid ei kattu
                start = start.AddDays(4).AddMinutes(Rnd.Next(0, 600));
            }
        }
    }
}
