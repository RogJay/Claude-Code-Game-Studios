# Simulationskern

> **Status**: In Design
> **Author**: Entwickler + Claude
> **Last Updated**: 2026-10-25
> **Last Verified**: 2026-10-25
> **Implements Pillar**: Pillar 1 (Die Welt lebt ohne dich), Pillar 2 (Jeder Eingriff hinterlässt eine Spur)

## Summary

Der Simulationskern ist der feste Takt, nach dem die ganze Welt von Allmacht rechnet:
Schritte fester Länge mit zwei Zeitebenen (Geschichte im Zeitraffer von 2 Jahren pro
Sekunde, Menschenleben deutlich langsamer), eigener Zufall pro System, Ereignisse mit
Ursache-Kette und die Zeitsteuerung. Er macht die Welt deterministisch und ohne Grafik
lauffähig, damit sie von selbst lebt, sich speichern und auswerten lässt und jeder
Eingriff des Spielers als festes Ereignis seine Spur hinterlässt.

> **Quick reference** — Layer: `Foundation` · Priority: `MVP` · Key deps: `None`

## Overview

Der Simulationskern ist der Takt, nach dem die Welt von Allmacht lebt. Er führt die
gesamte Simulation in festen Schritten aus, als reine Daten und ohne Grafik. Er
verwaltet die Spielzeit (Pause und die Geschwindigkeiten 1×, 4× und 16×), den
Zufall mit festem Startwert und die Ereignisse, über die alle anderen Systeme
miteinander reden. Er entscheidet, wann welches System rechnet, und garantiert,
dass derselbe Startzustand mit denselben Eingriffen immer dieselbe Geschichte
ergibt. Alles, was der Spieler sieht (die 3D-Welt, die Chronik, das HUD), ist
nur eine Ansicht dieses Zustands, und alles, was er tut (Kräfte, die Gotteshand),
kommt als festes Ereignis herein. Der Spieler bemerkt den Kern nicht direkt, er
erlebt, was er möglich macht: eine Welt, die ohne ihn weiterlebt (Pillar 1), und
Eingriffe, die als Spur erhalten bleiben (Pillar 2). Ohne ihn gäbe es keinen
Schnellvorlauf ohne Grafik, kein verlässliches Speichern und keine Auswertung
über viele Welten.

## Detailed Design

### Core Rules

**Schritte und die zwei Zeitebenen**

1. Die Simulation rechnet in **Schritten** fester Länge. Ein Schritt ist die
   kleinste Zeiteinheit. Es gibt keine Schritte mit variabler Länge, und keine
   Regel der Simulation liest die echte Zeit (keine Bildzeit, kein `deltaTime`).
   Prüfbar gemacht durch eine eigene Assembly ohne Engine-Referenzen
   (`noEngineReferences`); nur der Treiber pro Bild liegt außerhalb.
2. Auf dem Schrittzähler laufen **zwei Zeitebenen** (Entscheidung des Entwicklers
   vom 2026-10-25):
   - **Geschichtszeit** für Stämme, Kultur, Krieg, Gotteskraft und Chronik. Sie
     läuft im Zeitraffer wie in Prototyp 1: 2 Jahre pro Sekunde bei 1×, eine
     **Ära** sind 25 Jahre. Dadurch gelten die Messwerte aus Prototyp 1 weiter.
   - **Lebenszeit** für einzelne Menschen und ihre Handlungen (gehen, feiern,
     Szenen, Harem, Altern). Sie ist langsamer: Ein Mensch lebt mehrere Minuten,
     ein Fest dauert Sekunden bis eine Minute. Die Lebensdauer selbst legt das GDD
     Menschen & Bevölkerung fest.
   - Beide Ebenen sind nur Umrechnungen desselben Schrittzählers (siehe Formulas).
     Dass ein Mensch Hunderte Geschichtsjahre erlebt, ist gewollt: Geschichte ist
     gerafft, das Leben nicht. Die Chronik nennt Geschichtsjahre, das Alter eines
     Menschen wird in Lebensjahren angegeben, nie in Geschichtsjahren.
3. Jedes System meldet sich mit einem **Takt** an, also „alle N Schritte“, und
   einem **Versatz**, damit nicht alle Systeme im selben Schritt rechnen:
   - Entscheidungen einzelner Menschen: alle 1–2 Schritte (Lebenszeit)
   - Stammeslogik, Krieg, Kultur, Verfall der Erinnerung, Zufluss der Gotteskraft:
     einmal pro Geschichtsjahr (wie der Jahresschritt in Prototyp 1)
   - Ärenwechsel: alle 25 Geschichtsjahre, als Ereignis „Neue Ära“

   Die Taktraten gelten in **Schritten, nicht in echter Zeit**. Damit ist die offene
   Frage aus dem Konzept geklärt: Menschen entscheiden bei 1× mit 5–10 Hz wie
   geplant. Die Jahressysteme laufen einmal pro Geschichtsjahr, bei 1× also mit
   2 Hz statt der im Konzept geschätzten ~1 Hz (siehe Kalibrierung in Formulas).
   Bei 4× und 16× rechnen alle Systeme entsprechend öfter pro echter Sekunde.

**Ablauf eines Schritts**

4. Jeder Schritt läuft in dieser festen Reihenfolge ab:
   1. **Eingriffe anwenden:** Alle Eingriffe, die für diesen Schritt eingereiht
      sind, werden in der Reihenfolge ihres Eingangs angewendet. Danach meldet der
      Kern, falls fällig, das Ereignis „Neue Ära“ (Formel 2).
   2. **Fällige Systeme rechnen,** in fester Reihenfolge: Welt & Insel → Menschen &
      Bevölkerung → Kultur & Erinnerung → Stämme & Diplomatie → Begehren & Lust →
      Harem & Rituale → Gotteskraft & Götterkräfte → Chronik. Kultur vor Stämmen
      folgt dem Jahresschritt von Prototyp 1 (Verfall und Kulturwandel vor Krieg und
      Abspaltung), damit dessen Messwerte übertragbar bleiben.
   3. **Ereignisse zustellen:** Die Ereignisse dieses Schritts gehen in der
      Reihenfolge ihrer Entstehung an alle Empfänger. Wer auf ein Ereignis reagiert,
      ändert den Zustand erst im **nächsten** Schritt. Innerhalb eines Schritts gibt
      es keine Kettenreaktion.
   4. **Schritt abschließen:** Der Zähler steigt um 1. Erst danach darf die Ansicht
      den neuen Zustand lesen.
