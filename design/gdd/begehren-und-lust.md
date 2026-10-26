# Begehren & Lust

> **Status**: In Review (revised after design review 2026-10-26)
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
   Handlungsregister (Zustimmungsmodell Regeln 18, 19). **Keine Anfrage zweimal:** Wird
   eine Anfrage abgelehnt, merkt sich dieses System ihre **Prüfsumme** (die Prüfsumme der
   Anfrage aus dem Stempel, Zustimmungsmodell Regel 18; sie deckt jeden Wert, den der
   Anfrage-Baustein holt). Vor jeder Anfrage baut es sie über den Anfrage-Baustein und
   vergleicht ihre Prüfsumme: Stimmt sie mit einer gemerkten überein, ist dieser Kandidat
   für diese Handlungsart an diesem Ort **gesperrt** und wird bei der Wahl übersprungen;
   gefragt wird der Nächstbegehrte (Entscheidung des Entwicklers vom 2026-10-26). Hat sich
   ein Wert geändert (eine Norm, ein Tabu, eine Bindung), ist die Prüfsumme neu und die
   Anfrage wieder erlaubt. Je Initiator, Menge der Beteiligten, Handlungsart und Ort gibt
   es höchstens eine gemerkte Prüfsumme; sie wird ersetzt, wenn dieselbe Anfrage mit neuen
   Werten gestellt wird, und gelöscht, wenn ein Beteiligter stirbt.

**Alltag**

6. Im **Personentakt** (alle 2 Schritte, Simulationskern Formel 5) sucht ein verfügbarer
   Einzelner, der auf keinem Fest anwesend ist (Regel 13), mit kleiner Wahrscheinlichkeit
   nach seiner Lust eine Handlung (Formel 2): Er wählt Zärtlichkeit oder Sex zu zweit (mit
   mehr Lust eher Sex) und fragt unter den Verfügbaren im Umkreis von r_A = 3 Zellen, die
   auf keinem Fest anwesend und nicht gesperrt sind (Regel 5), den an, den er am meisten
   begehrt. Der Ort ist **privat**,
   wenn sonst niemand innerhalb einer Zelle steht, sonst **öffentlich**.

**Feste**

7. Jeder lebende Stamm hat eine **Festprüfung** alle 10 Schritte (2 Geschichtsjahre): Er
   beginnt ein Fest mit einer Wahrscheinlichkeit, die mit der Offenheit seiner Kultur für
   Sex zu zweit und seinem Wohlstand steigt (Formel 3). Kein Fest beginnt im Krieg
   (Stämme & Diplomatie: „im Krieg“), im Hunger (Welt & Insel: „hungert in diesem Jahr“),
   während schon ein Fest des Stamms läuft oder mit weniger als 2 verfügbaren Einzelnen.
   Trifft der Wurf, wird zuerst die Teilnahme gewürfelt (Regel 10, Formel 4); gehen weniger
   als 2 hin, beginnt kein Fest: kein Ereignis, kein Festplatz (Entscheidung des
   Entwicklers vom 2026-10-26).
8. Der **Fest-Segen** (Gotteskraft & Götterkräfte) startet als Auftrag sofort ein Fest des
   Stamms, auch im Krieg oder Hunger und auch, wenn weniger als 2 hingehen; läuft schon
   eines, verlängert er es um 150 Schritte, ohne Obergrenze (Entscheidung des Entwicklers vom
   2026-10-26; die Kosten legt Gotteskraft & Götterkräfte fest, Open Questions). Das Fest
   trägt dann die Eingriffs-ID als Ursache (Simulationskern Regel 15). Der Auftrag wird
   sofort in der Eingriffsphase des Schritts ausgeführt (Simulationskern Regel 4.1), also
   vor allen Teilschritten aus Regel 20; die Würfe der Teilnahme kommen dort aus dem Strom 5.
9. Ein Fest findet am **Festplatz** des Stamms statt (ein Ort von Welt & Insel, Regel 17
   dort). Hat der Stamm keinen, legt dieses System ihn beim ersten Fest als Auftrag an der
   Mitte des Stamms an. Ein Fest dauert **300 Schritte** (bei 1× 30 s). Der **Festbereich**
   sind die Zellen im Umkreis von r_F = 2 Zellen um den Festplatz.
10. Bei Festbeginn entscheidet jeder verfügbare Einzelne des Stamms einmal, ob er hingeht
    (mit mehr Geselligkeit eher), und verfügbare Einzelne anderer Stämme im Umkreis von
    r_Gast = 8 Zellen um den Festplatz (oder die Mitte, solange es keinen gibt) kommen mit
    halber Wahrscheinlichkeit als **Gäste** (Formel 4). Gefangene gehen nie. Wer geht,
    bekommt von Menschen & Bevölkerung das Ziel Festbereich und behält es bis zum Ende des
    Fests. Wer bei Festbeginn nicht verfügbar war (etwa in einer Handlung), entscheidet nach
    denselben Regeln einmal in seinem ersten Personentakt, in dem er während des Fests
    verfügbar ist, vor seiner Initiative. Wer schon zu einem Fest geht, würfelt für kein
    weiteres. Wer auf anderem Weg in den Festbereich kommt (er wandert hinein oder die
    Gotteshand setzt ihn dort ab), ist ohne Wurf anwesend (Regel 13); handeln kann er wie
    jeder andere erst, wenn er verfügbar ist (nach einem Wurf erst nach T_erschüttert).
    **Teilnehmer** eines Fests sind alle Lebenden und Freien, die das Ziel Festbereich dieses
    Fests haben oder dort anwesend sind, auch auf dem Weg.
11. Auf dem Fest sucht jeder anwesende Verfügbare viel öfter eine Handlung als im Alltag
    (Formel 5), am Ort **Fest**: mit Geselligkeit eher eine **Gruppe** (3 bis
    N_gruppe_max = 4 Beteiligte, die er am meisten begehrt), sonst mit Lust eher Sex zu zweit
    als Zärtlichkeit. Partner sind nur andere anwesende Verfügbare desselben Fests, die nicht
    gesperrt sind (Regel 5); eine Gruppe ist nie größer, als Partner da sind.
12. Ein Fest **endet** (Regel 20, Teilschritt 3), sobald seine Dauer abgelaufen ist, weniger
    als 2 Teilnehmer (Regel 10) bleiben oder der Stamm untergegangen ist. Mit dem Ende
    verlieren alle das Ziel Festbereich, und die Anwesenheit endet (Regel 13). Laufende
    Handlungen enden dabei nicht; sie laufen zu Ende.
