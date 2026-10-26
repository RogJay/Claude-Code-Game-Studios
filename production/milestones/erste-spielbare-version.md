# Milestone: Erste spielbare Version

*Angelegt: 2026-10-26 · Entscheidung des Entwicklers: zwei Etappen, mit Grundkörper ·
Quelle der Zahlen: Schätzung vom 2026-10-26 (unten)*

## Overview

- **Target Date**: offen. Wird nach den ersten 2–3 Sprints aus dem echten Tempo gesetzt
- **Type**: interner spielbarer Zwischenstand vor MVP-0 (kein Vertical Slice)
- **Duration**: erwartet ca. 560 h (380–880 h); bei 10–20 h/Woche ca. 28–56 Wochen,
  bei 15 h/Woche ca. 8,5 Monate
- **Number of Sprints**: offen (`/sprint-plan new` nach der Architektur)

## Milestone Goal

Zum ersten Mal läuft Allmacht als echtes Spiel und nicht als Wegwerf-Prototyp: Die
Simulation aus den sieben fertigen GDDs läuft in Unity, der Spieler sieht Stämme wachsen,
streiten und ihre Kultur wechseln, und er greift mit der Gotteshand, dem Blitz und dem
Fest-Segen ein. Am Ende lässt sich prüfen, ob die Spannung aus Pillar 1 (die Welt lebt ohne
dich) und Pillar 2 (jeder Eingriff hinterlässt eine Spur) im echten Spiel trägt, und alles
Gebaute ist Grundlage für MVP-0, nicht Wegwerfcode.

Der Meilenstein hat **zwei Etappen**. Etappe 1 ist für sich spielbar (zusehen und die Zeit
steuern), Etappe 2 macht daraus ein Spiel mit Eingriffen.

| Etappe | Inhalt | Optimistisch | **Erwartet** | Pessimistisch | bei 10–20 h/Woche (erwartet) |
|---|---|---|---|---|---|
| **1 „Die Welt lebt“** | Architektur, Projektgerüst, Simulationskern, Welt & Insel, Menschen & Bevölkerung, Stämme & Diplomatie, Kultur & Erinnerung, einfache Ansicht (Raster, Kapseln), Zeitsteuerung, Ereignisliste | 26,5 Tage (≈ 210 h) | **37,5 Tage (≈ 300 h)** | 57,5 Tage (≈ 460 h) | 15–30 Wochen |
| **2 „Eingreifen“** | Zustimmungsmodell, Begehren & Lust ohne Szenen (Feste, Bindungen), Blitz und Fest-Segen, Gotteshand aus Prototyp 2, Grundkörper mit einfachen Animationen und Ragdoll | 21 Tage (≈ 170 h) | **32,5 Tage (≈ 260 h)** | 53 Tage (≈ 420 h) | 13–26 Wochen |
| **Summe** | | 47,5 Tage (≈ 380 h) | **70 Tage (≈ 560 h)** | 110,5 Tage (≈ 880 h) | 28–56 Wochen |

Tage zu 8 h. **Nicht im Meilenstein:** Speichern & Laden, Harem & Rituale, sichtbare Szenen,
Gnade und Kraft-Ökonomie (Kosten der Kräfte vorerst fest oder frei), die echte Chronik (nur
eine Ereignisliste), Morphs, Debug-Werkzeuge über das Nötigste hinaus.

## Success Criteria

Der Meilenstein ist erreicht, wenn alle Punkte erfüllt sind.

**Etappe 1**
- [ ] Eine neue Welt (2 Stämme, 96 Köpfe, 40 Einzelne) läuft 30 Minuten bei 1×, 4× und 16×
      ohne Absturz und ohne Fehler im Log
- [ ] Ohne Eingriff zeigt die Ereignisliste in 10 Minuten bei 1× mindestens einen Krieg oder
      eine Abspaltung und einen Wechsel der Gesellschaftsform (gemessen über 10 Startwerte,
      mindestens 8 erfüllen es)
- [ ] Derselbe Startwert ergibt auf demselben Rechner nach 3.000 Schritten denselben
      Zustands-Hash (Simulationskern, Determinismus)
