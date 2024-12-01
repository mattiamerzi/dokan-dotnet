namespace DokanVsRemoteFS;

[Serializable]
internal class VsRemoteAuthenticationException(string message) : Exception(message)
{
}