13. **Anwesend** auf einem Fest ist jeder Einzelne, gleich welchen Stamms, der lebt, frei
    und nicht in Gottes Hand ist und in einer Zelle des Festbereichs eines laufenden Fests
    steht, auch in einer Handlung, verletzt oder im Kampf (Entscheidung des Entwicklers vom
    2026-10-26: Es zählt, wo jemand steht, nicht die Entscheidung aus Formel 4).
    **Anwesende altern halb so schnell** (Entscheidung des Entwicklers vom 2026-10-26):
    Ihre Schritte zählen für das Lebensalter nur halb (Simulationskern Formel `lebensjahr`,
    Formel 7 hier). Dieses System prüft die Anwesenheit einmal je Schritt (Regel 20,
    Teilschritt 5) und erteilt Menschen & Bevölkerung bei jedem Wechsel den Auftrag „Fest
    betreten“ oder „Fest verlassen“. Weil niemand in Gottes Hand anwesend ist, überschneiden
    sich Festschritte und gehaltene Schritte nie. Steht jemand im Festbereich zweier Feste,
    ist er auf dem Fest des Stamms mit der kleineren ID anwesend.

**Handlungen**

14. Eine zugestimmte Handlung dauert je nach Art T_a Schritte (Formel 6): Zärtlichkeit 20,
    Sex zu zweit 40, Gruppe 60. Sie endet auf genau einem von zwei Wegen:
    - **Dauer erreicht:** Im Schritt Beginn + T_a beendet dieses System sie (Regel 20,
      Teilschritt 1). Zuvor ruft es `PrüfeSperren` (Zustimmungsmodell Regel 14): Greift
      keine Sperre, endet sie mit dem Grund „Dauer“ und ist **vollendet**; greift eine,
      endet sie mit dem Grund der Sperre und ist nicht vollendet.
    - **Vorher:** Das Handlungsregister beendet sie wegen einer Sperre oder der
      Halteprüfung (Zustimmungsmodell Regel 14). Sie ist nicht vollendet.

    Eine Sperre, die erst ein späteres System im Schritt Beginn + T_a setzt (Harem &
    Rituale, Gotteskraft & Götterkräfte), kommt zu spät: Die Handlung ist dann schon
    vollendet. **Freiwilliges Beenden** (Zustimmungsmodell Regel 15) gibt es im MVP-0
    nicht; außer Dauer, Sperre und Halteprüfung beendet nichts eine Handlung (Festlegung im
    Design Review vom 2026-10-26, Open Questions).
15. Jeder Mensch nimmt höchstens an einer laufenden Handlung teil (Zustimmungsmodell, Edge
    Cases); Anfragen an Menschen in einer Handlung stellt dieses System nicht.

**Wirkungen in der Welt** (Pillar 3)

16. **Paarbindung**: Vollenden zwei **Ungebundene** (k_i = ungebunden, also ohne Bindung und
    nicht in Trauer) eine Handlung zu zweit (Zärtlichkeit oder Sex zu zweit), entsteht
    zwischen ihnen eine Bindung (Entscheidung des Entwicklers vom 2026-10-26). Jeder hat
    höchstens eine Bindung. Sie endet mit Tod oder Gefangennahme eines der beiden oder wenn
    beide T_Bindung = 5 Lebensjahre lang keine Handlung miteinander vollendet haben; dafür
    zählt jede vollendete Handlung, an der beide beteiligt sind, auch eine Gruppe (geprüft
    in Regel 20, Teilschritt 2). Für das Zustimmungsmodell gilt i als **gebunden** (k_i),
    solange seine Bindung besteht, und noch T_trauer lang nach Tod oder Gefangennahme des
    Partners (Zustimmungsmodell Regel 19). Bindung entsteht und vergeht als kleines Ereignis
    für die Chronik.
17. **Kulturwandel**: Jede vollendete Handlung erteilt Kultur & Erinnerung den Auftrag
    „Achse verschieben (Wert, Lust, Veranlasser)“, Veranlasser ist die Ursache des Fests,
    falls es ein gesegnetes war (Formel 8). Bis zur Kalibrierung ist das Gewicht 0.
18. **Zufriedenheit**: Dieses System führt je Stamm eine Zufriedenheit aus vollendeten
    Handlungen, aktualisiert alle 10 Schritte (Formel 8, Regel 20, Teilschritt 7), die
    Stämme & Diplomatie lesen kann (etwa für die Abspaltung). Bis
    zur Kalibrierung ist ihr Gewicht dort 0.
19. **Diplomatie**: Eine vollendete Handlung zwischen Menschen verschiedener Stämme erteilt
    Stämme & Diplomatie den Auftrag „Groll mindern“ zwischen beiden Stämmen (Formel 8). Bis
    zur Kalibrierung ist das Gewicht 0.

**Reihenfolge im Schritt**

20. Dieses System läuft an seiner Stelle in der Reihenfolge des Kerns (nach Stämme &
    Diplomatie, vor Harem & Rituale) mit dem Zufallsstrom system_id 5, in jedem Schritt;
    seine eigenen Takte (Personentakt, alle 10 Schritte) sind Vielfache von P_person
    (Simulationskern Formel 5). Aufträge an dieses System (Fest-Segen) führt es sofort im
    Schritt des Auftraggebers aus, in der Eingriffsphase vor allen Teilschritten. In einem
    Schritt:
    1. **Handlungen beenden**, deren Dauer erreicht ist (Regel 14), geordnet nach Beginn,
       dann nach der kleinsten ID der Beteiligten; jede vollendete sofort auswerten:
       Bindung (Regel 16), Kultur und Diplomatie (Regeln 17, 19), Zähler H_t der
       Zufriedenheit (Formel 8).
    2. **Bindungen prüfen**, in aufsteigender ID des kleineren Partners: Ende nach T_Bindung
       ohne gemeinsame Handlung; Tod und Gefangennahme liest es direkt bei Menschen &
       Bevölkerung (Simulationskern Regel 4.2). Die Trauer läuft ab dem Schritt, in dem
       dieses System das Ende sieht.
    3. **Feste beenden** (Regel 12), in aufsteigender Stammes-ID.
    4. **Festprüfungen** in aufsteigender Stammes-ID, falls t ein Vielfaches von 10 ist
       (Formel 3, dann Formel 4).
    5. **Anwesenheit** aller Einzelnen in aufsteigender ID prüfen; Aufträge „Fest betreten /
       verlassen“ (Regel 13).
    6. **Personentakt:** für jeden Einzelnen, dessen Personentakt in diesem Schritt liegt,
       in aufsteigender ID: eine ausstehende Entscheidung nach Formel 4 (Regel 10), dann
       die Initiative, auf dem Fest (anwesend, Formel 5) oder im Alltag (Formel 2).
    7. **Zufriedenheit** aktualisieren, falls t ein Vielfaches von 10 ist (Formel 8).

    Handlungen, die eine Sperre vor ihrer Dauer beendet, beendet die Sperrenprüfung des
    Handlungsregisters in jedem Schritt nach Gotteskraft & Götterkräfte (Simulationskern
    Formel 5).

