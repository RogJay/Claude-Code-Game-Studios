# Stämme & Diplomatie

> **Status**: Draft (Nachtarbeit 2026-10-26, ohne Freigabe des Entwicklers; jede Entscheidung steht in `production/nachtarbeit-2026-10-26.md`)
> **Author**: Claude (autonom, mit Erlaubnis des Entwicklers)
> **Last Updated**: 2026-10-26
> **Last Verified**: 2026-10-26
> **Implements Pillar**: Pillar 1 (Die Welt lebt ohne dich), Pillar 2 (Jeder Eingriff hinterlässt eine Spur)

## Summary

Stämme & Diplomatie führt die Stämme als Gemeinschaften: ihre Mitte, ihr Temperament
(Aggression), ihren Groll aufeinander, Kriege, Frieden, Eroberung, Abspaltung und
Aussterben. Krieg ist im MVP-0 abstrakt: Verluste als Zahlen, keine sichtbaren Kämpfe.
Die Regeln sind genau die aus Prototyp 1, damit dessen Messwerte gelten.

> **Quick reference** — Layer: `Gameplay` · Priority: `MVP` · Key deps: `Menschen & Bevölkerung, Welt & Insel`

## Overview

Ein Stamm ist mehr als seine Kopfzahl: Er hat eine feste Mitte auf der Insel, eine
Aggression zwischen 0,05 und 0,95 und gegenüber jedem anderen Stamm einen **Groll**, der
aus Gründen zusammengesetzt ist (Grenze, Hunger, Glaube, Abspaltung, Rache, ein Eingriff
des Gottes) und jedes Jahr ein wenig verblasst. Wird der Groll groß genug, erklärt ein
Stamm den Krieg. Krieg kostet beide Seiten jedes Jahr Köpfe, bis einer erobert wird oder
beide erschöpft Frieden schließen. Ein großer Stamm ohne Krieg kann sich abspalten; die
Abtrünnigen ziehen weg und glauben anders, was neuen Groll erzeugt. Fällt ein Stamm unter
3 Köpfe, ist er untergegangen. Damit erzählt die Welt ohne den Spieler Geschichten
(Pillar 1), und jeder Eingriff, den ein Nachbar als Frevel sieht, wird zu benanntem
Groll, den die Chronik bei der Kriegserklärung auf den Spieler zurückführt (Pillar 2).

## Player Fantasy

Der Spieler sieht Völker mit eigenen Beweggründen. Er versteht, warum zwei Stämme
Krieg führen („Streit um Land an der Grenze“, „ihr Glaube ist dem anderen ein Gräuel“),
und er erlebt, dass sein Segen für den einen bei den strenggläubigen Nachbarn Neid und
Hass weckt. Er fragt sich vor jedem Eingriff: Wer wird das sehen, und wie werden sie es
deuten?

## Detailed Design

### Core Rules

*Entwurf, 1:1 aus Prototyp 1 (`prototype.html`, Zeilen 155–437). Abweichungen stehen
unter „Kalibrierung gegen Prototyp 1“.*

**Der Stamm**

1. Ein Stamm hat: Stammes-ID (fortlaufend, nie wiederverwendet), Name, Mitte (eine
   Landzelle), Aggression a_t, lebt ja/nein, Geburtsjahr, Elternstamm (oder keiner),
   Groll gegen jeden anderen Stamm, laufende Kriege und Friedensfristen. Volk V_t gehört
   Menschen & Bevölkerung, Gebiet, Vorrat und Tragfähigkeit gehören Welt & Insel, die
   Kulturachse x_t, die Gesellschaftsform und die Stammeshingabe gehören Kultur &
   Erinnerung. Dieses System liest sie nur.
2. Die **Mitte** eines Stamms ändert sich nie (wie in Prototyp 1). Die Startmitten und
   die IDs 0 und 1 legt Welt & Insel fest (Welt Regel 5); die Mitte eines abgespaltenen
   Stamms legt dieses System mit der Platzsuche fest (Regel 15).
3. Der **Name** ist `NAMES[id mod 14]` aus der Liste von Prototyp 1 (Ura, Mirr, Kesh, Talo,
   Vend, Oru, Sela, Brak, Ilm, Daru, Feno, Ask, Lumi, Tharn); ab ID 14 mit angehängter
   römischer Ziffer `(id div 14) + 1`, also „Ura II“ für ID 14. Die Liste ist eine
   Datendatei.
4. Höchstens **7 Stämme** leben zugleich (MAX_TRIBES); das begrenzt nur Abspaltungen.

**Groll**

5. Der Groll G_{t→o} von Stamm t gegen Stamm o ist die Summe seiner **Gründe**: Grenze,
   Hunger, Glaube, Abspaltung, Rache und je Eingriff des Gottes ein eigener Grund „Gott“
   mit der Eingriffs-ID. Groll ist gerichtet: G_{t→o} und G_{o→t} sind verschiedene Werte.
6. Jedes Jahr verblasst jeder Grund und damit der ganze Groll um den Faktor 0,965
   (Formel 1). Groll gegen einen Stamm, der nicht mehr lebt, wird gelöscht.
7. Groll entsteht aus (Formel 1): einer gemeinsamen **Grenze** (die Reichweiten berühren
   sich), **Hunger** (ein hungernder Stamm neidet dem bestgenährten Nachbarn),
   **Glaube** (die Kulturachsen liegen weit auseinander), einer **Abspaltung** (beide
   Seiten), **Rache** (jedes Kriegsjahr, beide Seiten) und Eingriffen des Gottes, die
   ein anderer Stamm als Frevel sieht (Auftrag „Groll“ von Gotteskraft & Götterkräfte).

**Krieg**

8. Ein Stamm t erklärt o den **Krieg**, wenn sein Groll mal seine Kampfeslust groß
   genug ist, er nicht zu klein ist, nicht schon gegen o kämpft und keine Friedensfrist
   gegen o läuft (Formel 2). Krieg gilt immer für beide Seiten. Grund der Erklärung ist
   der größte Einzelgrund; ist es ein Eingriff des Gottes, nennt das Ereignis ihn als
   Ursache (Simulationskern Regel 15), und die Chronik schreibt „weil du …“.
