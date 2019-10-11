using CheckWordsterForWindows.Services;
using Nancy;
using Nancy.ModelBinding;

namespace CheckWordsterForWindows.Server
{
    public class CheckwordsterService : NancyModule
    {
        public CheckwordsterService()
        {
            Post["/checkWordster"] = parameters =>
            {
                CheckWordsterData recievedData = this.Bind<CheckWordsterData>();

                var cw = new CheckWordster(recievedData.numberInDigits);

                return new { numberInDigits = recievedData.numberInDigits, numberInWords = cw.GetWords() };
            };
        }
    }
}