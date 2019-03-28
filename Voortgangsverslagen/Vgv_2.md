# Voortgangsverslag #2
# (Voorlopige) Titel onderwerp: Gamification van een opleiding
## Promotors

* **Stagecoördinator:** Dhr. Smets Marc - marc.smets@ap.be
* **Stagementor:** Mevr. Braspenning Cindy - Cindy.Braspenning@mecoms.be
* **Stagebegeleider:** Dhr. Horsmans Serge - serge.horsmans@ap.be
* **Opleidingshoofd:** Dhr. Masset Yves - yves.masset@ap.be

## Abstract
<!--Het abstract is een samenvatting van je totale bachelorproef, inclusief reeds gekende resultaten-->
Het project bestaat uit een spel dat ontwikkeld is met behulp van Unity en de Unity Engine. Het spel zal de huidige opleiding vervangen en zal op een leuke en educatieve manier de informatie aan de gebruiker overbrengen.  
Het spel bestaat uit een Main Menu waar de speler een naam kan kiezen.
Daarna heeft de speler enkele opties.
* De introductie van het spel starten
* navigeren naar het Levels menu om een level te kiezen
* Het spel afsluiten

Bij de introductie krijgt de speler een korte uitleg over het spel zelf. (hoe het spel in zijn werking zal gaan, waarover het spel gaat ...) Na de introductie krijgt de speler een overzicht van alle partijen in de energiesector en ook een korte uitleg over elke partij.

Na deze korte introductie navigeert de speler verder naar het Levels Menu waar de speler kan kiezen uit alle levels.
De levels hebben allemaal hun eigen unieke speelwijze, werking en informatie die aan de speler zal worden overgebracht.

## Technische omschrijving
<!--Technische omschrijving van de evolutie van het project tijdens de betrokken periode, met aanduiding van de reeds bekomen resultaten en een planning voor de verdere uitwerking, welke problemen zijn ondervonden en hun oplossingen:-->
<!--Minimum 750 woorden-->
### Realisaties

#### Analyse

(elke partij in de energiesector stelt een level voor)

* Informatie verzamelen over alle partijen in de energiesector. (Producent, TGO, DGO, Leverancier, Consument...)
* Vragen en bijhorende antwoorden opstellen over elke partij.
* Concepten uitwerken voor alle partijen.

Om te weten hoe alle partijen van de energiesector werken, was het doornemen van de huidige opleiding noodzakelijk. Ook enkele meetings met de stagementor was noodzakelijk om ook een mondelinge toelichting te krijgen over de werking van alle partijen.
Door deze insteek van informatie maakt het bedenken van concepten voor levels makkelijker en maakt ook de gegeven informatie relevanter.
Ook zorgt deze informatie voor een manier om vragen en antwoorden te bedenken over een bepaalde partij in de energiesector.

Het doel van het spel was om alle levels een unieke werking te geven om elke level een verfrissend gevoel te geven zodat men niet hetzelfde hoeft te doen in elke level.
Enkele algemene zaken kunnen wel terugkomen in elke level zoals de manier waarop de level wordt geïntroduceerd, hoe de level gespeeld moet worden en de manier waarop de speler vragen kan beantwoorden over een bepaalde partij uit de energiesector.

#### Technisch

* Nieuwe levels aanmaken en uitwerken volgens de concepten.
  * Producer level - UI, Functionaliteit en code
  * DGO level - UI, Functionaliteit en code
  * Consumer level - UI, Functionaliteit en code
  * TGO level - UI, Functionaliteit en code
* Reeds aanwezige problemen oplossen
  * Spel compatibel maken met verschillende schermresoluties
  * Bugfixen van de Supplier level
* Algemene functionaliteit toegevoegd of verbeterd
  * De camera kon men enkel laten draaien door bepaalde toetsen in te drukken -> camera draait als de muisaanwijzer de zijkanten van het spel raakt
  * Het spel kan nu ook gepauzeerd worden door op de 'Escape' toets te drukken  
  
  Door het 'reverse engineeren' van de Supplier level, was het zeer makkelijk om nieuwe levels aan te maken en deze toe te voegen aan het spel. Enkele aanpassingen waren uiteraard noodzakelijk. Sommige scripts waren nog afhankelijk van objecten die niet meer aanwezig waren en deze scripts moesten dus herschreven of verwijderd worden om verder te gaan met het maken van de nieuwe levels.
  
  Ook de reeds aanwezige problemen in de supplier level en de meer algemene problemen zijn reeds opgelost. Dit was zeer makkelijk te bereiken door het refactoren van code van enkele scripts zodat deze op een universelere manier werken en dus makkelijk geïmplementeerd kan worden in nieuwe levels.
  
### Huidige werkpunten
<!--Beschrijven wat de huide focus punten zodat er progressie is in de BAP/Stage-->
* verder afwerken, bugfixen en testen van alle levels
* verder relevante vragen opstellen voor alle levels
* concept bedenken en uitwerken van nieuwe level: Mecoms level
* Werken met nieuwe syntaxen uit de Unity Engine en werken met nieuwe algoritme's uit zelf opgezette helper classes zoals de 'Fisher-Yates shuffle method'
* Documentatie ontwikkelen voor het hele project
  * Hoe kan een gebruiker extra vragen toevoegen
  * Hoe kan een gebruiker gemakkelijk variabelen aanpassen
  * Hoe kan een gebruiker best een nieuwe level aanmaken
  * uitleg van code in de scripts
 
### Toekomst

Het project is op een zeer gebruiksvriendelijke manier opgesteld. 

* Variabelen kunnen zeer snel aangepast worden in de Inspector van Unity of in de .cs bestanden zelf.
* Extra levels kunnen ook zeer snel aangemaakt worden en kunnen gemakkelijk geïntegreerd worden in het spel.
* Nieuwe vragen of informatie toevoegen kan men ook zeer snel bereiken door deze vragen of informatie toe te voegen aan het corresponderende bestand dat men kan terugvinden in de documentatie.

## Extra informatie
### Bijscholingen
<!--Bijgewoonde seminaries, presentaties, workshops, bedrijfsbezoeken etc in deze periode (onderwerp, datum, korte samenvatting en beoordeling)-->

### Nieuwe contacten
<!--Nieuwe contacten gemaakt in deze periode (naam, voornaam, e-mail, telefoonnummer, bedrijf, functie, relevantie voor het werk)-->

### Literatuur
<!--Nieuwe contacten gemaakt in deze periode (naam, voornaam, e-mail, telefoonnummer, bedrijf, functie, relevantie voor het onderzoek)-->
