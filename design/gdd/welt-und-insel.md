# Welt & Insel

> **Status**: In Review, Review-Runde 2 überarbeitet (Nachtarbeit 2026-10-25/26, ohne Freigabe des Entwicklers; jede Entscheidung steht in `production/nachtarbeit-2026-10-26.md`)
> **Author**: Claude (autonom, mit Erlaubnis des Entwicklers)
> **Last Updated**: 2026-10-25
> **Last Verified**: 2026-10-25
> **Implements Pillar**: Pillar 1 (Die Welt lebt ohne dich), Pillar 2 (Jeder Eingriff hinterlässt eine Spur)

## Summary

Welt & Insel ist die Insel als Daten: ein Raster aus 40 × 28 Zellen mit Land und Wasser,
einer Fruchtbarkeit pro Landzelle, dem Gebiet jedes Stamms, seiner Tragfähigkeit und
Nahrung, Dürren, verbrannter Erde und einer Liste benannter Orte. Sie übernimmt die
Inselregeln aus Prototyp 1 genau, damit dessen Messwerte gelten, und liefert den anderen
Systemen Zellen, Gebiete, Nahrung und Orte als feste Verträge.

> **Quick reference** — Layer: `Core` · Priority: `MVP` · Key deps: `Simulationskern`

## Overview

Welt & Insel beschreibt, worauf die Menschen leben. Die Insel ist ein Raster aus Zellen;
jede Zelle ist Wasser oder Land, und Land hat eine Fruchtbarkeit. Aus dem Welt-Startwert
entsteht die Insel immer gleich. Jeder Stamm beansprucht die Landzellen um seine Mitte,
je mehr Köpfe, desto weiter. Aus seinem Gebiet folgt seine Tragfähigkeit und daraus seine
Nahrung; reicht sie nicht, hungert er, und die Chronik nennt den Grund. Dürren senken die
Tragfähigkeit der ganzen Insel für einige Jahre, ein Blitz verbrennt Land. So entsteht
ohne den Spieler Druck, der Stämme wachsen, hungern, streiten und sich abspalten lässt
(Pillar 1), und jeder Blitz bleibt als Brandnarbe dauerhaft auf der Insel und in der
Erinnerung der Kultur (Pillar 2). Welt & Insel ist reine Simulation ohne Grafik; die
Weltansicht baut aus ihren Daten die 3D-Insel.

## Player Fantasy

Der Spieler blickt auf eine Insel, die er lesen kann: ein grünes, fruchtbares Hochland in
der Mitte, karge Küsten, die Grenzen der Stämme, eine Dürre, die das Land fahl macht, und
dunkle Narben, wo sein Zorn eingeschlagen hat, auch Ären später noch. Er versteht, warum
ein Stamm hungert, ohne eine Tabelle zu öffnen.

## Detailed Design

### Core Rules

*Entwurf, überarbeitet nach Review-Runde 1 (`systems-designer`, `game-designer`, `qa-lead`, `world-builder`, `creative-director`).*

**Das Raster**

1. Die Insel ist ein Raster aus **W × H = 40 × 28 Zellen** (Karte von Prototyp 1). Jede
   Zelle hat die feste **Zellen-ID** `c = y · W + x` (0 bis 1.119). „Zellenreihenfolge“
   heißt überall aufsteigende Zellen-ID; Menschen & Bevölkerung zieht Orte in dieser
   Reihenfolge.
2. Jede Zelle ist **Wasser** oder **Land**. Die Zellen am Rand (x = 0, x = 39, y = 0,
   y = 27) sind immer Wasser, damit die Insel überall vom Meer umgeben ist. Land hat eine
   **Fruchtbarkeit** f_c zwischen 0,1 und 1 und eine **Geländehöhe** h_c, die nur die
   Ansicht nutzt. Beides entsteht beim Erzeugen der Welt und ändert sich danach nicht;
   wirksam ist `f_eff = f_c · Π Modifikatoren` (heute nur verbrannte Erde, Formel 4).
3. Nur die größte zusammenhängende Landmasse (Nachbarschaft über die acht Nachbarn) ist
   die **Hauptinsel**. Landzellen außerhalb davon gelten als Wasser (in 500 Probeinseln kam
   das nie vor). Wasser gehört niemandem und ist nicht begehbar.
4. Nachbarn einer Zelle sind die bis zu acht Zellen ringsum (Chebyshev-Abstand 1), in der
   festen Reihenfolge N, NO, O, SO, S, SW, W, NW. Abstände für Wege sind
   Chebyshev-Abstände, für Gebiete euklidische Abstände der Zellmitten (Formeln 2 und 3,
   wie in Prototyp 1).

**Erzeugen**

5. Beim Erzeugen der Welt (Simulationskern Regel 17, als erstes System) entsteht die
   Insel aus dem Zufallsstrom dieses Systems (system_id 1) nach Formel 1. Danach legt
   Welt & Insel die beiden **Startplätze** der ersten Stämme fest (Formel 2), gibt ihnen
   die Stammes-IDs 0 und 1, berechnet ihr erstes Gebiet mit dem Startvolk von 48 Köpfen
   (Registry `start_volk_per_tribe`) und gibt jedem einen Vorrat von 48. Stämme &
   Diplomatie übernimmt diese Stämme beim eigenen Erzeugen; Menschen & Bevölkerung, das
   vor Stämme & Diplomatie erzeugt wird, verteilt mit Startplätzen und Gebiet die ersten
   Einzelnen.
6. Eine von Hand gebaute Insel ist im MVP-0 nicht vorgesehen (Open Questions).

**Gebiet**

7. Jeder lebende Stamm hat eine **Mitte** (eine Landzelle, Eigentum von Stämme &
   Diplomatie) und ein **Gebiet**: die Landzellen, die ihm nach Formel 3 gehören. Seine
   Mitte gehört immer ihm. Eine Zelle gehört höchstens einem Stamm.
8. Ein Stamm mit weniger als 3 Köpfen gilt als ausgestorben (wie Prototyp 1, Menschen &
   Bevölkerung Regel 2) und beansprucht kein Land mehr. Das Aussterben erklärt Stämme &
   Diplomatie. Was mit den letzten 1–2 Menschen geschieht, ist offen (Open Questions).
9. Das Gebiet wird in jedem Geschichtsjahr neu berechnet, wenn Welt & Insel rechnet (wie
   in Prototyp 1, wo es am Ende jedes Jahres geschieht), und sofort als **Auftrag** von
   Stämme & Diplomatie, wenn ein Stamm entsteht, abgespalten, erobert wird oder ausstirbt
   (Simulationskern Regel 4.2). Jede Neuberechnung berechnet auch die Tragfähigkeit neu.

