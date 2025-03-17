namespace Json2Text.Utilities
{
    public class Helpers
    {
        public static (bool success, string result) TryToReadTextFile(string? filePath, bool returnExceptionString = false)
        {
            bool success = true;
            string result = string.Empty;
            try
            {
                if (filePath is null) { throw new Exception("The 'filePath' parameter is null."); }
                result = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                success = false;
                if (returnExceptionString) { result = ex.Message; }
            }
            return (success, result);
        }
    }
}
