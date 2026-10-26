# Kultur & Erinnerung

> **Status**: Approved (vom Entwickler freigegeben am 2026-10-26 nach 2 Review-Runden)
> **Author**: Claude, mit dem Entwickler
> **Last Updated**: 2026-10-26
> **Last Verified**: 2026-10-26
> **Implements Pillar**: Pillar 2 (Jeder Eingriff hinterlässt eine Spur), Pillar 3 (Lust ist Kultur, nicht Beiwerk), Pillar 1 (Die Welt lebt ohne dich)

## Summary

Kultur & Erinnerung führt für jeden Stamm seine Kultur: eine Achse von Gottesstaat (−1)
bis Lust-Sekte (+1), daraus die Gesellschaftsform, die Stammeshingabe D_t, die sexuellen
Normen und Tabus für das Zustimmungsmodell und eine begrenzte Erinnerung an die Eingriffe
des Gottes, die verblasst. Ein Ursachenbuch hält fest, was die Achse bewegt hat, auch über
Umwege (ein Krieg, den der Spieler ausgelöst hat), damit die Chronik jeden Kulturwandel
erklären kann („weil du …“). Achse, Hingabe, Erinnerung und Gesellschaftsform sind 1:1 aus
Prototyp 1; Normen, Tabus, Veranlasser und der Schutz gegen gehäufte Handaktionen sind neu.

> **Quick reference** — Layer: `Gameplay` · Priority: `MVP` · Key deps: `Simulationskern, Welt & Insel, Stämme & Diplomatie`

## Overview

Jede Kultur hat eine Position auf einer Achse: Strafen, Angst, Hunger und Dürre ziehen sie
zum Strengen (Gottesstaat), Wohlstand, Frieden und Feste zum Freien (Lust-Sekte), und ein
eigenes Temperament gibt jedem Stamm eine Neigung. Überschreitet die Achse eine Grenze,
wechselt die Gesellschaftsform; der Wechsel ist ein großes Ereignis mit Ursache. Jeder
Eingriff des Gottes (Blitz, Fest-Segen, Handaktionen der Gotteshand) wird als
**Erinnerung** gespeichert und schiebt die Kultur noch Jahrzehnte weiter, während er
langsam verblasst. Das **Ursachenbuch** sammelt, was die Achse bewegt hat, und merkt sich
bei Krieg, Hunger und Dürre, welcher Eingriff sie veranlasst hat; aus ihm nennt die Chronik
den Grund eines Wandels. Aus der Achse folgen die **Normen**, also wie offen die Kultur für
Zärtlichkeit, Sex, Gruppen, Harem und Rituale ist. Sie folgen der Achse nur langsam, und
**Tabus** entstehen und fallen nur über diese langsamen Normen, damit kein Eingriff
Zustimmung oder Ablehnung im Moment erzwingt. Die **Stammeshingabe** steigt, je
ausgeprägter eine Kultur ist, und sinkt im Hunger; Menschen & Bevölkerung rechnet mit ihr
die Gnade.

## Player Fantasy

Der Spieler formt Kulturen mit der Zeit und sieht es: Ein Stamm, den er immer wieder segnet,
wird über Jahrzehnte freizügig, und die Chronik sagt „Kesh wird zur Lust-Sekte, weil du in
Ära III ihr Fest gesegnet hast“. Ein Stamm, dessen Rivalen er in einen Krieg getrieben hat,
wird streng, und die Chronik sagt „nach dem Krieg, den du ausgelöst hast“. Er weiß, dass
ihm die Kultur nicht sofort gehorcht, sondern sich erinnert: Was er heute tut, wirkt noch
Jahrzehnte nach.

## Detailed Design

### Core Rules

*Achse, Hingabe, Erinnerung, Ursachenbuch und Gesellschaftsform 1:1 aus Prototyp 1
(`prototype.html`, `govFor`, `addLedger`, `remember`, `causeOf`, Kultur-Teil von `step`,
`applyPower`); neu sind Normen und Tabus (Regeln 13–17), Veranlasser (Regel 5), kleine
Eingriffe (Regel 8) und der Grund Lust (Regel 6). Abweichungen stehen unter „Kalibrierung
gegen Prototyp 1“.*

**Die Kultur**

1. Jeder Stamm hat genau eine Kultur mit: Achse x_t (−1 bis 1), Temperament τ_t,
   Stammeshingabe D_t (0,05 bis 1), Gesellschaftsform, Ursachenbuch, Erinnerung
   (höchstens 8 Einträge), Normen (Regel 13) und Tabus (Regel 15). Die Kultur hat die
   Stammes-ID ihres Stamms; die IDs der Startstämme vergibt Welt & Insel, bevor Stämme &
   Diplomatie erzeugt wird (Regel 19).
2. Geht ein Stamm unter, wird seine Kultur **eingefroren**: Achse, Gesellschaftsform, D_t,
   Normen und Tabus bleiben unverändert lesbar, Ursachenbuch und Erinnerung werden
   gelöscht, und kein Schritt und kein Auftrag ändert sie mehr. Das Zustimmungsmodell
   liest ihre Normen noch für Menschen, die den Stamm vor Kurzem verlassen haben
   (Zustimmungsmodell Regel 4).
3. Die Achse wird nach jeder Änderung auf −1 bis 1 geklemmt.

**Jährlicher Kulturwandel**

4. Einmal pro Geschichtsjahr bewegt sich die Achse jedes lebenden Stamms um die Summe
   dieser Teile (Formel 2): Zufall und Temperament; Wohlstand (+, wenn kein Krieg, kein
   Hunger und volle Speicher); Hunger (−); Dürre (−); der Sog der eigenen
   Gesellschaftsform (Lust-Sekte +, Gottesstaat −); und jede Erinnerung schiebt in ihre
   Richtung, gewichtet mit ihrem verbleibenden Gewicht.
5. Jeder Teil wird im **Ursachenbuch** unter seinem Grund verbucht: Zufall, Wohlstand,
   Hunger, Dürre, Sog, Krieg, Eroberung, Lust, und je Eingriff unter seiner Eingriffs-ID
   (mit dem Text der Erinnerung und ihrem Jahr). Die Einträge Krieg, Hunger, Dürre,
   Eroberung und Lust führen zusätzlich **Anteile je Veranlasser**: Kommt eine Buchung mit
   einer Eingriffs-ID als Veranlasser (ein Krieg, dessen Ursache ein Eingriff war; ein
   Hunger, dessen Hauptgrund ein Brand durch einen Blitz war), wird sie auch unter diesem
   Anteil verbucht. Das Ursachenbuch verblasst jährlich um den Faktor 0,94, Anteile mit
   (Formel 4). Veranlasser ändern nie, welcher Grund als Ursache gewinnt, nur den Text.
6. Andere Systeme verschieben die Achse zusätzlich sofort über den Auftrag **„Achse
   verschieben (Wert, Grund, Veranlasser)“** mit dem Grund **Krieg** (Stämme & Diplomatie,
   Regel 9, Veranlasser = Ursache des Kriegs, falls es eine gibt) oder **Lust** (Begehren &
   Lust, Harem & Rituale). Für Lust gilt eine Obergrenze von |Summe| ≤ 0,01 pro Stamm und
   Geschichtsjahr; bis die GDDs der Lust-Systeme ihre Werte nach der Kalibrierung setzen,
   ist ihr Gewicht 0 (Pillar 3: Lust verändert die Kultur, die Schnittstelle steht).

**Erinnerung an Eingriffe**

7. Der Auftrag **„Eingriff erfassen“** ist der einzige Weg, auf dem ein Eingriff des Gottes
   eine Kultur direkt verändert. Er trägt: Stamm, Eingriffs-ID, Art, Richtung p (−1 bis 1),
   Sofortwirkung Δx auf die Achse, Wirkung ΔD auf die Hingabe, das Merkmal **klein** (ja bei
   Handaktionen der Gotteshand, nein bei Götterkräften) und den Text für die Chronik. Die
   Werte legen die GDDs der Eingriffe fest; Prototyp 1: Blitz p = −1, Δx = −0,12, ΔD = +0,15;
   Fest-Segen p = +1, Δx = +0,15, ΔD = +0,1. Beim Erfassen gilt sofort: Achse + Δx, Hingabe
   + ΔD (beide geklemmt), Ursachenbuch-Eintrag der Eingriffs-ID **=** Δx, und eine neue
   Erinnerung mit Gewicht 1.
