# BtCS Reader
_Bank to Customer Statement camt.053_

---

This application enables the import and viewing of [CAMT](https://www.ebics.de/securedl/sdl-eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE2Nzk4ODk2NjgsImV4cCI6MTY3OTk3OTY2OCwidXNlciI6MCwiZ3JvdXBzIjpbMCwtMV0sImZpbGUiOiJmaWxlYWRtaW5cL3Vuc2VjdXJlZFwvYW5sYWdlM1wvYW5sYWdlM19zcGVjX2FcL0FubGFnZV8zX0RhdGVuZm9ybWF0ZV9WMy42bUFFLnBkZiIsInBhZ2UiOjEwM30.LrgWfpgmKoKnRgZcFOIqqkROr44pjv9jLucBbcQg6fk/Anlage_3_Datenformate_V3.6mAE.pdf) files.
These are currently implemented according to the CSV-CAMT v8 standard of Nospa.

The application offers the following features:

* Multi User Environment (Shared usage of the same SFD)
* Per User Import of CAMT files
* SFD (Single File Database) with encryption according to [AES](https://www.litedb.org/docs/encryption/) standard
* Overview of the outputs via configurable reports
* Pre-planning of upcoming issues based on already existing data

## Feature roadmap:

| 0.1.0        | 0.2.0          | 0.3.0        |
|--------------|----------------|--------------|
| MUE          | CAMT Import    | CAMT Reports |
| SFD          | CAMT Analysis  |              |
| CAMT Preview | CAMT with LINQ |              |

## Installation and usage:

### Prerequisites:
* At least [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) must be installed on the target system.
* Graphic display min. 1024x768

### Installation:
The application ist portable, so just download the latest release archive and extract the files within to a folder where you have write access.

Before first start:
In the  application folder you can find a `settings.ini` file there you can give it an absolute path to the database file and an encryption password for the database encryption.
_If you dont change the default password, the application will generate one at the first start._

Here a the default settings:

```
[Database]
Filename=data.storage
Password=123456789
```

To start using the application just type in credentials, if a user doesn't exist with the given username the application will ask if it should create one.
After the user creation you can start preview or import CAMT files.