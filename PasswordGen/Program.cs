using System.Text;
using System.Text.RegularExpressions;
using TextCopy;

namespace PasswordGen
{
    class Program
    {
        static void Main(string[] args)
        {
            string title = @" ___                                  _ 
| . \ ___  ___ ___ _ _ _  ___  _ _  _| |
|  _/<_> |<_-<<_-<| | | |/ . \| '_>/ . |
|_|  <___|/__//__/|__/_/ \___/|_|  \___|
                                        
 ___                             _            
/  _>  ___ ._ _  ___  _ _  ___ _| |_ ___  _ _ 
| <_/\/ ._>| ' |/ ._>| '_><_> | | | / . \| '_>
`____/\___.|_|_|\___.|_|  <___| |_| \___/|_| 
.
____________________________________________________
";

            Console.WriteLine(title);

            // Start
            Console.Write("Create a password starting from 8 minimum characters: ");

            int length = 0;
            while (length < 8 || length > 127)
            {
                if (!int.TryParse(Console.ReadLine(), out length))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                if (length < 8 || length > 127)
                {
                    Console.WriteLine("Your password doesn't have the necessary security requirements (8 min. characters, 127 max)");
                }
                else
                {
                    Console.WriteLine("Password has been copied to clipboard"); // pw doesn't show for privacy sake, and it's already copied to clipboard
                    ClipboardService.SetText(GeneratePassword(length)); // use textcopy package and pass the generated value in it
                }
            }
        }

        public static string GeneratePassword(int length)
        {

            const string numbers = "0123456789";
            const string lower = "abdcefghilmnopqrstuvwxyz";
            const string upper = "ABCDEFGHILMNOPQRSTUVWXYZ";
            const string special = "#$%&'()*+,-./:;<=>?@[]^_`{|}~";

            StringBuilder sb = new StringBuilder();
            Random randomize = new Random();

            // Ensure at least one character from each category
            sb.Append(numbers[randomize.Next(numbers.Length)]);
            sb.Append(lower[randomize.Next(lower.Length)]);
            sb.Append(upper[randomize.Next(upper.Length)]);
            sb.Append(special[randomize.Next(special.Length)]);

            string allChars = numbers + lower + upper + special;
            // Fill the rest of the password with random characters
            for (int i = 4; i < length; i++)
            {
                int index = randomize.Next(allChars.Length);
                sb.Append(allChars[index]);
            }
            // Shuffle the password to ensure randomness
            return new string(sb.ToString().OrderBy(_ => randomize.Next()).ToArray());

            // var generatedvalue = sb.ToString(); // convert int value to string
            // return generatedvalue;
        }

        public static bool IsPasswordStrong(string password)
        {
            return password.Length >= 8 &&
            Regex.IsMatch(password, @"\d") && // Contains a digit
            Regex.IsMatch(password, @"[a-z]") && // Contains a lowercase letter
            Regex.IsMatch(password, @"[A-Z]") && // Contains an uppercase letter
            Regex.IsMatch(password, @"[#$%&'()*+,-./:;<=>?@[\]^_`{|}~]"); // Contains a special character
        }
    }
}