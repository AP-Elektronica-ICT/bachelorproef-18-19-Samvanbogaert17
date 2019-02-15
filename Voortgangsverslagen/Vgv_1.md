# Voortgangsverslag #1
# Gamification van een opleiding:
## Promotors

* **Stagecoördinator:** Dhr. Smets Marc - marc.smets@ap.be
* **Stagementor:** Mevr. Braspenning Cindy - Cindy.Braspenning@mecoms.be
* **Stagebegeleider:** Dhr. Horsmans Serge - serge.horsmans@ap.be
* **Opleidingshoof:** Dhr. Masset Yves - yves.masset@ap.be

## Onderwerp

Het doel van het project is de huidige opleiding, die vooral bestaat uit enkele PDF's die informatie bevatten over de werking van de energie sector en korte introductie tot het Mecoms product, te vervangen door een spel
die de speler op een leuke, maar toch informatieve wijze de werking van alle sectoren in de energie sector zal aanleren.

High-level overzicht van alle sectoren:

* Producer / Producent: Deze produceert alle energie en verkoopt deze door aan de Supplier / Leverancier.
* TGO (Transmission Grid Operator): Het doel van de TGO is de hoogspanning die gegenereerd is door de producent om te zetten naar een lagere hoogspanning.
* DGO (Distribution Grid Operator): Het doel van de DGO is de lagere hoogspanning van de TGO om te zetten naar laagspanning voor huishoudelijk gebruik. Ook houdt de DGO zich bezig met het onderhoud van het elektriciteitsnet en het uitlezen en vervangen van de elektriciteitsmeters bij de consumenten.
* Supplier / Leverancier: Koopt energie van de producent en verkoopt deze door aan de consument. 
* Consumer / Consument: De consument zelf kiest dus bij welke leverancier hij zijn energie aankoopt.

Dit project was al reeds in werking door andere jobstudenten die tijdens de zomer van 2018 van start zijn gegaan met het ontwikkelen van dit project.
Er was al reeds een spel aanwezig, waarin ook al enkele zaken aanwezig waren, zoals:

* **Main Menu**
	* **Levels Button**: Knop om naar Levels Menu te navigeren
	* **Highscore Button**: Knop om naar Highscore Menu te navigeren
	* 
* **Highscore Menu**
	* **Highscore Lijst**: Lijst van alle spelers met hun bijhorende score.
* **Levels Menu**
	* **Introdution Button**: Knop om Introductie level te starten
	* **Producer Button**: Knop om Producer level te starten
	* **TGO Button**: Knop om TGO level te starten
	* **DGO Button** Knop om DGO level te starten
	* **Supplier Button**: Knop om Supplier level te starten
	* **Consumer Button**: Knop om Consumer level te starten
* **Megan Introductie**: Introductie dat uitleg geeft over de inhoud van het spel
* **Game Introductie level**: Introductie tot alle levels en geeft een high-level overzicht van alle sectoren in de energie sector
* **leverancier level**
	
Het gewenste resultaat tegen het einde van de BAP zal het verwezelijken zijn van de ontbrekende levels (producer, TGO, DGO, Consumer) en de reeds aanwezige zaken te bugfixen.
Elke level zal zijn eigen unieke werking hebben.

### Producer Level

#### Algemene Werking / Gameplay

In de producer level is het de taak van de speler om zoveel mogelijk contracten aan de 'suppliers' te verkopen, en genoeg energie producerende gebouwen aan te kopen.
Sommige energie producerende gebouwen produceren ook vervuiling en dit wordt dan ook in rekening gebracht. Indien het vervuilingsgehalte van de stad te hoog is, zal de speler automatisch de level verliezen.
Ook gebouwen met groene energie hebben hun eigen voor en nadelen. Gebouwen die groene energie produceren zijn meestal afhankelijk van het weer of andere omstandigheden.

Doorheen de level, op willekeurige interval, zal het weer dan ook veranderen.
Indien het weer meer of minder gunstig is, zullen de 'grijze' energie producerende gebouwen meer of minder moeten produceren afhankelijk van het weer.
Door de energie productie van deze gebouwen te doen stijgen of dalen, zal dit ook enige extra vervuiling met zich meebrengen.

De speler kan extra gebouwen aankopen via het 'Market Menu'. Hier kan de speler zien hoeveel elk gebouw kost, produceert,wat voor type energie het gebouw produceert en hoeveel vervuiling dit gebouw procentueel met zich meebrengt.
Ook kan de speler hier zien hoeveel gebouwen hij van elk type heeft en de bijhorende statistieken hiervan (=totale productie, vervuiling en het eventuele gedrag in productie afhankelijk van het weer).

