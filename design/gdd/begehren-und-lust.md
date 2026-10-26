# Begehren & Lust

> **Status**: In Design
> **Author**: Claude, mit dem Entwickler
> **Last Updated**: 2026-10-26
> **Last Verified**: 2026-10-26
> **Implements Pillar**: Pillar 3 (Lust ist Kultur, nicht Beiwerk), Pillar 2 (Jeder Eingriff hinterlässt eine Spur), Pillar 1 (Die Welt lebt ohne dich)

## Summary

Begehren & Lust legt fest, wen ein Mensch begehrt (eine feste, einseitige Paar-Chemie),
wann er im Alltag oder auf einem Fest eine intime Handlung sucht, wie Feste entstehen und
ablaufen und was Lust in der Welt bewirkt: Paarbindungen sofort, Kulturwandel,
Zufriedenheit und Diplomatie über Schnittstellen, deren Gewicht bis zur Kalibrierung 0
ist. Jede Handlung geht durch das Zustimmungsmodell; dieses System fragt nur an und
beginnt, was zugestimmt wurde.

> **Quick reference** — Layer: `Feature` · Priority: `MVP` · Key deps: `Zustimmungsmodell, Kultur & Erinnerung, Menschen & Bevölkerung, Körper & Animation`

## Overview

Begehren & Lust ist das System, das Menschen zueinanderbringt. Jeder Einzelne begehrt
andere unterschiedlich stark; dieses Begehren b_ij ist eine feste Paar-Chemie, die beim
Hervortreten feststeht und die kein Eingriff verändert. Im Alltag sucht ein Mensch selten
und abhängig von seiner Lust jemanden in seiner Nähe, den er begehrt, und fragt beim
Zustimmungsmodell an; auf einem **Fest** geschieht das viel öfter, auch zu dritt oder zu
viert. Feste entstehen von selbst, je offener eine Kultur und je satter ein Stamm ist, und
der Spieler kann sie mit dem Fest-Segen auslösen. Lust verändert die Welt (Pillar 3):
Aus der ersten Handlung zu zweit wird eine **Paarbindung**, die Treue wirksam macht und in
der Chronik steht; Kulturwandel, Zufriedenheit eines Stamms und Diplomatie zwischen Stämmen
sind als Schnittstellen angelegt und bekommen ihre Werte nach der ersten Kalibrierung. Was
eine Figur tut, entscheidet immer das Zustimmungsmodell; die Darstellung bleibt einfach.

## Player Fantasy

Der Spieler sieht eine Welt, in der Menschen einander begehren und feiern, und er sieht,
dass die Kultur prägt, wie frei sie es tun: Die Lust-Sekte feiert oft und offen, der
Gottesstaat selten und verhalten. Segnet er ein Fest, erlebt er, wie es aufblüht, wie Paare
sich finden und Bindungen entstehen, und weiß, dass er niemanden zwingen kann: Er kann
Menschen zusammenbringen, aber nicht ihr Begehren oder ihr Ja.

## Detailed Design

### Core Rules

**Begehren**

1. Jeder Einzelne i hat für jeden anderen Einzelnen j ein **Begehren** b_ij zwischen 0 und 1
   (Formel 1). Es ist eine reine Funktion des Welt-Startwerts, der beiden IDs und der
   **Orientierung** von i; es ist einseitig (b_ij ≠ b_ji) und ändert sich nie. Keine Kraft,
   kein Eingriff, kein Platzieren mit der Gotteshand, keine Hingabe und kein Rang verändert
   es (Zustimmungsmodell Regel 19).
2. Die **Orientierung** ist eine Vorliebe, die dieses System beim Hervortreten an den
   Einzelnen hängt (Menschen Regel 6): welches Geschlecht er begehrt. Im MVP-0 begehren alle
   beide Geschlechter (Entscheidung des Entwicklers vom 2026-10-26); das Feld bleibt, damit
   andere Anteile später als Daten möglich sind.
3. Die Masse hat kein Begehren und nimmt an keiner Handlung teil (Menschen Regel 18).

**Wer handeln kann**

4. **Verfügbar** ist ein Einzelner, der lebt, frei und gesund ist, nicht in Gottes Hand,
   nicht erschüttert, nicht im Kampf und in keiner laufenden Handlung (Handlungsregister).
   Nur Verfügbare fragen an oder werden angefragt. Das Zustimmungsmodell prüft die harten
   Sperren trotzdem selbst; diese Regel spart nur Anfragen, sie ersetzt keine Prüfung.
5. Anfragen stellt dieses System nur über den Anfrage-Baustein des Zustimmungsmodells (nur
   IDs: Handlungsart, Beteiligte, Ort) und beginnt eine Handlung nur über `Beginne` im
   Handlungsregister (Zustimmungsmodell Regeln 18, 19). Eine abgelehnte Anfrage stellt es
   nicht noch einmal, solange ihre Prüfsumme gleich wäre (gleiche Beteiligte, Handlungsart,
   Ort und Werte).

**Alltag**

6. Im **Personentakt** (alle 2 Schritte, Simulationskern Formel 5) sucht ein verfügbarer
   Einzelner mit kleiner Wahrscheinlichkeit nach seiner Lust eine Handlung (Formel 2): Er
   wählt unter den Verfügbaren im Umkreis von 3 Zellen den, den er am meisten begehrt, und
   fragt Zärtlichkeit oder Sex zu zweit an (mit mehr Lust eher Sex). Der Ort ist **privat**,
   wenn sonst niemand innerhalb einer Zelle steht, sonst **öffentlich**.

**Feste**

