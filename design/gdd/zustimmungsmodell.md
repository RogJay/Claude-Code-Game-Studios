# Zustimmungsmodell

> **Status**: In Design
> **Author**: Entwickler + Claude
> **Last Updated**: 2026-10-25
> **Last Verified**: 2026-10-25
> **Implements Pillar**: Pillar 3 (Lust ist Kultur, nicht Beiwerk); setzt die nicht verhandelbaren Grenzen der Anti-Pillars durch

## Summary

Das Zustimmungsmodell ist eine einzige reine Funktion, durch die jede intime oder
sexuelle Handlung muss: Zärtlichkeit, Sex zu zweit, Gruppe, Harem beitreten und Ritual.
Gefangene, Verletzte, Menschen im Kampf, Tabus und jede ungültige Eingabe sperren
immer. Sonst entscheidet jeder Beteiligte über seine Bereitschaft aus Eigenschaften,
Begehren und den Normen seiner Kultur, und alle müssen zustimmen. Hingabe, Rang und
Eingriffe des Gottes sind keine Eingaben. Der Spieler formt Lust nur über die Kultur,
und Ablehnungen sind mit ihrem Grund sichtbar.

> **Quick reference** — Layer: `Feature` · Priority: `MVP` · Key deps: `Menschen & Bevölkerung, Kultur & Erinnerung`

## Overview

Das Zustimmungsmodell entscheidet, ob eine sexuelle oder intime Handlung stattfinden
darf. Es ist eine einzige reine Funktion: Sie bekommt eine **Anfrage** mit der Art der
Handlung (Zärtlichkeit, Sex zu zweit, Gruppe, Harem beitreten, Ritual), den
Beteiligten, dem Ort und den Normen ihrer Kulturen und antwortet mit Zustimmung oder
mit Ablehnung samt Grund. Zuerst prüft sie harte Sperren, die immer ablehnen: Gefangene,
Verletzte, Menschen im Kampf und jede ungültige oder fehlende Eingabe. Danach
entscheidet jeder Beteiligte für sich über einen Bereitschaftswert aus seinen eigenen
Eigenschaften, seinem Begehren und den Normen seiner Kultur. Die Handlung findet nur
statt, wenn alle zustimmen. Das Modell kennt weder Hingabe noch Rang, Gotteskraft oder
Eingriffe des Spielers, denn diese Eingaben gibt es in seiner Schnittstelle nicht. So
kann niemand, auch nicht der Gott, Zustimmung erzwingen. Der Spieler formt Lust
nur über die Kultur: Wer Feste segnet, verändert über die Zeit die Normen eines
Stamms, aber nie die Entscheidung eines Einzelnen im Moment. Ablehnungen sind mit ihrem
Grund sichtbar, damit der Spieler sieht, wie Kultur Lust formt (Pillar 2 und 3).

## Detailed Design

### Core Rules

*`game-designer`, `systems-designer` and `ai-programmer` not consulted for this section — Lean mode. Review manually before production.*

**Die Funktion**

1. Es gibt genau **eine** Prüffunktion `Prüfe(Anfrage, Datentabelle) → Ergebnis`.
   Die Datentabelle enthält die Gewichte, Schwellen und Margen aus den Formulas und
   wird einmal beim Laden geprüft. Den Bereitschaftswert legt eine eigene reine
   Hilfsfunktion `Bereitschaft(Beteiligter, Anfrage, Datentabelle)` offen, damit er
   sich einzeln testen lässt. Jede sexuelle oder
   intime Handlung im Spiel muss vor ihrem Beginn durch sie gehen. Kein System darf eine
   solche Handlung ohne Zustimmung beginnen oder fortsetzen.
2. Die Funktion ist **rein**: kein Zufall, kein gespeicherter Zustand, kein Lesen der
   Welt, keine Meldungen. Gleiche Anfrage, gleiches Ergebnis. Dass sie nur innerhalb
   eines Simulationsschritts aufgerufen wird, stellen die aufrufenden Systeme sicher
   (Simulationskern).
3. **Im Zweifel lehnt sie ab.** Jede fehlende, ungültige oder unbekannte Eingabe (ein
   Wert außerhalb von 0–1, NaN, eine unbekannte Handlungsart, eine fehlende Norm) führt
   zur Ablehnung mit dem Grund „ungültige Eingabe“.

**Die Anfrage** (die einzigen Eingaben)

4. Eine Anfrage enthält genau:
   - die **Handlungsart**: Zärtlichkeit, Sex zu zweit, Gruppe, Harem beitreten, Ritual
     (genau diese 5 Werte)
   - die **Phase**: Beginn (vor dem Start) oder Halten (Prüfung während einer laufenden
     Handlung, Regel 14)
   - die **Beteiligten** als Zustimmungsprofile aus Menschen & Bevölkerung (5
     Eigenschaften, Geschlecht, Freiheit, Gesundheit, im Kampf, Stammes-ID)
   - den **Ort**: privat, öffentlich, Fest, Ritual
   - die **Normen** der Kultur jedes Beteiligten für diese Handlungsart an diesem Ort:
     ein Wert 0–1 (Offenheit) und ein Merkmal „Tabu“ (Kultur & Erinnerung, vorläufiger
     Vertrag)
   - das **Begehren** jedes Beteiligten für jeden anderen, ein Wert 0–1 (Begehren &
     Lust). Es darf nur aus Eigenschaften und Vorlieben der Person entstehen. Beim
     Ritual gehört kein Begehren zur Anfrage.
   - die **Bindung**: Ist ein Beteiligter fest an jemanden gebunden, der nicht an der
     Handlung teilnimmt (Begehren & Lust, Harem & Rituale)?