Eerder aangehaald zal de speler dus contracten moeten verkopen aan de 'suppliers', deze zullen aangeboden worden na een bepaald interval via een popup.
Op elk contract zal te zien zijn hoeveel energie de speler aan de leverancier verkoopt en hoeveel winst hij maakt met dit contract.
Indien de speler

Elk contract zal terecht komen op het 'Contract Menu'.
Op dit menu is te zien welke contracten de speler al bezit.
Ook is er een optie om een contract op te zeggen.
Dit zal vooral nodig zijn als de speler te weinig energie produceert en dus een contract moet opzeggen.
Indien de speler dit niet doet, zal de algemene happiness dalen en ook hierdoor kan de speler dus de level verliezen.

Er zullen doorheen de level ook andere, gelijkaardige popups verschijnen als de popups van de contractaanbiedingen.
Deze popups duiden aan dat de speler een vraag kan beantwoorden in verband met de producer.
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
* Aantal consistent correct beantwoordde vragen > 20
* Aantal aangenomen contracten > 15

#### Technisch

De producer level zal volgende zaken bevatten:

* **UI Canvas**
	* **Player Score Panel**
		* **Player Score Text**: Geeft naam en score weer van de speler
	* **Pause Button**: Knop om spel te pauzeren en om Pauze Menu te openen
	* **Producing Panel**
		* **Producing Text**: Geeft het verschil weer tussen de totaal geproduceerde energie en de totaal verkochte energie aan alle leveranciers
	* **Money Panel**
		* **Money Text**: Geeft weer hoeveel geld de speler heeft
	* **Pollution Panel**
		* **Pollution Slider**: Geeft de vervuiling van de stad weer
	* **Happiness Panel**
		* **Happiness Slider**: Geeft het algemene geluk weer van de stad
	* **Contract popup**
		* **Contract content**: Geeft de naam van de leverancier weer, zowel als de hoeveelheid verkochte energie en de winst voor de speler
		* **Accept Button**: Knop om contract te accepteren
		* **Decline Button**: Knop om contract te weigeren
		* **(Update Button)**: Knop om bestaand contract up te daten
	* **Quiz Popup**
		* **Accept Button**: Knop om de quiz te starten
		* **Decline Button**: Knop om de quiz te weigeren
* **Market Canvas**
	* **Close Button**: Knop om het canvas te sluiten
	* **Total Energy Production Text**: tekst dat weergeeft hoeveel alle gebouwen in totaal produceren, uitgedrukt in kWh
	* **Market Panel**
		* **Market Grid**
			* **Building Type Text**: Geeft weer wat voor type gebouw de speler kan aankopen
			* **Energy Type Text**: Geeft weer wat voor type energie het gebouw produceert
			* **Production Text**: Geeft weer hoeveel energie dit gebouw gemiddeld zal produceren, uitgedrukt in kWh
			* **Pollution Text**: Geeft weer hoeveel vervuiling dit gebouw met zich meebrengt, uitgedrukt in een percentage
			* **Price Text**: Geeft weer hoeveel het gebouw kost
			* **Buy button**: Knop om een gebouw te kopen
		* **Market Button**: Schakelt het market canvas over naar het Market Panel
		* **Installed Button**: Schakelt het market canvas over naar het Installed Panel
	* **Installed Panel**
		* **InstalledGrid**
			* **Building Type Text**: Geeft weer wat voor type gebouw de speler kan aankopen
			* **Energy type Text**: Geeft weer wat voor type energie het gebouw produceert
			* **Production Text**: Geeft weer hoeveel energie dit gebouw gemiddeld zal produceren, uitgedrukt in kWh
			* **Pollution Text**: Geeft weer hoeveel vervuiling dit gebouw met zich meebrengt, uitgedukt in een percentage
			* **Behaviour Prefab**
				* **Behaviour Percentage Text**: Geeft de stijging of daling weer van de productie van de geïnstalleerde gebouwen, uitgedrukt in een percentage
				* **Behaviour Arrow**: Pijl dat weergeeft of het een positieve stijging of negatieve daling is
			**Sell Button**: Knop om een gebouw te verkopen
* **Contract Canvas**
	* **Close Button**
	* **Contract Grid**
		* **Supplier Name**: geeft de naam van de leverancier weer
		* **Energy Sold**: geeft weer hoeveel energie de speler levert aan de leverancier, uitgedrukt in kWh
		* **Profit**: geeft weer hoeveel winst de speler maakt op het contract
		* **Cancel** Button: knop om het contract te beëindigen
* **Quiz Canvas**
	* **Question Text**: Willekeurig geselecteerde vraag
	* **Answer Buttons**: Knoppen met de antwoorden van desbetreffende vraag
