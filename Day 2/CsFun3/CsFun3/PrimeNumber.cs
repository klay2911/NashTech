namespace CsFun3;

public static class PrimeNumber
{
    public static async Task<List<int>> GetPrimesAsync(int start, int end)
    {
        var result = new List<int>();
        await Task.Run(() =>
        {
            for (int i = start; i <= end; i++)
            {
                if (IsPrime(i))
                {
                    result.Add(i);
                }
            }
        });
        return result;
    }

    private static bool IsPrime(int number)
    {
        if (number % 2 == 0)
        {
            return number == 2;
        }
        else
        {
            var topLimit = (int)Math.Sqrt(number);
            for (int i = 3; i <= topLimit; i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}