using System; 
using System.Threading;
using System.IO;
using System.Security;
using System.Net;

namespace ATM_Machine
{
    class Program
    {
        enum Provider
        {
            Telkomsel = 1, XL, Axis, SmartFren, Indosat, Tri 
        }
        public static string Direct(string text)
        {
            string s = Path.Combine(Environment.CurrentDirectory, "text\\", text); ;
            return s;
        }
        static string[] Load()
        {
            StreamReader saya = new StreamReader(Direct("InfoNasabah.txt"));
            string[] temp = new string[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = saya.ReadLine();
            }
            saya.Close();
            return temp;
        }
        static long CekSaldo(string a)
        {
            long saldo = 0;
            string[] listpin = new string[5];
            string[] listsaldo = new string[5];
            string[] temp = Load();
            listpin = temp[0].Split(';');
            listsaldo = temp[1].Split(';');
            for (int i = 0; i < 5; i++)
            {
                if (listpin[i] == a)
                {
                    saldo += Convert.ToInt64(listsaldo[i]);
                }
            }
            return saldo;
        }
        static bool CekPin(string a) 
        {
            bool status = false;
            string[] listpin = new string[5];
            string[] listsaldo = new string[5];
            string[] temp = Load();
            listpin = temp[0].Split(';');
            listsaldo = temp[1].Split(';');
            for (int i = 0; i < 5; i++)
            {
                if (listpin[i] == a)
                {
                    status = true;
                }
            }
            return status;
        }
        static bool CekRekening(string a)
        {
            bool status = false;
            string[] listNoRekening = new string[5];
            string[] temp = Load();
            listNoRekening = temp[2].Split(';');
            for (int i = 0; i < 5; i++)
            {
                if (listNoRekening[i] == a)
                {
                    status = true;
                }
            }
            return status;
        }
        static string GetPinbyRekening(string a)
        {
            string pinReturn = "";
            string[] listpin = new string[5];
            string[] listNoRekening = new string[5];
            string[] temp = Load();
            listpin = temp[0].Split(';');
            listNoRekening = temp[2].Split(';');
            for (int i = 0; i < 5; i++)
            {
                if (listNoRekening[i] == a)
                {
                    pinReturn = listpin[i];
                }
            }
            return pinReturn;
        }
        static string GetRekeningbyPin(string a)
        {
            string rekeningReturn = "";
            string[] listpin = new string[5];
            string[] listNoRekening = new string[5];
            string[] temp = Load();
            listpin = temp[0].Split(';');
            listNoRekening = temp[2].Split(';');
            for (int i = 0; i < 5; i++)
            {
                if (listpin[i] == a)
                {
                    rekeningReturn = listNoRekening[i];
                }
            }
            return rekeningReturn;
        }
        static void UpdateSaldo(string a, long b)
        {
            string c = "", d = "", e = "", f = "";
            string[] listpin = new string[5];
            string[] listsaldo = new string[5];
            string[] listNoRekening = new string[5];
            string[] temp = Load();
            listpin = temp[0].Split(';');
            listsaldo = temp[1].Split(';');
            listNoRekening = temp[2].Split(';');
            for (int i = 0; i < 5; i++)
            {
                if (listpin[i] == a)
                {
                    listsaldo[i] = b.ToString();
                }
            }
            for (int i = 0; i < 5; i++)
            {
                c += listpin[i] + ';';
                d += listsaldo[i] + ';';
                f += listNoRekening[i] + ';';
            }
            e += c + "\n" + d + "\n" + f;
            File.WriteAllText(Direct("InfoNasabah.txt"), e);
        }
        static void UpdatePin(string a, string b)
        {
            string c = "", d = "", e = "", f = "";
            string[] listpin = new string[5];
            string[] listsaldo = new string[5];
            string[] listNoRekening = new string[5];
            string[] temp = Load();
            listpin = temp[0].Split(';');
            listsaldo = temp[1].Split(';');
            listNoRekening = temp[2].Split(';');
            for (int i = 0; i < 5; i++)
            {
                if (listpin[i] == a)
                {
                    listpin[i] = b;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                c += listpin[i] + ';';
                d += listsaldo[i] + ';';
                f += listNoRekening[i] + ';';
            }
            e += c + "\n" + d + "\n" + f;
            File.WriteAllText(Direct("InfoNasabah.txt"), e);
        }

        static bool validasiPin(string a)
        {
            bool stats = true;
            foreach (char i in a)
            {
                stats = Char.IsNumber(i);
                if (stats == false)
                    break;
            }
            return stats;
        }
        static void Welcome()
        {
            string[] erico = new string[15];
            StreamReader str = new StreamReader(Direct("erico.txt"));
            for (int i = 0; i < 15; i++)
            {
                erico[i] = str.ReadLine();
            }
            str.Close();
            for (int i = 0; i < 15; i++)
            {
                if (i >= 0 && i <= 6)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(erico[i]);
                    Console.ResetColor();
                }

                else if (i >= 7 && i <= 12)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(erico[i]);
                    Console.ResetColor();
                }
                else if (i == 14)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(erico[i]);
                    Console.ResetColor();
                }
            }
        }
        static void loadingScreen()
        {
            string[] erico = new string[12];
            StreamReader str = new StreamReader(Direct("logo.txt"));
            for (int i = 0; i < 12; i++)
            {
                erico[i] = str.ReadLine();
            }
            str.Close();
            Console.WriteLine();
            Console.CursorVisible = false;
            Console.WriteLine("\t\t\tSELAMAT DATANG DI");
            for (int i = 0; i < 12; i++)
            {
                foreach (char j in erico[i])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(j);
                    Thread.Sleep(1);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Thread.Sleep(800);
            Console.Clear();
            Thread.Sleep(300);
            Console.WriteLine();
            Console.WriteLine("\t\t\tSELAMAT DATANG DI");
            foreach (string i in erico)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(i);
                Console.ResetColor();
            }
            Thread.Sleep(800);
            Console.Clear();
            Thread.Sleep(300);
            Console.WriteLine();
            Console.WriteLine("\t\t\tSELAMAT DATANG DI");
            foreach (string i in erico)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(i);
                Console.ResetColor();
            }
            Thread.Sleep(800);
            Console.Clear();
            Thread.Sleep(400);
            Console.CursorVisible = true;
        }
        static void ThankYou()
        {
            Console.CursorVisible = false;
            Console.WriteLine();
            int i;
            string pesan = "TERIMA KASIH TELAH MENGGUNAKAN LAYANAN KAMI :)";
            for (i = 0; i < Console.WindowWidth - pesan.Length - 1; i++)
            {
                Console.CursorLeft = i;
                Console.Write("  " + pesan);
                Thread.Sleep(75);
            }
            Thread.Sleep(500);
            Console.Clear();
        }
        public static void tampilan1()
        {
            Console.WriteLine("\t+---------------------------------------+");
            Console.WriteLine("\t|       Silahkan Pilih Transaksi        |");
            Console.WriteLine("\t|                                       |");
            Console.WriteLine("\t|    1. Pembayaran / Top-Up Pulsa       |");
            Console.WriteLine("\t|    2. Transfer                        |");
            Console.WriteLine("\t|    3. Transaksi Lainnya               |");
            Console.WriteLine("\t|    4. Masukkan Ulang Pin              |");
            Console.WriteLine("\t+---------------------------------------+");
        }
        public static void tampilan2()
        {
            Console.WriteLine("\t     +-----------------------------+");
            Console.WriteLine("\t     |       Transaksi Lainnya     |");
            Console.WriteLine("\t     |                             |");
            Console.WriteLine("\t     |      1. Ganti Pin           |");
            Console.WriteLine("\t     |      2. Info Kurs           |");
            Console.WriteLine("\t     |      3. Tarik Tunai         |");
            Console.WriteLine("\t     |      4. Info Saldo          |");
            Console.WriteLine("\t     |      5. Kembali             |");
            Console.WriteLine("\t     +-----------------------------+");
        }

        public static void tampilan3()
        {
            Console.WriteLine("\t+-----------------------------------+");
            Console.WriteLine("\t|       Silahkan Pilih Provider     |");
            Console.WriteLine("\t|                                   |");
            Console.WriteLine("\t| 1. Telkomsel         2. XL        |");
            Console.WriteLine("\t| 3. Axis              4. SmartFren |");
            Console.WriteLine("\t| 5. Indosat           6. Tri       |");
            Console.WriteLine("\t| 7. Kembali                        |");
            Console.WriteLine("\t+-----------------------------------+");
        }

        public static void tampilan4()
        {
            Console.WriteLine("\t+--------------------+------------------+");
            Console.WriteLine("\t|      Mata Uang     |     Kurs Beli    |");
            Console.WriteLine("\t+--------------------+------------------+");
            Console.WriteLine("\t|       SGD ($)      |     10,246.00    |");
            Console.WriteLine("\t|       USD ($)      |     14,030.00    |");
            Console.WriteLine("\t|       THB (B)      |        465.89    |");
            Console.WriteLine("\t|       MYR (RM)     |      3,377.00    |");
            Console.WriteLine("\t|       JPY (¥)      |        129.64    |");
            Console.WriteLine("\t+--------------------+------------------+");
        }

        static void struk1(Provider a, long b, string c)
        {
            long ba;
            DateTime y = new DateTime();
            y = DateTime.Now;
            Console.WriteLine("\t+---------------------------------------+");
            Console.WriteLine("\t              Bukti Pembayaran                     ");
            Console.WriteLine("\t                                                   ");
            Console.WriteLine($"\t     {y.ToString("dd/MM/yy")}  {y.ToString("HH:mm")}");
            Console.WriteLine("\t                                                   ");
            Console.WriteLine($"\t    Provider     : {a}                    ");
            Console.WriteLine($"\t    Nomor HP     : {c}                    ");
            Console.WriteLine($"\t    Jumlah Pulsa : Rp.{b.ToString("#,###,###,###.00")}                 ");
            Console.WriteLine($"\t    Biaya Admin  : Rp.{(ba = b * 6 / 100).ToString("#,###,###,###.00")}");
            Console.WriteLine($"\t    Total        : Rp.{(ba + b).ToString("#,###,###,###.00")}            ");
            Console.WriteLine($"\t+---------------------------------------+");
        }

        static void struk2(long a, long b)
        {
            Random rnd = new Random();
            long ba;
            DateTime y = new DateTime();
            y = DateTime.Now;
            Console.WriteLine("\t+--------------------------------------+");
            Console.WriteLine("\t                                                        ");
            Console.WriteLine($"\t          {y.ToString("dd/MM/yy")}  {y.ToString("HH:mm")}");
            Console.WriteLine("\t                                                        ");
            Console.WriteLine($"\t         No. Record = {rnd.Next(0,5000)}               ");
            Console.WriteLine($"\t         Penarikan  = Rp.{a.ToString("#,###,###,###.00")}                           ");
            Console.WriteLine($"\t                                                       ");
            Console.WriteLine($"\t         Saldo      = Rp.{b.ToString("#,###,###,###.00")}                           ");
            Console.WriteLine("\t                                                        ");
            Console.WriteLine($"\t+--------------------------------------+");
        }

        static void struk3(string a, string b, long c)
        {
            long ba;
            DateTime y = new DateTime();
            y = DateTime.Now;
            Console.WriteLine("\t+---------------------------------------+");
            Console.WriteLine("\t            Informasi Transfer                     ");
            Console.WriteLine("\t                                                   ");
            Console.WriteLine($"\t     {y.ToString("dd/MM/yy")}  {y.ToString("HH:mm")}");
            Console.WriteLine("\t                                                   ");
            Console.WriteLine($"\t    Dari No. Rekening = {a}                    ");
            Console.WriteLine($"\t    Ke   No. Rekening = {b}                    ");
            Console.WriteLine($"\t                                               ");
            Console.WriteLine($"\t    Jumlah            = Rp.{c.ToString("#,###,###,###.00")}                    ");
            Console.WriteLine($"\t+---------------------------------------+");
        }
        static void kursExchange(double a)
        {
            Console.WriteLine();
            Console.WriteLine("   Dollar Singapore (SGD)      : {0:F2}", a / 10246);
            Console.WriteLine("   Dollar AS (USD)             : {0:F2}", a / 14030);
            Console.WriteLine("   Baht Thailand (THB)         : {0:F2}", a / 465.89);
            Console.WriteLine("   Ringgit Malaysia (MYR)      : {0:F2}", a / 3377);
            Console.WriteLine("   Yen Jepang (JPY)            : {0:F2}", a / 129.64);
        }
        static void Animation()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine();
                Console.Write("   Loading ");
                Thread.Sleep(600);
                Console.Write(".");
                Thread.Sleep(600);
                Console.Write(".");
                Thread.Sleep(600);
                Console.Write(".");
                Thread.Sleep(600);
                Console.Write(".");
                Console.Clear();
            }
        }

        private static SecureString maskInputString()
        {
            SecureString pass = new SecureString();
            ConsoleKeyInfo keyinfo;
            Console.ForegroundColor = ConsoleColor.Green;
            do
            {
                keyinfo = Console.ReadKey(true);
                if (!char.IsControl(keyinfo.KeyChar))
                {
                    pass.AppendChar(keyinfo.KeyChar);
                    Console.Write("X");
                }
                else if (keyinfo.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass.RemoveAt(pass.Length - 1);
                    Console.Write("\b \b");
                }
            } while (keyinfo.Key != ConsoleKey.Enter);

            Console.ResetColor();
            return pass;
        }

        static void Main(string[] args)
        {
            Console.Title = "Erico ATM";
            int stats = 0;
            string pin;
            do
            {
                Console.WriteLine("Masukkan pin (6 digit) :\n");
                Console.Write("\t");
                SecureString pass = maskInputString();
                pin = new NetworkCredential(string.Empty, pass).Password;
                
                if (CekPin(pin))
                {
                    Console.Clear();
                    loadingScreen();
                    do
                    {
                        try
                        {
                            stats = 1;
                            byte choose;
                            Console.Clear();
                            Welcome();
                            Console.WriteLine();
                            tampilan1();
                            Console.WriteLine();
                            Console.Write("   Pilihan Anda : ");
                            choose = Convert.ToByte(Console.ReadLine());

                            if (choose <= 0 || choose > 5)
                            {
                                stats = 1;
                                Console.Clear();
                            }

                            else if(choose == 1)
                            {
                                do
                                {
                                    try
                                    {
                                        stats = 2;
                                        long pulsa, saldoSekarang;
                                        saldoSekarang = CekSaldo(pin);
                                        byte choose1;
                                        Provider jaringan;
                                        Console.Clear();
                                        Console.WriteLine();
                                        tampilan3();
                                        Console.WriteLine();
                                        Console.Write("   Pilihan Anda : ");
                                        choose1 = Convert.ToByte(Console.ReadLine());

                                        if (choose1 <= 0 || choose1 > 7)
                                        {
                                            stats = 2;
                                            Console.Clear();
                                        }
                                        else if(choose1 == 7)
                                        {
                                            stats = 1;
                                        }
                                        else
                                        {
                                            string nomorTelepon, kodeNomor;
                                            jaringan = (Provider)choose1;
                                            Console.WriteLine();
                                            Console.Write("   Silahkan Masukkan Nomor Telepon                                         : ");
                                            nomorTelepon = Console.ReadLine();
                                            kodeNomor = nomorTelepon.Substring(0, 4);
                                            if (nomorTelepon.Length != 12)
                                            {
                                                Console.WriteLine("   Nomor Telepon salah, harap masukkan ulang ...");
                                                stats = 2;
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else if (jaringan == Provider.Telkomsel && (kodeNomor !="0811" && kodeNomor != "0812" && kodeNomor != "0813" && kodeNomor != "0821" && kodeNomor != "0822" && kodeNomor != "0852" && kodeNomor != "0853" && kodeNomor != "0823" && kodeNomor != "0851"))
                                            {
                                                Console.WriteLine("   Ini bukan nomor Telkomsel, harap masukkan ulang ...");
                                                stats = 2;
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else if (jaringan == Provider.XL && (kodeNomor != "0817" && kodeNomor != "0818" && kodeNomor != "0819" && kodeNomor != "0859" && kodeNomor != "0877" && kodeNomor != "0878"))
                                            {
                                                Console.WriteLine("   Ini bukan nomor XL, harap masukkan ulang ...");
                                                stats = 2;
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else if(jaringan == Provider.Axis && (kodeNomor != "0838" && kodeNomor != "0831" && kodeNomor != "0832" && kodeNomor != "0833"))
                                            {
                                                Console.WriteLine("   Ini bukan nomor Axis, harap masukkan ulang ...");
                                                stats = 2;
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else if (jaringan == Provider.SmartFren && (kodeNomor != "0881" && kodeNomor != "0882" && kodeNomor != "0883" && kodeNomor != "0884" && kodeNomor != "0885" && kodeNomor != "0886" && kodeNomor != "0887" && kodeNomor != "0888" && kodeNomor != "0889"))
                                            {
                                                Console.WriteLine("   Ini bukan nomor SmartFren, harap masukkan ulang ...");
                                                stats = 2;
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else if(jaringan == Provider.Indosat && (kodeNomor != "0814" && kodeNomor != "0815" && kodeNomor != "0816" && kodeNomor != "0855" && kodeNomor != "0856" && kodeNomor != "0857" && kodeNomor != "0858"))
                                            {
                                                Console.WriteLine("   Ini bukan nomor Indosat, harap masukkan ulang ...");
                                                stats = 2;
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else if (jaringan == Provider.Tri && (kodeNomor != "0895" && kodeNomor != "0896" && kodeNomor != "0897" && kodeNomor != "0898" && kodeNomor != "0899"))
                                            {
                                                Console.WriteLine("   Ini bukan nomor Tri, harap masukkan ulang ...");
                                                stats = 2;
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else
                                            {
                                                Console.Write("   Silahkan Masukkan Nominal Pulsa (Maximal = Rp. 100000 pecahan Rp. 5000) : Rp.");
                                                pulsa = Convert.ToInt64(Console.ReadLine());
                                                if (CekSaldo(pin) < pulsa || CekSaldo(pin) <= 50000 || saldoSekarang - pulsa < 50000)
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("   Saldo anda tidak cukup, top-up tidak bisa dilakukan ...");
                                                    stats = 2;
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                                else if(pulsa % 5000 != 0 || pulsa > 100000)
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("   Pilihan Nominal Tidak Ada, Harap Masukkan Ulang ...");
                                                    stats = 2;
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                                else
                                                {
                                                    do
                                                    {
                                                        stats = 3;
                                                        saldoSekarang -= pulsa + pulsa * 6 / 100;
                                                        UpdateSaldo(pin, saldoSekarang);
                                                        Console.WriteLine();
                                                        Console.Clear();
                                                        struk1(jaringan, pulsa, nomorTelepon);
                                                        byte choose3;
                                                        do
                                                        {
                                                            Console.WriteLine();
                                                            Console.WriteLine("\tApakah anda mau melakukan transaksi lain ?\n\t\t1. YA \t 2. TIDAK");
                                                            Console.WriteLine();
                                                            Console.Write("\tPilihan Anda : ");
                                                            choose3 = Convert.ToByte(Console.ReadLine());

                                                            if (choose3 == 1)
                                                            {
                                                                stats = 1;
                                                                Console.Clear();
                                                                break;
                                                            }
                                                            else if (choose3 == 2)
                                                            {
                                                                ThankYou();
                                                                stats = 4;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                stats = 3;
                                                                Console.Clear();
                                                                break;
                                                            }
                                                        } while (choose3 != 1 || choose3 != 2);
                                                    } while (stats == 3);
                                                }
                                            }
                                        }
                                    }
                                    catch (OverflowException)
                                    {
                                        stats = 2;
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        Console.WriteLine("   Nomor Telepon salah, harap masukkan ulang ...");
                                        stats = 2;
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    catch (FormatException)
                                    {
                                        stats = 2;
                                    }
                                } while (stats == 2);
                            }

                            else if (choose == 2)
                            {
                                do
                                {
                                    long saldoPengirim, saldoPenerima, jumlahPengiriman;
                                    string NoRekening, NoRekening2;
                                    stats = 2;
                                    Console.Clear();
                                    Console.WriteLine();
                                    Console.Write("Masukkan Nomor Rekening Tujuan                                  : ");
                                    NoRekening = Console.ReadLine();
                                    NoRekening2 = GetRekeningbyPin(pin);
                                    if (NoRekening.Length != 10)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Nomor Rekening Salah, Harap Masukkan Ulang ...");
                                        Console.ReadKey();
                                        stats = 2;
                                    }
                                    else if (!CekRekening(NoRekening))
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Nomor Rekening Tidak Terdaftar, Harap Masukkan Ulang ...");
                                        Console.ReadKey();
                                        stats = 2;
                                    }
                                    else
                                    {
                                        Console.Write("Masukkan Jumlah Pengiriman (Minimal Rp. 10000 pecahan Rp.1000) : Rp.");
                                        jumlahPengiriman = Convert.ToInt64(Console.ReadLine());
                                        if(CekSaldo(pin) < jumlahPengiriman || CekSaldo(pin) < 50000 || CekSaldo(pin) - jumlahPengiriman < 50000)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Saldo Anda Tidak Cukup, Harap Masukkan ulang ...");
                                            Console.ReadKey();
                                            stats = 2;
                                        }
                                        else if (jumlahPengiriman <= 10000 || jumlahPengiriman % 1000 != 0)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Jumlah Pengiriman tidak sesuai, Harap Masukkan Ulang ...");
                                            Console.ReadKey();
                                            stats = 2;
                                        }
                                        else
                                        {
                                            saldoPengirim = CekSaldo(pin) - jumlahPengiriman;
                                            saldoPenerima = CekSaldo(GetPinbyRekening(NoRekening)) + jumlahPengiriman;
                                            UpdateSaldo(pin, saldoPengirim);
                                            UpdateSaldo(GetPinbyRekening(NoRekening), saldoPenerima);
                                            Console.Clear();
                                            Animation();
                                            Console.Clear();
                                            do
                                            {
                                                stats = 3;
                                                Console.WriteLine();
                                                struk3(NoRekening2, NoRekening, jumlahPengiriman);
                                                Console.WriteLine();
                                                byte choose3;
                                                do
                                                {
                                                    Console.WriteLine("\tApakah anda mau melakukan transaksi lain ?\n\t\t1. YA \t 2. TIDAK");
                                                    Console.WriteLine();
                                                    Console.Write("\tPilihan Anda : ");
                                                    choose3 = Convert.ToByte(Console.ReadLine());

                                                    if (choose3 == 1)
                                                    {
                                                        stats = 1;
                                                        Console.Clear();
                                                        break;
                                                    }
                                                    else if (choose3 == 2)
                                                    {
                                                        ThankYou();
                                                        stats = 4;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        stats = 3;
                                                        Console.Clear();
                                                        break;
                                                    }
                                                } while (choose3 != 1 || choose3 != 2);
                                            } while (stats == 3);
                                        }
                                    }
                                } while (stats == 2);
                            }

                            else if (choose == 3)
                            {
                                do
                                {
                                    try
                                    {
                                        stats = 2;
                                        byte choose4;
                                        Console.Clear();
                                        Welcome();
                                        Console.WriteLine();
                                        tampilan2();
                                        Console.WriteLine();
                                        Console.Write("   Pilihan Anda : ");
                                        choose4 = Convert.ToByte(Console.ReadLine());

                                        if (choose4 <= 0 || choose4 > 5)
                                        {
                                            stats = 2;
                                            Console.Clear();
                                        }

                                        else if (choose4 == 1)
                                        {
                                            do
                                            {
                                                stats = 3;
                                                string pinNow, pinThen;
                                                Console.Clear();
                                                Console.WriteLine();
                                                Console.Write("   Silahkan Masukkan Pin Lama Anda : ");
                                                SecureString temp = maskInputString();
                                                pinNow = new NetworkCredential(string.Empty, temp).Password;
                                                if (CekPin(pinNow) && pinNow == pin)
                                                {
                                                    Console.WriteLine();
                                                    Console.Write("   Silahkan masukkan Pin Baru Anda : ");
                                                    SecureString temp1 = maskInputString();
                                                    pinThen = new NetworkCredential(string.Empty, temp1).Password;
                                                    if (pinThen.Length != 6)
                                                    {
                                                        Console.WriteLine();
                                                        Console.WriteLine("   Pin yang Anda masukan terlalu Panjang/Pendek, silahkan masukan ulang pin ...");
                                                        Console.ReadKey();
                                                        stats = 3;
                                                    }
                                                    else if (!validasiPin(pinThen))
                                                    {
                                                        Console.WriteLine();
                                                        Console.WriteLine("   Pin Hanya boleh Berupa Angka, Harap masukkan ulang ...");
                                                        Console.ReadKey();
                                                        stats = 3;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("\n");
                                                        UpdatePin(pin, pinThen);
                                                        Console.WriteLine("   Pin Anda Sukses Diganti, Silahkan Masukkan Kembali Ulang Pin ...");
                                                        Console.ReadKey();
                                                        stats = 0;
                                                        Console.Clear();
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("   Pin Anda Tidak Cocok, Silahkan Masukkan Ulang ...");
                                                    Console.ReadKey();
                                                    stats = 3;
                                                }
                                            } while (stats == 3);
                                        }

                                        else if (choose4 == 2)
                                        {
                                            do
                                            {
                                                double money;
                                                stats = 3;
                                                Console.Clear();
                                                Console.WriteLine();
                                                tampilan4();
                                                Console.WriteLine();
                                                Console.Write("   Masukkan Nilai Rupiah (IDR) : ");
                                                money = Convert.ToInt64(Console.ReadLine());
                                                kursExchange(money);
                                                stats = 4;
                                                Console.WriteLine();
                                                byte choose3;
                                                do
                                                {
                                                    Console.WriteLine("\t1. Info Kurs \t 2. Transaksi Lain\n\t3. Keluar");
                                                    Console.WriteLine();
                                                    Console.Write("\tPilihan Anda : ");
                                                    choose3 = Convert.ToByte(Console.ReadLine());

                                                    if (choose3 == 1)
                                                    {
                                                        stats = 3;
                                                        Console.Clear();
                                                        break;
                                                    }
                                                    else if (choose3 == 2)
                                                    {
                                                        stats = 2;
                                                        Console.Clear();
                                                        break;
                                                    }
                                                    else if (choose3 == 3)
                                                    {
                                                        ThankYou();
                                                        stats = 4;
                                                        break;
                                                    }
                                                } while (choose3 != 1 || choose3 != 2 || choose3 != 3);
                                            } while (stats == 3);
                                        }

                                        else if (choose4 == 3)
                                        {
                                            do
                                            {
                                                long penarikan, saldoNow = CekSaldo(pin);
                                                stats = 3;
                                                Console.Clear();
                                                Console.WriteLine();
                                                Console.Write("   Masukkan Jumlah Uang yang Ingin Ditarik (Pecahan Rp. 50000) : Rp.");
                                                penarikan = Convert.ToInt64(Console.ReadLine());
                                                if ( saldoNow < 50000 || saldoNow - penarikan < 50000)
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("   Saldo Anda tidak cukup, Harap Masukkan Ulang Jumlah Penarikan ... ");
                                                    Console.ReadKey();
                                                    stats = 3;
                                                    Console.Clear();
                                                }
                                                else if (penarikan > saldoNow || penarikan % 50000 != 0)
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("   Penarikan Tidak Dapat Dilakukan, Harap Masukkan Ulang Jumlah Penarikan ... ");
                                                    Console.ReadKey();
                                                    stats = 3;
                                                    Console.Clear();
                                                }
                                                else
                                                {
                                                    saldoNow -= penarikan;
                                                    UpdateSaldo(pin, saldoNow);
                                                    Console.Clear();
                                                    do
                                                    {
                                                        stats = 4;
                                                        struk2(penarikan, CekSaldo(pin));
                                                        Console.WriteLine();
                                                        byte choose3;
                                                        do
                                                        {
                                                            Console.WriteLine("\tApakah anda mau melakukan transaksi lain ?\n\t\t1. YA \t 2. TIDAK");
                                                            Console.WriteLine();
                                                            Console.Write("\tPilihan Anda : ");
                                                            choose3 = Convert.ToByte(Console.ReadLine());

                                                            if (choose3 == 1)
                                                            {
                                                                stats = 2;
                                                                Console.Clear();
                                                                break;
                                                            }
                                                            else if (choose3 == 2)
                                                            {
                                                                ThankYou();
                                                                stats = 5;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                stats = 4;
                                                                Console.Clear();
                                                                break;
                                                            }
                                                        } while (choose3 != 1 || choose3 != 2);
                                                    } while (stats == 4);
                                                }
                                            } while (stats == 3);
                                        }

                                        else if (choose4 == 4)
                                        {
                                            do
                                            {


                                                stats = 3;
                                                Console.Clear();
                                                byte choose3;
                                                Console.WriteLine();
                                                Console.WriteLine($"\t Saldo anda saat ini adalah : Rp.{CekSaldo(pin).ToString("#,###,###,###.00")}");
                                                Console.WriteLine();
                                                do
                                                {

                                                    Console.WriteLine("\t Apakah anda mau melakukan transaksi lain ?\n\t\t1. YA \t 2. TIDAK");
                                                    Console.WriteLine();
                                                    Console.Write("\tPilihan Anda : ");
                                                    choose3 = Convert.ToByte(Console.ReadLine());

                                                    if (choose3 == 1)
                                                    {
                                                        stats = 2;
                                                        Console.Clear();
                                                        break;
                                                    }
                                                    else if (choose3 == 2)
                                                    {
                                                        ThankYou();
                                                        stats = 4;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        stats = 3;
                                                        Console.Clear();
                                                        break;
                                                    }
                                                } while (choose3 != 1 || choose3 != 2);
                                            } while (stats == 3);
                                        }
                                        else if (choose4 == 5)
                                        {
                                            stats = 1;
                                        }
                                    }
                                    catch (OverflowException)
                                    {
                                        stats = 2;
                                    }
                                    catch (FormatException)
                                    {
                                        stats = 2;
                                    }
                                } while (stats == 2);
                            }

                            else if(choose == 4)
                            {
                                stats = 0;
                                Console.Clear();
                            }
                        }
                        catch (OverflowException)
                        {
                            stats = 1;
                        }
                        catch (FormatException)
                        {
                            stats = 1;
                        }
                    } while (stats == 1);
                }
                else
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Pin tidak ada, silahkan masukkan ulang pin ...");
                    Console.ReadKey();
                    Console.Clear();
                    stats = 0;
                }
            } while (stats == 0);
            Console.ReadKey();
        }
    }
}