### States and Transitions

Ein Einzelner hat zwei voneinander unabhängige Merkmale. Das erste ist sein **Festbezug**:

| Festbezug | Bedeutung | Wechsel |
|---|---|---|
| **Kein Fest** | kein Ziel Festbereich, nicht anwesend | → auf dem Weg (Formel 4) · → anwesend (betritt einen Festbereich) |
| **Auf dem Weg** | Ziel Festbereich, noch nicht anwesend; zählt als Teilnehmer, altert normal | → anwesend · → kein Fest (Fest endet, gefangen, tot) |
| **Anwesend** | im Festbereich eines laufenden Fests, lebt, frei, nicht in Gottes Hand; altert halb so schnell, auch in einer Handlung (Regel 13) | → kein Fest (Fest endet, verlässt den Festbereich, gefangen, gepackt, tot) |

Das zweite ist die **Handlung**: frei von Handlung → **in Handlung** (`Beginne`) → zurück,
sobald sie endet (Regel 14). Eine Initiative hat nur, wer verfügbar ist (Regel 4):
Anwesende nach Formel 5, alle anderen nach Formel 2.

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
| Zustimmungsmodell | Ergebnisse (Zustimmung, Gründe, Stempel mit Prüfsumme); Prüfsumme einer gebauten Anfrage (Regel 5); `PrüfeSperren`; Handlungsregister (wer in einer Handlung ist, Ende) | Anfragen per IDs; `Beginne`; `Beende` bei erreichter Dauer; b_ij und k_i als Funktionen für den Anfrage-Baustein | Zustimmungsmodell (Anfrage, Register), dieses System (b_ij, k_i) |
| Menschen & Bevölkerung | Einzelne mit Eigenschaften, Geschlecht, Ort, Zustand | Orientierung (Vorliebe), Aufträge „Ziel Festbereich“, „Fest betreten / verlassen“ (für das Altern) | Menschen |
| Kultur & Erinnerung | Normen (Offenheit Sex zu zweit), Tabus | Auftrag „Achse verschieben (Wert, Lust, Veranlasser)“, Gewicht 0 | Kultur |
| Welt & Insel | food_t, Hunger, Festplatz | Auftrag „Ort anlegen (Festplatz)“ | Welt |
| Stämme & Diplomatie | im Krieg, lebt | Zufriedenheit je Stamm (lesend), Auftrag „Groll mindern“, beide Gewicht 0 | dieses System (Zufriedenheit), Stämme (Groll) |
| Gotteskraft & Götterkräfte | Auftrag „Fest segnen (Stamm, Eingriffs-ID)“ | – | Gotteskraft (Kraft), dieses System (Fest) |
| Simulationskern | Personentakt, Zufallsstrom system_id 5, Ursache-Kette | Ereignisse: Fest beginnt / endet (mit Ursache), Bindung entsteht / endet, Handlung vollendet (klein); Formel `lebensjahr` mit Festschritten | Kern |
| Weltansicht & Kamera, Körper & Animation | – | Szenen (Handlungsart, Beteiligte, Ort, Beginn, Dauer), Feste mit Festbereich | Weltansicht (Darstellung, Zustimmungsmodell Regel 20) |
| Harem & Rituale | – | b_ij, Bindungen | dieses System |
| Chronik | – | Ereignisse | Kern-Ereignisformat |
| Speichern & Laden | kompletter Zustand | Orientierung je Einzelnem, laufende Feste (Stamm, Beginn, Ende, Ursache, wer hingeht, wer anwesend ist, wer noch nach Formel 4 entscheiden muss), Bindungen (Partner, Beginn, letzte gemeinsame Handlung, Trauer bis), Prüfsummen abgelehnter Anfragen mit ihrem Schlüssel, Zufriedenheit und H_t je Stamm | Speichern & Laden |

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

**Paarstrom genau:** Startwert `p_ij = SplitMix64(seed_sys5 ⊕ (ID_i · 2^32 + ID_j))`, Zustand
`s_k = SplitMix64(p_ij + k · 0x9E3779B97F4A7C15)` für k = 0 bis 3, Würfe und `NextDouble`
wie im Simulationskern (Formel 6). Die Summe wird in double von v_1 bis v_12 der Reihe nach
gebildet, dann wird 6 abgezogen; danach `0,5 + σ_b · z`, dann das Klemmen.
**Referenzwerte** (Python-Referenz nach Simulationskern Formel 6, geprüft gegen dessen
Referenzwerte, 2026-10-26; world_seed 42, o = 1): b(0, 1) = 0,5416594863685015,
b(1, 0) = 0,21431960616911583, b(3, 7) = 0,3755276737530158. Über 100.000 Paare: Mittel
0,5002, Streuung nach dem Klemmen 0,1978, je etwa 0,57 % bei 0 und bei 1.
**Leistung:** b_ij ist rein und darf je Paar zwischengespeichert werden (höchstens
N_max² = 3.600 Werte; ein Eintrag fällt weg, wenn ein Einzelner stirbt). Der Anfrage-Baustein
holt b_ij für alle Paare einer Anfrage aus diesem Speicher.

### 2. Initiative im Alltag

The Alltag formula is defined as:

je Personentakt eines Verfügbaren, der nicht anwesend ist: Initiative, wenn `u < λ_A · L_i / 30` · Kandidaten K = die Verfügbaren mit Abstand ≤ r_A, die nicht anwesend sind; ist K leer, endet die Initiative · Handlungsart: Sex zu zweit, wenn `u < L_i`, sonst Zärtlichkeit · Partner `j* = argmax_j b_ij` über die j aus K, für die diese Anfrage nicht gesperrt ist (Regel 5; Gleichstand: kleinere ID), nur `b_ij* > 0` · Ort: privat, wenn kein weiterer Einzelner mit Abstand ≤ 1 zu i oder j* steht, sonst öffentlich

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| λ_A | double | 0,1 / Lebensjahr | data file | Rate der Initiative im Alltag bei Lust 1 |
| L_i | double | 0–1 | calculated | Lust (Menschen & Bevölkerung) |
| r_A | int | 3 Zellen | data file | Umkreis, in dem im Alltag gesucht wird |
| u | double | 0–1 | calculated | erst der Wurf für die Initiative, dann (nur bei Initiative und nicht leerem K) der für die Art |

**Output Range:** Ein Mensch mit Lust 0,5 sucht etwa alle 20 Lebensjahre (2 min bei 1×)
eine Initiative, also etwa zweimal im Leben; 40 Einzelne zusammen etwa alle 3 s eine. Wie
viele davon Handlungen werden, entscheidet das Zustimmungsmodell. Ohne Kandidaten in der
Nähe wird nur der erste Wurf verbraucht; sind alle Kandidaten gesperrt, zwei.
**Example:** L_i = 0,6, u = 0,001 < 0,1 · 0,6 / 30 = 0,002 → Initiative; zwei Verfügbare in
der Nähe mit b = 0,4 und 0,7; u = 0,5 < 0,6 → Sex zu zweit; Partner ist der mit 0,7 (hat er
genau diese Anfrage schon abgelehnt und hat sich nichts geändert, der mit 0,4).

### 3. Fest-Chance

The Fest formula is defined as:

alle 10 Schritte je Stamm: Fest ausgelöst, wenn `u < c_F · n_t · W_t` · `W_t = clamp(food_t / V_t, 0, 1)` (bei V_t = 0 gleich 0) · `n_t` = Offenheit der Kultur für Sex zu zweit (hängt nicht vom Ort ab, Kultur & Erinnerung Regel 14) · Wahrscheinlichkeit 0 im Krieg, im Hunger, bei laufendem Fest oder unter 2 Verfügbaren (dann kein Wurf) · nach dem Auslösen folgen die Würfe aus Formel 4; das Fest beginnt nur, wenn mindestens 2 hingehen (sonst sind die Würfe verbraucht, und nichts geschieht)

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

