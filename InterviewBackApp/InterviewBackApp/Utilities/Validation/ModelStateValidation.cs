using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InterviewBackApp.Utilities.Validation
{
    public class ModelStateValidation
    {

        public static IList<string> GetErrors(ModelStateDictionary modelStateDictionary)
        {
            var result = new List<string>();
            foreach (var modelError in modelStateDictionary.Values)
            {
                foreach (var message in modelError.Errors)
                {
                    result.Add(message.ErrorMessage);
                }
            }
            return result;

        }

    }
}
