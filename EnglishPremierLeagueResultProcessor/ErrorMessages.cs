namespace EnglishPremierLeagueResultProcessor
{
    public class ErrorMessages
    {
        public static string ColTeamMissing => "Column Team is missing in the input";
        public static string ColForMissing => "Column For is missing in the input";
        public static string ColAgainstMissing => "Column Against is missing in the input";
        
        public static string ColTeamMoreThanOne => "Column Team is provided more than once in input";
        public static string ColForMoreThanOne => "Column For is provided more than once in input";
        public static string ColAgainstMoreThanOne => "Column Against is provided more than once in input";

    }
}
