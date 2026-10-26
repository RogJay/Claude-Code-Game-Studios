# Stämme & Diplomatie

> **Status**: In Review, Review-Runde 1 überarbeitet (Nachtarbeit 2026-10-26, ohne Freigabe des Entwicklers; jede Entscheidung steht in `production/nachtarbeit-2026-10-26.md`)
> **Author**: Claude (autonom, mit Erlaubnis des Entwicklers)
> **Last Updated**: 2026-10-26
> **Last Verified**: 2026-10-26
> **Implements Pillar**: Pillar 1 (Die Welt lebt ohne dich), Pillar 2 (Jeder Eingriff hinterlässt eine Spur)

## Summary

Stämme & Diplomatie führt die Stämme als Gemeinschaften: ihre Mitte, ihr Temperament
(Aggression), ihren Groll aufeinander, Kriege, Frieden, Eroberung, Abspaltung und
Aussterben. Krieg ist im MVP-0 abstrakt: Verluste als Zahlen, keine sichtbaren Kämpfe.
Die Regeln sind genau die aus Prototyp 1, damit dessen Messwerte gelten; die Chronik
führt einen Krieg auf den Spieler zurück, wann immer sein Eingriff den Ausschlag gab.

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
Groll. Gab er den Ausschlag für einen Krieg, nennt die Chronik ihn als Ursache
(Pillar 2).

## Player Fantasy

Der Spieler sieht Völker mit eigenen Beweggründen. Er versteht, warum zwei Stämme
Krieg führen („Streit um Land an der Grenze“, „ihr Glaube ist dem anderen ein Gräuel“),
und er erlebt, dass sein Segen für den einen bei den strenggläubigen Nachbarn Neid und
Hass weckt. Er sieht die Spannung wachsen, bevor der Krieg kommt, und er fragt sich vor
jedem Eingriff: Wer wird das sehen, und wie werden sie es deuten?

## Detailed Design

### Core Rules

*Entwurf, 1:1 aus Prototyp 1 (`prototype.html`, Zeilen 155–437), überarbeitet nach
Review-Runde 1 (`systems-designer`, `game-designer`, `qa-lead`, `creative-director`).
Abweichungen stehen unter „Kalibrierung gegen Prototyp 1“.*

**Der Stamm**

1. Ein Stamm hat: Stammes-ID (fortlaufend aus dem Zähler dieses Systems, nie
   wiederverwendet), Mitte (eine Landzelle), Aggression a_t, lebt ja/nein,
   Geburtsjahr, Elternstamm (oder keiner), Todesjahr und **Nachfolger** (Regel 18; nur
   bei untergegangenen Stämmen), Groll gegen jeden anderen Stamm, laufende Kriege und
   Friedensfristen. Der Name folgt aus der ID (Regel 3). Volk V_t gehört Menschen &
   Bevölkerung; Gebiet, Vorrat, Tragfähigkeit und die Merkmale „hungert in diesem Jahr“
   gehören Welt & Insel; Kulturachse x_t, Gesellschaftsform und Stammeshingabe gehören
   Kultur & Erinnerung. Dieses System liest sie nur.
2. Die **Mitte** eines Stamms ändert sich nie (wie in Prototyp 1). Die Startmitten und
   die IDs 0 und 1 legt Welt & Insel fest (Welt Regel 5); der ID-Zähler dieses Systems
   beginnt danach bei 2. Die Mitte eines abgespaltenen Stamms legt dieses System mit
   der Platzsuche fest (Regel 15).
3. Der **Name** ist `NAMES[id mod 14]` aus der Liste von Prototyp 1 (Ura, Mirr, Kesh, Talo,
   Vend, Oru, Sela, Brak, Ilm, Daru, Feno, Ask, Lumi, Tharn); ab ID 14 mit angehängter
   Zahl `n = (id div 14) + 1`, als römische Ziffer bis 3999, darüber als arabische Zahl
   („Ura II“ für ID 14). Die Liste ist eine Datendatei.
4. Höchstens **7 Stämme** leben zugleich (MAX_TRIBES); das begrenzt nur Abspaltungen.

**Groll**

5. Der Groll G_{t→o} von Stamm t gegen Stamm o ist die Summe seiner **Gründe**: Grenze,
   Hunger, Glaube, Abspaltung, Rache und je Eingriff des Gottes ein eigener Grund „Gott“
   mit der Eingriffs-ID. Groll ist gerichtet: G_{t→o} und G_{o→t} sind verschiedene Werte.
   Gründe sind in fester Reihenfolge gespeichert und summiert (Grenze, Hunger, Glaube,
   Abspaltung, Rache, dann Gott nach aufsteigender Eingriffs-ID), damit der
   Zustands-Hash eindeutig ist.
6. Jedes Jahr verblasst jeder Grund um den Faktor 0,965 (Formel 1). Groll gegen einen
   Stamm, der nicht mehr lebt, wird gelöscht.
7. Groll entsteht aus (Formel 1): einer gemeinsamen **Grenze** (die Reichweiten berühren
   sich), **Hunger** (ein hungernder Stamm neidet dem bestgenährten anderen Stamm der
   Insel, aber nur, wenn dieser nah genug ist; ein ärmerer Nachbar in der Nähe zählt
   nicht, wie in Prototyp 1), **Glaube** (die Kulturachsen liegen weit auseinander), einer
   **Abspaltung** (beide Seiten), **Rache** (jedes Kriegsjahr, beide Seiten, einmal in
   Schritt 6) und Eingriffen des Gottes, die ein anderer Stamm als Frevel sieht (Auftrag
   „Groll“ von Gotteskraft & Götterkräfte).

**Krieg**

8. Ein Stamm t erklärt o den **Krieg**, wenn sein Groll mal seine Kampfeslust groß
   genug ist, er nicht zu klein ist, nicht schon gegen o kämpft und keine Friedensfrist
   gegen o läuft (Formel 2). Krieg gilt immer für beide Seiten. Das Ereignis nennt den
   **Grund** (die größte Kategorie, wobei alle Gründe „Gott“ zusammen als eine Kategorie
   zählen), die vollständige Liste der Gründe mit ihren Werten und, falls ein Eingriff
   des Gottes den Ausschlag gab, dessen Eingriffs-ID als **Ursache** (Formel 2,
   Simulationskern Regel 15). Dann schreibt die Chronik „weil du …“. Die Zuordnung
   ändert nichts an der Simulation, nur an der Erklärung.
9. Jedes Kriegsjahr kämpft jedes Paar genau einmal, im selben Jahr wie die Erklärung
   beginnend: Beide verlieren Köpfe nach der Stärke des Gegners vor dem Kampf (Formel 3).
   Die Verluste setzt dieses System als Aufträge „Verlust k“ an Menschen & Bevölkerung,
   zuerst für o, dann für t; Menschen wählt die Opfer (Menschen Regel 13). Die Angst vor
   den Verlusten verschiebt danach die Kulturachse beider Seiten zum Strengen (Aufträge
   an Kultur & Erinnerung, zuerst t, dann o; ein Auftrag mit dem Wert 0 entfällt), und
   beide bekommen Rache-Groll.
10. Ist ein Stamm nach dem Kampf zu klein (Formel 4), wird er **erobert**. In dieser
    Reihenfolge: j = die Hälfte seiner Köpfe (abgerundet) wechselt zum Sieger (Auftrag
    an Menschen), die übrigen V_L − j sterben (Auftrag „Verlust“), der Eroberte ist
    untergegangen mit dem Sieger als Nachfolger (Auftrag „Stamm erloschen“, Regel 17).
    Die Kulturachse des Siegers mischt sich mit der des Eroberten, gewichtet ungefähr
    nach Köpfen, wobei die Überlebenden etwas weniger zählen als ihre Kopfzahl
    (Formel 4, wie in Prototyp 1); der Auftrag „Achsen mischen“ trägt x_S, V_S vor dem
    Wechsel, j und x_L.
11. Hat im laufenden Krieg jede Seite für sich mehr als 30 % verloren (Summe ihrer
    jährlichen Verlustanteile) oder dauert der Krieg länger als 18 Jahre, schließen sie
    **Frieden** (Formel 5), geprüft nach dem Kampf dieses Jahres. Bis zum Ablauf der
    Frist im Jahr Y + 20 erklärt keiner dem anderen den Krieg (19 Jahre ohne Erklärung).
    Ein bestehender Groll zwischen beiden wird **ganz** durch den Grund „Rache 0,2“
    ersetzt, auch Gründe „Gott“; fehlt der Groll, entsteht keiner. Die Spur des Spielers
    bleibt im Ereignis der Kriegserklärung und in Kultur & Erinnerung.
