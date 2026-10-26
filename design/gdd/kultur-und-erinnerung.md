# Kultur & Erinnerung

> **Status**: Draft (Nachtarbeit 2026-10-26, ohne Freigabe des Entwicklers; jede Entscheidung steht in `production/nachtarbeit-2026-10-26.md`)
> **Author**: Claude (autonom, mit Erlaubnis des Entwicklers)
> **Last Updated**: 2026-10-26
> **Last Verified**: 2026-10-26
> **Implements Pillar**: Pillar 2 (Jeder Eingriff hinterlässt eine Spur), Pillar 3 (Lust ist Kultur, nicht Beiwerk), Pillar 1 (Die Welt lebt ohne dich)

## Summary

Kultur & Erinnerung führt für jeden Stamm seine Kultur: eine Achse von Gottesstaat (−1)
bis Lust-Sekte (+1), daraus die Gesellschaftsform, die Stammeshingabe D_t, die sexuellen
Normen für das Zustimmungsmodell und eine begrenzte Erinnerung an die Eingriffe des
Gottes, die verblasst. Ein Ursachenbuch hält fest, was die Achse bewegt hat, damit die
Chronik jeden Kulturwandel erklären kann („weil du …“). Achse, Hingabe, Erinnerung und
Gesellschaftsform sind 1:1 aus Prototyp 1; die Normen sind neu.

> **Quick reference** — Layer: `Gameplay` · Priority: `MVP` · Key deps: `Simulationskern, Welt & Insel, Stämme & Diplomatie`

## Overview

Jede Kultur hat eine Position auf einer Achse: Strafen, Angst, Hunger und Dürre ziehen sie
zum Strengen (Gottesstaat), Wohlstand, Frieden und Feste zum Freien (Lust-Sekte), und ein
eigenes Temperament gibt jedem Stamm eine Neigung. Überschreitet die Achse eine Grenze,
wechselt die Gesellschaftsform; der Wechsel ist ein großes Ereignis mit Ursache. Jeder
Eingriff des Gottes (Blitz, Fest-Segen, später Würfe der Gotteshand) wird als
**Erinnerung** gespeichert und schiebt die Kultur noch Jahrzehnte weiter, während er
langsam verblasst. Das **Ursachenbuch** sammelt, was die Achse bewegt hat, und verblasst
schneller; aus ihm nennt die Chronik den Grund eines Wandels. Aus der Achse folgen die
**Normen**, also wie offen die Kultur für Zärtlichkeit, Sex, Gruppen, Harem und Rituale ist
und was tabu ist. Sie folgen der Achse nur langsam, damit kein Eingriff Zustimmung im
Moment erzwingt. Die **Stammeshingabe** steigt, je ausgeprägter eine Kultur ist, und sinkt
im Hunger; Menschen & Bevölkerung rechnet mit ihr die Gnade.

## Player Fantasy

Der Spieler formt Kulturen mit der Zeit und sieht es: Ein Stamm, den er immer wieder segnet,
wird über Jahrzehnte freizügig, und die Chronik sagt „Kesh wird zur Lust-Sekte, weil du in
Ära III ihr Fest gesegnet hast“. Ein Stamm, den er straft, wird ängstlich und streng. Er
weiß, dass ihm die Kultur nicht sofort gehorcht, sondern sich erinnert: Was er heute tut,
wirkt noch Generationen nach.

## Detailed Design

### Core Rules

*Entwurf. Achse, Hingabe, Erinnerung, Ursachenbuch und Gesellschaftsform 1:1 aus Prototyp 1
(`prototype.html`, `govFor`, `addLedger`, `remember`, `causeOf`, Kultur-Teil von `step`,
`applyPower`); die Normen (Regeln 13–16) sind neu. Abweichungen stehen unter
„Kalibrierung gegen Prototyp 1“.*

**Die Kultur**

1. Jeder Stamm hat genau eine Kultur mit: Achse x_t (−1 bis 1), Temperament τ_t, Stammeshingabe
   D_t (0,05 bis 1), Gesellschaftsform, Ursachenbuch, Erinnerung (höchstens 8 Einträge),
   Normen (Regel 13) und Tabus (Regel 15). Die Kultur entsteht mit dem Stamm (Regel 18)
   und bleibt nach seinem Untergang **eingefroren** lesbar: Sie ändert sich nicht mehr,
   aber das Zustimmungsmodell liest ihre Normen noch für Menschen, die den Stamm vor
   Kurzem verlassen haben (Zustimmungsmodell Regel 4).
2. Die Achse liegt bei −1 (streng, Gottesstaat) bis +1 (frei, Lust-Sekte). Sie wird nach
   jeder Änderung auf −1 bis 1 geklemmt.

**Jährlicher Kulturwandel**

3. Einmal pro Geschichtsjahr bewegt sich die Achse jedes lebenden Stamms um die Summe
   dieser Teile (Formel 2): Zufall und Temperament; Wohlstand (+, wenn kein Krieg, kein
   Hunger und volle Speicher); Hunger (−); Dürre (−); der Sog der eigenen
   Gesellschaftsform (Lust-Sekte +, Gottesstaat −); und jede Erinnerung schiebt in ihre
   Richtung, gewichtet mit ihrem verbleibenden Gewicht.
4. Jeder Teil wird im **Ursachenbuch** unter seinem Grund verbucht (Zufall, Wohlstand,
   Hunger, Dürre, Sog, Krieg, und je Eingriff unter seiner Eingriffs-ID). Das Ursachenbuch
   verblasst jährlich um den Faktor 0,94 (Formel 4).
5. Krieg verschiebt die Achse zusätzlich sofort, wenn Stämme & Diplomatie es als Auftrag
   meldet („Achse verschieben, Ursache Krieg“, Stämme Regel 9).

**Erinnerung an Eingriffe**

6. Der Auftrag **„Eingriff erfassen“** ist der einzige Weg, auf dem ein Eingriff des
   Gottes eine Kultur verändert (Simulationskern Interaktionen). Er trägt: Stamm,
   Eingriffs-ID, Art, Richtung p (−1 bis 1), Sofortwirkung Δx auf die Achse, Wirkung ΔD auf
   die Hingabe und den Text für die Chronik. Die Werte legen die GDDs der Eingriffe fest
   (Gotteskraft & Götterkräfte, Gotteshand & Ragdoll); Prototyp 1: Blitz p = −1,
   Δx = −0,12, ΔD = +0,15; Fest-Segen p = +1, Δx = +0,15, ΔD = +0,1.