**Abspaltung**

10. Für eine Abspaltung sucht Stämme & Diplomatie den Platz der neuen Mitte mit der
    Platzsuche aus Formel 2 und seinem eigenen Zufallsstrom (system_id 4); Welt & Insel
    liefert dafür Land und Fruchtbarkeit. Findet die Suche keinen Platz, liegt er auf der
    Mitte eines anderen Stamms oder bekäme der neue Stamm keine Zelle, findet die
    Abspaltung nicht statt. Der neue Stamm bekommt den Vorrat `0,5 · Teil`; der alte Stamm
    behält seinen ganzen Vorrat (Prototyp 1).

**Tragfähigkeit und Nahrung**

11. Die **Tragfähigkeit** cap_t eines Stamms folgt aus der wirksamen Fruchtbarkeit seines
    Gebiets und einer laufenden Dürre (Formel 4). Menschen & Bevölkerung liest cap_t für
    die Gnade.
12. Jeder Stamm hat einen **Vorrat** food_t. Einmal pro Geschichtsjahr erntet er bis zu
    seiner Tragfähigkeit und verbraucht einen Teil pro Kopf (Formel 5). Fällt der Vorrat
    unter 0, herrscht **Hunger**: Welt & Insel erteilt Menschen & Bevölkerung den Auftrag
    „Stamm verliert k Köpfe“, und der Vorrat wird 0. Das Ereignis „Hungersnot“ nennt den
    Hauptgrund (Formel 5) und erscheint in der Chronik nur, wenn die letzte Hungersnot
    dieses Stamms mehr als 6 Jahre zurückliegt (wie in Prototyp 1).
13. Krieg senkt die Ernte. Ob ein Stamm im Krieg ist, liest Welt & Insel bei Stämme &
    Diplomatie (Stand nach dem vorigen Schritt, Simulationskern Regel 4.2).
14. Der Fest-Segen (Gotteskraft & Götterkräfte) erhöht den Vorrat eines Stamms als
    Auftrag um `0,6 · V_t`, gerechnet mit dem Volk **nach** dem Geschenk an Menschen
    desselben Segens (Prototyp 1: erst `pop += gift`, dann `food += pop · 0,6`).
    Gotteskraft erteilt darum zuerst den Auftrag an Menschen & Bevölkerung, dann diesen.

**Dürre und verbrannte Erde**

15. Eine **Dürre** betrifft die ganze Insel. Sie beginnt zufällig, dauert einige Jahre
    und senkt jede Tragfähigkeit (Formel 6). Es läuft höchstens eine Dürre zugleich.
16. Ein **Blitz** (Gotteskraft & Götterkräfte) verbrennt als Auftrag die Landzellen im
    3 × 3-Feld um sein Ziel (nur Zellen im Raster). Verbrannte Erde trägt 39 Ernten lang
    halb so viel (Formel 7). Ein weiterer Blitz setzt die Frist neu, er verstärkt die
    Wirkung nicht. Unabhängig davon speichert jede Zelle für immer das Jahr und die
    Ursache (Ereignis-ID des Blitzes) ihres letzten Brands, damit die Ansicht eine alte
    Narbe zeigen und die Chronik und Kultur & Erinnerung sie nennen können.

**Orte**

17. Welt & Insel führt eine Liste benannter **Orte**: `{ort_id, zelle, art, stamm, jahr,
    ursache}`. Die ID steigt und wird nie wiederverwendet. Arten im MVP-0: **Siedlung**
    (die Mitte eines Stamms, entsteht mit ihm), **Ritualplatz** (anfangs die Mitte),
    **Festplatz**, **Brandstelle** (ein Blitz, der mindestens eine Landzelle verbrennt),
    **Ruine** (die Mitte eines ausgestorbenen oder eroberten Stamms). Festplatz und
    Ritualplatz legen Begehren & Lust und Harem & Rituale als Auftrag an; die Namen der
    Orte kommen aus der Chronik. Die Orte ändern keine Regel dieses Systems; sie sind
    Anker für Rituale, Feste, Chronik und Erinnerung.

**Abfragen**

18. Welt & Insel beantwortet ohne Zufall und ohne Zustand zu ändern: Ist c Land? Wem
    gehört c? Welche Zellen hat Stamm t (in Zellenreihenfolge)? Nächste Landzelle zu c
    (auch für einen Punkt außerhalb des Rasters)? Nächste Zelle von Stamm t zu c? Beide
    „nächste“-Abfragen nehmen den kleinsten Chebyshev-Abstand, bei Gleichstand die
    kleinste Zellen-ID; hat Stamm t keine Zelle, ist die Antwort „keine“.

**Reihenfolge im Schritt**

19. Welt & Insel ist ein Jahressystem mit Versatz O_year = 0 (Simulationskern Formel 5)
    und rechnet in dieser Reihenfolge: Dürre (Formel 6), Fristen verbrannter Erde
    (Formel 7), Gebiet und Tragfähigkeit (Formeln 3 und 4), dann für jeden lebenden Stamm
    in aufsteigender Stammes-ID Ernte und Hunger (Formel 5).

### States and Transitions

| Zustand der Insel | Bedeutung | Wechsel |
|---|---|---|
| **Normal** | keine Dürre, Dürrefaktor 1 | → Dürre mit Wahrscheinlichkeit p_drought pro Jahr |
| **Dürre** | Dürrefaktor 0,55, Restdauer 1 bis 10 Jahre | → Normal, sobald die Restdauer 0 erreicht |

| Zustand einer Landzelle | Bedeutung | Wechsel |
|---|---|---|
| **Frei** | gehört keinem Stamm | → Gebiet von t bei der nächsten Gebietsberechnung |
| **Gebiet von t** | gehört Stamm t | → anderes Gebiet oder frei bei der nächsten Gebietsberechnung |
| **Verbrannt** (Merkmal) | trägt halb so viel, Restfrist 1 bis 40 | → nicht verbrannt, sobald die Frist 0 erreicht; Jahr und Ursache des Brands bleiben gespeichert |

### Interactions with Other Systems