8. **Kleine Eingriffe** derselben Art am selben Stamm innerhalb von T_merge = 10
   Geschichtsjahren seit der letzten werden **zusammengefasst**, solange deren Erinnerung
   noch besteht (sonst entsteht eine neue): keine neue Erinnerung, sondern die vorhandene
   bekommt wieder Gewicht 1, ihr Zähler steigt, und die Sofortwirkung
   halbiert sich mit jeder Wiederholung (Formel 3); sie wird zum Ursachenbuch-Eintrag der
   ersten Eingriffs-ID **addiert**. So kann ein gehäuftes Werfen weder die Erinnerung fluten
   noch die Achse beliebig schnell schieben (die endgültigen Werte legt das GDD Gotteshand &
   Ragdoll fest).
9. Jedes Jahr verblasst jede Erinnerung um den Faktor 0,965 und schiebt danach die Achse um
   `p · Gewicht · 0,03` (Formel 3). Fällt das Gewicht auf 0,06 oder darunter, wird sie
   vergessen (nach 79 Jahren, bei 1× etwa 40 s). Sind schon 8 Erinnerungen da, fällt beim
   Erfassen die **schwächste** heraus (kleinstes Gewicht · |p|, bei Gleichstand die älteste);
   ihr Ursachenbuch-Eintrag bleibt und verblasst.
10. Ein einzelner Fest-Segen schiebt die Achse so insgesamt um etwa +0,93 (0,15 sofort und
    0,78 über die Jahre), wenn nichts dagegenwirkt (Prototyp 1; Open Questions).

**Gesellschaftsform**

11. Es gibt drei Gesellschaftsformen: **Stammesrat**, **Lust-Sekte** und **Gottesstaat**. Ein
    Stammesrat wird zur Lust-Sekte, wenn die Achse über 0,5 steigt, und zum Gottesstaat,
    wenn sie unter −0,5 fällt. Eine Lust-Sekte wird wieder Stammesrat, wenn die Achse unter
    0,35 fällt, ein Gottesstaat, wenn sie über −0,35 steigt (Formel 5). Von der Lust-Sekte
    zum Gottesstaat geht es nur über den Stammesrat.
12. Ein Wechsel ist das große Ereignis **„Kulturwandel“**. Es nennt die neue Form, die
    **Ursache** (den Eintrag im Ursachenbuch, der am stärksten in Richtung des Wechsels
    zeigt, Formel 4) und die drei größten Einträge. Ist die Ursache ein Eingriff, trägt das
    Ereignis dessen Eingriffs-ID, und die Chronik schreibt „weil du …“ mit dem gespeicherten
    Text, auch wenn die Erinnerung schon vergessen ist. Ist sie Krieg, Hunger, Dürre,
    Eroberung oder Lust und stammt mindestens die Hälfte davon von einem Veranlasser, trägt
    das Ereignis dessen Eingriffs-ID als Veranlasser, und die Chronik nennt ihn („nach dem
    Krieg, den du ausgelöst hast, als du …“). Den Text holt die Chronik über die
    Eingriffs-ID (Simulationskern Regel 15), denn ein Veranlasser muss diesen Stamm nie
    selbst getroffen haben. Veranlasser sind immer Eingriffs-IDs: Stämme liefert die
    Ursache des Kriegs, Welt & Insel die Eingriffs-ID des Brands (Welt Regel 16).

**Stammeshingabe**

13. Die Stammeshingabe D_t wandert jährlich: Sie steigt, je weiter die Achse von der Mitte
    entfernt ist, sinkt im Hunger und streut leicht (Formel 1). Eingriffe ändern sie über ΔD
    (Regel 7). Menschen & Bevölkerung liest D_t für die Gnade und die persönliche Hingabe
    (Menschen Regeln 16, 17), Gotteskraft & Götterkräfte für den Zufluss aus Ritualen.
    **D_t ist keine Eingabe des Zustimmungsmodells** und keiner Norm (Zustimmungsmodell
    Regel 5).

**Normen und Tabus für das Zustimmungsmodell** (neu, nicht im Prototyp)

14. Für jede Handlungsart (Zärtlichkeit, Sex zu zweit, Gruppe, Harem beitreten, Ritual) hat
    eine Kultur eine **Offenheit** n zwischen 0 und 1. Sie hängt nur von der Achse ab, nicht
    vom Ort; wie öffentlich der Ort ist, rechnet das Zustimmungsmodell selbst (π[Ort],
    Zustimmungsmodell Formel 1). Ziel ist `n_Ziel[a](x)` (Formel 6): Alle sexuellen
    Handlungsarten haben bei x = 0 die Offenheit 0,5 (darauf ist das Zustimmungsmodell
    kalibriert), hängen aber verschieden stark von der Kultur ab: Zärtlichkeit wenig, Sex zu
    zweit und Harem mittel, Gruppe stark. Das Ritual steigt mit dem Betrag der Achse (beide
    ausgeprägten Kulturen feiern Rituale).
15. Die Offenheit folgt ihrem Ziel **langsam**: höchstens um 0,02 pro Geschichtsjahr
    (Formel 6). Das bindet vor allem nach einem Eingriff, der die Achse springen lässt; der
    gewöhnliche Kulturwandel bewegt das Ziel ohnehin höchstens um etwa 0,02 pro Jahr.
16. **Tabus entstehen und fallen nur über die langsamen Normen.** Die Gesellschaftsform legt
    fest, welche Kombinationen aus Handlungsart und Ort überhaupt tabu werden können
    (**Kandidaten**, Tabelle in Formel 6). Ein Tabu entsteht in einem Kulturjahr, wenn die
    Kombination schon im Vorjahr Kandidat war und die Offenheit dieser Handlungsart unter
    0,25 liegt. Es fällt erst, wenn die Kombination seit mindestens T_tabu = 5
    Geschichtsjahren kein Kandidat mehr ist **und** die Offenheit mindestens 0,3 erreicht
    hat. Solange sie Kandidat ist, bleibt ein bestehendes Tabu. So setzt kein Eingriff ein
    Tabu oder hebt es auf, auch wenn er die Gesellschaftsform kippt; nach einem Gottesstaat
    dauert es etwa 10 Jahre, bis ein Tabu fällt.
17. Das Zustimmungsmodell liest die Normen über eine reine Abfrage
    `Norm(Stamm, Handlungsart, Ort) → (gültig, n, Tabu)`. Ungültig sind eine unbekannte
    Stammes-ID, der Ort Ritual mit einer anderen Handlungsart als Ritual und die
    Handlungsart Ritual an einem anderen Ort als Ritual; das Zustimmungsmodell behandelt
    „ungültig“ als ungültige Eingabe (Zustimmungsmodell Regel 3). Ein **Kontakt im Ritual**
    gilt als Handlung am Ort **Fest** (Zustimmungsmodell Regel 7); im Gottesstaat sind
    Rituale darum fromm, nicht sexuell.

**Neue Kultur, Mischung**

18. Beim Erzeugen der Welt legt dieses System die Kulturen der Startstämme 0 und 1 an. Bei
    einer Abspaltung legt es auf Auftrag die Kultur des neuen Stamms an (Stämme Regel 16),
    bei einer Eroberung mischt es auf Auftrag die Achse des Siegers (Stämme Formel 4) und
    verbucht die Änderung unter „Eroberung“. Eine neue Kultur bekommt (Formel 7): Achse
    (Start gewürfelt, Abspaltung vorgegeben und geklemmt), Temperament (gewürfelt),
    Hingabe (Start 0,4, Abspaltung die übergebene, geklemmt), die Gesellschaftsform nach
    Formel 5 ausgehend vom Stammesrat (auch Lust-Sekte oder Gottesstaat, ohne Ereignis),
    Normen gleich ihrem Ziel, keine Tabus und keine Kandidaten aus dem Vorjahr (Tabus
    entstehen frühestens im nächsten Kulturjahr), ein leeres Ursachenbuch und keine
    Erinnerung.