5. **Ausdrücklich keine Eingabe sind:** Hingabe, Rolle, Rang, Gotteskraft, Eingriffe
   und Kräfte des Spielers, Wahl des Spielers, Ruf, Status als Haremshaupt oder
   Anführer, wer jemanden gefangen hält. Die Typen der Schnittstelle lassen diese
   Werte nicht zu (geprüft in den Acceptance Criteria).
6. **Frömmigkeit** ist eine feste Eigenschaft, die beim Hervortreten gewürfelt wird
   und die kein Eingriff ändert. Sie darf daher in die Bereitschaft für Rituale eingehen.
   Die Hingabe dagegen kann der Gott über Eingriffe verändern. Sie geht deshalb nie ein,
   auch nicht über einen Umweg.

**Anzahl der Beteiligten**

7. Zärtlichkeit und Sex zu zweit haben genau 2 Beteiligte. Eine Gruppe hat 3 bis
   N_gruppe_max (Formulas). Harem beitreten hat 2: die Person, die beitritt, und das
   Haremshaupt. Beide müssen zustimmen, aber der Status des Haremshaupts zählt nicht.
   Ritual hat 1 Beteiligten pro Prüfung. Jede Person darf in einer Anfrage nur einmal
   vorkommen. Andernfalls lautet das Ergebnis „ungültige Eingabe“.

**Harte Sperren** (lehnen immer ab, unabhängig von allem anderen)

8. Die Anfrage wird abgelehnt, wenn bei **irgendeinem** Beteiligten eines davon gilt:
   - **gefangen**
   - **verletzt** (jede Verletzungsklasse)
   - **im Kampf**
   - die Kultur des Beteiligten hat für diese Handlungsart an diesem Ort ein **Tabu**
   - eine Eingabe fehlt oder ist ungültig (Regel 3)
9. Für die Masse gibt es kein Zustimmungsprofil, also auch keine Anfrage. Kinder und
   Tiere gibt es im Spiel nicht: Es gibt kein Profil für sie und keine Handlungsart mit
   ihnen.
10. Menschen „in Gottes Hand“ treffen keine Entscheidungen (Simulationskern). Begehren
    & Lust sowie Harem & Rituale stellen für sie keine Anfrage. Wird ein Beteiligter
    während einer Handlung gepackt, endet die Handlung sofort (Regel 14), bevor die
    Physik ihn bewegt. Gewalt und Sex überschneiden sich so nie.

**Die eigene Entscheidung**

11. Greift keine harte Sperre, entscheidet **jeder Beteiligte für sich**. Sein
    Bereitschaftswert entsteht aus seinen Eigenschaften, seinem Begehren für die
    anderen, seiner Bindung und den Normen seiner Kultur (Formulas). Er stimmt zu,
    wenn sein Wert die Schwelle der Handlungsart erreicht.
12. Die Handlung findet nur statt, wenn **alle** zustimmen. Das Ergebnis nennt bei
    Ablehnung jeden, der ablehnt, mit seinem **Hauptgrund**: dem Teil seines
    Bereitschaftswerts, der ihn am weitesten unter die Schwelle drückt (Formulas).

**Gründe** (für Anzeige und Chronik)

13. Mögliche Gründe einer Ablehnung: gefangen, verletzt, im Kampf, Tabu der Kultur,
    ungültige Eingabe, kein Begehren, treu gebunden, zu wenig Lust, Norm zu streng, zu
    wenig Geselligkeit, zu wenig Frömmigkeit, zu öffentlich. Die Anzeige formt daraus einen Satz, etwa „Ilsa lehnt ab: treu
    gebunden“.

**Während der Handlung**

14. Eine laufende Handlung wird **in jedem Personentakt** der Beteiligten neu geprüft.
    Sie endet sofort, wenn eine harte Sperre greift (zum Beispiel weil jemand verletzt,
    gefangen, in einen Kampf gezogen oder von der Gotteshand gepackt wird) oder wenn
    die Bereitschaft eines Beteiligten unter die Schwelle minus der Haltemarge fällt
    (Formulas). Die Marge verhindert, dass kleine Schwankungen der Normen eine Szene
    abbrechen.
15. Jeder Beteiligte kann eine Handlung jederzeit beenden. Wann das geschieht, legen
    Begehren & Lust sowie Harem & Rituale fest. Das Zustimmungsmodell erzwingt nie, dass
    eine Handlung weitergeht.
16. Einen Harem **verlassen** braucht keine Prüfung und ist immer möglich, außer in
    Gottes Hand (dann im ersten Personentakt danach). Eine Mitgliedschaft heißt nie,
    dass jemand jeder Handlung zustimmt: Jede Handlung im Harem wird einzeln geprüft.

**Was der Spieler beeinflussen kann**

17. Der Spieler kann Menschen an einen Ort bringen (zum Beispiel aufs Fest), die
    Kultur über die Zeit verändern (Normen über Kultur & Erinnerung) und neue Menschen
    erschaffen. Er kann nie die Eigenschaften, das Begehren oder die Entscheidung eines
    Einzelnen setzen. Keine Kraft und kein Griff der Gotteshand ist eine Eingabe dieser
    Funktion.

### States and Transitions