12. Ein Stamm ist **im Krieg**, wenn er mindestens einen Krieg gegen einen lebenden
    Stamm führt. Welt & Insel liest das für die Ernte (Welt Regel 13). Stirbt ein Stamm,
    enden alle Kriege gegen ihn sofort, ohne Frieden und ohne Friedensfrist.
13. Im MVP-0 ist Krieg abstrakt. Es gibt keine Kämpfe einzelner Menschen, kein Merkmal
    „im Kampf“ und keine Gefangenen; die Regeln dafür in Menschen & Bevölkerung (Regel 19,
    Zustand „im Kampf“) bleiben bis zum System Sichtbarer Krieg (Vertical Slice)
    ungenutzt. Damit erfüllt dieses System den Vertrag aus Zustimmungsmodell Regel 21
    (niemand wird aus einer laufenden Handlung in Kampf oder Gefangenschaft gezogen)
    schon dadurch, dass es niemanden zieht; Kriegstote wählt Menschen, das Beteiligte
    laufender Handlungen nie zieht.

**Aggression**

14. Jeder Stamm hat eine Aggression a_t zwischen 0,05 und 0,95. Startstämme würfeln sie
    beim Erzeugen zwischen 0,25 und 0,5 (zuerst ID 0, dann ID 1); ein abgespaltener Stamm
    erbt sie mit einer kleinen Abweichung. Jedes Jahr wandert sie: Strenggläubige werden
    kampflustiger, eine Lust-Sekte friedlicher (Formel 7).

**Abspaltung**

15. Ein Stamm mit mehr als 44 Köpfen, der keinen Krieg führt, kann sich abspalten, wenn
    weniger als 7 Stämme leben. Ist er gedrängt (mehr Köpfe als 90 % seiner
    Tragfähigkeit), ist das wahrscheinlicher (Formel 6). Die neue Mitte sucht die
    Platzsuche von Welt & Insel (Welt Formel 2) mit dem Zufallsstrom dieses Systems.
    Findet sie keinen Platz oder liegt der Platz auf der Mitte eines lebenden Stamms,
    fällt die Abspaltung aus, bevor der Teil gewürfelt wird, und der nächste Kandidat
    darf es versuchen. Weil eine Mitte immer ihrem Stamm gehört (Welt Regel 7), bekommt
    der neue Stamm dann immer mindestens seine Mitte; Welt Regel 10 („bekäme keine
    Zelle“) tritt darum nie ein. Höchstens eine Abspaltung pro Jahr; danach würfelt
    kein Kandidat mehr.
16. Bei einer Abspaltung wechselt ein Teil der Köpfe (35 bis 45 %, abgerundet) in den
    neuen Stamm. Er glaubt anders: Seine Kulturachse liegt 0,6 vom Elternstamm entfernt,
    zur Mitte hin, wenn der Elternstamm deutlich ausgerichtet ist, sonst zufällig in eine
    Richtung (Formel 6). Aufträge in dieser Reihenfolge: Menschen „Teil wechselt zum
    neuen Stamm“, Kultur „Kultur anlegen (x_neu, Hingabe des Elternstamms)“, Welt
    „Stamm entsteht mit Vorrat 0,5 · Teil“ (Welt rechnet dabei das Gebiet neu). Beide
    Stämme bekommen Abspaltungs-Groll gegeneinander.

**Aussterben**

17. Ein Stamm mit weniger als 3 Köpfen ist **untergegangen** (wie Prototyp 1, Menschen
    Regel 2, Welt Regel 8), geprüft in Schritt 8. Dieses System setzt „lebt“ auf nein,
    Todesjahr und Nachfolger (Regel 18), meldet das Ereignis „Untergang“ und erteilt
    Menschen & Bevölkerung den Auftrag „Stamm erloschen“: Die Masse verschwindet, jeder
    Einzelne des Stamms stirbt mit dem Ereignis „Tod“ und dem Untergang als Ursache
    (Menschen Regel 15); wer in Gottes Hand ist oder an einer laufenden intimen Handlung
    teilnimmt, stirbt nicht, sondern geht an den Nachfolger über, sobald er frei ist
    (Regel 18). Bis dahin zählt V_t nur diese. Ein eroberter Stamm geht ohne eigenes
    Ereignis „Untergang“ unter; das Ereignis „Eroberung“ nennt ihn.
18. Der **Nachfolger** eines untergegangenen Stamms wird beim Untergang festgelegt und
    gespeichert: bei einer Eroberung der Sieger, sonst der lebende Stamm, dessen Mitte
    der Mitte des Untergegangenen am nächsten liegt (kleinster quadrierter Abstand in
    ganzen Zellen, bei Gleichstand die kleinere ID); lebt keiner, gibt es keinen. Wer bei
    einer Eroberung oder einem Untergang nicht gezogen werden darf (in Gottes Hand,
    Beteiligter einer laufenden Handlung), gehört, sobald er frei ist, dem Nachfolger;
    ist der auch untergegangen, dessen Nachfolger und so fort bis zu einem lebenden
    Stamm. Gibt es keinen, stirbt er dann. Das gilt als Stammeswechsel (Menschen
    Regel 20, Zustimmungsmodell Regel 4). Menschen liest den Nachfolger direkt.

**Reihenfolge im Schritt**

19. Stämme & Diplomatie ist ein Jahressystem mit Versatz O_year = 2 und rechnet im
    selben Schritt nach Menschen & Bevölkerung (Simulationskern Formel 5). Reihenfolge,
    jeweils über die lebenden Stämme in aufsteigender ID, innerhalb eines Stamms über
    die anderen Stämme in aufsteigender ID: (1) Groll verblasst, Groll gegen Tote wird
    gelöscht; (2) Aggression wandert; (3) Hunger-Groll; (4) Grenz- und Glaubensgroll über
    alle geordneten Paare; (5) Kriegserklärungen; (6) je Kriegspaar, ausgelöst vom Stamm
    mit der kleineren ID: Kampf, dann Eroberung, dann Frieden; ein in diesem Schritt
    eroberter Stamm kämpft nicht weiter; (7) Abspaltung; (8) Aussterben. Jede Änderung
    an einem Stamm (Eroberung, Abspaltung, Untergang) erteilt sofort den Auftrag
    „Gebiet neu berechnen“ an Welt & Insel (Welt Regel 9). Die Merkmale von Welt
    („hungert in diesem Jahr“, food_t, cap_t) stammen aus dem Lauf von Welt bei
    Versatz 0 desselben Jahres und gelten bis zu dessen nächstem Lauf.

### States and Transitions

| Zustand eines Stamms | Bedeutung | Wechsel |
|---|---|---|
| **Lebt, in Frieden** | keine laufenden Kriege | → im Krieg (eigene oder fremde Erklärung) · → untergegangen (unter 3 Köpfen) |
| **Lebt, im Krieg** | mindestens ein Krieg gegen einen lebenden Stamm | → in Frieden (alle Kriege beendet) · → erobert · → untergegangen |
| **Untergegangen** | lebt nicht mehr; ID, Name, Mitte (als Ruine), Todesjahr und Nachfolger bleiben für Chronik, Erinnerung und Menschen | endgültig |

| Zustand eines Paars (t, o) | Bedeutung | Wechsel |
|---|---|---|
| **Neutral** | kein Krieg, keine Frist | → Krieg (Formel 2) |
| **Krieg** | seit Jahr Y_since, Verlustanteile L_t, L_o | → Friedensfrist (Formel 5) · → endet ohne Frist, wenn einer stirbt |
| **Friedensfrist** | bis Jahr Y + 19 keine Erklärung zwischen beiden | → neutral im Jahr Y + 20 |

### Interactions with Other Systems

