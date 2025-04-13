using System.Text;
using System.Text.RegularExpressions;
using TextCopy;

namespace PasswordGen
{
    class Program
    {
        static void Main(string[] args)
        {

            //Cool Ascii
            string title = @" ___                                  _ 
| . \ ___  ___ ___ _ _ _  ___  _ _  _| |
|  _/<_> |<_-<<_-<| | | |/ . \| '_>/ . |
|_|  <___|/__//__/|__/_/ \___/|_|  \___|
                                        
 ___                             _            
/  _>  ___ ._ _  ___  _ _  ___ _| |_ ___  _ _ 
| <_/\/ ._>| ' |/ ._>| '_><_> | | | / . \| '_>
`____/\___.|_|_|\___.|_|  <___| |_| \___/|_| 

Questo programma genererà una password alfanumerica.
____________________________________________________
";

            Console.WriteLine(title);


            // Start
            Console.Write("Definisci Lunghezza Password: ");
            //int length = Convert.ToInt32(Console.ReadLine());

            int length = 0;
            do
            {        
                int.TryParse(Console.ReadLine(), out length);
                if (length > 127 || length < 7)
                {
                    Console.WriteLine("La password non ha i requisiti di sicurezza necessari: Caratteri Minimi: 8, Caratteri Massimi: 127.");
                }

                else
                {
                   GeneratePassword(length);
                   Console.WriteLine("Password generata e copiata automaticamente agli appunti"); // pw doesn't show for privacy sake, and it's already copied to clipboard
                }

            } while (length > 7 || length < 128);
          
        }
        public static string GeneratePassword(int length)
        {
            const string chars = "0123456789" 
                + "abdcefghilmnopqrstuvwxyz" 
                + "ABCDEFGHILMNOPQRSTUVWXYZ" 
                + "#$%&'()*+,-./:;<=>?@[]^_`{|}~"; // could've used regex

            StringBuilder sb = new StringBuilder();
            Random randomize = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = randomize.Next(chars.Length);
                sb.Append(chars[index]);
            }

            if (length > 127 || length < 7)
            {
                return "";
            }

            var generatedvalue = sb.ToString(); // convert int value to string
            ClipboardService.SetText(generatedvalue); // use textcopy package and pass the generated value in it

            return generatedvalue;
        }

    }  
}