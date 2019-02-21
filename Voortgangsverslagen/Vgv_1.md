# Voortgangsverslag #1
# Gamification van een opleiding:
## Promotors

* **Stagecoördinator:** Dhr. Smets Marc - marc.smets@ap.be
* **Stagementor:** Mevr. Braspenning Cindy - Cindy.Braspenning@mecoms.be
* **Stagebegeleider:** Dhr. Horsmans Serge - serge.horsmans@ap.be
* **Opleidingshoofd:** Dhr. Masset Yves - yves.masset@ap.be

## Onderwerp

Het doel van het project is de huidige opleiding, die vooral bestaat uit enkele PDF's die informatie bevatten over de werking van de energie sector en een korte introductie tot het Mecoms product, te vervangen door een spel
die de speler op een leuke, maar toch informatieve wijze de werking van alle sectoren in de energie sector zal aanleren.

High-level overzicht van alle sectoren:

* Producer / Producent: Deze produceert alle energie en verkoopt deze aan de Supplier / Leverancier.
* TGO (Transmission Grid Operator): Het doel van de TGO is de hoogspanning die gegenereerd is door de producent om te zetten naar een lagere hoogspanning.
* DGO (Distribution Grid Operator): Het doel van de DGO is de lagere hoogspanning van de TGO om te zetten naar laagspanning dat geschikt is voor huishoudelijk gebruik. Ook houdt de DGO zich bezig met het onderhoud van het elektriciteitsnet en het uitlezen, installeren, vervangen en verwijderen van de elektriciteitsmeters bij de consumenten.
* Supplier / Leverancier: De leverancier koopt energie van de producent en verkoopt deze door aan de consument en regelt ook de facturatie hiervan. 
* Consumer / Consument: De consument zelf kiest dus bij welke leverancier hij zijn energie aankoopt.

Het gewenste resultaat tegen het einde van de BAP zal het verwezelijken zijn van de ontbrekende levels (Producer, TGO, DGO, Consumer) en de reeds aanwezige zaken te bugfixen.  
Elke level zal zijn eigen unieke werking hebben.

Dit project was, zoals eerder vermeld, reeds opgestart door andere jobstudenten die tijdens de zomer van 2018 van start zijn gegaan.
Er was al reeds een spel aanwezig, waarin ook al enkele zaken aanwezig waren, zoals:
* **Log in Menu**
	* **Username Text Field**: Veld om de username van de speler in te vullen, zodat deze opgeslagen kan worden in de highscores lijst.
* **Main Menu**
	* **Levels Button**: Knop om naar het Levels Menu te navigeren.
	* **Highscore Button**: Knop om naar het Highscore Menu te navigeren.
	* **Quit Button**: Knop om het spel af te sluiten.
* **Highscore Menu**
	* **Highscore Lijst**: Lijst van alle spelers met hun bijhorende score.
* **Levels Menu**
	* **Introdution Button**: Knop om het Introduction level te starten.
	* **Producer Button**: Knop om het Producer level te starten.
	* **TGO Button**: Knop om het TGO level te starten.
	* **DGO Button** Knop om het DGO level te starten.
	* **Supplier Button**: Knop om het Supplier level te starten.
	* **Consumer Button**: Knop om het Consumer level te starten.
* **Megan Introductie**: Introductie dat uitleg geeft over de inhoud van het spel.
* **Game Introductie level**: Introductie tot alle levels en geeft een high-level overzicht van alle sectoren in de energie sector.
* **Supplier level**: Level i.v.m. de leverancier.

### Producer Level

#### Algemene Werking / Gameplay

In de producer level is het de taak van de speler om zoveel mogelijk contracten aan de 'suppliers' te verkopen, en genoeg energie producerende gebouwen aan te kopen.
Sommige energie producerende gebouwen produceren ook vervuiling en dit wordt dan ook in rekening gebracht. Indien het vervuilingsgehalte van de stad te hoog is, zal de speler automatisch de level verliezen.
Ook gebouwen met groene energie hebben hun eigen voor- en nadelen. Gebouwen die groene energie produceren zijn meestal afhankelijk van het weer of andere omstandigheden.

