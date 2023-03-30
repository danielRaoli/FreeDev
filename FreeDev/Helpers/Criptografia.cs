using System.Security.Cryptography;
using System.Text;

namespace FreeDev.Helpers
{
    public static class Criptografia
    {
        public static string GerarHash(this string senha)
        {
            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(senha);

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();
            
            foreach(var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }
            return strHexa.ToString();
        }
    }
}