7. Jeder lebende Stamm hat eine **Festprüfung** alle 10 Schritte (2 Geschichtsjahre): Er
   beginnt ein Fest mit einer Wahrscheinlichkeit, die mit der Offenheit seiner Kultur für
   Sex zu zweit und seinem Wohlstand steigt (Formel 3). Kein Fest beginnt im Krieg, im
   Hunger, während schon ein Fest des Stamms läuft oder mit weniger als 2 verfügbaren
   Einzelnen.
8. Der **Fest-Segen** (Gotteskraft & Götterkräfte) startet als Auftrag sofort ein Fest des
   Stamms, auch im Krieg oder Hunger; läuft schon eines, verlängert er es um 150 Schritte.
   Das Fest trägt dann die Eingriffs-ID als Ursache (Simulationskern Regel 15).
9. Ein Fest findet am **Festplatz** des Stamms statt (ein Ort von Welt & Insel, Regel 17
   dort). Hat der Stamm keinen, legt dieses System ihn beim ersten Fest als Auftrag an der
   Mitte des Stamms an. Ein Fest dauert **300 Schritte** (bei 1× 30 s). Der **Festbereich**
   sind die Zellen im Umkreis von 2 Zellen um den Festplatz.
10. Bei Festbeginn entscheidet jeder verfügbare Einzelne des Stamms einmal, ob er hingeht
    (mit mehr Geselligkeit eher), und verfügbare Einzelne anderer Stämme im Umkreis von
    8 Zellen kommen mit halber Wahrscheinlichkeit als **Gäste** (Formel 4). Gefangene gehen
    nie. Wer geht, bekommt von Menschen & Bevölkerung das Ziel Festbereich und bleibt dort
    bis zum Ende. Wer während des Fests verfügbar wird oder von der Gotteshand in den
    Festbereich gesetzt wird, nimmt teil, sobald er verfügbar ist (Zustimmungsmodell: erst
    nach T_erschüttert).
11. Auf dem Fest sucht jeder anwesende Verfügbare viel öfter eine Handlung als im Alltag
    (Formel 5), am Ort **Fest**: mit Geselligkeit eher eine **Gruppe** (3 bis
    N_gruppe_max = 4 Beteiligte, die er am meisten begehrt), sonst mit Lust eher Sex zu zweit
    als Zärtlichkeit.
12. Ein Fest **endet** nach seiner Dauer, wenn weniger als 2 Teilnehmer bleiben oder wenn
    der Stamm untergeht. Laufende Handlungen enden dabei nicht; sie laufen zu Ende.
13. **Auf dem Fest altern Menschen halb so schnell** (Entscheidung des Entwicklers vom
    2026-10-26): Die Schritte eines Einzelnen im Festbereich während eines laufenden Fests
    zählen für sein Lebensalter nur halb (Simulationskern Formel `lebensjahr`, Formel 7
    hier). Dieses System meldet Menschen & Bevölkerung Beginn und Ende der Anwesenheit als
    Auftrag.

**Handlungen**

14. Eine zugestimmte Handlung dauert je nach Art (Formel 6): Zärtlichkeit 20, Sex zu zweit
    40, Gruppe 60 Schritte. Sie endet nach ihrer Dauer, wenn ein Beteiligter sie beendet
    oder wenn das Handlungsregister sie wegen einer Sperre oder der Halteprüfung beendet
    (Zustimmungsmodell Regel 14). Nur eine Handlung, die ihre Dauer erreicht, gilt als
    **vollendet**.
15. Jeder Mensch nimmt höchstens an einer laufenden Handlung teil (Zustimmungsmodell, Edge
    Cases); Anfragen an Menschen in einer Handlung stellt dieses System nicht.

**Wirkungen in der Welt** (Pillar 3)

16. **Paarbindung**: Vollenden zwei Menschen ohne Bindung eine Handlung zu zweit
    (Zärtlichkeit oder Sex zu zweit), entsteht zwischen ihnen eine Bindung (Entscheidung des
    Entwicklers vom 2026-10-26). Jeder hat höchstens eine Bindung. Sie endet mit Tod oder
    Gefangennahme eines der beiden oder wenn beide 5 Lebensjahre lang keine Handlung
    miteinander vollendet haben. Für das Zustimmungsmodell gilt i als **gebunden** (k_i),
    solange seine Bindung besteht, und noch T_trauer lang nach Tod oder Gefangennahme des
    Partners (Zustimmungsmodell Regel 19). Bindung entsteht und vergeht als kleines Ereignis
    für die Chronik.
17. **Kulturwandel**: Jede vollendete Handlung erteilt Kultur & Erinnerung den Auftrag
    „Achse verschieben (Wert, Lust, Veranlasser)“, Veranlasser ist die Ursache des Fests,
    falls es ein gesegnetes war (Formel 8). Bis zur Kalibrierung ist das Gewicht 0.
18. **Zufriedenheit**: Dieses System führt je Stamm eine Zufriedenheit aus vollendeten
    Handlungen (Formel 8), die Stämme & Diplomatie lesen kann (etwa für die Abspaltung). Bis
    zur Kalibrierung ist ihr Gewicht dort 0.
19. **Diplomatie**: Eine vollendete Handlung zwischen Menschen verschiedener Stämme erteilt
    Stämme & Diplomatie den Auftrag „Groll mindern“ zwischen beiden Stämmen (Formel 8). Bis
    zur Kalibrierung ist das Gewicht 0.

**Reihenfolge im Schritt**

