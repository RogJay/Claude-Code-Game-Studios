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