7. Beim Erfassen gilt sofort: Achse + Δx, Hingabe + ΔD (beide geklemmt), Ursachenbuch-
   Eintrag der Eingriffs-ID = Δx, und eine neue Erinnerung mit Gewicht 1. Sind schon 8
   Erinnerungen da, fällt die älteste heraus (ihr Ursachenbuch-Eintrag bleibt und
   verblasst).
8. Jedes Jahr verblasst jede Erinnerung um den Faktor 0,965 und schiebt danach die Achse um
   `p · Gewicht · 0,03` (Formel 3). Fällt das Gewicht auf 0,06 oder darunter, wird sie
   vergessen (nach 79 Jahren, bei 1× etwa 40 s). Ein einzelner Fest-Segen schiebt die Achse
   so insgesamt um etwa +0,93 (0,15 sofort und 0,78 über die Jahre), wenn nichts
   dagegenwirkt.

**Gesellschaftsform**

9. Es gibt drei Gesellschaftsformen: **Stammesrat**, **Lust-Sekte** und **Gottesstaat**. Ein
   Stammesrat wird zur Lust-Sekte, wenn die Achse über 0,5 steigt, und zum Gottesstaat,
   wenn sie unter −0,5 fällt. Eine Lust-Sekte wird wieder Stammesrat, wenn die Achse unter
   0,35 fällt, ein Gottesstaat, wenn sie über −0,35 steigt (Formel 5). Von der Lust-Sekte
   zum Gottesstaat geht es nur über den Stammesrat.
10. Ein Wechsel ist das große Ereignis **„Kulturwandel“**. Es nennt die neue Form und die
    **Ursache**: den Eintrag im Ursachenbuch, der am stärksten in Richtung des Wechsels
    zeigt (Formel 4). Ist es ein Eingriff, trägt das Ereignis dessen Eingriffs-ID, und die
    Chronik schreibt „weil du …“ mit dem Text der Erinnerung; ist die Erinnerung schon
    vergessen, „in Erinnerung an einen alten Eingriff des Gottes“.

**Stammeshingabe**

11. Die Stammeshingabe D_t wandert jährlich: Sie steigt, je weiter die Achse von der Mitte
    entfernt ist (ausgeprägte Kulturen sind frommer), sinkt im Hunger und streut leicht
    (Formel 1). Eingriffe ändern sie über ΔD (Regel 7).
12. Menschen & Bevölkerung liest D_t für die Gnade und die persönliche Hingabe (Menschen
    Regeln 16, 17), Gotteskraft & Götterkräfte für den Zufluss aus Ritualen.
    **D_t ist keine Eingabe des Zustimmungsmodells** und keiner Norm (Zustimmungsmodell
    Regel 5).

**Normen für das Zustimmungsmodell** (neu, nicht im Prototyp)

13. Für jede Handlungsart (Zärtlichkeit, Sex zu zweit, Gruppe, Harem beitreten, Ritual)
    hat eine Kultur eine **Offenheit** n zwischen 0 und 1. Sie hängt nur von der Achse ab,
    nicht vom Ort; wie öffentlich der Ort ist, rechnet das Zustimmungsmodell selbst
    (π[Ort], Zustimmungsmodell Formel 1). Ziel der Offenheit ist `n_Ziel(x)` (Formel 6):
    Für die vier sexuellen Handlungsarten steigt sie mit der Achse (Gottesstaat streng,
    Lust-Sekte offen), für das Ritual mit dem Betrag der Achse (beide ausgeprägten
    Kulturen feiern Rituale, der Stammesrat weniger).
14. Die Offenheit folgt ihrem Ziel **langsam**: höchstens um 0,02 pro Geschichtsjahr
    (Formel 6). So setzt kein Eingriff eine Norm direkt, auch wenn er die Achse sofort
    bewegt (Zustimmungsmodell, Pflichten der Aufrufer).
15. **Tabus** hängen an der Gesellschaftsform und gelten je Handlungsart und Ort (Tabelle in
    Formel 6). Ein Tabu sperrt die Handlung im Zustimmungsmodell ganz. Kommt mit einem
    Wechsel der Gesellschaftsform ein Tabu hinzu, gilt es sofort; fällt eines weg, gilt es
    noch T_tabu = 5 Geschichtsjahre weiter (im Zweifel ablehnen). Wechselt die Form in
    dieser Zeit zurück, bleibt das Tabu ohne Unterbrechung.
16. Das Zustimmungsmodell liest die Normen über eine reine Abfrage
    `Norm(Stamm, Handlungsart, Ort) → (n, Tabu)`. Ein Ort, der zur Handlungsart nicht passt
    (Ort Ritual ohne Handlungsart Ritual), ist eine ungültige Eingabe des
    Zustimmungsmodells, nicht dieses Systems; diese Abfrage liefert dann (0, Tabu).

**Neue Kultur, Mischung**

17. Beim Erzeugen der Welt legt dieses System die Kulturen der Startstämme 0 und 1 an (nach
    Menschen & Bevölkerung, vor Stämme & Diplomatie; Simulationskern Regel 17). Bei einer
    Abspaltung legt es auf Auftrag die Kultur des neuen Stamms an (Stämme Regel 16), bei
    einer Eroberung mischt es auf Auftrag die Achse des Siegers (Stämme Formel 4).
18. Eine neue Kultur bekommt (Formel 7): Achse (Start gewürfelt, Abspaltung vorgegeben),
    Temperament (gewürfelt), Hingabe (Start 0,4, Abspaltung die des Elternstamms), die
    Gesellschaftsform, die zur Achse passt, Normen gleich ihrem Ziel (eine neue Kultur
    ändert sich nicht, sie beginnt), die Tabus ihrer Form, ein leeres Ursachenbuch und keine
    Erinnerung.

**Reihenfolge im Schritt**

