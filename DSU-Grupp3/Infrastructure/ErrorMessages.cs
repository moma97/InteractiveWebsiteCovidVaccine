namespace DSU_Grupp3.Infrastructure
{
    public class ErrorMessages
    {
        public const string ApiDeserializingError = "Det blev fel under deserializeringen av API anropet";
        public const string ApiResponseError = "Det är fel med API:et. Dubbelkolla URL, endpoints och att API:et fungerar i swagger.";
        public const string HttpServerError = "Det uppstod ett fel vid HTTP-anropet. Dubbelkolla nätverklsanslutning. Server relaterat fel.";
        public const string unknownError = "Ett okänt fel har uppstått";

        public const string CannotSaveToJson = "Det gick inte att spara data från API till Json. Objektet är null";
        public const string ErrorSavingToJson = "Ett okänt fel uppstod när datan skulle sparas till Json";

        public const string ErrorReadingJson = "Ett okänt fel uppstod vid deserializeringen av Json filen";
        public const string CouldNotFileFilePath = "Ett fel uppstod vid öppning av filen";
        public const string JsonNull = "Json filen är null";
    }
}