- [ ] Alle Logik-Kriterien von Simulationskern, Welt & Insel, Menschen & Bevölkerung, Stämme &
      Diplomatie und Kultur & Erinnerung laufen grün in EditMode; Kriterien, die Speichern
      und Laden brauchen, sind ausdrücklich als vertagt markiert

**Etappe 2**
- [ ] Der Spieler packt, hebt und wirft einen Menschen mit Grundkörper; der Ragdoll läuft
      am echten Skelett, und das Ergebnis (unverletzt, verletzt, tot) wirkt in der Simulation
- [ ] Blitz und Fest-Segen wirken sichtbar und in der Simulation; ihre Folgen stehen mit
      „weil du …“ in der Ereignisliste (Pillar 2)
- [ ] Ein Fest lässt Menschen zum Festplatz strömen, und Bindungen entstehen; keine
      Handlung beginnt ohne Zustimmung (Zustimmungsmodell, `Beginne` als einziger Weg)
- [ ] Alle Logik-Kriterien von Zustimmungsmodell und Begehren & Lust laufen grün in EditMode
      (außer Speichern und Laden)

**Für beide**
- [ ] Bildzeit im Editor auf dem Entwicklungsrechner im Mittel ≤ 16,6 ms und im 99.
      Perzentil ≤ 33 ms bei 50 Menschen und 16 aktiven Ragdolls
- [ ] Jeder sichtbare Teil ist gestartet und angesehen; die Bildbelege liegen in
      `production/qa/evidence/` (Coding Standards: Run and observe)
- [ ] Keine offenen Fehler der Schwere S1 oder S2

## Feature List

### Must Ship (Milestone Fails Without These)

| Feature | Design Doc | Owner | Sprint Target | Status |
|---------|-----------|-------|--------------|--------|
| Architektur und kritische ADRs für Etappe 1 | `/create-architecture` | Entwickler mit technical-director | Etappe 1 | Not Started |
| Projektgerüst, Testumgebung, CI | `/test-setup` | Entwickler | Etappe 1 | Not Started |
| Simulationskern | `design/gdd/simulationskern.md` | Entwickler | Etappe 1 | Not Started |
| Welt & Insel | `design/gdd/welt-und-insel.md` | Entwickler | Etappe 1 | Not Started |
| Menschen & Bevölkerung | `design/gdd/menschen-und-bevoelkerung.md` | Entwickler | Etappe 1 | Not Started |
| Stämme & Diplomatie | `design/gdd/staemme-und-diplomatie.md` | Entwickler | Etappe 1 | Not Started |
| Kultur & Erinnerung | `design/gdd/kultur-und-erinnerung.md` | Entwickler | Etappe 1 | Not Started |
| Einfache Ansicht, Kamera, Zeitsteuerung, Ereignisliste | Kurzspezifikation (`/quick-design`) | Entwickler | Etappe 1 | Not Started |
| Zustimmungsmodell | `design/gdd/zustimmungsmodell.md` | Entwickler | Etappe 2 | Not Started |
| Begehren & Lust ohne Szenen | `design/gdd/begehren-und-lust.md` (vorher Review-Runde 2) | Entwickler | Etappe 2 | Not Started |
| Blitz und Fest-Segen | Kurzspezifikation (`/quick-design`) | Entwickler | Etappe 2 | Not Started |
| Gotteshand und Ragdoll aus Prototyp 2 | Kurzspezifikation, `prototypes/gotteshand-concept/REPORT.md` | Entwickler | Etappe 2 | Not Started |
| Grundkörper mit einfachen Animationen | Kurzspezifikation, Wahl des Körpers | Entwickler | Etappe 2 | Not Started |

### Should Ship (Planned but Cuttable)

| Feature | Design Doc | Owner | Sprint Target | Cut Impact | Status |
|---------|-----------|-------|--------------|-----------|--------|
| CI auf GitHub (game-ci) | `/test-setup` | Entwickler | Etappe 1 | Tests laufen nur lokal; braucht eine Unity-Lizenz als Secret | Not Started |
| Infofenster für Stamm und Einzelnen | Kurzspezifikation | Entwickler | Etappe 2 | Man sieht Werte nur in der Ereignisliste | Not Started |