| System | Was hineingeht | Was herauskommt | Wer besitzt die Schnittstelle |
|---|---|---|---|
| Simulationskern | Jahrestakt (O_year = 0), Zufallsstrom system_id 1, Erzeugen als erstes System | Ereignisse: Dürre beginnt, Dürre endet, Hungersnot (mit Hauptgrund), Land verbrannt | Kern |
| Menschen & Bevölkerung | V_t je Stamm (lesend) | Zellen, Gebiet je Stamm in Zellenreihenfolge, food_t, cap_t, nächste Landzelle, nächste Zelle eines Stamms, Startplätze; Auftrag „Verlust durch Hunger“ | Welt & Insel |
| Stämme & Diplomatie | Mitten der Stämme, Kriegszustand (lesend); Aufträge „Gebiet neu berechnen“, „Stamm entsteht mit Vorrat x“ | Gebiet, Zellenzahl, cap_t und food_t je Stamm (für Gedränge, Abspaltung, Grenzstreit), Startplätze und IDs der ersten Stämme, Land und Fruchtbarkeit für die Platzsuche | Welt & Insel (Gebiet), Stämme & Diplomatie (Mitten, Aussterben) |
| Kultur & Erinnerung | – | Hunger in diesem Jahr je Stamm mit Hauptgrund, Dürre aktiv, Wohlstand (food_t / V_t, bei V_t = 0 gleich 0), verbrannte Zellen je Gebiet mit Ursache | Welt & Insel |
| Gotteskraft & Götterkräfte | Aufträge „Brand“ (Zielzelle) und „Vorrat + 0,6 · V_t“ (Fest-Segen) | Ereignis „Land verbrannt“ mit den betroffenen Stämmen und Zellenzahlen | Gotteskraft (Kraft), Welt & Insel (Wirkung auf Zellen) |
| Begehren & Lust, Harem & Rituale | Aufträge „Ort anlegen“ (Festplatz, Ritualplatz) | Orte mit ID und Zelle | Welt & Insel |
| Simulationskern (Grenze zur Physik) | – | nächste Landzelle für ein Endereignis auf Wasser (Simulationskern Regel 9) | Welt & Insel |
| Weltansicht & Kamera | – | Raster, Höhe h_c, Fruchtbarkeit, Gebiete, verbrannte Zellen samt Jahr des Brands, Dürre, Orte | Welt & Insel |
| Chronik | – | Ereignisse mit Ursache (ein Brand nennt den Blitz, eine Hungersnot ihren Hauptgrund), Orte | Kern-Ereignisformat |
| Speichern & Laden | kompletter Zustand | f_c und h_c aller Zellen, Besitzer, Fristen, Jahr und Ursache des letzten Brands je Zelle, Restdauer der Dürre (der Faktor folgt daraus), food_t, Hunger in diesem Jahr, k und Jahr der letzten Hungersnot je Stamm, die Orte | Speichern & Laden |

## Formulas

Übernahme der Inselregeln aus `prototypes/weltengott-concept/prototype.html`
(`makeIsland`, `landNear`, `assignTerritory`, Klima- und Nahrungsteil von `step`,
Abspaltung, Fest-Segen und Blitz), damit die Messwerte aus Prototyp 1 gelten. Alle
Zufallszahlen kommen aus dem Strom system_id 1 (`NextDouble()`, Simulationskern Formel 6),
in der hier genannten Reihenfolge. Gerechnet wird in double, gespeichert wird f_c als
float wie im Prototyp (Float32); „runden“ heißt `floor(x + 0,5)` wie `Math.round` im
Prototyp, nicht das Runden auf die gerade Zahl, das C# als Voreinstellung hat.
Vergleiche abgeleiteter Werte in Tests haben die Toleranz 1e-9.

### 1. Insel erzeugen

The Insel formula is defined as:

`base = 1 − sqrt(dx² + dy²)` mit `dx = (x − W/2) / (0,42 · W)`, `dy = (y − H/2) / (0,40 · H)` · `n = Σ_k h_k · exp(−((x − bx_k)² + (y − by_k)²) / (2 · r_k²))` · `land = base + 0,25 · n + (u₁ − 0,5) · 0,12` · Land, wenn `land > 0,08`, dann `f = clamp(0,25 + 0,75 · (0,5 · n + 0,6 · base) + (u₂ − 0,5) · 0,2, 0,1, 1)` und `h = land`; danach Randzellen und Land außerhalb der Hauptinsel auf Wasser

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| W, H | int | 40, 28 | constant | Breite und Höhe des Rasters |
| bx_k, by_k, r_k, h_k | double | bx 6 bis W − 6, by 5 bis H − 5, r 3–9, h 0,3–0,8 | calculated | 9 Hügel, gewürfelt vor den Zellen: `bx = 6 + u · (W − 12)`, `by = 5 + u · (H − 10)`, `r = 3 + u · 6`, `h = 0,3 + u · 0,5`, je Hügel in dieser Reihenfolge |
| u₁, u₂ | double | 0–1 | calculated | je Zelle in Zellenreihenfolge; u₂ wird nur bei Land gezogen (wie im Prototyp), auch am Rand, der danach Wasser wird; der Rand ändert also keinen Wurf |
| base | double | ≤ 1 | calculated | Inselform, zur Mitte hin hoch |
| n | double | ≥ 0 | calculated | Summe der Hügel |
| f_c | float | 0,1–1 | calculated | Fruchtbarkeit einer Landzelle |
| h_c | double | > 0,08 | calculated | Geländehöhe einer Landzelle, nur für die Ansicht |

**Output Range:** Etwa 61 % der Zellen sind Land, die mittlere Fruchtbarkeit des Landes
ist etwa 0,77; Fruchtbarkeit und Höhe steigen gemeinsam zur Mitte und zu den Hügeln hin
(fruchtbares Hochland, karge Küste). Ohne die Randregel läge in 222 von 500 Probeinseln
Land am Rand; mit ihr ändert sich der Landanteil kaum (61,2 %). Land außerhalb der
Hauptinsel kam in 500 Probeinseln nie vor. (Python-Nachbau des Prototyps mit anderem
Zufallsgenerator, 2026-10-25; die Werte mit dem Kernstrom misst AC-02 zuerst.)
**Example:** Eine Zelle in der Inselmitte hat base ≈ 1; mit einem Hügel darüber (n = 0,5)
und u₁ = u₂ = 0,5 ist land = 1,125, also Land mit f = clamp(0,25 + 0,75 · (0,25 + 0,6),
0,1, 1) = 0,8875.

### 2. Platzsuche (Startplätze und Abspaltung)

The Platzsuche formula is defined as:

