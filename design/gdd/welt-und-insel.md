# Welt & Insel

> **Status**: Draft (Nachtarbeit 2026-10-25/26, ohne Freigabe des Entwicklers geschrieben; jede Entscheidung steht in `production/nachtarbeit-2026-10-26.md`)
> **Author**: Claude (autonom, mit Erlaubnis des Entwicklers)
> **Last Updated**: 2026-10-25
> **Last Verified**: 2026-10-25
> **Implements Pillar**: Pillar 1 (Die Welt lebt ohne dich), Pillar 2 (Jeder Eingriff hinterlässt eine Spur)

## Summary

Welt & Insel ist die Insel als Daten: ein Raster aus 40 × 28 Zellen mit Land und Wasser,
einer Fruchtbarkeit pro Landzelle, dem Gebiet jedes Stamms, seiner Tragfähigkeit und
Nahrung, Dürren und verbrannter Erde. Sie übernimmt die Inselregeln aus Prototyp 1
unverändert, damit dessen Messwerte gelten, und liefert den anderen Systemen Zellen,
Gebiete und Nahrung als feste Verträge.

> **Quick reference** — Layer: `Core` · Priority: `MVP` · Key deps: `Simulationskern`

## Overview

Welt & Insel beschreibt, worauf die Menschen leben. Die Insel ist ein Raster aus Zellen;
jede Zelle ist Wasser oder Land, und Land hat eine Fruchtbarkeit. Aus dem Welt-Startwert
entsteht die Insel immer gleich. Jeder Stamm beansprucht die Landzellen um seine Mitte,
je mehr Köpfe, desto weiter. Aus seinem Gebiet folgt seine Tragfähigkeit und daraus seine
Nahrung; reicht sie nicht, hungert er. Dürren senken die Tragfähigkeit der ganzen Insel
für einige Jahre, ein Blitz verbrennt Land für Jahrzehnte. So entsteht ohne den Spieler
Druck, der Stämme wachsen, hungern, streiten und sich abspalten lässt (Pillar 1), und
jeder Blitz bleibt als verbrannte Erde sichtbar und wirksam (Pillar 2). Welt & Insel ist
reine Simulation ohne Grafik; die Weltansicht baut aus ihren Daten die 3D-Insel.

## Player Fantasy

Der Spieler blickt auf eine Insel, die er lesen kann: grüne fruchtbare Täler, karge
Ränder, die Grenzen der Stämme, eine Dürre, die das Land fahl macht, und schwarze
Flecken, wo sein Zorn eingeschlagen hat. Er versteht, warum ein Stamm hungert, ohne eine
Tabelle zu öffnen.

## Detailed Design

### Core Rules

*Entwurf ohne Abstimmung mit `world-builder` und `systems-designer` (Nachtarbeit). Vor der Produktion prüfen.*

**Das Raster**

1. Die Insel ist ein Raster aus **W × H = 40 × 28 Zellen** (Karte von Prototyp 1). Jede
   Zelle hat die feste **Zellen-ID** `c = y · W + x` (0 bis 1.119). „Zellenreihenfolge“
   heißt überall aufsteigende Zellen-ID; Menschen & Bevölkerung zieht Orte in dieser
   Reihenfolge.
2. Jede Zelle ist **Wasser** oder **Land**. Land hat eine **Fruchtbarkeit** f_c zwischen
   0,1 und 1 und eine **Geländehöhe** h_c, die nur die Ansicht nutzt. Beides entsteht beim
   Erzeugen der Welt und ändert sich danach nicht.
3. Nachbarn einer Zelle sind die bis zu acht Zellen ringsum (Chebyshev-Abstand 1), in der
   festen Reihenfolge N, NO, O, SO, S, SW, W, NW. Abstände zwischen Zellen sind
   Chebyshev-Abstände, wenn es um Wege geht, und euklidische Abstände, wenn es um Gebiete
   geht (Formeln 2 und 3, wie in Prototyp 1).

**Erzeugen**

4. Beim Erzeugen der Welt (Simulationskern Regel 17, als erstes System) entsteht die
   Insel aus dem Zufallsstrom dieses Systems (system_id 1) nach Formel 1. Danach legt
   Welt & Insel die beiden **Startplätze** der ersten Stämme fest (Formel 2) und gibt
   ihnen die Stammes-IDs 0 und 1. Stämme & Diplomatie übernimmt diese Stämme beim eigenen
   Erzeugen; Menschen & Bevölkerung, das vor Stämme & Diplomatie erzeugt wird, nutzt die
   Startplätze und das erste Gebiet, um die ersten Einzelnen zu verteilen.
