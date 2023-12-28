namespace Order.Domain.Common;
public static class RandomDataGenerator
{
    public static int GetRandomIntValue()
    {
        Random random = new Random();
        return random.Next();
    }
    public static string GetRandomText()
    {
        Random random = new Random();
        String str = "abcdefghijklmnopqrstuvwxyz";
        int size = 10;
        // Initializing the empty string 
        String ran = "";
        for (int i = 0; i < size; i++)
        {
            // Selecting a index randomly 
            int x = random.Next(26);

            // Appending the character at the  
            // index to the random string. 
            ran = ran + str[x];
        }
        return ran;
    }
}