9. Jedes Kriegsjahr kämpft jedes Paar genau einmal: Beide verlieren Köpfe nach der
   Stärke des Gegners (Formel 3). Die Verluste setzt dieses System als Auftrag „Verlust
   k“ an Menschen & Bevölkerung, das die Opfer wählt (Menschen Regel 13). Die Angst vor
   den Verlusten verschiebt die Kulturachse beider Seiten zum Strengen (Auftrag an Kultur
   & Erinnerung), und beide bekommen Rache-Groll.
10. Ist ein Stamm nach dem Kampf zu klein (Formel 4), wird er **erobert**: Die Hälfte
    seiner Köpfe (abgerundet) wechselt zum Sieger, der Rest stirbt; der Eroberte ist
    untergegangen. Die Kulturachse des Siegers mischt sich mit der des Eroberten nach
    Köpfen (Auftrag an Kultur & Erinnerung).
11. Hat im laufenden Krieg jede Seite für sich mehr als 30 % verloren (Summe ihrer
    jährlichen Verlustanteile) oder dauert der Krieg länger als 18 Jahre, schließen sie **Frieden** (Formel 5). Danach herrscht
    20 Jahre Friedensfrist zwischen beiden, und ihr Groll gegeneinander, falls es ihn
    gibt, wird auf 0,2 Rache gesetzt.
12. Ein Stamm ist **im Krieg**, wenn er mindestens einen Krieg gegen einen lebenden
    Stamm führt. Welt & Insel liest das für die Ernte (Welt Regel 13). Stirbt ein Stamm,
    enden alle seine Kriege sofort, ohne Frieden und ohne Friedensfrist.
13. Im MVP-0 ist Krieg abstrakt. Es gibt keine Kämpfe einzelner Menschen, kein Merkmal
    „im Kampf“ und keine Gefangenen; die Regeln dafür in Menschen & Bevölkerung (Regel 19,
    Zustand „im Kampf“) bleiben bis zum System Sichtbarer Krieg (Vertical Slice)
    ungenutzt. Damit erfüllt dieses System den Vertrag aus Zustimmungsmodell Regel 21
    (niemand wird aus einer laufenden Handlung in Kampf oder Gefangenschaft gezogen)
    schon dadurch, dass es niemanden zieht; Kriegstote wählt Menschen, das Beteiligte
    laufender Handlungen nie zieht.

**Aggression**

14. Jeder Stamm hat eine Aggression a_t zwischen 0,05 und 0,95. Startstämme würfeln sie
    zwischen 0,25 und 0,5; ein abgespaltener Stamm erbt sie mit einer kleinen
    Abweichung. Jedes Jahr wandert sie: Strenggläubige werden kampflustiger, eine
    Lust-Sekte friedlicher (Formel 7).

**Abspaltung**

15. Ein Stamm mit mehr als 44 Köpfen, der keinen Krieg führt, kann sich abspalten, wenn
    weniger als 7 Stämme leben. Ist er gedrängt (mehr Köpfe als 90 % seiner
    Tragfähigkeit), ist das wahrscheinlicher (Formel 6). Die neue Mitte sucht die
    Platzsuche von Welt & Insel (Welt Formel 2) mit dem Zufallsstrom dieses Systems.
    Findet sie keinen Platz, liegt er auf der Mitte eines anderen Stamms oder bekäme der
    neue Stamm keine Zelle, fällt die Abspaltung aus (Welt Regel 10), und der nächste
    Stamm darf es versuchen. Höchstens eine Abspaltung pro Jahr.
16. Bei einer Abspaltung wechselt ein Teil der Köpfe (35 bis 45 %, abgerundet) in den
    neuen Stamm. Er glaubt anders: Seine Kulturachse liegt 0,6 vom Elternstamm entfernt,
    zur Mitte hin, wenn der Elternstamm deutlich ausgerichtet ist, sonst zufällig in eine
    Richtung (Formel 6). Beide Stämme bekommen Abspaltungs-Groll gegeneinander. Den
    Vorrat 0,5 · Teil vergibt Welt & Insel (Welt Regel 10).

**Aussterben**

17. Ein Stamm mit weniger als 3 Köpfen ist **untergegangen** (wie Prototyp 1, Menschen
    Regel 2, Welt Regel 8). Dieses System erklärt es mit dem Ereignis „Untergang“, setzt
    „lebt“ auf nein und erteilt Menschen & Bevölkerung den Auftrag „Stamm erloschen“
    (V_t wird 0). Ein eroberter Stamm geht ohne eigenes Ereignis „Untergang“ unter; das
    Ereignis „Eroberung“ nennt ihn.
18. Wer bei einer Eroberung oder einem Untergang **in Gottes Hand** ist, wird nicht
    gezogen (Menschen Regel 20). Er gehört von da an dem Sieger, beim Untergang dem
    Stamm, dessen Mitte seiner Zelle am nächsten liegt (bei Gleichstand die kleinere
    ID), und das gilt als Stammeswechsel (Menschen Regel 20, Zustimmungsmodell Regel 4).
    Lebt kein Stamm mehr, stirbt er beim Loslassen.

**Reihenfolge im Schritt**

19. Stämme & Diplomatie ist ein Jahressystem mit Versatz O_year = 2 und rechnet im
    selben Schritt nach Menschen & Bevölkerung (Simulationskern Formel 5). Reihenfolge,
    jeweils über die lebenden Stämme in aufsteigender ID: (1) Groll verblasst, Groll
    gegen Tote wird gelöscht; (2) Aggression wandert; (3) Hunger-Groll; (4) Grenz- und
    Glaubensgroll über alle geordneten Paare; (5) Kriegserklärungen; (6) Kämpfe, Eroberung
    und Frieden je Paar, ausgelöst vom Stamm mit der kleineren ID; (7) Abspaltung;
    (8) Aussterben; danach Auftrag „Gebiet neu berechnen“ an Welt & Insel, falls sich
    ein Stamm geändert hat. Innerhalb eines Stamms werden Gegner in aufsteigender ID
    durchgegangen.

### States and Transitions