19. Kultur & Erinnerung ist ein Jahressystem mit Versatz O_year = 1 (Simulationskern
    Formel 5, nach Welt & Insel, vor Menschen & Bevölkerung und Stämme & Diplomatie). Für
    jeden lebenden Stamm in aufsteigender ID: (1) Ursachenbuch verblasst, Einträge unter
    0,001 fallen weg; (2) Erinnerungen verblassen, vergessene fallen weg; (3) Hingabe
    (1 Wurf); (4) Achse (3 Würfe); (5) Gesellschaftsform und Ereignis; (6) Normen folgen,
    Tabus werden angepasst. Gelesen werden Hunger, Dürre, Vorrat und Volk von Welt &
    Insel und Menschen desselben Jahres (Volk vor der Gnade dieses Jahres), Krieg von
    Stämme & Diplomatie aus dem Vorjahr (Stand nach dem vorigen Schritt).

### States and Transitions

| Gesellschaftsform | Bedeutung | Wechsel |
|---|---|---|
| **Stammesrat** | Mitte; Gruppe öffentlich tabu | → Lust-Sekte bei x > 0,5 · → Gottesstaat bei x < −0,5 |
| **Lust-Sekte** | frei; Sog +0,003 pro Jahr, Aggression sinkt (Stämme Formel 7); keine Tabus | → Stammesrat bei x < 0,35 |
| **Gottesstaat** | streng; Sog −0,003 pro Jahr; Gruppe überall tabu, Sex zu zweit öffentlich und auf dem Fest tabu | → Stammesrat bei x > −0,35 |

| Zustand einer Erinnerung | Bedeutung | Wechsel |
|---|---|---|
| **Frisch** | Gewicht 1 beim Erfassen | → verblassend |
| **Verblassend** | Gewicht 0,06 bis 1, schiebt jährlich | → vergessen bei Gewicht ≤ 0,06 oder wenn eine neunte kommt |
| **Vergessen** | entfernt; ihr Ursachenbuch-Eintrag verblasst weiter | endgültig |

### Interactions with Other Systems

| System | Was hineingeht | Was herauskommt | Wer besitzt die Schnittstelle |
|---|---|---|---|
| Simulationskern | Jahrestakt O_year = 1, Zufallsstrom system_id 3, Erzeugen nach Menschen | Ereignis „Kulturwandel“ (neue Form, Ursache, Ursachenbuch-Auszug) | Kern; „Eingriff erfassen“ besitzt dieses System |
| Welt & Insel | hungert in diesem Jahr (mit Hauptgrund), Dürre aktiv, food_t | – | Welt & Insel |
| Menschen & Bevölkerung | V_t (vor der Gnade dieses Jahres) | D_t (lesend) | dieses System |
| Stämme & Diplomatie | im Krieg (Vorjahr); Aufträge „Kultur anlegen (x, Hingabe)“, „Achse verschieben (Wert, Ursache Krieg)“, „Achsen mischen (x_S, V_S, j, x_L)“ | x_t, Gesellschaftsform (lesend) | dieses System |
| Zustimmungsmodell | – | `Norm(Stamm, Handlungsart, Ort) → (n, Tabu)`, auch für eingefrorene Kulturen | dieses System; die Pflichten aus Zustimmungsmodell AC-27 übernimmt AC-12 hier |
| Gotteskraft & Götterkräfte, Gotteshand & Ragdoll | Auftrag „Eingriff erfassen“ | D_t (lesend, Zufluss aus Ritualen), x_t (wer einen Eingriff als Frevel sieht) | dieses System (Schnittstelle), die Eingriffe (Werte) |
| Begehren & Lust, Harem & Rituale | – | Normen und Gesellschaftsform (lesend, z. B. ob Feste stattfinden) | dieses System |
| Chronik | – | Ereignisse mit Ursache, Texte der Erinnerungen | Kern-Ereignisformat |
| Weltansicht & Kamera, HUD | – | Achse, Form, Hingabe, Erinnerungen mit Gewicht, die größten Ursachen | dieses System |
| Speichern & Laden | kompletter Zustand | alle Felder aus Regel 1 je Kultur, auch eingefrorener, Ursachenbuch in fester Reihenfolge, Erinnerungen in Reihenfolge des Erfassens, je Tabu das Jahr, bis zu dem es nachwirkt | Speichern & Laden |

## Formulas

Übernahme aus `prototypes/weltengott-concept/prototype.html` (Formeln 1–5 und 7), Formel 6
neu. Alle Zufallszahlen kommen aus dem Strom system_id 3 (`NextDouble()` = u,
Simulationskern Formel 6), in der Reihenfolge von Regel 19. Toleranz in Tests 1e-9.

### 1. Stammeshingabe

The Hingabe formula is defined as:

`D_t ← clamp(D_t + 0,004 · (|x_t| − 0,2) − 0,01 · [hungert] + (u − 0,5) · 0,01, 0,05, 1)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| D_t | double | 0,05–1 | calculated | Stammeshingabe |
| x_t | double | −1 bis 1 | calculated | Achse vor dem Kulturwandel dieses Jahres |
| hungert | bool | – | calculated | Welt & Insel, dieses Jahr |
| u | double | 0–1 | calculated | ein Wurf |

**Output Range:** Pro Jahr höchstens +0,0082 (bei |x| = 1) und höchstens −0,0158 (bei x = 0
im Hunger). Bei |x| > 0,2 steigt D ohne Hunger im Mittel; eine Kultur in der Mitte verliert
langsam Hingabe (−0,0008 pro Jahr bei x = 0).
**Example:** D = 0,4, x = 0,6, kein Hunger, u = 0,5 → 0,4 + 0,004 · 0,4 = 0,4016; mit Hunger
0,3916.

### 2. Kulturwandel der Achse

The Achse formula is defined as:

`Δx = z + τ_t + 0,003 · [Wohlstand] − 0,03 · [hungert] − 0,003 · [Dürre] + 0,003 · [Lust-Sekte] − 0,003 · [Gottesstaat] + Σ_Erinnerungen 0,03 · p · w` mit `z = (u₁ + u₂ + u₃ − 1,5) · 0,014` und `Wohlstand ⇔ nicht im Krieg ∧ food_t > 0,8 · V_t ∧ nicht hungert` · `x_t ← clamp(x_t + Δx, −1, 1)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| z | double | −0,021 bis 0,021 | calculated | Zufall, Mittel 0, Streuung 0,007 |
| τ_t | double | −0,006 bis 0,006 | calculated | Temperament (Formel 7) |
| p, w | double | p −1 bis 1, w 0,06–1 | calculated | Richtung und Gewicht einer Erinnerung nach dem Verblassen dieses Jahres |
| food_t, V_t | double, int | ≥ 0 | calculated | Welt & Insel, Menschen (V vor der Gnade) |
| Konstanten | double | 0,014; 0,003; 0,03; 0,8 | data file | Prototyp 1 |