5. Innerhalb eines Systems werden Menschen, Stämme und Orte immer in aufsteigender
   ID-Reihenfolge abgearbeitet, nie in der Reihenfolge einer Hash-Tabelle.

**Zufall**

6. Zufall kommt nur aus dem eigenen Zufallsgenerator des Kerns, nie aus
   `UnityEngine.Random` oder `System.Random` mit Zeit-Startwert. Jedes System hat
   einen eigenen Zufallsstrom, der aus dem **Welt-Startwert** und der Kennung des
   Systems abgeleitet ist. Ein zusätzlicher Zufallsaufruf in einem System verschiebt
   also nicht den Zufall der anderen. Die Zustände aller Ströme gehören zum
   Spielstand.

**Determinismus**

7. Derselbe Welt-Startwert und dieselbe Liste von Eingriffen (jeder mit seiner
   Schrittnummer) ergeben auf **demselben Build und demselben Rechner** immer genau
   denselben Zustand nach jedem Schritt. Über verschiedene Rechner oder Plattformen
   hinweg wird das nicht verlangt (Entscheidung vom 2026-10-25). Normale
   Gleitkommazahlen sind erlaubt. Für Tests liefert der Kern einen **kanonischen
   Zustands-Hash** (feste Reihenfolge aller Felder, unabhängig vom Dateiformat des
   Spielstands), mit dem zwei Läufe verglichen werden.
8. Die Geschwindigkeit ändert nie das Ergebnis, sondern nur, wie viele Schritte pro
   echter Sekunde laufen. 100 Ären bei 1×, bei 16× und ohne Grafik ergeben denselben
   Zustand.

**Grenze zur Physik und zur Ansicht**

9. Die Simulation liest nie die Physik und nie die 3D-Szene. Ergebnisse der Physik
   (ein Wurf landet, jemand wird verletzt) meldet die Ansicht als **festes
   Eingriffsereignis**: Art, Ziel-ID, Ort (Zelle der Insel), Ergebnis,
   Verletzungsklasse. Es wird für den nächsten Schritt eingereiht und wie jeder
   andere Eingriff aufgezeichnet. Eine aufgezeichnete Sitzung läuft deshalb auch
   ohne Physik exakt gleich nach.
10. Solange ein Mensch in der Gotteshand gehalten wird oder als Ragdoll fliegt, gilt
    er in der Simulation als **„in Gottes Hand“**: Er trifft keine Entscheidungen,
    bewegt sich nicht und kann nicht sterben. Die Ansicht meldet den Beginn und das
    Ende mit festen Ereignissen. Erst das Endereignis (mit Ort und Ergebnis) gibt ihn
    zurück. Ist das Ergebnis ein tödlicher Aufprall, stirbt er beim Anwenden des
    Endereignisses. Welche Verletzungsklassen es gibt, legt das GDD Gotteshand &
    Ragdoll fest.
11. Die Ansicht liest nur den Zustand nach einem abgeschlossenen Schritt und
    schreibt nie hinein. Was sie zwischen zwei Schritten zeigt (zum Beispiel
    Laufanimationen), ist Darstellung und hat keine Wirkung.

**Zeitsteuerung**

12. Es gibt die Geschwindigkeiten **Pause, 1×, 4× und 16×**, dazu für Werkzeuge
    und Auswertungen **„ohne Grafik“** (so schnell, wie der Rechner kann).
13. In der Pause läuft kein Schritt. Kamera, Chronik und das Ansehen von Menschen,
    Stämmen und Orten bleiben möglich. Kräfte und Gotteshand sind gesperrt, weil
    jeder Eingriff einen Schritt braucht, der ihn anwendet.
14. Schafft ein Bild nicht alle fälligen Schritte, rechnet der Kern höchstens eine
    feste Zahl Schritte pro Bild (siehe Formulas). Die Welt läuft dann langsamer als
    eingestellt, statt die Bildzeit aufzuschaukeln. Die Ansicht zeigt das an.

**Ereignisse**

15. Jede Änderung, die andere Systeme oder die Chronik betrifft, wird als
    **Ereignis** gemeldet: Typ, Schrittnummer, beteiligte IDs, Ursache (die ID des
    auslösenden Ereignisses oder Eingriffs, falls es eine gibt), Gewicht für die
    Chronik. Über diese Ursache-Kette kann die Chronik erklären, warum etwas
    passiert ist (Pillar 2).
16. Ereignisse sind reine Daten. Gespeichert wird nur zwischen zwei Schritten, dann
    sind alle Ereignisse des letzten Schritts zugestellt. Zum Spielstand gehören die
    **Eingriffswarteschlange** und alles, was Systeme als Reaktion für den nächsten
    Schritt vorgemerkt haben (das ist Teil ihres Zustands). Wie lange zugestellte
    Ereignisse aufbewahrt werden, entscheiden die Chronik und Kultur & Erinnerung.

### States and Transitions

| Zustand | Bedeutung | Schritte pro echter Sekunde | Eingriffe |
|---|---|---|---|
| **Pausiert** | Welt steht still | 0 | gesperrt (Ansehen erlaubt) |
| **Läuft 1×** | Normale Geschwindigkeit | Grundrate (Formulas) | erlaubt |
| **Läuft 4×** | Schnell | 4 × Grundrate | erlaubt |
| **Läuft 16×** | Sehr schnell | 16 × Grundrate | erlaubt |
| **Ohne Grafik** | Werkzeuge und Auswertung, keine Ansicht | so viele wie möglich | nur aus einer Eingriffsliste |
| **Lädt** | Spielstand wird eingelesen | 0 | gesperrt |

| Von | Nach | Auslöser |
|---|---|---|
| Pausiert | Läuft 1×/4×/16× | Spieler wählt eine Geschwindigkeit |
| Läuft (beliebig) | Läuft (andere Stufe) | Spieler wählt eine Geschwindigkeit, wirkt ab dem nächsten Bild |
| Läuft (beliebig) | Pausiert | Spieler drückt Pause, oder ein Spielstand wird gespeichert (Speichern pausiert kurz und kehrt zur vorigen Stufe zurück) |
| beliebig | Lädt | Spielstand laden |
| Lädt | Pausiert | Spielstand vollständig eingelesen, die Ansicht ist neu aufgebaut |
| (Start der Welt) | Pausiert | Neue Welt aus einem Startwert erzeugt |
| (Werkzeug) | Ohne Grafik | Debug- und Balancing-Werkzeuge starten einen Lauf |

### Interactions with Other Systems