Das Zustimmungsmodell hat **keinen eigenen Zustand**. Den Ablauf einer Handlung
(angefragt, läuft, beendet) führen Begehren & Lust sowie Harem & Rituale. Die Prüfung
greift an zwei Stellen ein:

| Zeitpunkt | Prüfung | Ergebnis |
|---|---|---|
| Vor Beginn | volle Prüfung: Sperren, dann Bereitschaft gegen Schwelle | Zustimmung → Handlung beginnt; Ablehnung → sie beginnt nicht, Grund wird gemeldet |
| In jedem Personentakt während der Handlung | Sperren, dann Bereitschaft gegen Schwelle minus Haltemarge | weiter, oder sofortiges Ende mit Grund |

### Interactions with Other Systems

| System | Was hineingeht | Was herauskommt | Wer besitzt die Schnittstelle |
|---|---|---|---|
| Menschen & Bevölkerung | Zustimmungsprofile (fester Inhalt, siehe dort Regel 18) | – | Menschen |
| Kultur & Erinnerung | Normen: Offenheit 0–1 und Tabu je Handlungsart und Ort | – | Kultur & Erinnerung (vorläufig, GDD fehlt) |
| Begehren & Lust | Anfragen für Zärtlichkeit, Sex zu zweit, Gruppe; Begehren-Werte; Bindungen | Ergebnis mit Gründen | Zustimmungsmodell (Format von Anfrage und Ergebnis) |
| Harem & Rituale | Anfragen für Harem beitreten und Ritual; Bindungen im Harem | Ergebnis mit Gründen | Zustimmungsmodell |
| Simulationskern | – | nichts: kein Zufall, kein Takt, Aufruf nur im Schritt | Kern |
| Chronik, HUD & Chronik-Ansicht | – | Gründe einer Ablehnung, die das aufrufende System als Ereignis meldet | aufrufendes System meldet, Chronik wählt aus |
| Gotteskraft & Götterkräfte, Gotteshand & Ragdoll | **keine Eingabe** (Regel 5, 17) | – | – |

## Formulas

Vorschlag des `systems-designer` (2026-10-25), im guided-Modus ohne Einzelfreigabe
übernommen; der Entwickler prüft ihn mit dem GDD. Alle Schätzungen unten sind
Näherungen über die Normalverteilung, keine Simulation. Bei der Umsetzung sind sie mit
einem Monte-Carlo-Test zu prüfen (Acceptance Criteria). Gleitkommazahlen sind erlaubt,
weil Determinismus nur auf demselben Rechner verlangt wird (Simulationskern).

### 1. Bereitschaft

The Bereitschaft formula is defined as:

`w_i = max(0, W_D·D_i + W_L·L_i + W_N·n_i + W_G·G_i + W_F·F_i − c_pub[a]·π[Ort]·(1 − G_i) − c_bind[a]·k_i·T_i)`

D_i ist bei Handlungen zu zweit das Begehren b_ij für den anderen. Beim Harem ist es das
Begehren für die andere Partei (Haremshaupt oder Beitretende). In der Gruppe gilt
`D_i = μ·min_j b_ij + (1 − μ)·mittel_j b_ij`. Beim Ritual gibt es kein D, deshalb ist
dort W_D = 0.

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| L_i, G_i, F_i, T_i | float | 0–1 | calculated | Lust, Geselligkeit, Frömmigkeit, Treue aus dem Zustimmungsprofil (fest); Treue wirkt nur über die Bindungsstrafe |
| n_i | float | 0–1 | calculated | Offenheit der Norm seiner Kultur für diese Handlungsart an diesem Ort (Kultur & Erinnerung) |
| b_ij | float | 0–1 | calculated | Begehren von i für j (Begehren & Lust) |
| k_i | bool | 0 oder 1 | calculated | an jemanden gebunden, der nicht teilnimmt |
| W_D, W_L, W_N, W_G, W_F | float | je ≥ 0, Summe genau 1 | data file | Gewichte je Handlungsart |
| c_pub[a], c_bind[a] | float | 0–0,6 | data file | Stärke der Strafen je Handlungsart |
| π[Ort] | float | 0–1 | data file | wie öffentlich der Ort ist |
| μ | float | 0–1, Default 0,5 | data file | Anteil des kleinsten Begehrens in der Gruppe |
| w_i | float | 0–1 | calculated | Bereitschaft von i |

**Standardwerte der Datentabelle:**

| Handlungsart | W_D | W_L | W_N | W_G | W_F | c_pub | c_bind |
|---|---|---|---|---|---|---|---|
| Zärtlichkeit | 0,50 | 0,20 | 0,30 | 0 | 0 | 0,20 | 0,20 |
| Sex zu zweit | 0,45 | 0,30 | 0,25 | 0 | 0 | 0,30 | 0,40 |
| Gruppe | 0,35 | 0,25 | 0,25 | 0,15 | 0 | 0,25 | 0,50 |
| Harem beitreten | 0,35 | 0,10 | 0,30 | 0,25 | 0 | 0 | 0,50 |
| Ritual | 0 | 0,20 | 0,40 | 0 | 0,40 | 0 | 0 |

π: privat 0, Ritual 0, Fest 0,5, öffentlich 1.

