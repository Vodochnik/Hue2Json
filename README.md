## Projekt in Arbeit :construction:

Das Projekt befindet sich aktuell (15.06.2018) in einem frühen Entwicklungsstadium. Eine detaillierte Roadmap und der Arbeitsfortschritt kann unter [Milestones](https://github.com/rmmlr/Hue2Json/milestones) angezeigt werden.

---

# Hue to Json

## Description
Hauptfunktion der Software __Hue to JSON__ ist das sichern der Parameter/Konfiguration einer _Philips Hue Bridge_ im JSON-Format. Die erstellten Backup-Dateien können zu einem späteren Zeitpunkt oder auf einer anderen Bridge wieder eingeladen werden.  
Aufgrund der fehlenden Möglichkeit, per API neue Geräte an die Bridge zu Koppeln, bietet __Hue to JSON__ eine sogennante [Remapping](https://github.com/rmmlr/Hue2Json#remapping) Funktionalität, somit kann mit wenigen Klicks eine fehlerhafte Konfiguration wiederhergestellt, bzw. eine neue Bridge konfiguriert werden. Zudem werden einige nützliche [Utilities](https://github.com/rmmlr/Hue2Json#utilities) bereit gestellt.


## Installation
Eine Installation des Programmes ist nicht erforderlich. Die Anwendung (hue2json.exe) sollte sich, einschließlich benötigter Bibliotheken (\*.dll) in einem Verzeichnis mit Schreib-/Lesezugriff befinden und kann direkt gestartet werden.  
Fertig kompilierte Releases werden im [Release-Feed](https://github.com/rmmlr/Hue2Json/releases) zum Download bereit gestellt, [siehe Releases](#releases).


## Usage

### Parameter
Folgende Parameter können verarbeitet werden. Ausgelesen werden alle Parameter, ein Restore ist hingegen nicht vollumfänglich möglich.

| Parameter     | Beschreibung         | Restore                               |
| ------------- |----------------------|:-------------------------------------:|
| Lights        | Leuchtmittel         | [siehe Remapping](#remapping)         |
| Groups        | Gruppen u. Räume     | :heavy_check_mark:                    |
| Schedules     | Timer                | :heavy_check_mark:                    |
| Scenes        | Szenen               | :heavy_check_mark:                    |
| Sensors       | Sensoren u. Schalter | [siehe Remapping](#remapping)         |
| Rules         | Regeln               | :heavy_check_mark:                    |
| Configuration | allg. Konfiguration  | bedingt möglich                       |
| Capability    | Speicherbelegung     | nur lesender Zugriff, rein Informativ |
| ResourceLinks | Links                | :heavy_check_mark:                    |
| WhiteList     | Benutzerliste        | :x:                                   |



### Remapping
Soll ein Backup auf einen neuen, respektive zurückgesetzten System wiederhergestellt werden, so ist es erforderlich alle Teilnehmer/Geräte (Leuchtmittel, Sensoren, Schalter, ...) manuell an die Bridge anzulernen. Hierbei vergibt die Bridge den Geräten neue IDs. Diese IDs werden intern für die Steuerung und Verknüpfung verwendet und sind somit essentiell. __Hue to JSON__ kann die *neuen IDs* auslesen und im vorhandenen Backup ein Remapping vornehmen, hierbei werden die alten IDs durch die neuen ersetzt. Da hierbei jedoch auch die UniqueIDs der Geräte herangezogen werden, bleibt die ursprüngliche Zuordnung erhalten.

### Anonymisierung
Standardgemäß werden alle ausgelesenen __Usernames__ anonymisiert und durch generische Namen (User 1, User 2 ...) ersetzt. Eine Zuordnung der einzelnen User zu den angelegten Szenen, Regeln und Links bleibt dabei erhalten.

Gerade wenn man die ausgelesenen Parameter-Dateien weiter geben möchte (z.B. zur Fehlerdiagnose), können optional Seriennummern und Namen anonymisiert werden.

#### Seriennummern
Alle ausgelesenen Unique-IDs, Seriennummern sowie die Ethernet-Konfiguration werden anonymisiert.

#### Namen
Alle ausgelesenen Beschreibungen und Namen einschließlich Gerätenamen werden anonymisiert.

### Utilities
Neben den Sichern und Wiederherstellen der Bridge Konfiguration bietet __Hue to JSON__ verschiedenen kleine Dienstfunktionen.

#### Bridge Reset
Löschen aller Einstellungen und Konfigurationen welche auf der Bridge hinterlegt sind. Nach einem Reset ist die Bridge "leer" lediglich ein User-Eintrag bleibt erhalten. Diese Funktion unterscheidet sich maßgeblich  vom Reset der Bridge per "Reset-Button". Da hier die jeweilige Werkseinstellung wieder hergestellt wird, was eigentlich nicht mit einem Reset gleich zusetzen ist. Kommt die Bridge zum Beispiel aus einem Starterset, werden hier auch die Leuchtmittel und Sensoren aus dem Set wieder eingetragen.

#### Speicherbelegung anzeigen
Anzeige der aktuellen Speicherbelegung auf der Bridge. Angezeigt werden alle verfügbaren Ressourcen, sowie die prozentuale Belegung der Bridge. Für Regeln gibt es eine weitere detaillierte Auflistung, da hier auch die eigentliche Konfiguration der Regeln entscheidend für die Auslastung ist.  
Mehr zum Thema Regeln und den gegebenen Limits kann in folgenden Artikel [Hue Bridge Limits (100prznt)](https://100prznt.de/philips-hue/hue-bridge-limits/) nachgelesen werden.

#### Einschaltverhalten der Leuchtmittel/Lampen konfigurieren
Auf der Tab-Page `Leuchtmittel/Lampen` kann durch einfaches klicken der entsprechenden Leuchtmittel ein Konfigurationsmenu für zur Auswahl des Einschaltverhaltens geöffnet werden. Hier können ab Version 1.0.80 drei verschiedene Konfigurationen eingestellt werden.

| Konfiguration | Beschreibung                                                                                                        |
| ------------- |---------------------------------------------------------------------------------------------------------------------|
| Safty         | Nach dem Einschalten geht das Leuchtmittel an, mit 100 % Helligkeit und einer Farbtemperatur von 2700 K (Warmweiß). |
| Powerfail     | Nach dem Einschalten nimmt das Leuchtmittel den Zustand an, welches es vor dem Ausschalten hatte. War es ausgeschalten bleibt es aus. Hat es mit 50 % Helligkeit rot geleuchtet, wird es nach dem Einschalten auch wieder mit 50 % Helligkeit rot leuchten. |
| Last On State | Nach dem Einschalten geht das Leuchtmittel an, und zwar mit den Einstellungen, mit welchem es zuletzt geleuchtet hat. Auch wenn es vor dem Ausschalten schon aus war, werden die Einstellungen des letzten „leuchtenden Zustandes“ geladen. |
| Custom        |                                                                                                     |


### Programm
Hauptfenster:

![MainView 1.0.67 - Screenshot][MainView_1_0_67]

[MainView_1_0_67]: docs/img/MainView_1.0.67.png "MainView 1.0.67 - Screenshot"


## Releases
Dieses Projekt wird auf der Continuous Integration Plattform [AppVeyor](https://www.appveyor.com/) kompiliert und im [Release-Feed](https://github.com/rmmlr/Hue2Json/releases) veröffentlicht.

[![AppVeyor Build](https://img.shields.io/appveyor/ci/rmmlr/Hue2Json.svg)](https://ci.appveyor.com/project/rmmlr/hue2json)  
[![AppVeyor Tests](https://img.shields.io/appveyor/tests/rmmlr/hue2json/master.svg)](https://ci.appveyor.com/project/rmmlr/hue2json/build/tests)

[![GitHub Release](https://img.shields.io/github/release/rmmlr/Hue2Json.svg)](https://github.com/rmmlr/Hue2Json/releases/latest)  
[![GitHub (Pre-)Release](https://img.shields.io/github/release/rmmlr/Hue2Json/all.svg)](https://github.com/rmmlr/Hue2Json/releases) (Pre-)Release


## Contributing

Ich bin auf der Suche nach weiteren Entwicklern für dieses Projekt. Ideeen und Verbesserungen können aus einem Fork per Pull Request eingereicht werden.

[![GitHub Contributors](https://img.shields.io/github/contributors/rmmlr/Hue2Json.svg)](https://github.com/rmmlr/Hue2Json/graphs/contributors)


## Credits

* **Elias Ruemmler** - *Initial work* - [rmmlr](https://github.com/rmmlr)

Unter [Contributors](https://github.com/rmmlr/Hue2Json/contributors) können weitere Projekt-Unterstützer eingesehen werden.

### Open Source Project Credits

* [Q42.HueApi](https://github.com/Q42/Q42.HueApi) Bedienung der Hue-API
* [Newtonsoft.Json](https://www.newtonsoft.com/json) Parameter-Serialisierung und Deserialisierung der Hue-API Antworten
* [Newtonsoft.Json.Bson](https://www.newtonsoft.com/json) AppKey-Serialisierung
* [UIkit](https://github.com/uikit/uikit) Parameter-Visualisierung

## License

Dieses Projekt (Hue to JSON) ist lizenziert unter der [MIT Lizenz](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form").  
Weitere Details unter [LICENSE](https://github.com/rmmlr/Hue2Json/blob/master/LICENSE.txt).

[![license](https://img.shields.io/github/license/rmmlr/Hue2Json.svg)](https://github.com/rmmlr/Hue2Json/blob/master/LICENSE.txt) 
