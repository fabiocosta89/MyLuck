using System.Text;

namespace MyLuck.WebApp.Features.EuroDreams;
internal static class EuroDreamsConsts
{
    internal static class Email
    {
        internal static readonly CompositeFormat Subject = CompositeFormat.Parse("EuroDreams - new draw {0}");
        internal static readonly  CompositeFormat Body = CompositeFormat.Parse("There is a new draw: {0}<br>Date: {1}");
        internal static readonly  CompositeFormat BodyWithUserKey = CompositeFormat.Parse(
            "There is a new draw: {0}<br>Date: {1}<br><br>Your lottery keys:<br>{2}<br><br>Matching numbers:<br>{3}");
    }
}