20. Dieses System läuft an seiner Stelle in der Reihenfolge des Kerns (nach Stämme &
    Diplomatie, vor Harem & Rituale) mit dem Zufallsstrom system_id 5. In einem Schritt:
    (1) Festprüfungen der Stämme in aufsteigender ID, falls der Schritt ein Vielfaches von
    10 ist; (2) Feste beenden, deren Dauer abgelaufen ist; (3) für jeden Einzelnen, dessen
    Personentakt in diesem Schritt liegt, in aufsteigender ID: Initiative im Alltag oder auf
    dem Fest; (4) vollendete Handlungen auswerten (Bindung, Wirkungen). Das Ende laufender
    Handlungen durch Sperren erledigt die Sperrenprüfung des Handlungsregisters in jedem
    Schritt (Simulationskern Formel 5).

### States and Transitions

| Zustand eines Einzelnen | Bedeutung | Wechsel |
|---|---|---|
| **Im Alltag** | verfügbar, kein Fest | → in Handlung (Zustimmung, `Beginne`) · → auf dem Weg zum Fest |
| **Auf dem Weg zum Fest** | Ziel Festbereich | → auf dem Fest (im Festbereich) · → im Alltag (Fest endet) |
| **Auf dem Fest** | im Festbereich, altert halb so schnell | → in Handlung · → im Alltag (Fest endet, nicht mehr verfügbar) |
| **In Handlung** | im Handlungsregister | → zurück, sobald die Handlung endet |

| Zustand eines Fests | Bedeutung | Wechsel |
|---|---|---|
| **Läuft** | 300 Schritte (+150 je Segen) | → beendet (Dauer, unter 2 Teilnehmer, Stamm untergegangen) |

| Zustand einer Bindung | Bedeutung | Wechsel |
|---|---|---|
| **Besteht** | k_i = gebunden für beide | → beendet (Tod, Gefangennahme, 5 Lebensjahre ohne gemeinsame Handlung) |
| **Trauer** | nach Tod oder Gefangennahme des Partners, T_trauer lang k_i = gebunden | → beendet |

### Interactions with Other Systems

| System | Was hineingeht | Was herauskommt | Wer besitzt die Schnittstelle |
|---|---|---|---|
| Zustimmungsmodell | Ergebnisse (Zustimmung, Gründe); Handlungsregister (wer in einer Handlung ist, Ende) | Anfragen per IDs; `Beginne`; b_ij und k_i als Funktionen für den Anfrage-Baustein | Zustimmungsmodell (Anfrage, Register), dieses System (b_ij, k_i) |
| Menschen & Bevölkerung | Einzelne mit Eigenschaften, Geschlecht, Ort, Zustand | Orientierung (Vorliebe), Aufträge „Ziel Festbereich“, „Fest betreten / verlassen“ (für das Altern) | Menschen |
| Kultur & Erinnerung | Normen (Offenheit Sex zu zweit), Tabus | Auftrag „Achse verschieben (Wert, Lust, Veranlasser)“, Gewicht 0 | Kultur |
| Welt & Insel | food_t, Hunger, Festplatz | Auftrag „Ort anlegen (Festplatz)“ | Welt |
| Stämme & Diplomatie | im Krieg, lebt | Zufriedenheit je Stamm (lesend), Auftrag „Groll mindern“, beide Gewicht 0 | dieses System (Zufriedenheit), Stämme (Groll) |
| Gotteskraft & Götterkräfte | Auftrag „Fest segnen (Stamm, Eingriffs-ID)“ | – | Gotteskraft (Kraft), dieses System (Fest) |
| Simulationskern | Personentakt, Zufallsstrom system_id 5, Ursache-Kette | Ereignisse: Fest beginnt / endet (mit Ursache), Bindung entsteht / endet, Handlung vollendet (klein); Formel `lebensjahr` mit Festschritten | Kern |
| Weltansicht & Kamera, Körper & Animation | – | Szenen (Handlungsart, Beteiligte, Ort, Beginn, Dauer), Feste mit Festbereich | Weltansicht (Darstellung, Zustimmungsmodell Regel 20) |
| Harem & Rituale | – | b_ij, Bindungen | dieses System |
| Chronik | – | Ereignisse | Kern-Ereignisformat |
| Speichern & Laden | kompletter Zustand | Orientierung je Einzelnem, laufende Feste (Stamm, Beginn, Ende, Ursache, Teilnehmer), Bindungen (Partner, Beginn, letzte gemeinsame Handlung, Trauer bis), Prüfsummen abgelehnter Anfragen, Zufriedenheit je Stamm | Speichern & Laden |

## Formulas

Alle Zufallszahlen, die nicht in Formel 1 stehen, kommen aus dem Strom system_id 5
(`NextDouble()` = u, Simulationskern Formel 6), in der Reihenfolge von Regel 20. Abstände
sind Chebyshev-Abstände in Zellen. Zeit in Schritten: 10 Schritte = 1 s bei 1×, 60 Schritte
= 1 Lebensjahr, Personentakt alle 2 Schritte (30 je Lebensjahr). Toleranz in Tests 1e-9.

### 1. Begehren

The Begehren formula is defined as:

`b_ij = o_i(g_j) · clamp(0,5 + σ_b · z_ij, 0, 1)` mit `z_ij = Σ_{k=1}^{12} v_k − 6`, wobei `v_1 … v_12` die ersten 12 Werte von `NextDouble` eines eigenen xoshiro256**-Stroms sind, gesät mit `SplitMix64(seed_sys5 ⊕ (ID_i · 2^32 + ID_j))`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| o_i(g) | int | 0 oder 1 | calculated | Orientierung: 1, wenn i das Geschlecht g begehrt; im MVP-0 immer 1 |
| σ_b | double | 0,2 | data file | Streuung des Begehrens (Annahme des Zustimmungsmodells) |
| z_ij | double | −6 bis 6 | calculated | Paar-Chemie, Irwin-Hall mit 12 Werten: Mittel 0, Streuung 1 |
| seed_sys5 | uint64 | – | calculated | Startwert des Stroms von system_id 5 (Simulationskern Formel 6) |
| ID_i, ID_j | int | 0 bis 2^32 − 1 | calculated | IDs der Einzelnen (Menschen & Bevölkerung); größere IDs brauchen eine andere Verknüpfung |

