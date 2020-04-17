using System;

namespace SymmetricCryptographyExercise
{
    class UI
    {
        EncryptionController control;

        public UI()
        {
            control = new EncryptionController(this);
        }

        public void StartUp()
        {
            Console.WriteLine("Symmetric encryptor starting up...\n");

            Console.WriteLine("Please enter the number corresponding to the algorithm you'd like to use...\n\n" +
                "1: DES\n" +
                "2: Tripple Des\n" +
                "3: AES\n");

            int.TryParse(Console.ReadLine(), out int input);

            control.ChooseEncryptionType(input);
        }

        public void HandleMenu()
        {
            Console.WriteLine("Would you like to:\n" +
                "1: Encrypt\n" +
                "2: Decrypt\n\n" +
                "Not that decryption for now requires that you already have something encrypted.\n");

            int.TryParse(Console.ReadLine(), out int input);

            control.ByteHandle(input);

            control.DoMagic();
        }

        public string SetMessage()
        {
            Console.WriteLine("Please enter a message, or accept the default value: \n");

            string message = Console.ReadLine();

            if (string.IsNullOrEmpty(message))
                message = "I am a message";
            return message;
        }

        public void Menu()
        {
            Console.WriteLine("Would you like to:\n\n" +
                    "1: Display Statistics\n" + //This one I'd completely forgotten about -- This program was therefor NOT made with that in mind.
                    "9: Exit\n" +
                    "Anything else: Start over\n");

            int.TryParse(Console.ReadLine(), out int input);
            switch (input)
            {
                case 1:
                    ShowStatistics();
                    break;
                case 9:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        void ShowStatistics()
        {
            //Conversions should be done by logic,but fuck it.
            Console.WriteLine("\nKey: " + Convert.ToBase64String(control.Alg.Key) + "\n" +
                "IV: " + Convert.ToBase64String(control.Alg.IV) + "\n" +
                "Last encryption took: " + control.EncryptionTime + " ms\n" +
                "Last decryption took: " + control.DecryptionTime + "ms\n");
        }

        public void PrintString(string toPrint)
        {
            Console.WriteLine(toPrint);
        }
        


    }
}
