<!-- PROTOTYPE - NOT FOR PRODUCTION -->
<!-- Question: Macht die Gotteshand (packen, hochheben, werfen) bei 50 Menschen nach 30 s Spass, und haelt die Bildzeit? -->
<!-- Date: 2026-10-25 -->

# Concept Prototype Report: Gotteshand und Ragdoll (Allmacht, Prototyp 2)

> **Date**: 2026-10-25
> **Prototype Path**: Engine (Unity 6000.3.27f1)
> **Concept File**: design/gdd/game-concept.md

---

## Hypothesis

Wenn der Spieler in einer Szene mit 50 Menschen jemanden mit der Gotteshand packt,
hochhebt und wirft, macht das schon in den ersten 30 Sekunden Spaß. Belegt, wenn:

1. der erste Wurf innerhalb von 30 Sekunden gelingt, mit nur einer Zeile
   Steuerungshinweis;
2. der Spieler freiwillig mindestens 5 Minuten weiterspielt;
3. mindestens 3 von 5 Würfen höchstens 3 m vom Zielring entfernt landen;
4. die Bildzeit im Editor mit 50 Menschen und 16 aktiven Ragdolls im Mittel
   höchstens 16,6 ms und im 99. Perzentil höchstens 33 ms beträgt.

---

## Riskiest Assumption Tested

Ob sich ein 3D-Wurf mit der Maus aus der Gotteskamera gezielt und befriedigend
steuern lässt (bei Black & White galt die Hand als fummelig), und an zweiter Stelle,
ob das Budget für die Bildzeit mit vielen Ragdolls hält (Abbruchkriterium des
Technical Directors für Prototyp 2).

**Ergebnis: belegt.** Der Spieler fand die Steuerung „sehr intuitiv“ und hat nach
eigener Aussage jeden Wurf in den Zielring gebracht. Die Bildzeit lag weit unter dem
Budget, auch mit dreimal so vielen Ragdolls wie verlangt.

---

## Approach

Ein Unity-Paket im Allmacht-Projekt, kein eigenes Projekt. Eingebunden über eine
Zeile in `Packages/manifest.json`, gestartet über das Menü **Allmacht → Prototyp 2 -
Gotteshand starten**, das eine leere Szene anlegt und sie komplett im Code aufbaut.
Gebaut in einer Sitzung am 2026-10-25. Er lief beim ersten Start ohne
Kompilierfehler.

**Path chosen:** Engine
**Reason for path:** Das Gefühl der Hand und die Bildzeit mit echter Unity-Physik
sind die Hypothese. Ein Browser hätte beides verfälscht.

**Shortcuts taken (intentional):**
- Menschen aus Grundkörpern (Würfel, Kugeln, Kapseln), Ragdoll aus 10 Teilen mit
  CharacterJoints, kein Grundkörper aus MakeHuman/MPFB, keine Morphs, keine Haut
- Gehen ohne Animationen: die Glieder schwingen per Code, Aufstehen als 0,7 s langer
  Übergang
- Keine Simulation, keine Kräfte, keine Kultur, keine Verletzungen, keine sexuellen
  Inhalte, kein Sound, keine Menüs
- Einzige Reaktion der Welt: Umstehende im Umkreis von 8 m fliehen 2,5 s nach einem
  Aufprall
- Feste Werte, fester Zufallsstartwert, Messanzeige mit IMGUI

---

## Result

Ein Spieler (der Entwickler), eine Sitzung am PC.

- **Einstieg:** „Also das Werfen macht Spaß. Ist auch sehr intuitiv alles.“ Erster
  Wurf nach etwa 20 Sekunden (Schätzung des Spielers, siehe Metrics).
- **Bester Moment:** „Als ich einen extrem weit weggeschmissen hab, fand ich gut.“
- **Schlechtester Moment:** keiner. Keine kaputten Ragdolls, falsch knickenden
  Gelenke, Fehlgriffe oder Probleme mit der Kamera gemeldet.
- **Überraschungen:** keine.
- **Grenze:** „Das Spielprinzip ist weiterhin sehr begrenzt. Nur Leute werfen macht
  nach ein paar Minuten keinen Spaß mehr.“ Langweilig wurde es nach 5–10 Minuten.
  Die übrige Zeit (insgesamt etwa 20 Minuten) war Ausprobieren und Testen. Der Spieler
  ordnet das selbst als erwartbar ein: „Nachvollziehbar, sind ja noch am Anfang.“
- **Zielring:** Nach Aussage des Spielers landeten alle Würfe darin.
- **Stresstest** (Console-Ausgabe, 5-s-Fenster):
  `Ø 3.8 ms, 99% 5.0 ms, max 6.0 ms bei bis zu 49 Ragdolls und 50 Menschen`

Kriterien:

| Kriterium | Ziel | Ergebnis |
|---|---|---|
| 1. Erster Wurf | ≤ 30 s | ca. 20 s (geschätzt) ✓ |
| 2. Freiwillig weitergespielt | ≥ 5 min | 5–10 min mit Spaß, ca. 20 min insgesamt (geschätzt) ✓, knapp |
| 3. Zielgenauigkeit | 3 von 5 im Ring | alle Würfe getroffen (Zahl der Würfe nicht genannt) ✓ |
| 4. Bildzeit | Ø ≤ 16,6 ms, 99 % ≤ 33 ms bei 16 Ragdolls | 3,8 / 5,0 ms, max 6,0 ms bei bis zu 49 Ragdolls ✓ |

---

## Metrics