5. Eine von Hand gebaute Insel ist im MVP-0 nicht vorgesehen (Open Questions).

**Gebiet**

6. Jeder lebende Stamm hat eine **Mitte** (eine Landzelle, Eigentum von Stämme &
   Diplomatie) und ein **Gebiet**: die Landzellen, die ihm nach Formel 3 gehören. Eine
   Zelle gehört höchstens einem Stamm; Wasser gehört niemandem. Das Gebiet wächst mit dem
   Volk V_t.
7. Das Gebiet wird neu berechnet, wenn Welt & Insel in einem ungeraden Geschichtsjahr
   rechnet (alle 2 Jahre, wie in Prototyp 1), und sofort als **Auftrag** von Stämme &
   Diplomatie, wenn ein Stamm entsteht, abgespalten, erobert wird oder ausstirbt
   (Simulationskern Regel 4.2).

**Tragfähigkeit und Nahrung**

8. Die **Tragfähigkeit** cap_t eines Stamms folgt aus der Fruchtbarkeit seines Gebiets,
   verbrannter Erde und einer laufenden Dürre (Formel 4). Menschen & Bevölkerung liest
   cap_t für die Gnade.
9. Jeder Stamm hat einen **Nahrungsvorrat** food_t. Einmal pro Geschichtsjahr erntet er
   bis zu seiner Tragfähigkeit und verbraucht einen Teil pro Kopf (Formel 5). Fällt der
   Vorrat unter 0, herrscht **Hunger**: Welt & Insel erteilt Menschen & Bevölkerung den
   Auftrag „Stamm verliert k Köpfe“, und der Vorrat wird 0.
10. Krieg senkt die Ernte. Ob ein Stamm im Krieg ist, liest Welt & Insel bei Stämme &
    Diplomatie (Stand nach dem vorigen Schritt, Simulationskern Regel 4.2).

**Dürre und verbrannte Erde**

11. Eine **Dürre** betrifft die ganze Insel. Sie beginnt zufällig, dauert einige Jahre
    und senkt jede Tragfähigkeit (Formel 6). Es läuft höchstens eine Dürre zugleich.
12. Ein **Blitz** (Gotteskraft & Götterkräfte) verbrennt als Auftrag die Landzellen im
    3 × 3-Feld um sein Ziel für 40 Geschichtsjahre. Verbrannte Erde trägt halb so viel
    (Formel 7). Ein weiterer Blitz setzt die Frist neu, er verstärkt die Wirkung nicht.

**Abfragen**

13. Welt & Insel beantwortet ohne Zufall und ohne Zustand zu ändern: Ist c Land? Wem
    gehört c? Welche Zellen hat Stamm t (in Zellenreihenfolge)? Nächste Landzelle zu c?
    Nächste Zelle von Stamm t zu c? Beide „nächste“-Abfragen nehmen den kleinsten
    Chebyshev-Abstand, bei Gleichstand die kleinste Zellen-ID.

**Reihenfolge im Schritt**

14. Welt & Insel ist ein Jahressystem mit Versatz O_year = 0 (Simulationskern Formel 5)
    und rechnet in dieser Reihenfolge: Dürre (Formel 6), Fristen verbrannter Erde
    (Formel 7), in ungeraden Jahren das Gebiet (Formel 3), dann für jeden lebenden Stamm
    in aufsteigender Stammes-ID Tragfähigkeit, Ernte und Hunger (Formeln 4 und 5).

### States and Transitions

| Zustand der Insel | Bedeutung | Wechsel |
|---|---|---|
| **Normal** | keine Dürre, Dürrefaktor 1 | → Dürre mit Wahrscheinlichkeit p_drought pro Jahr |
| **Dürre** | Dürrefaktor 0,55, Restdauer 1 bis 10 Jahre | → Normal, sobald die Restdauer 0 erreicht |

| Zustand einer Landzelle | Bedeutung | Wechsel |
|---|---|---|
| **Frei** | gehört keinem Stamm | → Gebiet von t bei der nächsten Gebietsberechnung |
| **Gebiet von t** | gehört Stamm t | → anderes Gebiet oder frei bei der nächsten Gebietsberechnung |
| **Verbrannt** (Merkmal) | trägt halb so viel, Restfrist 1 bis 40 Jahre | → nicht verbrannt, sobald die Frist 0 erreicht |

### Interactions with Other Systems