**Output Range:** Ohne Erinnerung und Hunger bewegt sich die Achse pro Jahr um höchstens
0,03; Hunger zieht sie um 0,03 zum Strengen. Eine frische Erinnerung schiebt im ersten Jahr
um 0,029. Jeder Teil wird im Ursachenbuch verbucht: z + τ_t unter „Zufall“, die anderen unter
ihrem Grund, jede Erinnerung unter ihrer Eingriffs-ID.
**Example:** x = 0,3, Würfe 0,5 / 0,5 / 0,5 (z = 0), τ = 0,002, Wohlstand, keine Dürre,
Stammesrat, eine Erinnerung p = +1 mit w = 0,5 → Δx = 0,002 + 0,003 + 0,015 = 0,02; x = 0,32.

### 3. Erinnerung

The Erinnerung formula is defined as:

jährlich je Erinnerung: `w ← 0,965 · w`; vergessen, wenn `w ≤ 0,06`; sonst Schub `0,03 · p · w` (Formel 2) · beim Erfassen: `w = 1`; bei 8 Erinnerungen fällt die älteste heraus

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| w | double | 0,06–1 | calculated | Gewicht |
| Verblassen | double | 0,965 | data file | Prototyp 1 |
| Vergessen ab | double | 0,06 | data file | Prototyp 1 |
| Höchstzahl | int | 8 | data file | Prototyp 1 |

**Output Range:** Eine Erinnerung schiebt 78 Jahre lang (0,965^78 = 0,0621, 0,965^79 = 0,0599)
und insgesamt um `0,03 · p · Σ_{k=1}^{78} 0,965^k = 0,7757 · p`. Nach 20 Jahren hat sie noch
das Gewicht 0,49.
**Example:** Ein Fest-Segen (p = +1, Δx = +0,15) in einem Stamm mit x = −0,4: sofort −0,25,
nach dem ersten Jahr etwa −0,22 (Schub 0,029), wenn nichts anderes wirkt; nach 78 Jahren
wäre er bei etwa +0,53.

### 4. Ursachenbuch und Ursache

The Ursache formula is defined as:

jährlich je Eintrag `v ← 0,94 · v`, Eintrag fällt weg bei `|v| < 0,001`; danach Buchungen dieses Jahres · `Ursache(Richtung r) = argmax_Eintrag (r · v)` über Einträge mit `r · v > 0` (Gleichstand: Zufall, Wohlstand, Hunger, Dürre, Sog, Krieg, dann Eingriffs-IDs aufsteigend); keiner → „Zufall“

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| v | double | – | calculated | Summe der Buchungen eines Grunds, verblassend |
| r | int | ±1 | calculated | +1 bei einem Wechsel zur Lust-Sekte oder vom Gottesstaat zum Stammesrat, −1 sonst |
| Verblassen | double | 0,94 | data file | Prototyp 1 |

**Output Range:** Ein Grund mit konstanter Buchung b strebt gegen `b / 0,06`, Wohlstand also
gegen 0,05, Hunger gegen −0,5. Der Sofort-Eintrag eines Blitzes (−0,12) und seine jährlichen
Schübe (−0,029, −0,028, …) bringen den Eintrag der Eingriffs-ID nach etwa 17 Jahren auf
höchstens −0,27, danach sinkt er mit der Erinnerung; darum gewinnt ein Eingriff gegen jahrelangen Hunger nur, wenn er neu
ist oder öfter kommt.
**Example:** Wechsel zum Gottesstaat (r = −1), Ursachenbuch: Krieg −0,05, Eingriff 77 −0,2,
Zufall +0,01 → Ursache Eingriff 77, „weil du …“ mit dem Text seiner Erinnerung.

**Textvorlagen** (Datendatei, aus `CAUSE_TEXT` von Prototyp 1):

| Grund | Text |
|---|---|
| Wohlstand | „nach Jahren voller Vorräte und Frieden“ |
| Hunger | „nach Hunger und Not“ (mit Hauptgrund von Welt & Insel: „nach dem Hunger in der Dürre“ usw.) |
| Krieg | „nach den Verlusten im Krieg“ |
| Dürre | „unter der langen Dürre“ |
| Zufall | „aus innerer Unruhe“ |
| Sog | „weil der alte Glaube sich festigte“ |
| Eingriff, erinnert | „weil du {Text der Erinnerung}“ |
| Eingriff, vergessen | „in Erinnerung an einen alten Eingriff des Gottes“ |

### 5. Gesellschaftsform

The Gesellschaftsform formula is defined as:

`Form(x, alt)`: alt = Lust-Sekte → Stammesrat, falls `x < 0,35`, sonst Lust-Sekte · alt = Gottesstaat → Stammesrat, falls `x > −0,35`, sonst Gottesstaat · alt = Stammesrat → Lust-Sekte, falls `x > 0,5`; Gottesstaat, falls `x < −0,5`; sonst Stammesrat

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| Eintrittsgrenze | double | 0,5 | data file | Prototyp 1 |
| Austrittsgrenze | double | 0,35 | data file | Prototyp 1 (die Lücke von 0,15 verhindert ein Flackern) |

**Output Range:** Höchstens ein Wechsel pro Jahr. Eine Lust-Sekte wird nie direkt zum
Gottesstaat.
**Example:** Stammesrat mit x = 0,52 → Lust-Sekte; fällt x danach auf 0,4, bleibt sie
Lust-Sekte; bei 0,34 wird sie wieder Stammesrat.

### 6. Normen

The Normen formula is defined as:

`n_Ziel[a](x) = clamp(α[a] + β[a] · x, 0, 1)` für die vier sexuellen Handlungsarten · `n_Ziel[Ritual](x) = clamp(α_R + β_R · |x|, 0, 1)` · jährlich `n[a] ← n[a] + clamp(n_Ziel[a] − n[a], −0,02, +0,02)` · Tabu je Gesellschaftsform, Handlungsart und Ort aus der Tabelle; ein wegfallendes Tabu gilt noch T_tabu Jahre

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| α[a], β[a] | double | α 0–1, β 0–1 | data file | Default für alle vier: α = 0,5, β = 0,5 |
| α_R, β_R | double | 0–1 | data file | Default α_R = 0,5, β_R = 0,4 |
| Δn_max | double | 0,02 / Jahr | data file | höchste Änderung einer Norm pro Geschichtsjahr |
| T_tabu | int | 5 Geschichtsjahre | data file | Nachwirkung eines wegfallenden Tabus |