| Metric | Value |
|--------|-------|
| Path used | Engine (Unity 6000.3.27f1, URP, PhysX) |
| Iterations to playable | 1 (lief beim ersten Start) |
| Prototype duration | 1 Sitzung (Bau und Playtest am 2026-10-25) |
| Playtesters | 1 intern (der Entwickler), 0 extern |
| Feel assessment | Greifen und Werfen „sehr intuitiv“, weite Würfe sind der Höhepunkt, das Zielen gelingt zuverlässig. Trägt allein 5–10 Minuten |
| Hypothesis verdict | CONFIRMED |

---

## Recommendation: PROCEED

Alle vier Kriterien sind erfüllt. Die Hand ist sofort verständlich, der erste Wurf
gelingt nach etwa 20 Sekunden, das Zielen funktioniert, und weite Würfe machen
sichtbar Spaß. Die Bildzeit lässt großen Spielraum: 49 gleichzeitige Ragdolls
brauchten im Editor ein Viertel des Budgets. Die Grenze, die der Playtest zeigt,
widerspricht dem Konzept nicht, sie bestätigt seinen Aufbau: Die Hand trägt den
30-Sekunden-Loop, aber nicht die längeren Loops. Die müssen aus den Folgen in der
Simulation kommen. Prototyp 1 hat das Gegenstück gezeigt (Simulation mit Folgen,
aber „zu flach, nichts Sichtbares“). Beides zusammen ist die Kernhypothese des MVP.
Urteil des Spielers: PROCEED.

**Creative-Director-Prüfung (CD-PLAYTEST):** übersprungen im Lean-Modus.

---

## If Proceeding

- **Core tuning values discovered:**
  - Hubhöhe 3,5 m über dem Boden, Greifradius 1,5 m um das Becken, Wurf aus der
    Handgeschwindigkeit der letzten 0,1 s plus 30 % davon nach oben (höchstens
    6 m/s), gedeckelt bei 30 m/s, Wurf ab 4 m/s (darunter nur Ablegen). Damit fühlten
    sich weite Würfe gut an und das Zielen gelang.
  - Gehaltene Figur: Die Brust folgt der Hand (Geschwindigkeit = Abstand × 10,
    höchstens 25 m/s), der Rest baumelt. Es gab keine Beschwerden.
  - Zielring mit 3 m Radius war vermutlich zu leicht (alle Würfe getroffen). Für ein
    echtes Ziel-Feature kleiner prüfen.
- **Assumptions confirmed:**
  - Die Gotteshand trägt als Kernverb (Konzept: Black & White als Beleg). Der
    30-Sekunden-Loop funktioniert.
  - Ein Wurf mit der Maus aus der Gotteskamera ist gezielt steuerbar.
  - Ragdolls nur für direkt betroffene Figuren, alle anderen kinematisch: Das reicht
    für 50 Menschen locker.
- **Assumptions disproved:**
  - Das Startbudget „höchstens 8–16 aktive Ragdolls“ am PC (Technical
    Considerations) ist sehr vorsichtig. 49 liefen mit 5,0 ms im 99. Perzentil. Gilt
    nur für Grundkörper und den PC des Entwicklers (Hardware nicht erfasst). Mit
    Skinned Meshes, Blend Shapes und Haut neu messen.
- **Emergent mechanics:**
  - Weite Würfe als eigener Reiz: Entfernung und Flugbahn sind der Höhepunkt, nicht
    der Aufprall.
- **Requirement for the GDDs:** Ein Wurf muss Folgen in der Simulation haben
  (Erinnerung der Kultur, Kulturwandel, Krieg, Reaktion der Stämme). Ohne diese
  Folgen wird das Werfen nach 5–10 Minuten langweilig. Das gehört in das GDD der
  Gotteshand und in das zum Kulturwandel mit Erinnerung.

**Next steps:**
1. `/design-review design/gdd/game-concept.md` (die überarbeitete Fassung aus der
   dritten Prüfung ist noch nicht geprüft, Ergebnis Prototyp 2 eintragen)
2. `/gate-check`
3. `/art-bible`
4. `/map-systems`
5. `/design-system [mechanic]` (die Werte oben in Tuning Knobs und Formulas)

---

## Lessons Learned

- **What assumptions were broken by actually building this?**
  Das Ragdoll-Budget ist mit einfachen Körpern kein Engpass. Die Grenze liegt beim
  Inhalt: Ohne Folgen in der Welt trägt das Verb nicht lange.

- **What surprised us that didn't show up in the brainstorm?**
  Nichts, laut Spieler. Weite Würfe als stärkster Moment waren nicht ausdrücklich
  vorhergesagt.

- **What would we test differently next time?**
  Die Messanzeige war für den Spieler nicht auffindbar. Spielzeit und erster Wurf
  sind deshalb nur geschätzt, und die Zahl der Würfe fehlt. Beim nächsten Mal die
  Messwerte zusätzlich in die Console schreiben (wie beim Stresstest), einen
  Screenshot als Nachweis machen und die Hardware notieren. Mindestens einen
  externen Spieler einsetzen, der das Spiel noch nicht kennt.

**Gaps in this evidence:**
- Erster Wurf, Spielzeit und Zahl der Würfe sind Schätzungen des Spielers, keine
  Messwerte.
- 1 Spieler, und es ist der Entwickler. Keine Daten zum ersten Eindruck von
  Fremden.
- Hardware des Testrechners nicht erfasst. Gemessen im Editor, nicht in einem Build.
- Kein Screenshot im Repository (Taste K speichert nach
  `prototypes/gotteshand-concept/evidence/`).

---

> *Prototype code location: `prototypes/gotteshand-concept/`*
> *This code is throwaway. Never refactor into production.*