* **Pauze Canvas**
	* **Continue button**: Spel verderzetten
	* **Exit Button**: Spel beëindigen en terugkeren naar de main menu


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

Verdere uitwerking voor de TGO level is zeker en vast nog nodig, maar was nog geen prioriteit volgens mijn stagementor.
<!-- Voor deze level is er nog veel analyse werk nodig -->

### DGO Level

#### Algemene Werking / Gameplay

In de DGO level, zal de speler met 'workers' te werk gaan om problemen op te lossen die zich in de stad zullen voordoen.
Elk probleem zal een zijn eigen 'moeilijkheidsgraad' hebben. Sommige problemen zullen dus meer workers vereisen en zullen ook langer duren voor dat deze problemen opgelost zijn.
De speler krijgt ook de mogelijkheid om bij elk probleem extra workers toe te voegen of te verwijderen.

De speler kan zien welke problemen er zich voordoen in het 'Problems Menu' dat de speler kan openen via het happiness panel

De speler zal een beloning krijgen per opgelost probleem in de vorm van geld, waarmee de speler een extra worker kan aankopen.
De prijs per worker zal exponentieel stijgen naarmate de speler meer workers bezit.

Ook deze level zal werken met een algemene 'happiness'.
Indien de speler geen workers inzet om een probleem om te lossen, zal per tijdseenheid de 'happiness' dalen afhankelijk van de 'moeilijkheidsgraad' van het probleem.

Ook dit level zal een quiz systeem bevatten dat zal werken met popups.
De speler kan kiezen om dit te accepteren of te weigeren.
Door de vraag correct te beantwoorden, zal de speler een bonus krijgen in 'happiness'
door de vraag te weigeren, zal de algemene 'happiness' dalen.
Om de speler toch aan te moedigen om regelmatig een vraag te accepteren, zal de 'happiness' doorheen de level blijven dalen.

**Korte samenvatting van de Game over voorwaarden:**

*enkel één van deze voorwaarden is voldoende om het spel te verliezen*

* Geld van de speler < 0
* Algemene 'happiness' < 0%

**Korte samenvatting van de Game Won voorwaarden:**

*enkel één van deze voorwaarden is voldoende om het spel te winnen*

* Aantal opgeloste problemen > 50
* Aantal consistent correct beantwoordde vragen > 20

#### Technisch

De DGO level zal volgende zaken bevatten:

* **UI Canvas**
	* **Player Score Panel**
		* **Player Score Text**: Geeft naam en score van de speler weer 
	* **Pause Button**: Knop om spel te pauzeren en om Pauze Canvas te openen	
	* **Worker Panel**
		**Worker Text**: Geeft weer hoeveel workers de speler in zijn bezit heeft
	* **Money Panel**
		* **Money Text**: Geeft weer hoeveel geld de speler heeft
	* **Happiness Panel**
		* **Happiness Slider**: Geeft het algemene geluk van de stad weer 
* **Worker Canvas**
	* **Close Button**: Knop om Worker Canvas te sluiten
	* **Worker Price text**: Geeft weer hoeveel een nieuwe worker zal kosten
	* **Buy Button**: Knop om een nieuwe worker aan te kopen
* **Problems Canvas**
	* **Close Button**: Knop om Problems Canvas te sluiten
	* **Available Worker Text**: Geeft de hoeveelheid resterende, inzetbare workers weer van de speler
	* **Problems Grid**:
		* **Problem Description Text**: Korte beschrijving van het probleem
		* **Time Remaining Text**: Resterende tijd om probleem op te lossen
		* **Workers Deployed Prefab**
			* **Workers Deployed Text**: Aantal ingezette workers om probleem op te lossen
			* **Add Worker Button**: Knop om worker toe te voegen om probleem op te lossen
			* **Remove Worker Button**: Knop om worker weg te halen van het probleem
* **Quiz Canvas**
	* **Question Text**: Willekeurig geselecteerde vraag
	* **Answer Buttons**: Knoppen met de antwoorden van desbetreffende vraag
* **Pauze Canvas**
	* **Continue button**: Spel verderzetten
	* **Exit Button**: Spel beëindigen en terugkeren naar de main menu

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

In deze level is het de bedoeling om de speler de beste keuzes te laten maken naar de consument toe.
De speler zal in de aanvang van de level enkele keuzes moeten maken waarmee hij de rest van de level mee zal verder zetten.
Elke keuze zal ook voor- en nadelen met zich meebrengen. 
Deze level is vooral een grote quiz waarvan de vragen en antwoorden afhankelijk zijn van de keuzes van de speler.

Deze keuzes kunnen inhouden, maar zijn niet gelimiteerd tot:

