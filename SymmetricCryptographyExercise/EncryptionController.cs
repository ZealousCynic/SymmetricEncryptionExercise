using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace SymmetricCryptographyExercise
{
    class EncryptionController
    {
        UI ui;
        SymmetricAlgorithm alg;
        Stopwatch encryptWatch;
        Stopwatch decryptWatch;
        ENCServiceByteHandle handle;

        bool hasEncrypted = false;

        string encryptedMessageString;
        long encryptionTime;
        long decryptionTime;

        public SymmetricAlgorithm Alg { get { return alg; } set { alg = value; } }

        public long EncryptionTime { get { return encryptionTime; } private set { encryptionTime = value; } }
        public long DecryptionTime { get { return decryptionTime; } private set { decryptionTime = value; } }

        public EncryptionController(UI ui)
        {
            this.ui = ui;
        }

        public void ChooseEncryptionType(int input)
        {
            AlgorithmHandler handler = new AlgorithmHandler();

            if (!Enum.IsDefined(typeof(AlgorithmType), input))
                Alg = handler.GetSymmetricEncryptor(AlgorithmType.INVALID);
            AlgorithmType algorithmChoice = (AlgorithmType)input;

            Alg = handler.GetSymmetricEncryptor(algorithmChoice);

            ui.HandleMenu();
        }

        public void ByteHandle(int input)
        {
            SymmetricEncryptionWorker worker = new SymmetricEncryptionWorker(Alg);

            switch (input)
            {
                case 1:
                    handle = worker.Encrypt;
                    break;
                case 2:
                    handle = null;
                    if (hasEncrypted && input == 2)
                    {
                        handle = worker.Decrypt;
                    }
                    break;
                default:
                    handle = null;
                    break;
            }
        }

        public void DoMagic()
        {
            if (handle is null)
                ui.PrintString("Something went wrong. Handle is null");
            if (!hasEncrypted && !(handle is null)) // <--- Dumbasscheck, means you can't encrypt more until you decrypt -- fix later
            {
                string message = ui.SetMessage();

                encryptWatch = new Stopwatch();
                encryptWatch.Start();

                byte[] data = Encoding.ASCII.GetBytes(message);
                byte[] encryptedMessage = handle(data);
                encryptedMessageString = Convert.ToBase64String(encryptedMessage);

                encryptWatch.Stop();

                encryptionTime = encryptWatch.ElapsedTicks;

                ui.PrintString(encryptedMessageString);
                hasEncrypted = true;
            }
            else if (hasEncrypted && !(handle is null))
            {
                decryptWatch = new Stopwatch();

                decryptWatch.Start();

                byte[] temp = Convert.FromBase64String(encryptedMessageString);
                byte[] decryptedBytes = handle(temp);
                string decryptedMessageString = Encoding.ASCII.GetString(decryptedBytes);

                decryptWatch.Stop();

                decryptionTime = decryptWatch.ElapsedTicks;

                ui.PrintString(decryptedMessageString);
                hasEncrypted = false;
            }
        }
    }
}