**Tabus (Default):**

| Gesellschaftsform | privat | Fest | öffentlich |
|---|---|---|---|
| Lust-Sekte | – | – | – |
| Stammesrat | – | – | Gruppe |
| Gottesstaat | Gruppe | Gruppe, Sex zu zweit | Gruppe, Sex zu zweit |

Zärtlichkeit, Harem beitreten und Ritual sind nirgends tabu. Ein Kontakt im Ritual wird als
eigene Handlungsart an seinem Ort geprüft (Zustimmungsmodell Regel 7), also gelten dort deren
Tabus.

**Output Range:** n zwischen 0 und 1. Lust-Sekte mit x = 0,8: sexuelle Normen 0,9, Ritual
0,82. Stammesrat mit x = 0: 0,5 und 0,5. Gottesstaat mit x = −0,8: 0,1 und 0,82. Das deckt
die Spanne, mit der das Zustimmungsmodell rechnet (dort 0,15, 0,5 und 0,9; Sex zu zweit
etwa 6 % gegen 69 %). Von 0,5 bis 0,9 braucht eine Norm 20 Geschichtsjahre (bei 1× 10 s).
**Example:** Ein Stamm mit x = 0,2 (n = 0,6) bekommt einen Fest-Segen, x springt auf 0,35:
n_Ziel = 0,675, n wird im nächsten Kulturjahr 0,62, im übernächsten 0,64 (das Ziel wandert
mit der Achse weiter).

### 7. Neue Kultur und Mischung

The Neue-Kultur formula is defined as:

Start: `x = (u − 0,5) · 0,4`, dann `τ = (u − 0,5) · 0,012`, `D = 0,4` · Abspaltung: `x = x_neu` (Stämme Formel 6), `τ = (u − 0,5) · 0,012`, `D = D_Eltern` · beide: `Form = Form(x, Stammesrat)`, `n[a] = n_Ziel[a](x)`, Tabus der Form ohne Nachwirkung, Ursachenbuch leer, keine Erinnerung · Mischung bei Eroberung: `x_S ← (x_S · (V_S + j) + x_L · j) / max(1, V_S + 2j)` (Stämme Formel 4)

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| x | double | Start −0,2 bis 0,2 | calculated | Startachse |
| τ | double | −0,006 bis 0,006 | calculated | Temperament |
| D | double | 0,05–1 | calculated | Start 0,4 (Prototyp 1) |

**Output Range:** Startstämme beginnen als Stammesrat mit Normen 0,4 bis 0,6. Würfe beim
Erzeugen: Stamm 0 (x, τ), dann Stamm 1 (x, τ).
**Example:** u = 0,75 und 0,25 → x = 0,1, τ = −0,003, Normen 0,55, Ritual 0,54.

### Kalibrierung gegen Prototyp 1

Achse, Hingabe, Erinnerung, Ursachenbuch und Gesellschaftsform sind die von Prototyp 1 und
gelten als „fest bis zur ersten Kalibrierungsmessung“ (AC-17). Abweichungen:
1. Der Zufall kommt aus dem eigenen Strom.
2. Im Prototyp rechnete die Gnade vor dem Kulturwandel desselben Stamms; hier rechnet
   Kultur vor Menschen (Simulationskern Formel 5), also prüft der Wohlstand das Volk vor
   der Gnade dieses Jahres. Der Unterschied liegt bei wenigen Köpfen.
3. Gleichstände im Ursachenbuch folgen einer festen Reihenfolge statt der
   Einfügereihenfolge.
4. Neu und ohne Wirkung auf die Achse: Normen und Tabus (Regeln 13–16), eingefrorene
   Kulturen untergegangener Stämme, der Hauptgrund des Hungers im Text.

## Edge Cases

- **If ein Eingriff die Achse oder die Hingabe über die Grenzen treiben würde**: Sie werden
  geklemmt; der Ursachenbuch-Eintrag ist trotzdem das volle Δx (wie im Prototyp).
- **If eine neunte Erinnerung kommt**: Die älteste fällt heraus, ihr Ursachenbuch-Eintrag
  bleibt. Ein Kulturwandel kann dann noch auf diesen Eingriff zeigen („alter Eingriff“).
- **If zwei Eingriffe im selben Schritt denselben Stamm treffen**: Sie werden in der
  Reihenfolge ihrer Eingriffs-IDs erfasst; jeder ist eine eigene Erinnerung.
- **If ein Eingriff einen untergegangenen Stamm treffen würde**: „Eingriff erfassen“ für eine
  eingefrorene Kultur wird ignoriert.
- **If die Achse genau auf einer Grenze liegt** (x = 0,5 im Stammesrat, x = 0,35 in der
  Lust-Sekte): kein Wechsel (die Vergleiche sind streng).
- **If im selben Jahr Hunger und Wohlstand zuträfen**: Unmöglich, Wohlstand setzt „nicht
  hungert“ voraus.
- **If die Gesellschaftsform hin- und zurückwechselt, während ein Tabu nachwirkt**: Das Tabu
  bleibt ohne Lücke; die Nachwirkung beginnt erst beim nächsten Wegfall neu.
- **If eine Norm sinkt, während eine Handlung läuft**: Das prüft das Zustimmungsmodell
  (dortige Edge Cases); dieses System ändert eine Norm höchstens um 0,02 pro Jahr.
- **If V_t = 0 beim Wohlstand**: Wohlstand gilt nicht (food_t > 0 ist nie > 0,8 · 0 bei
  food_t = 0; bei food_t > 0 und V_t = 0 gilt er formal, der Stamm geht aber in diesem Jahr
  unter, Stämme Regel 17).
- **If ein Mensch den Stamm wechselt**: Das ändert keine Kultur; das Zustimmungsmodell nimmt
  T_akkult lang die strengere Norm beider (auch einer eingefrorenen) Kulturen.

## Dependencies

**Hängt ab von:**