Doorheen de level, op een willekeurig interval, zal het weer dan ook veranderen.
Door veranderende weersomstandigheden, en dus ook de energie productie van 'groene' gebouwen, zal de productie van 'grijze' gebouwen omgekeerd evenredig moeten wijzigen om het energiegehalte in balans te houden.  
Door de energie productie van deze gebouwen te doen stijgen of dalen, zal dit ook extra vervuiling met zich meebrengen.

De speler kan extra gebouwen aankopen via het 'Market Menu'. Hier kan de speler zien hoeveel elk gebouw kost en produceert, wat voor type energie het gebouw produceert en de hoeveelheid vervuiling dat dit gebouw procentueel met zich meebrengt.
Tevens kan de speler hier zien hoeveel gebouwen hij van elk type heeft en de bijhorende statistieken hiervan (= totale productie, vervuiling en het eventuele gedrag in productie afhankelijk van het weer).

Zoals eerder vermeld zal de speler contracten moeten verkopen aan de 'suppliers'.  
Deze zullen aangeboden worden na een bepaald interval via een pop-up.
Op elk contract zal te zien zijn hoeveel energie de speler aan de leverancier verkoopt en hoeveel winst hij maakt door dit contract te accepteren.

Elk contract zal terechtkomen op het 'Contract Menu'.
Op dit menu is te zien welke contracten de speler in zijn bezit heeft.
Tevens is er een optie om een bepaald contract op te zeggen.
Dit zal vooral nodig zijn als de speler te weinig energie produceert en daardoor een contract moet opzeggen.
Indien de speler dit niet doet, zal de algemene 'happiness' dalen en ook hierdoor kan de speler dus de level verliezen.

Er zullen doorheen de level ook andere, gelijkaardige pop-ups verschijnen zoals de pop-ups van de contractaanbiedingen.
Deze pop-ups duiden aan dat de speler een vraag kan beantwoorden in verband met de producer.
De speler kan kiezen om deze vraag te weigeren of te accepteren.
Om de speler toch aan te moedigen om tijdig een vraag te beantwoorden, zal de 'happiness' doorheen de level blijven dalen.
Elk juist antwoord zal beloond worden met een bonus in score en 'happiness'.

**Korte samenvatting van de Game Over voorwaarden:**

*enkel één van deze voorwaarden is voldoende om het spel te verliezen*

* Geld van de speler < 0
* Algemene 'happiness' < 0%
* Vervuiling van de stad > 100%

**Korte samenvatting van de Game Won voorwaarden:**

*enkel één van deze voorwaarden is voldoende om het spel te winnen*

* Geld van de speler > 1.000.000
* Aantal consistent correct beantwoorde vragen > 20
* Aantal aangenomen contracten > 14

#### Technisch

De producer level zal volgende zaken bevatten:

* **UI Canvas**
	* **Player Score Panel**
		* **Player Score Text**: Geeft de naam en de score weer van de speler.
	* **Pause Button**: Knop om het spel te pauzeren en om het 'Pause Menu' te openen.
	* **Producing Panel**
		* **Producing Text**: Geeft het verschil weer tussen de totaal geproduceerde energie en de totaal verkochte energie aan alle leveranciers.
	* **Money Panel**
		* **Money Text**: Geeft weer hoeveel geld de speler bezit.
	* **Pollution Panel**
		* **Pollution Slider**: Geeft de vervuiling van de stad weer.
	* **Happiness Panel**
		* **Happiness Slider**: Geeft het algemene geluk weer van de stad.
	* **Contract Pop-up**
		* **Contract content**: Geeft de naam van de leverancier weer, alsook de hoeveelheid verkochte energie en de winst voor de speler.
		* **Accept Button/Update Button**: Knop om een contract te accepteren/updaten.
		* **Decline Button**: Knop om een contract te weigeren.
	* **Quiz Pop-up**
		* **Accept Button**: Knop om de quiz te starten.
		* **Decline Button**: Knop om de quiz te weigeren.