* Zonne Panelen
	* Voordelen:
		* Genereert energie indien er voldoende zonlicht aanwezig is.
	* Nadelen:
		* Hoge kostprijs.
* Contract van leverancier
	* Contract met eenvoudig tarief
		* Voordelen:
			* Dagtarief ligt lager dan dagtarief van tweevoudigtarief
		* Nadelen:
			* Nachttarief is hetzelfde als dagtarief en ligt ook hoger dan nachttarief van tweevoudigtarief
	* Contract met tweevoudig tarief
		* Voordelen:
			* Dagtarief ligt hoger dan dagtarief van eenvoudigtarief
		* Nadelen:
			* Nachttarief is goedkoper dan nachttarief van eenvoudigtarief
* Installeren van een Smart Meter
	* Voordelen: 
		* Houdt energieverbruik bij en stuurt deze automatisch door naar leverancier, manueel nakijken van meter door DGO is niet meer nodig.
		* (indien zonnepanelen: Houdt bij hoeveel energie de zonnepanelen hebben opgeleverd)
	* Nadelen:
		* Privacyrisico

**Korte samenvatting van de Game over voorwaarden:**

* Deze level valt niet te falen

**Korte samenvatting van de Game Won voorwaarden:**

* Aantal consistent correct beantwoordde vragen > 30

#### Mockups & Designs

<!-- Deze zijn momenteel nog afwezig. -->

#### Technisch

De Consumer Level  zal volgende zaken bevatten:

Bij aanvang van de level:

* **UI Canvas**
	* **Player Score Panel**
		* **Player Score Text**: Geeft naam en score van de speler weer 
	* **Pause Button**: Knop om spel te pauzeren en om Pauze Canvas te openen
* **Pauze Canvas**
	* **Continue button**: Spel verderzetten
	* **Exit Button**: Spel beëindigen en terugkeren naar de main menu
* **Inventory Canvas**
	* **ChoicePanel**
		* **Choice Title Text**: Geeft de titel weer van de keuze
		* **Choice 1 Panel**
			* **Advantages**: Voordelen van de 1ste keuze
			* **Disadvantages**: Nadelen van de 1ste keuze
		* **Choice 2 Panel**
			* **Advantages Text**: Voordelen van de 2de keuze
			* **Disadvantages Text**: Nadelen van de 2de keuze
	* **Next Button**: Knop om naar volgende keuze te gaan

Na aanvang van de level:

* **UI Canvas**
	* **Player Score Panel**
		* **Player Score Text**: Geeft naam en score van de speler weer 
	* **Pause Button**: Knop om spel te pauzeren en om Pauze Canvas te openen
	* **Inventory Panel**
		* **Choice 1 Square**
			* **Details Text**
		* **Choice 2 Square**
			* **Details Text**
		* **Choice 3 Square**
			* **Details Text**
		* **...**
* **Pauze Canvas**
	* **Continue button**: Spel verderzetten
	* **Exit Button**: Spel beëindigen en terugkeren naar de main menu
* **Quiz Canvas**
	* **Question Text**: Willekeurig geselecteerde vraag
	* **Answer Buttons**: Knoppen met de antwoorden van desbetreffende vraag

### Bug Fixing

Ook van de reeds aanwezige onderdelen in het project zijn er de nodige verbetereingen nodig.

De highscores worden momenteel opgeslagen in een bestand op een vaste filepath afhankelijk van de filepath van het project.
Indien de locatie van dit highscores bestand dus verandert, dan zijn de highscores niet meer functioneel.

De kleur van het correcte antwoord tijdens de quiz op de supplier level worden niet altijd correct weergegeven.
Ook op de volgende vraag is krijgen de knoppen dezelfde kleur als het antwoord van de vorige vraag.

Het spel is ook enkel speelbaar op 1920x1080, anders worden bepaalde canvassen en panels niet correct mee geschaald.


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
* leveranciers / Suppliers
* Consument / Consumer


## Milestones
<!--Geef hier kort weer wat te behalen milestones zijn per week-->

1. Week 1-2: Introductie tot het project, nodige Unity tutorials bekijken, Leren met Unity werken via het project
2. Sprint 1 (Week 3-4): Ontwikkelen van Producer Level
3. Sprint 2 (Week 5-6): Ontwikkelen van DGO Level
4. Sprint 3 (Week 7-8): Ontwikkelen van Consumer Level
5. Sprint 4 (Week 9-10): Bugfixen en afwerken van reeds bestaande onderdelen
6. Sprint 5 (Week 11-12): Ontwikkelen van TGO Level
7. Sprint 6 (Week 13-14): Bugfixen van het volledige project
8. Week 15: Afwerken project en scriptie