| System | Art | Was gebraucht wird | GDD |
|---|---|---|---|
| Simulationskern | hart | Jahrestakt O_year = 1, Zufallsstrom system_id 3, Erzeugen, Ereignisse mit Ursache | ✓ `design/gdd/simulationskern.md` (Approved) |
| Welt & Insel | hart, nur lesend | Hunger, Hauptgrund, Dürre, food_t | ✓ `design/gdd/welt-und-insel.md` (Approved) |
| Menschen & Bevölkerung | hart, nur lesend | V_t | ✓ `design/gdd/menschen-und-bevoelkerung.md` (Approved) |
| Stämme & Diplomatie | hart | Kriegszustand (lesend); Aufträge Kultur anlegen, Achse verschieben, Achsen mischen | ✓ `design/gdd/staemme-und-diplomatie.md` (Approved) |
| Gotteskraft & Götterkräfte, Gotteshand & Ragdoll | weich | Auftrag „Eingriff erfassen“ mit p, Δx, ΔD, Text | ✗ noch keins (Vertrag in Regel 6) |

**Wird gebraucht von:** Menschen & Bevölkerung (D_t), Zustimmungsmodell (Normen, Tabus),
Stämme & Diplomatie (x_t, Form), Begehren & Lust, Harem & Rituale (Normen, Form), Gotteskraft
& Götterkräfte (D_t, x_t), Chronik (Kulturwandel mit Ursache), Weltansicht & Kamera, HUD,
Speichern & Laden. Menschen, Zustimmungsmodell und Stämme nennen dieses System schon.

Die Kreise mit Menschen (D_t gegen V_t) und Stämme (x_t gegen Aufträge) sind über die
Reihenfolge im Schritt gelöst: Kultur liest V_t vor der Gnade und den Krieg des Vorjahrs,
Menschen und Stämme lesen D_t und x_t dieses Jahres.

## Tuning Knobs

Achse, Hingabe, Erinnerung und Form bleiben bis zur ersten Kalibrierungsmessung (AC-17) auf
den Werten von Prototyp 1. Die Normen (neu) sind Vorschläge und warten auf den Entwickler.

| Regler | Default | Sicherer Bereich | Wirkung |
|---|---|---|---|
| Zufall, Temperament | 0,014; ± 0,006 | 0,005–0,03; 0–0,012 | wie sehr Kulturen von selbst wandern |
| Wohlstand, Dürre, Sog | 0,003 je | 0–0,01 | sanfter Zug der Lebenslage |
| Hunger | 0,03 | 0–0,06 | Hunger macht streng |
| Schub der Erinnerung | 0,03 · p · w | 0,01–0,05 | wie stark ein Eingriff nachwirkt; 0,03 heißt etwa 0,78 Achse über die ganze Erinnerung |
| Verblassen der Erinnerung, Vergessen ab | 0,965; 0,06 | 0,95–0,98; 0,02–0,1 | wie lange ein Eingriff erinnert wird (78 Jahre) |
| Höchstzahl Erinnerungen | 8 | 4–16 | wie viele Eingriffe eine Kultur zugleich erinnert |
| Verblassen des Ursachenbuchs | 0,94 | 0,9–0,97 | wie weit die Chronik bei einem Wandel zurückblickt |
| Grenzen der Form | 0,5 / 0,35 | 0,4–0,7 / Lücke 0,1–0,25 | wie leicht Kulturen kippen |
| Hingabe | 0,004 · (\|x\| − 0,2), Hunger − 0,01, Zufall ± 0,005, Start 0,4 | – | wie fromm Stämme werden (wirkt auf Gnade und Gotteskraft) |
| Normen α, β, α_R, β_R | 0,5; 0,5; 0,5; 0,4 | α 0,3–0,7, β 0,3–0,6 | wie stark die Kultur das Verhalten ändert (Vorschlag) |
| Δn_max | 0,02 / Jahr | 0,005–0,05 | wie schnell Normen der Kultur folgen (Vorschlag) |
| Tabus, T_tabu | Tabelle, 5 Jahre | 0–20 Jahre | was eine Kultur ganz verbietet (Vorschlag) |

## Visual/Audio Requirements

Keine eigenen. Die Weltansicht zeigt die Gesellschaftsform (Materialien und Farben der Art
Bible, etwa ein Banner) und einen Kulturwandel sichtbar am Stamm; die Art Bible legt fest,
wie eine Lust-Sekte und ein Gottesstaat aussehen.

## Game Feel

Kultur soll sich wie ein langsamer Strom anfühlen, den der Spieler lenken, aber nicht
schalten kann: Ein Eingriff gibt einen spürbaren Ruck, dann zieht die Erinnerung über
Jahrzehnte weiter, und irgendwann kippt die Form. Der Moment, in dem die Chronik den Wandel
auf einen alten Eingriff zurückführt, ist das Ziel.

## UI Requirements

- **Kultur ansehen** (Teil von „Stamm ansehen“): Position auf der Achse als Balken von
  Gottesstaat bis Lust-Sekte mit Richtung der letzten Jahre, Gesellschaftsform, Hingabe als
  Wort (gering unter 0,3, fromm bis 0,6, sehr fromm darüber), die drei größten Einträge des
  Ursachenbuchs als Sätze (Eingriffe hervorgehoben, „weil du …“), die Erinnerungen mit
  verblassendem Gewicht
- **Normen** als Wort je Handlungsart (streng unter 0,3, zurückhaltend bis 0,6, offen
  darüber) und die Tabus; nie als Rohzahl
- **Meldungen** (über die Chronik): Kulturwandel mit Ursache

> **📌 UX Flag — Kultur & Erinnerung**: This system has UI requirements. In Phase 4
> (Pre-Production), run `/ux-design` to create a UX spec for each screen or HUD
> element this system contributes to **before** writing epics. Stories that
> reference UI should cite `design/ux/[screen].md`, not the GDD directly.

## Cross-References