`Platz(cx, cy, minD, maxD)`: 120 Versuche, in jedem `a = u · 2π` und `d = minD + u · (maxD − minD)` (beide Würfe in jedem Versuch, auch wenn er ungültig wird), `x = runden(cx + cos(a) · d)`, `y = runden(cy + sin(a) · d)`; gültig, wenn `1 ≤ x < W − 1`, `1 ≤ y < H − 1` und Land; Wert `s = f_c − 0,3 · Σ_t max(0, 5 − |c − m_t|)` über die lebenden Stämme t; der Bestwert beginnt bei −1, ein Versuch gewinnt nur mit `s` größer als der bisherige Bestwert; gewinnt keiner, ist das Ergebnis „kein Platz“

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| cx, cy | double | – | calculated | Zielpunkt: Start von Stamm 0 bei (0,34 · W, 0,5 · H) = (13,6; 14), Start von Stamm 1 bei (26,4; 14); Abspaltung bei der Mitte des alten Stamms |
| minD, maxD | double | – | calculated | Start: 0 und 3; Abspaltung: r_t + 1 und r_t + 6 (r_t aus Formel 3) |
| Ersatzplatz | Zelle | (10, 14) bzw. (30, 14) | constant | nur beim Start: gilt, wenn kein Platz gefunden wird; ist er Wasser, die nächste Landzelle zu ihm |

**Output Range:** Beim Start liegen die Plätze höchstens 3 Zellen (Chebyshev) von (14, 14)
bzw. (26, 14), in x 11 bis 17 und 23 bis 29, also immer mindestens 6 Zellen auseinander.
Bei einer Abspaltung ist „kein Platz“ ein gültiges Ergebnis; dann findet sie nicht statt
(Regel 10).
**Example:** Der alte Stamm hat V = 64, also r_t = 8; gesucht wird 9 bis 14 Zellen von
seiner Mitte. Ein Versuch 3 Zellen neben einem dritten Stamm bekommt den Abzug
0,3 · (5 − 3) = 0,6.

### 3. Gebiet

The Gebiet formula is defined as:

`r_t = 2 + 0,75 · sqrt(V_t)` für Stämme mit `V_t ≥ 3` · Landzelle c gehört dem Stamm t mit dem kleinsten `q = |c − m_t| / r_t`, sofern `q < 1`; bei Gleichstand die kleinere Stammes-ID; die Mitte m_t gehört immer t; sonst niemandem

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| m_t | Zelle | Landzelle | calculated | Mitte von Stamm t (Stämme & Diplomatie) |
| V_t | int | ≥ 3 für lebende Stämme | calculated | Volk (Menschen & Bevölkerung) |
| r_t | double | ≥ 3,3 | calculated | Reichweite in Zellen |
| \|c − m_t\| | double | ≥ 0 | calculated | euklidischer Abstand der Zellmitten |

**Output Range:** Bei V_t = 48 ist r_t = 7,2; ein Startstamm hat etwa 154 Landzellen
(142–161 in 300 Probeinseln). Ein Stamm mit V_t < 3 hat kein Gebiet.
**Example:** Zwei Stämme mit V = 48 und Mitten (14, 14) und (27, 14): Die Zelle (20, 14)
hat q = 6 / 7,2 = 0,83 gegen 7 / 7,2 = 0,97 und gehört Stamm 0.

### 4. Tragfähigkeit

The Tragfähigkeit formula is defined as:

`cap_t = δ · Σ_{c ∈ Gebiet(t)} f_eff,c · κ` mit `f_eff,c = f_c · (0,5, falls verbrannt, sonst 1)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| κ | double | 0,55 | constant | Ernte je Fruchtbarkeit (Prototyp 1) |
| f_eff,c | double | 0,05–1 | calculated | wirksame Fruchtbarkeit; weitere Modifikatoren werden hier als Faktor angehängt |
| δ | double | 1 oder 0,55 | calculated | Dürrefaktor (Formel 6) |
| cap_t | double | ≥ 0 | calculated | Tragfähigkeit; Menschen & Bevölkerung nutzt sie für die Gnade |

**Output Range:** Ein Startstamm hat etwa 77 (66–86), weil das Land um die Mitte
fruchtbarer ist als der Inseldurchschnitt (f ≈ 0,91 statt 0,77); bei einer Dürre etwa 42.
**Example:** 154 Zellen mit f = 0,91: 154 · 0,91 · 0,55 = 77,08; sind 9 davon verbrannt,
fallen 9 · 0,91 · 0,55 · 0,5 = 2,25 weg (74,82); bei einer Dürre 41,15.

### 5. Ernte, Hunger und Hauptgrund

The Nahrung formula is defined as:

`prod = min(cap_t, 1,25 · V_t) · (0,85, falls im Krieg, sonst 1)` · `food_t = min(food_t + prod − V_t, 3 · V_t)` · Hunger, falls `food_t < 0`: `k = max(1, runden(0,06 · V_t + 0,25 · (−food_t)))`, Auftrag „Verlust k“ an Menschen & Bevölkerung, danach `food_t = 0` · **Hauptgrund:** `Δ_Dürre = prod(δ = 1) − prod`, `Δ_Brand = prod(ohne Brand) − prod`, `Δ_Krieg = prod(ohne Krieg) − prod`; der größte positive gewinnt (bei Gleichstand in dieser Reihenfolge), sind alle 0, ist der Grund **Gedränge**

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| food_t | double | 0 bis 3 · V_t nach der Ernte; ein Fest-Segen kann es bis zur nächsten Ernte darüber heben | calculated | Vorrat; Startstämme 48, ein abgespaltener Stamm 0,5 · Teil, Fest-Segen + 0,6 · V_t |
| prod | double | ≥ 0 | calculated | Ernte dieses Jahres |
| k | int | ≥ 1 | calculated | Hungertote; höchstens etwa 0,31 · V_t, was das Volk nicht hergibt (Gehaltene), meldet Menschen als „Verlust nicht vollständig“ |
| Hauptgrund | Aufzählung | Dürre, Brand, Krieg, Gedränge | calculated | wird nur berechnet und gemeldet, ändert kein Ergebnis; „Brand“ nennt die Ursache des jüngsten Brands im Gebiet |

**Output Range:** Ein Stamm, der seine Tragfähigkeit übersteigt, verliert pro Hungerjahr
etwa 6 % plus ein Viertel des Fehlbetrags. Der Vorrat deckt höchstens 3 Jahre Verbrauch.
**Example:** V = 60, cap = 42 wegen einer Dürre (ohne sie 76), Vorrat 10, kein Krieg:
prod = 42, Vorrat 10 + 42 − 60 = −8, Hunger mit k = max(1, runden(3,6 + 2)) = 6, Vorrat
0. Ohne Dürre wäre prod = 75, also Δ_Dürre = 33: Hauptgrund „Dürre“.

### 6. Dürre

The Dürre formula is defined as:

`läuft eine Dürre`: `rest = rest − 1`, bei `rest = 0` endet sie · `sonst`: Dürre beginnt, wenn `u < p_drought`; dann `rest = 4 + NextInt(0, 7)` · `δ = 0,55`, solange `rest > 0`, sonst 1

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| p_drought | double | 0,022 | data file | Wahrscheinlichkeit pro Jahr ohne Dürre (Prototyp 1) |
| rest | int | 0–10 | calculated | verbleibende Jahre; δ wird nicht gespeichert, sondern folgt daraus |
| δ | double | 1 oder 0,55 | calculated | Dürrefaktor |

**Output Range:** Eine Dürre dauert 4 bis 10 Jahre (Mittel 7). Auf lange Sicht herrscht
in etwa 13,3 % aller Jahre Dürre (0,022 · 7 / (1 + 0,022 · 7)), im Mittel etwa alle 52
Jahre eine. In einem Jahr mit laufender Dürre wird nicht gewürfelt.
**Example:** u = 0,01 → Dürre beginnt; NextInt ergibt 3 → sie dauert 7 Jahre und senkt
die Ernte schon in diesem Jahr.

### 7. Verbrannte Erde

The Brand formula is defined as:

`Brand(Ziel z, Ursache e)`: für jede Landzelle c im Raster mit Chebyshev-Abstand ≤ 1 zu z gilt `frist_c = T_burn`, `brand_jahr_c = Y`, `brand_ursache_c = e`; jährlich (Regel 19) `frist_c = frist_c − 1`; `verbrannt_c = (frist_c > 0)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| T_burn | int | 40 | data file | Frist verbrannter Erde (Prototyp 1) |
| frist_c | int | 0–40 | calculated | Restfrist |
| brand_jahr_c, brand_ursache_c | int | – | calculated | Jahr und Ereignis-ID des letzten Brands, bleiben für immer |

