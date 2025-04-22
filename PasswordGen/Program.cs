using System.Text;
using TextCopy;

namespace PasswordGen;

internal class Program
{
    private static void Main(string[] args)
    {
        var title = @" ___                                  _ 
| . \ ___  ___ ___ _ _ _  ___  _ _  _| |
|  _/<_> |<_-<<_-<| | | |/ . \| '_>/ . |
|_|  <___|/__//__/|__/_/ \___/|_|  \___|
                                        
 ___                             _            
/  _>  ___ ._ _  ___  _ _  ___ _| |_ ___  _ _ 
| <_/\/ ._>| ' |/ ._>| '_><_> | | | / . \| '_>
`____/\___.|_|_|\___.|_|  <___| |_| \___/|_| 
____________________________________________________
";

        Console.WriteLine(title);
        Console.Write("Create a password starting from 12 minimum characters: ");

        var length = 0;
        while (length < 12 || length > 127)
        {
            if (!int.TryParse(Console.ReadLine(), out length))
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }

            if (length < 12 || length > 127)
            {
                Console.WriteLine(
                    "Your password doesn't have the necessary security requirements (12 minimum characters, 127 maximum)");
            }
            else
            {
                Console.WriteLine("Password has been copied to clipboard");
                ClipboardService.SetText(
                    GeneratePassword(length)); // use textcopy package and pass the generated value in it
            }
        }

        // Wait for user input before closing
        Console.WriteLine("Press Enter to end the program...");
        Console.ReadLine();
    }

    public static string GeneratePassword(int length)
    {
        const string numbers = "0123456789";
        const string lowerChar = "abdcefghilmnopqrstuvwxyz";
        const string upperChar = "ABCDEFGHILMNOPQRSTUVWXYZ";
        const string special = "#$%&'()*+,-./:;<=>?@[]^_`{|}~";

        var sb = new StringBuilder();
        var randomize = new Random();

        // Ensure at least one character from each category
        sb.Append(numbers[randomize.Next(numbers.Length)]);
        sb.Append(lowerChar[randomize.Next(lowerChar.Length)]);
        sb.Append(upperChar[randomize.Next(upperChar.Length)]);
        sb.Append(special[randomize.Next(special.Length)]);

        var allChars = numbers + lowerChar + upperChar + special;
        // Include 1 of each char + fill the rest of the password with random characters
        for (var i = 4; i < length; i++)
        {
            var index = randomize.Next(allChars.Length);
            sb.Append(allChars[index]);
        }

        // Shuffle the password to ensure randomness
        return new string(sb.ToString().OrderBy(_ => randomize.Next()).ToArray());
    }
}