* **Market Canvas**
	* **Close Button**: Knop om het canvas te sluiten.
	* **Total Energy Production Text**: tekst dat weergeeft hoeveel alle gebouwen in totaal produceren, uitgedrukt in kWh.
	* **Market Panel**
		* **Market Grid**
			* **Building Type Text**: Geeft weer wat voor type gebouw de speler kan aankopen.
			* **Energy Type Text**: Geeft weer wat voor type energie het gebouw produceert.
			* **Production Text**: Geeft weer hoeveel energie dit gebouw gemiddeld zal produceren, uitgedrukt in kWh.
			* **Pollution Text**: Geeft weer hoeveel vervuiling dit gebouw met zich meebrengt, uitgedrukt in een percentage.
			* **Price Text**: Geeft weer hoeveel het gebouw kost.
			* **Buy button**: Knop om een gebouw te kopen.
		* **Market Button**: Schakelt het Market Canvas over naar het Market Panel.
		* **Installed Button**: Schakelt het Market Canvas over naar het Installed Panel.
	* **Installed Panel**
		* **Installed Grid**
			* **Building Type Text**: Geeft weer wat voor type gebouw de speler kan aankopen.
			* **Energy Type Text**: Geeft weer wat voor type energie het gebouw produceert.
			* **Production Text**: Geeft weer hoeveel energie dit gebouw gemiddeld zal produceren, uitgedrukt in kWh.
			* **Pollution Text**: Geeft weer hoeveel vervuiling dit gebouw met zich meebrengt, uitgedukt in een percentage.
			* **Behaviour Prefab**
				* **Behaviour Percentage Text**: Geeft de stijging of daling weer van de productie van de geïnstalleerde gebouwen, uitgedrukt in een percentage.
				* **Behaviour Arrow**: Pijl die weergeeft of het een positieve stijging of negatieve daling is.
			**Sell Button**: Knop om een gebouw te verkopen.
* **Contract Canvas**
	* **Close Button**
	* **Contract Grid**
		* **Supplier Name**: Geeft de naam van de leverancier weer.
		* **Energy Sold**: Geeft weer hoeveel energie de speler levert aan de leverancier, uitgedrukt in kWh.
		* **Profit**: Geeft weer hoeveel winst de speler maakt op het contract.
		* **Cancel** Button: knop om het contract te beëindigen.
* **Quiz Canvas**
	* **Question Text**: Willekeurig geselecteerde vraag.
	* **Answer Buttons**: Knoppen met de antwoorden van desbetreffende vraag.
* **Pauze Canvas**
	* **Continue button**: Spel verderzetten.
	* **Exit Button**: Spel beëindigen en terugkeren naar het Main Menu.


#### Mockups & Designs