| Dokument | Was hier verwendet wird |
|---|---|
| `prototypes/weltengott-concept/prototype.html` | `govFor`, `addLedger`, `remember`, `CAUSE_TEXT`, `causeOf`, Hingabe und Kulturwandel in `step`, `newTribe`, Blitz und Fest in `applyPower` |
| `prototypes/weltengott-concept/REPORT.md` | Kulturwandel 393 in 1.000 Ären; 89 von 100 Welten führen mindestens 2 große Ereignisse auf den Testspieler zurück; ohne Temperament drifteten alle Kulturen zur Lust-Sekte |
| `design/gdd/game-concept.md` | Pillar 2 (Spur), Pillar 3 (Lust ist Kultur), MVP-0: Kulturwandel auf einer Achse mit Erinnerung |
| `design/gdd/simulationskern.md` | Regel 4, 15, 17, Formel 5 (O_year = 1), Formel 6 (Strom 3), „Eingriff erfassen“ |
| `design/gdd/zustimmungsmodell.md` | Regeln 4, 5, 7, Formel 1 (n_i, π[Ort]), Pflichten der Aufrufer, AC-27 |
| `design/gdd/menschen-und-bevoelkerung.md` | Regeln 16, 17 (D_t) |
| `design/gdd/staemme-und-diplomatie.md` | Regeln 9, 10, 16, Formeln 3, 4, 6, 7 |
| `design/gdd/welt-und-insel.md` | Hunger mit Hauptgrund, Dürre, food_t |

## Acceptance Criteria

Testwurzel: `Assets/Tests/EditMode/KulturUndErinnerung/`. Testhilfen wie bei Stämme &
Diplomatie: `IRandomStream` (vorgegebene Würfe, scheitert bei Über- oder Unterverbrauch),
`IEventSink`, `IWorldSource` (hungert, Hauptgrund, Dürre, food_t), `IPopulationSource` (V_t),
`ITribeSource` (lebt, im Krieg), eine feste `CultureConfig` mit den Defaults dieses GDD;
Schritte einzeln über `RunStep(n)`.

- **AC-01 Formel 1 [Logic]** **GIVEN** D = 0,4, x = 0,6, kein Hunger, u = 0,5, **THEN** ist D danach 0,4016; mit Hunger 0,3916; **GIVEN** D = 0,05, x = 0, Hunger, u = 0, **THEN** bleibt D = 0,05; **GIVEN** D = 1, x = 1, u = 0,999, **THEN** bleibt D = 1.
- **AC-02 Formel 2 [Logic]** **GIVEN** x = 0,3, Würfe 0,5 / 0,5 / 0,5, τ = 0,002, Wohlstand, Stammesrat, eine Erinnerung p = +1 mit Gewicht 0,5 nach dem Verblassen, **THEN** ist x = 0,32, und das Ursachenbuch hat Zufall + 0,002, Wohlstand + 0,003 und den Eintrag der Eingriffs-ID + 0,015 zusätzlich; **GIVEN** food = 0,8 · V genau, **THEN** gilt kein Wohlstand; **GIVEN** x = 0,99 und Δx = 0,05, **THEN** ist x = 1.
- **AC-03 Formel 3 [Logic]** **GIVEN** eine frische Erinnerung, **WHEN** 78 Kulturjahre laufen, **THEN** besteht sie noch mit Gewicht 0,965^78; im 79. ist sie vergessen; ihre Schübe ergeben zusammen 0,03 · Σ_{k=1}^{78} 0,965^k ± 1e-9; **GIVEN** 8 Erinnerungen und ein neuer Eingriff, **THEN** fällt die älteste heraus, und ihr Ursachenbuch-Eintrag bleibt.
- **AC-04 Regel 7 [Logic]** **GIVEN** x = 0,95 und der Auftrag „Eingriff erfassen (ID 77, p = +1, Δx = +0,15, ΔD = +0,1, Text)“, **THEN** ist x = 1, der Ursachenbuch-Eintrag 77 = +0,15, D um 0,1 höher (geklemmt), und es gibt eine Erinnerung mit Gewicht 1; der Auftrag für eine eingefrorene Kultur ändert nichts.
- **AC-05 Formel 4 [Logic]** **GIVEN** ein Ursachenbuch mit Krieg −0,05, Eingriff 77 −0,2 und Zufall +0,01, **WHEN** die Form zum Gottesstaat wechselt, **THEN** nennt das Ereignis die Ursache 77 mit dem Text der Erinnerung; ist die Erinnerung 77 vergessen, lautet der Text „in Erinnerung an einen alten Eingriff des Gottes“, die Ursache bleibt 77; **GIVEN** nur Einträge in Gegenrichtung, **THEN** ist die Ursache „Zufall“; **GIVEN** ein Eintrag mit |v| = 0,00105, **THEN** fällt er beim nächsten Verblassen weg (0,000987).
- **AC-06 Formel 5 [Logic]** **GIVEN** Stammesrat, **THEN** x = 0,5 bleibt Stammesrat, 0,5001 wird Lust-Sekte; Lust-Sekte mit x = 0,35 bleibt, 0,3499 wird Stammesrat; Lust-Sekte mit x = −0,9 wird Stammesrat, nicht Gottesstaat; spiegelbildlich für den Gottesstaat; jeder Wechsel erzeugt genau ein Ereignis „Kulturwandel“.
- **AC-07 Formel 6, Ziel [Logic]** **GIVEN** x = 0,8, 0 und −0,8, **THEN** sind die Ziele der sexuellen Normen 0,9, 0,5 und 0,1 und des Rituals 0,82, 0,5 und 0,82; **GIVEN** x = 1 und α = β = 0,6 in einer Test-Config, **THEN** ist das Ziel 1 (geklemmt).
- **AC-08 Formel 6, langsam [Logic]** **GIVEN** n = 0,6 und ein Eingriff, der x von 0,2 auf 0,35 hebt, **THEN** ist n nach dem nächsten Kulturjahr 0,62, nie mehr als 0,02 über dem Vorjahr; **GIVEN** 10.000 zufällige Folgen von Eingriffen mit beliebigem Δx, **THEN** ändert sich keine Norm zwischen zwei Kulturjahren um mehr als 0,02 + 1e-12, und innerhalb eines Jahres nie (Property-Test).
- **AC-09 Regel 15, Tabus [Logic]** **GIVEN** ein Stammesrat wird zum Gottesstaat, **THEN** gelten die Tabus des Gottesstaats ab diesem Kulturjahr; **GIVEN** ein Gottesstaat wird im Jahr Y zum Stammesrat, **THEN** gilt „Sex zu zweit auf dem Fest“ bis einschließlich Jahr Y + 4 als Tabu und ab Y + 5 nicht mehr; **GIVEN** er wird im Jahr Y + 2 wieder Gottesstaat, **THEN** gibt es keine Lücke.
- **AC-10 Regel 16, Abfrage [Logic]** **GIVEN** einen Gottesstaat mit n = 0,1, **THEN** liefert `Norm(t, Gruppe, privat)` (0,1, Tabu), `Norm(t, Zärtlichkeit, öffentlich)` (0,1, kein Tabu), `Norm(t, Ritual, Ritual)` die Ritualnorm ohne Tabu und `Norm(t, Sex zu zweit, Ritual)` (0, Tabu); die Abfrage ändert keinen Zustand.
- **AC-11 Regel 1, eingefroren [Logic]** **GIVEN** ein Stamm geht unter, **WHEN** 100 Jahre laufen, **THEN** sind Achse, Normen und Tabus seiner Kultur bitgleich, und `Norm` liefert sie weiter.
- **AC-12 Pflichten aus dem Zustimmungsmodell [Logic, Property]** **GIVEN** 100.000 Zustände, **WHEN** D_t, Hingabe der Einzelnen, Gotteskraft oder Rang beliebig geändert werden, **THEN** sind alle Normen und Tabus bitgleich; nur Achse und Gesellschaftsform bewegen sie (Zustimmungsmodell AC-27).
- **AC-13 Formel 7 [Logic]** **GIVEN** beim Erzeugen die Würfe 0,75, 0,25, 0,5, 0,5, **THEN** hat Stamm 0 x = 0,1, τ = −0,003, D = 0,4, Stammesrat, Normen 0,55 und Ritual 0,54, und Stamm 1 x = 0, τ = 0; **GIVEN** der Auftrag „Kultur anlegen (x = −0,1, D = 0,62)“ mit Wurf 1,0 − 1e-9 fürs Temperament, **THEN** ist τ fast 0,006, die Normen sind 0,45 ohne Nachwirkung, Ursachenbuch und Erinnerung sind leer.
- **AC-14 Aufträge von Stämme [Logic]** **GIVEN** „Achse verschieben (−0,001, Krieg)“, **THEN** sinkt x sofort um 0,001 und der Eintrag Krieg um 0,001; **GIVEN** „Achsen mischen (0,2, 80, 7, −0,6)“, **THEN** ist x_S = 13,2/94 ± 1e-9.
- **AC-15 Regel 19, Reihenfolge [Logic]** **GIVEN** 2 Stämme, **WHEN** ein Kulturjahr läuft, **THEN** verbraucht es genau 4 Würfe je Stamm (Hingabe, dann 3 für die Achse), Stamm 0 vor Stamm 1, die Hingabe rechnet mit der Achse vor ihrem Wandel, und die Normen mit der Achse danach.
- **AC-16 Speichern [Integration]** **GIVEN** ein Zustand mit 8 Erinnerungen, einem Ursachenbuch mit allen Gründen, einem nachwirkenden Tabu und einer eingefrorenen Kultur, **WHEN** nach Schritt 1.500 gespeichert und geladen wird, **THEN** gleichen die Hashes der Schritte 1.501 bis 3.000 denen eines durchgehenden Laufs.
- **AC-17 Kalibrierung [Integration, ADVISORY; läuft erst, wenn alle MVP-Systeme der Simulation und die Debug-Werkzeuge stehen]** **GIVEN** die Startwerte 0 bis 99, je 10 Ären ohne Eingriff, **THEN** liegt die Zahl der Kulturwandel pro Ära bei 0,39 ± 20 % wie in Prototyp 1; als Kennzahl wird gemeldet, welcher Anteil der Stammesjahre in welcher Form verbracht wird (Prototyp 1 vor der Abstimmung: alles Lust-Sekte; das darf nicht wiederkommen); **GIVEN** der programmierte Testspieler aus Prototyp 1 (etwa 4 Eingriffe), **THEN** führt die Chronik in mindestens 80 von 100 Welten mindestens 2 große Ereignisse auf ihn zurück (Prototyp 1: 89).
- **AC-18 Spielertest [Playtest, ADVISORY]** **GIVEN** ein Tester segnet einen Stamm wiederholt, bis er zur Lust-Sekte wird, **WHEN** er gefragt wird, warum, **THEN** nennt er in mindestens 4 von 5 Fällen seinen Segen, nur aus Chronik und Anzeige.