**Reihenfolge im Schritt**

19. Beim Erzeugen kommt dieses System nach Menschen & Bevölkerung und vor Stämme &
    Diplomatie (Simulationskern Regel 17); Menschen braucht D_t beim Erzeugen nicht, weil
    die persönliche Hingabe beim Lesen berechnet wird (Menschen Formel 8). Im Lauf ist es
    ein Jahressystem mit Versatz O_year = 1 (Simulationskern Formel 5). Für jeden lebenden
    Stamm in aufsteigender ID: (1) Ursachenbuch verblasst, Einträge unter 0,001 fallen weg;
    (2) Erinnerungen verblassen, vergessene fallen weg; (3) Hingabe (1 Wurf); (4) Achse
    (3 Würfe); (5) Gesellschaftsform und Ereignis; (6) Normen folgen; (7) Tabus. Gelesen
    werden Hunger, Hauptgrund und Dürre von Welt & Insel (Versatz 0 desselben Jahres), V_t
    so, wie es in diesem Schritt nach dem Lauf von Menschen steht (die Gnade des Jahres
    kommt erst bei Versatz 2), und der Krieg von Stämme & Diplomatie aus dem Vorjahr.
    Aufträge („Eingriff erfassen“, „Achse verschieben“, „Kultur anlegen“, „Achsen
    mischen“) wirken sofort, wann immer sie kommen; zwei im selben Schritt in der
    Reihenfolge ihres Eintreffens, Eingriffe nach aufsteigender Eingriffs-ID.

### States and Transitions

| Gesellschaftsform | Bedeutung | Tabu-Kandidaten | Wechsel |
|---|---|---|---|
| **Stammesrat** | Mitte | Gruppe öffentlich | → Lust-Sekte bei x > 0,5 · → Gottesstaat bei x < −0,5 |
| **Lust-Sekte** | frei; Sog +0,003 pro Jahr | keine | → Stammesrat bei x < 0,35 |
| **Gottesstaat** | streng; Sog −0,003 pro Jahr | Gruppe überall, Sex zu zweit auf dem Fest und öffentlich | → Stammesrat bei x > −0,35 |

| Zustand einer Tabu-Kombination | Bedeutung | Wechsel |
|---|---|---|
| **Frei** | kein Tabu | → Tabu, wenn sie im Vorjahr Kandidat war und n < 0,25 |
| **Tabu** | sperrt im Zustimmungsmodell | → frei, wenn sie seit ≥ 5 Jahren kein Kandidat ist und n ≥ 0,3 |

| Zustand einer Erinnerung | Bedeutung | Wechsel |
|---|---|---|
| **Frisch** | Gewicht 1 beim Erfassen oder Zusammenfassen | → verblassend |
| **Verblassend** | Gewicht 0,06 bis 1, schiebt jährlich | → vergessen bei Gewicht ≤ 0,06 oder als schwächste bei einer neunten |
| **Vergessen** | entfernt; ihr Ursachenbuch-Eintrag mit Text verblasst weiter | endgültig |

### Interactions with Other Systems

| System | Was hineingeht | Was herauskommt | Wer besitzt die Schnittstelle |
|---|---|---|---|
| Simulationskern | Jahrestakt O_year = 1, Zufallsstrom system_id 3, Erzeugen nach Menschen, Ursache-Kette der Ereignisse | Ereignisse „Kulturwandel“ (groß), „Tabu entsteht“, „Tabu fällt“, „Norm kippt“ (klein) | Kern; „Eingriff erfassen“ besitzt dieses System |
| Welt & Insel | hungert in diesem Jahr, Hauptgrund mit Ursache (bei Brand die Eingriffs-ID), Dürre aktiv, food_t | – | Welt & Insel |
| Menschen & Bevölkerung | V_t | D_t (lesend) | dieses System |
| Stämme & Diplomatie | im Krieg (Vorjahr); Aufträge „Kultur anlegen (x, D)“, „Achse verschieben (Wert, Krieg, Veranlasser)“, „Achsen mischen (x_S, V_S, j, x_L)“ | x_t, Gesellschaftsform (lesend) | dieses System |
| Zustimmungsmodell | – | `Norm(Stamm, Handlungsart, Ort) → (gültig, n, Tabu)`, auch für eingefrorene Kulturen | dieses System; die Pflichten aus Zustimmungsmodell AC-27 übernimmt AC-14 hier |
| Gotteskraft & Götterkräfte, Gotteshand & Ragdoll | Auftrag „Eingriff erfassen“ (p, Δx, ΔD, klein, Text) | D_t (Zufluss aus Ritualen), x_t (wer einen Eingriff als Frevel sieht) | dieses System (Schnittstelle), die Eingriffe (Werte) |
| Begehren & Lust, Harem & Rituale | Auftrag „Achse verschieben (Wert, Lust, Veranlasser)“, Gewicht vorerst 0 | Normen, Tabus, Gesellschaftsform (lesend) | dieses System |
| Chronik | – | Ereignisse mit Ursache, Veranlasser und Texten | Kern-Ereignisformat |
| Weltansicht & Kamera, HUD | – | Achse, Form, Hingabe, Erinnerungen mit Gewicht, die größten Ursachen, Normen als Wort, Tabus | dieses System |
| Speichern & Laden | kompletter Zustand | je lebender Kultur alle Felder aus Regel 1, Ursachenbuch mit Anteilen in fester Reihenfolge, Erinnerungen (mit Zähler und Jahr der letzten Wiederholung) in Reihenfolge des Erfassens, je Tabu-Kombination Zustand, Kandidat im Vorjahr und Jahr des Kandidatur-Endes; je eingefrorener Kultur Achse, Form, D_t, Normen, Tabus | Speichern & Laden |

## Formulas

Übernahme aus `prototypes/weltengott-concept/prototype.html` (Formeln 1–5 und 7), Formel 6
und die neuen Teile von Formel 3 und 4 sind neu. Alle Zufallszahlen kommen aus dem Strom
system_id 3 (`NextDouble()` = u, Simulationskern Formel 6), in der Reihenfolge von Regel 19.
Toleranz in Tests 1e-9. Alle Faktoren und Grenzen sind Werte der Datendatei.

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
langsam Hingabe (−0,0008 pro Jahr bei x = 0). Eine Gegenkraft nach oben fehlt (wie im
Prototyp; Open Questions).
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
| food_t, V_t | double, int | ≥ 0 | calculated | Welt & Insel, Menschen (Regel 19) |
| Form | Aufzählung | – | calculated | Gesellschaftsform vor dem Wechsel dieses Jahres |
| Konstanten | double | 0,014; 0,003; 0,03; 0,8 | data file | Prototyp 1 |

**Output Range:** Ohne Erinnerung und Hunger bewegt sich die Achse pro Jahr um höchstens
±0,033 (Zufall 0,021, Temperament 0,006, Wohlstand oder Dürre 0,003, Sog 0,003); Hunger
zieht sie um 0,03 zum Strengen. Jede Erinnerung schiebt um bis zu 0,029; acht frische in
dieselbe Richtung schieben bis zu 0,23 pro Jahr (wie im Prototyp gewollt; eine Obergrenze ist
offen). Jeder Teil wird im Ursachenbuch verbucht: z + τ_t unter „Zufall“, die anderen unter
ihrem Grund, jede Erinnerung unter ihrer Eingriffs-ID; ein Teil mit dem Wert 0 wird nicht
verbucht.
**Example:** x = 0,3, Würfe 0,5 / 0,5 / 0,5 (z = 0), τ = 0,002, Wohlstand, keine Dürre,
Stammesrat, eine Erinnerung p = +1 mit w = 0,5 → Δx = 0,002 + 0,003 + 0,015 = 0,02; x = 0,32.