| System | Was hineingeht | Was herauskommt | Wer besitzt die Schnittstelle |
|---|---|---|---|
| Simulationskern | Jahrestakt (O_year = 2, nach Menschen), Zufallsstrom system_id 4, Erzeugen nach Kultur & Erinnerung | Ereignisse: Krieg erklärt (Grund, Liste der Gründe, Ursache), Frieden, Eroberung, Abspaltung (mit Richtung), Untergang | Kern |
| Welt & Insel | Startmitten und IDs 0 und 1, Gebiet, Zellenzahl, cap_t, food_t, „hungert in diesem Jahr“; Platzsuche (Welt Formel 2, mit dem Strom dieses Systems) | Mitten, Kriegszustand, Aussterben; Aufträge „Gebiet neu berechnen“, „Stamm entsteht mit Vorrat x“ | Welt & Insel (Gebiet), dieses System (Mitten, Aussterben) |
| Menschen & Bevölkerung | V_t je Stamm (lesend, nach jedem Auftrag neu) | Aufträge „Verlust k“, „n Köpfe wechseln von A zu B“ (Abspaltung, Eroberung), „Stamm erloschen“; Nachfolger untergegangener Stämme (lesend) | Menschen (Opferwahl, Tod), dieses System (Zahlen, Nachfolger) |
| Kultur & Erinnerung | Kulturachse x_t, Gesellschaftsform (lesend, Stand nach dem Lauf von Kultur in diesem Jahr) | Aufträge „Kultur anlegen (x, Hingabe)“, „Achse verschieben (Wert, Ursache Krieg)“, „Achsen mischen (x_S, V_S, j, x_L)“; Ereignisse für die Erinnerung | Kultur & Erinnerung (vorläufig, GDD fehlt) |
| Gotteskraft & Götterkräfte | Auftrag „Groll (von o gegen t, Wert, Eingriffs-ID)“ | Liste der Stämme mit Mitte und Achse, damit Gotteskraft festlegt, wer einen Eingriff als Frevel sieht | Gotteskraft (vorläufig, GDD fehlt) |
| Zustimmungsmodell | – | Vertrag Regel 21 erfüllt (Regel 13 hier) | Zustimmungsmodell |
| Chronik | – | Ereignisse mit Grund, Gründen und Ursache, Namen der Stämme, Textvorlagen (Formel 2) | Kern-Ereignisformat |
| Weltansicht & Kamera, HUD | – | Mitten, Namen, Kriege, Friedensfristen, Kriegsnähe je gerichtetem Paar mit Hindernis, eingehender Groll | dieses System |
| Speichern & Laden | kompletter Zustand | alle Felder aus Regel 1, Grund und Wert jedes Grolls in fester Reihenfolge, Kriege mit Beginn und Verlustanteilen, Friedensfristen, ID-Zähler | Speichern & Laden |

## Formulas

Übernahme aus `prototypes/weltengott-concept/prototype.html` (`addGriev`, Groll-Teil
und Krieg, Frieden, Abspaltung und Aussterben in `step`). Alle Zufallszahlen kommen aus
dem Strom system_id 4 (`NextDouble()` = u, Simulationskern Formel 6), in der Reihenfolge
von Regel 19. „runden“ heißt `floor(x + 0,5)` wie `Math.round` im Prototyp; Abstände sind
euklidische Abstände der Mitten; `r_t = 2 + 0,75 · sqrt(V_t)` für jedes V_t ≥ 0 (wie im
Prototyp; Welt Formel 3 nutzt es nur ab 3 Köpfen für das Gebiet). Wohlstand ist
`food_t / V_t`, bei V_t = 0 gleich 0 (Welt & Insel, Interaktionen). Toleranz in Tests
1e-9.

### 1. Groll

The Groll formula is defined as:

`G_{t→o} = Σ_Gründe g` · jährlich `g ← 0,965 · g` für jeden Grund · Zuwachs je Jahr: **Grenze** bei `|m_t − m_o| < r_t + r_o`: `+0,02`, falls `V_t > 0,9 · cap_t`, sonst `+0,007` · **Glaube** `+0,022 · Δx`, wenn `Δx = |x_t − x_o| > 0,7` und `|m_t − m_o| < r_t + r_o + 8` · **Hunger** falls t in diesem Jahr hungert: R = der Stamm o ≠ t mit dem größten Wohlstand (bei Gleichstand die kleinere ID), `+0,14` gegen R, falls `|m_t − m_R| < r_t + r_R + 6`, sonst nichts · **Abspaltung** `+0,35` beidseitig · **Rache** `+0,01` beidseitig je Kampf (Formel 3) · **Gott** Wert aus dem Auftrag

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| g | double | ≥ 0 | calculated | Wert eines Grunds |
| m_t | Zelle | – | calculated | Mitte von Stamm t |
| r_t | double | ≥ 2 | calculated | Reichweite |
| V_t | int | ≥ 0 | calculated | Volk (Menschen & Bevölkerung) |
| cap_t, food_t | double | ≥ 0 | calculated | Welt & Insel, Stand nach der Ernte dieses Jahres |
| hungert | bool | – | calculated | Welt & Insel, in diesem Jahr |
| x_t | double | −1 bis 1 | calculated | Kulturachse (Kultur & Erinnerung) |
| Δx | double | 0–2 | calculated | Abstand der Achsen |
| Konstanten | double | 0,965; 0,9; 0,007; 0,02; 0,022; 0,7; 8; 0,14; 6; 0,35; 0,01 | data file | Prototyp 1 |

**Output Range:** Ein Grund, der jedes Jahr gleich wächst, strebt gegen `Zuwachs / 0,035`:
Grenze 0,2 (gedrängt 0,57), Glaube bei Δx = 1,5 etwa 0,94, bei Δx = 2 etwa 1,26, Hunger
höchstens 4. Abspaltungs-Groll fällt in 20 Jahren von 0,35 auf 0,17. Groll ist nie negativ.
**Example:** Zwei Nachbarn berühren sich, t ist gedrängt, Δx = 0,9: Nach einem Jahr hat t
0,02 + 0,0198 = 0,0398 Groll gegen o; nach sehr vielen Jahren etwa 0,57 + 0,57 = 1,14.

### 2. Kriegserklärung und Zuordnung

The Kriegserklärung formula is defined as:

`t erklärt o den Krieg ⇔ G_{t→o} · (0,5 + a_t) > 0,6 ∧ V_t ≥ 0,6 · V_o ∧ V_t ≥ 12 ∧ kein Krieg zwischen t und o ∧ Jahr ≥ Frist_{t,o}` · **Grund** = die Kategorie mit der größten Summe, wobei alle Gründe „Gott“ eine Kategorie bilden (Gleichstand: Grenze, Hunger, Glaube, Abspaltung, Rache, Gott) · **Ursache** = die Eingriffs-ID des größten einzelnen Grunds „Gott“ (Gleichstand: kleinere ID), falls Grund = Gott **oder** `(G_{t→o} − G_Gott) · (0,5 + a_t) ≤ 0,6`; sonst keine

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| a_t | double | 0,05–0,95 | calculated | Aggression |
| Kriegsschwelle | double | 0,6 | data file | Prototyp 1 |
| Größenverhältnis | double | 0,6 | data file | t muss mindestens 60 % von o haben |
| Mindestvolk | int | 12 | data file | kleinere Stämme erklären keinen Krieg |
| Frist_{t,o} | int | Jahr | calculated | Ende der Friedensfrist (gleich für beide Richtungen) |
| G_Gott | double | ≥ 0 | calculated | Summe aller Gründe „Gott“ |
| Kriegsnähe | double | ≥ 0 | calculated | `G_{t→o} · (0,5 + a_t) / 0,6`, nur für die Anzeige; ab 1 kriegsbereit |

**Output Range:** Bei a_t = 0,4 genügt ein Groll über 0,67, bei a_t = 0,05 erst über 1,09,
bei a_t = 0,95 schon über 0,41. Grenzgroll allein (höchstens 0,57) reicht nur einem
gedrängten, sehr kampflustigen Stamm (a_t > 0,55). Grund und Ursache ändern kein Ergebnis
der Simulation.
**Example:** G = 0,8, a_t = 0,4 → 0,72 > 0,6; t hat 20 Köpfe, o hat 33 (0,6 · 33 = 19,8):
Krieg. Hätte o 34 Köpfe (20,4), gäbe es keinen. **Zuordnung:** Glaube 0,63 und drei Segen
0,4 (ID 77), 0,3 (ID 80), 0,2 (ID 91): Gott zählt 0,9 > 0,63, Grund Gott, Ursache 77. Glaube
0,63 und ein Segen 0,1 (ID 77), a = 0,4: 0,73 · 0,9 = 0,657 > 0,6, Grund Glaube; ohne den
Segen 0,567 ≤ 0,6, also Ursache 77: „Grund: ihr Glaube …, weil du …“.

**Textvorlagen der Chronik** (Datendatei; die Chronik setzt Namen ein):

