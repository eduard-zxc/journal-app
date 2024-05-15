## How to run

### Pre-requisites

Install the .NET 7 SDK

### Commands

Run the application: `dotnet run --project src/Client`

Run unit tests: `dotnet test`


### Used Patterns

`Singleton`

Deoarece a trebuit să implementez un container DI, înregistrarea singletonilor a făcut parte din proces. Notă: implementarea mea nu este sigură pentru threaduri, sper să nu apară plângeri legate de asta.

// src/Application/DI/Container/DIContainer.cs

`Builder`
Unul dintre cele mai utile tipare de proiectare, în special pentru creatorii de biblioteci, a fost folosit pentru a reduce lanțurile if else și pentru a crea o interfață internă elegantă.

// src/Application/Panels/Options/OptionsBuilder.cs

`Prototype`
Am folosit tiparul de proiectare prototip pe modelul JournalEntry. Voi folosi asta pentru a implementa acțiunea de copiere a unei înregistrări din jurnal stocată.

// src/Domain/Models/JournalEntry.cs

`Factory Method`
// src/Domain/Factory/RepositoryFactory.cs

`Facade`
Tipul de design utilizat pe larg in aplicatia mea are o relatie directa cu principiul encapsularii din programarea orientata pe obiecte (OOP). Un exemplu este clasa OptionMenuView. Aceasta clasa este initializata cu o lista de optiuni si o functie callback pentru momentul in care o optiune este selectata. Extern, clasa expune o singura metoda - Render. Intern, clasa se ocupa de o serie de lucruri: urmareste indexul optiunii selectate, detecteaza apasarile pe taste, calculeaza numarul de linii ocupate de meniu si le sterge inainte de fiecare re-randare si itereaza prin optiuni pentru a stabili pe care sa o coloreze cu verde pentru a indica selectarea. In esenta, clasa ofera o interfata simpla si reutilizabila.

// src/Application/Views/OptionMenuView.cs

`Flyweight`
Pentru optimizarea aplicatiei am folosit un tipar de proiectare mai puțin obișnuit, și anume tiparul Flyweight. Am ales acest tipar pentru etichete (tag-uri) deoarece acestea sunt obiecte partajate între multe intrări. Prin urmare, se folosește mai puțină memorie, iar egalitatea etichetelor poate fi verificată prin referință. În realitate, însă, câștigul de performanță este neglijabil.

// src/Application/Tags/TagFactory.cs

`Decorator`
Modelul decorator este folosit în aplicația mea pentru vizualizări. Cu decoratori pot intercepta metoda Render și adăuga funcționalități suplimentare acesteia, precum golirea consolei înainte de randare sau adăugarea unui mesaj de feedback. Acesta este un model foarte puternic, dar este folosit de obicei cu fluxuri sau biblioteci externe. În cazul meu, existau modalități mai ușoare, dar aduce o oarecare valoare.
Notă pentru exemple: IPanelState implementează IView

// src/Application/Views/Decorator/WelcomeMessageViewDecorator.cs

`Command`
Acest pattern este adesea folosit pentru aplicații cu o interfață grafică (UI) pentru a crea componente extensibile. Am încercat să reproduc acest caz de utilizare pentru aplicația mea consolă și am separat cu succes logica de prezentare de logica de business. Modelul următor arată cum meniul meu de opțiuni primește o funcție lambda care gestionează opțiunea selectată. Acest lucru îmi permite să refolosesc meniul în diferite locuri, cu acțiuni diferite pentru fiecare opțiune, menținând lucrurile simple.

// src/Application/Views/OptionMenuView.cs

`State`
Modelul stării este folosit în aplicația mea pentru a avea o mașină de stări și pentru a restricționa fluxul de control între diferite panouri. Ajută la menținerea logicii simple, iar codul devine slab cuplat și foarte coeziv. Următoarea este clasa mașină de stări pentru aplicație, la care fiecare stare are o referință:

// src/Application/Panels/PanelController.cs

`Strategy`
Aplicația mea acceptă diferite opțiuni de stocare pentru înregistrările jurnalului. Modelul strategie este folosit pentru a permite utilizatorului să aleagă opțiunea de stocare la runtime. Următoarea este interfața pentru strategie:

// src/Application/JournalEntries/Abstractions/IJournalEntryRepository.cs

iar aceasta are 2 implementări:

•	MemoryJournalEntryRepository - stochează înregistrările în memorie, pe durata utilizării aplicației
•	FileJournalEntryRepository - stochează înregistrările într-un fișier json, pentru persistență

// src/DataAccess/DI/Registration.cs