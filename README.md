# Nøsted
Denne repositoryen tilhører medlemmene i gruppe 10 ved Universitetet i Agder, IT og Informasjonssystem:
Andreas Wahl Iversen,
Daniel Vu,
Eskil Skaret,
Lamek Tesfazghi,
Samuel Systad,
Simen Chan Amundsen.

Dette er en web applikasjon for Nøsted & AS, der vi har laget en hjemmeside for personer med administrativ tilgang, en service side med en dropdown meny for de forskjellige prosessene de gjennomgår når de skal utføre service, en side der de skal kunne se lagerbeholdningen på forskjellig utstyr og en side med arbeidsdokumentet der de hele tiden kan ha oversikt over hva de jobber med.

Vi har også laget en logg inn knapp, slik at etterhvert skal bare personer fra Nøsted & og andre administrative personell få tilgang til nettsiden.

<!-- Dette er hvordan man setter opp databasen i docker med cmd -->
docker run --name is201-mariadb -p 127.0.0.1:3306:3306/tcp -v Dokumenter:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=1234 -d mariadb:latest

docker exec -it is201-mariadb bash 

mariadb -u root -p
1234