**Output Range:** Weil die Frist vor der Ernte sinkt, wirkt ein Brand auf genau 39
Ernten (bei 1× rund 20 s). Ein Blitz verbrennt bis zu 9 Landzellen und kostet einen Stamm
bis zu 9 · f · 0,275 Tragfähigkeit (etwa 2,3 im Startgebiet, rund 3 %). Die Narbe selbst
bleibt als Datum und Ursache dauerhaft.
**Example:** Blitz auf eine Küstenzelle mit 5 Landzellen im 3 × 3-Feld: Diese 5 werden
verbrannt, die 4 Wasserzellen bleiben Wasser. Ein Blitz auf (0, 0) betrifft nur die
Zellen im Raster: (0, 1) und (1, 0) liegen am Rand und sind Wasser, (1, 1) liegt innen und
ist nur in der Regel Wasser (base ≈ −0,62), nicht immer.

### Kalibrierung gegen Prototyp 1

Insel, Platzsuche, Gebiet (jedes Jahr), Tragfähigkeit, Ernte, Hunger, Dürre, Brand,
Vorrat bei Abspaltung und Fest-Segen und Aussterben unter 3 Köpfen folgen dem Prototyp.
Fünf Abweichungen:
1. Der Zufall kommt aus dem Kernstrom statt aus dem Prototyp-Generator; die Inseln sind
   andere, ihre Verteilung gleich.
2. Die Ernte rechnet als eigenes Jahressystem vor Kultur & Erinnerung; im Prototyp lag der
   Verfall der Erinnerung im selben Durchlauf davor, beides hängt nicht voneinander ab.
   Das Gebiet rechnet am Anfang des Jahres statt am Ende des vorigen; mit demselben Volk
   ist das Ergebnis gleich, nur ein Eingriff des Spielers zwischen zwei Jahren wirkt hier
   ein Jahr früher auf die Reichweite.
3. Der Rand ist immer Wasser; das betrifft wenige Randzellen und ändert keinen Wurf.
4. Eine Abspaltung auf die Mitte eines anderen Stamms findet nicht statt; im Prototyp
   entstand dann ein Stamm ohne Land, der verhungerte (selten, nur wenn kein besserer
   Versuch gelang).
5. Der Ersatzplatz beim Start wird auf Land geprüft; im Prototyp konnte er im Wasser
   liegen.

Die Regler gelten als „fest bis zur ersten AC-16-Messung“: Erst wenn der Nachbau die
Messwerte von Prototyp 1 wiedergibt, gibt es eine verlässliche Grundlage, an der jede
spätere Änderung gemessen wird (Open Questions).

## Edge Cases

- **If kein Startplatz gefunden wird**: Es gilt der Ersatzplatz; ist er Wasser, die
  nächste Landzelle zu ihm (Regel 18).
- **If zwei Stämme gleich weit von einer Zelle entfernt sind** (gleiches q): Die Zelle
  gehört dem mit der kleineren Stammes-ID.
- **If ein Stamm von Nachbarn eingekesselt wird**: Er behält mindestens seine Mitte;
  cap_t ist dann sehr klein, er hungert, bis er schrumpft, sich ausbreitet oder ausstirbt.
- **If eine Abspaltung auf der Mitte eines anderen Stamms landen würde oder der neue
  Stamm keine Zelle bekäme**: Die Abspaltung findet nicht statt (Regel 10), so entsteht
  nie ein Stamm ohne Gebiet und Menschen muss nie aus null Zellen würfeln.
- **If ein Stamm im selben Jahr hungert und im Krieg ist**: Die Ernte ist um 15 %
  geringer, der Hunger kommt zum Kriegsverlust hinzu.
- **If der Hunger mehr Tote verlangt, als der Stamm hergeben kann** (Gehaltene und
  Beteiligte laufender Handlungen zählen nicht): Menschen & Bevölkerung tötet, was möglich
  ist, und meldet den Rest als „Verlust nicht vollständig“.
- **If ein Blitz Wasser oder den Rand trifft**: Verbrannt werden nur Landzellen im Raster;
  trifft er keine Landzelle, entsteht weder ein Ereignis „Land verbrannt“ noch eine
  Brandstelle.
- **If während einer Dürre eine neue gewürfelt würde**: Es gibt keine zweite; der Wurf
  findet nur in Jahren ohne Dürre statt.
- **If ein Stamm unter 3 Köpfe fällt**: Er beansprucht im selben Schritt kein Land mehr,
  sein Vorrat verfällt, seine Mitte wird zur Ruine.
- **If ein Ergebnis der Physik auf Wasser oder außerhalb des Rasters landet**: Der Kern
  setzt den Menschen auf die nächste Landzelle (Simulationskern Regel 9); Welt & Insel
  liefert sie.
- **If V_t = 0 beim Wohlstand**: food_t / V_t gilt als 0, es wird nie durch 0 geteilt.

## Dependencies

**Hängt ab von:**

