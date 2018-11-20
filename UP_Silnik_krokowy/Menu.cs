using System;

namespace UP_Silnik_krokowy
{
    class Menu
    {
        ConnectionHandler ch = new ConnectionHandler();

        public void ShowMenu()
        {
            Console.WriteLine("1. Obrot w prawo o ilosc krokow");
            Console.WriteLine("2. Obrot w lewo o ilosc krokow");
            Console.WriteLine("3. Obrot w prawo o kat");
            Console.WriteLine("4. Obrot w lewo o kat");
            Console.WriteLine("5. Obrot lewej w dol o ilosc krokow:");
            Console.WriteLine("6. Obrot prawej w dol o ilosc krokow:");
            Console.WriteLine("7. Obrot lewej w dol o kat:");
            Console.WriteLine("8. Obrot prawej w dol o kat:");
            var input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                Console.WriteLine("Ile krokow w prawo wykonac: ");
                var inputStepsCount = int.Parse(Console.ReadLine());
                ch.Step(inputStepsCount, 100, ch.stepRightBytes);
                ch.Disconnect();
            }
            else if (input == 2)
            {
                Console.WriteLine("Ile krokow w lewo wykonac: ");
                var inputStepsCount = int.Parse(Console.ReadLine());
                ch.Step(inputStepsCount, 100, ch.stepLeftBytes);
                ch.Disconnect();
            }
            else if (input == 3)
            {
                Console.WriteLine("O jaki kat obrocic w prawo: ");
                var inputAngle = int.Parse(Console.ReadLine());
                var stepsCount = (int)(inputAngle / 7.5f);
                ch.Step(stepsCount, 100, ch.stepRightBytes);
                ch.Disconnect();
            }
            else if (input == 4)
            {
                Console.WriteLine("O jaki kat obrocic w lewo: ");
                var inputAngle = int.Parse(Console.ReadLine());
                var stepsCount = (int)(inputAngle / 7.5f);
                ch.Step(stepsCount, 100, ch.stepLeftBytes);
                ch.Disconnect();
            }
            else if (input == 5)
            {
                Console.WriteLine("O ile krokow lewa w dol: ");
                var inputStepsCount = int.Parse(Console.ReadLine());
                ch.Step(inputStepsCount, 100, ch.stepLeftDownBytes);
                ch.Step(1, 100, ch.disconnectBytes);

            }
            else if (input == 6)
            {
                Console.WriteLine("O ile krokow prawa w dol: ");
                var inputStepsCount = int.Parse(Console.ReadLine());
                ch.Step(inputStepsCount, 100, ch.stepRightDownBytes);
                ch.Disconnect();
            }
            else if (input == 7)
            {
                Console.WriteLine("O jaki kat obrocic lewa w dol: ");
                var inputAngle = int.Parse(Console.ReadLine());
                var stepsCount = (int)(inputAngle / 7.5f * 5);
                ch.Step(stepsCount, 100, ch.stepLeftDownBytes);
                ch.Disconnect();
            }
            else if (input == 8)
            {
                Console.WriteLine("O jaki kat obrocic prawa w dol: ");
                var inputAngle = int.Parse(Console.ReadLine());
                var stepsCount = (int)(inputAngle / 7.5f * 5);
                ch.Step(stepsCount, 100, ch.stepRightDownBytes);
                ch.Disconnect();
            }
        }
    }
}
