
using System;
using Google.Cloud.SecretManager.V1;

public class GcpSecretManager
{
    public String AccessSecretVersion(
      string projectId = "singular-glow-313017", string secretId = "rob-api-conn-str", string secretVersionId = "1")
    {
        // Create the client.
        SecretManagerServiceClient client = SecretManagerServiceClient.Create();

        // Build the resource name.
        SecretVersionName secretVersionName = new SecretVersionName(projectId, secretId, secretVersionId);

        // Call the API.
        AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);

        // Convert the payload to a string. Payloads are bytes by default.
        String payload = result.Payload.Data.ToStringUtf8();
        return payload;
    }
}