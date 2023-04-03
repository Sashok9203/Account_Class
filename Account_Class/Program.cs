namespace Account_Class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Account? ac = new Account("MyEmail@net","123pass");
                ac.SetAccountInfo();
                Console.WriteLine(ac.GetAccountInfo());
            }
            catch (Exception e) { Console.WriteLine(e.Message);}
        }
    }
}