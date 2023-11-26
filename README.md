# Nøsted
Denne repositoryen tilhører medlemmene i gruppe 10 ved Universitetet i Agder, IT og Informasjonssystem:
Andreas Wahl Iversen,
Daniel Vu,
Eskil Skaret,
Lamek Tesfazghi,
Samuel Systad,
Simen Chan Amundsen.

Velkommen til gruppe 10 sin webapplikasjon for Nøsted & AS. Her har gruppen skapt en hjemmeside for brukere hos Nøsted & AS med administrativ tilgang.
Prosjektets overordnede mål er å utvikle en web-applikasjon som forenkler kundeordrer for Nøsted & AS. Da gruppen startet med prosjektet hadde gruppen lite kunnskap innen fullverdig systemutvikling for backend, frontend og databaseutvikling, men dette endret seg gradvis da prosjektet omhandlet å oppnå et faglig mål som kompetansebygging.

En gjennomgang av webapplikasjonen: https://www.youtube.com/watch?v=xXnUZv4o1eE

Systemet til gruppen tilbyr brukerregistrering, datainnsamling og service prosessering. Brukere logger inn, utfører handlinger gjennom et grensesnitt, og sikkerhetsaspekter som autentisering er implementert. Dersom man ikke har en bruker kommer man seg ikke videre i systemet; men det er mulig å registrere en ny bruker. Administratorene har tilgang til en navbar som tilbyr service, lagerbeholdning, arbeidsdokumenter og kundebehandling. 

I kundebehandling har man muligheten til å legge til kunder, og registrere informasjon om kunden. Når du har registrert kunden kan du komme til liste over kunder (kjent som Kundeoversikt). Her kan du redigere på kundene og slette kunder. 

I service siden kommer du deg til registrering. Her fyller man inn alt om kundeinformasjon.  booking, når servicen er ferdig, produkttype, feilbeskrivelse, tekstboks osv. Man har også muligheten til å legge til eksisterende kunder så man slipper å fylle ut kundeinformasjon. 

Arbeidsdokument: her finner man oversikt over registreringsskjema. I skjemaene får du alt om serviceinformasjon. Man har muligheten til å redigere og slette skjemaene, men også til å fullføre ordene.

Lagerbeholdningen har blitt satt opp, men ikke gjort ferdig ettersom gruppen ikke har mottatt Nøsted sin API til lagerbeholdningen. Det følger at gruppen ikke får koblet opp booking- og tilgjengelighet av utstyr, noe gruppen har som mål å legge til ved en senere anledning.

Kjøring av Docker og kobling opp mot database: 

Gruppen har brukt en Docker Container for å kjøre webapplikasjonen. For å kunne kjøre applikasjon må du ha Docker installert på systemet ditt.

Start med følgende: cd Nsted 
docker run --name is201-mariadb -p 127.0.0.1:3306:3306/tcp -v Dokumenter:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=1234 -d mariadb:latest
docker exec -it is201-mariadb bash 
mariadb -u root -p
Passord “1234”

Oppsett av MySqlWorkbench på MacOs:
Sett opp en database med Hostname 127.0.0.1, Port 3306 og Username root
Deretter vil man bli spurt om et passord, skriv da inn 1234
Kopier innholdet i Nsted/Data/SQL/Tables.sql og copy paste det inn i en MySql query.
Kjør queryen

Oppsett av MySqlWorkbench på windows:
For å legge til databasen, kan man bruke nuget package konsollen på tools. Da skriver man først add-migration (kalle den hva du vil), så legger den til databasen i prosjektet, så skriver man update-database i konsollen, så blir MySqlWorkbench oppdatert av informasjonen til migrationsene.

Applikasjonsoppsett, Digitalisering av Serviceordrer for IGLAND-produkter:

Modell (Model):
Database: Implementer en database for å lagre serviceordrer, kundedata, vedlikeholdshistorikk og relatert informasjon. Bruk datamodellering teknologi som MySQL for å strukturere og administrere dataene.

Kontroller (Controller):
Backend-applikasjon: Utvikle en serverapplikasjon som fungerer som kontrolleren.
Implementer logikken for å håndtere brukerforespørsler, behandle data fra modellen og sende riktig informasjon til visningen. Bruker et programmeringsspråket C#.

Visning (View):
Frontend-applikasjon: Utvikle et brukergrensesnitt for visningen. 
Lag et visuelt grensesnitt, enten via C#.
Sørg for at visningen kan vise informasjon om serviceordrer, reparasjonshistorikk, anbefalinger og forbedringsforslag.

Kommunikasjon og Systemoppdeling:
Klient-Server Kommunikasjon: Implementer en klient-server-modell for kommunikasjonen.
Klientene, enten det er mekanikere eller administrativt personell, sender forespørsler til serverapplikasjonen for å utføre handlinger knyttet til serviceordrer og informasjonshåndtering.
Del systemet inn i separate deler for administrasjon av serviceordrer og visning av vedlikeholdsinformasjon og reparasjonshistorikk.