| Zustand eines Stamms | Bedeutung | Wechsel |
|---|---|---|
| **Lebt, in Frieden** | keine laufenden Kriege | → im Krieg (eigene oder fremde Erklärung) · → untergegangen (unter 3 Köpfen) |
| **Lebt, im Krieg** | mindestens ein Krieg gegen einen lebenden Stamm | → in Frieden (alle Kriege beendet) · → erobert · → untergegangen |
| **Untergegangen** | lebt nicht mehr; ID, Name und Mitte (als Ruine) bleiben für Chronik und Erinnerung | endgültig |

| Zustand eines Paars (t, o) | Bedeutung | Wechsel |
|---|---|---|
| **Neutral** | kein Krieg, keine Frist | → Krieg (Formel 2) |
| **Krieg** | seit Jahr Y_since, Verlustanteile L_t, L_o | → Friedensfrist (Formel 5) · → endet ohne Frist, wenn einer stirbt |
| **Friedensfrist** | bis Jahr Y + 20 kein Krieg zwischen beiden | → neutral nach Ablauf |

### Interactions with Other Systems

| System | Was hineingeht | Was herauskommt | Wer besitzt die Schnittstelle |
|---|---|---|---|
| Simulationskern | Jahrestakt (O_year = 2, nach Menschen), Zufallsstrom system_id 4, Erzeugen nach Kultur & Erinnerung | Ereignisse: Krieg erklärt (Grund, Ursache), Frieden, Eroberung, Abspaltung, Untergang | Kern |
| Welt & Insel | Startmitten und IDs 0 und 1, Gebiet, Zellenzahl, cap_t, food_t, Hunger in diesem Jahr; Land und Fruchtbarkeit für die Platzsuche | Mitten, Kriegszustand, Aussterben; Aufträge „Gebiet neu berechnen“, „Stamm entsteht mit Vorrat x“ | Welt & Insel (Gebiet), dieses System (Mitten, Aussterben) |
| Menschen & Bevölkerung | V_t je Stamm (lesend) | Aufträge „Verlust k“, „n Köpfe wechseln von A zu B“ (Abspaltung, Eroberung), „Stamm erloschen“, Zugehörigkeit eines Losgelassenen nach Regel 18 | Menschen (Opferwahl), dieses System (Zahlen) |
| Kultur & Erinnerung | Kulturachse x_t, Gesellschaftsform (lesend) | Aufträge „Kultur für neuen Stamm anlegen (Achse, Hingabe des Elternstamms)“, „Achse verschieben (Wert, Ursache Krieg)“, „Achsen mischen (Eroberung)“; Ereignisse für die Erinnerung | Kultur & Erinnerung (vorläufig, GDD fehlt) |
| Gotteskraft & Götterkräfte | Auftrag „Groll (von o gegen t, Wert, Eingriffs-ID)“ | Liste der Stämme mit Mitte und Achse, damit Gotteskraft festlegt, wer einen Eingriff als Frevel sieht | Gotteskraft (vorläufig, GDD fehlt) |
| Zustimmungsmodell | – | Vertrag Regel 21 erfüllt (Regel 13 hier) | Zustimmungsmodell |
| Chronik | – | Ereignisse mit Grund und Ursache, Namen der Stämme | Kern-Ereignisformat |
| Weltansicht & Kamera, HUD | – | Mitten, Namen, Kriege, Friedensfristen, Groll als Stufe | dieses System |
| Speichern & Laden | kompletter Zustand | alle Felder aus Regel 1 außer Name (folgt aus der ID), Grund und Wert jedes Grolls, Kriege mit Beginn und Verlustanteilen, Friedensfristen | Speichern & Laden |

## Formulas

Übernahme aus `prototypes/weltengott-concept/prototype.html` (`addGriev`, Groll-Teil
und Krieg, Frieden, Abspaltung und Aussterben in `step`). Alle Zufallszahlen kommen aus
dem Strom system_id 4 (`NextDouble()` = u, Simulationskern Formel 6), in der Reihenfolge
von Regel 19. „runden“ heißt `floor(x + 0,5)` wie `Math.round` im Prototyp; Abstände sind
euklidische Abstände der Mitten; `r_t = 2 + 0,75 · sqrt(V_t)` (Welt Formel 3). Toleranz in
Tests 1e-9.

### 1. Groll

The Groll formula is defined as:

`G_{t→o} = Σ_Gründe g` · jährlich `g ← 0,965 · g` für jeden Grund · Zuwachs je Jahr: **Grenze** `+0,02`, wenn `|m_t − m_o| < r_t + r_o` und `V_t > 0,9 · cap_t`, sonst `+0,007` bei Berührung · **Glaube** `+0,022 · Δx`, wenn `Δx = |x_t − x_o| > 0,7` und `|m_t − m_o| < r_t + r_o + 8` · **Hunger** `+0,14` gegen den Stamm o ≠ t mit dem größten `food_o / V_o`, falls t in diesem Jahr hungert und `|m_t − m_o| < r_t + r_o + 6` · **Abspaltung** `+0,35` beidseitig · **Rache** `+0,01` beidseitig je Kriegsjahr · **Gott** Wert aus dem Auftrag

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| g | double | ≥ 0 | calculated | Wert eines Grunds |
| m_t | Zelle | – | calculated | Mitte von Stamm t |
| r_t | double | ≥ 3,3 | calculated | Reichweite (Welt Formel 3) |
| cap_t, food_t | double | ≥ 0 | calculated | Welt & Insel, Stand nach der Ernte dieses Jahres |
| x_t | double | −1 bis 1 | calculated | Kulturachse (Kultur & Erinnerung) |
| Verblassen | double | 0,965 | data file | jährlicher Faktor (Prototyp 1) |

**Output Range:** Ein Grund, der jedes Jahr gleich wächst, strebt gegen `Zuwachs / 0,035`:
Grenze 0,2 (gedrängt 0,57), Glaube bei Δx = 1,5 etwa 0,94, bei Δx = 2 etwa 1,26. Abspaltungs-
Groll fällt in 20 Jahren von 0,35 auf 0,17. Groll ist nie negativ.
**Example:** Zwei Nachbarn berühren sich, t ist gedrängt, Δx = 0,9: Nach einem Jahr hat t
0,02 + 0,0198 = 0,0398 Groll gegen o; nach sehr vielen Jahren etwa 0,57 + 0,57 = 1,14.