| System | Art | Was gebraucht wird | GDD |
|---|---|---|---|
| Simulationskern | hart | Jahrestakt O_year = 0, Zufallsstrom system_id 1, Erzeugen als erstes System, Aufträge | ✓ `design/gdd/simulationskern.md` |
| Menschen & Bevölkerung | hart, nur lesend | V_t je Stamm | ✓ `design/gdd/menschen-und-bevoelkerung.md` |
| Stämme & Diplomatie | hart, nur lesend | Mitten der Stämme, Kriegszustand, Aussterben | ✓ `design/gdd/staemme-und-diplomatie.md` (Draft) |

Menschen & Bevölkerung und Stämme & Diplomatie hängen ihrerseits von Welt & Insel ab.
Der Kreis ist über die Reihenfolge im Schritt gelöst: Welt & Insel rechnet zuerst und
liest V_t und den Kriegszustand nach dem vorigen Schritt; die anderen lesen Gebiet, cap_t
und food_t desselben Jahres.

**Wird gebraucht von:** Menschen & Bevölkerung (Zellen, Gebiet, food_t, cap_t,
Startplätze, Hunger), Stämme & Diplomatie (Gebiet, Gedränge, Startstämme, Platzsuche),
Kultur & Erinnerung (Hunger mit Grund, Dürre, Wohlstand, Brandnarben), Gotteskraft &
Götterkräfte (Brand, Fest-Segen), Begehren & Lust und Harem & Rituale (Orte),
Simulationskern (nächste Landzelle), Weltansicht & Kamera, Chronik, Speichern & Laden. Ein
GDD haben davon der Simulationskern und Menschen & Bevölkerung; beide nennen Welt & Insel.

**Verträge mit anderen Systemen:** Stämme & Diplomatie erklärt das Aussterben unter
3 Köpfen, sucht Abspaltungsplätze mit Formel 2 und seinem eigenen Strom und erteilt die
Aufträge „Gebiet neu berechnen“ und „Stamm entsteht mit Vorrat x“ (übernommen in
`design/gdd/staemme-und-diplomatie.md`, Regeln 15–17). Für GDDs, die noch fehlen: Gotteskraft &
Götterkräfte erteilt „Brand“ und „Vorrat + 0,6 · V_t“. Die Weltansicht blendet eine Dürre
weich ein und aus, damit eine Dürre von wenigen Sekunden lesbar bleibt, und zeigt alte
Brandnarben verblasst, aber nie gelöscht.

## Tuning Knobs

Alle Defaults bleiben bis zur ersten AC-16-Messung auf den Werten von Prototyp 1; die
sicheren Bereiche gelten für Änderungen danach.

| Regler | Default | Sicherer Bereich | Wirkung |
|---|---|---|---|
| W × H | 40 × 28 | fest bis zur ersten AC-16-Messung | Größe der Insel; größer heißt mehr Platz und weniger Streit |
| κ | 0,55 | fest bis zur ersten AC-16-Messung | Ernte je Fruchtbarkeit |
| Reichweite r_t = a + b · sqrt(V) | a = 2, b = 0,75 | fest bis zur ersten AC-16-Messung | wie schnell ein Gebiet mit dem Volk wächst |
| Ernte-Deckel | 1,25 · V | fest bis zur ersten AC-16-Messung | höchste Ernte pro Kopf |
| Vorrat-Deckel | 3 · V | 1–5 · V | wie viele Jahre ein Vorrat überbrückt |
| Kriegsabzug | 0,85 | 0,7–1 | wie sehr Krieg die Ernte senkt |
| Hunger | 0,06 · V + 0,25 · Fehlbetrag | fest bis zur ersten AC-16-Messung | Tote je Hungerjahr |
| Chronik-Abstand der Hungersnot | 6 Jahre | 3–12 Jahre | wie oft die Chronik über Hunger desselben Stamms schreibt |
| p_drought, Dauer, δ | 0,022; 4–10 Jahre; 0,55 | 0,01–0,05; –; 0,4–0,8 | wie oft und wie hart Dürren sind |
| T_burn | 40 Jahre | 10–120 Jahre | wie lange ein Blitz die Ernte senkt |
| Abzug verbrannter Erde | 0,5 (halbe Ernte) | 0–0,8 | wie stark ein Blitz das Land trifft |
| Schwellen der Fruchtbarkeit in der Anzeige | karg < 0,6, mittel < 0,9, fruchtbar ab 0,9 | – | etwa 24 / 37 / 39 % des Landes |

## Visual/Audio Requirements

Keine eigenen. Die Weltansicht baut die Insel aus h_c und f_c (Höhe und Grün), zeigt
Grenzen der Gebiete, verbrannte Erde (dunkel, mit der Frist verblassend, danach als alte
Narbe, nie ganz gelöscht), eine Dürre (fahles Land, weich ein- und ausgeblendet) und die
Orte. Verbrannte Erde muss aus der normalen Zoomstufe sofort erkennbar sein (Pillar 2);
das Kriterium dafür gehört ins GDD Weltansicht & Kamera.

## Game Feel

Die Insel soll sich wie ein Ort anfühlen, nicht wie ein Spielbrett: Stämme wachsen
sichtbar in ihr Land hinein, eine Dürre legt sich über alles, und ein Blitz hinterlässt
eine Narbe, die man auch zwei Ären später noch sieht.

## UI Requirements

- **Zelle ansehen** (auch in der Pause): Land oder Wasser, Fruchtbarkeit als Wort (karg,
  mittel, fruchtbar; Schwellen in den Tuning Knobs), Besitzer, verbrannt mit Restfrist,
  eine alte Narbe mit dem Jahr des Brands, ein Ort mit seiner Art
- **Stamm ansehen:** Gebietsgröße, Tragfähigkeit und Vorrat als einfache Anzeige („satt“,
  „knapp“, „hungert“), nie als Rohzahl
- **Meldungen** (über die Chronik): Dürre beginnt und endet, Hungersnot mit Grund, Land
  verbrannt

> **📌 UX Flag — Welt & Insel**: This system has UI requirements. In Phase 4
> (Pre-Production), run `/ux-design` to create a UX spec for each screen or HUD
> element this system contributes to **before** writing epics. Stories that
> reference UI should cite `design/ux/[screen].md`, not the GDD directly.

## Cross-References

| Dokument | Was hier verwendet wird |
|---|---|
| `prototypes/weltengott-concept/prototype.html` | `makeIsland`, `landNear`, `assignTerritory`, Klima- und Nahrungsteil von `step`, Abspaltung, Aussterben unter 3, Fest-Segen, Brand eines Blitzes |
| `prototypes/weltengott-concept/REPORT.md` | Kalibrierung: 0 % Aussterben, 60 von 1.000 Ären ohne großes Ereignis, Pillar-1-Test in 56 % der Welten erfüllt |
| `design/gdd/simulationskern.md` | Regel 4 (Reihenfolge, Aufträge), 9 (Physikergebnis auf Wasser), 17 (Erzeugen), Formel 5 (O_year = 0), Formel 6 (Strom 1) |
| `design/gdd/menschen-und-bevoelkerung.md` | Verträge für food_t, cap_t, Gebiet, Zellenreihenfolge, Orte der Einzelnen, Verlust als Auftrag |
| `design/gdd/systems-index.md` | Abhängigkeiten |