**Output Range:** 0 bis 1, Mittel 0,5, Streuung 0,2 (wie im Zustimmungsmodell angenommen);
etwa 0,6 % der Paare liegen bei 0 und ebenso viele bei 1. Weil b_ij ohne den Zufallsstrom
des Systems und ohne Zustand berechnet wird, ist es eine reine Funktion: dieselbe Welt,
dieselben IDs, derselbe Wert, in jeder Reihenfolge und nach jedem Laden.
**Example:** Ergeben die 12 Werte zusammen 7,0, ist z = 1,0 und b = 0,7; zusammen 4,0
ergibt z = −2,0 und b = 0,1.

### 2. Initiative im Alltag

The Alltag formula is defined as:

je Personentakt: Initiative, wenn `u < λ_A · L_i / 30` · Partner `j* = argmax_j b_ij` über die Verfügbaren mit Abstand ≤ 3 (Gleichstand: kleinere ID), nur `b_ij* > 0` · Handlungsart: Sex zu zweit, wenn `u < L_i`, sonst Zärtlichkeit · Ort: privat, wenn kein weiterer Einzelner mit Abstand ≤ 1 zu i oder j* steht, sonst öffentlich

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| λ_A | double | 0,1 / Lebensjahr | data file | Rate der Initiative im Alltag bei Lust 1 |
| L_i | double | 0–1 | calculated | Lust (Menschen & Bevölkerung) |
| u | double | 0–1 | calculated | erst der Wurf für die Initiative, dann (nur bei Initiative und Partner) der für die Art |

**Output Range:** Ein Mensch mit Lust 0,5 sucht etwa alle 20 Lebensjahre (2 min bei 1×)
eine Handlung, also etwa zweimal im Leben; 40 Einzelne zusammen etwa alle 3 s eine. Ohne
Partner in der Nähe wird nur der erste Wurf verbraucht.
**Example:** L_i = 0,6, u = 0,001 < 0,1 · 0,6 / 30 = 0,002 → Initiative; zwei Verfügbare in
der Nähe mit b = 0,4 und 0,7 → Partner ist der mit 0,7; u = 0,5 < 0,6 → Sex zu zweit.

### 3. Fest-Chance

The Fest formula is defined as:

alle 10 Schritte je Stamm: Fest beginnt, wenn `u < c_F · n_t · W_t` · `W_t = clamp(food_t / V_t, 0, 1)` (bei V_t = 0 gleich 0) · `n_t` = Offenheit der Kultur für Sex zu zweit · Wahrscheinlichkeit 0 im Krieg, im Hunger, bei laufendem Fest oder unter 2 Verfügbaren (dann kein Wurf)

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| c_F | double | 0,01 | data file | Fest-Häufigkeit |
| n_t | double | 0–1 | calculated | Kultur & Erinnerung, Norm Sex zu zweit |
| W_t | double | 0–1 | calculated | Wohlstand (Welt & Insel) |

**Output Range:** Eine satte Lust-Sekte (n = 0,9) feiert im Mittel alle 111 s, ein Stammesrat
(0,5) alle 200 s, ein Gottesstaat (0,1) alle 17 min, jeweils bei 1×; dazu kommen Feste aus
dem Fest-Segen. Eine Lust-Sekte feiert so etwa ein Fünftel der Zeit.
**Example:** n = 0,9, W = 1 → p = 0,009; mit u = 0,005 beginnt ein Fest.

### 4. Teilnahme

The Teilnahme formula is defined as:

bei Festbeginn je verfügbarem Einzelnen in aufsteigender ID: eigener Stamm `geht, wenn u < 0,3 + 0,6 · G_i`; Gast (anderer Stamm, Abstand zum Festplatz ≤ 8) `geht, wenn u < 0,5 · (0,3 + 0,6 · G_i)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| G_i | double | 0–1 | calculated | Geselligkeit |
| Gastfaktor | double | 0,5 | data file | wie oft Nachbarn mitfeiern |

**Output Range:** Vom eigenen Stamm gehen 30 % (G = 0) bis 90 % (G = 1), im Mittel 60 %; Gäste
halb so oft.
**Example:** G = 0,5 → eigener Stamm 0,6, Gast 0,3.

### 5. Initiative auf dem Fest

The Fest-Initiative formula is defined as:

je Personentakt im Festbereich: Initiative, wenn `u < λ_F · L_i / 30` · Gruppe, wenn `u < 0,5 · G_i` und mindestens 2 weitere Verfügbare da sind: Größe `N = 3 + floor(u · (N_gruppe_max − 2))`, Beteiligte sind die N − 1 Verfügbaren mit dem größten b_ij (Gleichstand: kleinere ID) · sonst Sex zu zweit, wenn `u < L_i`, sonst Zärtlichkeit, mit dem am meisten Begehrten · Ort Fest

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| λ_F | double | 2,0 / Lebensjahr | data file | Rate der Initiative auf dem Fest bei Lust 1 (20-mal der Alltag) |
| N_gruppe_max | int | 4 | data file | Zustimmungsmodell, Registry `n_gruppe_max` |

**Output Range:** Ein Mensch mit Lust 0,5 sucht auf dem Fest etwa jedes Lebensjahr (6 s)
eine Handlung, also etwa fünfmal in einem Fest. Würfe je Initiative: Initiative, Gruppe ja
oder nein, dann Größe (nur bei Gruppe) oder Art.
**Example:** L = 0,5, G = 0,8: u = 0,01 < 0,033 → Initiative; u = 0,3 < 0,4 → Gruppe; u = 0,7 →
N = 3 + floor(1,4) = 4.

### 6. Dauer

The Dauer formula is defined as:

`T_Zärtlichkeit = 20`, `T_Sex = 40`, `T_Gruppe = 60`, `T_Fest = 300` Schritte; Fest-Segen bei laufendem Fest `+150`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| T_a | int | Schritte | data file | Dauer je Handlungsart (2, 4, 6 s bei 1×) |
| T_Fest | int | 300 | data file | Dauer eines Fests (30 s bei 1×, Entscheidung des Entwicklers) |

**Output Range:** Handlungen 2–6 s, Feste 30 s und mehr. Eine Handlung kann früher enden, nie
später.
**Example:** Ein Fest beginnt in Schritt 1.000, ein Segen in Schritt 1.200 verlängert es bis
Schritt 1.450.

### 7. Altern auf dem Fest

The Altern formula is defined as:

`fest_steps_i` = Zahl der Schritte, in denen i während eines laufenden Fests im Festbereich war · im Simulationskern: `age_Lj = floor(((t − t_birth) − held_steps − f_slow · fest_steps) / S_Lj)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| f_slow | double | 0,5 | data file | Anteil der Festzeit, der nicht altert (Entscheidung des Entwicklers) |
| fest_steps | int | ≥ 0 | calculated | Menschen & Bevölkerung, aus den Aufträgen „Fest betreten / verlassen“ |