### 2. Kriegserklärung

The Kriegserklärung formula is defined as:

`t erklärt o den Krieg ⇔ G_{t→o} · (0,5 + a_t) > 0,6 ∧ V_t ≥ 0,6 · V_o ∧ V_t ≥ 12 ∧ kein Krieg zwischen t und o ∧ Jahr ≥ Frist_{t,o}`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| a_t | double | 0,05–0,95 | calculated | Aggression |
| Kriegsschwelle | double | 0,6 | data file | Prototyp 1 |
| Größenverhältnis | double | 0,6 | data file | t muss mindestens 60 % von o haben |
| Mindestvolk | int | 12 | data file | kleinere Stämme erklären keinen Krieg |
| Frist_{t,o} | int | Jahr | calculated | Ende der Friedensfrist (gleich für beide Richtungen) |
| Grund | Aufzählung | – | calculated | der größte Einzelgrund; bei Gleichstand in der Reihenfolge Grenze, Hunger, Glaube, Abspaltung, Rache, Gott (kleinere Eingriffs-ID zuerst) |

**Output Range:** Bei a_t = 0,4 genügt ein Groll über 0,67, bei a_t = 0,05 erst über 1,09,
bei a_t = 0,95 schon über 0,41. Grenzgroll allein (höchstens 0,57) reicht nur einem
gedrängten, sehr kampflustigen Stamm (a_t > 0,55).
**Example:** G = 0,8, a_t = 0,4 → 0,72 > 0,6; t hat 20 Köpfe, o hat 33 (0,6 · 33 = 19,8):
Krieg. Hätte o 34 Köpfe (20,4), gäbe es keinen.

### 3. Kampfverluste

The Kampf formula is defined as:

`S_t = V_t · (0,5 + a_t)` · `k_o = min(V_o, runden(S_t · 0,03 · (0,6 + 0,8 · u₁)))` · `k_t = min(V_t, runden(S_o · 0,03 · (0,6 + 0,8 · u₂)))` · `L_t ← L_t + k_t / max(1, V_t)` · Achse `x_t ← x_t − 0,04 · k_t / max(1, V_t − k_t)` (Auftrag an Kultur) · beide `+0,01` Rache

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| S_t | double | ≥ 0 | calculated | Stärke |
| k_t, k_o | int | ≥ 0 | calculated | Verluste dieses Jahres (Auftrag an Menschen) |
| u₁, u₂ | double | 0–1 | calculated | in dieser Reihenfolge, t ist der Stamm mit der kleineren ID |
| L_t | double | ≥ 0 | calculated | Summe der jährlichen Verlustanteile im laufenden Krieg (V_t vor dem Kampf) |
| Kampfrate | double | 0,03 | data file | Prototyp 1 |

**Output Range:** Ein Stamm verliert pro Kriegsjahr 1,8 bis 4,2 % der gegnerischen
Stärke, also bei gleich großen Stämmen mit a = 0,4 etwa 2,7 % des eigenen Volks (1,6 bis
3,8 %). Die Verschiebung der Achse ist klein (etwa −0,001 pro Jahr).
**Example:** t: V = 60, a = 0,4 → S = 54; o: V = 40, a = 0,2 → S = 28; u₁ = u₂ = 0,5:
k_o = runden(1,62) = 2, k_t = runden(0,84) = 1. L_t += 1/60, L_o += 2/40.

### 4. Eroberung

The Eroberung formula is defined as:

nach dem Kampf: `o ist erobert ⇔ V_o < 6 ∨ V_o < 0,22 · V_t`, sonst `t ist erobert ⇔ V_t < 6 ∨ V_t < 0,22 · V_o` · `j = floor(0,5 · V_L)` wechseln zum Sieger S, `V_L − j` sterben · Achse `x_S ← (x_S · (V_S + j) + x_L · j) / max(1, V_S + 2j)` mit V_S vor dem Wechsel

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| V_L, V_S | int | – | calculated | Volk des Eroberten und des Siegers nach dem Kampf |
| j | int | ≥ 0 | calculated | Überlebende, die zum Sieger wechseln |
| Eroberungsgrenzen | int, double | 6; 0,22 | data file | Prototyp 1 |

**Output Range:** Erobert wird, wer nach dem Kampf unter 6 Köpfen oder unter 22 % des
Gegners liegt. Die Mischformel gibt den Überlebenden weniger Gewicht als ihre Kopfzahl,
genau wie in Prototyp 1 (dort steht das schon erhöhte Volk des Siegers im Zähler und
Nenner).
**Example:** S: V = 80, x = 0,2; L: V = 15, x = −0,6. 15 < 17,6 → erobert; j = 7, 8 sterben;
S hat 87 Köpfe, x_S = (0,2 · 87 − 0,6 · 7) / 94 = 0,140426.

### 5. Frieden

The Frieden formula is defined as:

`Frieden ⇔ (L_t > 0,3 ∧ L_o > 0,3) ∨ (Jahr − Y_since > 18)` · danach `Frist_{t,o} = Jahr + 20`, `G_{t→o} = 0,2 Rache` und `G_{o→t} = 0,2 Rache`, sofern der Groll existiert

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| L_t, L_o | double | ≥ 0 | calculated | Verlustanteile (Formel 3) |
| Erschöpfung | double | 0,3 | data file | Prototyp 1 |
| Höchstdauer | int | 18 Jahre | data file | Prototyp 1 |
| Friedensfrist | int | 20 Jahre | data file | Prototyp 1 |

**Output Range:** Ein Krieg hat höchstens 20 Kampfjahre (gekämpft wird schon im Jahr der
Erklärung, Frieden frühestens nach dem Kampf des 19. Jahres danach). Bei 2,7 % Verlust pro
Jahr sind beide Seiten nach etwa 12 Jahren erschöpft. Nach dem Frieden liegt der Groll bei 0,2 und
verblasst; die Frist verhindert für 20 Jahre einen neuen Krieg zwischen beiden.
**Example:** Krieg seit Jahr 100, L_t = 0,31, L_o = 0,28 im Jahr 110: weiter. Im Jahr 119
(19 > 18): Frieden, Frist bis 139.

