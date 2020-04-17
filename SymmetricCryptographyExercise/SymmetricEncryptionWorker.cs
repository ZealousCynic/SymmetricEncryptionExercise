using System.IO;
using System.Security.Cryptography;

namespace SymmetricCryptographyExercise
{
    public delegate byte[] ENCServiceByteHandle(byte[] msg);
    class SymmetricEncryptionWorker
    {
        SymmetricAlgorithm _alg;
        public SymmetricAlgorithm Alg { get { return _alg; } set { _alg = value; } }

        public SymmetricEncryptionWorker(SymmetricAlgorithm alg)
        {
            _alg = alg;
        }


        #region Delegate dumbness

        public byte[] Encrypt(byte[] msg)
        {
            //Wonder how this will break?
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, _alg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(msg, 0, msg.Length);
                    cs.Close();
                }
                return ms.ToArray();
            }
        }

        public byte[] Decrypt(byte[] msg)
        {
            byte[] toRet = new byte[msg.Length];
            //Wonder how this will break?
            using (MemoryStream ms = new MemoryStream(msg))
            {
                using (CryptoStream cs = new CryptoStream(ms, _alg.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(toRet, 0, msg.Length);
                    cs.Close();
                }
            }
            return toRet;
        }
        #endregion
    }
}
