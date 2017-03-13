namespace Infodoctor.Web.Infrastructure.Interfaces
{
    public interface IConfigService
    {
        string PathToArticlesImages { get; }
        string PathToDoctorsImages { get; }
        string PathToClinicsImages { get; }
    }
}