| System | Was hineingeht | Was herauskommt | Wer besitzt die Schnittstelle |
|---|---|---|---|
| Welt & Insel | – | Takt (jährlich), Zufallsstrom, Ereignisse (z. B. Dürre) | Kern |
| Menschen & Bevölkerung | – | Takt (1–2 Schritte, Lebenszeit), Umrechnung Lebensjahr ↔ Schritte, Zufallsstrom, Status „in Gottes Hand“ | Kern |
| Stämme & Diplomatie | – | Takt (jährlich), Zufallsstrom, Ereignisse (Krieg, Frieden, Abspaltung) | Kern |
| Kultur & Erinnerung | – | Takt (jährlich), Ereignisse samt Ursache-Kette, angewendete Eingriffe | Kern; „Eingriff erfassen“ besitzt Kultur & Erinnerung |
| Zustimmungsmodell | – | keine: Es ist eine reine Funktion ohne Zufall und ohne Takt, wird aber nur innerhalb eines Schritts aufgerufen | Zustimmungsmodell |
| Begehren & Lust, Harem & Rituale | – | Takte, Zufallsströme, Ereignisse | Kern |
| Gotteskraft & Götterkräfte | Eingriffe (Kraft, Ziel, Schritt) | Takt (jährlich), Eingriff angewendet als Ereignis | Kern (Eingriffswarteschlange) |
| Gotteshand & Ragdoll | feste Eingriffsereignisse (Beginn und Ende „in Gottes Hand“, Ergebnis, Verletzungsklasse) | Status „in Gottes Hand“ | Kern (Format des Eingriffsereignisses) |
| Weltansicht & Kamera | – | Lesezugriff auf den Zustand nach jedem Schritt, aktuelle Geschwindigkeit, Hinweis „läuft langsamer als eingestellt“ | Kern |
| Chronik | – | alle Ereignisse mit Ursache und Gewicht | Kern (Ereignisformat), Chronik (Auswahl und Gewichtung) |
| Speichern & Laden | kompletter Zustand beim Laden | Schrittzähler, Zustände aller Zufallsströme, Zustände aller Systeme (inklusive vorgemerkter Reaktionen), Eingriffswarteschlange, kanonischer Zustands-Hash für Tests | Kern legt fest, *was* gespeichert wird, Speichern & Laden legt das Format fest |
| Debug- & Balancing-Werkzeuge | Startwert, Eingriffsliste, Laufdauer | Lauf ohne Grafik, Zustand und Ereignisse nach jedem Schritt | Kern |
| HUD & Chronik-Ansicht | Geschwindigkeitswahl, Pause | Geschichtsjahr, Ära, aktuelle Geschwindigkeit, Hinweis „läuft langsamer“ | Kern |

## Formulas

Vorschlag des `systems-designer` (2026-10-25), im guided-Modus ohne Einzelfreigabe
übernommen; der Entwickler prüft ihn mit dem GDD. Die Kernentscheidung: 5 Schritte pro Geschichtsjahr und 10 Schritte pro
Sekunde bei 1×. Damit entscheiden Menschen mit 5–10 Hz bei 1× wie geplant, und die
Jahreslogik läuft genau einmal pro Geschichtsjahr wie in Prototyp 1.

### 1. Grundrate

The Grundrate formula is defined as:

`R = S_year * Y_s` und `R_v = m * R`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| S_year | int | 5–10, Default 5 | data file | Schritte pro Geschichtsjahr. Mindestens 5, weil die fünf Jahressysteme die Versätze 0–4 belegen (Formel 5) |
| Y_s | int | 2 | constant | Geschichtsjahre pro echter Sekunde bei 1× (120 Jahre pro Minute, Konzept) |
| R | int | 10–20, Default 10 | calculated | Grundrate: Schritte pro echter Sekunde bei 1× |
| m | int | {0, 1, 4, 16} | constant | Geschwindigkeitsfaktor (Pause = 0) |
| R_v | int | {0, 10, 40, 160} | calculated | Schritte pro echter Sekunde bei der gewählten Geschwindigkeit |

**Output Range:** 0 in der Pause. „Ohne Grafik“ hat keine Obergrenze und läuft bis
zur Ziellänge. S_year muss eine ganze Zahl ≥ 1 sein, sonst schwankt die Jahreslogik
zwischen zwei Schrittzahlen.
**Example:** S_year = 5 → R = 10 Schritte/s bei 1×, 40 bei 4×, 160 bei 16×.

### 2. Geschichtszeit

The Geschichtszeit formula is defined as:

`Y = floor(t / S_year)` · `Era = floor(Y / E_len)` · `S_era = E_len * S_year`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| t | int64 | ≥ 0 | calculated | Schrittzähler seit Beginn der Welt |
| Y | int | ≥ 0 | calculated | Geschichtsjahr |
| E_len | int | 25 | constant | Geschichtsjahre pro Ära (Konzept) |
| Era | int | ≥ 0 | calculated | Ärenindex, ab 0 (angezeigt ab I) |
| S_era | int | 125 bei Default | calculated | Schritte pro Ära |

**Output Range:** Alle Werte sind ganzzahlig und wachsen monoton. Eine Ära dauert
bei 1× 12,5 s.
**Example:** t = 1000 → Y = 200, Era = 8 (Ära IX). Der Ärenwechsel liegt auf
t mod 125 = 0 und t > 0. Der Kern meldet das Ereignis „Neue Ära“ in diesem
Schritt nach dem Anwenden der Eingriffe und vor Welt & Insel. Bei t = 0 gibt es kein
Ära-Ereignis, die Welt beginnt in Ära I.

### 3. Lebensjahr (Lebenszeit)

The Lebensjahr formula is defined as:

`age_Lj = floor(age_steps / S_Lj)` mit `age_steps = (t - t_birth) - held_steps` und `T_life = L_Lj * S_Lj / R`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| t_birth | int64 | ≥ 0 | calculated | Schritt, in dem der Mensch erschaffen wurde |
| held_steps | int64 | ≥ 0 | calculated | Summe aller Schritte, die der Mensch „in Gottes Hand“ war (Altern ist dann angehalten, Core Rule 10) |
| age_steps | int64 | ≥ 0 | calculated | gelebte Schritte ohne die Zeit in Gottes Hand |
| S_Lj | int | 30–120, Default 60, Vielfaches von S_year | data file | Schritte pro Lebensjahr |
| L_Lj | int | Default 40 | data file | Lebensdauer in Lebensjahren, **Eigentum von Menschen & Bevölkerung** (hier nur zur Umrechnung) |
| age_Lj | int | ≥ 0 | calculated | Alter in Lebensjahren seit der Erschaffung |
| T_life | float | Sekunden | calculated | Lebensdauer in echten Sekunden bei 1× |