**Output Range:** Ein ganzes Fest (300 Schritte) kostet 2,5 statt 5 Lebensjahre, also etwa 6 %
statt 12,5 % eines Lebens von 40 Lebensjahren.
**Example:** t − t_birth = 1.200, held_steps = 0, fest_steps = 300 → (1.200 − 150) / 60 = 17,5 →
17 Lebensjahre (ohne Fest 20).

### 8. Wirkungen

The Wirkungen formula is defined as:

je vollendeter Handlung: Kultur `Δx = w_K · (1, auf einem Fest; 0,5, sonst)` an jeden beteiligten Stamm, Grund Lust · Zufriedenheit `Z_t ← Z_t · 0,9 + Σ_Handlungen dieses Geschichtsjahrs / max(1, E_t)` einmal je Geschichtsjahr · Diplomatie je Handlung zwischen den Stämmen t und o: `Groll mindern (t gegen o und o gegen t, w_D)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| w_K | double | 0 bis zur Kalibrierung | data file | Kulturwirkung je Handlung; Kultur deckelt bei 0,01 pro Jahr |
| Z_t | double | ≥ 0 | calculated | Zufriedenheit des Stamms (vollendete Handlungen je Einzelnem, verblassend) |
| E_t | int | ≥ 0 | calculated | Einzelne des Stamms |
| w_D | double | 0 bis zur Kalibrierung | data file | Groll, den eine Handlung zwischen zwei Stämmen mindert |

**Output Range:** Bis zur Kalibrierung ändern Kultur und Diplomatie nichts; Z_t wird berechnet
und gemeldet, Stämme & Diplomatie liest sie mit Gewicht 0. Z_t strebt bei konstanter Rate r
Handlungen je Einzelnem und Jahr gegen 10 · r.
**Example:** Ein Stamm mit 20 Einzelnen und 4 vollendeten Handlungen in einem Jahr, vorher
Z = 1,0: Z = 0,9 + 0,2 = 1,1.

## Edge Cases

- **If kein Verfügbarer in der Nähe ist**: Die Initiative verbraucht nur ihren Wurf, und es
  passiert nichts.
- **If der am meisten Begehrte b = 0 hat** (zum Beispiel später eine Orientierung, die nicht
  passt): keine Anfrage.
- **If eine Anfrage abgelehnt wird**: Keine Folge außer der gemerkten Prüfsumme; die Chronik
  bekommt keine Meldung, die Anzeige kann den Grund zeigen (Zustimmungsmodell Regel 13).
- **If eine Gruppe nicht zustande kommt** (einer lehnt ab): Keine Handlung; der Initiator
  darf im selben Personentakt keine weitere Anfrage stellen (Zustimmungsmodell Edge Cases:
  danach eine neue mit weniger Beteiligten).
- **If ein Mensch in den Festbereich geworfen oder gesetzt wird**: Der Wurf ist keine Eingabe
  des Begehrens. Er ist erst T_erschüttert lang gesperrt und nimmt danach wie jeder
  Verfügbare teil, auch als Gast. Sein Begehren ist dasselbe wie ohne Wurf.
- **If ein Teilnehmer gefangen, verletzt, gepackt oder getötet wird**: Er ist nicht mehr
  verfügbar; seine laufende Handlung beendet das Handlungsregister im selben Schritt; das
  Altern zählt wieder normal (Auftrag „Fest verlassen“).
- **If der Stamm eines Fests in den Krieg zieht oder hungert**: Das laufende Fest läuft zu
  Ende; nur neue Feste beginnen nicht.
- **If zwei Stämme zugleich feiern**: Jeder Mensch nimmt an höchstens einem Fest teil; ein
  Gast wählt das erste, zu dem er nach Formel 4 geht (Stämme in aufsteigender ID).
- **If ein Fest-Segen einen Stamm ohne Verfügbare trifft**: Das Fest beginnt und endet im
  nächsten Schritt (unter 2 Teilnehmer); der Segen ist trotzdem ein Eingriff mit Ursache.
- **If eine Bindung entsteht, während einer der beiden schon gebunden ist**: Unmöglich, eine
  Bindung entsteht nur zwischen zwei Ungebundenen. Hatten zwei Gebundene trotzdem eine
  Handlung (die Bindungsstrafe hat nicht abgelehnt), entsteht keine neue Bindung.
- **If der Partner stirbt**: Die Bindung endet, k_i bleibt T_trauer lang „gebunden“; erst
  danach kann eine neue Bindung entstehen.
- **If ein Stamm untergeht, während er feiert**: Das Fest endet im selben Schritt; Gäste und
  Teilnehmer gehen in den Alltag.
- **If V_t = 0 oder food_t < 0 bei der Fest-Chance**: W_t = 0, kein Fest.

## Dependencies

**Hängt ab von:**

| System | Art | Was gebraucht wird | GDD |
|---|---|---|---|
| Zustimmungsmodell | hart | Anfrage-Baustein, `Prüfe`, Handlungsregister (`Beginne`, Ende, wer in einer Handlung ist) | ✓ `design/gdd/zustimmungsmodell.md` (Approved) |
| Kultur & Erinnerung | hart | Offenheit Sex zu zweit; Auftrag „Achse verschieben (Lust)“ | ✓ `design/gdd/kultur-und-erinnerung.md` (Approved) |
| Menschen & Bevölkerung | hart | Einzelne, Eigenschaften, Geschlecht, Ort, Zustände; Aufträge „Ziel“, „Fest betreten / verlassen“ | ✓ `design/gdd/menschen-und-bevoelkerung.md` (Approved) |
| Simulationskern | hart | Personentakt, Zufallsstrom 5, Ereignisse, Formel `lebensjahr` mit Festschritten | ✓ `design/gdd/simulationskern.md` (Approved) |
| Welt & Insel | hart | food_t, Hunger, Festplatz | ✓ `design/gdd/welt-und-insel.md` (Approved) |
| Stämme & Diplomatie | weich | im Krieg, lebt; liest Zufriedenheit, Auftrag „Groll mindern“ (Gewicht 0) | ✓ `design/gdd/staemme-und-diplomatie.md` (Approved) |
| Körper & Animation | weich | Animationen für einfache Szenen | ✗ noch keins |
| Gotteskraft & Götterkräfte | weich | Auftrag „Fest segnen“ | ✗ noch keins (Vertrag in Regel 8) |

**Pflichten aus dem Zustimmungsmodell**, die dieses System übernimmt (AC-14 hier): Handlungen
nur über `Beginne`; Anfragen nur über den Anfrage-Baustein; b_ij ist eine reine Funktion fester
Werte und wächst nie durch Platzieren mit der Gotteshand; k_i hält T_trauer nach Tod oder
Gefangennahme des Partners an; Hingabe, Eingriffe, Kräfte und Rang ändern b_ij und k_i nie;
dieselbe Anfrage erst wieder, wenn sich ein Wert geändert hat.

**Wird gebraucht von:** Zustimmungsmodell (b_ij, k_i), Harem & Rituale (b_ij, Bindungen),
Kultur & Erinnerung (Lust als Grund), Stämme & Diplomatie (Zufriedenheit, Groll mindern),
Weltansicht & Kamera (Szenen, Feste), Chronik, Speichern & Laden; später Paar-Animationen.

## Tuning Knobs

Die Wirkungen auf Kultur, Zufriedenheit und Diplomatie bleiben bis zur ersten
Kalibrierungsmessung bei Gewicht 0.

| Regler | Default | Sicherer Bereich | Wirkung |
|---|---|---|---|
| σ_b | 0,2 | 0,15–0,25 | wie verschieden Menschen einander begehren; ändert die Werte des Zustimmungsmodells (neu rechnen) |
| Orientierung | alle beide | Anteile je Geschlecht | wer wen begehren kann |
| λ_A | 0,1 / Lebensjahr | 0,02–0,3 | wie oft im Alltag etwas geschieht |
| Umkreis Alltag | 3 Zellen | 1–5 | wie weit Menschen im Alltag suchen |
| c_F | 0,01 | 0,003–0,03 | wie oft Feste entstehen |
| T_Fest, Segen-Verlängerung | 300; 150 Schritte | 150–600; 0–300 | wie lange Feste dauern |
| Teilnahme, Gastfaktor | 0,3 + 0,6 · G; 0,5 | –; 0–1 | wie voll Feste werden, wie oft Nachbarn kommen |
| λ_F | 2,0 / Lebensjahr | 0,5–4 | wie viel auf einem Fest geschieht |
| Gruppe | 0,5 · G | 0–1 | wie oft Gruppen entstehen |
| T_a | 20 / 40 / 60 Schritte | 10–120 | wie lange Szenen dauern |
| f_slow | 0,5 | 0–1 | wie viel langsamer auf dem Fest gealtert wird (1 = gar nicht) |
| Bindung endet nach | 5 Lebensjahren ohne gemeinsame Handlung | 2–20 | wie stabil Bindungen sind |
| w_K, w_D, Gewicht der Zufriedenheit | 0 | nach der Kalibrierung | Wirkungen von Lust in der Welt |

## Visual/Audio Requirements

Szenen sind einfach (Entwicklerentscheidung, Konzept MVP-0): eine Ausblende oder eine
einfache vorhandene Animation auf dem Grundkörper, keine Paar-Animationen, keine Physik.
Die Weltansicht zeigt eine Szene nur nach Zustimmungsmodell Regel 20; Gewalt und Sex
überschneiden sich nie. Ein Fest ist von Weitem erkennbar (Licht in warmem Amber, Konzept:
„Lust bekommt warmes Kerzenlicht-Amber“), der Festbereich als Ort. Die Art Bible und das GDD
Körper & Animation legen die Darstellung fest.

## Game Feel

Feste sollen sich wie ein Aufblühen anfühlen: Menschen strömen zum Festplatz, Licht und
Bewegung nehmen zu, Paare finden sich, und nach 30 s ebbt es ab. Der Alltag bleibt ruhig, mit
gelegentlichen Begegnungen, damit Feste herausstechen.

## UI Requirements

- **Einzelnen ansehen:** Bindung (mit wem, seit wann), auf dem Fest ja oder nein; das
  Begehren nie als Zahl, höchstens als Hinweis „begehrt X“ für die drei Höchsten (Vorschlag)
- **Stamm ansehen:** ob gerade ein Fest läuft, wie oft er feiert (Wort), Zufriedenheit als
  Wort
- **Meldungen** (über die Chronik): Fest beginnt (mit „weil du …“ bei einem Segen), Fest endet,
  Bindung entsteht oder endet

> **📌 UX Flag — Begehren & Lust**: This system has UI requirements. In Phase 4
> (Pre-Production), run `/ux-design` to create a UX spec for each screen or HUD
> element this system contributes to **before** writing epics. Stories that
> reference UI should cite `design/ux/[screen].md`, not the GDD directly.

## Cross-References

| Dokument | Was hier verwendet wird |
|---|---|
| `design/gdd/game-concept.md` | Pillar 3, nicht verhandelbare Grenzen, MVP-0 (einfache Szenen), Farbe der Lust |
| `design/gdd/zustimmungsmodell.md` | Regeln 7, 13–21, Formel 1 (b_ij, k_i, π), Formel 5 (Annahme b ~ 0,5 ± 0,2), Edge Cases, Pflichten der Aufrufer, AC-27 |
| `design/gdd/kultur-und-erinnerung.md` | Regeln 6, 14–17 |
| `design/gdd/menschen-und-bevoelkerung.md` | Regeln 4, 6, 18, 19, 21, 23 |
| `design/gdd/simulationskern.md` | Regel 15, Formeln 5, 6, `lebensjahr` |
| `design/gdd/welt-und-insel.md` | Regel 17 (Orte), Wohlstand |
| `design/gdd/staemme-und-diplomatie.md` | Kriegszustand, Groll |

## Acceptance Criteria

Testwurzel: `Assets/Tests/EditMode/BegehrenUndLust/`. Testhilfen wie bei den anderen
GDDs: `IRandomStream` (vorgegebene Würfe, scheitert bei Über- oder Unterverbrauch),
`IEventSink`, `IConsentStub` (Anfrage-Baustein und Register als Stub mit vorgegebenen
Ergebnissen und Aufrufprotokoll), `IPopulationSource` (Einzelne, Zustände, Orte, nimmt
Aufträge entgegen), `ICultureSource` (Normen, nimmt Aufträge entgegen), `IWorldSource`,
`ITribeSource`; reine Funktion `Begehren(seed, i, j, orientierung)`; Einstieg `RunStep(t)`.

- **AC-01 Formel 1, rein [Logic]** **GIVEN** world_seed 42, **THEN** ist `Begehren(i, j)` für 10.000 Paare bei zweimaligem Aufruf, in umgekehrter Reihenfolge und nach Speichern und Laden bitgleich, und es verbraucht keinen Wurf aus dem Strom des Systems; b_ij ≠ b_ji für mindestens 99 % der Paare.
- **AC-02 Formel 1, Verteilung [Logic]** **GIVEN** 100.000 Paare, **THEN** liegt das Mittel bei 0,5 ± 0,005, die Streuung bei 0,2 ± 0,005, und je 0,6 % ± 0,2 Punkte liegen bei 0 und bei 1; **GIVEN** Würfe mit Summe 7,0, **THEN** ist b = 0,7.
- **AC-03 Regel 1, Eingriffe ändern nichts [Logic, Property]** **GIVEN** zwei Welten mit gleichem Startwert, in einer werden 1.000-mal Menschen mit der Gotteshand nebeneinandergesetzt, gesegnet oder geblitzt, Hingabe und Rang geändert, **THEN** ist b_ij für alle lebenden Paare gleich (Zustimmungsmodell, Pflichten).
- **AC-04 Formel 2 [Logic]** **GIVEN** L = 0,6 und die Würfe 0,001 und 0,5, zwei Verfügbare im Abstand 2 mit b = 0,4 und 0,7, **THEN** fragt das System Sex zu zweit mit dem zweiten an, Ort privat; steht ein Dritter im Abstand 1, Ort öffentlich; mit dem Wurf 0,0021 keine Initiative; ohne Verfügbare in der Nähe wird genau ein Wurf verbraucht.
- **AC-05 Regel 4 [Logic]** **GIVEN** Kandidaten, die gefangen, verletzt, in Gottes Hand, erschüttert, im Kampf oder in einer Handlung sind, **THEN** werden sie nie angefragt und fragen nie an.
- **AC-06 Regel 5 [Logic]** **GIVEN** eine abgelehnte Anfrage, **WHEN** sich keiner ihrer Werte ändert, **THEN** stellt das System sie nicht erneut; ändert sich die Norm oder der Ort, wieder.
- **AC-07 Formel 3 [Logic]** **GIVEN** n = 0,9, W = 1, kein Krieg, kein Hunger, **THEN** beginnt mit dem Wurf 0,005 ein Fest, mit 0,009 nicht; im Krieg, im Hunger, bei laufendem Fest oder unter 2 Verfügbaren wird nicht gewürfelt; die Prüfung läuft nur in Schritten, die ein Vielfaches von 10 sind.
- **AC-08 Regeln 8, 9, Formel 6 [Logic]** **GIVEN** ein Fest-Segen (Eingriff 77) auf einen Stamm ohne Fest und ohne Festplatz, **THEN** beginnt ein Fest mit Ursache 77 und Ende in 300 Schritten, und Welt erhält „Ort anlegen (Festplatz)“ an der Mitte; ein zweiter Segen in Schritt 200 des Fests verlängert es um 150.
- **AC-09 Formel 4 [Logic]** **GIVEN** Festbeginn, ein Einzelner mit G = 0,5 und der Wurf 0,59, **THEN** geht er (0,59 < 0,6), mit 0,61 nicht; als Gast im Abstand 8 mit 0,29 ja, mit 0,31 nicht, im Abstand 9 gar nicht; Gefangene nie.
- **AC-10 Formel 5 [Logic]** **GIVEN** L = 0,5, G = 0,8 im Festbereich mit 3 Verfügbaren (b = 0,2, 0,9, 0,6) und die Würfe 0,01, 0,3, 0,7, **THEN** fragt das System eine Gruppe aus 4 an; mit 2 Verfügbaren und Gruppe-Wurf 0,3 eine Handlung zu zweit mit dem am meisten Begehrten; Ort Fest.
- **AC-11 Regel 14, vollendet [Logic]** **GIVEN** eine Handlung Sex zu zweit, **THEN** endet sie nach 40 Schritten als vollendet; beendet das Register sie vorher (Sperre), ist sie nicht vollendet, und es entsteht keine Bindung und keine Wirkung.
- **AC-12 Regel 16, Bindung [Logic]** **GIVEN** zwei Ungebundene vollenden Zärtlichkeit, **THEN** entsteht eine Bindung mit Ereignis; eine Gruppe erzeugt keine; ein schon Gebundener bekommt keine zweite; nach 5 Lebensjahren ohne gemeinsame Handlung endet sie; stirbt der Partner, liefert k_i noch T_trauer lang „gebunden“, danach nicht.
- **AC-13 Regel 13, Formel 7 [Integration, mit Simulationskern und Menschen]** **GIVEN** ein Einzelner mit t − t_birth = 1.200 Schritten, davon 300 im Festbereich eines laufenden Fests, **THEN** ist sein Alter 17 Lebensjahre (ohne Fest 20); Schritte auf dem Weg zum Fest zählen normal.
- **AC-14 Pflichten aus dem Zustimmungsmodell [Logic, Property]** **GIVEN** 100.000 zufällige Zustände, **THEN** beginnt jede Handlung über `Beginne` mit einem Ergebnis aus diesem Schritt, jede Anfrage kommt aus dem Anfrage-Baustein, kein Mensch steht in zwei laufenden Handlungen, und Hingabe, Eingriffe, Kräfte und Rang ändern b_ij und k_i nie (Zustimmungsmodell AC-27).
- **AC-15 Formel 8 [Logic]** **GIVEN** w_K = 0 und w_D = 0, **THEN** sind Kulturachse und Groll nach 1.000 vollendeten Handlungen bitgleich mit einem Lauf ohne sie; **GIVEN** eine Test-Config mit w_K = 0,001, **THEN** erhält Kultur je vollendeter Handlung auf dem Fest den Auftrag mit 0,001 und Veranlasser 77, wenn das Fest gesegnet war; Z_t eines Stamms mit 20 Einzelnen, 4 Handlungen und vorher 1,0 ist 1,1.
- **AC-16 Regel 20, Reihenfolge und Determinismus [Integration]** **GIVEN** derselbe Startwert auf demselben Build und Rechner, **WHEN** zwei Läufe 3.000 Schritte mit Festen rechnen, **THEN** ist der Zustands-Hash nach jedem Schritt gleich, und nach Speichern und Laden in Schritt 1.500 ebenfalls.
- **AC-17 Pillar 3 [Integration, ADVISORY bis zur ersten Messung]** **GIVEN** 100 Welten × 10 Ären ohne Eingriff, **THEN** feiert eine Lust-Sekte im Mittel 3- bis 10-mal so oft wie ein Gottesstaat, und Handlungen je Einzelnem und Lebensjahr liegen in der Lust-Sekte mindestens 3-mal so hoch wie im Gottesstaat; als Kennzahlen werden gemeldet: Feste je Ära, Anteil der Zeit mit Fest, Handlungen je Fest, Anteil gebundener Einzelner.
- **AC-18 Spielertest [Playtest, ADVISORY]** **GIVEN** 10 Tester segnen ein Fest, **WHEN** sie danach gefragt werden, ob sie eine Figur zu etwas zwingen konnten, **THEN** sagen mindestens 8 „nein, ich konnte sie nur zusammenbringen“, und mindestens 7 erkennen eine Bindung, die auf dem Fest entstand.

## Open Questions

| Frage | Wer klärt | Bis wann |
|---|---|---|
| Werte für Kulturwandel (w_K), Zufriedenheit und Diplomatie (w_D) | Entwickler, nach der Kalibrierung | nach der ersten Messung |
| Liest Stämme & Diplomatie die Zufriedenheit (etwa weniger Abspaltung), und gibt es den Auftrag „Groll mindern“? Beides heute nur als Vertrag mit Gewicht 0 | GDD Stämme & Diplomatie (Ergänzung) | mit den Werten |
| Andere Orientierungen als „beide“ (Anteile als Daten, vielleicht je Kultur) | Entwickler | nach dem ersten Playtest |
| Zeigt die Anzeige, wen jemand begehrt („begehrt X“), oder bleibt das verborgen? | UX-Spec Einzelnen ansehen | `/ux-design` |
| Wie viele Szenen sind auf einem Fest zugleich lesbar? Bei 20 Teilnehmern etwa 40 Handlungen je Fest | Weltansicht & Kamera, Playtest | erster Playtest |
| Brauchen Feste ein eigenes Ereignis für Gotteskraft (Zufluss) oder nur Rituale? | GDD Gotteskraft & Götterkräfte | mit dem GDD |