### 3. Erinnerung

The Erinnerung formula is defined as:

jährlich je Erinnerung: `w ← 0,965 · w`; vergessen, wenn `w ≤ 0,06`; sonst Schub `0,03 · p · w` (Formel 2) · beim Erfassen: `w = 1`, Zähler k = 1 · kleiner Eingriff derselben Art innerhalb von T_merge Jahren seit der letzten Wiederholung: `w = 1`, `k ← k + 1`, Sofortwirkung `Δx_eff = Δx · 0,5^(k−1)`, `ΔD_eff = ΔD · 0,5^(k−1)` · bei 8 Erinnerungen fällt die mit dem kleinsten `w · |p|` heraus (Gleichstand: die älteste)

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| w | double | 0,06–1 | calculated | Gewicht |
| k | int | ≥ 1 | calculated | Zahl der zusammengefassten Wiederholungen |
| Verblassen, Vergessen ab | double | 0,965; 0,06 | data file | Prototyp 1 |
| Höchstzahl | int | 8 | data file | Prototyp 1 |
| T_merge, Halbierung | int, double | 10 Jahre; 0,5 | data file | Vorschlag, endgültig im GDD Gotteshand & Ragdoll |

**Output Range:** Eine Erinnerung schiebt 78 Jahre lang (0,965^78 = 0,0621, 0,965^79 = 0,0599)
und insgesamt um `0,03 · p · Σ_{k=1}^{78} 0,965^k ≈ 0,7758 · p`. Nach 20 Jahren hat sie noch das
Gewicht 0,49. Beliebig viele kleine Eingriffe derselben Art schieben die Achse sofort höchstens
um 2 · Δx und belegen nur eine Erinnerung.
**Example:** Ein Fest-Segen (p = +1, Δx = +0,15) in einem Stamm mit x = −0,4: sofort −0,25,
nach dem ersten Jahr etwa −0,22; nach 78 Jahren wäre er bei etwa +0,53, wenn nichts anderes
wirkt. Drei Würfe (klein, Δx = −0,02) in 4 Jahren: −0,02, −0,01, −0,005, eine Erinnerung mit
k = 3.

### 4. Ursachenbuch und Ursache

The Ursache formula is defined as:

jährlich je Eintrag und Anteil `v ← 0,94 · v`, Eintrag fällt weg bei `|v| < 0,001` (mit seinen Anteilen); danach Buchungen dieses Jahres · Buchung mit Veranlasser e: Eintrag `+= b`, Anteil[e] `+= b` · `Ursache(r) = argmax_Eintrag (r · v)` über Einträge mit `r · v > 0` (Gleichstand: Zufall, Wohlstand, Hunger, Dürre, Sog, Krieg, Eroberung, Lust, dann Eingriffs-IDs aufsteigend); keiner → „Zufall“ · Veranlasser im Text, falls `r · Anteil[e] ≥ 0,5 · r · v` für den größten Anteil e

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| v | double | – | calculated | Summe der Buchungen eines Grunds, verblassend |
| Anteil[e] | double | – | calculated | Teil von v, den Eingriff e veranlasst hat |
| r | int | ±1 | calculated | +1 bei einem Wechsel zur Lust-Sekte oder vom Gottesstaat zum Stammesrat, −1 sonst |
| Verblassen, Wegfall | double | 0,94; 0,001 | data file | Prototyp 1 |

**Output Range:** Ein Grund mit konstanter Buchung b strebt gegen `b / 0,06`, Wohlstand also
gegen 0,05, Hunger gegen −0,5. Der Eintrag eines Blitzes erreicht nach etwa 17 Jahren höchstens
−0,27 und liegt nach 50 Jahren bei etwa −0,15; danach ist er kaum noch stärker als der
Wohlstand, eine Zuordnung zu Eingriffen ist also etwa 50 Jahre lang verlässlich. Anteile
ändern die Wahl der Ursache nie.
**Example:** Wechsel zum Gottesstaat (r = −1), Ursachenbuch: Krieg −0,05, Eingriff 77 −0,2,
Zufall +0,01 → Ursache Eingriff 77. Krieg −0,3 mit Anteil[77] = −0,2, Eingriff 77 −0,1 → Ursache
Krieg, Veranlasser 77 (0,2 ≥ 0,15): „nach dem Krieg, den du ausgelöst hast, als du …“.

**Textvorlagen** (Datendatei; der Text einer Erinnerung ist ein Satzteil ohne „du“, etwa
„in Ära III das Fest von Kesh gesegnet hast“):

| Grund | Text |
|---|---|
| Wohlstand | „nach Jahren voller Vorräte und Frieden“ |
| Hunger | „nach Hunger und Not“ (mit Hauptgrund: „nach dem Hunger in der Dürre“ usw.) |
| Krieg | „nach den Verlusten im Krieg“ |
| Eroberung | „nach der Eroberung von {Stamm}“ |
| Lust | „nach Jahren voller Feste und Lust“ |
| Dürre | „unter der langen Dürre“ |
| Zufall | „aus innerer Unruhe“ |
| Sog | „weil der alte Glaube sich festigte“ |
| Eingriff | „weil du {Text}“, bei k > 1 „weil du immer wieder {Text}“ |
| mit Veranlasser | Text des Grunds, ergänzt um „, den du ausgelöst hast, als du {Text}“ |

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

### 6. Normen und Tabus

The Normen formula is defined as:

`n_Ziel[a](x) = clamp(0,5 + β[a] · x, 0, 1)` für die sexuellen Handlungsarten · `n_Ziel[Ritual](x) = clamp(0,5 + β_R · |x|, 0, 1)` · jährlich `n[a] ← n[a] + clamp(n_Ziel[a](x) − n[a], −0,02, +0,02)` · Tabu (a, Ort) entsteht, wenn (a, Ort) im Vorjahr Kandidat war und `n[a] < 0,25`; fällt, wenn (a, Ort) seit mindestens 5 Jahren kein Kandidat ist und `n[a] ≥ 0,3`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| β[a] | double | 0–0,6 | data file | Zärtlichkeit 0,3; Sex zu zweit 0,5; Gruppe 0,6; Harem beitreten 0,5 |
| β_R | double | 0–0,5 | data file | Ritual 0,4 |
| Δn_max | double | 0,02 / Jahr | data file | höchste Änderung einer Norm pro Geschichtsjahr |
| θ_an, θ_aus | double | 0,25; 0,3 | data file | Offenheit, unter der ein Tabu entsteht, und ab der es fallen kann |
| T_tabu | int | 5 Geschichtsjahre | data file | Mindestzeit ohne Kandidatur, bevor ein Tabu fällt |

**Kandidaten (Default):**

| Gesellschaftsform | privat | Fest | öffentlich |
|---|---|---|---|
| Lust-Sekte | – | – | – |
| Stammesrat | – | – | Gruppe |
| Gottesstaat | Gruppe | Gruppe, Sex zu zweit | Gruppe, Sex zu zweit |

Zärtlichkeit, Harem beitreten und Ritual sind nie Kandidat. Ein Kontakt im Ritual gilt als am
Ort Fest (Regel 17).

**Output Range:** n zwischen 0 und 1. Ziele bei x = 0,8 / 0 / −0,8:

| Handlungsart | Lust-Sekte (0,8) | Stammesrat (0) | Gottesstaat (−0,8) |
|---|---|---|---|
| Zärtlichkeit | 0,74 | 0,5 | 0,26 |
| Sex zu zweit, Harem beitreten | 0,9 | 0,5 | 0,1 |
| Gruppe | 0,98 | 0,5 | 0,02 |
| Ritual | 0,82 | 0,5 | 0,82 |

