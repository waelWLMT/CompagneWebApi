using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public interface IStringCryptorDecryptor
    {
        public string EncryptString(string text);
        public string DecryptString(string cipherText);
    }
}
