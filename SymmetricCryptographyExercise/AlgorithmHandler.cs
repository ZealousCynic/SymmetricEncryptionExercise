using System.Security.Cryptography;

namespace SymmetricCryptographyExercise
{
    class AlgorithmHandler
    {
        public SymmetricAlgorithm GetSymmetricEncryptor(AlgorithmType _type) 
        {
            SymmetricAlgorithm toRet;
            AlgorithmInitializer init;
            switch(_type)
            {
                case AlgorithmType.DES:
                    toRet = DES.Create();
                    init = new AlgorithmInitializer(8);
                    break;
                case AlgorithmType.TDES:
                    toRet = TripleDES.Create();
                    init = new AlgorithmInitializer(16,8, "./tdes/");
                    break;
                case AlgorithmType.AES:
                    toRet = Aes.Create();
                    init = new AlgorithmInitializer(16,8, "./aes/");
                    break;
                default:
                    toRet = null;
                    init = null;
                    break;
            }
            init.Initalize(toRet);

            return toRet;
        }
    }
}
