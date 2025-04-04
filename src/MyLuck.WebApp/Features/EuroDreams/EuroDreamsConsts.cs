namespace MyLuck.WebApp.Features.EuroDreams;
internal static class EuroDreamsConsts
{
    internal static class Email
    {
        internal const string Subject = "EuroDreams - new draw {0}";
        internal const string Body = "There is a new draw: {0}<br>Date: {1}";
        internal const string BodyWithUserKey = "There is a new draw: {0}<br>Date: {1}<br><br>Your lottery keys:<br>{2}<br><br>Matching numbers:<br>{3}";
    }
}
