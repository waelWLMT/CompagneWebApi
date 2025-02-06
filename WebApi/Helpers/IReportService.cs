namespace WebApi.Helpers
{
    public interface IReportService
    {
        byte[] CreateReportFile(string pathRdlc);
    }
}