### 6. Abspaltung

The Abspaltung formula is defined as:

Kandidat, wenn `V_t > 44`, kein Krieg und weniger als 7 Stämme leben; Abspaltung, wenn `u < p`, mit `p = 0,06`, falls `V_t > 0,9 · cap_t`, sonst `p = 0,025` · Platz `= Welt.Platzsuche(m_t, r_t + 1, r_t + 6)` · `Teil = floor(V_t · (0,35 + 0,1 · u))` · Richtung `s = −sign(x_t)`, falls `|x_t| > 0,3`, sonst `s = +1`, falls `u < 0,5`, sonst −1 · `x_neu = clamp(x_t + 0,6 · s, −1, 1)` · `a_neu = clamp(a_t + (u − 0,5) · 0,2, 0,05, 0,95)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| p | double | 0,025 / 0,06 | data file | Wahrscheinlichkeit pro Jahr (Prototyp 1) |
| r_t | double | – | calculated | mit V_t vor der Abspaltung |
| Teil | int | 35–45 % von V_t | calculated | Köpfe des neuen Stamms (Auftrag an Menschen) |
| s | int | ±1 | calculated | Richtung des neuen Glaubens; +1 heißt freier (Richtung Lust-Sekte) |
| Würfe | – | – | calculated | in dieser Reihenfolge: u für p, die Würfe der Platzsuche, u für Teil, u für s (nur bei \|x_t\| ≤ 0,3), u für a_neu |

**Output Range:** Ein Stamm mit 64 Köpfen gibt 22 bis 28 ab. Ohne Gedränge spaltet sich ein
großer Stamm im Mittel alle 40 Jahre, gedrängt alle 17 Jahre (wenn nicht ein anderer
Stamm im selben Jahr zuerst an der Reihe ist). Fällt die Platzsuche aus, darf der nächste
Kandidat würfeln; seine Würfe kommen danach.
**Example:** V = 64, cap = 70 (gedrängt, 64 > 63), u = 0,03 < 0,06 → Abspaltung. Platz in 9 bis
14 Zellen Abstand. u = 0,6 → Teil = floor(64 · 0,41) = 26. x_t = 0,5 → s = −1 (kein Wurf),
x_neu = −0,1. a_t = 0,4, u = 0,25 → a_neu = 0,35.

### 7. Aggression

The Aggression formula is defined as:

Start `a = 0,25 + 0,25 · u` · jährlich `a_t ← clamp(a_t − 0,003 · x_t − (0,002, falls Lust-Sekte, sonst 0) + (u − 0,5) · 0,01, 0,05, 0,95)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| x_t | double | −1 bis 1 | calculated | Kulturachse dieses Jahres (Kultur rechnet vorher) |
| Gesellschaftsform | Aufzählung | – | calculated | Kultur & Erinnerung |

**Output Range:** Ein Gottesstaat mit x = −0,8 wird pro Jahr um etwa 0,0024 kampflustiger,
eine Lust-Sekte mit x = 0,8 um 0,0044 friedlicher; der Zufall streut um ±0,005.
**Example:** a = 0,4, x = 0,5, Lust-Sekte, u = 0,5 → 0,4 − 0,0015 − 0,002 = 0,3965.

### Kalibrierung gegen Prototyp 1

Alle Regeln und Werte sind die von Prototyp 1. Die Regler gelten als „fest bis zur ersten
AC-16-Messung“ (Welt & Insel, AC-16; hier AC-15). Vier Abweichungen:
1. Der Zufall kommt aus dem eigenen Strom statt aus dem gemeinsamen Generator; die
   Geschichten sind andere, ihre Verteilung soll gleich bleiben.
2. Im Prototyp liefen Ernte, Gnade und Kulturwandel Stamm für Stamm in einer Schleife;
   der Hunger-Groll eines Stamms sah dort den Vorrat mancher Nachbarn vor, mancher nach
   ihrer Ernte. Hier sieht er alle nach der Ernte (Welt rechnet zuerst).
3. Ein Stamm, der im selben Jahr erobert wurde, kämpft nicht weiter. Im Prototyp konnte
   ein eroberter Stamm mit kleinerer ID in derselben Schleife noch einmal „erobert“
   werden (mit 0 Überlebenden); das hat die Zahl der Eroberungen etwas erhöht.
4. Stirbt ein Stamm, enden die Kriege gegen ihn sofort für alle. Im Prototyp blieb ein
   solcher Krieg bei manchen Gegnern bis zu ihrem nächsten Kampf stehen und senkte noch
   eine Ernte um 15 % oder verhinderte eine Abspaltung.

## Edge Cases

- **If zwei Stämme sich im selben Jahr gegenseitig den Krieg erklären würden**: Die
  Erklärung des Stamms mit der kleineren ID kommt zuerst; danach besteht schon Krieg, und
  die zweite fällt aus. Es gibt ein Ereignis.
- **If ein Stamm gleichzeitig mehrere Kriege führt**: Jeder Krieg kämpft einmal pro Jahr
  in der Reihenfolge von Regel 19; die Verluste summieren sich. Wird er in einem davon
  erobert, kämpft er in den übrigen nicht mehr (Abweichung 3).
- **If der Kampf beide Stämme unter die Eroberungsgrenze drückt**: Geprüft wird zuerst
  der Gegner o (größere ID); er ist erobert, t ist Sieger, auch wenn t selbst klein ist.
  Fällt t danach unter 3 Köpfe, geht er in Schritt (8) unter.
- **If Menschen & Bevölkerung einen Verlust nicht vollständig ausführen kann** (Gehaltene,
  Beteiligte laufender Handlungen): Die Verlustanteile und die Eroberungsprüfung nutzen
  das V_t, das Menschen danach meldet, nicht die verlangte Zahl.
- **If ein eroberter oder untergegangener Stamm noch Menschen in Gottes Hand hat**: Regel
  18. Der Stamm gilt trotzdem als untergegangen, auch wenn Menschen kurz noch Köpfe
  zählt; sie gehen beim Loslassen in den neuen Stamm über.
- **If ein Stamm einen Krieg gegen einen erklärt, mit dem er eine Friedensfrist hat**: Das
  geschieht nicht; die Frist gilt in beide Richtungen.