| System | Was hineingeht | Was herauskommt | Wer besitzt die Schnittstelle |
|---|---|---|---|
| Simulationskern | Jahrestakt (O_year = 0), Zufallsstrom system_id 1, Erzeugen als erstes System | Ereignisse: Dürre beginnt, Dürre endet, Hungersnot, Land verbrannt | Kern |
| Menschen & Bevölkerung | V_t je Stamm (lesend) | Zellen, Gebiet je Stamm in Zellenreihenfolge, food_t, cap_t, nächste Landzelle, nächste Zelle eines Stamms, Startplätze; Auftrag „Verlust durch Hunger“ | Welt & Insel |
| Stämme & Diplomatie | Mitten der Stämme, Kriegszustand (lesend); Auftrag „Gebiet neu berechnen“ | Gebiet, Zellenzahl, cap_t und food_t je Stamm (für Gedränge, Abspaltung, Grenzstreit), Startplätze und IDs der ersten Stämme | Welt & Insel (Gebiet), Stämme & Diplomatie (Mitten) |
| Kultur & Erinnerung | – | Hunger in diesem Jahr je Stamm, Dürre aktiv, Wohlstand (food_t / V_t) | Welt & Insel |
| Gotteskraft & Götterkräfte | Auftrag „Brand“ (Zielzelle) | Ereignis „Land verbrannt“ | Gotteskraft (Kraft), Welt & Insel (Wirkung auf Zellen) |
| Simulationskern (Grenze zur Physik) | – | nächste Landzelle für ein Endereignis auf Wasser (Simulationskern Regel 9) | Welt & Insel |
| Weltansicht & Kamera | – | Raster, Höhe h_c, Fruchtbarkeit, Gebiete, verbrannte Zellen, Dürre | Welt & Insel |
| Chronik | – | Ereignisse mit Ursache (ein Brand nennt den Blitz) | Kern-Ereignisformat |
| Speichern & Laden | kompletter Zustand | Fruchtbarkeit und Höhe aller Zellen, Besitzer, Fristen verbrannter Erde, Dürre (Restdauer, Faktor), food_t je Stamm | Speichern & Laden |

## Formulas

Übernahme der Inselregeln aus `prototypes/weltengott-concept/prototype.html`
(`makeIsland`, `landNear`, `assignTerritory`, Klima- und Nahrungsteil von `step`), damit
die Messwerte aus Prototyp 1 gelten. Alle Zufallszahlen kommen aus dem Strom system_id 1
(`NextDouble()`, Simulationskern Formel 6), in der hier genannten Reihenfolge. Gerechnet
wird in double; „runden“ heißt `floor(x + 0,5)` wie `Math.round` im Prototyp.

### 1. Insel erzeugen

The Insel formula is defined as:

`base = 1 − sqrt(dx² + dy²)` mit `dx = (x − W/2) / (0,42 · W)`, `dy = (y − H/2) / (0,40 · H)` · `n = Σ_k h_k · exp(−((x − bx_k)² + (y − by_k)²) / (2 · r_k²))` · `land = base + 0,25 · n + (u₁ − 0,5) · 0,12` · Land, wenn `land > 0,08`, dann `f = clamp(0,25 + 0,75 · (0,5 · n + 0,6 · base) + (u₂ − 0,5) · 0,2, 0,1, 1)` und `h = land`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| W, H | int | 40, 28 | constant | Breite und Höhe des Rasters |
| bx_k, by_k, r_k, h_k | double | bx 6 bis W − 6, by 5 bis H − 5, r 3–9, h 0,3–0,8 | calculated | 9 Hügel, gewürfelt vor den Zellen: `bx = 6 + u · (W − 12)`, `by = 5 + u · (H − 10)`, `r = 3 + u · 6`, `h = 0,3 + u · 0,5`, je Hügel in dieser Reihenfolge |
| u₁, u₂ | double | 0–1 | calculated | je Zelle in Zellenreihenfolge; u₂ wird nur bei Land gezogen (wie im Prototyp) |
| base | double | ≤ 1 | calculated | Inselform, zur Mitte hin hoch |
| n | double | ≥ 0 | calculated | Summe der Hügel |
| f_c | double | 0,1–1 | calculated | Fruchtbarkeit einer Landzelle |
| h_c | double | > 0,08 | calculated | Geländehöhe einer Landzelle, nur für die Ansicht |

**Output Range:** Etwa 61 % der Zellen sind Land (in 300 Probeinseln 56–66 %, 5. bis 95.
Perzentil), die mittlere Fruchtbarkeit des Landes ist etwa 0,77 (Python-Nachbau des
Prototyps mit anderem Zufallsgenerator, 2026-10-25; die genauen Werte mit dem Kernstrom
misst AC-02).
**Example:** Eine Zelle in der Inselmitte hat base ≈ 1; mit einem Hügel darüber (n = 0,5)
und u₁ = u₂ = 0,5 ist land = 1,125, also Land mit f = clamp(0,25 + 0,75 · (0,25 + 0,6),
0,1, 1) = 0,8875.

