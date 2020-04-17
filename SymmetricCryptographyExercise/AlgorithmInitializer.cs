using System.IO;
using System.Security.Cryptography;

namespace SymmetricCryptographyExercise
{
    class AlgorithmInitializer
    {
        string keypath = "key.txt";
        string ivpath = "iv.txt";
        int keylength;
        int ivlength;
        public AlgorithmInitializer(int keylength)
        {
            this.keylength = keylength;
        }

        public AlgorithmInitializer(int keylength, string path) : this(keylength)
        {
            Directory.CreateDirectory(path);
            string temp = keypath;
            keypath = path + temp;
            temp = ivpath;
            ivpath = path + ivpath;
        }

        public AlgorithmInitializer(int keylength, int ivlength) : this(keylength)
        {
            this.ivlength = ivlength;
        }

        public AlgorithmInitializer(int keylength, int ivlength, string path) : this(keylength, ivlength)
        {
            Directory.CreateDirectory(path);
            string temp = keypath;
            keypath = path + temp;
            temp = ivpath;
            ivpath = path + ivpath;
        }

        public int KeyLength { get { return keylength; } set { keylength = value; } }
        public string KeyPath { get { return keypath; } set { keypath = value; } }
        public string IVPath { get { return ivpath; } set { ivpath = value; } }
        public SymmetricAlgorithm Initalize(SymmetricAlgorithm toInit)
        {
            toInit.Key = GetBytes(KeyPath, keylength);
            toInit.IV = GetBytes(IVPath, ivlength);
            toInit.Mode = CipherMode.CBC;
            toInit.Padding = PaddingMode.PKCS7;

            //toInit.GenerateIV();

            return toInit;
        }

        byte[] GetBytes(string path, int length)
        {
            byte[] data;
            if (File.Exists(path))
                data = GetFromFile(path);
            else
            {
                data = GenerateKey(length);
                SaveBytes(path, data);
            }
            return data;
        }

        byte[] GetFromFile(string path)
        {
            byte[] data = File.ReadAllBytes(path);
            return data;
        }

        byte[] GenerateKey(int length)
        {
            byte[] key = new byte[length];

            using(RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            return key;
        }

        void SaveBytes(string path, byte[] key)
        {
            File.WriteAllBytes(path, key);
        }
    }
}