- **If der Groll gegen einen Stamm aus einem Eingriff des Gottes kommt und der Eingriff
  nicht mehr in der Erinnerung ist**: Der Grund bleibt mit seiner Eingriffs-ID stehen,
  bis er verblasst; die Chronik nennt dann den Eingriff über seine ID (Simulationskern
  Regel 15).
- **If der hungernde Stamm keinen lebenden Nachbarn hat**: Es entsteht kein Hunger-Groll.
  Haben zwei Nachbarn denselben Wohlstand, gilt der mit der kleineren ID.
- **If 7 Stämme leben**: Es gibt keine Abspaltung, auch wenn ein Stamm gedrängt ist; er
  hungert, kämpft oder wartet, bis ein Stamm untergeht.
- **If die Abspaltung den Elternstamm unter 3 Köpfe drücken würde**: Unmöglich, er hat
  mehr als 44 und gibt höchstens 45 % ab.
- **If kein Stamm mehr lebt**: Das System rechnet nichts mehr; die Welt ist ausgestorben
  (Kennzahl in AC-15, in Prototyp 1 nie geschehen).

## Dependencies

**Hängt ab von:**

| System | Art | Was gebraucht wird | GDD |
|---|---|---|---|
| Simulationskern | hart | Jahrestakt O_year = 2, Zufallsstrom system_id 4, Aufträge, Ereignisse mit Ursache | ✓ `design/gdd/simulationskern.md` (In Review) |
| Welt & Insel | hart | Startmitten, Gebiet, cap_t, food_t, Hunger, Platzsuche; Aufträge „Gebiet neu berechnen“, „Stamm entsteht mit Vorrat x“ | ✓ `design/gdd/welt-und-insel.md` (In Review) |
| Menschen & Bevölkerung | hart | V_t; Aufträge Verlust, Wechsel, Stamm erloschen | ✓ `design/gdd/menschen-und-bevoelkerung.md` (In Review) |
| Kultur & Erinnerung | hart | x_t und Gesellschaftsform (lesend); Aufträge für neue Kultur, Achse verschieben, Achsen mischen | ✗ noch keins (vorläufiger Vertrag in diesem GDD) |
| Gotteskraft & Götterkräfte | weich | Auftrag „Groll“ für Eingriffe, die Nachbarn als Frevel sehen | ✗ noch keins (vorläufiger Vertrag) |

**Wird gebraucht von:** Welt & Insel (Mitten, Krieg, Aussterben), Menschen & Bevölkerung
(Aufträge), Kultur & Erinnerung (Ereignisse für die Erinnerung, neue Stämme), Harem &
Rituale (Stammeszugehörigkeit), Gotteskraft & Götterkräfte (wer sieht einen Eingriff als
Frevel), Zustimmungsmodell (Vertrag Regel 21), Chronik, Weltansicht & Kamera, Speichern &
Laden; später Ziel-Befehle und Sichtbarer Krieg (Vertical Slice). Welt & Insel,
Menschen & Bevölkerung und Zustimmungsmodell nennen dieses System schon.

**Verträge für GDDs, die noch fehlen:** Kultur & Erinnerung legt beim Auftrag „Kultur für
neuen Stamm anlegen“ Achse, Hingabe und Temperament an (im Prototyp: Startstämme
x = (u − 0,5) · 0,4 und Hingabe 0,4; abgespaltene Stämme x_neu und die Hingabe des
Elternstamms), führt die Verschiebung durch Kriegsangst im Ledger unter „Krieg“ und
erinnert sich an Kriege, Eroberungen und Abspaltungen. Gotteskraft legt fest, welcher
Stamm einen Eingriff als Frevel sieht (Prototyp 1: Blitz +0,35 bei x_o < 0,1, Fest-Segen
+0,4 bei x_o < −0,1, jeweils gegen den begünstigten oder getroffenen Stamm).

## Tuning Knobs

Alle Defaults bleiben bis zur ersten Kalibrierungsmessung (AC-15) auf den Werten von
Prototyp 1; die sicheren Bereiche gelten für Änderungen danach.

| Regler | Default | Sicherer Bereich | Wirkung |
|---|---|---|---|
| Verblassen des Grolls | 0,965 / Jahr | 0,94–0,98 | wie lange Groll nachwirkt |
| Grenzgroll (normal / gedrängt) | 0,007 / 0,02 | 0–0,04 | Streit um Land |
| Glaubensgroll | 0,022 · Δx ab Δx > 0,7, Reichweite + 8 | 0,01–0,04 | Glaubenskriege |
| Hunger-Groll | 0,14, Reichweite + 6 | 0–0,3 | Neid in der Hungerzeit |
| Abspaltungs-Groll | 0,35 | 0,1–0,5 | Feindschaft nach der Abspaltung |
| Kriegsschwelle | 0,6 | 0,4–1,2 | wie leicht Krieg ausbricht; größer heißt weniger Krieg |
| Größenverhältnis, Mindestvolk | 0,6; 12 | 0,4–1; 6–24 | wer sich Krieg zutraut |
| Kampfrate | 0,03 | 0,01–0,06 | wie blutig ein Kriegsjahr ist |
| Eroberungsgrenzen | 6; 0,22 | 3–10; 0,1–0,4 | wann ein Stamm erobert wird |
| Erschöpfung, Höchstdauer, Friedensfrist | 0,3; 18; 20 Jahre | 0,15–0,5; 8–30; 10–40 | wie lange Kriege und Frieden dauern |
| Abspaltung | V > 44, p 0,025 / 0,06, Teil 35–45 %, Achse ± 0,6 | – | wie oft neue Stämme entstehen |
| MAX_TRIBES | 7 | 4–10 | wie voll die Insel wird |
| Aggression | Start 0,25–0,5, Drift 0,003 · (−x) | – | Temperament der Stämme |

## Visual/Audio Requirements

Keine eigenen. Die Weltansicht zeigt Mitten, Grenzen (aus Welt & Insel) und Kriege
zwischen Stämmen (eine Linie oder Markierung, GDD Weltansicht & Kamera), Sichtbarer Krieg
(Vertical Slice) zeigt später Kämpfe.

## Game Feel

