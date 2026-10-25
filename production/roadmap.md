# Interne Roadmap: Allmacht

*Stand: 2026-10-25 · Quelle: `design/gdd/game-concept.md` (Stufentabelle, MVP
Definition), Prototyp-Berichte in `prototypes/`*

Das ist eine grobe Orientierung, **keine Schätzung**. Die Dauern stammen aus der
Stufentabelle des Konzepts (Vollzeit-Monate, solo). Die Schätzung für MVP-0 galt
für eine Kraft und wird mit `/estimate` neu gemacht. Danach wird diese Datei
aktualisiert.

**Arbeitszeit:** 10–20 Stunden pro Woche (Angabe des Entwicklers, 2026-10-25).
Gegenüber Vollzeit (ca. 40 h) ist das der **Faktor 2 bis 4**. Die Teilzeit-Spalte
rechnet die Vollzeit-Werte einfach hoch. Echte Werte kommen aus den ersten Sprints.

## Phasen

| Phase | Inhalt | Dauer (Vollzeit) | Ende ca. Vollzeit | Ende ca. bei 10–20 h/Woche |
|---|---|---|---|---|
| **0. Konzept** ✓ | Prototyp 1 ✓ und 2 ✓, letztes `/design-review`, `/gate-check` | Tage | – | – |
| **1. Systemplanung und Pre-Production** (jetzt) | `/map-systems` ✓, `/estimate` für MVP-0, GDDs pro System (Zustimmungs- und Lust-System früh), `/art-bible`, `/create-architecture` und die kritischen ADRs, MVP-0 neu schätzen | einige Wochen (ungeschätzt) | – | – |
| **2. MVP-0 „Gott-Spielzeug“** | Simulation mit abstraktem Krieg, Kulturwandel auf einer Achse (Lust-Sekte ↔ Gottesstaat) mit Erinnerung, Zustimmungsmodell als reine Funktion mit Tests, Begehren und Lust mit einfachen Szenen, Harem und Rituale, Grundkörper ohne Morphs, Gotteshand und Ragdoll, 4 Kräfte und passive Gnade, eine Zoomstufe, Speichern, Zeitsteuerung, Chronik | 3–4 Monate (neu schätzen) | Monat 4 | Monat 8–16 |
| **3. MVP-1** | Weitere Kräfte, zweite Zoomstufe, Morphs und Look B, volles System für Kulturwandel und Erinnerung | +2–3 Monate | Monat 6–7 | Monat 12–28 |
| **4. Vertical Slice (v1)** | In dieser Reihenfolge: sichtbarer Krieg → Haut in Detailstufen → Besessen-Sein. 1–3 Stämme, 30–80 Menschen, 2–3 Zoomstufen | +6–10 Monate | Monat 12–17 | Monat 24–68 |
| **5. Alpha (Ausbau)** | Weitere Gesellschaftsformen, Zeitalter mit Inhalten, mehr Kräfte, Paar-Animationen (mit eigener Entscheidung, ob man sie baut), mehr Animationen | +6–12 Monate | Monat 18–29 | Monat 36–116 |
| **Volle Vision** | Viele Zivilisationen, Fantasy- und Tiervölker, nahtloser Zoom, detaillierte Szenen, tiefe Verletzungssimulation | – | Monat 30–48 | Monat 60–192 |
| Android (optional) | Draufsicht, Touch-Gotteshand, einfacher Look. Einstieg erst nach dem Haut-Test auf dem Zielgerät (Prototyp 3) | eigene Stufe | – | – |

**Wahrscheinlichster Abbruchpunkt** (Konzept): in v1. Wird v1 in der Reihenfolge
oben gebaut, bleibt nach dem sichtbaren Krieg schon ein gutes Gottspiel, denn Harem
und Rituale sind seit MVP-0 drin.

## Lust und Sex: wann was kommt

Regel aus Pillar 3: **Das System ist geschützt, die Darstellung darf einfach
sein.** Lust ist ab MVP-0 Teil der Systeme, die Darstellung wächst mit den Stufen.

| Stufe | Was dazukommt |
|---|---|
| MVP-0 | Kulturrichtung Lust-Sekte, Feste (Fest-Segen), Zustimmungsmodell als geprüfter Code, **einfache sichtbare Szenen** auf dem Grundkörper, **Harem und Rituale** spielbar (Rituale bringen Gotteskraft und neue Erwachsene). Vorgezogen am 2026-10-25 |
| MVP-1 | Morphs und Look B für die Körper |
| v1 | Haut in Detailstufen |
| Alpha | Paar-Animationen (das teuerste Animationsproblem), weitere Gesellschaftsformen wie die Monarchie mit Harem |
| Volle Vision | Detaillierte Szenen |

Am 2026-10-25 hat der Entwickler Harem und Rituale sowie einfache Szenen ins MVP-0
vorgezogen. MVP-0 wird dadurch größer; die Dauer oben stammt noch aus der Schätzung
für eine Kraft und wird mit `/estimate` neu bestimmt. Die nicht verhandelbaren Grenzen aus den
Anti-Pillars gelten in jeder Stufe.

## Regeln für die Systemplanung

Festgelegt vom Entwickler am 2026-10-25 beim Gate-Check (Bericht in
`production/gate-checks/2026-10-25-concept-to-systems-design.md`):

- GDDs nur für Systeme von MVP-0. MVP-1 und v1 warten.
- Höchstens 2 `/design-review`-Runden pro GDD. Was danach offen ist, wandert ins
  nächste GDD oder in die Stories.
- Kleine Wegwerf-Tests in Unity nebenher sind erlaubt (unter `prototypes/`), damit es
  in der Designphase etwas zu spielen gibt.
- Reihenfolge der GDDs: siehe `design/gdd/systems-index.md` (Simulationskern,
  Menschen, dann sofort das Zustimmungsmodell; Begehren & Lust und Harem & Rituale
  direkt nach der Kultur).

## Nächste Schritte

1. ~~`/design-review design/gdd/game-concept.md`~~ (4 Durchgänge, erledigt)
2. ~~`/gate-check` (Konzept → Systemplanung)~~ (CONCERNS, Risiken akzeptiert, Phase
   ist jetzt Systems Design)
3. ~~`/map-systems`~~ (33 Systeme, 16 im MVP, `design/gdd/systems-index.md`)
4. `/estimate` für MVP-0 (vorgezogen, beschlossen am 2026-10-25)
5. `/art-bible` (vor der Architektur, für URP oder HDRP; die GDDs brauchen sie noch nicht)
6. `/design-system [system]` für jedes MVP-0-System, das Zustimmungs- und
   Lust-System früh
7. `/create-architecture`, dann die kritischen ADRs (Simulation ist die Autorität,
   Zustimmungsmodell als reine Funktion, URP oder HDRP)
8. Roadmap mit der Schätzung aktualisieren
9. `/sprint-plan new`
