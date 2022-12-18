namespace InterviewBackApp.ViewModels.Result
{
    public class ResultViewModel
    {
        public ResultViewModel()
        {
            ErrorMessages =
                new List<string>();
            HiddenMessages =
                new List<string>();
            InformationMessages =
                new List<string>();
        }

        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public IList<string> HiddenMessages { get; set; }
        public IList<string> InformationMessages { get; set; }

    }
}
