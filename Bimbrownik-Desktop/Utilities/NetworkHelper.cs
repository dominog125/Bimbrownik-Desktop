using System.Net.NetworkInformation;

namespace Bimbrownik_Desktop.Utilities;

public static class NetworkHelper
{
    public static bool IsInternetAvailable()
    {
        return NetworkInterface.GetIsNetworkAvailable();
    }
}