Wdrożenie aplikacji na host za pośrednictwem folderu wykonuję się przez VisualStudio i funkcję "Opublikuj"
Instalacji konfigurujemy wybierając publikacje do folderu oraz dodajemy jego ścieżkę w tym przypadku aktualnie:

\\iis-2019.wmi.amu.edu.pl\s153355\public_iis\ 

Do aktualizacji serwera należy dodać plik do folderu wykorzystując ekplorator plików wpisując ścieżkę do serwera 
\\iis-2019.wmi.amu.edu.pl\s153355\public_iis\ 
Plik app_offline.htm który zatrzyma działanie i wyświetli zawartość HTML pliku.  

Wykorzystano serwer UAM na potrzeby projektu. Można użyć innego serwera.
Do projektu W pliku appsettings.json zostasła podłączona baza danych SQL za pomocą ConnectionString. 
Zmiana serwera bazodanowego odbywa się przez zmiane ConnectionString w sposób następujący:

 "DefaultConnection": Server=URL_SERWERA;Database=NAZWA_BAZY_DANYCH;User Id=LOGIN_UŻYTKOWNIKA Password=HASŁO_UŻYTKOWNIKA;

 Jeśli zmienimy nazwę DefaultConnection trzeba zmienić również tę nazwę w pliku Program.cs:

 var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


Migracje na bazę danych wykonuję się przez pakiet EntityFrameworkCore:
Add-Migration Initial     w przypadku pierwszego dodawania do bazy
następnie:
Update-Database
W ten sposób utworzone zostaną automatycznie wszystkie table oraz zostaną wgrane do serwera bazodanowego bez
Konieczności ręcznego tworzenia ich.

Import silnika Front End odbywa się w plikach Layout w postaci:

        <link rel="stylesheet" href="~/css/materialize.css" asp-append-version="true" />
        <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />

Do poprawnego działania projektu muszą być zainstalowane pakiety NuGet:
-EntityFrameworkCore
-EntityFrameworkCoreTools
-Mailkit
-MimeKit
-ASP.Identity
-ASP.Identity.EntityFramework



@MiodyKraftowe 2023

------------------------------------------------------------------------------------------------------

Deployment of the application to the host via a folder is done through Visual Studio using the "Publish"
function. The configuration of the deployment involves selecting the publishing to a folder and adding its path, currently:

\\iis-2019.wmi.amu.edu.pl\s153355\public_iis\

To update the server, a file needs to be added to the folder using File Explorer by entering the path to the server:

\\iis-2019.wmi.amu.edu.pl\s153355\public_iis\

The file app_offline.htm should be added, which will stop the operation and display the content of the HTML file.

The UAM server was used for the project. Another server can be used as needed.

For the project, a SQL database is connected in the appsettings.json file using a ConnectionString. 
Changing the database server is done by changing the ConnectionString as follows:


"DefaultConnection": Server=SERVER_URL;Database=DATABASE_NAME;User Id=USERNAME Password=USER_PASSWORD;
If you change the name of DefaultConnection, you also need to change this name in the Program.cs file:


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
Database migrations are performed using the EntityFrameworkCore package:


Add-Migration Initial  # for the first database addition
Then:


Update-Database
This way, all tables will be automatically created, and they will be uploaded to the database server without the need for manual creation.

The import of the Front-End engine is done in the Layout files as:


    <link rel="stylesheet" href="~/css/materialize.css" asp-append-version="true" />
    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />

For the project to function correctly, the following NuGet packages must be installed:

-EntityFrameworkCore
-EntityFrameworkCoreTools
-Mailkit
-MimeKit
-ASP.Identity
-ASP.Identity.EntityFramework


@MiodyKraftowe 2023
