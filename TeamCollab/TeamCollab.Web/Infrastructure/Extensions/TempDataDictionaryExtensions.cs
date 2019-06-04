using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TeamCollab.Web.Infrastructure.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData["Success"] = message;
        }

        public static void AddErrorMessage(this ITempDataDictionary tempData, string message)
        {
            tempData["Error"] = message;
        }
    }
}