Das deckt die Spanne, mit der das Zustimmungsmodell rechnet (dort 0,15, 0,5 und 0,9). Gruppe
wird in einem Stammesrat erst bei x < −0,417 öffentlich tabu (streng geneigter Rat). Nach dem
Wechsel zum Gottesstaat entsteht das Tabu auf Gruppe meist nach 1 bis 3 Jahren, auf Sex zu
zweit, wenn die Achse unter −0,5 weiter fällt. Nach dem Ende eines Gottesstaats fällt das
Tabu auf Sex zu zweit nach etwa 10 Jahren (die Norm steigt von etwa 0,1 mit höchstens 0,02 pro
Jahr auf 0,3).
**Example:** Ein Stamm mit x = 0,2 (Sex: n = 0,6) bekommt einen Fest-Segen, x springt auf 0,35:
n_Ziel = 0,675, n wird im nächsten Kulturjahr 0,62, im übernächsten 0,64.

### 7. Neue Kultur und Mischung

The Neue-Kultur formula is defined as:

Start: `x = (u − 0,5) · 0,4`, dann `τ = (u − 0,5) · 0,012`, `D = 0,4` · Abspaltung: `x = clamp(x_neu, −1, 1)`, `τ = (u − 0,5) · 0,012`, `D = clamp(D_übergeben, 0,05, 1)` · beide: `Form = Form(x, Stammesrat)`, `n[a] = n_Ziel[a](x)`, keine Tabus, keine Kandidaten im Vorjahr, Ursachenbuch leer, keine Erinnerung · Mischung bei Eroberung: `x_S ← (x_S · (V_S + j) + x_L · j) / (V_S + 2j)`, falls `V_S + 2j > 0`, sonst unverändert; die Änderung wird unter „Eroberung“ verbucht

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| x | double | Start −0,2 bis 0,2 | calculated | Startachse |
| τ | double | −0,006 bis 0,006 | calculated | Temperament |
| D | double | 0,05–1 | calculated | Start 0,4 (Prototyp 1) |

**Output Range:** Startstämme beginnen als Stammesrat mit den Normen Zärtlichkeit 0,44–0,56,
Sex zu zweit und Harem 0,4–0,6, Gruppe 0,38–0,62 und Ritual 0,5–0,58. Würfe beim Erzeugen: Stamm 0 (x, τ), dann Stamm 1 (x, τ).
**Example:** u = 0,75 und 0,25 → x = 0,1, τ = −0,003, Zärtlichkeit 0,53, Sex zu zweit 0,55,
Gruppe 0,56, Ritual 0,54.

### Kalibrierung gegen Prototyp 1

Achse, Hingabe, Erinnerung, Ursachenbuch und Gesellschaftsform sind die von Prototyp 1 und
gelten als „fest bis zur ersten Kalibrierungsmessung“ (AC-21). Abweichungen:
1. Der Zufall kommt aus dem eigenen Strom.
2. Im Prototyp rechnete die Gnade vor dem Kulturwandel desselben Stamms; hier rechnet Kultur
   vor der Gnade (Simulationskern Formel 5), also prüft der Wohlstand das Volk vor der Gnade
   dieses Jahres. Der Unterschied liegt bei wenigen Köpfen.
3. Gleichstände im Ursachenbuch folgen einer festen Reihenfolge statt der
   Einfügereihenfolge.
4. Bei 8 Erinnerungen fällt die schwächste statt der ältesten heraus; mit den etwa 4
   Eingriffen des Testspielers aus Prototyp 1 kommt das nicht vor.
5. Ohne Wirkung auf die Achse: Normen und Tabus, eingefrorene Kulturen, Veranlasser,
   Buchungen unter „Eroberung“, der Grund „Lust“ mit Gewicht 0, die Texte. Kleine Eingriffe
   gab es im Prototyp nicht.

## Edge Cases

- **If ein Eingriff die Achse oder die Hingabe über die Grenzen treiben würde**: Sie werden
  geklemmt; der Ursachenbuch-Eintrag ist trotzdem das volle Δx (wie im Prototyp).
- **If „Eingriff erfassen“ ungültige Werte trägt** (p außerhalb −1 bis 1, nicht endliche Werte,
  unbekannter oder untergegangener Stamm): Der Auftrag wird ignoriert. Δx = 0 ist gültig: Es
  entsteht eine Erinnerung, aber kein Ursachenbuch-Eintrag. p und Δx dürfen verschiedene
  Vorzeichen haben (ein Eingriff, der sofort streng macht, aber später frei).
- **If eine Eingriffs-ID schon im Ursachenbuch steht** (zusammengefasster kleiner Eingriff):
  Die Sofortwirkung wird addiert; sonst gilt die Zuweisung aus Regel 7.
- **If eine neunte Erinnerung kommt**: Die schwächste fällt heraus, ihr Ursachenbuch-Eintrag
  bleibt mit Text und Jahr.
- **If die Achse genau auf einer Grenze liegt** (x = 0,5 im Stammesrat, x = 0,35 in der
  Lust-Sekte): kein Wechsel (die Vergleiche sind streng).
- **If im selben Jahr Hunger und Wohlstand zuträfen**: Unmöglich, Wohlstand setzt „nicht
  hungert“ voraus.
- **If V_t = 0 beim Wohlstand**: Bei food_t = 0 gilt kein Wohlstand; bei food_t > 0 und V_t = 0
  gilt er formal, der Stamm geht aber in diesem Jahr unter (Stämme Regel 17).
- **If die Gesellschaftsform hin- und zurückwechselt**: Eine Kombination, die wieder Kandidat
  wird, behält ihr Tabu; die 5 Jahre beginnen beim nächsten Ende der Kandidatur neu.
- **If eine Norm sinkt, während eine Handlung läuft**: Das prüft das Zustimmungsmodell
  (dortige Edge Cases); dieses System ändert eine Norm höchstens um 0,02 pro Jahr.
- **If ein Mensch den Stamm wechselt**: Das ändert keine Kultur; das Zustimmungsmodell nimmt
  T_akkult lang die strengere Norm beider (auch einer eingefrorenen) Kulturen.
- **If ein Auftrag eine eingefrorene Kultur trifft**: Er wird ignoriert.

## Dependencies

**Hängt ab von:**

| System | Art | Was gebraucht wird | GDD |
|---|---|---|---|
| Simulationskern | hart | Jahrestakt O_year = 1, Zufallsstrom system_id 3, Erzeugen, Ereignisse mit Ursache-Kette | ✓ `design/gdd/simulationskern.md` (Approved) |
| Welt & Insel | hart, nur lesend | Hunger, Hauptgrund mit Ursache, Dürre, food_t | ✓ `design/gdd/welt-und-insel.md` (Approved) |
| Menschen & Bevölkerung | hart, nur lesend | V_t | ✓ `design/gdd/menschen-und-bevoelkerung.md` (Approved) |
| Stämme & Diplomatie | hart | Kriegszustand (lesend); Aufträge Kultur anlegen, Achse verschieben (mit Veranlasser), Achsen mischen | ✓ `design/gdd/staemme-und-diplomatie.md` (Approved) |
| Gotteskraft & Götterkräfte, Gotteshand & Ragdoll | weich | Auftrag „Eingriff erfassen“ mit p, Δx, ΔD, klein, Text | ✗ noch keins (Vertrag in Regel 7) |
| Begehren & Lust, Harem & Rituale | weich | Auftrag „Achse verschieben“ mit Grund Lust | Begehren & Lust ✓ `design/gdd/begehren-und-lust.md` (Regel 17 dort); Harem & Rituale noch keins |

**Wird gebraucht von:** Menschen & Bevölkerung (D_t), Zustimmungsmodell (Normen, Tabus),
Stämme & Diplomatie (x_t, Form), Begehren & Lust, Harem & Rituale (Normen, Form), Gotteskraft
& Götterkräfte (D_t, x_t), Chronik, Weltansicht & Kamera, HUD, Speichern & Laden. Menschen,
Zustimmungsmodell und Stämme nennen dieses System schon.

Die Kreise mit Menschen (D_t gegen V_t) und Stämme (x_t gegen Aufträge) sind über die
Reihenfolge im Schritt gelöst (Regel 19).

## Tuning Knobs

