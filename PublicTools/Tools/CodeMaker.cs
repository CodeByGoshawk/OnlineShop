namespace PublicTools.Tools;
public static class CodeMaker
{
    public static string MakeRandom()
    {
        var random = new Random();
        var randomValue = random.Next(100000, 999999);
        return randomValue.ToString();
    }
}
