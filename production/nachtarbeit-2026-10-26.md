# Nachtarbeit 2026-10-25/26

Der Entwickler hat am 2026-10-25 gegen 22:05 UTC für etwa 6 Stunden die Erlaubnis
gegeben, ohne Rückfrage zu entscheiden. Dieses Dokument hält fest, was in dieser Zeit
gemacht wurde und **jede Entscheidung, die ohne Freigabe getroffen wurde**, damit sie
morgens geprüft und bei Bedarf zurückgenommen werden kann. Alle Änderungen liegen als
einzelne Commits auf dem Branch `ccr-5d375a45-xl9qbu` (Draft-PR #1); nichts wurde
gemergt oder außerhalb des Branches veröffentlicht.

## Plan

1. Runde 2 `/design-review` Simulationskern (lean), mit den empfohlenen Punkten aus Runde 1
2. Runde 2 Menschen & Bevölkerung (lean)
3. Runde 2 Zustimmungsmodell (full, mit Spezialisten-Agenten)
4. `/consistency-check`
5. Falls Zeit bleibt: Entwurf Welt & Insel (#4)

Regel der Roadmap: höchstens 2 Review-Runden pro GDD. Runde 2 ist also die letzte;
was danach offen bleibt, wandert ins nächste GDD oder in die Stories.

## Ergebnisse

### 1. Simulationskern, Review-Runde 2 (lean)

Urteil: **NEEDS REVISION** mit 2 kleinen neuen Blockern, beide behoben. Die 4 Blocker aus
Runde 1 waren gelöst. Zusätzlich sind alle 14 empfohlenen Punkte aus Runde 1
eingearbeitet. Damit ist die zweite und laut Roadmap letzte Runde für dieses GDD erledigt.
Der Status im Systems-Index bleibt **In Review**: Freigeben (Approved) kann nur ein Review
mit dem Urteil APPROVED oder du.

- Blocker: In der Pause wurden alle Eingriffe abgelehnt, der Abgleich der Ansicht muss
  aber auch in der Pause ein „abgesetzt“ einreihen können. Jetzt sind nur neue Eingriffe
  des Spielers gesperrt (Regel 13, AC-16, neues AC-35).
- Blocker: Es fehlte, welches System einen Eingriff anwendet. Jetzt hat jede Eingriffsart
  genau einen Handler (Regel 4.1, neues AC-36).
- Empfohlen und erledigt: Zufallsgenerator vollständig festgelegt (xoshiro256**,
  `NextInt` nach Lemire, `NextDouble`) mit einer Tabelle von Referenzwerten für AC-09;
  ID-Zähler für Ereignisse und Eingriffe; Erzeugen einer Welt (neue Regel 17, AC-33);
  ungültiger Ort eines Endereignisses; neue Tuning-Knobs-Tabelle; veraltete
  Dependencies; kleinere Zahlenfehler; AC-01, AC-02, AC-03, AC-09, AC-28 präzisiert,
  AC-34 neu.
- Registry: `menschen_system_id` und `menschen_year_offset` neu (beide Wert 2).
- Konzept: die alte Angabe „Stammeslogik mit etwa 1 Hz“ auf „2 Hz bei 1×“ angeglichen.

### 2. Menschen & Bevölkerung, Review-Runde 2 (lean)

Urteil: **NEEDS REVISION** mit 1 neuem Blocker, behoben. Die 4 Blocker aus Runde 1
waren gelöst. Dazu sind die 11 empfohlenen Punkte eingearbeitet. Letzte Runde laut
Roadmap; Status bleibt **In Review**.

- Blocker: Der überarbeitete Kern gibt das Packen eines Statisten und die Rückgabe aus
  Gottes Hand als Auftrag an Menschen weiter, Regel 23 kannte beides nicht. Jetzt ist
  der Ablauf festgelegt (Regel 10, 23, neues AC-32).
- Empfohlen und erledigt: Namen eindeutig über alle Stämme; Gefangene wandern im Gebiet
  des Stamms, der sie hält; Gehaltene wechseln bei einer Abspaltung nie; Zustand als
  „Masse / Lebend / Tot plus vier Merkmale“; AC-05 sinnvoll gemacht; double und Promille
  statt float; Tuning-Knobs-Tabelle; Wert d_t und R / P_person korrigiert; AC-33 neu.

### 3. Zustimmungsmodell, Review-Runde 2 (full, mit Spezialisten)

Gelaufen sind `game-designer`, `systems-designer`, `qa-lead`, `unity-specialist`,
`security-engineer` und als Senior `creative-director`. Urteil: **NEEDS REVISION** (nicht
MAJOR: der Kern trägt). Es gab 16 Blocker; ich habe die 16 Korrekturen des Creative
Directors eingearbeitet. Was er ausdrücklich dir überlässt, steht jetzt als offene Frage
im GDD. Status bleibt **In Review**; der Creative Director empfiehlt, dass du das GDD
selbst noch einmal ansiehst, bevor es freigegeben wird.

Die wichtigsten Funde und was jetzt im GDD steht:
- **Fälschbares Ergebnis:** In C# lässt sich jede Struktur leer erzeugen (`default`), das
  hätte als Zustimmung gelten können. Jetzt: Wert 0 heißt immer „ungültig“, das Ergebnis
  trägt einen Stempel (Schritt, Anfrage, Tabelle) und gilt nur für genau diese Handlung in
  diesem Schritt; eigene Typen für Beginn und Halten; ein **Handlungsregister** ist der
  einzige Weg in eine Handlung (Regel 18, AC-25).
- **Anfragen baut das Modell selbst** aus IDs, kein Aufrufer kann Werte einsetzen
  (Regel 19).
- **Sperren:** „in Gottes Hand“, „tot“ und „erschüttert“ sind jetzt Sperren, die
  Sperrenprüfung läuft nach Gotteskraft (vorher an der falschen Stelle), die Ansicht
  sperrt vor jeder Gewalt-Darstellung selbst und Ragdolls kollidieren nicht mit einer
  Szene (Regeln 8, 14, 20; Kern Formel 5; AC-21, AC-26).
- **Gewalt als Hebel geschlossen:** Partner töten hebt „treu gebunden“ nicht auf
  (Trauerzeit), Eroberung macht niemanden über Nacht offener (strengere Norm für eine
  Zeit), gefangene Harem-Mitglieder fallen sofort heraus, Sklaverei wäre immer
  „nicht frei“ (Regeln 4, 8, 16, 19; AC-31).
- **Ritual:** Die Ritual-Zeile prüft nur noch die Teilnahme (mit Frömmigkeits-Tor), jeder
  Kontakt im Ritual wird als die Handlung geprüft, die er ist. Frömmigkeit ersetzt nie
  Begehren (Regel 7, AC-30).
- **Mathematik und Datentabelle:** „kein Begehren“ ist jetzt garantiert der Grund, wenn
  das Begehren fehlt; neues Merkmal „kulturell“ zeigt, wo die Kultur entschieden hat;
  strengere Ladeprüfung der Tabelle (Formeln 2 und 4, AC-19, AC-29).
- **Tests:** strengere Grenzen für den Monte-Carlo-Test, Bildaufzeichnung statt
  3 Screenshots, Fälle für leere Werte, einseitiges Begehren und Blitz (AC-02, AC-20,
  AC-22, AC-28).

Mitgezogen: Simulationskern (Sperrenprüfung nach Gotteskraft, Sperrmerkmale) und Menschen &
Bevölkerung (Opferwahl zieht keine Beteiligten einer Handlung; Stammeswechsel wird
gemerkt; nur „frei“ lässt zu).

### 4. Konsistenz-Check (alle vier GDDs)

Kein Widerspruch bei den registrierten Werten. Veraltete Verweise („Welt & Insel hat noch
kein GDD“) in Kern und Menschen sind angeglichen, die Sperrenprüfung steht jetzt auch in
der Schritt-Reihenfolge von Kern-Regel 4.2. Registry: 4 neue Einträge für Welt & Insel
(Rastergröße 40 × 28, Formeln Tragfähigkeit und Nahrung).

### 5. Welt & Insel, Entwurf

Neu: `design/gdd/welt-und-insel.md` (Status **Draft**). Ich habe die Inselregeln aus
Prototyp 1 übernommen (Insel erzeugen, Startplätze, Gebiet, Tragfähigkeit, Ernte und
Hunger, Dürre, verbrannte Erde), damit die Messwerte aus Prototyp 1 gelten, und die
Verträge erfüllt, die Kern und Menschen schon festlegen. Nachgerechnet mit einem
Python-Nachbau: etwa 61 % Land, ein Startstamm hat etwa 154 Zellen und eine
Tragfähigkeit von etwa 77 bei 48 Köpfen, Dürre in etwa 13 % der Jahre.

**Review Runde 1 (full: Systems Designer, Game Designer, QA Lead, World Builder, Creative
Director): NEEDS REVISION, 11 blockierende und 18 empfohlene Punkte, alle eingearbeitet.**
Die wichtigsten: Beim Abgleich mit `prototype.html` hatte mein Entwurf vier Übertragungsfehler
(Gebiet jedes Jahr statt nur bei Änderungen, Platzsuche bei Abspaltungen, Vorrat für
abgespaltene Stämme und Fest-Segen, Aussterben unter 3 Köpfen); die sind korrigiert, damit
die Kalibrierung wirklich gilt. Neu sind dauerhafte Brandnarben (Jahr und Ursache je Zelle),
ein Hauptgrund für jede Hungersnot (Dürre, Brand, Krieg oder Gedränge), eine Liste benannter
Orte (Siedlung, Ritualplatz, Festplatz, Brandstelle, Ruine) und 18 Akzeptanzkriterien mit
festen Zahlen. Status bleibt **Designed** (Runde 2 steht aus).

**Was auf dich wartet:** Der Game Designer hält einen Blitz für zu schwach (rund 3 %
Tragfähigkeit) und Ära I für zu ruhig. Seine Vorschläge (verbrannte Erde trägt 0 und gibt
danach einen Aschebonus, 3 Startstämme; später regionale Dürren, gewichtete Gebiete) ändern
die Simulation. Der Creative Director hat entschieden: Das entscheidest du, und erst nach
der ersten Kalibrierungsmessung (AC-16). Beides steht mit Kennzahlen in den Open Questions.

**Review Runde 2 (lean, letzte Runde): NEEDS REVISION, 3 blockierende Punkte, behoben.**
Ein Testfall nahm an, eine Zelle sei immer Wasser, obwohl die Inselformel das nicht
garantiert (jetzt mit eingespielter Insel); beim Fest-Segen war offen, ob der Vorrat mit dem
Volk vor oder nach dem Geschenk rechnet (jetzt danach, wie im Prototyp); und das Aussterben
unter 3 Köpfen ist jetzt auch in Menschen & Bevölkerung Regel 2 so festgelegt (offen bleibt nur,
was mit den letzten 1–2 Menschen geschieht; bis dahin verschwinden sie wie im Prototyp).
Status jetzt **In Review** wie die anderen drei; „Approved“ entscheidest du.

### 6. Zweite Konsistenzprüfung (alle 4 GDDs)

**PASS**: 29 Registry-Einträge über Kern, Menschen, Zustimmung und Welt geprüft, kein
Widerspruch. Der einzige Widerspruch (Aussterben) war schon in Runde 2 von Welt behoben.
Nachgezogen: Kern und Menschen nennen Welt & Insel jetzt „In Review“ statt „Entwurf“.

### 7. Stämme & Diplomatie, Entwurf

Neu: `design/gdd/staemme-und-diplomatie.md`. Wie bei Welt &
Insel habe ich die Regeln aus Prototyp 1 übernommen: Groll aus sechs Gründen, der jedes
Jahr verblasst; Kriegserklärung, Kampfverluste, Eroberung, Frieden mit 20 Jahren Frist;
Abspaltung von Andersdenkenden; Aussterben unter 3 Köpfen; Aggression als Temperament.
Krieg bleibt im MVP-0 abstrakt (nur Zahlen), ohne Gefangene. Beim Abgleich mit dem
Prototyp habe ich zwei kleine Fehler im Prototyp gefunden (ein eroberter Stamm konnte im
selben Jahr ein zweites Mal „erobert“ werden; Kriege gegen tote Stämme blieben manchmal ein
Jahr stehen); die korrigiere ich und nenne sie als Abweichungen. Das Hauptproblem aus dem
Prototyp (Krieg überdeckt den Kulturwandel, 4 zu 1) habe ich **nicht** angefasst: Das ist
deine Entscheidung nach der ersten Messung, die Hebel stehen in den Open Questions.

**Review Runde 1 (full: Systems Designer, Game Designer, QA Lead, Creative Director):
NEEDS REVISION, 11 blockierende Punkte, alle behoben, ohne ein Ergebnis der Simulation zu
ändern.** Der wichtigste Fund kam vom Game Designer: Wie im Prototyp hätte die Chronik einen
Krieg, den dein Segen ausgelöst hat, oft mit „Glaube“ erklärt, weil jeder Segen nur ein
kleiner Einzelgrund war. Jetzt zählen alle Eingriffe zusammen, und wenn dein Eingriff den
Ausschlag gab, schreibt die Chronik „weil du …“. Dazu: eine Anzeige „Kriegsnähe“ mit dem
Grund, warum (noch) kein Krieg ist, und „Wer grollt diesem Stamm“; ein gespeicherter
Nachfolger für untergegangene Stämme, damit niemand in Gottes Hand einem toten Stamm gehört
(Menschen Regel 2 und 20 angepasst); eine eindeutige Reihenfolge bei der Abspaltung (Welt
Regel 10 angepasst); 22 Akzeptanzkriterien mit festen Zahlen und Testhilfen. Status
**In Review**.

**Was auf dich wartet (Open Questions):** Krieg überwiegt wie im Prototyp; Neid ohne Ventil
(ein kleiner Nachbar darf einem großen gesegneten Stamm nicht den Krieg erklären); ob Groll
aus Eingriffen langsamer verblassen soll; leere Kriege kleiner Stämme ohne Tote; eine
Vorschau „Wer nimmt Anstoß?“ vor einem Eingriff (neue Funktion, deine Entscheidung).

**Review Runde 2 (lean, nur die Verträge, letzte Runde): NEEDS REVISION, 1 Punkt, behoben.**
Menschen kannte den neuen Auftrag „Stamm erloschen“ noch nicht, und es war offen, wann ein
Mensch nach dem Ende einer Handlung zum Nachfolger wechselt (jetzt: im nächsten
Personentakt). Status bleibt **In Review**.

## Entscheidungen ohne Freigabe

| # | GDD | Entscheidung | Warum | Zurücknehmen |
|---|---|---|---|---|
| 1 | Simulationskern | In der Pause sind nur neue Eingriffe des Spielers gesperrt; Endereignisse aus dem Abgleich der Ansicht werden eingereiht | Sonst bliebe ein Mensch mit verlorener Figur bis zum Fortsetzen ohne Meldung hängen | Regel 13 |
| 2 | Simulationskern | Beginn und Ende „in Gottes Hand“ wendet der Kern selbst an, Kräfte wendet Gotteskraft an | Der Kern besitzt den Status „in Gottes Hand“ schon | Regel 4.1 |
| 3 | Simulationskern | Landet ein Wurf im Wasser, gilt die nächste Landzelle | Ertrinken wäre eine Spielregel, die Welt & Insel und Gotteshand festlegen sollen | Regel 9 |
| 4 | Simulationskern | Beim Erzeugen einer Welt entstehen keine Ereignisse; Schritt 0 meldet „Welt erschaffen“ | Die Chronik braucht einen ersten Eintrag; Erzeugen bleibt reproduzierbar | Regel 17 |
| 5 | Simulationskern | Ein fliegender Mensch wird beim Speichern an seiner eigenen Zelle abgesetzt, nicht an der Hand | Er ist nicht mehr in der Hand | Edge Case Speichern |
| 6 | Simulationskern | Zufall: xoshiro256**, `NextInt` ohne Verzerrung nach Lemire, `NextDouble` mit 53 Bit | Gängiger, gut getesteter Standard; sonst würden zwei GDDs verschiedene `NextInt` annehmen | Formel 6 |
| 7 | Simulationskern | Pause sperrt Kräfte und Gotteshand weiterhin | Das war deine Fassung; ob das so bleibt, steht jetzt als offene Frage im GDD | Open Questions |
| 8 | Menschen | **Persönliche Hingabe klingt langsamer ab:** λ_h 0,94 → 0,98851, Halbwertszeit 5 Lebensjahre (30 s bei 1×) statt 6 s | Mit 6 s war ein Segen auf einen Einzelnen kaum zu sehen (Pillar 2). Das ist die größte inhaltliche Änderung der Nacht | Formel 8, AC-21 |
| 9 | Menschen | Namen sind über alle Stämme eindeutig; bei erschöpfter Liste ein Wurf über die ganze Liste plus die kleinste freie römische Ziffer | Wechsler hätten sonst Doppelnamen erzeugt | Formel 5, Edge Case |
| 10 | Menschen | Gefangene wandern im Gebiet des Stamms, der sie hält, und bekommen kein Fest- oder Ritualziel | Sie sind ohnehin von allem Sexuellen ausgeschlossen | Regel 19 |
| 11 | Menschen | Wer „in Gottes Hand“ ist, wechselt bei einer Abspaltung nie den Stamm; beim Packen wird „im Kampf“ gelöscht | Gleiche Logik wie bei der Opferwahl | Regel 20, 23 |
| 12 | Menschen | Ein gepackter Statist tritt mit der Zelle hervor, in der die Ansicht ihn gepackt hat | Er hatte vorher keinen Ort in der Simulation | Regel 10 |
| 13 | Zustimmung | Ritual = nur Teilnahme (mit Frömmigkeits-Tor F_min); jeder Kontakt wird als Zärtlichkeit, Sex zu zweit oder Gruppe geprüft | Deine Entscheidung „Ritual kann Kontakt enthalten, paarweise prüfen“ bleibt; so ersetzt Frömmigkeit nie Begehren | Regel 7 |
| 14 | Zustimmung | **Vorschlagswerte:** F_min 0,30, T_trauer 5 Lebensjahre, T_akkult 60 Geschichtsjahre, T_erschüttert 1 Lebensjahr | Niemand hat sie festgelegt; sie sind als Vorschlag markiert | Tuning Knobs, Open Questions |
| 15 | Zustimmung | Bei einem Stammeswechsel gilt T_akkult lang min(alte, neue Norm), statt dass die Norm langsam wandert | Kein neuer Zustand pro Person nötig (keine Lebenssimulation), leicht testbar | Regel 4 |
| 16 | Zustimmung | „in Gottes Hand“, „lebt“, „erschüttert“ sind Sperrmerkmale beim Beteiligten, nicht im Zustimmungsprofil | Das Profil bleibt bei genau 10 Feldern; Sperren können nur ablehnen | Regel 4, 8 |
| 17 | Zustimmung | Neues Handlungsregister als einziger Weg in eine Handlung; wie es in Assemblies geschnitten wird, entscheidet die Architektur | Ohne einen Besitzer der laufenden Handlungen lässt sich „kein anderer Weg“ nicht prüfen | Regel 18 |
| 18 | Zustimmung | Ladeprüfung: θ 0,30–0,90, W_D + W_L ≥ 0,40, W_N ≤ 0,40, (θ − Δ) · b_ref ≥ 0,09 | Alle Default-Zeilen bestehen; eine Mod-Tabelle kann die Kultur nicht mehr allein entscheiden lassen | Formel 2 |
| 19 | Kern, Menschen | Sperrenprüfung als eigener Eintrag nach Gotteskraft; Opferwahl zieht keine Beteiligten einer Handlung | Gegenstücke zum Zustimmungsmodell; der Creative Director hätte sie dir überlassen, ich habe sie mitgezogen, damit die GDDs sich nicht widersprechen | Kern Formel 5, Menschen Regel 13 |
| 20 | Welt & Insel | Ganzes GDD als Entwurf geschrieben, Regeln 1:1 aus Prototyp 1 | Kalibrierung bleibt gültig; jede Abweichung ist im GDD genannt | ganzes GDD |
| 21 | Welt & Insel | Welt & Insel legt beim Erzeugen die zwei Startplätze und die Stammes-IDs 0 und 1 fest, Stämme & Diplomatie übernimmt sie | Welt wird zuerst erzeugt, Menschen braucht die Gebiete vor Stämme & Diplomatie | Regel 5 |
| 22 | Welt & Insel | Hunger ist ein Auftrag „Verlust“ von Welt & Insel an Menschen; die Ernte gehört zur Welt, nicht zu den Stämmen | Menschen hatte food_t und cap_t schon Welt & Insel zugeordnet | Regel 12 |
| 23 | Welt & Insel | Keine von Hand gebauten Inseln im MVP-0 (offene Frage) | Das Konzept lässt beides zu; erzeugt reicht für die Kalibrierung | Regel 6 |
| 24 | Welt & Insel | Gebiet wird jedes Geschichtsjahr neu berechnet und zusätzlich sofort bei Entstehen, Abspaltung, Eroberung, Aussterben | So macht es Prototyp 1; mein Entwurf hatte es falsch übertragen | Regel 9 |
| 25 | Welt & Insel | Eine Platzsuche für Start und Abspaltung (120 Versuche, beide Würfe je Versuch); kein Platz, fremde Mitte oder null Zellen = keine Abspaltung | Prototyp 1; verhindert einen Stamm ohne Land | Formel 2, Regel 10 |
| 26 | Welt & Insel | Aussterben unter 3 Köpfen (Prototyp 1); Menschen Regel 2 sagt noch V = 0, **nicht angeglichen**, als offene Frage für Stämme & Diplomatie | Wer die letzten 1–2 Menschen bekommt, ist eine Spielregel | Regel 8, Open Questions |
| 27 | Welt & Insel | Randzellen sind immer Wasser (dritte Abweichung vom Prototyp, ändert keinen Wurf) | In 222 von 500 Probeinseln lag sonst Land am Rand | Regel 2, Formel 1 |
| 28 | Welt & Insel | Brandnarben bleiben für immer gespeichert (Jahr, Ursache); die Ernte-Wirkung bleibt bei 39 Ernten wie im Prototyp | Pillar 2 („Spur“) ohne die Kalibrierung zu ändern | Regel 16, Formel 7 |
| 29 | Welt & Insel | Hungersnot nennt einen Hauptgrund; in der Chronik nur, wenn die letzte desselben Stamms mehr als 6 Jahre her ist | Prototyp 1 hat den 6-Jahre-Abstand; der Grund ändert nichts an der Simulation | Formel 5, Regel 12 |
| 30 | Welt & Insel | Liste benannter Orte mit fünf Arten | Das Systemverzeichnis verspricht Orte; Rituale, Feste und Chronik brauchen Anker | Regel 17 |
| 31 | Welt & Insel | Runden = floor(x + 0,5) wie im Prototyp, nicht C#-Standard | Sonst weichen Hungertote bei x,5 ab | Formeln, AC-07 |
| 32 | Welt & Insel | Vorschläge, die die Simulation verändern, bleiben offen (zwei Runden, mit Kennzahlen) | Entscheid des Creative Director: erst du, dann AC-16 | Open Questions |
| 33 | Welt, Menschen | Aussterben unter 3 Köpfen jetzt auch in Menschen Regel 2; die letzten 1–2 Menschen verschwinden vorerst wie im Prototyp | Widerspruch zwischen zwei GDDs entfernt, Kalibrierung bleibt gültig; ihr Schicksal bleibt offen | Menschen Regel 2, Welt Regel 8 |
| 34 | Welt & Insel | Fest-Segen: Vorrat + 0,6 · Volk nach dem Geschenk, Gotteskraft erteilt zuerst den Auftrag an Menschen | Reihenfolge wie im Prototyp | Regel 14 |
| 35 | Welt & Insel | Zwei weitere Abweichungen vom Prototyp offen benannt (Abspaltung auf fremde Mitte fällt aus, Ersatz-Startplatz an Land) | Beide selten; sonst entstünde ein Stamm ohne Land | Kalibrierung |
| 36 | Stämme | GDD Stämme & Diplomatie als Entwurf geschrieben, ganze Datei auf einmal (nicht Abschnitt für Abschnitt mit Freigabe) | Du schläfst; jede Regel stammt aus Prototyp 1 | ganzes GDD |
| 37 | Stämme | Krieg im MVP-0 abstrakt: kein Merkmal „im Kampf“, keine Gefangenen | Das Systemverzeichnis legt „Sichtbarer Krieg“ in den Vertical Slice; Prototyp 1 hatte beides nicht | Regel 13 |
| 38 | Stämme | Zwei Fehler des Prototyps korrigiert (Eroberung zweimal, Kriege gegen Tote) | Sonst zählt die Kalibrierung falsche Eroberungen; die Abweichung ist genannt | Kalibrierung 3 und 4 |
| 39 | Stämme | Wer bei Eroberung oder Untergang in Gottes Hand ist, gehört beim Loslassen dem Sieger bzw. dem nächsten Stamm | Menschen Regel 20 zieht Gehaltene nie; ohne Regel gehörten sie einem toten Stamm | Regel 18, Open Questions |
| 40 | Stämme | Stammesnamen ab ID 14 mit römischer Ziffer (Ura II) | Im Prototyp gab es Doppelnamen; gleiche Lösung wie bei den Einzelnen | Regel 3 |
| 41 | Stämme | Gleichstände (Grund, reichster Nachbar, Eroberung) nach fester Reihenfolge bzw. kleinerer ID | Bestimmt den Ablauf eindeutig; im Prototyp hing das an der Reihenfolge der Schleifen | Formeln 1, 2, 4 |
| 42 | Registry | Neue Einträge `staemme_system_id` (4) und `aussterben_grenze` (3) | Werden von drei GDDs genutzt | Registry |
| 43 | Stämme | Alle Eingriffe zählen bei der Frage nach dem Kriegsgrund zusammen; gab ein Eingriff den Ausschlag, ist er die Ursache | Pillar 2; ändert nur die Erklärung, nicht die Simulation | Regel 8, Formel 2 |
| 44 | Stämme, Menschen | Untergegangene Stämme haben einen gespeicherten Nachfolger (Sieger oder nächste Mitte); Gehaltene und Beteiligte laufender Handlungen gehen an ihn über, die übrigen Einzelnen sterben mit Tod-Ereignis | Sonst gehörten Menschen einem toten Stamm oder verschwänden ohne Ereignis | Stämme Regeln 17–18, Menschen Regeln 2 und 20 |
| 45 | Stämme | Kein eigener Schritt für Stämme unter 3 Köpfen vor dem Kampf (Vorschlag des Systems Designer abgelehnt, Entscheid des Creative Director) | Würde die Kalibrierung ändern; als offene Frage mit Kennzahlen | Open Questions |
| 46 | Stämme, Welt | Abspaltung fällt aus, wenn der Platz auf der Mitte eines lebenden Stamms liegt, geprüft vor dem Würfeln des Teils; Gebiet wird bei jeder Änderung sofort neu berechnet | Sonst war der Vertrag nicht ausführbar; Ergebnis wie im Prototyp | Stämme Regeln 15, 19, Welt Regel 10 |
| 47 | Stämme | Anzeige „Kriegsnähe“ mit Hindernis und „Wer grollt diesem Stamm“; keine neue Chronik-Zeile bei Stufenwechseln | Spannung vorher sichtbar, ohne die Chronik weiter mit Krieg zu füllen | UI Requirements |
| 48 | Stämme | Sichere Bereiche eingeengt (Kriegsschwelle ab 0,5, Verblassen bis 0,975), Querbedingungen genannt | Mit 0,4 gäbe es an jeder engen Grenze Dauerkrieg | Tuning Knobs |