### 2. Startplätze

The Startplatz formula is defined as:

`Startplatz(cx, cy)`: 120 Versuche mit `a = u · 2π`, `d = u · 3`, `x = runden(cx + cos(a) · d)`, `y = runden(cy + sin(a) · d)`; gültig, wenn `1 ≤ x < W − 1`, `1 ≤ y < H − 1` und Land; Wert `f_c − 0,3 · Σ_s max(0, 5 − |c − s|)` über schon gesetzte Startplätze s; der beste Wert gewinnt, bei Gleichstand der frühere Versuch

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| cx, cy | double | – | constant | Zielpunkte: Stamm 0 bei (0,34 · W, 0,5 · H) = (13,6; 14), Stamm 1 bei (0,66 · W, 0,5 · H) = (26,4; 14) |
| Ersatzplatz | Zelle | (10, 14) bzw. (30, 14) | constant | gilt, wenn kein Versuch gültig ist |

**Output Range:** zwei Landzellen nahe der Mitte der linken und der rechten Inselhälfte,
höchstens 3 Zellen vom Zielpunkt entfernt (oder die Ersatzplätze).
**Example:** Liegt der erste Startplatz bei (14, 14), bekommt ein Versuch für Stamm 1 bei
(17, 14) den Abzug 0,3 · (5 − 3) = 0,6 und verliert gegen fast jede Zelle weiter rechts.

### 3. Gebiet

The Gebiet formula is defined as:

`r_t = 2 + 0,75 · sqrt(V_t)` · Landzelle c gehört dem Stamm t mit dem kleinsten `q = |c − m_t| / r_t`, sofern `q < 1`; bei Gleichstand die kleinere Stammes-ID; sonst niemandem

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| m_t | Zelle | Landzelle | calculated | Mitte von Stamm t (Stämme & Diplomatie) |
| V_t | int | ≥ 0 | calculated | Volk (Menschen & Bevölkerung) |
| r_t | double | ≥ 2 | calculated | Reichweite in Zellen |
| \|c − m_t\| | double | ≥ 0 | calculated | euklidischer Abstand der Zellmittelpunkte |

**Output Range:** Bei V_t = 48 ist r_t = 7,2; ein Startstamm hat etwa 154 Landzellen
(142–161 in 300 Probeinseln). Ein ausgestorbener Stamm (V_t = 0) hat kein Gebiet.
**Example:** Zwei Stämme mit V = 48 und Mitten 13 Zellen auseinander: Eine Zelle 6 vom
einen und 7 vom anderen entfernt hat q = 0,83 gegen 0,97 und gehört dem ersten.

### 4. Tragfähigkeit

The Tragfähigkeit formula is defined as:

`cap_t = δ · Σ_{c ∈ Gebiet(t)} f_c · κ · (1 − 0,5 · verbrannt_c)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| κ | double | 0,55 | constant | Ernte je Fruchtbarkeit (Prototyp 1) |
| verbrannt_c | int | 0 oder 1 | calculated | Zelle ist verbrannt (Formel 7) |
| δ | double | 1 oder 0,55 | calculated | Dürrefaktor (Formel 6) |
| cap_t | double | ≥ 0 | calculated | Tragfähigkeit; Menschen & Bevölkerung nutzt sie für die Gnade |

**Output Range:** Ein Startstamm hat etwa 77 (66–86); bei einer Dürre etwa 42.
**Example:** 154 Zellen mit f = 0,77 im Mittel: 154 · 0,77 · 0,55 = 65,2; bei einer Dürre
35,9. Sind 9 davon verbrannt, fallen 9 · 0,77 · 0,55 · 0,5 = 1,9 weg.

### 5. Ernte und Hunger

The Nahrung formula is defined as:

`prod = min(cap_t, 1,25 · V_t) · (0,85, falls im Krieg, sonst 1)` · `food_t = min(food_t + prod − V_t, 3 · V_t)` · Hunger, falls `food_t < 0`: `k = max(1, runden(0,06 · V_t + 0,25 · (−food_t)))`, Auftrag „Verlust k“ an Menschen & Bevölkerung, danach `food_t = 0`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| food_t | double | 0 bis 3 · V_t nach dem Jahr | calculated | Vorrat; beim Erzeugen gleich V_t |
| prod | double | ≥ 0 | calculated | Ernte dieses Jahres |
| k | int | ≥ 1 | calculated | Hungertote; was das Volk nicht hergibt, meldet Menschen als „Verlust nicht vollständig“ |

**Output Range:** Ein Stamm, der seine Tragfähigkeit übersteigt, verliert pro Hungerjahr
etwa 6 % plus ein Viertel des Fehlbetrags. Der Vorrat deckt höchstens 3 Jahre Verbrauch.
**Example:** V = 60, cap = 42 (Dürre), Vorrat 10, kein Krieg: prod = 42, Vorrat
10 + 42 − 60 = −8, Hunger mit k = max(1, runden(3,6 + 2)) = 6, Vorrat 0.

### 6. Dürre

The Dürre formula is defined as:

`läuft eine Dürre`: `rest = rest − 1`, bei `rest = 0` endet sie (δ = 1) · `sonst`: Dürre beginnt, wenn `u < p_drought`; dann `rest = 4 + NextInt(0, 7)` und `δ = 0,55`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| p_drought | double | 0,022 | data file | Wahrscheinlichkeit pro Jahr ohne Dürre (Prototyp 1) |
| rest | int | 0–10 | calculated | verbleibende Jahre |
| δ | double | 1 oder 0,55 | calculated | Dürrefaktor |

**Output Range:** Eine Dürre dauert 4 bis 10 Jahre (Mittel 7). Auf lange Sicht herrscht
in etwa 13 % aller Jahre Dürre (0,022 · 7 / (1 + 0,022 · 7)), also im Mittel etwa alle
52 Jahre eine, rund alle 2 Ären.
**Example:** u = 0,01 → Dürre beginnt; NextInt ergibt 3 → sie dauert 7 Jahre.

### 7. Verbrannte Erde

The Brand formula is defined as:

`Brand(Ziel z)`: für jede Landzelle c mit Chebyshev-Abstand ≤ 1 zu z gilt `frist_c = T_burn`; jährlich `frist_c = frist_c − 1`; `verbrannt_c = (frist_c > 0)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| T_burn | int | 40 | data file | Jahre verbrannter Erde (Prototyp 1) |
| frist_c | int | 0–40 | calculated | Restfrist |

