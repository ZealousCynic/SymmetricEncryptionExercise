namespace SymmetricCryptographyExercise
{
    class Program
    {

        static void Main(string[] args)
        {
            UI ui = new UI();

            while (true)
            {
                ui.StartUp();                

                ui.Menu();
            }
        }
    }
}
