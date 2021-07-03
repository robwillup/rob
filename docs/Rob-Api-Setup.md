# Rob.Api Setup

## GCP Secret Manager

This HTTP API uses a connection string to connect to the database.
In development, this connection string is stored using `.NET user-secrets`.

In other environments, this secret is stored in GCP Secret Manager.

For the API to be able to access this secret, the following steps were performed:

### Created Service Account

```bash
gcloud iam service-accounts create rob-api-account
```

### Grant Permissions to the Service Account

```bash
gcloud projects add-iam-policy-binding singular-glow-313017 --member="serviceAccount:rob-api-account@singular-glow-313017.iam.gserviceaccount.com" --role="roles/owner"
```

### Generate the key file

```bash
gcloud iam service-accounts keys create rob_api_secrets.json --iam-account=rob-api-account@singular-glow-313017.iam.gserviceaccount.com
```

### Environment Variable

When running the application in GCP, it will automatically be able to find the secrets.
Anywhere else we need to setup this environment variable:

```bash
export GOOGLE_APPLICATION_CREDENTIALS="KEY_PATH"
```