**Output Range:** Ein Blitz verbrennt bis zu 9 Landzellen für 40 Geschichtsjahre, bei 1×
also 20 s, und kostet einen Stamm bis zu 9 · f · 0,275 Tragfähigkeit (etwa 1,9 im Mittel).
**Example:** Blitz auf eine Küstenzelle mit 5 Landzellen im 3 × 3-Feld: Diese 5 werden
verbrannt, die 4 Wasserzellen bleiben Wasser.

### Kalibrierung gegen Prototyp 1

Insel, Startplätze, Gebiet, Tragfähigkeit, Ernte, Hunger, Dürre und Brand folgen dem
Prototyp. Zwei Abweichungen: Der Zufall kommt aus dem Kernstrom statt aus dem
Prototyp-Generator (die Inseln sind andere, ihre Verteilung gleich), und die Ernte
rechnet als eigenes Jahressystem vor Kultur & Erinnerung (im Prototyp lag der Verfall der
Erinnerung im selben Durchlauf davor; beides hängt nicht voneinander ab). Die Aussterbe-
und Ereignisraten aus Prototyp 1 misst AC-16 mit den Debug- und Balancing-Werkzeugen neu.

## Edge Cases

*Entwurf, Nachtarbeit. Vor der Produktion von `systems-designer` prüfen lassen.*

- **If die erzeugte Insel zu klein ist** (weniger als 400 Landzellen, außerhalb der
  Prototyp-Verteilung): Die Welt wird trotzdem verwendet, denn eine Neuerzeugung würde
  den Startwert umdeuten. Das Debug-Werkzeug meldet sie; AC-02 prüft, dass das bei den
  Defaults praktisch nie vorkommt.
- **If kein Versuch für einen Startplatz gültig ist**: Es gilt der Ersatzplatz. Ist der
  Ersatzplatz Wasser, gilt die nächste Landzelle zu ihm (Regel 13).
- **If zwei Stämme gleich weit von einer Zelle entfernt sind** (gleiches q): Die Zelle
  gehört dem mit der kleineren Stammes-ID.
- **If ein Stamm keine einzige Zelle bekommt** (eingekesselt, Mitte auf einer fremden
  Zelle): cap_t = 0, er hungert, bis er schrumpft, abwandert (Stämme & Diplomatie) oder
  ausstirbt. Seine Einzelnen gehen zur nächsten Zelle ihres Gebiets; hat er keins, bleiben
  sie stehen (Menschen & Bevölkerung Formel 9).
- **If ein Stamm im selben Jahr hungert und im Krieg ist**: Die Ernte ist um 15 %
  geringer, der Hunger kommt zum Kriegsverlust hinzu.
