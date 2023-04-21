using Capstone.Services.Model;

namespace Capstone.Services
{
    public interface IWeather
    {
        Weather GetLiveWeather(string location);

    }
}