Achse, Hingabe, Erinnerung und Form bleiben bis zur ersten Kalibrierungsmessung (AC-21) auf
den Werten von Prototyp 1. Normen und Tabus hat der Entwickler am 2026-10-26 festgelegt;
T_merge und die Halbierung sind Vorschläge für das GDD Gotteshand & Ragdoll.

| Regler | Default | Sicherer Bereich | Wirkung |
|---|---|---|---|
| Zufall, Temperament | 0,014; ± 0,006 | 0,005–0,03; 0–0,012 | wie sehr Kulturen von selbst wandern |
| Wohlstand, Dürre, Sog; Wohlstand ab | 0,003 je; food > 0,8 · V | 0–0,01; 0,5–1,5 | sanfter Zug der Lebenslage |
| Hunger | 0,03 | 0–0,06 | Hunger macht streng |
| Schub der Erinnerung | 0,03 · p · w | 0,01–0,05 | wie stark ein Eingriff nachwirkt |
| Verblassen der Erinnerung, Vergessen ab | 0,965; 0,06 | 0,95–0,98; 0,02–0,1 | wie lange ein Eingriff erinnert wird (78 Jahre) |
| Höchstzahl Erinnerungen | 8 | 4–16 | wie viele Eingriffe eine Kultur zugleich erinnert |
| T_merge, Halbierung | 10 Jahre; 0,5 | 3–30; 0,3–0,8 | wie stark gehäufte Handaktionen wirken |
| Verblassen des Ursachenbuchs, Wegfall | 0,94; 0,001 | 0,9–0,97; 0,0005–0,005 | wie weit die Chronik bei einem Wandel zurückblickt |
| Veranlasser im Text ab | 50 % | 30–70 % | wann die Chronik einen ausgelösten Krieg dem Spieler zuschreibt |
| Grenzen der Form | 0,5 / 0,35 | 0,4–0,7 / Lücke 0,1–0,25 | wie leicht Kulturen kippen |
| Hingabe | 0,004 · (\|x\| − 0,2), Hunger − 0,01, Zufall ± 0,005, Start 0,4 | – | wie fromm Stämme werden (wirkt auf Gnade und Gotteskraft) |
| β je Handlungsart, β_R | 0,3 / 0,5 / 0,6 / 0,5; 0,4 | 0,2–0,6; 0,2–0,5 | wie stark die Kultur das Verhalten ändert |
| Δn_max | 0,02 / Jahr | 0,01–0,05 | wie schnell Normen der Kultur folgen |
| θ_an, θ_aus, T_tabu | 0,25; 0,3; 5 Jahre | 0,15–0,3; θ_an + 0,03 bis + 0,1; 2–15 | wann Tabus entstehen und fallen |
| Obergrenze Lust | 0,01 / Jahr, Gewicht 0 | 0–0,02 | wie stark Lust die Kultur verändert (Werte in den Lust-GDDs) |

## Visual/Audio Requirements

Keine eigenen. Die Weltansicht zeigt die Gesellschaftsform (Materialien und Farben der Art
Bible, etwa ein Banner) und einen Kulturwandel sichtbar am Stamm; die Art Bible legt fest,
wie eine Lust-Sekte und ein Gottesstaat aussehen.

## Game Feel

Kultur soll sich wie ein langsamer Strom anfühlen, den der Spieler lenken, aber nicht
schalten kann: Ein Eingriff gibt einen spürbaren Ruck, dann zieht die Erinnerung über
Jahrzehnte weiter, und irgendwann kippt die Form. Der Moment, in dem die Chronik den Wandel
auf einen alten Eingriff oder einen ausgelösten Krieg zurückführt, ist das Ziel.

## UI Requirements

- **Kultur ansehen** (Teil von „Stamm ansehen“): Position auf der Achse als Balken von
  Gottesstaat bis Lust-Sekte mit Richtung der letzten Jahre, Gesellschaftsform, Hingabe als
  Wort (gering unter 0,3, fromm bis 0,6, sehr fromm darüber), die drei größten Einträge des
  Ursachenbuchs als Sätze (Eingriffe und Veranlasser hervorgehoben, „weil du …“), die
  Erinnerungen mit verblassendem Gewicht
- **Normen** als Wort je Handlungsart (streng unter 0,4, gemäßigt bis 0,6, offen darüber) und
  die Tabus; nie als Rohzahl
- **Meldungen** (über die Chronik): Kulturwandel mit Ursache (groß); Tabu entsteht, Tabu fällt,
  Norm kippt (ein Wort wechselt, erst wenn n die Grenze um 0,02 überschritten hat, damit es
  nicht flackert) als kleine Meldungen

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
| `design/gdd/simulationskern.md` | Regeln 4, 7, 15, 17, Formel 5 (O_year = 1), Formel 6 (Strom 3), „Eingriff erfassen“ |
| `design/gdd/zustimmungsmodell.md` | Regeln 3, 4, 5, 7, Formel 1 (n_i, π[Ort]), Pflichten der Aufrufer, AC-27 |
| `design/gdd/menschen-und-bevoelkerung.md` | Regeln 16, 17, Formel 8 (D_t) |
| `design/gdd/staemme-und-diplomatie.md` | Regeln 8, 9, 10, 16, Formeln 3, 4, 6, 7 |
| `design/gdd/welt-und-insel.md` | Hunger mit Hauptgrund und Ursache, Dürre, food_t |

## Acceptance Criteria

Testwurzel: `Assets/Tests/EditMode/KulturUndErinnerung/`. **Testschnittstellen:** reine
Funktionen `Form(x, alt)`, `NZiel(a, x)`, `Ursache(buch, r)`, `TabuSchritt(zustand, n,
kandidat, jahr)`; ein interner Einstieg `RunCultureYear(stamm)` für die Schritte (1)–(7) aus
Regel 19 und einzeln `RunCultureSubstep(stamm, k)`; lesender Zugriff auf Ursachenbuch,
Erinnerungen, Normen und Tabus; Setzer für diese Werte nur in Tests. **Testhilfen** (mit
Aufrufprotokoll): `IRandomStream` (vorgegebene Würfe, scheitert bei Über- oder
Unterverbrauch), `IClock` (Geschichtsjahr), `IEventSink`, `IWorldSource` (hungert,
Hauptgrund mit Ursache, Dürre, food_t), `IPopulationSource` (V_t), `ITribeSource` (lebt, im
Krieg), `IInterventionSource` (sendet „Eingriff erfassen“). Eine feste `CultureConfig` mit den
Defaults dieses GDD und eine `ExactConfig` mit binär genauen Faktoren (Verblassen 0,5,
Vergessen ab 0,25, Wegfall 0,125) für Grenzfälle. „Gleich“ heißt gleicher kanonischer
Zustands-Hash (Simulationskern Regel 7).