- **If der Hunger mehr Tote verlangt, als der Stamm hergeben kann** (Gehaltene zählen
  nicht): Menschen & Bevölkerung tötet, was möglich ist, und meldet den Rest als
  „Verlust nicht vollständig“.
- **If ein Blitz Wasser trifft**: Verbrannt werden nur die Landzellen im 3 × 3-Feld; trifft
  er nur Wasser, entsteht kein Ereignis „Land verbrannt“.
- **If während einer Dürre eine neue gewürfelt würde**: Es gibt keine zweite; der Wurf
  findet nur in Jahren ohne Dürre statt.
- **If ein Stamm ausstirbt**: Sein Gebiet wird sofort neu verteilt (Auftrag von Stämme &
  Diplomatie), sein Vorrat verfällt.
- **If ein Ergebnis der Physik auf Wasser landet**: Der Kern setzt den Menschen auf die
  nächste Landzelle (Simulationskern Regel 9); Welt & Insel liefert sie.

## Dependencies

**Hängt ab von:**

| System | Art | Was gebraucht wird | GDD |
|---|---|---|---|
| Simulationskern | hart | Jahrestakt O_year = 0, Zufallsstrom system_id 1, Erzeugen als erstes System, Aufträge | ✓ `design/gdd/simulationskern.md` |
| Menschen & Bevölkerung | hart, nur lesend | V_t je Stamm | ✓ `design/gdd/menschen-und-bevoelkerung.md` |
| Stämme & Diplomatie | hart, nur lesend | Mitten der Stämme, Kriegszustand | ✗ noch keins (vorläufiger Vertrag in diesem GDD) |

Menschen & Bevölkerung und Stämme & Diplomatie hängen ihrerseits von Welt & Insel ab.
Der Kreis ist über die Reihenfolge im Schritt gelöst: Welt & Insel rechnet zuerst und
liest V_t und den Kriegszustand nach dem vorigen Schritt; die anderen lesen Gebiet, cap_t
und food_t desselben Jahres.

**Wird gebraucht von:** Menschen & Bevölkerung (Zellen, Gebiet, food_t, cap_t,
Startplätze, Hunger), Stämme & Diplomatie (Gebiet, Gedränge, Startstämme), Kultur &
Erinnerung (Hunger, Dürre, Wohlstand), Gotteskraft & Götterkräfte (Brand), Simulationskern
(nächste Landzelle), Weltansicht & Kamera, Chronik, Speichern & Laden. Ein GDD haben davon
der Simulationskern und Menschen & Bevölkerung; beide nennen Welt & Insel bereits.

## Tuning Knobs

| Regler | Default | Sicherer Bereich | Wirkung |
|---|---|---|---|
| W × H | 40 × 28 | fest bis zur Messung | Größe der Insel; größer heißt mehr Platz und weniger Streit |
| κ | 0,55 | fest (Prototyp 1) | Ernte je Fruchtbarkeit |
| Reichweite r_t = a + b · sqrt(V) | a = 2, b = 0,75 | fest (Prototyp 1) | wie schnell ein Gebiet mit dem Volk wächst |
| Ernte-Deckel | 1,25 · V | fest (Prototyp 1) | höchste Ernte pro Kopf |
| Vorrat-Deckel | 3 · V | 1–5 · V | wie viele Jahre ein Vorrat überbrückt |
| Kriegsabzug | 0,85 | 0,7–1 | wie sehr Krieg die Ernte senkt |
| Hunger | 0,06 · V + 0,25 · Fehlbetrag | fest (Prototyp 1) | Tote je Hungerjahr |
| p_drought, Dauer, δ | 0,022; 4–10 Jahre; 0,55 | 0,01–0,05; –; 0,4–0,8 | wie oft und wie hart Dürren sind |
| T_burn, Abzug | 40 Jahre; halbe Ernte | 10–80 Jahre | wie lange ein Blitz im Land sichtbar und wirksam bleibt |

„Fest (Prototyp 1)“ heißt: Eine Änderung bricht die Kalibrierung, die Debug-Werkzeuge
müssen dann neu messen.

## Visual/Audio Requirements

Keine eigenen. Die Weltansicht baut die Insel aus h_c und f_c (Höhe und Grün), zeigt
Grenzen der Gebiete, verbrannte Erde (dunkel, mit der Frist verblassend) und eine Dürre
(fahles Land). Verbrannte Erde muss aus der normalen Zoomstufe sofort erkennbar sein
(Pillar 2).

## Game Feel

Die Insel soll sich wie ein Ort anfühlen, nicht wie ein Spielbrett: Stämme wachsen
sichtbar in ihr Land hinein, eine Dürre legt sich über alles, und ein Blitz hinterlässt
eine Narbe, die man noch zwei Ären später sieht.

