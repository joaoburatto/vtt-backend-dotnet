namespace Requests.Utility
{
    public static class PingRequest
    {
	private const String ResponseJson = "pong";

	public static String Response()
	{
	    return ResponseJson;
	}
    }
}