## Acceptance Criteria

Testwurzel: `Assets/Tests/EditMode/WeltUndInsel/`. Damit Welt & Insel allein testbar ist,
nutzen die Tests die Stubs `ITribeSource` (Mitten, Krieg, Aussterben) und `IPopulationSource`
(V_t), beide mit Aufrufprotokoll.

- **AC-01 Formel 1 [Logic/EditMode]** **GIVEN** world_seed 42, **WHEN** die Insel zweimal erzeugt wird, **THEN** sind Land, f_c und h_c aller 1.120 Zellen bitgleich, jede Landzelle hat 0,1 ≤ f_c ≤ 1, jede Randzelle (x = 0, x = 39, y = 0, y = 27) ist Wasser, alles Land gehört zur Hauptinsel, und der Strom steht danach an derselben Stelle wie ohne die Randregel.
- **AC-02 Formel 1, statistisch [Logic/EditMode, Grenzen vorläufig]** **GIVEN** die Startwerte 0 bis 999, **WHEN** je eine Insel erzeugt wird, **THEN** liegen der mittlere Landanteil, sein 5. und 95. Perzentil, die mittlere Fruchtbarkeit und der Anteil der drei Fruchtbarkeitsklassen innerhalb der Werte der ersten Messung mit dem Kernstrom (Erwartung nach dem Python-Nachbau: 61 % bei 56–66 %, 0,77, 24 / 37 / 39 %); bis zur ersten Messung gelten ± 3 Punkte.
- **AC-03 Formel 2, Start [Logic/EditMode]** **GIVEN** 1.000 Startwerte, **WHEN** die Startplätze gesetzt werden, **THEN** sind beide Land und höchstens 3 Zellen (Chebyshev) von (14, 14) bzw. (26, 14) entfernt, und die Stämme haben die IDs 0 und 1; **GIVEN** eine eingespielte Insel ohne gültigen Versuch, **THEN** gilt (10, 14); **GIVEN** zusätzlich (10, 14) ist Wasser, **THEN** die nächste Landzelle zu ihm.
- **AC-04 Formel 3 [Logic/EditMode]** **GIVEN** zwei Stämme mit V = 48 und Mitten (14, 14) und (27, 14) auf reinem Land, **WHEN** das Gebiet berechnet wird, **THEN** gehört (20, 14) Stamm 0 (q = 0,833 gegen 0,972), keine Zelle zwei Stämmen; **GIVEN** die Mitten (14, 14) und (26, 14), **THEN** gehört (20, 14) bei gleichem q Stamm 0; **GIVEN** eine Wasserzelle im Radius, **THEN** gehört sie niemandem; **GIVEN** ein Stamm mit V = 2, **THEN** hat er keine Zelle.
- **AC-05 Formel 3, Takt [Logic/EditMode]** **GIVEN** S_year = 5, **WHEN** 20 Geschichtsjahre laufen, **THEN** werden Gebiet und Tragfähigkeit in jedem Jahr genau einmal beim Versatz 0 neu berechnet, und zusätzlich sofort im Schritt eines Auftrags „Gebiet neu berechnen“.
- **AC-06 Formel 4 [Logic/EditMode]** **GIVEN** 154 Zellen, davon 145 mit f = 0,91 und 9 verbrannte mit f = 0,91, und eine verbrannte Zelle außerhalb des Gebiets, **WHEN** cap_t ohne und mit Dürre berechnet wird, **THEN** ist es 74,82475 und 41,1536125 (Toleranz 1e-9), und die Zelle außerhalb zählt nicht.
- **AC-07 Formel 5 [Logic/EditMode]** **GIVEN** V = 60, cap = 42, food = 10, kein Krieg, **WHEN** das Jahr läuft, **THEN** erhält Menschen den Auftrag „Verlust 6“ und food ist 0; im Krieg ist prod = 35,7 und k = 7; **GIVEN** V = 25, cap = 21, food = 0, **THEN** ist k = 3 (runden(2,5) = 3, nicht 2); **GIVEN** V = 10, food = 28, cap = 100, **THEN** ist food danach 30.
- **AC-08 Formel 6 [Logic/EditMode]** **GIVEN** ein fester Startwert, **WHEN** 100.000 Geschichtsjahre laufen, **THEN** liegt der Anteil der Dürrejahre bei 13,3 % ± 0,8 Punkte, jede Dauer von 4 bis 10 kommt vor und 11 nie, die mittlere Dauer ist 7 ± 0,15, und in Dürrejahren wird kein Wurf für den Beginn gezogen.
- **AC-09 Formel 7 [Logic/EditMode, eingespielte Insel]** **GIVEN** eine eingespielte Insel und ein Blitz in Jahr B auf eine Zelle mit 5 Landzellen im 3 × 3-Feld, **WHEN** die Jahre laufen, **THEN** sind genau diese 5 in genau 39 Ernten verbrannt, Jahr und Ursache ihres Brands bleiben danach gespeichert, und eine Brandstelle entsteht; ein zweiter Blitz 30 Jahre später setzt die Frist neu, ohne die Wirkung zu verdoppeln; auf derselben Insel, auf der (1, 1) Wasser ist, verbrennt ein Blitz auf (0, 0) oder einer nur auf Wasser nichts, wirft keine Ausnahme und meldet nichts; ist (1, 1) Land, verbrennt der Blitz auf (0, 0) genau diese Zelle.
- **AC-10 Regel 18 [Logic/EditMode]** **GIVEN** eine eingespielte Insel mit Wasser in (5, 5) und Land in (7, 5) und (5, 7), **WHEN** „nächste Landzelle zu (5, 5)“ abgefragt wird, **THEN** ist die Antwort (7, 5) (Abstand 2, kleinere ID 207); „nächste Zelle“ eines Stamms ohne Zelle ergibt „keine“; der Zustand ist danach bitgleich.
- **AC-11 Regel 19 [Logic/EditMode, mit Stubs]** **GIVEN** die Stubs, **WHEN** ein Jahr bei O_year = 0 läuft, **THEN** ist die Reihenfolge Dürre → Fristen → Gebiet und Tragfähigkeit → Ernte und Hunger je Stamm in aufsteigender ID; ein Krieg, den der Stub nach dem Lauf in Jahr Y setzt, ändert die Ernte erst in Y + 1.
- **AC-12 Regel 5 [Logic/EditMode]** **GIVEN** ein Startwert, **WHEN** die Welt erzeugt wird, **THEN** stehen Insel, Startplätze, erstes Gebiet (mit V = 48) und Vorrat 48 je Stamm bereit, bevor Menschen & Bevölkerung erzeugt wird, und zwei Erzeugungen im selben Lauf ergeben denselben Zustands-Hash. Ein beim ersten grünen Lauf festgehaltener Hash gilt nur auf demselben Build und Rechner (Simulationskern Regel 7) und ist in CI kein Prüfwert.
- **AC-13 Speichern [Logic/EditMode]** **GIVEN** ein eingespielter Zustand (Dürre mit Rest 3, 5 Zellen mit Frist 17, ein Stamm mit Hunger in diesem Jahr, Jahr der letzten Hungersnot, Orte), **WHEN** gespeichert und geladen wird, **THEN** gleichen die Hashes der Jahre 301 bis 600 denen eines durchgehenden Laufs.
- **AC-14 Abspaltung [Logic/EditMode, mit Stubs]** **GIVEN** ein Stamm mit V = 64 spaltet 25 Köpfe ab, **WHEN** die Platzsuche läuft, **THEN** sucht sie 9 bis 14 Zellen von seiner Mitte, der neue Stamm bekommt den Vorrat 12,5 und mindestens eine Zelle, der alte behält seinen Vorrat, und Gebiet und Tragfähigkeit sind im selben Schritt neu; **GIVEN** jeder Versuch ergibt s ≤ −1 oder der beste liegt auf einer fremden Mitte, **THEN** findet die Abspaltung nicht statt.
- **AC-15 Hauptgrund und Chronik [Logic/EditMode]** **GIVEN** das Beispiel aus Formel 5, **WHEN** der Hunger gemeldet wird, **THEN** ist der Hauptgrund „Dürre“; bei einem Fehlbetrag nur durch verbrannte Erde ist er „Brand“ und nennt die Ursache des Brands; ohne Dürre, Brand und Krieg ist er „Gedränge“; Hungersnöte desselben Stamms in den Jahren Y, Y + 4 und Y + 11: Y + 4 wird gemeldet, aber nicht für die Chronik markiert (4 Jahre nach der vorigen), Y + 11 schon (7 Jahre nach der vorigen, auch wenn diese nicht markiert war). Das Ergebnis der Simulation ist mit und ohne Hauptgrund bitgleich.
- **AC-16 Kalibrierung [Integration/EditMode, ADVISORY bis zur ersten Messung]** **GIVEN** die Startwerte 0 bis 99, je 10 Ären ohne Eingriff, mit den Debug-Werkzeugen, **WHEN** sie laufen, **THEN** stirbt keine Welt aus (alle V = 0), der Anteil der Ären ohne großes Ereignis liegt bei 6 % ± 3 Punkte wie in Prototyp 1, und als Kennzahl (keine Hürde) wird gemeldet, in wie vielen Welten der Pillar-1-Test erfüllt ist (Prototyp 1: 56 %).
- **AC-17 Orte [Logic/EditMode]** **GIVEN** eine neue Welt, eine Abspaltung, ein Blitz auf Land und ein Stamm, der unter 3 Köpfe fällt, **WHEN** das geschieht, **THEN** entstehen je eine Siedlung (zwei beim Start), eine Brandstelle und eine Ruine an der alten Mitte, mit steigenden, nie wiederverwendeten IDs, und sie überstehen Speichern und Laden.
- **AC-18 Regel 2 [Logic/EditMode]** **GIVEN** 300 Jahre mit 20 Blitzen, **WHEN** f_c und h_c verglichen werden, **THEN** sind sie bitgleich mit dem Erzeugen, und nur f_eff ändert sich.