### Stretch Goals (Only if Ahead of Schedule)

| Feature | Design Doc | Owner | Value Add |
|---------|-----------|-------|----------|
| Speichern & Laden | GDD fehlt | Entwickler | Lange Testläufe fortsetzen; schließt die vertagten Kriterien |
| Erste einfache Szenen auf dem Grundkörper | `design/gdd/begehren-und-lust.md` | Entwickler | Pillar 3 sichtbar |

## Quality Gates

| Gate | Threshold | Measurement Method |
|------|-----------|-------------------|
| Abstürze | 0 in 30 min Dauerlauf bei 16× | Dauerlauf im Editor, Log |
| Bildzeit | Ø ≤ 16,6 ms, 99 % ≤ 33 ms bei 50 Menschen und 16 Ragdolls | Messanzeige wie in Prototyp 2 |
| Determinismus | gleicher Hash nach 3.000 Schritten | Test im Simulationskern |
| Kritische Fehler | 0 offene S1 | Fehlerliste |
| Schwere Fehler | 0 offene S2 | Fehlerliste |
| Tests | alle Logik-Kriterien der enthaltenen GDDs grün (außer Speichern und Laden) | Unity Test Runner, EditMode |

## Schätzung (2026-10-26)

Nach dem Verfahren von `/estimate`. Grundlage: die sieben GDDs (zusammen 184
Akzeptanzkriterien), der Code von Prototyp 2 (etwa 700 Zeilen in vier Dateien) und die
MVP-0-Schätzung vom 2026-10-25.

| # | Teil | Etappe | Optimistisch | **Erwartet** | Pessimistisch |
|---|---|---|---|---|---|
| 0a | Architektur und ADRs für die Simulation | 1 | 2 | **2,5** | 4 |
| 0b | Kurzspezifikationen (Gotteshand, Kräfte, Ansicht), Review-Runde 2 Begehren & Lust, Wahl des Grundkörpers | 2 | 1 | **1,5** | 2 |
| 1 | Projektgerüst: Assemblies, Testumgebung, CI, Datendateien | 1 | 1,5 | **2** | 4 |
| 2 | Simulationskern (36 Kriterien) | 1 | 4 | **6** | 9 |
| 3 | Welt & Insel (18) | 1 | 3,5 | **5** | 7 |
| 4 | Menschen & Bevölkerung (33) | 1 | 4 | **6** | 9 |
| 5 | Stämme & Diplomatie (22) | 1 | 3,5 | **5** | 7 |
| 6 | Kultur & Erinnerung (22) | 1 | 3 | **4,5** | 6,5 |
| 7 | Zustimmungsmodell (32) | 2 | 3 | **4,5** | 6,5 |
| 8 | Begehren & Lust ohne Szenen (21) | 2 | 3,5 | **5** | 7,5 |
| 9 | Blitz und Fest-Segen | 2 | 2 | **3** | 5 |
| 10a | Ansicht und Kamera: Raster, Kapseln, Schwenk und Zoom | 1 | 2,5 | **3** | 5 |
| 10b | Ansicht: Festbereich, Wirkung des Blitzes | 2 | 0,5 | **1** | 2 |
| 11 | Grundkörper: Lizenz, Rig, Lauf- und Stehanimation, Ragdoll am Skelett | 2 | 5 | **8** | 14 |
| 12 | Gotteshand übernehmen und an die Simulation anschließen | 2 | 2,5 | **4** | 7 |
| 13a | Oberfläche: Zeitsteuerung, Ereignisliste | 1 | 1 | **1,5** | 2,5 |
| 13b | Oberfläche: Kraft-Knöpfe, Infofenster | 2 | 1 | **1,5** | 2,5 |
| 14a | Zusammenbau Etappe 1, Dauerlauf, Bildbelege | 1 | 1,5 | **2** | 3,5 |
| 14b | Zusammenbau Etappe 2, Balancing, Playtest, Bildbelege | 2 | 2,5 | **4** | 6,5 |
| | **Summe** | | **47,5** | **70** | **110,5** |