- **AC-01 Formel 1 [Logic]** **GIVEN** D = 0,4, x = 0,6, kein Hunger, u = 0,5, **WHEN** Schritt (3) läuft, **THEN** ist D = 0,4016 ± 1e-9; mit Hunger 0,3916; mit x = −0,6 ebenfalls 0,4016; **GIVEN** D = 0,05, x = 0, Hunger, u = 0, **THEN** bleibt D = 0,05; **GIVEN** D = 1, x = 1, u = 0,999, **THEN** bleibt D = 1.
- **AC-02 Formel 2, Teile [Logic]** **GIVEN** x = 0,3, Würfe 0,5 / 0,5 / 0,5, τ = 0,002, Wohlstand, Stammesrat, eine Erinnerung p = +1 mit Gewicht 0,5 nach Schritt (2), **WHEN** Schritt (4) läuft, **THEN** ist x = 0,32 ± 1e-9, und das Ursachenbuch bekommt Zufall + 0,002, Wohlstand + 0,003 und den Eintrag der Eingriffs-ID + 0,015; **GIVEN** Hunger, **THEN** Hunger − 0,03 und kein Wohlstand; **GIVEN** Dürre, **THEN** Dürre − 0,003; **GIVEN** Lust-Sekte bzw. Gottesstaat, **THEN** Sog + 0,003 bzw. − 0,003; **GIVEN** τ = 0 und Würfe 0,5, **THEN** wird kein Zufall verbucht.
- **AC-03 Formel 2, Wohlstand und Grenzen [Logic]** **GIVEN** V = 5 und food = 4,0, **THEN** gilt kein Wohlstand, mit food = `Math.BitIncrement(4,0)` schon; im Krieg nie; **GIVEN** x = 0,99 und zwei Erinnerungen p = +1 mit w = 0,8 nach Schritt (2), sonst nichts, **THEN** ist x = 1; der größte Betrag ohne Erinnerung und Hunger ist 0,033 ± 1e-9 (Würfe 1 − 1e-12, τ = 0,006, Wohlstand, Lust-Sekte).
- **AC-04 Formel 3 [Logic]** **GIVEN** eine frische Erinnerung mit `CultureConfig`, **WHEN** 78 Kulturjahre laufen, **THEN** besteht sie noch, im 79. ist sie vergessen, und ihre Schübe ergeben zusammen das iterativ berechnete 0,03 · Σ 0,965^k ± 1e-9; **GIVEN** `ExactConfig` und w = 0,5, **THEN** ist sie nach einem Jahr (w = 0,25) vergessen.
- **AC-05 Regel 7 [Logic]** **GIVEN** x = 0,95, D = 0,4 und „Eingriff erfassen (ID 77, p = +1, Δx = +0,15, ΔD = +0,1, groß)“ außerhalb eines Kulturschritts, **THEN** ist x = 1, der Ursachenbuch-Eintrag 77 = +0,15 mit Text und Jahr, D = 0,5, und es gibt eine Erinnerung mit Gewicht 1; mit D = 0,95 wird D = 1; mit p = 1,5 oder für eine eingefrorene Kultur ändert sich nichts; mit Δx = 0 entsteht eine Erinnerung, aber kein Ursachenbuch-Eintrag.
- **AC-06 Regel 8, kleine Eingriffe [Logic]** **GIVEN** drei kleine Würfe (Δx = −0,02, ΔD = 0) am selben Stamm im selben Geschichtsjahr, **THEN** gibt es eine Erinnerung mit k = 3 und Gewicht 1, x sinkt um 0,02 + 0,01 + 0,005, und der Ursachenbuch-Eintrag der ersten ID ist −0,035; ein Wurf 10 Jahre nach dem letzten wird noch zusammengefasst, einer 11 Jahre danach ist eine neue Erinnerung; ein großer Eingriff wird nie zusammengefasst.
- **AC-07 Regel 9, Verdrängung [Logic]** **GIVEN** 8 Erinnerungen mit w · |p| = 0,9, 0,3, 0,3, 0,8, …, wobei die beiden mit 0,3 verschieden alt sind, **WHEN** eine neunte kommt, **THEN** fällt die ältere der beiden mit 0,3 heraus, und ihr Ursachenbuch-Eintrag bleibt.
- **AC-08 Formel 4 [Logic]** **GIVEN** ein Ursachenbuch mit Krieg −0,05, Eingriff 77 −0,2 und Zufall +0,01, **THEN** ist `Ursache(buch, −1)` = 77, `Ursache(buch, +1)` = Zufall; nur Einträge in Gegenrichtung → „Zufall“; Gleichstand Krieg −0,1 und Hunger −0,1 → Hunger; **GIVEN** `ExactConfig` und ein Eintrag 0,25, **THEN** ist er nach dem Verblassen 0,125 und bleibt, ein Eintrag 0,2 fällt weg (0,1 < 0,125).
- **AC-09 Formel 4, Veranlasser [Logic]** **GIVEN** Krieg −0,3 mit Anteil[77] = −0,2 und Eingriff 77 −0,1, **WHEN** die Form zum Gottesstaat wechselt, **THEN** ist die Ursache Krieg mit Veranlasser 77 und dem Text „…, den du ausgelöst hast, als du …“; mit Anteil[77] = −0,1 (unter 50 %) ohne Veranlasser; **GIVEN** zwei Welten, die sich nur in den Anteilen unterscheiden, **THEN** sind Achse, Form und Ursache gleich.
- **AC-10 Regel 12, Ereignis [Logic]** **GIVEN** ein Wechsel mit Ursache Eingriff 77, dessen Erinnerung vergessen ist, **THEN** trägt das Ereignis „Kulturwandel“ die neue Form, die Ursache 77, die drei größten Einträge und den gespeicherten Text, und es entsteht genau ein Ereignis.
- **AC-11 Formel 5 [Logic, reine Funktion]** **THEN** gilt `Form(0,5, Stammesrat)` = Stammesrat, `Form(0,5001, Stammesrat)` = Lust-Sekte, `Form(0,35, Lust-Sekte)` = Lust-Sekte, `Form(0,3499, Lust-Sekte)` = Stammesrat, `Form(−0,9, Lust-Sekte)` = Stammesrat, `Form(−0,5, Stammesrat)` = Stammesrat, `Form(−0,5001, Stammesrat)` = Gottesstaat, `Form(−0,35, Gottesstaat)` = Gottesstaat, `Form(−0,3499, Gottesstaat)` = Stammesrat.
- **AC-12 Formel 6, Ziel und Tempo [Logic]** **GIVEN** x = 0,8, 0 und −0,8, **THEN** liefert `NZiel` die Werte der Tabelle in Formel 6 (je Handlungsart, mit einer Test-Config, in der jede Art ein anderes β hat, damit Vertauschungen auffallen); **GIVEN** β = 0,6 und x = −1, **THEN** 0 (geklemmt); **GIVEN** n = 0,6 und ein Eingriff, der x von 0,2 auf 0,35 hebt, **THEN** ist n nach dem nächsten Kulturjahr 0,62.
- **AC-13 Regel 16, Tabus [Logic]** **GIVEN** ein Stammesrat mit Tabu auf Gruppe öffentlich und n[Gruppe] = 0,21 wird im Jahr Y zum Gottesstaat, **THEN** gibt es im Jahr Y noch kein Tabu auf Gruppe privat und auf dem Fest, im Jahr Y + 1 schon; **GIVEN** n[Sex] = 0,26, **THEN** entsteht das Tabu auf Sex zu zweit erst im Kulturjahr, in dem n unter 0,25 fällt; **GIVEN** ein Gottesstaat wird im Jahr Y zum Stammesrat mit n[Sex] = 0,28, das jährlich um 0,02 steigt, **THEN** fällt das Tabu auf Sex zu zweit im Jahr Y + 5 (n = 0,38 ≥ 0,3); mit n[Sex] = 0,1 erst, wenn n 0,3 erreicht (Y + 10); **GIVEN** er wird im Jahr Y + 2 wieder Gottesstaat, **THEN** bleibt das Tabu ohne Lücke; eine datengetriebene Prüfung deckt alle 3 Formen × 5 Handlungsarten × 4 Orte der Kandidatentabelle ab.
- **AC-14 Pflichten aus dem Zustimmungsmodell [Logic, metamorph]** **GIVEN** zwei Welten mit festem Startwert, die sich nur in D_t, ΔD von Eingriffen, der Hingabe der Einzelnen, Rolle, Rang, Gotteskraft und Kräften ohne Δx unterscheiden, **WHEN** 300 Kulturjahre laufen, **THEN** sind Normen und Tabus beider Welten in jedem Jahr gleich; **GIVEN** 10.000 zufällige Folgen von Eingriffen mit Δx in [−2, 2], **THEN** ändert sich keine Norm zwischen zwei Kulturjahren um mehr als 0,02 + 1e-12, `Norm` ist vor und nach jedem Erfassen gleich, n bleibt in 0–1, und kein Tabu entsteht oder fällt außerhalb von Schritt (7) (Zustimmungsmodell AC-27).
- **AC-15 Regel 17, Abfrage [Logic]** **GIVEN** einen Gottesstaat mit n[Gruppe] = 0,02 und Tabu, **THEN** liefert `Norm(t, Gruppe, privat)` (gültig, 0,02, Tabu), `Norm(t, Zärtlichkeit, öffentlich)` (gültig, n, kein Tabu), `Norm(t, Ritual, Ritual)` (gültig, Ritualnorm, kein Tabu); `Norm(t, Sex zu zweit, Ritual)`, `Norm(t, Ritual, Fest)` und `Norm(99, …)` liefern „ungültig“; keine Abfrage ändert den Zustand.
- **AC-16 Regel 2, eingefroren [Logic]** **GIVEN** ein Stamm mit einem bestehenden Tabu und einer Kombination, die seit 2 Jahren kein Kandidat mehr ist, geht unter, **WHEN** 100 Jahre laufen, **THEN** ist der Hash der eingefrorenen Kultur gleich, `Norm` liefert dieselben Werte, für sie wird kein Wurf verbraucht, Ursachenbuch und Erinnerung sind leer, und Aufträge an sie ändern nichts.
- **AC-17 Formel 7 und Mischung [Logic]** **GIVEN** beim Erzeugen die Würfe 0,75, 0,25, 0,5, 0,5, **THEN** hat Stamm 0 x = 0,1, τ = −0,003 ± 1e-9, D = 0,4, Stammesrat und die Normen aus dem Beispiel, Stamm 1 x = 0, τ = 0; **GIVEN** „Kultur anlegen (x = −0,7, D = 1,2)“, **THEN** ist die Form Gottesstaat ohne Ereignis, D = 1, es gibt keine Tabus, und Tabus können frühestens im nächsten Kulturjahr entstehen; **GIVEN** „Achsen mischen (0,2, 80, 7, −0,6)“, **THEN** ist x_S = 13,2/94 ± 1e-9 und unter „Eroberung“ ist die Differenz verbucht; mit V_S = 0 und j = 0 bleibt x_S.
- **AC-18 Regel 6 [Logic]** **GIVEN** „Achse verschieben (−0,001, Krieg, Veranlasser 77)“, **THEN** sinkt x sofort um 0,001, Krieg um 0,001 und Anteil[77] um 0,001; **GIVEN** „Achse verschieben (+0,008, Lust)“ zweimal im selben Jahr, **THEN** wirken zusammen nur 0,01; mit Gewicht 0 in `CultureConfig` liefern die Lust-Systeme 0, und der Zustand ist gleich wie ohne den Auftrag.
- **AC-19 Regel 19, Reihenfolge [Logic]** **GIVEN** 2 lebende und 1 eingefrorener Stamm, **WHEN** ein Kulturjahr läuft, **THEN** verbraucht es genau 4 Würfe je lebendem Stamm (Hingabe, dann 3 für die Achse), Stamm 0 vor Stamm 1, keinen für den eingefrorenen; die Hingabe rechnet mit der Achse vor ihrem Wandel (Fall mit x = 0,15 → 0,25, bei dem beide Ergebnisse sich unterscheiden), die Normen mit der Achse danach; zwei Eingriffe im selben Schritt werden nach ihrer ID erfasst.
- **AC-20 Speichern [Logic, Rundreise]** **GIVEN** ein Zustand mit 8 Erinnerungen (eine zusammengefasst), einem Ursachenbuch mit allen acht Gründen, Anteilen und Eingriffs-IDs, einem Tabu, einer Kombination mit laufender Mindestzeit und einer eingefrorenen Kultur, **WHEN** gespeichert, geladen und mit denselben Würfen 20 Kulturjahre weitergerechnet wird, **THEN** ist der Hash nach jedem Jahr gleich dem eines Laufs ohne Speichern. (Die Integration mit Speichern & Laden prüft dessen GDD.)
- **AC-21 Kalibrierung [Integration, ADVISORY bis zur ersten Messung; läuft, wenn alle MVP-Systeme der Simulation und die Debug-Werkzeuge stehen]** **GIVEN** die Startwerte 0 bis 99, je 10 Ären zu 25 Geschichtsjahren ohne Eingriff, **THEN** liegt die Zahl der Kulturwandel pro Welt und Ära bei 0,39 ± 20 % wie in Prototyp 1, und keine Form hat mehr als 90 % der Stammesjahre; als Kennzahlen werden gemeldet: Anteil der Stammesjahre je Form, Verteilung von D_t je Ära (Anteil bei 1), Kulturwandel je einzelnem Eingriff, Alter der zugeordneten Ursachen; **GIVEN** der programmierte Testspieler aus Prototyp 1 (etwa 4 Eingriffe), **THEN** führt die Chronik in mindestens 80 von 100 Welten mindestens 2 große Ereignisse (Krieg, Frieden, Eroberung, Abspaltung, Untergang, Kulturwandel) auf ihn zurück (Prototyp 1: 89).
- **AC-22 Spielertest [Playtest, ADVISORY]** **GIVEN** 10 Tester segnen einen Stamm wiederholt, bis er zur Lust-Sekte wird, und treiben einen anderen in einen Krieg, **WHEN** sie gefragt werden, warum die Kulturen sich verändert haben, **THEN** nennen mindestens 8 ihren Segen und mindestens 6 den ausgelösten Krieg, nur aus Chronik und Anzeige.