| Grund | Vorlage |
|---|---|
| Grenze | „Streit um Land an der Grenze zwischen {t} und {o}“ |
| Hunger | „Neid von {t} auf die vollen Speicher von {o} in der Hungerzeit“ |
| Glaube | „der Glaube von {o} ({Form o}) ist {t} ({Form t}) ein Gräuel“ |
| Abspaltung | „alter Groll seit der Abspaltung von {Kind} aus {Eltern}“ |
| Rache | „Rache für die Toten des Kriegs von {Jahr}“ (Jahr des letzten Kriegs zwischen beiden) |
| Gott | „weil du {Eingriff} hast“ (Text des Eingriffs aus der Chronik) |

### 3. Kampfverluste

The Kampf formula is defined as:

`S_t = V_t · (0,5 + a_t)`, `S_o` ebenso, beide vor dem Kampf · `k_o = min(V_o, runden(S_t · 0,03 · (0,6 + 0,8 · u₁)))` · `k_t = min(V_t, runden(S_o · 0,03 · (0,6 + 0,8 · u₂)))` · `L_t ← L_t + k_t / max(1, V_t)` mit V_t vor dem Kampf · Achse `x_t ← x_t − 0,04 · k_t / max(1, V_t')` mit V_t' nach dem Verlust (Auftrag an Kultur) · beide `+0,01` Rache

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| S_t | double | ≥ 0 | calculated | Stärke |
| k_t, k_o | int | ≥ 0 | calculated | verlangte Verluste dieses Jahres (Aufträge an Menschen) |
| u₁, u₂ | double | 0–1 | calculated | in dieser Reihenfolge, t ist der Stamm mit der kleineren ID |
| V_t' | int | ≥ 0 | calculated | Volk nach dem Auftrag, wie Menschen es meldet |
| L_t | double | ≥ 0 | calculated | Summe der jährlichen Verlustanteile im laufenden Krieg |
| Kampfrate, Angst | double | 0,03; 0,04 | data file | Prototyp 1 (Angst dort 0,004 · 10) |

**Output Range:** Ein Stamm verliert pro Kriegsjahr 1,8 bis 4,2 % der gegnerischen
Stärke, also bei gleich großen Stämmen mit a = 0,4 etwa 2,7 % des eigenen Volks (1,6 bis
3,8 %). Ist `S · 0,042 < 0,5` (bei a = 0,4 bis 13 Köpfe), rundet der Verlust immer auf 0:
Kleine Stämme kämpfen dann ohne Tote, bis die Höchstdauer den Frieden bringt (wie im
Prototyp, Open Questions). Die Verschiebung der Achse ist klein (etwa −0,001 pro Jahr).
**Example:** t: V = 60, a = 0,4 → S = 54; o: V = 40, a = 0,2 → S = 28; u₁ = 0,9, u₂ = 0,1:
k_o = runden(54 · 0,03 · 1,32) = runden(2,1384) = 2, k_t = runden(28 · 0,03 · 0,68) =
runden(0,5712) = 1. L_t += 1/60, L_o += 2/40; Achse t − 0,04/59, o − 0,08/38.

### 4. Eroberung

The Eroberung formula is defined as:

nach dem Kampf: `o ist erobert ⇔ V_o < 6 ∨ V_o < 0,22 · V_t`, sonst `t ist erobert ⇔ V_t < 6 ∨ V_t < 0,22 · V_o` · `j = floor(0,5 · V_L)` wechseln zum Sieger S, `V_L − j` sterben · Achse `x_S ← (x_S · (V_S + j) + x_L · j) / max(1, V_S + 2j)` mit V_S vor dem Wechsel

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| V_L, V_S | int | – | calculated | Volk des Eroberten und des Siegers nach dem Kampf |
| j | int | ≥ 0 | calculated | Überlebende, die zum Sieger wechseln |
| Eroberungsgrenzen, Anteil | int, double | 6; 0,22; 0,5 | data file | Prototyp 1 |

**Output Range:** Erobert wird, wer nach dem Kampf unter 6 Köpfen oder unter 22 % des
Gegners liegt. Die Mischformel gibt den Überlebenden etwas weniger Gewicht als ihre
Kopfzahl, genau wie in Prototyp 1 (dort steht das schon erhöhte Volk des Siegers in
Zähler und Nenner). Das Ergebnis liegt immer zwischen x_S und x_L.
**Example:** S: V = 80, x = 0,2; L: V = 15, x = −0,6. 15 < 17,6 → erobert; j = 7, 8 sterben;
S hat 87 Köpfe, x_S = (0,2 · 87 − 0,6 · 7) / 94 = 13,2 / 94 ≈ 0,1404 (nach Köpfen wären es
0,1356).

### 5. Frieden

The Frieden formula is defined as:

nach Kampf und Eroberungsprüfung: `Frieden ⇔ (L_t > 0,3 ∧ L_o > 0,3) ∨ (Jahr − Y_since > 18)` · danach `Frist_{t,o} = Jahr + 20`; jeder bestehende Groll zwischen beiden wird ersetzt durch `{Rache: 0,2}`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| L_t, L_o | double | ≥ 0 | calculated | Verlustanteile (Formel 3) |
| Erschöpfung | double | 0,3 | data file | Prototyp 1 |
| Höchstdauer | int | 18 Jahre | data file | Prototyp 1 |
| Friedensfrist | int | 20 | data file | Prototyp 1; erklärt wird wieder ab Jahr + 20 |

**Output Range:** Ein Krieg hat höchstens 20 Kampfjahre (gekämpft wird schon im Jahr der
Erklärung, Frieden frühestens nach dem Kampf des 19. Jahres danach). Bei 2,7 % Verlust pro
Jahr sind beide Seiten nach etwa 12 Jahren erschöpft. Nach dem Frieden liegt der Groll bei
0,2 und verblasst.
**Example:** Krieg seit Jahr 100, L_t = 0,31, L_o = 0,28 im Jahr 110: weiter. Im Jahr 119
(19 > 18): Frieden, Frist 139; erklärt werden kann wieder ab Jahr 139.

### 6. Abspaltung

The Abspaltung formula is defined as:

Kandidat, wenn `V_t > 44`, kein Krieg und weniger als 7 Stämme leben; Abspaltung, wenn `u < p`, mit `p = 0,06`, falls `V_t > 0,9 · cap_t`, sonst `p = 0,025` · Platz `= Welt.Platzsuche(m_t, r_t + 1, r_t + 6)`; kein Platz oder Mitte eines lebenden Stamms → nächster Kandidat · `Teil = floor(V_t · (0,35 + 0,1 · u))` · Richtung `s = −sign(x_t)`, falls `|x_t| > 0,3`, sonst `s = +1`, falls `u < 0,5`, sonst −1 · `x_neu = x_t + 0,6 · s` · `a_neu = clamp(a_t + (u − 0,5) · 0,2, 0,05, 0,95)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| p | double | 0,025 / 0,06 | data file | Wahrscheinlichkeit pro Jahr (Prototyp 1) |
| r_t | double | – | calculated | mit V_t vor der Abspaltung |
| Teil | int | 35–45 % von V_t | calculated | Köpfe des neuen Stamms (Auftrag an Menschen) |
| s | int | ±1 | calculated | Richtung des neuen Glaubens; +1 heißt freier (Richtung Lust-Sekte) |
| x_neu | double | −0,9 bis 0,9 | calculated | braucht kein clamp: bei \|x_t\| > 0,3 geht es zur Mitte, sonst ist \|x_t\| ≤ 0,3 |
| Würfe | – | – | calculated | in dieser Reihenfolge: u für p, die 240 Würfe der Platzsuche, u für Teil, u für s (nur bei \|x_t\| ≤ 0,3), u für a_neu |

**Output Range:** Ein Stamm mit 64 Köpfen gibt 22 bis 28 ab, der Elternstamm behält
mindestens 25. Ohne Gedränge spaltet sich ein großer Stamm im Mittel alle 40 Jahre,
gedrängt alle 17 Jahre (wenn nicht ein anderer Stamm im selben Jahr zuerst an der Reihe
ist).
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
| Gesellschaftsform | Aufzählung | – | calculated | Kultur & Erinnerung, Stand dieses Jahres |

**Output Range:** Ein Gottesstaat mit x = −0,8 wird pro Jahr um etwa 0,0024 kampflustiger,
eine Lust-Sekte mit x = 0,8 um 0,0044 friedlicher; der Zufall streut um ±0,005.
**Example:** a = 0,4, x = 0,5, Lust-Sekte, u = 0,5 → 0,4 − 0,0015 − 0,002 = 0,3965.

### Kalibrierung gegen Prototyp 1

Alle Regeln und Werte, die das Ergebnis bestimmen, sind die von Prototyp 1. Die Regler
gelten als „fest bis zur ersten Kalibrierungsmessung“ (AC-21). Abweichungen:
1. Der Zufall kommt aus dem eigenen Strom statt aus dem gemeinsamen Generator; die
   Geschichten sind andere, ihre Verteilung soll gleich bleiben.
2. Im Prototyp liefen Ernte, Gnade und Kulturwandel Stamm für Stamm in einer Schleife.
   Hier sieht der Hunger-Groll alle Stämme nach der Ernte und nach der Gnade, und die
   Aggression liest die Gesellschaftsform dieses Jahres (im Prototyp teils die des
   Vorjahres).
3. Ein Stamm, der im selben Jahr erobert wurde, kämpft nicht weiter. Im Prototyp konnte
   ein eroberter Stamm mit kleinerer ID in derselben Schleife noch einmal „erobert“
   werden (mit 0 Überlebenden); das hat die Zahl der Eroberungen etwas erhöht.
4. Stirbt ein Stamm, enden die Kriege gegen ihn sofort für alle. Im Prototyp blieb ein
   solcher Krieg bei manchen Gegnern bis zu ihrem nächsten Kampf stehen und senkte noch
   eine Ernte um 15 % oder verhinderte eine Abspaltung.
5. Eine Abspaltung auf die Mitte eines lebenden Stamms fällt aus (Welt & Insel,
   Abweichung 4).
6. Gleichstände (Gründe, reichster Stamm, nächster Nachfolger) folgen einer festen
   Reihenfolge; im Prototyp hingen sie an der Reihenfolge der Schleifen oder Schlüssel.
   Bei Gleitkommawerten kommen sie praktisch nicht vor.
7. Ohne Einfluss auf die Simulation: Namen ab ID 14 mit Zahl, Grund und Ursache der
   Kriegserklärung (Formel 2), Nachfolger für Menschen in Gottes Hand, Tod-Ereignisse
   beim Untergang.

## Edge Cases

- **If zwei Stämme sich im selben Jahr gegenseitig den Krieg erklären würden**: Die
  Erklärung des Stamms mit der kleineren ID kommt zuerst; danach besteht schon Krieg, und
  die zweite fällt aus. Es gibt ein Ereignis.
- **If ein Stamm gleichzeitig mehrere Kriege führt**: Jeder Krieg kämpft einmal pro Jahr
  in der Reihenfolge von Regel 19; die Verluste summieren sich. Wird er in einem davon
  erobert, kämpft er in den übrigen nicht mehr (Abweichung 3).
- **If der Kampf beide Stämme unter die Eroberungsgrenze drückt**: Geprüft wird zuerst
  der Gegner o (größere ID); er ist erobert, t ist Sieger, auch wenn t selbst klein ist.
  Fällt t danach unter 3 Köpfe, geht er in Schritt 8 unter.
- **If ein Stamm im Krieg durch Hunger unter 3 Köpfe fällt**: Er wird in Schritt 6
  erobert (er liegt unter 6); hat er nach dem Kampf noch 2 Köpfe, wechselt einer zum
  Sieger, und es gibt das Ereignis „Eroberung“, nicht „Untergang“ (wie in Prototyp 1).
- **If ein lebender Stamm in diesem Jahr 0 bis 2 Köpfe hat** (Hunger oder Alter vor
  Schritt 8): Er nimmt an allen Schritten bis zum Aussterben teil; seine Reichweite ist
  2 bis 3,06, sein Wohlstand bei 0 Köpfen 0. Er erklärt keinen Krieg (unter 12).
- **If Menschen & Bevölkerung einen Verlust nicht vollständig ausführen kann** (Gehaltene,
  Beteiligte laufender Handlungen): Die Verschiebung der Achse und die Eroberungsprüfung
  nutzen das V_t, das Menschen danach meldet, nicht die verlangte Zahl; L nutzt die
  verlangte Zahl k (wie im Prototyp).
- **If ein eroberter oder untergegangener Stamm noch Menschen hat, die nicht gezogen
  werden dürfen**: Regel 17 und 18. Der Stamm gilt trotzdem als untergegangen; sie gehen
  an den Nachfolger über, sobald sie frei sind.
- **If ein Stamm einen Krieg gegen einen erklärt, mit dem er eine Friedensfrist hat**: Das
  geschieht nicht; die Frist gilt in beide Richtungen.
- **If der Groll gegen einen Stamm aus einem Eingriff des Gottes kommt und der Eingriff
  nicht mehr in der Erinnerung ist**: Der Grund bleibt mit seiner Eingriffs-ID stehen,
  bis er verblasst oder ein Frieden ihn ersetzt; die Chronik nennt den Eingriff über
  seine ID (Simulationskern Regel 15).
- **If der bestgenährte Stamm zu weit weg ist**: Es entsteht kein Hunger-Groll, auch wenn
  ein ärmerer Nachbar nah ist (Prototyp 1). Lebt kein anderer Stamm, entsteht keiner.
- **If ein Auftrag „Groll“ ungültig ist** (von o gegen sich selbst, gegen oder von einem
  toten Stamm, Wert ≤ 0): Er wird ignoriert.
- **If 7 Stämme leben**: Es gibt keine Abspaltung, auch wenn ein Stamm gedrängt ist; es
  wird nicht gewürfelt.
- **If die Abspaltung den Elternstamm unter 3 Köpfe drücken würde**: Unmöglich, er hat
  mehr als 44 und gibt höchstens 45 % ab.
- **If kein Stamm mehr lebt**: Das System rechnet nichts mehr; die Welt ist ausgestorben
  (Kennzahl in AC-21, in Prototyp 1 nie geschehen).

## Dependencies

**Hängt ab von:**

| System | Art | Was gebraucht wird | GDD |
|---|---|---|---|
| Simulationskern | hart | Jahrestakt O_year = 2, Zufallsstrom system_id 4, Aufträge, Ereignisse mit Ursache | ✓ `design/gdd/simulationskern.md` (In Review) |
| Welt & Insel | hart | Startmitten, Gebiet, cap_t, food_t, Hunger, Platzsuche; Aufträge „Gebiet neu berechnen“, „Stamm entsteht mit Vorrat x“ | ✓ `design/gdd/welt-und-insel.md` (In Review) |
| Menschen & Bevölkerung | hart | V_t; Aufträge Verlust, Wechsel, Stamm erloschen; liest den Nachfolger | ✓ `design/gdd/menschen-und-bevoelkerung.md` (In Review) |
| Kultur & Erinnerung | hart | x_t und Gesellschaftsform (lesend); Aufträge Kultur anlegen, Achse verschieben, Achsen mischen | ✗ noch keins (vorläufiger Vertrag in diesem GDD) |
| Gotteskraft & Götterkräfte | weich | Auftrag „Groll“ für Eingriffe, die Nachbarn als Frevel sehen | ✗ noch keins (vorläufiger Vertrag) |

**Wird gebraucht von:** Welt & Insel (Mitten, Krieg, Aussterben), Menschen & Bevölkerung
(Aufträge, Nachfolger), Kultur & Erinnerung (Ereignisse für die Erinnerung, neue
Stämme), Harem & Rituale (Stammeszugehörigkeit), Gotteskraft & Götterkräfte (wer sieht
einen Eingriff als Frevel), Zustimmungsmodell (Vertrag Regel 21), Chronik, Weltansicht &
Kamera, Speichern & Laden; später Ziel-Befehle und Sichtbarer Krieg (Vertical Slice).
Welt & Insel, Menschen & Bevölkerung und Zustimmungsmodell nennen dieses System schon.

**Verträge für GDDs, die noch fehlen:** Kultur & Erinnerung legt beim Auftrag „Kultur
anlegen“ Achse, Hingabe und Temperament an (im Prototyp: Startstämme x = (u − 0,5) · 0,4
und Hingabe 0,4; abgespaltene Stämme x_neu und die Hingabe des Elternstamms), führt die
Verschiebung durch Kriegsangst im Ledger unter „Krieg“, rechnet „Achsen mischen“ mit den
übergebenen Werten nach Formel 4 und erinnert sich an Kriege mit ihren Gründen, an
Eroberungen und Abspaltungen. Gotteskraft legt fest, welcher Stamm einen Eingriff als
Frevel sieht (Prototyp 1: Blitz +0,35 bei x_o < 0,1, Fest-Segen +0,4 bei x_o < −0,1,
jeweils gegen den begünstigten oder getroffenen Stamm).

## Tuning Knobs

Alle Defaults bleiben bis zur ersten Kalibrierungsmessung (AC-21) auf den Werten von
Prototyp 1. Die Bereiche gelten für Änderungen danach und sind vor der Veröffentlichung
mit einem Durchlauf der Debug-Werkzeuge zu bestätigen. Querbedingung: Die
Eroberungsgrenze (0,22) muss klar unter dem Größenverhältnis (0,6) bleiben, sonst wird
der Erklärende im ersten Jahr erobert; die Kriegsschwelle muss über dem gedrängten
Grenzgroll mal Kampfeslust liegen (0,57 · (0,5 + a)), sonst herrscht an jeder engen
Grenze Dauerkrieg.

| Regler | Default | Sicherer Bereich | Wirkung |
|---|---|---|---|
| Verblassen des Grolls | 0,965 / Jahr | 0,95–0,975 | wie lange Groll nachwirkt; 0,98 hebt alle Grenzwerte um 75 % |
| Grenzgroll (normal / gedrängt) | 0,007 / 0,02 | 0–0,03 | Streit um Land |
| Gedrängt ab | 0,9 · cap | 0,8–1,0 | wann Grenzgroll und Abspaltung stärker werden |
| Glaubensgroll | 0,022 · Δx ab Δx > 0,7, Reichweite + 8 | 0,01–0,04 | Glaubenskriege |
| Hunger-Groll | 0,14, Reichweite + 6 | 0–0,3 | Neid in der Hungerzeit |
| Abspaltungs-Groll | 0,35 | 0,1–0,5 | Feindschaft nach der Abspaltung |
| Rache je Kampf, Rache nach Frieden | 0,01; 0,2 | 0–0,03; 0–0,4 | wie Kriege nachwirken |
| Kriegsschwelle | 0,6 | 0,5–1,2 (0,4 heißt Dauerkrieg an engen Grenzen) | wie leicht Krieg ausbricht |
| Größenverhältnis, Mindestvolk | 0,6; 12 | 0,4–1; 6–24 | wer sich Krieg zutraut |
| Kampfrate, Angst | 0,03; 0,04 | 0,01–0,06; 0–0,1 | wie blutig ein Kriegsjahr ist und wie sehr es die Kultur verschiebt |
| Eroberungsgrenzen, Anteil der Überlebenden | 6; 0,22; 0,5 | 3–10; 0,1–0,35; 0,3–0,7 | wann ein Stamm erobert wird und wie viele bleiben |
| Erschöpfung, Höchstdauer, Friedensfrist | 0,3; 18; 20 Jahre | 0,15–0,5; 8–30; 10–40 | wie lange Kriege und Frieden dauern |
| Abspaltung | V > 44; p 0,025 / 0,06; Teil 35–45 %; Achse ± 0,6; Aggression ± 0,1 | 30–60; 0,01–0,1; –; 0,3–0,8; 0–0,2 | wie oft neue Stämme entstehen und wie verschieden sie sind |
| MAX_TRIBES | 7 | 4–10 | wie voll die Insel wird |
| Aggression | Start 0,25–0,5; Drift 0,003 · (−x), Lust-Sekte − 0,002, Zufall ± 0,005 | – | Temperament der Stämme |

## Visual/Audio Requirements

Keine eigenen. Die Weltansicht zeigt Mitten, Grenzen (aus Welt & Insel), Kriege zwischen
Stämmen und die Kriegsnähe gespannter Paare (eine Linie oder Markierung, GDD Weltansicht &
Kamera); Sichtbarer Krieg (Vertical Slice) zeigt später Kämpfe.

## Game Feel

Die Stämme sollen wie Nachbarn mit Gedächtnis wirken: Eine alte Abspaltung schwelt noch
Jahre später, und ein Krieg kommt nicht aus dem Nichts, sondern hat einen Grund, den der
Spieler vorher an der wachsenden Kriegsnähe sehen kann.

## UI Requirements

- **Stamm ansehen:** Name, Gesellschaftsform (Kultur), Aggression als Wort (friedlich
  unter 0,3, wachsam bis 0,6, kriegerisch darüber), laufende Kriege mit Dauer, Frieden
  bis Jahr
- **Kriegsnähe** je gerichtetem Paar (Formel 2) als Stufe: ruhig unter 0,5, gespannt bis
  unter 1, kriegsbereit ab 1; mit dem größten Grund als Text und dem Hindernis, falls
  eines den Krieg verhindert („zu schwach“ bei Größenverhältnis oder Mindestvolk,
  „Friedensfrist bis Jahr …“)
- **Wer grollt diesem Stamm:** die eingehende Kriegsnähe der anderen Stämme mit Grund,
  damit der Spieler sieht, wer seinen Liebling bedroht
- **Meldungen** (über die Chronik): Krieg erklärt mit Grund und Ursache, Frieden,
  Eroberung, Abspaltung mit Richtung, Untergang

> **📌 UX Flag — Stämme & Diplomatie**: This system has UI requirements. In Phase 4
> (Pre-Production), run `/ux-design` to create a UX spec for each screen or HUD
> element this system contributes to **before** writing epics. Stories that
> reference UI should cite `design/ux/[screen].md`, not the GDD directly.

## Cross-References

| Dokument | Was hier verwendet wird |
|---|---|
| `prototypes/weltengott-concept/prototype.html` | `newTribe`, `addGriev`, `GRIEV_TEXT`, Groll, Krieg, Kampf, Eroberung, Frieden, Abspaltung, Aussterben in `step`, Groll aus `applyPower` |
| `prototypes/weltengott-concept/REPORT.md` | Kalibrierung: Krieg 1620, Abspaltung 1008, Eroberung 838, Frieden 500, Kulturwandel 393, Untergang 1 in 1.000 Ären; 5,0 Stämme am Ende; Krieg ist zu eintönig; ein gesegneter Stamm wurde ohne sichtbaren Grund angegriffen |
| `design/gdd/simulationskern.md` | Regel 4 (Reihenfolge, Aufträge), 7 (Determinismus), 15 (Ursache), Formel 5 (O_year = 2), Formel 6 (Strom 4) |
| `design/gdd/welt-und-insel.md` | Regeln 5, 7, 8, 9, 10, 13, Formeln 2 und 3, AC-16 |
| `design/gdd/menschen-und-bevoelkerung.md` | Regeln 2, 13, 15, 19, 20, 23 |
| `design/gdd/zustimmungsmodell.md` | Regeln 4, 21 |

## Acceptance Criteria

Testwurzel: `Assets/Tests/EditMode/StaemmeUndDiplomatie/`. **Testhilfen** (alle mit
Aufrufprotokoll): `IRandomStream` (vorgegebene Folge von `NextDouble()`-Werten; der Test
scheitert, wenn mehr oder weniger Würfe verbraucht werden), `IEventSink` (sammelt
Ereignisse mit Typ, Grund, Gründen, Ursache, IDs), `IPopulationSource` (V_t, führt
Verluste und Wechsel aus, kann eine Teilausführung melden), `ICultureSource` (x_t,
Gesellschaftsform, zeichnet Aufträge auf), `IWorldSource` (Mitten, cap_t, food_t,
„hungert“, Platzsuche, die genau 240 Würfe verbraucht und einen vorgegebenen Platz
liefert; zeichnet Aufträge auf), `IGodSource` (sendet den Auftrag „Groll“). Eine feste
`TribeConfig` mit den Defaults dieses GDD, damit eine geänderte Datendatei die Tests
nicht bricht. Logic-Tests rufen einzelne Schritte über `RunStep(n)` auf; „ein Jahr“ heißt
die Schritte 1 bis 8.

- **AC-01 Formel 1, Verblassen [Logic]** **GIVEN** ein Groll mit Grenze 0,2 und Rache 0,1, **WHEN** Schritt 1 läuft, **THEN** sind es 0,193 und 0,0965; **GIVEN** 300 Jahre gedrängte Berührung ohne andere Gründe, **THEN** liegt der Grenzgroll bei 0,02 · (1 − 0,965^300) / 0,035 ± 1e-9; **GIVEN** der Gegner ist tot, **THEN** ist der Groll gegen ihn nach Schritt 1 gelöscht.
- **AC-02 Formel 1, Auslöser [Logic]** **GIVEN** zwei Mitten mit V = 48 und V = 4 (r-Summe 10,696), **THEN** entsteht Grenzgroll im Abstand 10 und nicht im Abstand 11; **GIVEN** x = 0,35 und −0,35 (Δx = 0,7) im Abstand 18, **THEN** kein Glaubensgroll; **GIVEN** x = 0,4 und −0,35, **THEN** 0,0165 Glaubensgroll; im Abstand 19 (> 18,696) keiner; **GIVEN** t hungert, der reichste Stamm (Wohlstand 1,5) ist zu weit weg und ein ärmerer (1,2) nah, **THEN** entsteht kein Hunger-Groll; **GIVEN** zwei gleich reiche nahe Stämme, **THEN** bekommt der mit der kleineren ID 0,14.
- **AC-03 Formel 2, Erklärung [Logic]** **GIVEN** Grenze 0,3 und Glaube 0,5, a = 0,4, V_t = 20, V_o = 33, **WHEN** Schritt 5 läuft, **THEN** gibt es Krieg mit Grund Glaube und der Liste beider Gründe; **GIVEN** V_o = 34, **THEN** nicht; **GIVEN** V_t = 11, V_o = 10, **THEN** nicht; **GIVEN** V_t = 12, V_o = 10, **THEN** Krieg; **GIVEN** Frist = Jahr + 1, **THEN** nicht, mit Frist = Jahr schon, in beiden Richtungen; **GIVEN** Grenze 0,4 und Glaube 0,4, **THEN** ist der Grund Grenze.
- **AC-04 Formel 2, Zuordnung zum Spieler [Logic]** **GIVEN** Glaube 0,63 und drei Gründe „Gott“ 0,4 (ID 77), 0,3 (ID 80), 0,2 (ID 91), a = 0,4, **THEN** ist der Grund Gott und die Ursache 77; **GIVEN** Glaube 0,63 und Gott 0,1 (ID 77), **THEN** ist der Grund Glaube und die Ursache 77, und die Chronik-Zeile enthält „weil du“; **GIVEN** Glaube 0,8 und Gott 0,1 (ID 77), **THEN** hat das Ereignis keine Ursache; in allen drei Fällen ist der Zustand nach dem Schritt bitgleich mit einem Lauf, der Grund und Ursache nicht berechnet.
- **AC-05 Formel 2, gegenseitig [Logic]** **GIVEN** zwei Stämme erfüllen im selben Jahr gegeneinander die Bedingung, **THEN** entsteht genau ein Krieg, erklärt vom Stamm mit der kleineren ID.
- **AC-06 Formel 3 [Logic]** **GIVEN** t: V = 60, a = 0,4; o: V = 40, a = 0,2; Würfe 0,9 und 0,1, **WHEN** gekämpft wird, **THEN** ist das Protokoll: Menschen „Verlust 2“ für o, „Verlust 1“ für t, Kultur „Achse t − 0,04/59“, „Achse o − 0,08/38“; L_t = 1/60, L_o = 2/40; Rache je + 0,01 (einmal); **GIVEN** beide V = 12, a = 0,25, **THEN** sind beide Verluste 0, es gibt keinen Auftrag an Menschen oder Kultur, und die Rache wächst trotzdem; `RoundHalfUp(2,5) = 3`, `RoundHalfUp(2,4999) = 2`.
- **AC-07 Formel 4 [Logic]** **GIVEN** S: V = 80, x = 0,2 und L: V = 15, x = −0,6 nach dem Kampf, **WHEN** die Eroberung geprüft wird, **THEN** ist das Protokoll: Menschen „7 wechseln von L zu S“, „Verlust 8“ für L, „Stamm erloschen“ für L, Kultur „Achsen mischen (0,2, 80, 7, −0,6)“, Welt „Gebiet neu berechnen“; der Nachfolger von L ist S; es gibt ein Ereignis „Eroberung“ und kein „Untergang“; nach Formel 4 ist x_S = 13,2/94 ± 1e-9; **GIVEN** beide unter der Grenze, **THEN** ist der Stamm mit der größeren ID erobert.
- **AC-08 Formel 4, kein zweites Mal [Logic]** **GIVEN** Stamm 1 führt Kriege gegen 2 und 3 und wird im Kampf mit 2 erobert, **THEN** kämpft Stamm 1 nicht gegen 3, es gibt genau ein Ereignis „Eroberung“, und Stamm 3 ist danach nicht mehr im Krieg mit 1; **GIVEN** Stamm 3 wird im Kampf mit 1 erobert und führt auch Krieg gegen 2, **THEN** kämpft 2 nicht gegen 3.
- **AC-09 Formel 5 [Logic]** **GIVEN** ein Krieg seit Jahr 100, **THEN** gibt es mit L = 0,31 und 0,28 im Jahr 110 keinen Frieden; mit L = 0,3 und 0,31 keinen; mit L = 0,31 und 0,31 Frieden nach dem Kampf dieses Jahres; im Jahr 118 geht er weiter, im Jahr 119 ist Frieden, Frist 139; eine Erklärung im Jahr 138 fällt aus, im Jahr 139 ist sie möglich; der Groll beider ist danach genau {Rache: 0,2}, auch wenn er Gründe „Gott“ hatte; ohne vorherigen Groll entsteht keiner.
- **AC-10 Formel 6 [Logic]** **GIVEN** V = 64, cap = 70, x = 0,5, a = 0,4 und die Würfe 0,03, 240 Werte für die Platzsuche, 0,6, 0,25, **WHEN** Schritt 7 läuft, **THEN** fordert dieses System die Platzsuche mit 9 und 14 an, das Protokoll ist Menschen „26 wechseln zum neuen Stamm“, Kultur „Kultur anlegen (−0,1, Hingabe des Elternstamms)“, Welt „Stamm entsteht mit Vorrat 13“, der neue Stamm hat a = 0,35, beide haben 0,35 Abspaltungs-Groll, das Ereignis nennt die Richtung „streng“, und spätere Kandidaten würfeln nicht; **GIVEN** x = 0,3, **THEN** wird ein Wurf für die Richtung gezogen; **GIVEN** V = 44, **THEN** ist er kein Kandidat, mit V = 45 schon; **GIVEN** V = 63 und cap = 70 mit dem Wurf 0,03, **THEN** gilt p = 0,025, und es gibt keine Abspaltung; **GIVEN** die Platzsuche liefert die Mitte eines lebenden Stamms oder keinen Platz, **THEN** wird kein Teil gewürfelt, und der nächste Kandidat würfelt; **GIVEN** 7 lebende Stämme oder ein Kandidat im Krieg, **THEN** wird für ihn nicht gewürfelt.
- **AC-11 Formel 7 [Logic]** **GIVEN** a = 0,4, x = 0,5, Lust-Sekte, u = 0,5, **THEN** ist a danach 0,3965; **GIVEN** a = 0,05, x = 1, u = 0, **THEN** bleibt a = 0,05; **GIVEN** a = 0,95, x = −1, u = 0,999, **THEN** bleibt a = 0,95; beim Erzeugen ergeben u = 0 und u = 0,999 die Werte 0,25 und 0,49975.
- **AC-12 Regel 17 [Logic]** **GIVEN** ein Stamm ohne Krieg fällt durch Hunger auf 2 Köpfe, **WHEN** ein Jahr läuft, **THEN** ist er in Schritt 8 untergegangen, Menschen erhält „Stamm erloschen“, Welt „Gebiet neu berechnen“, alle Kriege gegen ihn sind beendet, ohne Friedensfrist, und es gibt ein Ereignis „Untergang“; mit 3 Köpfen lebt er; **GIVEN** derselbe Stamm führt Krieg gegen einen mit 12 Köpfen und a = 0,05 (dessen Stärke tötet niemanden), **THEN** wird er in Schritt 6 erobert, 1 wechselt, und es gibt „Eroberung“, nicht „Untergang“.
- **AC-13 Regel 17 und 18 [Integration, mit Menschen & Bevölkerung]** **GIVEN** ein Einzelner des Stamms L ist in Gottes Hand und einer nimmt an einer laufenden Handlung teil, als L erobert wird, **WHEN** sie frei werden, **THEN** gehören beide dem Sieger mit gemerktem Stammeswechsel; **GIVEN** L geht unter, **THEN** sterben seine übrigen Einzelnen mit Ereignis „Tod“ und der Ursache des Untergangs; **GIVEN** zwei lebende Mitten gleich weit von der Mitte des Untergegangenen (quadrierter Abstand in ganzen Zellen), **THEN** ist der Nachfolger der mit der kleineren ID; **GIVEN** der Nachfolger ist beim Loslassen selbst untergegangen, **THEN** gilt dessen Nachfolger; **GIVEN** kein Stamm lebt mehr, **THEN** stirbt der Losgelassene.
- **AC-14 Regel 19, Reihenfolge [Logic]** **GIVEN** 3 Stämme (IDs 0, 1, 2) mit Groll, einem Krieg zwischen 0 und 2 und einem Abspaltungskandidaten 1, **WHEN** ein Jahr läuft, **THEN** ist das Protokoll der Würfe genau: 3 (Schritt 2), 2 (Schritt 6), 1 + 240 + 1 + (1, falls |x_1| ≤ 0,3) + 1 (Schritt 7), und die Aufträge kommen in der Reihenfolge der Schritte.
- **AC-15 Determinismus [Integration]** **GIVEN** derselbe Startwert auf demselben Build und Rechner, **WHEN** zwei Läufe 1.500 Schritte rechnen, **THEN** ist der Zustands-Hash nach jedem Schritt gleich.
- **AC-16 Speichern [Integration]** **GIVEN** ein Zustand mit Groll aus allen sechs Gründen, zwei Kriegen, einer Friedensfrist und einem untergegangenen Stamm mit Nachfolger, **WHEN** nach Schritt 1.500 gespeichert und geladen wird, **THEN** gleichen die Hashes der Schritte 1.501 bis 3.000 denen eines durchgehenden Laufs, und alle double-Werte überstehen das Speichern bitgleich.
- **AC-17 Regel 3 [Logic]** **GIVEN** die Stammes-IDs 0, 13, 14, 28, 29, 42, 112 und 55.986, **THEN** heißen sie Ura, Tharn, Ura II, Ura III, Mirr III, Ura IV, Ura IX und Ura 4000.
- **AC-18 Regel 12 [Logic]** **GIVEN** Stamm A führt nur Krieg gegen B, **WHEN** B in Schritt 6 oder 8 stirbt, **THEN** meldet die Abfrage „im Krieg“ für A danach nein.
- **AC-19 Auftrag „Groll“ [Logic]** **GIVEN** der Auftrag „Groll (o gegen t, 0,4, ID 77)“, **THEN** hat o gegen t einen Grund „Gott“ 0,4 mit ID 77; ein zweiter Auftrag mit ID 78 ist ein eigener Grund; Aufträge von o gegen o, gegen einen toten Stamm oder mit Wert ≤ 0 ändern nichts.
- **AC-20 Kriegsnähe [Logic]** **GIVEN** G = 0,8 und a = 0,4, **THEN** ist die Kriegsnähe 1,2 (kriegsbereit); mit V_t < 0,6 · V_o meldet sie das Hindernis „zu schwach“, mit einer Frist „Friedensfrist bis Jahr …“; die Stufen wechseln bei 0,5 und 1.
- **AC-21 Kalibrierung [Integration, ADVISORY; läuft erst, wenn Kultur & Erinnerung, Gotteskraft und die Debug-Werkzeuge stehen]** **GIVEN** die Startwerte 0 bis 99, je 10 Ären zu 25 Geschichtsjahren ohne Eingriff, **WHEN** sie laufen, **THEN** liegen Kriege, Abspaltungen und Frieden pro Ära bei 1,62 / 1,01 / 0,50 ± 15 %, Eroberungen pro Ära zwischen 0,60 und 0,92 (Abweichung 3 senkt sie), die mittlere Zahl der Stämme am Ende bei 5,0 ± 0,5, und keine Welt stirbt aus; als Kennzahlen (keine Hürde) werden gemeldet: Krieg zu Kulturwandel (Prototyp 1: 4,1 zu 1), Anteil der Kriege mit Ursache Gott, Anteil der Kriege ohne Tote, Verteilung der Gründe.
- **AC-22 Spielertest [Playtest, ADVISORY]** **GIVEN** ein Tester segnet einen Stamm, den ein strenggläubiger Nachbar danach angreift, **WHEN** er nach dem Grund gefragt wird, **THEN** nennt er ihn in mindestens 4 von 5 Fällen richtig, nur aus Chronik und Anzeige.

**Ohne eigenes AC** (abgedeckt durch AC-15 und AC-16 oder später durch die UX-Spec): Regel 1
(ID nie wiederverwendet, Mitte unverändert), der Ablauf einer Friedensfrist ohne
Erklärung, die Aggressionswörter (UX-Spec „Stamm ansehen“, mit Screenshot als Beleg).

## Open Questions

| Frage | Wer klärt | Bis wann |
|---|---|---|
| **Krieg überdeckt den Kulturwandel** (Prototyp 1: 1620 Kriege gegen 393 Kulturwechsel, „Krieg, Frieden, Krieg“ war eintönig). Mögliche Hebel nach der Kalibrierung: höhere Kriegsschwelle, längere Friedensfrist, Groll, der statt Krieg auch Abspaltung oder Kulturwandel auslöst, mehr Ereignisarten (Bündnis, Handel, Heirat). Kennzahlen aus AC-21: Krieg zu Kulturwandel, Kriege je Stamm und Jahrhundert, Ereignisarten je Ära | Entwickler, dann Messung | nach der ersten AC-21-Messung |
| **Neid ohne Ventil:** Ein kleinerer strenggläubiger Nachbar darf einem großen gesegneten Stamm keinen Krieg erklären (Größenverhältnis), sein Groll staut sich. Andere Folgen wie Überfall, Tribut, Abspaltung? Kennzahl: gerichtete Paare mit Kriegsnähe ≥ 1, die 30 Jahre lang nie Krieg führen | Entwickler | nach der Messung |
| **Groll aus Eingriffen und die Verzögerung bis zum Krieg:** Ein Segen (0,4) halbiert sich in etwa 20 Jahren. Langsameres Verblassen für Gründe „Gott“? Kennzahl: mittlere Jahre vom Eingriff bis zum Krieg, Anteil der Kriege mit Ursache Gott, die mehr als 50 Jahre nach dem Eingriff beginnen | Entwickler | nach der Messung |
| Soll ein Frieden die Gründe „Gott“ löschen (heute ja, wie im Prototyp)? | Entwickler | nach der Messung |
| **Leere Kriege:** Kleine Stämme (bis etwa 13 Köpfe) verlieren nie jemanden und kämpfen bis zur Höchstdauer. Restbeträge mitführen wie bei der Gnade? Kennzahl: Anteil der Kriege ohne Tote | Entwickler | nach der Messung |
| Soll ein Stamm unter 3 Köpfen vor dem Kampf untergehen (eigener Schritt 0), statt wie im Prototyp erobert zu werden? Kennzahlen: Stämme unter 3, die erobert werden oder untergehen; Überlebende, die so wechseln, je 1.000 Jahre | Entwickler | nach der Messung |
| Die Friedensfrist von 20 sperrt Erklärungen nur 19 Jahre lang (wie im Prototyp). So lassen? | Entwickler | nach der Messung |
| Sehr kleine Gründe „Gott“ aus dem Zustand löschen (heute bleiben sie, bis ein Frieden sie ersetzt oder der Gegner stirbt; in Anzeige und Ereignissen werden Gründe unter 0,001 ausgeblendet)? | Entwickler | nach der Messung |
| **Vorschau „Wer nimmt Anstoß?“** vor einem Eingriff: Die Zielanzeige hebt die Stämme hervor, die den Eingriff als Frevel sehen würden. Beantwortet die Frage der Player Fantasy, nimmt aber Entdeckung weg; eine neue Funktion, keine Korrektur | Entwickler, dann GDD Gotteskraft & Götterkräfte | mit dem GDD |
| Eine kleine Chronik-Zeile, wenn ein Paar eine Stufe der Kriegsnähe überschreitet? Mehr Meldungen in einer Chronik, in der Krieg schon überwiegt | Entwickler, dann GDD Chronik | mit dem GDD |
| Bündnisse, Handel oder Tribut als Diplomatie über Krieg und Frieden hinaus? Das Systemverzeichnis nennt „Beziehungen“; Prototyp 1 hatte nur Groll | Entwickler | vor dem Vertical Slice |
| Darf die Mitte eines Stamms wandern (weg von verbrannter Erde, hin zu fruchtbarem Land)? Heute fest wie im Prototyp | Entwickler | nach der Kalibrierung |
| Was geschieht mit den letzten 1–2 Menschen eines untergegangenen Stamms (heute sterben sie, Gehaltene gehen an den Nachfolger)? Sie könnten auch zum Nachfolger gehen | Entwickler | mit dem GDD Kultur & Erinnerung |
| Kampf und Gefangenschaft einzelner Menschen (Merkmal „im Kampf“, Gefangene) | GDD Sichtbarer Krieg | Vertical Slice |