**Verlässlichkeit: niedrig.** Es gibt keine Sprint-Historie, also kein gemessenes Tempo; die
Zahlen sind nicht an das echte Tempo kalibriert. Offen ist auch, wie viel Code Claude-Agenten
schreiben und wie viel der Entwickler selbst im Unity-Editor macht; das kann das Tempo in
beide Richtungen stark verschieben. Nach den ersten 2–3 Sprints wird die Schätzung durch
das gemessene Tempo ersetzt.

**Annahmen:** Kein Speichern und Laden; Kosten der Kräfte fest oder frei; der Grundkörper
ist ein fertiger, lizenzierter Körper mit lizenzierten Animationen (kein eigener Körper,
keine Morphs); die vier Systeme ohne GDD bekommen nur Kurzspezifikationen; die Arbeit an den
übrigen MVP-0-GDDs läuft nicht parallel (sonst verschiebt sich der Meilenstein).

## Risk Register

| Risk | Probability | Impact | Mitigation | Owner | Status |
|------|------------|--------|-----------|-------|--------|
| Grundkörper: Lizenz, Rigging, Animationen und Ragdoll am echten Skelett sind neu; breiteste Spanne (5–14 Tage) | hoch | hoch | Früher Unity-Test des Körpers schon in Etappe 1 (`/prototype`, zeitlich begrenzt); Rückfall auf die Kapseln aus Prototyp 2 | Entwickler | offen |
| Kein gemessenes Tempo, Schätzung niedrig verlässlich | sicher | mittel | Nach 2–3 Sprints neu schätzen; Zieldatum erst dann setzen | Entwickler | offen |
| CI für Unity braucht eine Unity-Lizenz als Secret | mittel | niedrig | Tests vorerst lokal; CI als „Should Ship“ | Entwickler | offen |
| 184 Kriterien bedeuten viel Testcode | hoch | mittel | Testhilfen früh bauen (`/test-helpers`); Property-Tests mit festem Startwert | Entwickler | offen |
| Gotteshand, Kräfte und Ansicht haben nur Kurzspezifikationen; spätere GDDs ändern Verträge | mittel | mittel | Kurzspezifikationen halten sich an die Verträge der fertigen GDDs; spätere GDDs prüfen gegen diesen Code | Entwickler | offen |
| Begehren & Lust ist erst nach Runde 1 im Review | mittel | niedrig | Runde 2 vor Etappe 2 | Entwickler | offen |

## Dependencies

### Internal Dependencies

| Feature | Depends On | Owner of Dependency | Status |
|---------|-----------|-------------------|--------|
| Jeder Code | Architektur und ADRs (`/create-architecture`) | Entwickler | Not Started |
| Welt, Menschen, Stämme, Kultur | Simulationskern | Entwickler | Not Started |
| Begehren & Lust | Zustimmungsmodell, Menschen, Kultur, Welt; Review-Runde 2 | Entwickler | In Review |
| Gotteshand | Simulationskern (Status „in Gottes Hand“), Menschen, Grundkörper | Entwickler | Not Started |
| Blitz und Fest-Segen | Kultur, Welt, Menschen, Begehren & Lust (Fest) | Entwickler | Not Started |

### External Dependencies

| Dependency | Provider | Status | Risk if Delayed |
|-----------|---------|--------|----------------|
| Lizenzierter Grundkörper und Animationen | Asset-Anbieter (noch zu wählen) | offen | Etappe 2 verschiebt sich; Rückfall auf Kapseln |
| Unity 6.3 LTS (6000.3.27f1) | Unity | installiert | – |
| Unity-Lizenz für CI | Unity | offen | nur lokale Tests |

## Review Schedule

| Date | Review Type | Attendees |
|------|-----------|-----------|
| nach Sprint 2 | Tempo messen, Schätzung ersetzen, Zieldatum setzen | Entwickler, producer |
| Ende Etappe 1 | `/milestone-review` der Etappe 1, Playtest „Die Welt lebt“ | Entwickler |
| Mitte Etappe 2 | Fortschritt, Stand des Grundkörpers | Entwickler |
| Ende Etappe 2 | `/milestone-review erste-spielbare-version` | Entwickler |
