namespace HandCrafter.Services
{
    public class ValidService
    {
        public bool ValidString(string? parametr)
        {
            if(parametr == null)
            {
                return false;
            }
            return true;
        }

        public bool ValidId(int? parametr)
        {
            if (parametr == null)
            {
                return false;
            }
            return true;
        }
    }
}
