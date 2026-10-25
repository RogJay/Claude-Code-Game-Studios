<!-- PROTOTYPE - NOT FOR PRODUCTION -->
<!-- Question: Macht die Gotteshand (packen, hochheben, werfen) bei 50 Menschen nach 30 s Spass, und haelt die Bildzeit? -->
<!-- Date: 2026-10-25 -->

# Prototyp 2: Gotteshand und Ragdoll (Allmacht)

**Status:** abgeschlossen am 2026-10-25, Urteil **PROCEED** (siehe `REPORT.md`).

Das ist **kein zweites Spiel**. Es ist ein Wegwerf-Test für ein Stück von Allmacht:
die Gotteshand mit Ragdoll-Physik. Er läuft im Allmacht-Unity-Projekt. Nach dem
Test wird der Code nicht weiterverwendet. Die Erkenntnisse gehen ins Konzept und in
die GDDs, und das echte Spiel wird sauber neu geschrieben.

## Hypothese

Wenn du in einer Szene mit 50 Menschen jemanden mit der Gotteshand packst,
hochhebst und wirfst, macht das schon in den ersten 30 Sekunden Spaß.

Sie gilt als bestätigt, wenn:

1. der erste Wurf innerhalb von 30 Sekunden gelingt, mit nur einer Zeile
   Steuerungshinweis (Anzeige „erster Wurf“),
2. du freiwillig mindestens 5 Minuten weiterspielst (Anzeige „Spielzeit“),
3. mindestens 3 von 5 Würfen höchstens 3 m vom Zielring entfernt landen
   (Anzeige „Zieltreffer“),
4. die Bildzeit im Editor mit 50 Menschen und 16 aktiven Ragdolls im Mittel
   höchstens 16,6 ms und im 99. Perzentil höchstens 33 ms beträgt (Taste F,
   Anzeige „Letzter Stresstest“).

Wenn 1 oder 2 scheitern, hat das Gefühl der Hand ein Problem. Scheitert 4, ist das
Budget das Problem. Das riskanteste Element ist, ob sich ein 3D-Wurf mit der Maus
aus der Gotteskamera gezielt und befriedigend steuern lässt.

## Starten

1. Öffne das Allmacht-Projekt in Unity 6000.3.27f1. Unity lädt das Paket
   `com.allmacht.prototype.gotteshand` über eine Zeile in `Packages/manifest.json`.
2. Wähle im Menü **Allmacht → Prototyp 2 - Gotteshand starten**. Das legt eine leere
   Szene mit dem Test an und startet den Play Mode. Ungespeicherte Änderungen an der
   offenen Szene fragt Unity vorher ab.
3. Steuerung:
   - Linke Maustaste: Mensch packen, ziehen, loslassen. Mit Schwung loslassen heißt werfen.
   - WASD oder Pfeiltasten: Kamera bewegen
   - Mausrad: Zoom
   - Q/E: Kamera drehen
   - F: Stresstest mit 16 Ragdolls
   - N: 25 Menschen mehr
   - K: Screenshot nach `prototypes/gotteshand-concept/evidence/`
   - R: Messung zurücksetzen
   - H: Anzeige ein- und ausblenden

## Umfang

Im Test enthalten:

- eine flache Insel
- 50 Menschen aus Grundkörpern in 2 Stämmen, jeweils ein Ragdoll aus 10 Teilen
- die Gotteshand
- eine Fluchtreaktion von Umstehenden nach einem Aufprall
- ein Zielring
- eine Messanzeige und ein Stresstest

Bewusst weggelassen:

- die Simulation
- die Kräfte
- die Kultur
- sexuelle Inhalte
- Morph-Körper
- Animationen
- Verletzungen
- Sound und Menüs

## Aufräumen

Wenn der Test fertig ist, kommt die Zeile `com.allmacht.prototype.gotteshand` aus
`Packages/manifest.json` wieder raus. Dieser Ordner bleibt als Nachweis liegen.

## Ergebnisse

Playtest mit 1 Spieler am 2026-10-25, alle 4 Kriterien erfüllt (Details in `REPORT.md`):

- Greifen und Werfen „sehr intuitiv“. Erster Wurf nach ca. 20 s, alle Würfe im
  Zielring (beides nach Angabe des Spielers).
- Stresstest: Ø 3,8 ms, 99 % 5,0 ms, max 6,0 ms bei bis zu 49 Ragdolls und 50 Menschen.
- Bester Moment: jemanden extrem weit wegschleudern.
- Grenze: Werfen allein trägt 5–10 Minuten. Ein Wurf muss im Spiel Folgen in der
  Simulation haben.