## Open Questions

| Frage | Wer klärt | Bis wann |
|---|---|---|
| **Normen und Tabus sind ein Vorschlag** (eine Kurve für alle sexuellen Handlungsarten, Ritual nach dem Betrag der Achse, Tabutabelle, 0,02 pro Jahr, 5 Jahre Nachwirkung). Sollen sich Handlungsarten unterscheiden (etwa Zärtlichkeit weniger von der Kultur abhängig)? | Entwickler | vor dem GDD Begehren & Lust |
| Ist Harem im Gottesstaat erlaubt (heute ja, kein Tabu)? | Entwickler | vor dem GDD Harem & Rituale |
| Kulturwandel überwiegt nicht: Krieg 4 zu 1 (Stämme & Diplomatie, Open Questions). Kann die Kultur mehr Ereignisse beitragen (etwa „Hingabe kippt“, „neue Sitte“)? | Entwickler | nach der ersten Messung |
| Welche Werte (p, Δx, ΔD) haben die Hand-Aktionen der Gotteshand (Packen, Werfen, Ablegen, Verletzen)? Prototyp 2: Werfen allein trägt nur 5–10 Minuten | GDD Gotteshand & Ragdoll | mit dem GDD |
| Soll ein Blitz auf ein Mitglied die Offenheit seines Stamms zusätzlich senken, damit Auslese durch Töten nicht wirkt (Zustimmungsmodell)? Heute senkt der Blitz die Achse (Δx = −0,12) und damit langsam die Normen; die Eigenschaften der Überlebenden ändern sich nicht | Entwickler, mit Zustimmungsmodell | nach der ersten Messung |
| Sollen sich Kulturen auch durch Lust selbst verändern (Feste, Rituale, Harem verschieben die Achse; Pillar 3 „Kulturwandel“ als Wirkung von Lust)? Prototyp 1 hatte das nicht | GDD Begehren & Lust, Harem & Rituale | mit den GDDs |
| Weitere Gesellschaftsformen und Achsen (Sklavenstaat, Alpha) | GDD Weitere Gesellschaftsformen | Alpha |