## UI Requirements

- **Zelle ansehen** (auch in der Pause): Land oder Wasser, Fruchtbarkeit als Wort
  (karg, mittel, fruchtbar), Besitzer, verbrannt mit Restfrist
- **Stamm ansehen:** Gebietsgröße, Tragfähigkeit und Vorrat als einfache Anzeige („satt“,
  „knapp“, „hungert“), nie als Rohzahl
- **Meldungen** (über die Chronik): Dürre beginnt und endet, Hungersnot, Land verbrannt

> **📌 UX Flag — Welt & Insel**: This system has UI requirements. In Phase 4
> (Pre-Production), run `/ux-design` to create a UX spec for each screen or HUD
> element this system contributes to **before** writing epics. Stories that
> reference UI should cite `design/ux/[screen].md`, not the GDD directly.

## Cross-References

| Dokument | Was hier verwendet wird |
|---|---|
| `prototypes/weltengott-concept/prototype.html` | `makeIsland`, `landNear`, `assignTerritory`, Klima- und Nahrungsteil von `step`, Brand eines Blitzes |
| `prototypes/weltengott-concept/REPORT.md` | Kalibrierung: 0 % Aussterben, Ereignisraten |
| `design/gdd/simulationskern.md` | Regel 4 (Reihenfolge, Aufträge), 9 (Physikergebnis auf Wasser), 17 (Erzeugen), Formel 5 (O_year = 0), Formel 6 (Strom 1) |
| `design/gdd/menschen-und-bevoelkerung.md` | Verträge für food_t, cap_t, Gebiet, Zellenreihenfolge, Orte, Verlust als Auftrag |
| `design/gdd/systems-index.md` | Abhängigkeiten |

## Acceptance Criteria

Entwurf (Nachtarbeit), noch nicht vom `qa-lead` geprüft. Testwurzel:
`Assets/Tests/EditMode/WeltUndInsel/`.