**Output Range:** 0–1. Die Gewichte ergeben zusammen 1, deshalb gibt es keine Division.
Den Wert 1 erreicht w nur, wenn alle Terme 1 sind und keine Strafe greift. Unter 0
verhindert das max. Jeder positive Term hat ein Gewicht ≥ 0, und die Strafe für
Öffentlichkeit sinkt mit steigender Geselligkeit. Mehr Begehren, Lust, Offenheit,
Geselligkeit oder Frömmigkeit senkt w also nie. Eine Datentabelle, deren Gewichte nicht
genau 1 ergeben, gilt als ungültig (Formel 2).
**Example:** Sex zu zweit auf einem Fest mit b = 0,6, L = 0,5, n = 0,5, G = 0,4, k = 1,
T = 0,7. Die Terme ergeben 0,27 + 0,15 + 0,125 = 0,545. Die Strafe für Öffentlichkeit
ist 0,3 · 0,5 · 0,6 = 0,09, die für die Bindung 0,4 · 0,7 = 0,28. Damit ist w = 0,175.

### 2. Schwellen

The Schwelle formula is defined as:

`zustimmung_i = (w_i ≥ θ[a])`; die Handlung findet statt, wenn `zustimmung_i` für alle Beteiligten gilt, mit `3 ≤ N_gruppe ≤ N_gruppe_max`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| θ[a] | float | 0,10–0,90 | data file | Schwelle je Handlungsart. Defaults: Zärtlichkeit 0,40, Sex zu zweit 0,48, Gruppe 0,52, Harem beitreten 0,55, Ritual 0,45 |
| N_gruppe_max | int | 3–6, Default 4 | data file | größte Gruppe. Mehr Beteiligte machen die Zustimmung aller unwahrscheinlicher und die Anzeige schwerer lesbar |

**Output Range:** ja oder nein je Beteiligtem. θ = 0 wäre automatische Zustimmung, θ > 1
hieße nie. Beides ist verboten, ebenso alles außerhalb von 0,10–0,90. Beim Laden der
Daten muss gelten: θ_Zärtlichkeit ≤ θ_Sex ≤ θ_Gruppe. Verletzt die Datentabelle eine
dieser Regeln, lehnt das Modell jede Anfrage mit „ungültige Eingabe“ ab (Regel 3).
**Example:** Der Beteiligte aus Formel 1 hat w = 0,175 gegen θ = 0,48 und lehnt ab.

### 3. Haltemarge

The Haltemarge formula is defined as:

`weiter_i = (w_i ≥ θ[a] − Δ_hold)`, geprüft nach den harten Sperren

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| Δ_hold | float | 0–0,15, Default 0,08 | data file | Haltemarge für laufende Handlungen |

**Output Range:** Die wirksame Schwelle für eine laufende Handlung ist θ − Δ_hold. Beim
Laden muss θ − Δ_hold ≥ 0,20 gelten, damit eine Handlung nicht durch eine fast leere
Schwelle unbegrenzt weiterläuft. Δ_hold = 0 bedeutet keine Hysterese.
**Example:** Sex zu zweit beginnt ab 0,48 und endet unter 0,40. Sinkt w durch eine
Normänderung von 0,46 auf 0,43, läuft die Handlung weiter.

### 4. Hauptgrund

The Hauptgrund formula is defined as:

`s_D = W_D·(1 − D)`, `s_L = W_L·(1 − L)`, `s_N = W_N·(1 − n)`, `s_G = W_G·(1 − G)`, `s_F = W_F·(1 − F)`, `s_pub = c_pub·π·(1 − G)`, `s_bind = c_bind·k·T`; `Hauptgrund = argmax(s)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| s_x | float | 0–1 | calculated | Beitrag zum Fehlbetrag: so viel hat w durch den Term x verloren |
| Hauptgrund | enum | einer der Gründe unten | calculated | gemeldeter Grund eines Ablehnenden |

Zuordnung und Reihenfolge bei Gleichstand (Werte innerhalb von 1e-9 gelten als gleich,
dann gewinnt der Erste in dieser Liste):

| Term | Grund |
|---|---|
| s_bind | treu gebunden |
| s_D | kein Begehren |
| s_pub | zu öffentlich |
| s_L | zu wenig Lust |
| s_N | Norm zu streng |
| s_G | zu wenig Geselligkeit |
| s_F | zu wenig Frömmigkeit |

**Output Range:** Berechnet nur für Ablehnende (w unter θ, oder bei laufender Handlung
unter θ − Δ_hold). Ohne Klemmen gilt w = 1 − Σ s. Bei einer Ablehnung ist die Summe also
größer als 1 − θ, und mindestens ein s ist positiv. Harte Sperren haben eigene Gründe
(gefangen, verletzt, im Kampf, Tabu, ungültige Eingabe) und kommen immer vor dieser
Formel.
**Example:** Für den Beteiligten aus Formel 1 gilt s_D = 0,18, s_L = 0,15, s_N = 0,125,
s_pub = 0,09 und s_bind = 0,28. Hauptgrund: „treu gebunden“.

### 5. Plausibilität: Wie stark formt die Kultur das Verhalten?

Annahmen: Eigenschaften wie in Menschen & Bevölkerung (Formel 5: Mittel dreier
Gleichverteilter, Mittelwert 0,5, Streuung 0,167). Für das Begehren, das Begehren & Lust
noch festlegt, ist eine Normalverteilung mit Mittelwert 0,5 und Streuung 0,2 angenommen,
auf 0–1 geklemmt. Dazu privater Ort, keine Bindung. Die Werte geben an, wie oft alle Beteiligten
zustimmen, wenn man ihre Entscheidungen als unabhängig betrachtet. Zärtlichkeit, Sex und
Harem haben 2 Beteiligte, die Gruppe 3, das Ritual 1.

| Handlungsart | Norm 0,5 | Lust-Sekte (Norm 0,9) | Gottesstaat (Norm 0,15) |
|---|---|---|---|
| Zärtlichkeit | ca. 69 % | ca. 96 % | ca. 23 % |
| Sex zu zweit | ca. 33 % | ca. 78 % | ca. 6 % |
| Gruppe (3) | ca. 4 % | ca. 50 % | unter 0,1 % |
| Harem beitreten | ca. 7 % | ca. 64 % | ca. 0,1 % |
| Ritual (1) | ca. 75 % | ca. 99 % | ca. 12 % |

Rechenweg am Beispiel Sex zu zweit bei Norm 0,5: Der mittlere Wert ist 0,5 bei einer
Streuung von etwa 0,10. Ein Einzelner stimmt mit (0,48 − 0,50) / 0,103 ≈ −0,2, also etwa
58 %, zu, beide mit 0,58² ≈ 33 %.

Die Kultur verändert das Verhalten um das 2- bis über 10-Fache, und bei Norm 0,5 stimmt
weder jeder immer noch nie zu (Pillar 3). Ein Gottesstaat sollte für Rituale eine hohe
Norm setzen und kommt dann auf etwa 99 %. Die Spanne bei Sex zu zweit, 6 % im Gottesstaat
gegen 78 % in der Lust-Sekte, ist das Prüfziel für Pillar 3 (Acceptance Criteria). Die
Schätzung übersieht, dass Begehren zwischen zwei Partnern zusammenhängen kann. Ein
Mensch mit Treue 0 bekommt keine Bindungsstrafe, das ist gewollt.

## Edge Cases

*`systems-designer` not consulted for this section — Lean mode. Review manually before production.*

- **If die Gotteshand einen Beteiligten während einer Handlung packt**: Die Ansicht
  beendet die Darstellung der Szene im selben Bild, bevor der Ragdoll startet. Die
  Simulation beendet die Handlung beim Anwenden des Ereignisses „Beginn in Gottes
  Hand“ mit dem Grund „unterbrochen“. Ragdoll und Szene sind also nie gleichzeitig zu
  sehen (nicht verhandelbare Grenze „Gewalt und Sex überschneiden sich nie“).
- **If das Begehren einseitig ist** (A begehrt B, B nicht): B lehnt mit „kein
  Begehren“ ab, die Handlung findet nicht statt.
- **If die Beteiligten aus verschiedenen Stämmen kommen**: Jeder entscheidet nach den
  Normen seiner eigenen Kultur. Ein Tabu in einer der Kulturen sperrt die ganze
  Anfrage.
- **If in einer Gruppe ein Einzelner ablehnt**: Die ganze Gruppenhandlung findet nicht
  statt. Begehren & Lust darf danach eine neue Anfrage mit weniger Beteiligten stellen,
  und die wird neu und vollständig geprüft.
- **If während einer Handlung die Norm sinkt** (zum Beispiel durch Kulturwandel): Die
  Handlung läuft weiter, solange die Bereitschaft aller über θ − Δ_hold liegt. Darunter
  endet sie. Gemeldet werden alle, die darunter liegen, mit ihrem Hauptgrund, in der
  Reihenfolge ihres Index in der Anfrage.
- **If ein Beteiligter während einer Handlung verletzt, gefangen oder in einen Kampf
  gezogen wird**: Die Handlung endet im nächsten Personentakt mit dem Grund der Sperre.
  Den Verletzten trifft dabei kein weiterer Kontakt.
- **If das Haremshaupt beim Beitritt gefangen, verletzt oder im Kampf ist**: Harte
  Sperre, der Beitritt wird abgelehnt.
- **If ein Mensch an jemanden gebunden ist, der an der Handlung teilnimmt**: Es gibt
  keine Bindungsstrafe (k_i = 0). Die Strafe gilt nur für Bindungen nach außen.
- **If dieselbe Person zweimal in einer Anfrage steht, oder die Zahl der Beteiligten
  nicht zur Handlungsart passt**: „ungültige Eingabe“.
- **If ein Wert in der Anfrage NaN, unendlich oder außerhalb von 0–1 ist, oder die
  Handlungsart oder der Ort unbekannt**: „ungültige Eingabe“. Das Modell versucht nie,
  einen Wert zu korrigieren oder zu klemmen.
- **If die Datentabelle ungültig ist** (Gewichte ergeben nicht 1, eine Schwelle liegt
  außerhalb von 0,10–0,90, die Reihenfolge der Schwellen stimmt nicht, θ − Δ_hold < 0,20):
  Jede Anfrage wird mit „ungültige Eingabe“ abgelehnt. Das Spiel läuft weiter, nur ohne
  sexuelle Handlungen. Den Fehler meldet der Ladevorgang der Datentabelle einmal in der
  Konsole und im Debug-Werkzeug; die Prüffunktion selbst meldet nichts.
- **If die Bereitschaft genau der Schwelle entspricht**: Zustimmung (≥).
- **If eine Person zur gleichen Zeit in zwei Anfragen steht** (zum Beispiel ein Fest
  und ein Ritual im selben Schritt): Das Modell ist rein und kann das nicht wissen.
  Begehren & Lust sowie Harem & Rituale stellen sicher, dass ein Mensch höchstens an
  einer laufenden Handlung teilnimmt. Die zweite Anfrage stellen sie nicht.
- **If ein Mensch aufs Fest geworfen wird**: Der Wurf selbst ist keine Eingabe. Landet
  er unverletzt, kann Begehren & Lust später eine normale Anfrage stellen. Ist er
  verletzt, sperrt die Verletzung jede Handlung, bis er geheilt ist.
- **If zwei Menschen desselben Geschlechts einander begehren**: Das Modell
  berücksichtigt das Geschlecht nicht. Ob jemand begehrt wird, entscheidet allein der
  Begehren-Wert aus Begehren & Lust.

## Dependencies

**Hängt ab von:**

| System | Art | Was gebraucht wird | GDD |
|---|---|---|---|
| Menschen & Bevölkerung | hart | Zustimmungsprofil (fester Inhalt) | ✓ `design/gdd/menschen-und-bevoelkerung.md` |
| Kultur & Erinnerung | hart | Normen: Offenheit 0–1 und Tabu je Handlungsart und Ort | ✗ noch keins (vorläufiger Vertrag) |
| Begehren & Lust | hart, nur als Daten in der Anfrage | Begehren-Werte b_ij, Bindungen | ✗ noch keins |
| Harem & Rituale | weich, nur als Daten in der Anfrage | Bindungen im Harem | ✗ noch keins |
| Simulationskern | weich | Aufruf nur innerhalb eines Schritts; kein Zufall, kein Takt | ✓ `design/gdd/simulationskern.md` |

Begehren & Lust und Harem & Rituale hängen ihrerseits vom Zustimmungsmodell ab
(Systemübersicht). Der Kreis ist über die Schnittstelle aufgelöst: Sie liefern ihre
Werte als Daten in der Anfrage, und das Zustimmungsmodell ruft nie zurück.

**Wird gebraucht von:** Begehren & Lust (Zärtlichkeit, Sex zu zweit, Gruppe) und Harem &
Rituale (Harem beitreten, Ritual). Beide müssen es in ihren Dependencies nennen und
jede Handlung durch es prüfen lassen.

**Ausdrücklich keine Abhängigkeit:** Gotteskraft & Götterkräfte und Gotteshand &
Ragdoll liefern keine Eingabe (Core Rules 5 und 17).

## Visual/Audio Requirements

Keine eigenen. Die Darstellung von Szenen, Festen und Ritualen gehört Begehren & Lust,
Harem & Rituale und Körper & Animation. Vorgabe aus diesem System: Eine Ablehnung ist
sichtbar und ruhig, zum Beispiel ein Abwenden. Sie ist nie eine Strafe oder etwas, das
den Abgelehnten bloßstellt. Nach der Farbphilosophie des Konzepts nutzen Lust und
Zustimmung warmes Amber und nie das Rot, das dem Blut gehört.

## Game Feel

Der Spieler soll spüren, dass die Menschen eigene Entscheidungen treffen und dass er
sie nur über die Kultur formen kann. Ein Gottesstaat ist sichtbar zurückhaltend, eine
Lust-Sekte sichtbar offen (Formel 5). Eine Ablehnung mit klarem Grund („treu
gebunden“) soll sich wie eine kleine Geschichte anfühlen, nicht wie ein Fehler des
Spiels.

## UI Requirements

- **Ablehnung beim Ansehen eines Einzelnen:** die letzte Ablehnung mit Grund und
  Gegenüber, etwa „lehnte Aron ab: treu gebunden“ (Entscheidung des Entwicklers vom
  2026-10-25: Ablehnungen sind mit Grund sichtbar).
- **Chronik:** Ablehnungen nur, wenn sie wichtig sind, zum Beispiel ein abgelehnter
  Haremsbeitritt oder eine Gruppe, die an einem Tabu scheitert. Was wichtig ist, legt
  die Chronik über das Gewicht fest.
- Gründe stehen in einfacher Sprache, nie als Zahl.

> **📌 UX Flag — Zustimmungsmodell**: This system has UI requirements. In Phase 4
> (Pre-Production), run `/ux-design` to create a UX spec for each screen or HUD
> element this system contributes to **before** writing epics. Stories that
> reference UI should cite `design/ux/[screen].md`, not the GDD directly.

## Cross-References

| Dokument | Was hier verwendet wird |
|---|---|
| `design/gdd/game-concept.md` (Anti-Pillars, Pillar 3, Core Mechanic 5) | nicht verhandelbare Grenzen, Zustimmung nur aus Eigenschaften und Kulturnormen, eine reine Funktion mit Property-Tests |
| `design/gdd/menschen-und-bevoelkerung.md` (Core Rule 18, Formel 5) | Zustimmungsprofil, Verteilung der Eigenschaften |
| `design/gdd/simulationskern.md` (Core Rules 2, 10) | kein Zufall, Aufruf im Schritt, Menschen in Gottes Hand entscheiden nicht |
| `design/gdd/systems-index.md` | Abhängigkeiten, Kreis mit Begehren & Lust |

## Acceptance Criteria

Entwurf des `qa-lead` (2026-10-25) mit den eingearbeiteten Korrekturen. Testwurzel:
`Assets/Tests/EditMode/Zustimmungsmodell/`. Alle Property-Tests nutzen einen festen
Startwert im Zufallsgenerator des **Tests**, nie im Modell. „Standardtabelle“ meint
die Defaults aus den Formeln 1 bis 3.

### Harte Sperren und Eingabe (nicht verhandelbar)
- **AC-01 [Logic/EditMode]** **GIVEN** 100.000 zufällige Anfragen, jede mit mindestens einem Beteiligten, der gefangen, verletzt oder im Kampf ist oder dessen Kultur ein Tabu setzt, **WHEN** `Prüfe` läuft, **THEN** gibt es 100.000 Ablehnungen und 0 Zustimmungen, der Grund ist immer eine Sperre und nie ein Bereitschaftsgrund, auch bei Begehren 1, Norm 1 und Lust 1.
- **AC-02 [Logic/EditMode]** **GIVEN** 100.000 Anfragen mit je einem ungültigen Wert (NaN, ± unendlich, unter 0, über 1, unbekannte Handlungsart, unbekannter Ort, unbekannte Phase, fehlende Norm), falscher Zahl von Beteiligten, doppelter Person oder einer Gruppe über N_gruppe_max, **WHEN** geprüft wird, **THEN** lautet das Ergebnis immer „ungültige Eingabe“, und die Anfrage des Aufrufers ist danach unverändert (nichts wird geklemmt).
- **AC-03 [Logic/EditMode]** **GIVEN** gültige Anfragen aller 5 Handlungsarten, **WHEN** die Zahl der Beteiligten variiert wird, **THEN** werden genau 2 (Zärtlichkeit, Sex, Harem), 3 bis N_gruppe_max (Gruppe) und 1 (Ritual) akzeptiert; ob ein Beteiligter Haremshaupt ist, ändert das Ergebnis nie (Vergleich gleicher Anfragen).
- **AC-04 [Logic/EditMode]** **GIVEN** ein Beteiligter ist gesperrt und liegt zugleich unter der Schwelle, **WHEN** geprüft wird, **THEN** ist der Grund die Sperre.

### Schnittstelle (nicht verhandelbar)
- **AC-05 [Logic/EditMode]** **GIVEN** die Typen `Anfrage` und `Beteiligter` und die Signatur von `Prüfe`, **WHEN** sie per Reflection geprüft werden, **THEN** ist die Feldmenge genau die Liste aus Core Rule 4; es gibt kein Feld mit Namen oder Typ Hingabe, Rolle, Rang, Gotteskraft, Eingriff, Spielerwahl, Ruf, Haremshaupt-Status oder Gefangenenhalter, und die Parameter sind genau `Anfrage` und `Datentabelle`, ohne `object`, `dynamic` oder Wörterbücher mit Zeichenketten-Schlüsseln.
- **AC-06 [Logic/EditMode]** **GIVEN** die Assembly des Zustimmungsmodells, **WHEN** ihre Referenzen gelesen werden, **THEN** verweist sie weder auf Gotteskraft, Gotteshand noch auf die Innereien von Menschen & Bevölkerung, nur auf die Assembly des Zustimmungsprofils; ein zusätzlicher Verweis lässt den Test scheitern.
- **AC-07 [Logic/EditMode]** **GIVEN** das Zustimmungsprofil, **WHEN** es geprüft wird, **THEN** enthält es genau Frömmigkeit, Lust, Treue, Mut, Geselligkeit, Geschlecht, Freiheit, Gesundheit, im Kampf und Stammes-ID (Menschen & Bevölkerung, Core Rule 18), nie Hingabe.

### Reinheit
- **AC-08 [Logic/EditMode]** **GIVEN** 10.000 Anfragen, **WHEN** sie in drei Reihenfolgen (vorwärts, rückwärts, gemischt) je zweimal geprüft werden, mit anderen Aufrufen dazwischen, **THEN** ist jedes Ergebnis bitgleich, einschließlich der Gründe und der Reihenfolge der Ablehnenden.
- **AC-09 [Logic/EditMode]** **GIVEN** die Assembly, **WHEN** sie per Reflection durchsucht wird, **THEN** gibt es keine statischen Felder außer Konstanten und unveränderlichen `static readonly`, und `Prüfe` sowie `Bereitschaft` verweisen nicht auf `System.Random`, `UnityEngine.Random`, `DateTime`, `Time`, `Application`, Dateizugriffe oder `Debug`.

### Monotonie und Einstimmigkeit
- **AC-10 [Logic/EditMode]** **GIVEN** 100.000 zufällige Anfragen ohne Sperre, **WHEN** Begehren, Lust, Norm, Geselligkeit oder Frömmigkeit eines Beteiligten erhöht wird und alles andere gleich bleibt, **THEN** wird eine Zustimmung nie zur Ablehnung und w sinkt nie (Toleranz 1e-9); höhere Treue bei Bindung nach außen (k = 1) oder ein öffentlicherer Ort erhöhen w nie.
- **AC-11 [Logic/EditMode]** **GIVEN** 100.000 Anfragen, **WHEN** für jeden Beteiligten w ≥ θ einzeln berechnet wird, **THEN** stimmt die Anfrage genau dann zu, wenn alle zustimmen; ein einzelner Ablehnender sperrt sie auch bei Begehren 1 und Norm 1 aller anderen, und jeder Ablehnende wird genau einmal genannt.

### Formeln
- **AC-12 Formel 1 und 4 [Logic/EditMode]** **GIVEN** Sex zu zweit auf einem Fest mit b = 0,6, L = 0,5, n = 0,5, G = 0,4, k = 1, T = 0,7 und der Standardtabelle, **WHEN** `Bereitschaft` und `Prüfe` laufen, **THEN** ist w = 0,175 ± 1e-6, die Anfrage wird gegen θ = 0,48 abgelehnt, der Hauptgrund ist „treu gebunden“, und s_D = 0,18, s_L = 0,15, s_N = 0,125, s_pub = 0,09, s_bind = 0,28 (je ± 1e-6).
- **AC-13 Formel 2 [Logic/EditMode]** **GIVEN** eine Testtabelle, deren Schwelle genau auf das berechnete w gesetzt ist, **WHEN** geprüft wird, **THEN** wird zugestimmt (≥), und bei θ = w + 1e-6 wird abgelehnt.
- **AC-14 Formel 1, Gruppe [Logic/EditMode]** **GIVEN** μ = 0,5 und Begehren 0,2 / 0,8 / 0,5 eines Beteiligten für die anderen, **WHEN** D berechnet wird, **THEN** ist D = 0,5 · 0,2 + 0,5 · 0,5 = 0,35 ± 1e-6; ein Ritual braucht und akzeptiert kein Begehren.
- **AC-15 Formel 1, Grenzen [Logic/EditMode]** **GIVEN** 100.000 zufällige Anfragen, **WHEN** w berechnet wird, **THEN** gilt immer 0 ≤ w ≤ 1, und ein roher Wert unter 0 ergibt 0.
- **AC-16 Formel 3 [Logic/EditMode]** **GIVEN** θ = 0,48 und Δ_hold = 0,08, **WHEN** w = 0,46 bei Phase Beginn und dann 0,43 und 0,39 bei Phase Halten ist, **THEN** wird der Beginn abgelehnt, die laufende Handlung bei 0,43 fortgesetzt und bei 0,39 mit Hauptgrund beendet.
- **AC-17 Formel 4 [Logic/EditMode]** **GIVEN** zwei gleich große Beiträge (Differenz unter 1e-9), **WHEN** der Hauptgrund gewählt wird, **THEN** gewinnt der frühere in der Liste (Bindung, Begehren, Öffentlichkeit, Lust, Norm, Geselligkeit, Frömmigkeit); in 10.000 Zufallsfällen hat der Hauptgrund immer den größten Beitrag.
- **AC-18 Edge Case Bindung [Logic/EditMode]** **GIVEN** ein Beteiligter ist an einen anderen Teilnehmer derselben Handlung gebunden, **WHEN** geprüft wird, **THEN** ist k = 0, und es gibt keine Bindungsstrafe.

### Datentabelle
- **AC-19 [Logic/EditMode]** **GIVEN** je eine ungültige Tabelle (Gewichte ≠ 1 bei Toleranz 1e-6, θ unter 0,10 oder über 0,90, θ_Zärtlichkeit > θ_Sex oder θ_Sex > θ_Gruppe, θ − Δ_hold < 0,20, Δ_hold > 0,15, c über 0,6), **WHEN** sie geladen wird und 1.000 Anfragen folgen, **THEN** wird jede mit „ungültige Eingabe“ abgelehnt, der Ladevorgang meldet den Fehler genau einmal, `Prüfe` meldet nichts, und die Standardtabelle wird akzeptiert.

### Pillar 3 (Monte Carlo)
- **AC-20 [Logic/EditMode]** **GIVEN** 20.000 zufällige Paare (fester Startwert; Eigenschaften wie Menschen & Bevölkerung Formel 5; Begehren normalverteilt mit 0,5 und 0,2, auf 0–1 geklemmt; privat; keine Bindung), **WHEN** Sex zu zweit bei Norm 0,9, 0,15 und 0,5 geprüft wird, **THEN** liegt der Anteil der Zustimmungen bei 78 % ± 10 Punkte, 6 % ± 4 Punkte und bei Norm 0,5 echt über 0 % und unter 100 % (erwartet etwa 33 %).

### Gotteshand in einer Szene (nicht verhandelbar)
- **AC-21 [Integration/PlayMode]** **GIVEN** eine laufende Szene, **WHEN** die Gotteshand einen Beteiligten packt, **THEN** beendet die Ansicht die Darstellung der Szene im selben Bild vor dem ersten Ragdoll-Bild, die Simulation beendet die Handlung mit „unterbrochen“ beim Ereignis „Beginn in Gottes Hand“, und in keinem Bild sind Szene und Ragdoll zugleich zu sehen.
- **AC-22 [Manual]** **GIVEN** dieselbe Situation im Build, **WHEN** gepackt wird, **THEN** liegen Screenshots der Bilder davor, dabei und danach in `production/qa/evidence/` mit Freigabe des Leads, und Szene und Ragdoll sind nie gleichzeitig sichtbar.

## Open Questions

| Frage | Wer klärt | Bis wann |
|---|---|---|
| Welche Normen (Offenheit, Tabu) hat eine Kultur je Handlungsart und Ort, und wie verändern sie sich? | GDD Kultur & Erinnerung | mit dem GDD |
| Wie entsteht der Begehren-Wert b_ij, und welche Vorlieben gibt es? Die Verteilung in Formel 5 ist bis dahin angenommen | GDD Begehren & Lust | mit dem GDD |
| Darf die Gotteshand Beteiligte einer laufenden Szene überhaupt packen, oder sind sie dann nicht greifbar? Vorerst gilt: Packen ist erlaubt und beendet die Szene (Edge Cases) | GDD Gotteshand & Ragdoll | mit dem GDD |
| Stimmen die geschätzten Anteile aus Formel 5 nach der Umsetzung? | Monte-Carlo-Test AC-20 | erste Umsetzung im MVP-0 |