MVC Designmønster:
Bruk av MVC-arkitektur: Strukturer applikasjonen i samsvar med Model-View-Controller mønsteret. Modellen tar seg av databasestrukturen og datalogikken.
Kontrolleren håndterer interaksjonen mellom visningen og modellen, og styrer dataflyten.
Visningen viser informasjonen på en forståelig måte for sluttbrukeren.

Dette oppsettet gir en generell struktur for å bygge en applikasjon som oppfyller Nøsted & AS' behov for digitalisering av serviceordrene for IGLAND-produktene. Det tar hensyn til bruk av MVC-mønsteret og viktige komponenter som modell, visning, kontroller, kommunikasjon og systemoppdeling for å skape en effektiv applikasjon.

Komponenter av applikasjonen:
Ansvarlig for databasestrukturen og organisering av data.
Lagrer og administrerer informasjonen om serviceordrer, kundedata og vedlikeholdshistorikk på en logisk måte.
Implementerer forretningslogikken for behandling av serviceordrer, inkludert lagrings- og oppdateringsfunksjoner.

Kontroller (Controller):
Håndterer brukerinteraksjonene og behandler forespørsler fra brukere angående serviceordrer.
Tar imot inngående data fra visningen og samhandler med modellen for å utføre de nødvendige handlingene på dataene. Sørger for at riktig informasjon blir hentet fra modellen og sendt til visningen.

Visning (View):
Ansvarlig for å presentere dataene til brukerne på en forståelig og brukervennlig måte.
Bruker HTML-markup, kanskje med MySQL Workbench, for å skape et visuelt grensesnitt som viser informasjon om serviceordrene, reparasjonshistorikk, anbefalinger og forbedringsforslag.
Kommuniserer med kontrolleren for å hente og vise riktig informasjon til brukeren.

Kommunikasjon og Systemoppdeling:
Applikasjonen er basert på klient-server-kommunikasjon der klientene, som kan være mekanikere eller administrativt personell, sender forespørsler til serveren.
Systemet er delt inn i to separate systemer, en for brukere som administrerer serviceordre og en for brukere som interagerer med vedlikeholdsinformasjonen eller reparasjonshistorikken.
MVC-modellen brukes for klientene, noe som muliggjør standardisering av arbeidsmåten og enklere vedlikehold og utvidelse av systemet på kort tid.
Ved å implementere disse komponentene i henhold til MVC-designmønsteret, vil applikasjonen ha en modulær struktur som gjør det enklere å vedlikeholde, utvide og skalere etter behov. Modellen håndterer dataene, kontrolleren styrer flyten av informasjonen, og visningen presenterer den på en forståelig måte. Kommunikasjonsstrukturen med klient-server gir en klar forståelse av hvordan brukerne interagerer med systemet, og oppdelingen i separate systemer gjør det mulig å fokusere på spesifikke formål uten å blande funksjonalitet.


Tanken bak koden:
Prinsipper og strategier sikrer ikke bare funksjonalitet, men også vedlikeholdbarhet, sikkerhet og ytelse. Når man koder, er det avgjørende å ha et sett med retningslinjer som danner grunnlaget for utviklingen. Derfor har gruppen fokusert på noen prinsipper. Gruppen mener er viktig for å skrive gode koder.

Lesbarhet:
Koden bør være enkel å lese og forstå. Gruppen har prøvd å lage en kode som er lett å lese og forstå. Brukt beskrivende variabelnavn og kommentarer for å forklare kompleks funksjonalitet.
Gjenbrukbarhet:
Skriv kode som kan gjenbrukes. Gruppen har prøvd å unngå dette med å passe på det ikk er  duplisering ved å strukturere logikken i funksjoner, klasser eller moduler.
Skalerbarhet:
Hvordan koden din vil håndtere vekst var et viktig punkt for Nøsted &AS. Gruppen har prøvd å bruke skalerbare strukturer og teknikker for å støtte fremtidig utvidelse.
Modularitet:
Det går ut på å unngå store og komplekse struktur. Derfor har gruppen etter best evne prøvd å legge koden inn i moduler eller komponenter for enklere håndtering og gjenbruk.
Versjonskontroll og dokumentasjon:
Dette prinsippet går ut på å bruk versjonskontroll for å spore endringer og samarbeide med andre utviklere.  Gruppen har dokumentert koden godt for å lette forståelsen for andre og for fremtidig referanse.
Disse kriteriene danner grunnlaget for å skrive kvalitetskode. Å følge disse prinsippene bidrar til å skape en mer vedlikeholdbar, sikker og effektiv applikasjon samtidig som det gjør det enklere for andre utviklere å samarbeide og forstå koden.
Gruppe 10 vil gjerne takke alle som har bidratt til å hjelpe oss. En spesiell takk til Nøsted & AS for et spennende prosjekt. Dette har ført til mange utfordringer og læring underveis. Gruppen vil også takke lærere og læringsassistenten for god oppfølging og tilbakemeldinger underveis i prosjektet. 