**Output Range:** Bei S_Lj 30–120 und 40 Lebensjahren sind das 120–480 s
(2–8 Minuten), passend zu „mehrere Minuten“. Ein Lebensjahr sind beim Default
12 Geschichtsjahre.
**Example:** 40 × 60 / 10 = 240 s = 4 Minuten. Der Mensch erlebt dabei 480
Geschichtsjahre, also etwa 19 Ären (gewollt, Core Rule 2). Ein Fest von 30 s bei 1×
dauert 300 Schritte, also 5 Lebensjahre.

### 4. Schrittbudget pro Bild

The Schrittbudget formula is defined as:

`A = A + min(dt, dt_max) * R * m` · `n = min(floor(A), cap)` · `A = min(A - n, 1)` · `v_eff = min(m, cap * fps / R)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| dt | float | > 0 s | calculated | echte Bildzeit, nur im Treiber des Kerns, nie in einer Regel der Simulation |
| dt_max | float | 0,1 s | data file | Deckel gegen lange Aussetzer (Laden, Fenster verschoben) |
| A | float | 0 bis 1 nach jedem Bild (innerhalb des Bilds kurz bis cap + 1) | calculated | Schritt-Akkumulator |
| cap | int | 4–32, Default 8 | data file | höchstens gerechnete Schritte pro Bild |
| n | int | 0..cap | calculated | in diesem Bild gerechnete Schritte |
| fps | float | > 0 | calculated | tatsächliche Bildrate |
| v_eff | float | 0..m | calculated | tatsächliche Geschwindigkeit in Vielfachen von 1× |

**Output Range:** n liegt zwischen 0 und cap. Ein Überschuss über cap wird verworfen
(A bleibt ≤ 1): Die Welt läuft langsamer, baut aber keinen Rückstand auf, und das
Ergebnis der Simulation ändert sich nicht. Damit 16× bei der kleinsten unterstützten
Bildrate erreichbar bleibt, gilt `cap ≥ ceil(16 * R / fps_min)`. Bei fps_min = 30
sind das 6, mit Reserve ergibt sich der Default 8.
**Example:** 16× bei 60 fps braucht 2,67 Schritte pro Bild (abwechselnd 2 oder 3),
v_eff = min(16, 8 · 60 / 10) = 16. Mit cap = 2 wäre bei 60 fps nur 12× möglich. Bei
1× und 60 fps fällt nur etwa in jedem sechsten Bild ein Schritt an. Die Ansicht muss
dazwischen interpolieren, das ist reine Darstellung (Core Rule 11).

### 5. Taktplan

The Takt formula is defined as:

`läuft(sys, t) = (t mod P_sys == O_sys)` mit `0 ≤ O_sys < P_sys`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| P_sys | int | ≥ 1 | data file | Takt eines Systems in Schritten |
| O_sys | int | 0..P_sys − 1 | data file | Versatz |
| P_person | int | {1, 2}, Default 2 | data file | Takt der Entscheidungen einzelner Menschen: 10 bzw. 5 Hz bei 1× |
| O_person | int | id mod P_person | calculated | Versatz pro Mensch, damit pro Schritt nur ein Teil der Bevölkerung entscheidet |
| P_year | int | = S_year | calculated | Takt aller Jahressysteme |
| O_year | int | siehe Tabelle | data file | Versatz der Jahressysteme |

Versätze der Jahressysteme, in der Reihenfolge des Jahresschritts von Prototyp 1:

| System | Welt & Insel | Kultur & Erinnerung (inkl. Verfall) | Stämme & Diplomatie (inkl. Krieg, Abspaltung) | Gotteskraft (Zufluss) | Chronik |
|---|---|---|---|---|---|
| O_year | 0 | 1 | 2 | 3 | 4 |

Begehren & Lust sowie Harem & Rituale arbeiten auf der Ebene einzelner Menschen und
laufen im Personentakt. Ihre GDDs können einen eigenen Takt festlegen, der ein
Vielfaches von P_person ist.

**Output Range:** Pro Schritt laufen höchstens ein Jahressystem und ein Teil der
Menschen. Das Muster wiederholt sich alle kgV(P_person, S_year) Schritte (10 beim
Default). Ist O ≥ P, läuft ein System nie, das ist ein Konfigurationsfehler. P_person
darf nicht größer als S_year sein, sonst entscheiden Menschen seltener als einmal pro
Geschichtsjahr.
**Example:** t = 11 → 11 mod 5 = 1: Kultur & Erinnerung läuft, 11 mod 2 = 1: Menschen
mit ungerader ID entscheiden. t = 12 → 12 mod 5 = 2: Stämme & Diplomatie läuft,
Menschen mit gerader ID entscheiden.

### 6. Startwert der Zufallsströme

The Stromstartwert formula is defined as:

`seed_sys = SplitMix64( SplitMix64(world_seed) + system_id * 0x9E3779B97F4A7C15 )` (Rechnung in uint64 mit Überlauf)

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| world_seed | uint64 | 0 bis 2^64 − 1 | data file | Welt-Startwert (aus dem Spielstand oder bei neuer Welt eingegeben/gewürfelt) |
| system_id | int | 1..N, feste Konstanten, werden nie umnummeriert | constant | Kennung des Systems |
| SplitMix64(x) | uint64 | – | constant | z = x + 0x9E3779B97F4A7C15; z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9; z = (z ^ (z >> 27)) * 0x94D049BB133111EB; Ergebnis z ^ (z >> 31) |
| seed_sys | uint64 | – | calculated | Startwert des Zufallsstroms dieses Systems |

Feste Systemkennungen (werden nie umnummeriert, neue Systeme bekommen die nächste freie Zahl):

| system_id | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 |
|---|---|---|---|---|---|---|---|---|
| System | Welt & Insel | Menschen & Bevölkerung | Kultur & Erinnerung | Stämme & Diplomatie | Begehren & Lust | Harem & Rituale | Gotteskraft & Götterkräfte | Chronik |

Zustimmungsmodell, Gotteshand und die Ansicht haben keinen Strom: Das
Zustimmungsmodell kennt keinen Zufall, und die Physik der Ansicht ist nicht Teil der
Simulation.

**Output Range:** volle 64 Bit. Die Konstante ist ungerade, deshalb bekommen
verschiedene Systeme derselben Welt nie denselben Startwert. Aus seed_sys füllen vier
weitere SplitMix64-Ausgaben den Zustand eines xoshiro256**-Generators (nie alle vier
null). `string.GetHashCode` und `System.Random` werden nicht verwendet, weil sie in
.NET nicht stabil oder nicht festgelegt sind.
**Example:** world_seed = 42 und system_id = 3 ergeben den Strom für System 3. Mit
system_id = 4 entsteht ein unabhängiger Strom. Wie oft System 3 würfelt, verschiebt
System 4 nicht.

### Kalibrierung gegen Prototyp 1

- Jeder Wert von S_year zwischen 5 und 10 lässt die Messwerte von Prototyp 1 gelten,
  solange Kultur, Stämme, Krieg und Verfall genau einmal pro Geschichtsjahr laufen.
- Die Messwerte brechen, wenn ein Jahressystem in einem anderen Takt läuft, zum
  Beispiel Stammeslogik bei „1 Hz in echter Zeit“ (alle 2 Jahre) oder in jedem
  Schritt. Aus 2 Jahren pro Sekunde folgt deshalb zwingend: Stammeslogik bei 2 Hz
  bei 1× und bei 32 Hz bei 16×, nicht bei den im Konzept geschätzten ~1 Hz.
- Prototyp 1 kannte kein Altern einzelner Menschen. Sobald Menschen & Bevölkerung
  Altern und Tod über S_Lj einführt, ist die Aussterberate mit den Debug- und
  Balancing-Werkzeugen neu zu messen.

## Edge Cases

*`systems-designer` not consulted for this section — Lean mode. Review manually before production.*

- **If der Rechner die Geschwindigkeit nicht schafft** (n erreicht in mehreren
  Bildern hintereinander cap): Die Welt läuft mit v_eff statt m. Das HUD zeigt
  „läuft langsamer (v_eff×)“, sobald v_eff seit mindestens 1 echten Sekunde unter
  90 % von m liegt, und blendet es aus, sobald v_eff wieder 95 % von m erreicht
  (Hysterese gegen Flackern). Der Zustand nach
  einer bestimmten Schrittzahl ist derselbe wie bei voller Geschwindigkeit.
- **If ein Bild länger als dt_max dauert** (Laden, Fenster verschoben, Rechner
  stockt): Es wird nur dt_max angerechnet. Die verlorene Zeit wird nicht nachgeholt,
  die Welt springt also nicht.
- **If ein Eingriff für einen Schritt ankommt, der schon gerechnet ist** (zum
  Beispiel ein Physikergebnis, das zwischen zwei Schritten fertig wird): Er wird für
  den nächsten noch nicht gerechneten Schritt eingereiht. Aufgezeichnet wird die
  Schrittnummer, in der er tatsächlich angewendet wurde.
- **If zwei Eingriffe im selben Schritt dasselbe Ziel treffen**: Sie werden in der
  Reihenfolge ihres Eingangs angewendet, der zweite sieht das Ergebnis des ersten.
- **If ein Eingriff ein Ziel trifft, das es nicht mehr gibt** (der Mensch ist
  zwischen Klick und Anwendung gestorben, der Stamm untergegangen): Der Eingriff wird
  nicht angewendet. Der Kern meldet das Ereignis „Eingriff ins Leere“ mit der
  ursprünglichen Absicht. Ob Gotteskraft zurückerstattet wird, entscheidet das GDD
  Gotteskraft & Götterkräfte.
- **If ein Mensch „in Gottes Hand“ ist, wenn seine Lebenszeit endet**: Er stirbt
  nicht in der Hand und nicht im Flug. Wird das Endereignis in Schritt e angewendet,
  stirbt er frühestens im ersten Schritt s > e mit s mod P_person = id mod P_person,
  also in seinem nächsten Personentakt nach dem Schritt der Rückgabe. Altern ist angehalten, solange er „in Gottes Hand“ ist.
- **If die Pause gedrückt wird, während ein Mensch fliegt**: Die Simulation macht
  keine Schritte mehr, und die Ansicht hält auch die Physik an. Beim Fortsetzen
  fliegt der Mensch weiter. Ein Wurf landet nie in einer pausierten Welt.
- **If die Geschwindigkeit während eines Wurfs geändert wird**: Die Physik in der
  Ansicht läuft weiter in echter Zeit, die Welt rechnet schneller oder langsamer.
  Ein Flug von 2 echten Sekunden bei 16× dauert 320 Schritte, also 64
  Geschichtsjahre, in denen der Mensch angehalten ist und nicht altert. Das ist
  gewollt, die Hand ist bei jeder Geschwindigkeit erlaubt.
- **If gespeichert wird, während ein Mensch „in Gottes Hand“ ist**: Vor dem
  Speichern setzt die Ansicht ihn an der Stelle der Hand ab und meldet das
  Endereignis („abgesetzt“). Ein Spielstand enthält nie einen Menschen in der Hand
  oder im Flug.
- **If die Ansicht eine gehaltene oder fliegende Figur verliert** (Figur
  verschwindet, Fehler in der Ansicht): Beim nächsten Abgleich meldet die Ansicht
  für jeden Menschen „in Gottes Hand“ ohne zugehörige Figur ein Endereignis
  „abgesetzt“ an der zuletzt bekannten Zelle. Kein Mensch bleibt dauerhaft
  angehalten.
- **If der Welt-Startwert 0 ist**: erlaubt. SplitMix64 erzeugt auch aus 0 gültige,
  nicht verschwindende Ströme.
- **If ein Spielstand in einem anderen Build geladen wird**: Er lädt, weil er den
  ganzen Zustand enthält und nichts nachrechnet. Eine aufgezeichnete Sitzung
  (Startwert und Eingriffsliste) ist nur im selben Build garantiert gleich. Sie
  speichert die Build-Kennung, und bei Abweichung warnen die Werkzeuge, dass die
  Wiedergabe abweichen kann.
- **If in einem Schritt sehr viele Ereignisse entstehen** (zum Beispiel ein Krieg
  mit vielen Toten): Alle werden zugestellt, es gibt keine Obergrenze im Kern. Was
  davon in der Chronik erscheint, entscheidet die Chronik über Gewicht und Filter.
- **If der Schrittzähler sehr groß wird**: Er ist int64. Bei 160 Schritten pro
  Sekunde reicht das für über eine Milliarde Jahre Dauerbetrieb, ein Überlauf ist
  also ausgeschlossen.

## Dependencies

**Hängt ab von:** nichts. Der Simulationskern ist das unterste System (Foundation).
Er ist reines C# ohne Abhängigkeit von Unity-Szenen oder Physik. Nur der Treiber, der
pro Bild die fälligen Schritte auslöst, sitzt in der Unity-Bildschleife.

**Wird gebraucht von** (alle hart: ohne den Kern funktionieren sie nicht):

| System | Was es vom Kern braucht | GDD |
|---|---|---|
| Welt & Insel | Jahrestakt, Zufallsstrom, Ereignisse | noch keins |
| Menschen & Bevölkerung | Personentakt, Umrechnung Lebensjahr ↔ Schritte, Zufallsstrom, Status „in Gottes Hand“ | noch keins |
| Kultur & Erinnerung | Jahrestakt, Ereignisse mit Ursache-Kette, angewendete Eingriffe | noch keins |
| Stämme & Diplomatie | Jahrestakt, Zufallsstrom, Ereignisse | noch keins |
| Begehren & Lust | Personentakt, Zufallsstrom, Ereignisse | noch keins |
| Harem & Rituale | Personentakt, Zufallsstrom, Ereignisse | noch keins |
| Gotteskraft & Götterkräfte | Jahrestakt, Eingriffswarteschlange | noch keins |
| Gotteshand & Ragdoll | Format des Eingriffsereignisses, Status „in Gottes Hand“ | noch keins |
| Weltansicht & Kamera | Lesezugriff nach jedem Schritt, Geschwindigkeit, Pause hält die Physik an | noch keins |
| Chronik | Ereignisformat mit Ursache und Gewicht | noch keins |
| Speichern & Laden | Liste dessen, was zum Zustand gehört | noch keins |
| Debug- & Balancing-Werkzeuge | Lauf ohne Grafik, Eingriffsliste, Zustand nach jedem Schritt | noch keins |
| HUD & Chronik-Ansicht | Geschwindigkeitswahl, Jahr, Ära, Hinweis „läuft langsamer“ | noch keins |

**Weich:** Das Zustimmungsmodell braucht den Kern nicht. Es ist eine reine Funktion
ohne Zufall und ohne Takt. Es darf aber nur innerhalb eines Schritts aufgerufen
werden (von Begehren & Lust sowie Harem & Rituale).

**Gegenseitigkeit:** Keines der abhängigen Systeme hat schon ein GDD. Jedes muss den
Simulationskern in seinem Abschnitt Dependencies nennen und die Verträge oben
übernehmen.

## Visual/Audio Requirements

Keine eigenen. Der Simulationskern hat keine Darstellung und keinen Ton. Was der
Spieler von ihm sieht (Ärenwechsel, Hinweis „läuft langsamer“), gestalten die
Weltansicht, die Chronik und das HUD.

## Game Feel

Der Kern bestimmt das Tempo der Welt: Bei 1× vergeht etwa alle 3 Sekunden ein großes
Ereignis (Prototyp 1), alle 12,5 Sekunden beginnt eine Ära. Ziel ist, dass ein
Geschwindigkeitswechsel sofort spürbar ist: Er wirkt ab dem nächsten Bild, ohne
Verzögerung und ohne Sprung. Bei 1× fällt nur etwa in jedem sechsten Bild ein
Schritt an. Damit Bewegungen trotzdem flüssig wirken, interpoliert die Ansicht
zwischen zwei Schritten (Core Rule 11).

## UI Requirements

Der Kern liefert die Daten, das HUD zeigt sie an:

- Zeitsteuerung mit Pause, 1×, 4× und 16×, die aktive Stufe sichtbar markiert
- aktuelles Geschichtsjahr und die Ära (römisch, ab I)
- Hinweis „läuft langsamer (v_eff×)“ mit der Hysterese aus den Edge Cases
- in der Pause ein deutlicher Zustand „Pausiert“; Kräfte und Gotteshand sind dann
  sichtbar gesperrt

> **📌 UX Flag — Simulationskern**: This system has UI requirements. In Phase 4
> (Pre-Production), run `/ux-design` to create a UX spec for each screen or HUD
> element this system contributes to **before** writing epics. Stories that
> reference UI should cite `design/ux/[screen].md`, not the GDD directly.

## Cross-References

| Dokument | Was hier verwendet wird |
|---|---|
| `design/gdd/game-concept.md` (Session-Level, Technical Considerations) | Zeitskala 120 Jahre pro Minute, Ära 25 Jahre, Geschwindigkeiten, Richtwerte für Taktraten, Architektur „Simulation ist die Autorität“ |
| `prototypes/weltengott-concept/REPORT.md` und `prototype.html` | Jahresschritt und seine Reihenfolge, 2 Jahre pro Sekunde, Messwerte für die Kalibrierung |
| `prototypes/gotteshand-concept/REPORT.md` | Gotteshand mit Ragdoll als Ursache der Grenze Physik → festes Ereignis |
| `production/gate-checks/2026-10-25-concept-to-systems-design.md` | Hinweise des Technical Directors: Grenze des Determinismus, Zeitbasis, Speichern und Debug als eigene Systeme |
| `design/gdd/systems-index.md` | Abhängigkeiten, Systemreihenfolge |

Die abhängigen Systeme (siehe Dependencies) haben noch keine GDDs.

## Acceptance Criteria

Entwurf des `qa-lead` (2026-10-25). Defaults in allen Kriterien: S_year = 5,
E_len = 25, P_person = 2, S_Lj = 60, cap = 8, dt_max = 0,1 s. Tests liegen unter
`Assets/Tests/EditMode/Simulationskern/`. Die Treiber bekommen dt von außen
übergeben, keine Regel der Simulation liest die echte Zeit.

### A. Schritt, Zeit, Takt
- **AC-01 (Rule 1, 9) [Logic/EditMode]** **GIVEN** die Kern-Assembly hat eine asmdef mit `noEngineReferences`, **WHEN** ein Symbolscan über ihren Quelltext läuft, **THEN** gibt es 0 Treffer auf `UnityEngine`, `Time.`, `DateTime.Now`, `System.Random` und Physik-APIs.
- **AC-02 (Rule 2, Formel 2) [Logic/EditMode]** **GIVEN** S_year = 5, **WHEN** t = 0, 4, 5, 124, 125, 1000 und 2^40 ist, **THEN** ist Y = 0, 0, 1, 24, 25, 200 und 2^40 / 5 (abgerundet) und Era = 0, 0, 0, 0, 1, 8 und korrekt ohne Überlauf; Alter wird als age_Lj, die Chronik in Y geführt, als getrennte Typen.
- **AC-03 (Rule 2, Formel 2) [Logic/EditMode]** **GIVEN** ein Lauf ab t = 0, **WHEN** t = 125 erreicht ist, **THEN** entsteht genau 1 Ereignis „Neue Ära“, nach den Eingriffen und vor allen Ereignissen der Jahressysteme dieses Schritts; bei t = 0, 124 und 126 entsteht keines.
- **AC-04 (Rule 3, Formel 5) [Logic/EditMode]** **GIVEN** die Default-Versätze (Welt 0, Kultur 1, Stämme 2, Gotteskraft 3, Chronik 4), **WHEN** t = 0 bis 999 läuft, **THEN** läuft jedes Jahressystem genau 200-mal, pro Schritt höchstens 1 Jahressystem; bei t = 11 laufen Kultur und die Menschen mit ungerader ID, bei t = 12 Stämme und die Menschen mit gerader ID; das Muster wiederholt sich alle 10 Schritte.
- **AC-05 (Formel 1, 5) [Logic/EditMode]** **GIVEN** eine Konfiguration mit O_sys ≥ P_sys, P_person > S_year oder S_year < 5, **WHEN** sie geprüft wird, **THEN** meldet der Kern einen Konfigurationsfehler und startet nicht, statt ein System still nie laufen zu lassen.
- **AC-06 (Rule 4) [Logic/EditMode]** **GIVEN** Stub-Systeme protokollieren ihre Aufrufe, **WHEN** ein Schritt mit einem Eingriff läuft, **THEN** ist die Reihenfolge: Eingriffe (nach Eingang) → Welt → Menschen → Kultur → Stämme → Begehren → Harem → Gotteskraft → Chronik → Zustellung der Ereignisse → Zähler t + 1.
- **AC-07 (Rule 4.3, 15) [Logic/EditMode]** **GIVEN** System A meldet Ereignis E in Schritt t und System B empfängt E, **WHEN** die Schritte t und t + 1 laufen, **THEN** ist der Zustand von B nach t unverändert und nach t + 1 verändert; in t entsteht keine Folgekette.
- **AC-08 (Rule 5) [Logic/EditMode]** **GIVEN** Menschen und Stämme werden in der Reihenfolge 7, 3, 9, 1 angelegt, **WHEN** ein System sie abarbeitet, **THEN** ist die Reihenfolge 1, 3, 7, 9; bei 100 verschiedenen Anlegereihenfolgen ist der Zustands-Hash identisch.

### B. Zufall
- **AC-09 (Formel 6) [Logic/EditMode]** **GIVEN** world_seed ∈ {0, 42, 2^64 − 1} und die system_id 1 bis 8, **WHEN** seed_sys berechnet wird, **THEN** stimmt es mit festen Referenzwerten überein (Referenz für SplitMix64(0): 0xE220A8397B1DCDAF, gegen eine Referenzimplementierung zu prüfen), alle seed_sys sind paarweise verschieden, der xoshiro-Zustand ist nie ganz null, und es gibt keine Ausnahme bei Überlauf.
- **AC-10 (Rule 6) [Logic/EditMode]** **GIVEN** zwei Läufe, in denen System 3 einmal 0-mal und einmal 1000-mal würfelt, **WHEN** die ersten 100 Werte von System 4 verglichen werden, **THEN** sind sie identisch; nach Speichern in Schritt k und Laden setzt jeder Strom genau so fort wie im durchgehenden Lauf.

### C. Determinismus
- **AC-11 (Rule 7, 8) [Logic/EditMode]** **GIVEN** world_seed 42 (und 0), 50 Menschen und eine Eingriffsliste mit mindestens 20 Eingriffen samt Schrittnummer, **WHEN** 12.500 Schritte (100 Ären) laufen mit dem Treiber (a) 1× bei dt = 1/60, (b) 16× bei dt = 1/60, (c) 16× mit schwankendem dt inklusive Ausreißern über dt_max und verworfenem Überschuss über cap, (d) „ohne Grafik“, **THEN** ist der kanonische Zustands-Hash nach Schritt 1.000, 5.000 und 12.500 in allen vier Läufen identisch, ebenso die Ereignisfolge, in den ersten 1.000 Schritten auch nach jedem einzelnen Schritt; ein wiederholter Lauf ist identisch, ein anderer Startwert ergibt einen anderen Hash.

### D. Grenze zur Ansicht
- **AC-12 (Rule 9, systemübergreifend) [Integration/PlayMode]** **GIVEN** ein Wurf landet zwischen zwei Schritten und die Ansicht meldet das Eingriffsereignis (Art, Ziel-ID, Zelle, Ergebnis, Verletzungsklasse), **WHEN** der Kern weiterläuft, **THEN** wird es im nächsten noch nicht gerechneten Schritt t + 1 angewendet, erscheint im Ereignisstrom mit der Eingriffs-ID als Ursache, die Aufzeichnung enthält t + 1, und eine Wiedergabe der Aufzeichnung im Edit Mode ohne Physik ergibt denselben Hash.
- **AC-13 (Rule 10) [Logic/EditMode]** **GIVEN** ein Mensch ist „in Gottes Hand“, **WHEN** 600 Schritte laufen und ein Todesauslöser eintritt, **THEN** gibt es 0 Entscheidungsaufrufe für ihn, Ort und Alter bleiben unverändert, er lebt, und erst das Endereignis gibt ihn zurück.
- **AC-14 (Rule 11) [Logic/EditMode]** **GIVEN** die Ansicht bekommt nur eine schreibgeschützte Sicht auf den Zustand, **WHEN** 1.000 Lesezugriffe stattfinden, **THEN** ist der Zustands-Hash unverändert, und die Assembly der Ansicht kann den Zustand nicht verändern (Prüfung beim Kompilieren).

### E. Zeitsteuerung
- **AC-15 (Formel 1, Rule 12) [Logic/EditMode]** **GIVEN** m ∈ {0, 1, 4, 16} und S_year = 5, **WHEN** R_v berechnet wird, **THEN** ist R_v = 0, 10, 40 und 160; für S_year 5 bis 10 gilt R = 2 · S_year.
- **AC-16 (Rule 13, States) [Logic/EditMode]** **GIVEN** die Welt ist pausiert, **WHEN** 600 Bilder mit dt = 1/60 laufen, **THEN** läuft kein Schritt, Lesezugriffe funktionieren, und ein Eingriff in „Pausiert“ oder „Lädt“ wird abgelehnt (0 Einträge in der Warteschlange); Speichern pausiert und kehrt zur vorigen Stufe zurück, Laden endet in „Pausiert“, und ein Geschwindigkeitswechsel wirkt ab dem nächsten Bild.
- **AC-17 (Formel 4, Rule 14) [Logic/EditMode]** **GIVEN** 16× bei 60 fps, **WHEN** 60 Bilder laufen, **THEN** ist n pro Bild 2 oder 3 und die Summe 160 ± 1; bei 1× sind es 10 ± 1 Schritte, und immer gilt n ≤ cap.
- **AC-18 (Formel 4, Rule 14, Edge Case) [Logic/EditMode + Manual]** **GIVEN** 16× bei 15 fps, **WHEN** 100 Bilder laufen, **THEN** ist n = 8 pro Bild, A ≤ 1 nach jedem Bild, v_eff = 12, das Signal „läuft langsamer“ ist gesetzt, und der Zustand nach k Schritten gleicht dem bei voller Geschwindigkeit; [Manual] das HUD zeigt „läuft langsamer (12×)“ nach spätestens 1 s und blendet es aus, sobald v_eff 15,2 erreicht.
- **AC-19 (Edge Case dt_max) [Logic/EditMode]** **GIVEN** ein Bild mit dt = 2 s, **WHEN** es verarbeitet wird, **THEN** wird nur 0,1 s angerechnet (n ≤ cap), und im folgenden Bild wird nichts nachgeholt.

### F. Lebenszeit, Ereignisse, Sonderfälle
- **AC-20 (Formel 3) [Logic/EditMode]** **GIVEN** t_birth = 0, S_Lj = 60 und keine Zeit in Gottes Hand, **WHEN** t = 59, 60 und 2.399 ist, **THEN** ist age_Lj = 0, 1 und 39, T_life = 240 s bei R = 10, und ein Fest von 30 s sind 300 Schritte oder 5 Lebensjahre; **GIVEN** zusätzlich 120 Schritte in Gottes Hand, **THEN** ist age_Lj bei t = 2.399 gleich 37.
- **AC-21 (Rule 15, 16) [Logic/EditMode]** **GIVEN** die Kette Eingriff → E1 → E2, **WHEN** die Ursache-Kette von E2 zurückverfolgt wird, **THEN** endet sie lückenlos bei der Eingriffs-ID; jedes Ereignis hat Typ, Schritt, IDs, Ursache und Gewicht; 10.000 Ereignisse in einem Schritt werden alle zugestellt; Eingriffswarteschlange und vorgemerkte Reaktionen überstehen Speichern und Laden mit gleichem Hash.
- **AC-22 (Edge Case Eingriff ins Leere) [Logic/EditMode]** **GIVEN** das Ziel eines Eingriffs ist vor der Anwendung gestorben, **WHEN** der Eingriff an der Reihe ist, **THEN** bleibt der Zustand unverändert, und es entsteht das Ereignis „Eingriff ins Leere“ mit der ursprünglichen Absicht und der Eingriffs-ID als Ursache.
- **AC-23 (Edge Cases Eingriffe) [Logic/EditMode]** **GIVEN** zwei Eingriffe auf dasselbe Ziel im selben Schritt, **WHEN** sie angewendet werden, **THEN** sieht der zweite das Ergebnis des ersten, und mit vertauschtem Eingang entsteht ein anderes Ergebnis; **GIVEN** ein Eingriff für den schon gerechneten Schritt t, **THEN** wird er in t + 1 angewendet und t + 1 aufgezeichnet.
- **AC-24 (Edge Case Tod aufgeschoben) [Logic/EditMode]** **GIVEN** ein Mensch erreicht L_Lj, während er gehalten wird, **WHEN** das Endereignis in Schritt e angewendet wird, **THEN** stirbt er im ersten Schritt s > e mit s mod P_person = id mod P_person und nicht früher.
- **AC-25 (Edge Case Speichern in der Hand) [Integration/PlayMode]** **GIVEN** ein Mensch wird gehalten, **WHEN** gespeichert wird, **THEN** wird vorher das Endereignis „abgesetzt“ angewendet, und der Spielstand enthält 0 Menschen „in Gottes Hand“.
- **AC-26 (Edge Case verlorene Figur) [Integration/PlayMode]** **GIVEN** die Figur eines gehaltenen Menschen wird zerstört, **WHEN** der nächste Abgleich läuft, **THEN** kommt das Endereignis „abgesetzt“ an der letzten bekannten Zelle, und einen Schritt später ist der Mensch nicht mehr angehalten.
- **AC-27 (Edge Case anderer Build) [Logic/EditMode]** **GIVEN** eine Aufzeichnung mit abweichender Build-Kennung, **WHEN** sie abgespielt wird, **THEN** erscheint eine Warnung ohne Abbruch; ein Spielstand aus einem anderen Build lädt, und der Hash des geladenen Zustands gleicht dem gespeicherten.
- **AC-28 (Edge Cases Pause im Flug, Wurf bei 16×) [Manual]** **GIVEN** ein Mensch fliegt, **WHEN** pausiert wird, **THEN** stehen Physik und Schrittzähler still; **GIVEN** 16× und ein Flug von 2 s, **THEN** steigt der Schrittzähler um 320 (64 Geschichtsjahre), und das Alter des Menschen bleibt gleich.

### G. Leistung (Vorschlag, wartet auf Profiling)
- **AC-29 [Integration/PlayMode, Performance — ADVISORY bis zur ersten Messung]** **GIVEN** 50 Menschen bei 16× (160 Schritte pro Sekunde), **WHEN** 60 s auf dem Entwicklungsrechner bei 60 fps gemessen wird, **THEN** braucht der Kern pro Bild im Mittel höchstens 2,0 ms (12 % von 16,7 ms) und im schlechtesten Bild mit cap = 8 Schritten höchstens 4,0 ms (0,5 ms pro Schritt), mit 0 Byte Speicherzuteilung pro Schritt im Dauerbetrieb. Die Hardware des Entwicklungsrechners ist bei der ersten Messung zu notieren.

## Open Questions

| Frage | Wer klärt | Bis wann |
|---|---|---|
| Wie lange lebt ein Mensch (L_Lj) und wie wirkt sich Altern auf das Aussterben aus? S_Lj = 60 ist ein Vorschlag | GDD Menschen & Bevölkerung, dann Messung mit den Debug-Werkzeugen | mit dem GDD Menschen & Bevölkerung |
| Hardware des Entwicklungsrechners und echtes Leistungsbudget für AC-29 | Entwickler, beim ersten Profiling | erste Messung im MVP-0 |
| Technischer Weg für den Kern (einfache C#-Klassen oder ECS/DOTS) | ADR in der Architekturphase | `/create-architecture` |
| Wird Gotteskraft für einen „Eingriff ins Leere“ zurückerstattet? | GDD Gotteskraft & Götterkräfte | mit dem GDD Gotteskraft & Götterkräfte |
| Geschichte in Jahren und Leben in Lebensjahren: Versteht der Spieler die zwei Zeitebenen, oder braucht die Chronik eine Erklärung? | Playtest im MVP-0 | erster MVP-0-Playtest |