- **AC-01 Formel 1 [Logic/EditMode]** **GIVEN** world_seed 42, **WHEN** die Insel zweimal erzeugt wird, **THEN** sind Land, f_c und h_c aller 1.120 Zellen bitgleich, jede Landzelle hat 0,1 ≤ f_c ≤ 1, und Zellen am Rand (x = 0, x = 39, y = 0, y = 27) sind Wasser.
- **AC-02 Formel 1, statistisch [Logic/EditMode]** **GIVEN** die Startwerte 0 bis 999, **WHEN** je eine Insel erzeugt wird, **THEN** liegt der mittlere Landanteil bei 61 % ± 4 Punkte, die mittlere Fruchtbarkeit bei 0,77 ± 0,05, und höchstens 1 % der Inseln hat weniger als 400 Landzellen.
- **AC-03 Formel 2 [Logic/EditMode]** **GIVEN** 1.000 Startwerte, **WHEN** die Startplätze gesetzt werden, **THEN** sind beide Land, höchstens 3 Zellen (Chebyshev) vom Zielpunkt oder der Ersatzplatz, nie dieselbe Zelle, und die Stämme haben die IDs 0 und 1.
- **AC-04 Formel 3 [Logic/EditMode]** **GIVEN** zwei Stämme mit V = 48 und Mitten (14, 14) und (27, 14) auf reinem Land, **WHEN** das Gebiet berechnet wird, **THEN** gehört (20, 14) dem Stamm 0 (q = 6 / 7,2 gegen 7 / 7,2), keine Zelle zwei Stämmen, Wasser keinem, und bei gleichem q die kleinere ID.
- **AC-05 Formel 3, Takt [Logic/EditMode]** **GIVEN** S_year = 5, **WHEN** 20 Geschichtsjahre laufen, **THEN** wird das Gebiet genau in den ungeraden Jahren neu berechnet, und zusätzlich sofort im Schritt eines Auftrags „Gebiet neu berechnen“.
- **AC-06 Formel 4 [Logic/EditMode]** **GIVEN** 154 Zellen mit f = 0,77, davon 9 verbrannt, **WHEN** cap_t ohne und mit Dürre berechnet wird, **THEN** ist es 65,2 − 1,9 = 63,3 ± 0,1 und 34,8 ± 0,1.
- **AC-07 Formel 5 [Logic/EditMode]** **GIVEN** V = 60, cap = 42, food = 10, kein Krieg, **WHEN** das Jahr läuft, **THEN** erhält Menschen den Auftrag „Verlust 6“, food ist 0, und ein Ereignis „Hungersnot“ nennt Stamm und Zahl; **GIVEN** food würde 3 · V übersteigen, **THEN** ist food = 3 · V; im Krieg ist prod um 15 % kleiner.
- **AC-08 Formel 6 [Logic/EditMode]** **GIVEN** ein fester Startwert, **WHEN** 100.000 Geschichtsjahre laufen, **THEN** dauert jede Dürre 4 bis 10 Jahre, nie laufen zwei zugleich, und der Anteil der Dürrejahre liegt bei 13,3 % ± 1,5 Punkte.
- **AC-09 Formel 7 [Logic/EditMode]** **GIVEN** ein Blitz auf eine Zelle mit 5 Landzellen im 3 × 3-Feld, **WHEN** der Brand angewendet wird, **THEN** sind genau diese 5 für 40 Jahre verbrannt und danach nicht mehr; ein zweiter Blitz nach 30 Jahren setzt die Frist auf 40, ohne die Wirkung zu verdoppeln; ein Blitz nur auf Wasser verbrennt nichts und meldet nichts.
- **AC-10 Regel 13 [Logic/EditMode]** **GIVEN** eine Wasserzelle und ein Stamm, **WHEN** „nächste Landzelle“ und „nächste Zelle von Stamm t“ abgefragt werden, **THEN** ist das Ergebnis die Zelle mit dem kleinsten Chebyshev-Abstand, bei Gleichstand die kleinste ID, und der Zustand ist danach bitgleich (keine Wirkung, kein Zufall).
- **AC-11 Regel 14 [Logic/EditMode]** **GIVEN** Stub-Systeme, **WHEN** ein Jahr bei O_year = 0 läuft, **THEN** ist die Reihenfolge Dürre → Fristen → (in ungeraden Jahren) Gebiet → Tragfähigkeit, Ernte und Hunger je Stamm in aufsteigender ID, und Kultur & Erinnerung (Versatz 1) sowie Menschen (Versatz 2) sehen Hunger, food_t und cap_t desselben Jahres.
- **AC-12 Regel 4 [Logic/EditMode]** **GIVEN** ein Startwert, **WHEN** die Welt erzeugt wird, **THEN** stehen Insel, Startplätze und erstes Gebiet (mit V = 48 je Stamm) vor dem Erzeugen von Menschen & Bevölkerung bereit, und der Strom von Welt & Insel ist danach in einem bekannten Zustand (Hash).
- **AC-13 Speichern [Logic/EditMode]** **GIVEN** ein Zustand nach 300 Jahren mit laufender Dürre und verbrannter Erde, **WHEN** gespeichert und geladen wird, **THEN** ist der Zustands-Hash gleich und der Lauf geht bitgleich weiter.
- **AC-14 Edge Cases [Logic/EditMode]** **GIVEN** ein eingekesselter Stamm ohne Zellen, **WHEN** ein Jahr läuft, **THEN** ist cap_t = 0 und er hungert; **GIVEN** ein ausgestorbener Stamm, **THEN** wird sein Gebiet im selben Schritt neu verteilt.
- **AC-15 [Manual]** **GIVEN** eine Welt bei 1×, **WHEN** eine Dürre beginnt und ein Blitz einschlägt, **THEN** ist beides aus der normalen Zoomstufe sofort zu erkennen; Screenshots liegen in `production/qa/evidence/`.
- **AC-16 Kalibrierung [Integration/EditMode, ADVISORY bis zur ersten Messung]** **GIVEN** 100 Welten × 10 Ären ohne Eingriff mit den Debug-Werkzeugen, **WHEN** sie laufen, **THEN** stirbt keine Welt aus, und mindestens eine Hungersnot und eine Dürre treten in mindestens 80 % der Welten auf.

## Open Questions

| Frage | Wer klärt | Bis wann |
|---|---|---|
| Soll es im MVP-0 von Hand gebaute Inseln geben (gleiches Datenformat, aus einer Datei)? | Entwickler | vor dem Level-Design |
| Wem gehört die Mitte eines Stamms, und darf sie wandern? Dieser Entwurf liest sie nur | GDD Stämme & Diplomatie | mit dem GDD |
| Ist Wasser tödlich (Ertrinken nach einem Wurf ins Meer), oder setzt der Kern den Menschen immer an Land? | GDD Gotteshand & Ragdoll | mit dem GDD |
| Reicht ein Raster von 40 × 28 für die 3D-Ansicht, oder braucht die Ansicht ein feineres Gelände (dann nur Darstellung)? | GDD Weltansicht & Kamera | mit dem GDD |
| Fest-Segen hatte im Prototyp ein Leuchten auf der Karte; gehört das zur Welt oder nur zur Ansicht? Dieser Entwurf: nur Ansicht | GDD Gotteskraft & Götterkräfte | mit dem GDD |
