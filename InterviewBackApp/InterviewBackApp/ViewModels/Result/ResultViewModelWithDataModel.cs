namespace InterviewBackApp.ViewModels.Result
{
    public class ResultViewModelWithDataModel<T> : ResultViewModel
    {
        public T Data { get; set; }
    }
}