##### UI Canvas
![UI Canvas](https://github.com/AP-Elektronica-ICT/bachelorproef-18-19-Samvanbogaert17/blob/master/Project/doc/Documentatie%20Sam/Mockups%20-%20Designs%20-%20Manuals/Producer/Mockups/png/lvl_producer_ui.png "Producer Level UI Canvas")

##### Market Canvas
![Market Canvas](https://github.com/AP-Elektronica-ICT/bachelorproef-18-19-Samvanbogaert17/blob/master/Project/doc/Documentatie%20Sam/Mockups%20-%20Designs%20-%20Manuals/Producer/Mockups/png/lvl_producer_marketCanvas.png "Producer Level Market Canvas")

##### Contract Canvas
![Contract Canvas](https://github.com/AP-Elektronica-ICT/bachelorproef-18-19-Samvanbogaert17/blob/master/Project/doc/Documentatie%20Sam/Mockups%20-%20Designs%20-%20Manuals/Producer/Mockups/png/lvl_producer_contractsCanvas.png "Producer Level Contract Canvas")

##### Quiz Canvas
![Quiz Canvas](https://github.com/AP-Elektronica-ICT/bachelorproef-18-19-Samvanbogaert17/blob/master/Project/doc/Documentatie%20Sam/Mockups%20-%20Designs%20-%20Manuals/Producer/Mockups/png/lvl_producer_eventCanvas.png "Producer Level Quiz Canvas")

### TGO Level

Bij de TGO Level is het doel van de speler om hoogspanning om te zetten naar een lagere hoogspanning.
Dit kan de speler bereiken door het spelen van enkele minigames en door vragen over de TGO te beantwoorden.

Enkele voorbeelden van mogelijke minigames:

* Memory
* Hangman
* Starcatcher (in dit geval dan 'lightningcatcher')
* (indien veel tijd kleine 2D Platformer level)

Verdere uitwerking voor de TGO level is zeker en vast nog nodig, maar dit is voorlopig nog geen prioriteit.
<!-- Voor deze level is er nog veel analyse werk nodig -->

### DGO Level

#### Algemene Werking / Gameplay

In de DGO level zal de speler met 'workers' te werk gaan om problemen op te lossen die zich in de stad zullen voordoen.
Elk probleem zal zijn eigen 'severity' of moeilijkheidsgraad hebben. Sommige problemen zullen dus langer duren voordat ze opgelost zijn.
De speler krijgt ook de mogelijkheid om bij elk probleem extra workers toe te voegen of te verwijderen om zo de tijd, die nodig is om het probleem op te lossen, te verminderen.

De speler kan zien welke problemen zich voordoen in het 'Problems Menu' die de speler kan openen via het Happiness Panel.

De speler zal een beloning krijgen per opgelost probleem in de vorm van geld, waarmee de speler een extra worker kan aankopen.
De prijs per worker zal exponentieel stijgen naarmate de speler meer workers bezit.

Ook deze level zal werken met een algemene 'happiness'.
Indien de speler geen workers inzet om een probleem op te lossen, zal per tijdseenheid de 'happiness' dalen afhankelijk van de 'severity' van het probleem.

Tevens zal deze level een quiz systeem bevatten dat werkt met pop-ups.
De speler kan kiezen om een vraag te accepteren of te weigeren.
Door de vraag correct te beantwoorden, zal de speler een bonus krijgen in 'happiness'.
Indien de vraag geweigerd wordt, zal de algemene 'happiness' dalen.
Om de speler toch aan te moedigen om regelmatig een vraag te accepteren, zal de 'happiness' doorheen de level blijven dalen.

**Korte samenvatting van de Game over voorwaarden:**

*enkel één van deze voorwaarden is voldoende om het spel te verliezen*

* Geld van de speler < 0
* Algemene 'happiness' < 0%

**Korte samenvatting van de Game Won voorwaarden:**

*enkel één van deze voorwaarden is voldoende om het spel te winnen*

* Aantal opgeloste problemen > 50
* Aantal consistent correct beantwoorde vragen > 20

#### Technisch

De DGO level zal volgende zaken bevatten:

* **UI Canvas**
	* **Player Score Panel**
		* **Player Score Text**: Geeft de naam en de score van de speler weer. 
	* **Pause Button**: Knop om het spel te pauzeren en om het Pauze Canvas te openen.	
	* **Worker Panel**
		**Worker Text**: Geeft weer hoeveel workers de speler in zijn bezit heeft.
	* **Money Panel**
		* **Money Text**: Geeft weer hoeveel geld de speler heeft.
	* **Happiness Panel**
		* **Happiness Slider**: Geeft het algemene geluk van de stad weer. 
* **Worker Canvas**
	* **Close Button**: Knop om het Worker Canvas te sluiten.
	* **Worker Price text**: Geeft weer hoeveel een nieuwe worker zal kosten.
	* **Buy Button**: Knop om een nieuwe worker aan te kopen.
* **Problems Canvas**
	* **Close Button**: Knop om het Problems Canvas te sluiten.
	* **Available Worker Text**: Geeft de hoeveelheid resterende, inzetbare workers van de speler weer.
	* **Problems Grid**:
		* **Problem Description Text**: Korte beschrijving van het probleem.
		* **Time Remaining Text**: Resterende tijd om een probleem op te lossen.
		* **Workers Deployed Prefab**
			* **Workers Deployed Text**: Aantal ingezette workers om een bepaald probleem op te lossen.
			* **Add Worker Button**: Knop om een worker toe te voegen om een bepaald probleem op te lossen.
			* **Remove Worker Button**: Knop om een worker weg te halen van het probleem.
* **Quiz Canvas**
	* **Question Text**: Willekeurig geselecteerde vraag.
	* **Answer Buttons**: Knoppen met de antwoorden van desbetreffende vraag.
* **Pauze Canvas**
	* **Continue button**: Spel verderzetten.
	* **Exit Button**: Spel beëindigen en terugkeren naar de Main Menu.

#### Mockups & Designs

##### UI Canvas

![UI Canvas](https://github.com/AP-Elektronica-ICT/bachelorproef-18-19-Samvanbogaert17/blob/master/Project/doc/Documentatie%20Sam/Mockups%20-%20Designs%20-%20Manuals/DGO/Mockups/png/lvl_DGO_ui.png "DGO UI Canvas")

##### Problem Canvas

![Problem Canvas](https://github.com/AP-Elektronica-ICT/bachelorproef-18-19-Samvanbogaert17/blob/master/Project/doc/Documentatie%20Sam/Mockups%20-%20Designs%20-%20Manuals/DGO/Mockups/png/lvl_DGO_problemsCanvas.png "DGO Problem Canvas")

##### Worker Canvas

![Worker Canvas](https://github.com/AP-Elektronica-ICT/bachelorproef-18-19-Samvanbogaert17/blob/master/Project/doc/Documentatie%20Sam/Mockups%20-%20Designs%20-%20Manuals/DGO/Mockups/png/lvl_DGO_recruitmentCanvas.png "DGO Worker Canvas")

##### Quiz Canvas

![Quiz Canvas](https://github.com/AP-Elektronica-ICT/bachelorproef-18-19-Samvanbogaert17/blob/master/Project/doc/Documentatie%20Sam/Mockups%20-%20Designs%20-%20Manuals/DGO/Mockups/png/lvl_DGO_eventCanvas.png "DGO Quiz Canvas")

### Consumer Level

#### Algemene Werking / Gameplay

In deze level is het de bedoeling om de speler de beste keuzes te laten maken als consument.
De speler zal bij aanvang van de level enkele keuzes moeten maken waarmee hij de rest van de level zal verderzetten.
Elke keuze zal ook voor- en nadelen met zich meebrengen. 
Deze level is vooral een grote quiz waarvan de vragen en antwoorden afhankelijk zijn van de keuzes van de speler.

De volgende keuzes zijn enkele voorbeelden waartussen de speler kan kiezen:

* Zonnepanelen
	* Voordelen:
		* Genereert energie indien er voldoende zonlicht aanwezig is.
	* Nadelen:
		* Hoge kostprijs.
* Contract van leverancier
	* Contract met enkelvoudig tarief
		* Voordelen:
			* Dagtarief ligt lager dan dagtarief van tweevoudig tarief.
		* Nadelen:
			* Nachttarief is hetzelfde als dagtarief en ligt ook hoger dan nachttarief van tweevoudig tarief.
	* Contract met tweevoudig tarief
		* Voordelen:
			* Dagtarief ligt hoger dan dagtarief van enkelvoudig tarief.
		* Nadelen:
			* Nachttarief is goedkoper dan nachttarief van enkelvoudig tarief.
* Installeren van een Smart Meter
	* Voordelen: 
		* Houdt energieverbruik bij en stuurt deze automatisch door naar de leverancier. Het manueel nakijken van de meter door de DGO is niet meer nodig.
		* (indien zonnepanelen: Houdt de energieproductie van de zonnepanelen bij.)
	* Nadelen:
		* Privacyrisico

**Korte samenvatting van de Game over voorwaarden:**

* Deze level valt niet te falen

**Korte samenvatting van de Game Won voorwaarden:**

* Aantal consistent correct beantwoorde vragen > 30

#### Mockups & Designs

<!-- Deze zijn momenteel nog afwezig. -->

#### Technisch

De Consumer Level zal volgende zaken bevatten:

Bij aanvang van de level:

* **UI Canvas**
	* **Player Score Panel**
		* **Player Score Text**: Geeft de naam en de score van de speler weer. 
	* **Pause Button**: Knop om het spel te pauzeren en om het Pauze Canvas te openen.
* **Pauze Canvas**
	* **Continue Button**: Spel verderzetten.
	* **Exit Button**: Spel beëindigen en terugkeren naar het Main Menu.
* **Inventory Canvas**
	* **Choice Panel**
		* **Choice Title Text**: Geeft de titel van de keuze weer.
		* **Choice 1 Panel**
			* **Advantages**: Voordelen van de 1ste keuze.
			* **Disadvantages**: Nadelen van de 1ste keuze.
		* **Choice 2 Panel**
			* **Advantages Text**: Voordelen van de 2de keuze.
			* **Disadvantages Text**: Nadelen van de 2de keuze.
	* **Next Button**: Knop om naar volgende keuze te gaan.

Na aanvang van de level:

* **UI Canvas**
	* **Player Score Panel**
		* **Player Score Text**: Geeft de naam en de score van de speler weer.
	* **Pause Button**: Knop om het spel te pauzeren en om het Pauze Canvas te openen.
	* **Inventory Panel**
		* **Choice 1 Square**
			* **Details Text**
		* **Choice 2 Square**
			* **Details Text**
		* **Choice 3 Square**
			* **Details Text**
		* **...**
* **Pauze Canvas**
	* **Continue Button**: Spel verderzetten.
	* **Exit Button**: Spel beëindigen en terugkeren naar het Main Menu.
* **Quiz Canvas**
	* **Question Text**: Willekeurig geselecteerde vraag.
	* **Answer Buttons**: Knoppen met de antwoorden van desbetreffende vraag.

### Bug Fixing

Ook de reeds aanwezige onderdelen in het project vereisen de nodige verbeteringen.

De highscores worden momenteel opgeslagen in een bestand op een vaste filepath dat afhankelijk is van de filepath van het project.
Indien de locatie van dit highscores bestand wijzigt, zijn deze niet meer functioneel.

De kleur van het juiste antwoord tijdens de quiz op de supplier level wordt niet altijd correct weergegeven.
Ook op de volgende vraag krijgen de knoppen dezelfde kleur als de kleur van het antwoord op de vorige vraag.

Het spel is ook enkel speelbaar op een resolutie van 1920x1080, anders worden bepaalde canvassen en panels niet correct mee geschaald.


## Keywords
<!--Noteer hier enkele relevante keywords van het onderwerp-->
<!--Minimum 5 keywords-->
* Game Development
* Unity
* Opleiding energie sector
* MeComs product
* Producent / Producer
* Transmission Grid Operator
* Distribution Grid Operator
* Leveranciers / Suppliers
* Consument / Consumer


## Milestones
<!--Geef hier kort weer wat te behalen milestones zijn per week-->

1. Week 1-2: Introductie tot het project, nodige Unity tutorials bekijken, Leren met Unity werken via het project
2. Sprint 1 (Week 3-4): Ontwikkelen van het Producer Level
3. Sprint 2 (Week 5-6): Ontwikkelen van het DGO Level
4. Sprint 3 (Week 7-8): Ontwikkelen van het Consumer Level
5. Sprint 4 (Week 9-10): Ontwikkelen van het TGO Level
6. Sprint 5 (Week 11-12): Bugfixen en afwerken van reeds bestaande onderdelen
7. Sprint 6 (Week 13-14): Bugfixen van het volledige project
8. Week 15: Afwerken van het project en de scriptie
