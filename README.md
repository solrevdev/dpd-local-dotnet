# dpd-local-dotnet

> #deprecated and on-hold (not an active project currently but may pick up again later)

A netstandard2.0 library that consumes the DPD Local API and provides a .NET client

## Register with DPD Local

You will need to register/sign up with DPD Local to get access to the API.

Using your existing DPD Local username and password login to either the website or the DPD Local application installed on your machine as you would normally do and view the API page in order to get to the next step.

![Login Screen](docs/dpd-login-screen.png)

I tried logging in here [https://www.dpdlocal.co.uk/apps/api/](https://www.dpdlocal.co.uk/apps/api/) but it didn't work so I had to install the macOS version of the application and then log in there with the existing DPD Local username and password credentials and registered from there.

![API Screen](docs/dpd-api-screen.png)

Once you have registered you will receive some separate FTP login details which give you access to all the API documentation used to create this API wrapper library which you will need to read and follow in order to make your application live.

![API Docs](docs/dpd-pdf-spec-screenshot.png)

However, I hope this dpd-local-dotnet library will give you a good head start without too much referring to the API documentation.

## Getting Started

todo....

### Environments and appsettings.json

The DPD Local documentation really only covers the the development environnement only mentioning that a sign off is required to get access to production credentials so with that in mind we are going to store our development credentials in an `appsettings.Development.json` file so that we can store our production credentials in an `appsettings.json` file.

For example:

**appsettings.Development.json**

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "DpdCredentials": {
    "Name": "Development - DPD API Credentials",
    "ApiUrl" : "api.dpdlocal.co.uk",
    "Username": "DSmith",
    "Password": "MYPassWd66",
    "AccountNumber": "830567"
  }
}
```

**appsettings.json**

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "DpdCredentials": {
    "Name": "Production - DPD API Credentials",
    "ApiUrl" : "api.dpdlocal.co.uk",
    "Username": "DSmith",
    "Password": "MYPassWd66",
    "AccountNumber": "830567"
  }
}

```
