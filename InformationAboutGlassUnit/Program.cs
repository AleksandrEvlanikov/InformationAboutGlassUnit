namespace InformationAboutGlassUnit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string _connectionString = "Server=localhost;Port=3306;Database=aboutglassunit;User ID=root;Password=123456789Sasha;";

            BDGlassPacketSQL bDGlassPacketSQL = new BDGlassPacketSQL(_connectionString);

            Console.WriteLine("Введите режим работы программы \n" +
                              "1. Создать таблицу в БД.\n" +
                              "2. Добавить новый стеклопакет.\n" +
                              "3. Вывести по артиклю.");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int mode))
            {
                Console.WriteLine("Неверный режим работы");
                return;
            }
            switch (mode)
            {
                case 0:
                    bDGlassPacketSQL.DropTableInformationGlassPacket();
                    Console.WriteLine("Таблица удалена из БД");
                    break;
                case 1:
                    bDGlassPacketSQL.CreateGlassPacketTable();
                    Console.WriteLine("Таблица в БД, создана.");
                    break;
                case 2:
                    Console.WriteLine("Введите Артикул: ");
                    string inputArticle = Console.ReadLine();
                    Console.WriteLine("Введите камерность, толщину СП, толщину стекла через пробел: ");
                    string inputInfoGP = Console.ReadLine();
                    string[] InfoGP = inputInfoGP.Split(" ");
                    if (inputArticle != null)
                    {
                        if (InfoGP.Length >= 3)
                        {
                            GlassPacket glassPacket = new GlassPacket
                            {
                                ArticleGP = inputArticle,
                                Chambering = int.Parse(InfoGP[0]),
                                ThicknessGP = double.Parse(InfoGP[1]),
                                ThicknessGlass = double.Parse(InfoGP[2])
                            };
                            bDGlassPacketSQL.CreateGlassPacket(glassPacket);
                            Console.WriteLine("Стеклопакет создан.");
                        }
                    }
                    else Console.WriteLine("Некоректные значение стеклопакета");

                    break;
                case 3:
                    Console.WriteLine("Введите Артикул для поиска в БД: ");
                    inputArticle = Console.ReadLine();
                    bDGlassPacketSQL.GetGlassPacketByArticle(inputArticle);
                    break;
                default:
                    Console.WriteLine("Некоректный режим работы программы");
                    break;
            }
        }
    }
}