Die Stämme sollen wie Nachbarn mit Gedächtnis wirken: Eine alte Abspaltung schwelt noch
Jahre später, und ein Krieg kommt nicht aus dem Nichts, sondern hat einen Grund, den der
Spieler vorher an wachsendem Groll sehen kann.

## UI Requirements

- **Stamm ansehen:** Name, Gesellschaftsform (Kultur), Aggression als Wort (friedlich,
  wachsam, kriegerisch: unter 0,3, bis 0,6, darüber), laufende Kriege mit Dauer, Frieden
  bis Jahr, Groll gegen jeden Nachbarn als Stufe (ruhig unter 0,2, gespannt bis 0,5,
  feindlich darüber) mit dem größten Grund als Text
- **Meldungen** (über die Chronik): Krieg erklärt mit Grund, Frieden, Eroberung,
  Abspaltung mit Richtung, Untergang

> **📌 UX Flag — Stämme & Diplomatie**: This system has UI requirements. In Phase 4
> (Pre-Production), run `/ux-design` to create a UX spec for each screen or HUD
> element this system contributes to **before** writing epics. Stories that
> reference UI should cite `design/ux/[screen].md`, not the GDD directly.

## Cross-References

| Dokument | Was hier verwendet wird |
|---|---|
| `prototypes/weltengott-concept/prototype.html` | `newTribe`, `addGriev`, `GRIEV_TEXT`, Groll, Krieg, Kampf, Eroberung, Frieden, Abspaltung, Aussterben in `step`, Groll aus `applyPower` |
| `prototypes/weltengott-concept/REPORT.md` | Kalibrierung: Krieg 1620, Abspaltung 1008, Eroberung 838, Frieden 500, Kulturwandel 393, Untergang 1 in 1.000 Ären; 5,0 Stämme am Ende; Krieg ist zu eintönig |
| `design/gdd/simulationskern.md` | Regel 4 (Reihenfolge, Aufträge), 15 (Ursache), Formel 5 (O_year = 2), Formel 6 (Strom 4) |
| `design/gdd/welt-und-insel.md` | Regeln 5, 8, 9, 10, 13, Formeln 2 und 3, AC-16 |
| `design/gdd/menschen-und-bevoelkerung.md` | Regeln 2, 13, 19, 20, 23 |
| `design/gdd/zustimmungsmodell.md` | Regel 21 |

## Acceptance Criteria

Testwurzel: `Assets/Tests/EditMode/StaemmeUndDiplomatie/`. Kultur & Erinnerung und
Gotteskraft fehlen noch; die Tests nutzen die Stubs `ICultureSource` (x_t,
Gesellschaftsform, Aufrufprotokoll der Aufträge), `IPopulationSource` (V_t, führt Verluste
und Wechsel aus) und `IWorldSource` (Mitten, cap_t, food_t, Hunger, Platzsuche).