## Open Questions

| Frage | Wer klärt | Bis wann |
|---|---|---|
| **Erste Änderungen nach der Kalibrierung** (Empfehlung des Creative Director): verbrannte Erde trägt 0 statt halb und gibt nach dem Heilen eine Zeit lang einen Aschebonus (f · 1,3); 3 statt 2 Startstämme. Ziel: ein Blitz soll spürbar sein (heute rund 3 % Tragfähigkeit) und Ära I soll belebter werden (29 der 60 stillen Ären von Prototyp 1 waren Ära I). Kennzahlen: Tote durch Blitze des Spielers, Pillar-1-Test, Hungersnöte je Stamm und Ära | Entwickler, dann Messung mit AC-16 | nach der ersten AC-16-Messung |
| **Zweite Runde**: regionale, seltenere und längere Dürren (15–40 Jahre); Gebiet gewichtet nach Fruchtbarkeit und Brand; niedrigere Grenze der Gnade (etwa 0,95 · cap) oder eine stille „Knappheit“ unter 10 % Hungertoten gegen wiederkehrende kleine Hungersnöte; längere Wirkung verbrannter Erde | Entwickler, dann Messung | danach |
| Aussterben: Prototyp 1, dieser Entwurf und Menschen & Bevölkerung Regel 2 sagen unter 3 Köpfen (angeglichen 2026-10-25). Offen: Was geschieht mit den letzten 1–2 Menschen (im Prototyp verschwinden sie, V = 0)? | GDD Stämme & Diplomatie, dann Menschen & Bevölkerung | mit dem GDD Stämme & Diplomatie |
| Soll es im MVP-0 von Hand gebaute Inseln geben (gleiches Datenformat, aus einer Datei)? | Entwickler | vor dem Level-Design |
| Darf die Mitte eines Stamms wandern (etwa weg von verbrannter Erde, hin zu fruchtbarem Land)? Dieser Entwurf liest sie nur; Stämme & Diplomatie hält sie vorerst fest wie im Prototyp | Entwickler | nach der Kalibrierung |
| Ist Wasser tödlich (Ertrinken nach einem Wurf ins Meer)? Das wäre ein kostenloser Tötungsknopf; bis dahin setzt der Kern den Menschen an Land | GDD Gotteshand & Ragdoll, zusammen mit den Kosten | mit dem GDD |
| Reicht ein Raster von 40 × 28 für die 3D-Ansicht, oder braucht sie ein feineres Gelände (dann nur Darstellung)? | GDD Weltansicht & Kamera | mit dem GDD |
| Sollen später alle Welten anders aussehen (gedrehte Startachse, mehrere Inselformen)? Heute liegt jede Insel als Oval mit Stämmen links und rechts | Entwickler | nach der Kalibrierung |