## Open Questions

| Frage | Wer klärt | Bis wann |
|---|---|---|
| **Hingabe ohne Gegenkraft:** Ausgeprägte Kulturen streben gegen D = 1 (Prototyp 1), was die Gnade dauerhaft hebt. Kennzahl: Anteil der Stämme mit D = 1 je Ära (AC-21) | Entwickler | nach der ersten Messung |
| **Ein Fest-Segen wirkt sehr stark:** etwa +0,93 auf der Achse über 78 Jahre, genug, um einen Stammesrat zur Lust-Sekte zu machen; die Player Fantasy sagt „immer wieder segnet“. Kennzahl: Kulturwandel je einzelnem Eingriff | Entwickler | nach der ersten Messung |
| **Gestapelte Erinnerungen:** acht gleichgerichtete Erinnerungen schieben bis zu 0,23 pro Jahr. Obergrenze pro Jahr? | Entwickler | nach der ersten Messung |
| **Zuordnung verblasst nach etwa 50 Jahren:** danach gewinnen Wohlstand oder Sog als Ursache. Langsameres Verblassen für Eingriffe? Kennzahl: Alter der zugeordneten Ursachen | Entwickler | nach der ersten Messung |
| **Der Gottesstaat hat keinen Vorteil:** Hingabe und Rituale sind symmetrisch zur Lust-Sekte, Aggression und Tabus sprechen gegen ihn. Kennzahl: Gotteskraft je Stammesjahr nach Form | GDD Gotteskraft & Götterkräfte | mit dem GDD |
| Werte der Handaktionen (p, Δx, ΔD, T_merge, Halbierung) | GDD Gotteshand & Ragdoll | mit dem GDD |
| Soll ein Blitz auf ein Mitglied die Offenheit seines Stamms zusätzlich senken, damit Auslese durch Töten nicht wirkt (Zustimmungsmodell)? Heute senkt der Blitz die Achse (Δx = −0,12) und damit langsam die Normen; die Eigenschaften der Überlebenden ändern sich nicht | Entwickler, mit Zustimmungsmodell | nach der ersten Messung |
| Werte für Lust → Kultur (Gewicht, Obergrenze) | GDD Begehren & Lust, Harem & Rituale | mit den GDDs, nach der Kalibrierung |
| Bleibt Harem nirgends tabu, und ist ein Gottesstaat-Harem mit Offenheit 0,1 („fast nie“) gewollt? | Entwickler | vor dem GDD Harem & Rituale |
| Kann die Kultur mehr Ereignisse beitragen, damit Krieg die Chronik nicht überwiegt (Stämme & Diplomatie, Open Questions)? Die kleinen Meldungen zu Tabus und Normen sind ein erster Schritt | Entwickler | nach der ersten Messung |
| Weitere Gesellschaftsformen und Achsen (Sklavenstaat) | GDD Weitere Gesellschaftsformen | Alpha |