- **AC-01 Formel 1 [Logic/EditMode]** **GIVEN** ein Groll mit Grenze 0,2 und Rache 0,1, **WHEN** ein Jahr ohne neuen Groll vergeht, **THEN** sind es 0,193 und 0,0965 (Summe 0,2895); **GIVEN** 300 Jahre gedrängte Berührung, **THEN** liegt der Grenzgroll bei 0,5714 ± 0,001; **GIVEN** der Gegner stirbt, **THEN** ist der Groll gegen ihn im nächsten Jahr gelöscht.
- **AC-02 Formel 1, Auslöser [Logic/EditMode]** **GIVEN** zwei Mitten im Abstand 10 mit r = 7,2 und 3,5, **THEN** entsteht Grenzgroll (10 < 10,7); im Abstand 11 nicht; **GIVEN** Δx = 0,9 und Abstand 18, **THEN** entsteht Glaubensgroll 0,0198 (18 < 18,7); bei Δx = 0,7 nicht; **GIVEN** t hungert und zwei Nachbarn haben food/V = 1,2 und 1,2, **THEN** bekommt nur der mit der kleineren ID 0,14 Hunger-Groll.
- **AC-03 Formel 2 [Logic/EditMode]** **GIVEN** G = 0,8, a = 0,4, V_t = 20, V_o = 33, **WHEN** Schritt (5) läuft, **THEN** gibt es Krieg mit dem größten Grund als Grund; mit V_o = 34, mit V_t = 11 oder mit einer Friedensfrist bis zu diesem Jahr + 1 nicht; **GIVEN** der größte Grund ist „Gott“ mit Eingriffs-ID 77, **THEN** trägt das Ereignis die Ursache 77.
- **AC-04 Formel 2, gegenseitig [Logic/EditMode]** **GIVEN** zwei Stämme erfüllen im selben Jahr gegeneinander die Bedingung, **THEN** entsteht genau ein Krieg, erklärt vom Stamm mit der kleineren ID.
- **AC-05 Formel 3 [Logic/EditMode, mit Stubs]** **GIVEN** t: V = 60, a = 0,4; o: V = 40, a = 0,2; Würfe 0,5 und 0,5, **WHEN** gekämpft wird, **THEN** erhält Menschen „Verlust 2“ für o und „Verlust 1“ für t, L_t = 1/60, L_o = 2/40, Kultur erhält zwei Aufträge „Achse verschieben“ mit −0,04/59 und −0,08/38, und beide haben +0,01 Rache; **GIVEN** S · 0,03 · (0,6 + 0,8u) = 2,5, **THEN** ist der Verlust 3.
- **AC-06 Formel 4 [Logic/EditMode]** **GIVEN** S: V = 80, x = 0,2 und L: V = 15, x = −0,6 nach dem Kampf, **WHEN** die Eroberung geprüft wird, **THEN** wechseln 7, sterben 8, Kultur erhält „Achsen mischen“ mit dem Ergebnis 0,140426, L lebt nicht mehr, es entsteht ein Ereignis „Eroberung“ und kein „Untergang“; **GIVEN** beide unter der Grenze, **THEN** ist der Stamm mit der größeren ID erobert.
- **AC-07 Formel 4, kein zweites Mal [Logic/EditMode]** **GIVEN** Stamm 1 führt Kriege gegen 2 und 3 und wird von 2 erobert, **WHEN** Schritt (6) weiterläuft, **THEN** kämpft Stamm 1 nicht gegen 3, es gibt genau ein Ereignis „Eroberung“, und Stamm 3 ist danach nicht mehr im Krieg.
- **AC-08 Formel 5 [Logic/EditMode]** **GIVEN** ein Krieg seit Jahr 100, **WHEN** im Jahr 110 L = 0,31 und 0,28 ist, **THEN** geht er weiter; im Jahr 119 ist Frieden, die Frist läuft bis 139, der Groll beider ist 0,2 Rache; **GIVEN** L = 0,31 und 0,31, **THEN** ist sofort Frieden.
- **AC-09 Formel 6 [Logic/EditMode, mit Stubs]** **GIVEN** V = 64, cap = 70, x = 0,5, a = 0,4 und die Würfe 0,03, (Platzsuche), 0,6, 0,25, **WHEN** Schritt (7) läuft, **THEN** fordert dieses System die Platzsuche mit 9 und 14 an, Menschen erhält „26 Köpfe wechseln“, Kultur „neue Kultur mit x = −0,1“, der neue Stamm hat a = 0,35, beide haben 0,35 Abspaltungs-Groll, und es wird kein Wurf für die Richtung gezogen; **GIVEN** die Platzsuche liefert „kein Platz“, **THEN** würfelt der nächste Kandidat; **GIVEN** 7 lebende Stämme, **THEN** wird gar nicht gewürfelt.
- **AC-10 Formel 7 [Logic/EditMode]** **GIVEN** a = 0,4, x = 0,5, Lust-Sekte, u = 0,5, **THEN** ist a danach 0,3965; **GIVEN** a = 0,05, x = 1, u = 0, **THEN** bleibt a = 0,05.
- **AC-11 Regel 17 [Logic/EditMode]** **GIVEN** ein Stamm fällt durch Hunger auf 2 Köpfe, **WHEN** Schritt (8) läuft, **THEN** lebt er nicht mehr, Menschen erhält „Stamm erloschen“, Welt „Gebiet neu berechnen“, alle Kriege gegen ihn sind beendet, ohne Friedensfrist, und es gibt ein Ereignis „Untergang“.
- **AC-12 Regel 18 [Integration/EditMode, mit Menschen]** **GIVEN** ein Einzelner des Stamms L ist in Gottes Hand, als L erobert wird, **WHEN** er losgelassen wird, **THEN** gehört er dem Sieger mit gemerktem Stammeswechsel; **GIVEN** bei einem Untergang liegen zwei Mitten gleich weit von seiner Zelle, **THEN** gehört er dem Stamm mit der kleineren ID.
- **AC-13 Regel 19 [Logic/EditMode, mit Stubs]** **GIVEN** 3 Stämme mit Groll, einem Krieg und einem Abspaltungskandidaten, **WHEN** ein Jahr läuft, **THEN** ist die Reihenfolge der Schritte (1) bis (8) und der Würfe wie in Regel 19 und Formel 6, und zwei Läufe mit demselben Startwert ergeben denselben Zustands-Hash.
- **AC-14 Speichern [Logic/EditMode]** **GIVEN** ein eingespielter Zustand mit Groll aus allen sechs Gründen, zwei Kriegen, einer Friedensfrist und einem untergegangenen Stamm, **WHEN** gespeichert und geladen wird, **THEN** gleichen die Hashes der Jahre 301 bis 600 denen eines durchgehenden Laufs.
- **AC-15 Kalibrierung [Integration/EditMode, ADVISORY bis zur ersten Messung]** **GIVEN** die Startwerte 0 bis 99, je 10 Ären ohne Eingriff, mit allen MVP-Systemen und den Debug-Werkzeugen, **WHEN** sie laufen, **THEN** liegen Kriege, Abspaltungen, Eroberungen und Frieden pro Ära bei 1,62 / 1,01 / 0,84 / 0,50 ± 15 % und die mittlere Zahl der Stämme am Ende bei 5,0 ± 0,5 wie in Prototyp 1 (Eroberungen eher darunter, Abweichung 3); als Kennzahl (keine Hürde) wird das Verhältnis Krieg zu Kulturwandel gemeldet (Prototyp 1: 4,1 zu 1).
- **AC-16 Regel 3 [Logic/EditMode]** **GIVEN** Stammes-IDs 0, 13, 14 und 29, **THEN** heißen sie Ura, Tharn, Ura II und Mirr III.

## Open Questions

| Frage | Wer klärt | Bis wann |
|---|---|---|
| **Krieg überdeckt den Kulturwandel** (Prototyp 1: 1620 Kriege gegen 393 Kulturwechsel, „Krieg, Frieden, Krieg“ war eintönig). Mögliche Hebel nach der Kalibrierung: höhere Kriegsschwelle, längere Friedensfrist, Groll, der statt Krieg auch Abspaltung oder Kulturwandel auslöst, mehr Ereignisarten (Bündnis, Handel, Heirat). Kennzahl: Verhältnis Krieg zu Kulturwandel aus AC-15 | Entwickler, dann Messung | nach der ersten AC-15-Messung |
| Bündnisse, Handel oder Tribut als Diplomatie über Krieg und Frieden hinaus? Das Systemverzeichnis nennt „Beziehungen“; Prototyp 1 hatte nur Groll | Entwickler | vor dem Vertical Slice |
| Darf die Mitte eines Stamms wandern (weg von verbrannter Erde, hin zu fruchtbarem Land)? Heute fest wie im Prototyp | Entwickler | nach der Kalibrierung |
| Was geschieht mit den letzten 1–2 Menschen eines untergegangenen Stamms (heute verschwinden sie, V = 0)? Möglich wäre, dass sie zum nächsten Stamm gehen | Entwickler | mit dem GDD Kultur & Erinnerung |
| Kampf und Gefangenschaft einzelner Menschen (Merkmal „im Kampf“, Gefangene) | GDD Sichtbarer Krieg | Vertical Slice |
| Ist Regel 18 (wer in Gottes Hand ist, geht beim Loslassen an den Sieger oder den nächsten Stamm) so gewollt? | Entwickler, dann GDD Gotteshand & Ragdoll | mit dem GDD |
