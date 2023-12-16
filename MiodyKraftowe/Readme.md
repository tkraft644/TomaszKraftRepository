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