bei Festbeginn je verfügbarem Einzelnen in aufsteigender ID, der noch zu keinem Fest geht (Nachzügler: in ihrem ersten verfügbaren Personentakt, Regel 10): eigener Stamm `geht, wenn u < p_0 + p_G · G_i`; Gast (anderer Stamm, Abstand zum Festplatz ≤ r_Gast, ohne Wurf, wenn weiter) `geht, wenn u < f_Gast · (p_0 + p_G · G_i)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| G_i | double | 0–1 | calculated | Geselligkeit |
| p_0, p_G | double | 0,3; 0,6 | data file | Grundanteil und Gewicht der Geselligkeit (p_0 + p_G ≤ 1) |
| f_Gast | double | 0,5 | data file | wie oft Nachbarn mitfeiern |
| r_Gast | int | 8 Zellen | data file | Umkreis um den Festplatz, aus dem Gäste kommen |

**Output Range:** Vom eigenen Stamm gehen 30 % (G = 0) bis 90 % (G = 1), im Mittel 60 %; Gäste
halb so oft.
**Example:** G = 0,5 → eigener Stamm 0,6, Gast 0,3.

### 5. Initiative auf dem Fest

The Fest-Initiative formula is defined as:

je Personentakt eines anwesenden Verfügbaren: Initiative, wenn `u < λ_F · L_i / 30` · Kandidaten K = die anderen anwesenden Verfügbaren desselben Fests; ist K leer, endet die Initiative · Gruppe-Wurf: Gruppe, wenn `u < g_G · G_i` und |K| ≥ 2 · bei Gruppe Größen-Wurf `N = min(3 + floor(u · (N_gruppe_max − 2)), 1 + |K|)`, Beteiligte sind die N − 1 aus K mit dem größten b_ij (Gleichstand: kleinere ID); ist genau diese Gruppe gesperrt (Regel 5), fällt der am wenigsten Begehrte heraus, solange N ≥ 3 bleibt; ist auch die Gruppe aus 3 gesperrt, wird es eine Handlung zu zweit · sonst (oder nach diesem Rückfall) Art-Wurf: Sex zu zweit, wenn `u < L_i`, sonst Zärtlichkeit, mit dem am meisten Begehrten aus K, für den diese Anfrage nicht gesperrt ist (Gleichstand: kleinere ID), nur b > 0 · Ort Fest

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| λ_F | double | 2,0 / Lebensjahr | data file | Rate der Initiative auf dem Fest bei Lust 1 (20-mal der Alltag) |
| g_G | double | 0,5 | data file | wie stark Geselligkeit eine Gruppe statt einer Handlung zu zweit wählen lässt |
| N_gruppe_max | int | 4 | data file | Zustimmungsmodell, Registry `n_gruppe_max` |

**Output Range:** Ein Mensch mit Lust 0,5 startet auf dem Fest etwa jedes Lebensjahr (6 s)
eine Initiative, also etwa fünf in einem Fest. Wie viele daraus Handlungen werden,
entscheiden das Zustimmungsmodell und die Zeit, in der jemand schon in einer Handlung ist:
Zustimmungsmodell AC-20 nennt für Sex zu zweit bei Norm 0,9 / 0,5 / 0,15 etwa 69 / 33 / 6,6 %
Zustimmung (privat, ohne Bindung) und für eine Gruppe zu dritt 32 / 2,2 / unter 0,1 %.
Gruppen entstehen daher fast nur in einer Lust-Sekte, und dort ist ein Anwesender mehr als
die Hälfte des Fests schon in einer Handlung. Gemessen wird das in AC-17. Würfe je
Initiative: Initiative; bei nicht leerem K Gruppe ja oder nein, dann Größe (nur bei Gruppe)
oder Art; nach einem Rückfall aus der Gruppe zusätzlich die Art.
**Example:** L = 0,5, G = 0,8, drei Kandidaten: u = 0,01 < 0,033 → Initiative; u = 0,3 < 0,4 →
Gruppe; u = 0,7 → N = min(3 + floor(1,4), 1 + 3) = 4. Mit nur zwei Kandidaten ergäbe derselbe
Wurf N = min(4, 3) = 3.

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

`fest_steps_i` = Zahl der Schritte, in denen i anwesend war (Regel 13) · im Simulationskern: `age_Lj = floor(((t − t_birth) − held_steps − f_slow · fest_steps) / S_Lj)`

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

je vollendeter Handlung: Kultur `Δx = w_K · (1, auf einem Fest; 0,5, sonst)` an jeden beteiligten Stamm (einmal je Stamm), Grund Lust · Diplomatie für jedes Paar verschiedener Stämme t < o unter den Beteiligten einmal: `Groll mindern (t gegen o und o gegen t, w_D)` · Zufriedenheit alle 10 Schritte (Regel 20, Teilschritt 7): `Z_t ← Z_t · d_Z + H_t / max(1, E_t)`, danach `H_t ← 0`; H_t zählt die in diesen 10 Schritten vollendeten Handlungen mit mindestens einem Beteiligten aus t, jede einmal

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| w_K | double | 0 bis zur Kalibrierung | data file | Kulturwirkung je Handlung; Kultur deckelt bei 0,01 pro Jahr |
| Z_t | double | ≥ 0 | calculated | Zufriedenheit des Stamms (vollendete Handlungen je Einzelnem, verblassend) |
| d_Z | double | 0,81 | data file | Verblassen je 10 Schritte (0,9 je Geschichtsjahr, auf den Takt von 10 Schritten gebracht) |
| H_t | int | ≥ 0 | calculated | vollendete Handlungen mit Beteiligten aus t seit der letzten Aktualisierung |
| E_t | int | ≥ 0 | calculated | lebende Einzelne des Stamms (Menschen & Bevölkerung) |
| w_D | double | 0 bis zur Kalibrierung | data file | Groll, den eine Handlung zwischen zwei Stämmen mindert |

**Output Range:** Bis zur Kalibrierung ändern Kultur und Diplomatie nichts; Z_t wird berechnet
und gemeldet, Stämme & Diplomatie liest sie mit Gewicht 0. Z_t strebt bei konstant r
vollendeten Handlungen je Einzelnem und 10 Schritten gegen r / (1 − d_Z) ≈ 5,3 · r. Die
Halbwertszeit ist etwa 33 Schritte (gut ein halbes Lebensjahr); Z_t ist also ein kurzes
Stimmungsmaß (Open Questions).
**Example:** Ein Stamm mit 20 Einzelnen und 4 vollendeten Handlungen in 10 Schritten, vorher
Z = 1,0: Z = 0,81 + 0,2 = 1,01.

## Edge Cases

- **If kein Verfügbarer in der Nähe ist**: Die Initiative verbraucht nur ihren Wurf, und es
  passiert nichts.
- **If der am meisten Begehrte b = 0 hat** (zum Beispiel später eine Orientierung, die nicht
  passt): keine Anfrage.
- **If eine Anfrage abgelehnt wird**: Keine Folge außer der gemerkten Prüfsumme; die Chronik
  bekommt keine Meldung, die Anzeige kann den Grund zeigen (Zustimmungsmodell Regel 13).
  Bei der nächsten Initiative ist diese Anfrage gesperrt, und der Initiator fragt den
  Nächstbegehrten (Regel 5).
- **If alle Kandidaten gesperrt sind**: Die Initiative endet ohne Anfrage; die verbrauchten
  Würfe bleiben verbraucht.
- **If eine Gruppe nicht zustande kommt** (einer lehnt ab): Keine Handlung und im selben
  Personentakt keine weitere Anfrage. Bei der nächsten Gruppen-Initiative ist genau diese
  Gruppe gesperrt; Formel 5 versucht dann eine kleinere oder eine Handlung zu zweit
  (Zustimmungsmodell Edge Cases: danach eine neue mit weniger Beteiligten).
- **If ein Mensch in den Festbereich geworfen oder gesetzt wird**: Der Wurf ist keine Eingabe
  des Begehrens. Er ist sofort anwesend und altert halb so schnell, ist aber erst
  T_erschüttert lang gesperrt und handelt danach wie jeder anwesende Verfügbare, auch als
  Mitglied eines anderen Stamms. Sein Begehren ist dasselbe wie ohne Wurf.
- **If ein Anwesender gefangen, gepackt oder getötet wird**: Er ist nicht mehr verfügbar und
  nicht mehr anwesend (Auftrag „Fest verlassen“, das Altern zählt wieder normal); seine
  laufende Handlung beendet das Handlungsregister im selben Schritt.
- **If ein Anwesender verletzt oder in einen Kampf gezogen wird**: Er ist nicht mehr
  verfügbar, seine laufende Handlung endet, aber er bleibt anwesend und altert halb so
  schnell, solange er im Festbereich steht (Regel 13).
- **If eine Handlung im Schritt ihres Endes gesperrt wird**: Setzt ein System vor diesem
  (Eingriff, Menschen, Stämme) die Sperre, ist sie nicht vollendet; setzt sie ein späteres
  (Harem, Gotteskraft), ist sie schon vollendet (Regel 14).
- **If der Stamm eines Fests in den Krieg zieht oder hungert**: Das laufende Fest läuft zu
  Ende; nur neue Feste beginnen nicht.
- **If zwei Stämme zugleich feiern**: Jeder Mensch geht zu höchstens einem Fest; wer nach
  Formel 4 schon zu einem geht, würfelt für kein weiteres (Stämme in aufsteigender ID).
  Überlappen sich zwei Festbereiche, ist er auf dem Fest des Stamms mit der kleineren ID
  anwesend (Regel 13).
- **If bei einer Festprüfung weniger als 2 hingehen**: Kein Fest, kein Ereignis, kein
  Festplatz; die Würfe aus Formel 3 und 4 sind verbraucht (Regel 7).
- **If ein Fest-Segen einen Stamm ohne Verfügbare trifft**: Das Fest beginnt in der
  Eingriffsphase und endet noch im selben Schritt in Teilschritt 3 (unter 2 Teilnehmer);
  der Segen bleibt ein Eingriff mit Ursache, beide Ereignisse tragen sie.
- **If ein Fest durch wiederholten Segen sehr lange läuft**: Keine Obergrenze (Regel 8);
  Anwesende altern so lange halb so schnell. Die Kosten legt Gotteskraft & Götterkräfte fest
  (Open Questions).
- **If eine Bindung entsteht, während einer der beiden schon gebunden ist**: Unmöglich, eine
  Bindung entsteht nur zwischen zwei Ungebundenen. Hatten zwei Gebundene trotzdem eine
  Handlung (die Bindungsstrafe hat nicht abgelehnt), entsteht keine neue Bindung.
- **If der Partner stirbt**: Die Bindung endet, k_i bleibt T_trauer lang „gebunden“; erst
  danach kann eine neue Bindung entstehen (Regel 16: Ungebundene sind nicht in Trauer).
  Eine Handlung, die ein Trauernder in dieser Zeit vollendet, erzeugt keine Bindung.
- **If ein Stamm untergeht, während er feiert**: Das Fest endet im selben Schritt
  (Teilschritt 3; Stämme & Diplomatie rechnet vorher); alle verlieren das Ziel Festbereich,
  und die Anwesenheit endet.
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
| Stämme & Diplomatie | weich | im Krieg, lebt; liest Zufriedenheit, Auftrag „Groll mindern“ (Gewicht 0) | ✓ `design/gdd/staemme-und-diplomatie.md` (Approved; Interactions-Zeile für dieses System seit 2026-10-26, Werte offen) |
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
| r_A | 3 Zellen | 1–5 | wie weit Menschen im Alltag suchen |
| c_F | 0,01 | 0,003–0,03 | wie oft Feste entstehen |
| T_Fest, Segen-Verlängerung | 300; 150 Schritte (ohne Obergrenze) | 150–600; 0–300 | wie lange Feste dauern |
| r_F | 2 Zellen | 1–3 | wie groß der Festbereich ist (wer dort steht, ist anwesend) |
| p_0, p_G | 0,3; 0,6 | p_0 + p_G ≤ 1 | wie voll Feste werden |
| f_Gast, r_Gast | 0,5; 8 Zellen | 0–1; 4–12 | wie oft und aus welcher Entfernung Nachbarn mitfeiern |
| λ_F | 2,0 / Lebensjahr | 0,5–4 | wie viel auf einem Fest geschieht |
| g_G | 0,5 | 0–1 | wie oft Gruppen statt Paaren angefragt werden |
| T_a | 20 / 40 / 60 Schritte | 10–120 | wie lange Szenen dauern |
| f_slow | 0,5 | 0–1 | wie viel langsamer auf dem Fest gealtert wird (1 = gar nicht) |
| T_Bindung | 5 Lebensjahre ohne gemeinsame Handlung | 2–20 | wie stabil Bindungen sind |
| T_trauer | 5 Lebensjahre (Zustimmungsmodell, dort eingestellt) | 1–20 | wie lange ein Hinterbliebener gebunden zählt |
| d_Z | 0,81 je 10 Schritte | 0,5–0,95 | wie lange Zufriedenheit nachwirkt |
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
| `design/gdd/simulationskern.md` | Regeln 4.1, 4.2, 15, Formeln 5, 6 (Paarstrom, Referenzwerte), `lebensjahr` |
| `design/gdd/welt-und-insel.md` | Regel 17 (Orte), Wohlstand |
| `design/gdd/staemme-und-diplomatie.md` | Kriegszustand, Groll |

## Acceptance Criteria

Testwurzel: `Assets/Tests/EditMode/BegehrenUndLust/`. Testhilfen wie bei den anderen
GDDs: `IRandomStream` (vorgegebene Würfe, scheitert bei Über- oder Unterverbrauch),
`IEventSink`, `IConsentStub` (Anfrage-Baustein und Register als Stub mit vorgegebenen
Ergebnissen und Aufrufprotokoll), `IPopulationSource` (Einzelne, Zustände, Orte, nimmt
Aufträge entgegen), `ICultureSource` (Normen, nimmt Aufträge entgegen), `IWorldSource`,
`ITribeSource`; reine Funktion `Begehren(seed, i, j, orientierung)`; Einstieg `RunStep(t)`.

- **AC-01 Formel 1, rein [Logic]** **GIVEN** world_seed 42, **THEN** ist `Begehren(i, j)` für 10.000 Paare bei zweimaligem Aufruf, in umgekehrter Reihenfolge und nach Speichern und Laden bitgleich, und es verbraucht keinen Wurf aus dem Strom des Systems; b_ij ≠ b_ji für mindestens 99 % der Paare; die Referenzwerte aus Formel 1 stimmen bitgleich: b(0, 1) = 0,5416594863685015, b(1, 0) = 0,21431960616911583, b(3, 7) = 0,3755276737530158.
- **AC-02 Formel 1, Verteilung [Logic]** **GIVEN** 100.000 Paare, **THEN** liegt das Mittel bei 0,5 ± 0,005, die Streuung bei 0,2 ± 0,005, und je 0,6 % ± 0,2 Punkte liegen bei 0 und bei 1; **GIVEN** Würfe mit Summe 7,0, **THEN** ist b = 0,7.
- **AC-03 Regel 1, Eingriffe ändern nichts [Logic, Property]** **GIVEN** zwei Welten mit gleichem Startwert, in einer werden 1.000-mal Menschen mit der Gotteshand nebeneinandergesetzt, gesegnet oder geblitzt, Hingabe und Rang geändert, **THEN** ist b_ij für alle lebenden Paare gleich (Zustimmungsmodell, Pflichten).
- **AC-04 Formel 2 [Logic]** **GIVEN** L = 0,6 und die Würfe 0,001 und 0,5, zwei Verfügbare im Abstand 2 mit b = 0,4 und 0,7, **THEN** fragt das System Sex zu zweit mit dem zweiten an, Ort privat; steht ein Dritter im Abstand 1, Ort öffentlich; mit dem Wurf 0,0021 keine Initiative; ohne Verfügbare in der Nähe wird genau ein Wurf verbraucht.
- **AC-05 Regel 4 [Logic]** **GIVEN** Kandidaten, die gefangen, verletzt, in Gottes Hand, erschüttert, im Kampf oder in einer Handlung sind, **THEN** werden sie nie angefragt und fragen nie an.
- **AC-06 Regel 5 [Logic]** **GIVEN** A begehrt B (0,7) mehr als C (0,4), beide verfügbar im Umkreis, und B hat A's Anfrage Sex zu zweit abgelehnt, **WHEN** A wieder Initiative mit denselben Würfen hat und sich kein Wert geändert hat, **THEN** fragt A C, nie B; ändert sich eine Norm, ein Tabu oder der Ort, fragt A wieder B, und die gemerkte Prüfsumme wird ersetzt; stirbt B, ist sie gelöscht; je Initiator, Beteiligtenmenge, Handlungsart und Ort gibt es höchstens eine gemerkte Prüfsumme; sind B und C beide gesperrt, endet die Initiative nach zwei Würfen ohne Anfrage.
- **AC-07 Formel 3 [Logic]** **GIVEN** n = 0,9, W = 1, kein Krieg, kein Hunger, **THEN** beginnt mit dem Wurf 0,005 ein Fest, mit 0,009 nicht; im Krieg, im Hunger, bei laufendem Fest oder unter 2 Verfügbaren wird nicht gewürfelt; die Prüfung läuft nur in Schritten, die ein Vielfaches von 10 sind; **GIVEN** der Wurf löst ein Fest aus, aber nach Formel 4 geht nur 1, **THEN** beginnt kein Fest, es gibt kein Ereignis und keinen Auftrag „Ort anlegen“, und die Würfe sind verbraucht.
- **AC-08 Regeln 8, 9, Formel 6 [Logic]** **GIVEN** ein Fest-Segen (Eingriff 77) auf einen Stamm ohne Fest und ohne Festplatz, **THEN** beginnt ein Fest mit Ursache 77 und Ende in 300 Schritten, und Welt erhält „Ort anlegen (Festplatz)“ an der Mitte; ein zweiter Segen in Schritt 200 des Fests verlängert es um 150, ein dritter um weitere 150; der Segen beginnt das Fest auch, wenn nach Formel 4 weniger als 2 hingehen, und die Würfe der Teilnahme fallen in der Eingriffsphase vor Teilschritt 1 (Aufrufprotokoll).
- **AC-09 Formel 4 [Logic]** **GIVEN** Festbeginn, ein Einzelner mit G = 0,5 und der Wurf 0,59, **THEN** geht er (0,59 < 0,6), mit 0,61 nicht; als Gast im Abstand 8 mit 0,29 ja, mit 0,31 nicht, im Abstand 9 gar nicht; Gefangene nie.
- **AC-10 Formel 5 [Logic]** **GIVEN** ein anwesender Verfügbarer mit L = 0,5, G = 0,8 und 3 anwesenden verfügbaren Kandidaten (b = 0,2, 0,9, 0,6) und die Würfe 0,01, 0,3, 0,7, **THEN** fragt das System eine Gruppe aus 4 an, Ort Fest; mit nur den Kandidaten 0,9 und 0,6 und denselben Würfen eine Gruppe aus 3 (N = min(4, 3)); mit nur dem Kandidaten 0,9 und den Würfen 0,01, 0,3, 0,4 eine Handlung zu zweit (Sex, 0,4 < 0,5), genau drei Würfe; ist die Gruppe aus 4 gesperrt, wird die Gruppe ohne den Kandidaten 0,2 angefragt; ist auch sie gesperrt, folgt ein Art-Wurf und eine Handlung zu zweit mit 0,9; ein Kandidat, der zwar im Festbereich steht, aber in Gottes Hand ist, wird nie gewählt.
- **AC-11 Regel 14, vollendet [Logic]** **GIVEN** eine Handlung Sex zu zweit, begonnen in Schritt s, **THEN** beendet dieses System sie in Schritt s + 40 in Teilschritt 1 mit dem Grund „Dauer“ als vollendet; wird ein Beteiligter in Schritt s + 40 von Stämme & Diplomatie gefangen (vor diesem System), endet sie dort mit dem Grund der Sperre und ist nicht vollendet; setzt ein Stub für Gotteskraft in Schritt s + 40 eine Sperre (nach diesem System), ist sie vollendet; beendet das Register sie vorher (Sperre oder Halteprüfung), ist sie nicht vollendet, und es entsteht keine Bindung und keine Wirkung; kein anderer Weg beendet eine Handlung.
- **AC-12 Regel 16, Bindung [Logic]** **GIVEN** zwei Ungebundene vollenden Zärtlichkeit, **THEN** entsteht eine Bindung mit Ereignis; ebenso bei Sex zu zweit; eine Gruppe erzeugt keine; ein schon Gebundener bekommt keine zweite; nach 5 Lebensjahren ohne gemeinsame Handlung endet sie, eine vollendete Gruppe mit beiden Partnern setzt diese Frist zurück; stirbt der Partner, liefert k_i noch T_trauer lang „gebunden“, danach nicht, und eine Handlung zu zweit, die der Trauernde in dieser Zeit mit einem Ungebundenen vollendet, erzeugt keine Bindung, danach schon.
- **AC-13 Regel 13, Formel 7 [Integration, mit Simulationskern und Menschen]** **GIVEN** ein Einzelner mit t − t_birth = 1.200 Schritten, davon 300 anwesend auf einem laufenden Fest, **THEN** ist sein Alter 17 Lebensjahre (ohne Fest 20); Schritte auf dem Weg zum Fest zählen normal; ein Einzelner, der nicht hingehen wollte, aber in den Festbereich wandert, zählt ab dem Schritt des Betretens halb; wird ein Anwesender gepackt, zählen die gehaltenen Schritte nicht zusätzlich als Festschritte (kein doppelter Abzug); ein Verletzter im Festbereich zählt weiter halb; nach Speichern und Laden mitten im Fest ist das Alter in jedem folgenden Schritt gleich wie ohne Laden.
- **AC-14 Pflichten aus dem Zustimmungsmodell [Logic, Property]** **GIVEN** 100.000 zufällige Zustände, **THEN** beginnt jede Handlung über `Beginne` mit einem Ergebnis aus diesem Schritt, jede Anfrage kommt aus dem Anfrage-Baustein, kein Mensch steht in zwei laufenden Handlungen, und Hingabe, Eingriffe, Kräfte und Rang ändern b_ij und k_i nie (Zustimmungsmodell AC-27).
- **AC-15 Formel 8 [Logic]** **GIVEN** w_K = 0 und w_D = 0, **THEN** sind Kulturachse und Groll nach 1.000 vollendeten Handlungen bitgleich mit einem Lauf ohne sie; **GIVEN** eine Test-Config mit w_K = 0,001, **THEN** erhält Kultur je vollendeter Handlung auf dem Fest den Auftrag mit 0,001 und Veranlasser 77, wenn das Fest gesegnet war; Z_t eines Stamms mit 20 Einzelnen, 4 vollendeten Handlungen in 10 Schritten und vorher 1,0 ist danach 1,01, und Z_t ändert sich nur in Schritten mit t mod 10 = 0; eine Handlung zwischen zwei Mitgliedern desselben Stamms zählt dort einmal, eine zwischen den Stämmen t und o bei beiden je einmal und erteilt genau einen Auftrag „Groll mindern“ für das Paar.
- **AC-16 Regel 20, Reihenfolge und Determinismus [Integration]** **GIVEN** derselbe Startwert auf demselben Build und Rechner, **WHEN** zwei Läufe 3.000 Schritte mit Festen rechnen, **THEN** ist der Zustands-Hash nach jedem Schritt gleich, und nach Speichern und Laden in Schritt 1.500 ebenfalls.
- **AC-17 Pillar 3 [Integration, ADVISORY bis zur ersten Messung]** **GIVEN** 100 Welten × 10 Ären ohne Eingriff, **THEN** feiert eine Lust-Sekte im Mittel 3- bis 10-mal so oft wie ein Gottesstaat, und Handlungen je Einzelnem und Lebensjahr liegen in der Lust-Sekte mindestens 3-mal so hoch wie im Gottesstaat; als Kennzahlen werden gemeldet: Feste je Ära, Anteil der Zeit mit Fest, Handlungen je Fest, Anteil gebundener Einzelner.
- **AC-18 Spielertest [Playtest, ADVISORY]** **GIVEN** 10 Tester segnen ein Fest, **WHEN** sie danach gefragt werden, ob sie eine Figur zu etwas zwingen konnten, **THEN** sagen mindestens 8 „nein, ich konnte sie nur zusammenbringen“, und mindestens 7 erkennen eine Bindung, die auf dem Fest entstand.
- **AC-19 Regel 12, Fest-Ende [Logic]** **GIVEN** ein laufendes Fest, dessen Teilnehmer bei Festbeginn alle noch auf dem Weg sind, **THEN** endet es nicht (sie zählen als Teilnehmer); **GIVEN** nur noch 2 Teilnehmer, **WHEN** einer stirbt oder gefangen wird, **THEN** endet das Fest im selben Schritt in Teilschritt 3 mit Ereignis, alle verlieren das Ziel Festbereich, jeder Anwesende bekommt „Fest verlassen“, und laufende Handlungen laufen weiter; **GIVEN** der Stamm geht unter, **THEN** endet sein Fest im selben Schritt; **GIVEN** ein Fest-Segen auf einen Stamm ohne Verfügbare, **THEN** beginnt und endet das Fest im selben Schritt, beide Ereignisse mit der Ursache des Segens.
- **AC-20 Regeln 10, 13, zwei Feste und Nachzügler [Logic]** **GIVEN** zwei Stämme beginnen im selben Schritt ein Fest, und ein Einzelner steht im Gastumkreis beider, **THEN** würfelt er zuerst für das Fest des Stamms mit der kleineren ID und, wenn er geht, für das zweite nicht; steht er in beiden Festbereichen, ist er auf dem Fest mit der kleineren ID anwesend; **GIVEN** jemand ist bei Festbeginn in einer Handlung, **THEN** würfelt er nach Formel 4 genau einmal, in seinem ersten Personentakt nach dem Ende der Handlung, vor seiner Initiative; ein Gefangener im Festbereich ist nie anwesend.
- **AC-21 Regel 20, Teilschritte [Logic]** **GIVEN** Stubs, die jeden Aufruf protokollieren, und ein Schritt, in dem alles fällig ist (t mod 10 = 0, eine Handlung mit erreichter Dauer, eine abgelaufene Bindung, ein endendes Fest, Einzelne im Personentakt), **THEN** folgen die Aufrufe der Reihenfolge der Teilschritte 1 bis 7, ein Fest-Segen-Auftrag desselben Schritts liegt vor Teilschritt 1, und die Würfe aus dem Strom 5 werden in genau dieser Reihenfolge verbraucht.

## Open Questions

| Frage | Wer klärt | Bis wann |
|---|---|---|
| Werte für Kulturwandel (w_K), Zufriedenheit und Diplomatie (w_D) | Entwickler, nach der Kalibrierung | nach der ersten Messung |
| Liest Stämme & Diplomatie die Zufriedenheit (etwa weniger Abspaltung), und gibt es den Auftrag „Groll mindern“? Beides heute nur als Vertrag mit Gewicht 0 | GDD Stämme & Diplomatie (Ergänzung) | mit den Werten |
| Andere Orientierungen als „beide“ (Anteile als Daten, vielleicht je Kultur) | Entwickler | nach dem ersten Playtest |
| Zeigt die Anzeige, wen jemand begehrt („begehrt X“), oder bleibt das verborgen? | UX-Spec Einzelnen ansehen | `/ux-design` |
| Wie viele Szenen sind auf einem Fest zugleich lesbar? Bei 20 Teilnehmern etwa 40 Handlungen je Fest | Weltansicht & Kamera, Playtest | erster Playtest |
| Brauchen Feste ein eigenes Ereignis für Gotteskraft (Zufluss) oder nur Rituale? | GDD Gotteskraft & Götterkräfte | mit dem GDD |
| Was kostet ein Fest-Segen? Die Verlängerung hat keine Obergrenze (Entscheidung des Entwicklers vom 2026-10-26), also erlaubt wiederholtes Segnen beliebig lange Feste mit halbem Altern | GDD Gotteskraft & Götterkräfte | mit dem GDD |
| Der Anfrage-Baustein muss die Prüfsumme einer gebauten Anfrage lesbar machen (dieselbe wie im Stempel), damit Regel 5 vor dem Fragen sperren kann. Heute steht nur die Prüfsumme im Stempel des Ergebnisses | Zustimmungsmodell (Ergänzung), technical-director | vor den Stories |
| Bleiben die Beteiligten einer Handlung stehen? Partner im Alltag stehen bis zu 3 Zellen auseinander, und Menschen & Bevölkerung gibt ihnen heute kein Ziel; die Darstellung braucht sie beieinander | Menschen & Bevölkerung, Weltansicht & Kamera | vor den Stories |
| Freiwilliges Beenden einer Handlung (Zustimmungsmodell Regel 15): im MVP-0 keins, festgelegt im Design Review vom 2026-10-26 | Entwickler | erster Playtest |
| Z_t hat eine Halbwertszeit von etwa 33 Schritten. Braucht die Zufriedenheit ein längeres Gedächtnis, bevor Stämme & Diplomatie sie mit Gewicht > 0 liest? | Entwickler, Kalibrierung | mit den Werten |
