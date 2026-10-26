# Zustimmungsmodell

> **Status**: Approved (vom Entwickler freigegeben am 2026-10-26 nach 2 Review-Runden)
> **Author**: Entwickler + Claude
> **Last Updated**: 2026-10-25
> **Last Verified**: 2026-10-25
> **Implements Pillar**: Pillar 3 (Lust ist Kultur, nicht Beiwerk); setzt die nicht verhandelbaren Grenzen der Anti-Pillars durch

## Summary

Das Zustimmungsmodell ist eine einzige reine Funktion, durch die jede intime oder
sexuelle Handlung muss: Zärtlichkeit, Sex zu zweit, Gruppe, Harem beitreten und Ritual.
Gefangene, Verletzte, Menschen im Kampf, Tabus und jede ungültige Eingabe sperren
immer. Sonst entscheidet jeder Beteiligte über seine Bereitschaft aus Eigenschaften,
Begehren und den Normen seiner Kultur, und alle müssen zustimmen. Ohne Begehren
füreinander ist die Bereitschaft null. Hingabe, Rang und
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
Eigenschaften, seinem Begehren und den Normen seiner Kultur; ohne Begehren
füreinander ist er null. Die Handlung findet nur
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
     Eigenschaften, Geschlecht, Freiheit, Gesundheit, im Kampf, Stammes-ID), dazu je
     Beteiligtem seine ID und drei **Sperrmerkmale** vom Simulationskern: **in Gottes
     Hand**, **lebt** und **erschüttert** (Regel 8). Diese Merkmale können nur sperren, nie
     Zustimmung schaffen; deshalb widersprechen sie Regel 5 nicht
   - den **Ort**: privat, öffentlich, Fest, Ritual (der Ort „Ritual“ ist nur mit der
     Handlungsart Ritual gültig, sonst „ungültige Eingabe“)
   - die **Normen** der Kultur jedes Beteiligten für diese Handlungsart an diesem Ort:
     ein Wert 0–1 (Offenheit) und ein Merkmal „Tabu“ (Kultur & Erinnerung, Regeln 14–17). Hat ein Beteiligter in den letzten T_akkult Geschichtsjahren den Stamm
     gewechselt (Eroberung, Abspaltung, Gefangenschaft), gilt die strengere Norm beider
     Kulturen, `n = min(n_Herkunft, n_neu)`, und ein Tabu einer der beiden sperrt. Ein
     Krieg macht so niemanden über Nacht offener
   - das **Begehren** jedes Beteiligten für jeden anderen, ein Wert 0–1 (Begehren &
     Lust). Es darf nur aus Eigenschaften und Vorlieben der Person entstehen und
     gehört zu jeder Anfrage, auch zum Ritual.
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
   Ein Ritual hat 2 bis N_gruppe_max Beteiligte. Die Anfrage „Ritual“ prüft nur, ob
   jemand **teilnimmt**; dort zählt die Frömmigkeit, und wer weniger als F_min hat, nimmt
   nie teil. Jeder **Kontakt** im Ritual (Entscheidung des Entwicklers vom 2026-10-25: ein
   Ritual kann Kontakt enthalten) ist eine **eigene Anfrage der Handlungsart, die er ist**:
   zwei Personen als Zärtlichkeit oder Sex zu zweit, drei oder mehr zugleich als Gruppe,
   jeweils mit deren Gewichten (dort ist W_F = 0), Schwelle und Strafe für
   Öffentlichkeit, am Ort **Fest** (Entscheidung des Entwicklers vom 2026-10-26, Kultur &
   Erinnerung Regel 17). Das heißt „paarweise geprüft“: Jeder Kontakt besteht die Prüfung
   seiner eigenen Art, und Frömmigkeit ersetzt nie Begehren. Jede Person darf in einer Anfrage nur einmal
   vorkommen. Andernfalls lautet das Ergebnis „ungültige Eingabe“.

**Harte Sperren** (lehnen immer ab, unabhängig von allem anderen)

8. Die Anfrage wird abgelehnt, wenn bei **irgendeinem** Beteiligten eines davon gilt:
   - **nicht frei**: jeder Wert der Freiheit außer „frei“ (heute „gefangen“; einen
     späteren Zustand wie Sklaverei modelliert jedes System als Freiheit ≠ frei, nie als
     Rolle)
   - **in Gottes Hand** oder **nicht lebend**
   - **erschüttert**: in den ersten T_erschüttert Schritten nach der Rückgabe aus Gottes
     Hand, damit ein Wurf aufs Fest nie im nächsten Takt zu einer Handlung führt
   - **verletzt** (jede Verletzungsklasse)
   - **im Kampf**
   - die Kultur des Beteiligten hat für diese Handlungsart an diesem Ort ein **Tabu**
   - eine Eingabe fehlt oder ist ungültig (Regel 3)
9. Für die Masse gibt es kein Zustimmungsprofil, also auch keine Anfrage. Kinder und
   Tiere gibt es im Spiel nicht: Es gibt kein Profil für sie und keine Handlungsart mit
   ihnen.
10. Menschen „in Gottes Hand“ treffen keine Entscheidungen (Simulationskern). Begehren
    & Lust sowie Harem & Rituale stellen für sie keine Anfrage. Wird ein Beteiligter
    während einer Handlung gepackt, endet die Handlung im selben Schritt (Sperrenprüfung,
    Regel 14), und die Ansicht zeigt die Szene nicht mehr, bevor die Physik ihn bewegt.
    Weil die Ansicht in echter Zeit packt, der Kern den Griff aber erst im nächsten
    Schritt anwendet, setzt der Griff zuerst eine Sperre in der Ansicht (Regel 20). Gewalt
    und Sex überschneiden sich so nie.

**Die eigene Entscheidung**

11. Greift keine harte Sperre, entscheidet **jeder Beteiligte für sich**. Sein
    Bereitschaftswert entsteht aus seinen Eigenschaften, seinem Begehren für die
    anderen, seiner Bindung und den Normen seiner Kultur (Formulas). Ohne Begehren
    füreinander ist der Wert 0: Der Begehrens-Faktor (Formulas) skaliert ihn, und keine
    Eigenschaft und keine Norm gleicht fehlendes Begehren aus (Entscheidung des
    Entwicklers vom 2026-10-25). Er stimmt zu, wenn sein Wert die Schwelle der
    Handlungsart erreicht.
12. Die Handlung findet nur statt, wenn **alle** zustimmen. Das Ergebnis nennt bei
    Ablehnung jeden, der ablehnt, mit seinem **Hauptgrund**: dem Teil seines
    Bereitschaftswerts, der ihm am meisten fehlt (Formel 4), und dem Merkmal
    **„kulturell“**, wenn er bei voller Offenheit seiner Kultur zugestimmt hätte.

**Gründe** (für Anzeige und Chronik)

13. Mögliche Gründe einer Ablehnung: ungültige Eingabe, tot, in Gottes Hand, nicht frei,
    verletzt, im Kampf, erschüttert, Tabu der Kultur (die Sperren in dieser Rangfolge,
    der erste zutreffende gewinnt; Beteiligte in der Reihenfolge der Anfrage), kein Begehren, treu gebunden, zu wenig Lust, Norm zu streng, zu
    wenig Geselligkeit, zu wenig Frömmigkeit, zu öffentlich. Die Anzeige formt daraus einen Satz, etwa „Ilsa lehnt ab: treu
    gebunden“, mit dem Merkmal „kulturell“ etwa „Ilsa lehnt ab: Norm zu streng; in einer
    offeneren Kultur hätte sie zugestimmt“.

**Während der Handlung**

14. Eine laufende Handlung wird auf zwei Takten geprüft. **Harte Sperren** (Regel 8)
    prüft das Handlungsregister (Regel 18) für jede laufende Handlung **in jedem Schritt**,
    als eigenen Eintrag mit Takt 1 **nach Gotteskraft & Götterkräfte und vor der Chronik**,
    also nach allen Systemen, die eine Sperre setzen können (Simulationskern Formel 5).
    Greift eine Sperre (zum Beispiel weil jemand verletzt, gefangen, in einen Kampf
    gezogen, gepackt wird oder stirbt), endet die Handlung in demselben Schritt, in dem
    sich der Zustand ändert. Die **Bereitschaft** prüft es im Personentakt **jedes**
    Beteiligten neu (bei zwei Personen mit verschiedenem Takt also in jedem Schritt) und
    zusätzlich im ersten Schritt nach dem Laden eines Spielstands; sie muss mindestens die
    Einwilligungslinie θ − Δ_hold erreichen, sonst endet die Handlung (Formulas).
    Entscheidung des Entwicklers vom 2026-10-25: Der Beginn verlangt θ, die Einwilligung
    gilt bis θ − Δ_hold. Eine Handlung läuft also weiter, wenn die Bereitschaft zwischen
    beiden liegt; die Marge verhindert, dass kleine Schwankungen der Normen eine Szene
    abbrechen. Die Hilfsfunktion `PrüfeSperren(Beteiligte)` prüft nur die harten Sperren,
    sieht über die Sperrmerkmale auch „in Gottes Hand“ und „lebt“ und ist ebenfalls rein.
15. Jeder Beteiligte kann eine Handlung jederzeit beenden. Wann das geschieht, legen
    Begehren & Lust sowie Harem & Rituale fest. Das Zustimmungsmodell erzwingt nie, dass
    eine Handlung weitergeht.
16. Einen Harem **verlassen** braucht keine Prüfung und ist immer möglich, außer in
    Gottes Hand (dann im ersten Personentakt danach). Eine Mitgliedschaft heißt nie,
    dass jemand jeder Handlung zustimmt: Jede Handlung im Harem wird einzeln geprüft. Wird
    ein Mitglied gefangen genommen, endet seine Mitgliedschaft im selben Schritt; nach
    der Befreiung kommt es nur über eine neue Anfrage „Harem beitreten“ zurück (Konzept,
    nicht verhandelbare Grenzen).

**Was der Spieler beeinflussen kann**

17. Der Spieler kann Menschen an einen Ort bringen (zum Beispiel aufs Fest), die
    Kultur über die Zeit verändern (Normen über Kultur & Erinnerung) und neue Menschen
    erschaffen. Er kann nie die Eigenschaften, das Begehren oder die Entscheidung eines
    Einzelnen setzen. Keine Kraft und kein Griff der Gotteshand ist eine Eingabe dieser
    Funktion.

**Durchsetzung** (die Funktion allein kann nicht verhindern, dass ein Aufrufer sie umgeht)

18. **Ergebnis und Handlungsregister.** `Prüfe` liefert für den Beginn ein
    `Zustimmungsergebnis` und für das Halten ein eigenes `Halteergebnis`; beide sind
    Werte, die sich nicht speichern lassen (in C# `ref struct`), und haben keinen
    öffentlichen Konstruktor. Jede Aufzählung im Modell hat den Wert 0 = „ungültig“, ein
    leerer oder gefälschter Wert (in C# `default`) gilt also nie als Zustimmung. Das
    Ergebnis trägt als eigene Werte das Gesamturteil, die Handlungsart, die IDs der
    Beteiligten und einen **Stempel** (Schrittnummer, Prüfsumme der Anfrage, Kennung der
    Datentabelle), den nur `Prüfe` setzt. Laufende intime Handlungen hält genau ein
    Besitzer, das **Handlungsregister**: Es hat nach außen nur `Beginne(Zustimmungsergebnis)`
    und `Beende(Handlung, Grund)`. `Beginne` lehnt ab, wenn der Stempel fehlt, nicht vom
    aktuellen Schritt ist oder Handlungsart und Beteiligte nicht zur gewünschten Handlung
    passen; Begehren & Lust sowie Harem & Rituale beginnen Handlungen nur über `Beginne`.
    Das Register führt die Sperrenprüfung jedes Schritts und die Halteprüfung selbst aus
    (Regel 14). Wie das in Assemblies geschnitten wird, entscheidet die Architektur
    (technical-director); die Regel ist: kein anderer Weg in eine Handlung.
19. **Die Anfrage baut das Modell, nicht der Aufrufer.** `Anfrage`, `Zustimmungsprofil`
    und `Datentabelle` haben keinen öffentlichen Konstruktor. Ein einziger
    Anfrage-Baustein nimmt nur IDs (Handlungsart, Beteiligte, Ort) und holt jeden Wert bei
    seinem Besitzer: das Profil bei Menschen & Bevölkerung, die Sperrmerkmale beim Kern,
    das Begehren b_ij bei Begehren & Lust, die Bindung k_i aus den Beziehungen, die
    Normen bei Kultur & Erinnerung, die Tabelle aus dem geprüften Ladevorgang. Für diese
    Werte gilt:
    - b_ij ist eine reine Funktion fester Eigenschaften und einmal gewürfelter Vorlieben;
      es wächst nie dadurch, dass die Gotteshand zwei Menschen zusammenbringt.
    - Eine Bindung bleibt für die Bereitschaft noch **T_trauer** bestehen, nachdem der
      Partner gestorben oder gefangen genommen ist. Ein Blitz auf den Partner oder seine
      Gefangennahme macht eine Ablehnung „treu gebunden“ also nie sofort zur Zustimmung.
    - Normen ändern sich nur langsam (höchstens 0,02 pro Geschichtsjahr) und nie durch
      einen einzelnen Eingriff; Tabus entstehen und fallen nur über diese Normen (Kultur &
      Erinnerung Regeln 15, 16); nach einem Stammeswechsel gilt Regel 4.
    - Hingabe, Eingriffe, Kräfte und Rang ändern keinen dieser Werte (Property-Test im
      GDD des Besitzers, siehe Dependencies).
    - Dieselbe Anfrage (gleiche Beteiligte, gleiche Handlungsart) stellen die Aufrufer
      erst wieder, wenn sich einer ihrer Werte geändert hat.
20. **Die Ansicht.** Sie zeigt eine Szene nur, solange (a) die Sperrenprüfung für alle
    Beteiligten frei ist, (b) kein eingereihter, noch nicht angewendeter Eingriff einen
    Beteiligten betrifft und (c) ihre eigene Sperre frei ist. Ein Griff der Gotteshand
    oder eine Gewalt-Kraft setzt zuerst diese eigene Sperre, blendet die Szene aus und
    startet erst danach die Physik. Ragdolls kollidieren nie mit Beteiligten einer
    laufenden Szene (eigene Kollisionsebene). So gibt es in keinem gezeichneten Bild
    Gewalt und Sex zugleich, auch nicht in den Bildern zwischen Eingriff und Ende der
    Handlung.
21. **Umgekehrt** (Konzept: Kampf- und Verletzungszustände sperren Sexuelles und
    umgekehrt): Stämme & Diplomatie zieht niemanden, der in einer laufenden Handlung
    steht, in einen Kampf oder in Gefangenschaft, und die Opferwahl von Menschen &
    Bevölkerung zieht ihn bei einem Verlust nicht. Gewalttätige Kräfte und die Gotteshand
    dürfen einen Beteiligten treffen, aber die Handlung endet dann nach Regel 14 im
    selben Schritt und die Ansicht nach Regel 20 schon vorher.

### States and Transitions

Die Prüffunktion hat **keinen Zustand**. Die laufenden Handlungen hält das
Handlungsregister (Regel 18); wann eine Handlung angefragt und freiwillig beendet wird,
entscheiden Begehren & Lust sowie Harem & Rituale. Die Prüfung greift an drei Stellen ein:

| Zeitpunkt | Prüfung | Ergebnis |
|---|---|---|
| Vor Beginn | volle Prüfung: Sperren, dann Bereitschaft gegen Schwelle | Zustimmung → Handlung beginnt; Ablehnung → sie beginnt nicht, Grund wird gemeldet |
| In jedem Schritt während der Handlung, nach Gotteskraft und vor der Chronik | nur die harten Sperren (`PrüfeSperren`) | weiter, oder Ende in demselben Schritt mit dem Grund der Sperre |
| Im Personentakt jedes Beteiligten und im ersten Schritt nach dem Laden | Sperren, dann Bereitschaft gegen die Einwilligungslinie (Schwelle minus Haltemarge) | weiter, oder sofortiges Ende mit Grund |

### Interactions with Other Systems

| System | Was hineingeht | Was herauskommt | Wer besitzt die Schnittstelle |
|---|---|---|---|
| Menschen & Bevölkerung | Zustimmungsprofile (fester Inhalt, siehe dort Regel 18) | – | Menschen |
| Kultur & Erinnerung | Normen: Offenheit 0–1 und Tabu je Handlungsart und Ort | – | Kultur & Erinnerung (`design/gdd/kultur-und-erinnerung.md`, Regel 17) |
| Begehren & Lust | Anfrage per IDs (Zärtlichkeit, Sex zu zweit, Gruppe, Kontakt im Ritual); Begehren b_ij und Bindungen als Funktion | Ergebnis mit Gründen; Beginne / Beende im Handlungsregister | Zustimmungsmodell (Anfrage-Baustein, Ergebnis, Handlungsregister) |
| Harem & Rituale | Anfrage per IDs (Harem beitreten, Teilnahme am Ritual); Bindungen im Harem | Ergebnis mit Gründen; Beginne / Beende im Handlungsregister | Zustimmungsmodell |
| Simulationskern | Sperrmerkmale in Gottes Hand, lebt, erschüttert; Schrittnummer für den Stempel | Eintrag „Sperrenprüfung“ mit Takt 1 nach Gotteskraft (Handlungsregister); die Prüffunktion selbst hat keinen Zufall und keinen Takt | Kern (Reihenfolge), Zustimmungsmodell (Register) |
| Chronik, HUD & Chronik-Ansicht | – | Gründe einer Ablehnung, die das aufrufende System als Ereignis meldet | aufrufendes System meldet, Chronik wählt aus |
| Gotteskraft & Götterkräfte, Gotteshand & Ragdoll | **keine Eingabe** (Regel 5, 17) | – | – |
| Stämme & Diplomatie | – | Vertrag (Regel 21): zieht niemanden aus einer laufenden Handlung in Kampf oder Gefangenschaft | Stämme & Diplomatie (`design/gdd/staemme-und-diplomatie.md`, Draft: im MVP-0 zieht es niemanden, Regel 13) |
| Weltansicht & Kamera | Sperrenprüfung und eingereihte Eingriffe lesend | Szene nur nach Regel 20, eigene Sperre vor jeder Gewalt-Darstellung, Kollisionsebene | Weltansicht |
| Menschen & Bevölkerung (Opferwahl, Stammeswechsel) | – | Vertrag: Beteiligte einer laufenden Handlung werden bei Verlusten nicht gezogen; Stamm und Schritt des letzten Wechsels für Regel 4 | Menschen |

## Formulas

Vorschlag des `systems-designer` (2026-10-25), im guided-Modus ohne Einzelfreigabe
übernommen; der Entwickler prüft ihn mit dem GDD. Die Anteile in Formel 5 sind
Monte-Carlo-Schätzungen; die Umsetzung prüft sie mit AC-20. Gerechnet wird überall in
double. Gleitkommazahlen sind erlaubt, weil Determinismus nur auf demselben Rechner verlangt
wird (Simulationskern); Vergleiche beim Laden der Tabelle haben eine Toleranz von 1e-9.

### 1. Bereitschaft

The Bereitschaft formula is defined as:

`w_i = g_i · max(0, W_D·D_i + W_L·L_i + W_N·n_i + W_G·G_i + W_F·F_i − c_pub[a]·π[Ort]·(1 − G_i) − c_bind[a]·k_i·T_i)` mit dem Begehrens-Faktor `g_i = min(1, min_j b_ij / b_ref)`

D_i ist bei Handlungen zu zweit das Begehren b_ij für den anderen. Beim Harem ist es das
Begehren für die andere Partei (Haremshaupt oder Beitretende). In der Gruppe gilt
`D_i = μ·min_j b_ij + (1 − μ)·mittel_j b_ij`, ebenso beim Ritual (bei 2 Beteiligten ist das
einfach b_ij). Der Begehrens-Faktor g_i nimmt immer das **kleinste** Begehren von i für einen
der anderen Beteiligten: Hat i für auch nur eine Person kein Begehren, ist g_i = 0 und damit
w_i = 0, ganz gleich wie hoch Lust, Norm, Geselligkeit oder Frömmigkeit sind (Entscheidung des
Entwicklers vom 2026-10-25: „mit dem Begehren multiplizieren“). Ab b_ref bremst der Faktor nicht
mehr (g = 1), und die Schwellen aus Formel 2 gelten wie zuvor.

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| L_i, G_i, F_i, T_i | float | 0–1 | calculated | Lust, Geselligkeit, Frömmigkeit, Treue aus dem Zustimmungsprofil (fest); Treue wirkt nur über die Bindungsstrafe |
| n_i | float | 0–1 | calculated | Offenheit der Norm seiner Kultur für diese Handlungsart an diesem Ort (Kultur & Erinnerung) |
| b_ij | float | 0–1 | calculated | Begehren von i für j (Begehren & Lust) |
| k_i | bool | 0 oder 1 | calculated | an jemanden gebunden, der nicht teilnimmt |
| W_D, W_L, W_N, W_G, W_F | double | je ≥ 0, Summe 1 ± 1e-6, beim Laden auf genau 1 normiert; W_D + W_L ≥ 0,40; W_N ≤ 0,40; beim Ritual W_D > 0 | data file | Gewichte je Handlungsart |
| c_pub[a], c_bind[a] | float | 0–0,6 | data file | Stärke der Strafen je Handlungsart |
| π[Ort] | float | 0–1 | data file | wie öffentlich der Ort ist |
| μ | float | 0–1, Default 0,5 | data file | Anteil des kleinsten Begehrens in der Gruppe und beim Ritual |
| b_ref | float | 0,2–0,5, Default 0,3 | data file | Begehren, ab dem der Faktor g nicht mehr bremst; darunter sinkt w linear auf 0 |
| g_i | float | 0–1 | calculated | Begehrens-Faktor von i |
| F_min | double | 0–0,5, Vorschlag 0,30 (wartet auf den Entwickler) | data file | nur Ritual: mindestens so viel Frömmigkeit, sonst nimmt jemand nicht teil (Regel 7); etwa 12 % haben weniger |
| w_i | float | 0–1 | calculated | Bereitschaft von i |

**Standardwerte der Datentabelle:**

| Handlungsart | W_D | W_L | W_N | W_G | W_F | c_pub | c_bind |
|---|---|---|---|---|---|---|---|
| Zärtlichkeit | 0,50 | 0,20 | 0,30 | 0 | 0 | 0,20 | 0,20 |
| Sex zu zweit | 0,45 | 0,30 | 0,25 | 0 | 0 | 0,30 | 0,40 |
| Gruppe | 0,35 | 0,25 | 0,25 | 0,15 | 0 | 0,25 | 0,50 |
| Harem beitreten | 0,35 | 0,10 | 0,30 | 0,25 | 0 | 0 | 0,50 |
| Ritual | 0,25 | 0,15 | 0,30 | 0 | 0,30 | 0 | 0,40 |

π: privat 0, Ritual 0, Fest 0,5, öffentlich 1. Die Ritual-Zeile prüft nur die
**Teilnahme**; jeder Kontakt im Ritual nutzt die Zeile der Handlung, die er ist (Regel 7),
dort ist W_F = 0. Der Ort „Ritual“ gilt nur für die Handlungsart Ritual.

**Output Range:** 0–1. Die Gewichte ergeben zusammen 1, deshalb gibt es keine Division.
Den Wert 1 erreicht w nur, wenn alle Terme 1 sind und keine Strafe greift. Unter 0
verhindert das max. Jeder positive Term hat ein Gewicht ≥ 0, und die Strafe für
Öffentlichkeit sinkt mit steigender Geselligkeit. Mehr Begehren, Lust, Offenheit,
Geselligkeit oder Frömmigkeit senkt w also nie. Eine Datentabelle, deren Gewichte nicht
1 ± 1e-6 ergeben, gilt als ungültig (Formel 2); eine gültige wird beim Laden auf genau 1
normiert, sodass w nie über 1 steigt. Auch g_i ist nie negativ und steigt mit dem
Begehren, das Produkt senkt w bei mehr Begehren also nie. Zusicherung: Für jeden Beteiligten mit
min_j b_ij < θ[a] · b_ref gilt w_i ≤ g_i < θ[a], er lehnt also ab (Grund „kein Begehren“), was auch
immer seine übrigen Werte sind. Bei den Defaults ist das ein Begehren unter 0,12 (Zärtlichkeit),
0,144 (Sex), 0,156 (Gruppe), 0,165 (Harem) und 0,135 (Ritual).
**Example:** Sex zu zweit auf einem Fest mit b = 0,6, L = 0,5, n = 0,5, G = 0,4, k = 1,
T = 0,7. Die Terme ergeben 0,27 + 0,15 + 0,125 = 0,545. Die Strafe für Öffentlichkeit
ist 0,3 · 0,5 · 0,6 = 0,09, die für die Bindung 0,4 · 0,7 = 0,28. Damit ist w = 0,175
(b = 0,6 liegt über b_ref, also g = 1).
**Example 2 (Begehren fehlt):** Sex zu zweit, privat, b = 0,1, L = 1, n = 1, k = 0. Der Rohwert
ist 0,045 + 0,30 + 0,25 = 0,595 und läge ohne Faktor über θ = 0,48. Mit b_ref = 0,3 ist
g = 0,333 und w = 0,198: Ablehnung, Grund „kein Begehren“. Bei b = 0 ist w = 0. Mit b = 0,3 ist
g = 1 und w = 0,685.

### 2. Schwellen

The Schwelle formula is defined as:

`zustimmung_i = (w_i ≥ θ[a])`; die Handlung findet statt, wenn `zustimmung_i` für alle Beteiligten gilt, mit `3 ≤ N_gruppe ≤ N_gruppe_max` (beim Ritual `2 ≤ N_ritual ≤ N_gruppe_max`)

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| θ[a] | double | 0,30–0,90 | data file | Beginn-Schwelle je Handlungsart. Defaults: Zärtlichkeit 0,40, Sex zu zweit 0,48, Gruppe 0,52, Harem beitreten 0,55, Ritual 0,45 |
| N_gruppe_max | int | 3–6, Default 4 | data file | größte Gruppe, auch beim Ritual. Mehr Beteiligte machen die Zustimmung aller unwahrscheinlicher und die Anzeige schwerer lesbar |

**Output Range:** ja oder nein je Beteiligtem. θ = 0 wäre automatische Zustimmung, θ > 1
hieße nie. Beides ist verboten, ebenso alles außerhalb von 0,30–0,90. Beim Laden der
Daten muss (mit Toleranz 1e-9) gelten:
- θ_Zärtlichkeit ≤ θ_Sex ≤ θ_Gruppe
- θ − Δ_hold ≥ 0,20 und (θ − Δ_hold) · b_ref ≥ 0,09 für jede Handlungsart, damit auch
  eine laufende Handlung nie bei fast keinem Begehren weiterläuft (bei den Defaults
  0,096 / 0,12 / 0,132 / 0,141 / 0,111)
- W_D + W_L ≥ 0,40 und W_N ≤ 0,40, damit nie die Kultur allein entscheidet; beim Ritual
  W_D > 0
- jede Zahl endlich und in ihrem Bereich, keine Zeile fehlt, N_gruppe_max 3–6, μ, π und
  b_ref in ihren Bereichen

Verletzt die Datentabelle eine dieser Regeln, lehnt das Modell jede Anfrage mit
„ungültige Eingabe“ ab (Regel 3). Die Tabelle hat keinen öffentlichen Konstruktor, nur der
Ladevorgang erzeugt sie (Regel 19); eine leere Tabelle (`default`) ist ungültig.
**Example:** Der Beteiligte aus Formel 1 hat w = 0,175 gegen θ = 0,48 und lehnt ab.

### 3. Haltemarge

The Haltemarge formula is defined as:

`weiter_i = (w_i ≥ θ[a] − Δ_hold)`, geprüft nach den harten Sperren

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| Δ_hold | float | 0–0,15, Default 0,08 | data file | Haltemarge für laufende Handlungen |

**Output Range:** Die **Einwilligungslinie** einer laufenden Handlung ist θ − Δ_hold
(Entscheidung des Entwicklers vom 2026-10-25). Der Beginn verlangt θ als Sicherheitsabstand; wer
einmal klar zugestimmt hat, bleibt bis zur Linie dabei, und kleine Schwankungen brechen keine
Szene ab. Der Preis: Eine Handlung läuft weiter, solange w im Streifen [θ − Δ_hold, θ) liegt.
Darunter endet sie sofort. Der Begehrens-Faktor gilt unverändert (fällt das Begehren auf 0, ist
w = 0 und die Handlung endet). Eine laufende Handlung braucht mindestens das Begehren
(θ − Δ_hold) · b_ref, bei den Defaults 0,096 bis 0,141 (Formel 2). Beim
Laden muss θ − Δ_hold ≥ 0,20 gelten, damit eine Handlung nicht durch eine fast leere
Schwelle unbegrenzt weiterläuft. Δ_hold = 0 bedeutet keine Hysterese.
**Example:** Sex zu zweit beginnt ab 0,48 und endet unter 0,40. Sinkt w durch eine
Normänderung von 0,46 auf 0,43, läuft die Handlung weiter.

### 4. Hauptgrund

The Hauptgrund formula is defined as:

`s_D = W_D·(1 − D) + w_roh·(1 − g)`, `s_L = W_L·(1 − L)`, `s_N = W_N·(1 − n)`, `s_G = W_G·(1 − G)`, `s_F = W_F·(1 − F)`, `s_pub = c_pub·π·(1 − G)`, `s_bind = c_bind·k·T`; `Hauptgrund = argmax(s)`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| s_x | float | 0–1 | calculated | Beitrag zum Fehlbetrag: so viel hat w durch den Term x verloren. s_D bündelt alles, was am fehlenden Begehren hängt: den eigenen Term und den Teil, den der Faktor g abzieht |
| w_roh | float | 0–1 | calculated | Bereitschaft vor dem Begehrens-Faktor, das `max(0, …)` aus Formel 1 |
| Hauptgrund | enum | einer der Gründe unten | calculated | gemeldeter Grund eines Ablehnenden |

Vor dem argmax gilt: Ist g_i < θ[a] (in der Phase Halten g_i < θ[a] − Δ_hold), ist der
Hauptgrund immer „kein Begehren“, auch wenn Strafen w_roh auf 0 drücken. Sonst gewinnt
der größte Beitrag: m = max(s), und der Erste in dieser Liste mit s ≥ m − 1e-9:

| Term | Grund |
|---|---|
| s_bind | treu gebunden |
| s_D | kein Begehren |
| s_pub | zu öffentlich |
| s_L | zu wenig Lust |
| s_N | Norm zu streng |
| s_G | zu wenig Geselligkeit |
| s_F | zu wenig Frömmigkeit |

Das Merkmal **„kulturell“** ist gesetzt, wenn derselbe Beteiligte mit n = 1 (und sonst
gleichen Werten) zugestimmt hätte. Es macht sichtbar, wo die Kultur entschieden hat, auch
wenn ein anderer Term der größte ist (bei Norm 0,5 wird „Norm zu streng“ sonst nie
Hauptgrund).

**Output Range:** Berechnet nur für Ablehnende (w unter θ, oder bei laufender Handlung
unter θ − Δ_hold). Ohne Klemmen gilt w = 1 − Σ s. Bei einer Ablehnung ist die Summe also
größer als 1 − θ, und mindestens ein s ist positiv. Harte Sperren haben eigene Gründe
(gefangen, verletzt, im Kampf, Tabu, ungültige Eingabe) und kommen immer vor dieser
Formel.
**Example:** Für den Beteiligten aus Formel 1 gilt s_D = 0,18, s_L = 0,15, s_N = 0,125,
s_pub = 0,09 und s_bind = 0,28. Hauptgrund: „treu gebunden“. Für Beispiel 2 gilt
s_D = 0,45 · 0,9 + 0,595 · 0,667 = 0,802 und alle anderen 0: Hauptgrund „kein Begehren“, und
w = 1 − 0,802 = 0,198.

### 5. Plausibilität: Wie stark formt die Kultur das Verhalten?

Annahmen: Eigenschaften wie in Menschen & Bevölkerung (Formel 5: Mittel dreier
Gleichverteilter, Mittelwert 0,5, Streuung 0,167). Für das Begehren, das Begehren & Lust
noch festlegt, ist eine Normalverteilung mit Mittelwert 0,5 und Streuung 0,2 angenommen,
auf 0–1 geklemmt. Dazu privater Ort, keine Bindung, μ = 0,5 und b_ref = 0,3. Die Werte geben
an, wie oft alle Beteiligten zustimmen, wenn man ihre Entscheidungen als unabhängig
betrachtet. Zärtlichkeit, Sex, Harem und Ritual haben 2 Beteiligte, die Gruppe 3. Gerechnet
wurde per Monte Carlo (300.000 Wiederholungen je Feld, fester Startwert, 2026-10-25), weil der
Begehrens-Faktor die Normalverteilungsnäherung der ersten Fassung ungenau macht.

| Handlungsart | Norm 0,5 | Lust-Sekte (Norm 0,9) | strenge Norm (0,15) |
|---|---|---|---|
| Zärtlichkeit | ca. 66 % | ca. 81 % | ca. 23 % |
| Sex zu zweit | ca. 33 % | ca. 69 % | ca. 6,6 % |
| Gruppe (3) | ca. 2,2 % | ca. 32 % | unter 0,1 % |
| Harem beitreten | ca. 7,4 % | ca. 59 % | ca. 0,1 % |
| Teilnahme am Ritual (2, mit F_min = 0,30) | ca. 45 % | ca. 63 % | ca. 5,3 % |

Der Begehrens-Faktor kostet gegenüber der ersten Fassung (ohne Faktor) bei Sex zu zweit in
der Lust-Sekte etwa 9 Punkte (78 % → 69 %); bei Norm 0,5 und im Gottesstaat ändert sich
fast nichts. Mit b_ref = 0,5 wären es nur 42 % in der Lust-Sekte, der Faktor bremst dann zu
stark; deshalb liegt der Default bei 0,3.

Die Kultur verändert das Verhalten um das 3,5- bis über 10-Fache (Sex zu zweit: 6,6 % gegen
69 %), und bei Norm 0,5 stimmt weder jeder immer noch nie zu (Pillar 3). Ein Gottesstaat, der
für Rituale eine hohe Norm setzt, kommt bei der Teilnahme auf etwa 63 %; Kontakt im Ritual
folgt den Anteilen von Zärtlichkeit, Sex zu zweit und Gruppe (Regel 7). Die Spanne bei Sex zu zweit ist das
Prüfziel für Pillar 3 (Acceptance Criteria). Gruppen sind bei neutraler Norm selten (2 %) und
werden erst in einer Lust-Sekte häufig (32 %); das ist gewollt, weil die Gruppe die höchste
Hürde hat. Die Schätzung übersieht, dass Begehren zwischen zwei Partnern zusammenhängen kann.
Ein Mensch mit Treue 0 bekommt keine Bindungsstrafe, das ist gewollt.

## Edge Cases

*`systems-designer` not consulted for this section — Lean mode. Review manually before production.*

- **If die Gotteshand einen Beteiligten während einer Handlung packt**: Die Ansicht
  beendet die Darstellung der Szene im selben Bild, bevor der Ragdoll startet (Regel
  20). Die Simulation beendet die Handlung in dem Schritt, in dem der Kern das Ereignis
  „Beginn in Gottes Hand“ anwendet (die Sperrenprüfung jedes Schritts sieht den
  Status), mit dem Grund „in Gottes Hand“. Dasselbe gilt für jede gewalttätige Kraft
  (Blitz): Die Ansicht beendet die Szene vor dem ersten Wirkungsbild. Ragdoll und Szene
  sind also nie gleichzeitig zu sehen (nicht verhandelbare Grenze „Gewalt und Sex
  überschneiden sich nie“).
- **If das Begehren einseitig ist** (A begehrt B, B nicht): B lehnt mit „kein
  Begehren“ ab, die Handlung findet nicht statt. Das gilt für jede Kombination der
  übrigen Werte, denn ohne Begehren ist w = 0 (Formel 1, AC-23).
- **If die Beteiligten aus verschiedenen Stämmen kommen**: Jeder entscheidet nach den
  Normen seiner eigenen Kultur. Ein Tabu in einer der Kulturen sperrt die ganze
  Anfrage.
- **If in einer Gruppe ein Einzelner ablehnt**: Die ganze Gruppenhandlung findet nicht
  statt. Begehren & Lust darf danach eine neue Anfrage mit weniger Beteiligten stellen,
  und die wird neu und vollständig geprüft.
- **If eine einzelne Person allein betet oder opfert**: Das ist keine intime Handlung
  und geht nicht durch das Modell. Die Teilnahme an einem Ritual mit anderen ist die
  Anfrage „Ritual“ (2 bis N_gruppe_max Beteiligte); jeder Kontakt darin ist eine eigene
  Anfrage seiner Handlungsart (Regel 7). Ein Kontakt zwischen mehr als N_gruppe_max
  Personen zugleich gibt es nicht.
- **If der Partner eines Gebundenen getötet oder gefangen genommen wird** (zum Beispiel
  durch einen Blitz, nachdem die Anzeige „treu gebunden“ gezeigt hat): Die Bindung zählt
  noch T_trauer weiter (Regel 19). Gewalt öffnet so keine Zustimmung.
- **If ein Stamm erobert wird oder sich abspaltet**: Wer den Stamm wechselt, behält
  T_akkult lang die strengere Norm beider Kulturen (Regel 4).
- **If ein Harem-Mitglied gefangen genommen wird**: Die Mitgliedschaft endet im selben
  Schritt; nach der Befreiung nur über eine neue Beitrittsanfrage (Regel 16).
- **If ein Beteiligter während einer Handlung stirbt** (Alter, Hunger, Blitz): Sperre
  „tot“, die Handlung endet im selben Schritt; die Opferwahl zieht Beteiligte einer
  laufenden Handlung ohnehin nicht (Regel 21).
- **If ein Spielstand mit laufenden Handlungen geladen wird**: Im ersten Schritt prüft das
  Register alle laufenden Handlungen neu (Regel 14), auch wenn sich die Datentabelle
  seither geändert hat.
- **If während einer Handlung die Norm sinkt** (zum Beispiel durch Kulturwandel): Die
  Handlung läuft weiter, solange die Bereitschaft aller über θ − Δ_hold liegt. Darunter
  endet sie. Gemeldet werden alle, die darunter liegen, mit ihrem Hauptgrund, in der
  Reihenfolge ihres Index in der Anfrage.
- **If ein Beteiligter während einer Handlung verletzt, gefangen oder in einen Kampf
  gezogen wird**: Die Handlung endet in demselben Schritt mit dem Grund der Sperre
  (Regel 14), und die Ansicht zeigt die Szene sofort nicht mehr (Regel 20). Den
  Verletzten trifft dabei kein weiterer Kontakt. Stämme & Diplomatie zieht ohnehin
  niemanden aus einer laufenden Handlung in Kampf oder Gefangenschaft (Regel 21).
- **If das Haremshaupt beim Beitritt gefangen, verletzt oder im Kampf ist**: Harte
  Sperre, der Beitritt wird abgelehnt.
- **If ein Mensch an jemanden gebunden ist, der an der Handlung teilnimmt**: Es gibt
  keine Bindungsstrafe (k_i = 0). Die Strafe gilt nur für Bindungen nach außen.
- **If dieselbe Person zweimal in einer Anfrage steht, oder die Zahl der Beteiligten
  nicht zur Handlungsart passt**: „ungültige Eingabe“.
- **If ein Wert in der Anfrage NaN, unendlich oder außerhalb von 0–1 ist, oder die
  Handlungsart oder der Ort unbekannt**: „ungültige Eingabe“. Das Modell versucht nie,
  einen Wert zu korrigieren oder zu klemmen.
- **If die Datentabelle ungültig ist** (eine Regel aus Formel 2 verletzt: Gewichte
  ergeben nicht 1 oder sind negativ, eine Schwelle außerhalb von 0,30–0,90, die
  Reihenfolge der Schwellen stimmt nicht, θ − Δ_hold < 0,20, (θ − Δ_hold) · b_ref < 0,09,
  W_D + W_L < 0,40, W_N > 0,40, Ritual mit W_D = 0, b_ref oder F_min außerhalb des
  Bereichs, NaN oder eine fehlende Zeile):
  Jede Anfrage wird mit „ungültige Eingabe“ abgelehnt. Das Spiel läuft weiter, nur ohne
  sexuelle Handlungen. Den Fehler meldet der Ladevorgang der Datentabelle einmal in der
  Konsole und im Debug-Werkzeug; die Prüffunktion selbst meldet nichts.
- **If die Bereitschaft genau der Schwelle entspricht**: Zustimmung (≥).
- **If eine Person zur gleichen Zeit in zwei Anfragen steht** (zum Beispiel ein Fest
  und ein Ritual im selben Schritt): Das Modell ist rein und kann das nicht wissen.
  Begehren & Lust sowie Harem & Rituale stellen sicher, dass ein Mensch höchstens an
  einer laufenden Handlung teilnimmt. Die zweite Anfrage stellen sie nicht.
- **If ein Mensch aufs Fest geworfen wird**: Der Wurf selbst ist keine Eingabe. Landet
  er unverletzt, ist er zuerst T_erschüttert lang gesperrt („erschüttert“, Regel 8);
  danach kann Begehren & Lust eine normale Anfrage stellen. Ist er
  verletzt, sperrt die Verletzung jede Handlung, bis er geheilt ist.
- **If zwei Menschen desselben Geschlechts einander begehren**: Das Modell
  berücksichtigt das Geschlecht nicht. Ob jemand begehrt wird, entscheidet allein der
  Begehren-Wert aus Begehren & Lust.

## Dependencies

**Hängt ab von:**

| System | Art | Was gebraucht wird | GDD |
|---|---|---|---|
| Menschen & Bevölkerung | hart | Zustimmungsprofil (fester Inhalt) | ✓ `design/gdd/menschen-und-bevoelkerung.md` |
| Kultur & Erinnerung | hart | Normen: Offenheit 0–1 und Tabu je Handlungsart und Ort | ✓ `design/gdd/kultur-und-erinnerung.md` (Regeln 14–17, Formel 6) |
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

**Pflichten, die andere Systeme übernehmen müssen** (jedes dieser GDDs nennt sie in
seinen Dependencies und führt die Tests):

- Begehren & Lust und Harem & Rituale: Handlungen nur über `Beginne` im
  Handlungsregister (Regel 18); Anfragen nur über den Anfrage-Baustein (Regel 19); b_ij
  ist eine reine Funktion fester Eigenschaften und einmal gewürfelter Vorlieben, wächst
  nie durch Platzieren mit der Gotteshand (Wiedergabe-Test mit und ohne Platzieren),
  und k_i hält T_trauer nach Tod oder Gefangennahme des Partners an; Property-Test, dass
  Hingabe, Eingriffe, Kräfte und Rang b_ij und k_i nie ändern; dieselbe Anfrage erst
  wieder, wenn sich ein Wert geändert hat.
- Harem & Rituale zusätzlich: Gefangennahme beendet die Mitgliedschaft im selben
  Schritt, Rückkehr nur über eine neue Beitrittsanfrage; jeder Kontakt im Ritual als
  eigene Anfrage seiner Handlungsart (Regel 7).
- Kultur & Erinnerung: Normen ändern sich nur langsam, kein Eingriff setzt eine Norm
  direkt (höchstens 0,02 pro Geschichtsjahr; Tabus nur über die Normen, Kultur &
  Erinnerung Regeln 15, 16, AC-14).
- Menschen & Bevölkerung: Freiheit hat nur „frei“ als Wert, der nicht sperrt; die
  Opferwahl zieht keine Beteiligten einer laufenden Handlung; Stamm und Schritt des
  letzten Stammeswechsels für die Normen (Regel 4).
- Simulationskern: Eintrag „Sperrenprüfung“ mit Takt 1 nach Gotteskraft und vor der
  Chronik; Sperrmerkmale in Gottes Hand, lebt und erschüttert für jeden Beteiligten.
- Stämme & Diplomatie: zieht niemanden aus einer laufenden Handlung in Kampf oder
  Gefangenschaft (Regel 21); der Test dafür steht in deren GDD gegen den echten
  Auswahlweg.
- Weltansicht & Kamera: zeigt Szenen nur nach Regel 20, mit eigener Sperre vor jeder
  Gewalt-Darstellung und einer Kollisionsebene für Ragdolls.

## Tuning Knobs

| Regler | Default | Sicherer Bereich | Wirkung |
|---|---|---|---|
| θ je Handlungsart | 0,40 / 0,48 / 0,52 / 0,55 / 0,45 | 0,30–0,90, Reihenfolge Zärtlichkeit ≤ Sex ≤ Gruppe | wie leicht eine Handlung beginnt |
| Δ_hold | 0,08 | 0–0,15, θ − Δ_hold ≥ 0,20 | wie weit die Bereitschaft während einer Handlung sinken darf |
| b_ref | 0,3 | 0,2–0,5, (θ − Δ_hold) · b_ref ≥ 0,09 | ab welchem Begehren der Faktor nicht mehr bremst |
| Gewichte W | Tabelle in Formel 1 | Summe 1, W_D + W_L ≥ 0,40, W_N ≤ 0,40 | wie viel Person und Kultur zählen |
| c_pub, c_bind | Tabelle in Formel 1 | 0–0,6 | Strafe für Öffentlichkeit und Bindung nach außen |
| π je Ort | 0 / 0 / 0,5 / 1 | 0–1 | wie öffentlich ein Ort ist |
| μ | 0,5 | 0–1 | wie stark das kleinste Begehren in der Gruppe zählt |
| N_gruppe_max | 4 | 3–6 | größte Gruppe und größter Kontakt im Ritual |
| F_min | 0,30 (Vorschlag) | 0–0,5 | wer an einem Ritual nie teilnimmt |
| T_trauer | 5 Lebensjahre (Vorschlag) | 1–20 Lebensjahre | wie lange eine Bindung nach Tod oder Gefangennahme des Partners zählt |
| T_akkult | 60 Geschichtsjahre (Vorschlag) | 25–250 | wie lange nach einem Stammeswechsel die strengere Norm gilt |
| T_erschüttert | 1 Lebensjahr (Vorschlag) | 0,5–5 Lebensjahre | Sperre nach der Rückgabe aus Gottes Hand |

Die mit „Vorschlag“ markierten Werte hat die Nachtarbeit gesetzt; sie warten auf den
Entwickler (Open Questions).

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
- **AC-01 [Logic/EditMode]** **GIVEN** 100.000 zufällige Anfragen, jede mit mindestens einem Beteiligten, der gefangen, verletzt oder im Kampf ist oder dessen Kultur ein Tabu setzt, **WHEN** `Prüfe` läuft, **THEN** gibt es 100.000 Ablehnungen und 0 Zustimmungen, der Grund ist immer eine Sperre und nie ein Bereitschaftsgrund, auch bei Begehren 1, Norm 1 und Lust 1; die Anfragen decken jede Handlungsart × jede Sperre (dazu Freiheit ≠ frei, in Gottes Hand, nicht lebend, erschüttert) × jede Position in der Anfrage × beide Phasen ab, und bei mehreren Sperren gilt die Rangfolge aus Regel 13.
- **AC-02 [Logic/EditMode]** **GIVEN** 100.000 Anfragen mit je einem ungültigen Wert (NaN, ± unendlich, unter 0, über 1, unbekannte Handlungsart, unbekannter Ort, unbekannte Phase, fehlende Norm), falscher Zahl von Beteiligten, doppelter Person oder einer Gruppe über N_gruppe_max, **WHEN** geprüft wird, **THEN** lautet das Ergebnis immer „ungültige Eingabe“, und die Anfrage des Aufrufers ist danach unverändert (nichts wird geklemmt); ebenso für `default`-Werte jedes Typs (eine leere Norm entfernt nie ein Tabu), null, zu kurze Felder, eine Begehrensmatrix kleiner als N × N und nicht definierte Werte einer Aufzählung, ohne Ausnahme; jede Aufzählung hat den Wert 0 = „ungültig“.
- **AC-03 [Logic/EditMode]** **GIVEN** gültige Anfragen aller 5 Handlungsarten, **WHEN** die Zahl der Beteiligten variiert wird, **THEN** werden genau 2 (Zärtlichkeit, Sex, Harem), 3 bis N_gruppe_max (Gruppe) und 2 bis N_gruppe_max (Ritual) akzeptiert; ob ein Beteiligter Haremshaupt ist, ändert das Ergebnis nie (Vergleich gleicher Anfragen).
- **AC-04 [Logic/EditMode]** **GIVEN** ein Beteiligter ist gesperrt und liegt zugleich unter der Schwelle, **WHEN** geprüft wird, **THEN** ist der Grund die Sperre.

### Schnittstelle (nicht verhandelbar)
- **AC-05 [Logic/EditMode]** **GIVEN** die Typen `Anfrage` und `Beteiligter` und die Signatur von `Prüfe`, **WHEN** sie per Reflection geprüft werden, **THEN** ist die Feldmenge genau die Liste aus Core Rule 4; es gibt kein Feld mit Namen oder Typ Hingabe, Rolle, Rang, Gotteskraft, Eingriff, Spielerwahl, Ruf, Haremshaupt-Status oder Gefangenenhalter, und die Parameter sind genau `Anfrage` und `Datentabelle`, ohne `object`, `dynamic` oder Wörterbücher mit Zeichenketten-Schlüsseln; `Anfrage`, `Zustimmungsprofil` und `Datentabelle` haben keinen öffentlichen Konstruktor.
- **AC-06 [Logic/EditMode]** **GIVEN** die Assembly des Zustimmungsmodells, **WHEN** ihre Referenzen gelesen werden, **THEN** verweist sie weder auf Gotteskraft, Gotteshand noch auf die Innereien von Menschen & Bevölkerung, nur auf die Assembly des Zustimmungsprofils; ein zusätzlicher Verweis lässt den Test scheitern. Geprüft wird die `.asmdef`-Datei selbst (auch Verweise per GUID), nicht `GetReferencedAssemblies`: Modell und Profil haben `noEngineReferences: true`, alle Aufrufer `allowUnsafeCode: false`.
- **AC-25 Durchsetzung [Logic/EditMode]** **GIVEN** das Handlungsregister und Stub-Handlungsarten (jetzt, nicht erst wenn Begehren & Lust existiert), **WHEN** `Beginne` aufgerufen wird mit (a) `default(Zustimmungsergebnis)`, (b) einem Ergebnis aus einem früheren Schritt, (c) einem Ergebnis für eine andere Handlungsart (Zärtlichkeit statt Sex), (d) einem Ergebnis für andere Beteiligte (A–B statt A–C), (e) einem `Halteergebnis`, **THEN** lehnt es in allen fünf Fällen ab und beginnt nichts; nur ein Zustimmungsergebnis aus diesem Schritt für genau diese Handlung beginnt sie; die Typen sind `ref struct` ohne öffentlichen Konstruktor, und das Register hat nach außen nur `Beginne` und `Beende`.
- **AC-07 [Logic/EditMode]** **GIVEN** das Zustimmungsprofil, **WHEN** es geprüft wird, **THEN** enthält es genau Frömmigkeit, Lust, Treue, Mut, Geselligkeit, Geschlecht, Freiheit, Gesundheit, im Kampf und Stammes-ID (Menschen & Bevölkerung, Core Rule 18), nie Hingabe.

### Reinheit
- **AC-08 [Logic/EditMode]** **GIVEN** 10.000 Anfragen, **WHEN** sie in drei Reihenfolgen (vorwärts, rückwärts, gemischt) je zweimal geprüft werden, mit anderen Aufrufen dazwischen, **THEN** ist jedes Ergebnis bitgleich, einschließlich der Gründe und der Reihenfolge der Ablehnenden.
- **AC-09 [Logic/EditMode]** **GIVEN** die Assembly, **WHEN** sie per Reflection durchsucht wird, **THEN** gibt es keine statischen Felder außer Konstanten und `static readonly` von einfachen Typen, Zeichenketten und Aufzählungen (vom Compiler erzeugte Typen ausgenommen), und eine IL-Prüfung aller Methoden der Assembly (auch Hilfsmethoden und `PrüfeSperren`) findet keinen Aufruf von `System.Random`, `UnityEngine.Random`, `DateTime`, `Time`, `Application`, Dateizugriffen oder `Debug`.

### Monotonie und Einstimmigkeit
- **AC-10 [Logic/EditMode]** **GIVEN** 100.000 zufällige Anfragen ohne Sperre, **WHEN** Begehren, Lust, Norm, Geselligkeit oder Frömmigkeit eines Beteiligten erhöht wird und alles andere gleich bleibt, **THEN** wird eine Zustimmung nie zur Ablehnung und w sinkt nie (Toleranz 1e-9); höhere Treue bei Bindung nach außen (k = 1) oder ein öffentlicherer Ort erhöhen w nie.
- **AC-11 [Logic/EditMode]** **GIVEN** 100.000 Anfragen, **WHEN** für jeden Beteiligten w ≥ θ einzeln berechnet wird, **THEN** stimmt die Anfrage genau dann zu, wenn alle zustimmen; ein einzelner Ablehnender sperrt sie auch bei Begehren 1 und Norm 1 aller anderen, und jeder Ablehnende wird genau einmal genannt.

### Formeln
- **AC-12 Formel 1 und 4 [Logic/EditMode]** **GIVEN** Sex zu zweit auf einem Fest mit b = 0,6, L = 0,5, n = 0,5, G = 0,4, k = 1, T = 0,7 und der Standardtabelle, **WHEN** `Bereitschaft` und `Prüfe` laufen, **THEN** ist w = 0,175 ± 1e-6, die Anfrage wird gegen θ = 0,48 abgelehnt, der Hauptgrund ist „treu gebunden“, und s_D = 0,18, s_L = 0,15, s_N = 0,125, s_pub = 0,09, s_bind = 0,28 (je ± 1e-6).
- **AC-13 Formel 2 [Logic/EditMode]** **GIVEN** eine Testtabelle, deren Schwelle genau auf das berechnete w gesetzt ist, **WHEN** geprüft wird, **THEN** wird zugestimmt (≥), und bei θ = w + 1e-6 wird abgelehnt.
- **AC-14 Formel 1, Gruppe [Logic/EditMode]** **GIVEN** μ = 0,5 und Begehren 0,2 / 0,8 / 0,5 eines Beteiligten für die anderen, **WHEN** D berechnet wird, **THEN** ist D = 0,5 · 0,2 + 0,5 · 0,5 = 0,35 ± 1e-6; ein Ritual rechnet D wie die Gruppe (bei 2 Beteiligten einfach b_ij).
- **AC-15 Formel 1, Grenzen [Logic/EditMode]** **GIVEN** 100.000 zufällige Anfragen, **WHEN** w berechnet wird, **THEN** gilt immer 0 ≤ w ≤ 1, und ein roher Wert unter 0 ergibt 0.
- **AC-16 Formel 3 [Logic/EditMode]** **GIVEN** θ = 0,48 und Δ_hold = 0,08, **WHEN** w = 0,46 bei Phase Beginn und dann 0,43 und 0,39 bei Phase Halten ist, **THEN** wird der Beginn abgelehnt, die laufende Handlung bei 0,43 fortgesetzt und bei 0,39 mit Hauptgrund beendet.
- **AC-17 Formel 4 [Logic/EditMode]** **GIVEN** zwei gleich große Beiträge (Differenz unter 1e-9), **WHEN** der Hauptgrund gewählt wird, **THEN** gewinnt der frühere in der Liste (Bindung, Begehren, Öffentlichkeit, Lust, Norm, Geselligkeit, Frömmigkeit); in 10.000 Zufallsfällen hat der Hauptgrund immer den größten Beitrag.
- **AC-18 Edge Case Bindung [Logic/EditMode]** **GIVEN** ein Beteiligter ist an einen anderen Teilnehmer derselben Handlung gebunden, **WHEN** geprüft wird, **THEN** ist k = 0, und es gibt keine Bindungsstrafe.
- **AC-23 Formel 1, Begehren [Logic/EditMode]** **GIVEN** alle 5 Handlungsarten, Phase Beginn und Halten, Lust, Norm, Geselligkeit und Frömmigkeit 1, keine Bindung, privat, **WHEN** das kleinste Begehren eines Beteiligten 0 ist (bei Gruppe und Ritual: gegenüber einem von mehreren, die anderen 1), **THEN** ist sein w = 0, er lehnt ab, und der Grund ist „kein Begehren“; für 100.000 zufällige Anfragen der Phase Beginn gilt: liegt min_j b_ij unter θ[a] · b_ref (Defaults 0,12 / 0,144 / 0,156 / 0,165 / 0,135), ist sie abgelehnt, auch wenn alle anderen Werte 1 sind, und w sinkt nie, wenn ein Begehren wächst (Toleranz 1e-9).
- **AC-24 Formel 1, Beispiel 2 [Logic/EditMode]** **GIVEN** Sex zu zweit, privat, b = 0,1, L = 1, n = 1, k = 0, b_ref = 0,3, **WHEN** `Bereitschaft` und `Prüfe` laufen, **THEN** ist w = 0,198 ± 1e-3 (Rohwert 0,595, g = 0,333), die Anfrage wird abgelehnt, der Hauptgrund ist „kein Begehren“ mit s_D = 0,802 ± 1e-3; dieselbe Anfrage mit b = 0,3 hat g = 1, w = 0,685 und wird angenommen.

### Datentabelle
- **AC-19 [Logic/EditMode]** **GIVEN** je eine ungültige Tabelle (Gewichte ≠ 1 bei Toleranz 1e-6, Ritual mit W_D = 0, b_ref außerhalb 0,2–0,5, θ unter 0,10 oder über 0,90, θ_Zärtlichkeit > θ_Sex oder θ_Sex > θ_Gruppe, θ − Δ_hold < 0,20, Δ_hold > 0,15, c über 0,6), sowie negative Gewichte mit Summe 1, W_D + W_L < 0,40, W_N > 0,40, (θ − Δ_hold) · b_ref < 0,09, N_gruppe_max außerhalb 3–6, μ oder π außerhalb 0–1, Δ_hold < 0, NaN, eine fehlende Zeile, F_min außerhalb des Bereichs und `default(Datentabelle)` direkt an `Prüfe`, **WHEN** sie geladen wird und 1.000 Anfragen folgen, **THEN** wird jede mit „ungültige Eingabe“ abgelehnt, der Ladevorgang meldet den Fehler genau einmal, `Prüfe` meldet nichts, und die Standardtabelle wird akzeptiert.

### Pillar 3 (Monte Carlo)
- **AC-20 [Logic/EditMode]** **GIVEN** 20.000 zufällige Paare (fester Startwert; Eigenschaften wie Menschen & Bevölkerung Formel 5; Begehren normalverteilt mit 0,5 und 0,2, auf 0–1 geklemmt; privat; keine Bindung), **WHEN** Sex zu zweit bei Norm 0,9, 0,15 und 0,5 geprüft wird, **THEN** liegt der Anteil der Zustimmungen (mit b_ref = 0,3) bei 69 % ± 3 Punkte, 6,6 % ± 1 Punkt und 33 % ± 3 Punkte; dieselbe Prüfung ergibt für Zärtlichkeit 81 / 23 / 66 % (je ± 3), für Harem beitreten 59 / 0,1 / 7,4 % (± 3, höchstens 0,5 %, ± 1), für die Teilnahme an einem Ritual mit 2 Beteiligten und F_min = 0,30 63 / 5,3 / 45 % (± 3, ± 1, ± 3) und für eine Gruppe mit 3 Beteiligten 32 / unter 0,1 % / 2,2 % (± 3, höchstens 0,5 %, ± 1), je bei Norm 0,9 / 0,15 / 0,5; derselbe Lauf ohne Begehrens-Faktor ergibt für Sex zu zweit in der Lust-Sekte deutlich mehr (etwa 78 %), sodass ein fehlender Faktor auffällt.

### Gotteshand in einer Szene (nicht verhandelbar)
- **AC-21 [Integration/PlayMode]** **GIVEN** eine laufende Szene, **WHEN** die Gotteshand einen Beteiligten packt, **THEN** setzt der Griff zuerst die Sperre der Ansicht und blendet die Szene aus, bevor die Physik startet, die Simulation beendet die Handlung mit dem Grund „in Gottes Hand“ in dem Schritt, in dem der Kern den Griff anwendet, und ein Aufzeichner, der je gezeichnetem Bild „Szene sichtbar“ und „Ragdoll aktiv“ protokolliert, findet über 200 Griffe und 50 Blitze zu zufälligen Zeitpunkten (auch genau auf einer Schrittgrenze), bei 30, 60 und 144 fps und bei 1× und 4× kein Bild mit beidem.
- **AC-22 [Manual]** **GIVEN** dieselbe Situation im Build, **WHEN** gepackt wird, **THEN** liegt eine Bildfolge (60 fps, je ± 10 Bilder um 10 Griffe und 5 Blitze) in `production/qa/evidence/` mit Freigabe des Leads, und Szene und Ragdoll sind in keinem Bild gleichzeitig sichtbar.
- **AC-26 Sperren in jedem Schritt [Integration/EditMode, mit dem Simulationskern]** **GIVEN** eine laufende Handlung (Stub) mit 2 Beteiligten, **WHEN** in Schritt t eine Sperre aus jedem möglichen Platz der Reihenfolge entsteht (Eingriff, Menschen, Stämme, Begehren, Harem, Gotteskraft: verletzt, gefangen, im Kampf, in Gottes Hand, tot), **THEN** ist die Handlung nach Schritt t beendet (für gerade und ungerade IDs), der Grund ist die Sperre nach der Rangfolge aus Regel 13, und `PrüfeSperren` liefert für den Zustand nach Schritt t „gesperrt“. (Die Pflicht von Stämme & Diplomatie aus Regel 21 prüft deren GDD gegen den echten Auswahlweg.)
- **AC-28 Einseitiges Begehren [Logic/EditMode]** **GIVEN** Sex zu zweit, privat, alle anderen Werte 1, b_AB = 1 und b_BA = 0, **WHEN** geprüft wird, **THEN** stimmt A zu, B lehnt mit „kein Begehren“ ab, die Anfrage ist abgelehnt, und mit vertauschter Reihenfolge spiegelt sich das Ergebnis; Property-Test über 100.000 Fälle: eine Änderung von b_ji, n_j oder dem Profil von j (j ≠ i) lässt w_i bitgleich.
- **AC-29 Hauptgrund und „kulturell“ [Logic/EditMode]** **GIVEN** 100.000 zufällige abgelehnte Anfragen mit Strafen (öffentlich, Bindung), **WHEN** der Hauptgrund bestimmt wird, **THEN** ist er bei g_i < θ (Halten: θ − Δ_hold) immer „kein Begehren“, sonst der erste Term in Listenreihenfolge mit s ≥ max(s) − 1e-9 (gegen eine Referenzrechnung im Test); „kulturell“ ist genau dann gesetzt, wenn dieselbe Anfrage mit n = 1 zugestimmt hätte (Beispiel: Sex zu zweit, privat, b = 0,6, L = 0,4, n = 0,15 → w = 0,4275, abgelehnt; mit n = 1 wäre w = 0,64, also „kulturell“).
- **AC-30 Ritual [Logic/EditMode]** **GIVEN** b = 0,3, n = 0,9, F = 0,9, L = 0,5, **WHEN** die Teilnahme am Ritual und ein Kontakt zu zweit im Ritual geprüft werden, **THEN** wird die Teilnahme mit der Ritual-Zeile und der Kontakt als Sex zu zweit mit W_F = 0 und dessen Schwelle geprüft (w = 0,51 gegen 0,48 an einem privaten Ort, bei Zuschauern als öffentlich mit Strafe); eine Person mit F < F_min nimmt nie teil; der Ort „Ritual“ ist für jede andere Handlungsart „ungültige Eingabe“.
- **AC-31 Gewalt öffnet keine Zustimmung [Logic/EditMode, mit Stubs für Begehren & Lust und Menschen]** **GIVEN** ein Beteiligter lehnt mit „treu gebunden“ ab, **WHEN** sein Partner durch einen Blitz stirbt oder gefangen wird, **THEN** steigt seine Bereitschaft innerhalb von T_trauer nicht; **GIVEN** ein Mensch wechselt durch Eroberung den Stamm, **THEN** ist seine Norm T_akkult lang min(alte, neue), und ein Tabu einer der beiden sperrt; **GIVEN** ein Harem-Mitglied wird gefangen, **THEN** endet die Mitgliedschaft im selben Schritt, und nach der Befreiung entsteht sie nur durch eine neue Beitrittsanfrage.
- **AC-32 Erschüttert, Laden, Halten [Logic/EditMode]** **GIVEN** ein Mensch wird aufs Fest geworfen und unverletzt zurückgegeben, **WHEN** in den nächsten T_erschüttert Schritten eine Anfrage kommt, **THEN** wird sie mit „erschüttert“ abgelehnt, danach normal geprüft; **GIVEN** ein Spielstand mit einer laufenden Handlung, deren Bereitschaft unter der neuen Tabelle unter der Einwilligungslinie liegt, **WHEN** er geladen wird, **THEN** endet die Handlung im ersten Schritt; die Halteprüfung läuft im Personentakt jedes Beteiligten.
- **AC-27 Pflichten der Aufrufer [Logic/EditMode]** (Vorgabe; die GDDs von Begehren & Lust, Harem & Rituale und Kultur & Erinnerung übernehmen sie als eigene Kriterien) **GIVEN** 100.000 Zustände, **WHEN** Hingabe, Rolle, Rang, Gotteskraft, Eingriffe oder Kräfte beliebig geändert werden, **THEN** sind b_ij und k_i bitgleich, keine Norm ändert sich um mehr als die Höchständerung pro Geschichtsjahr, und kein einzelner Eingriff setzt eine Norm.

## Open Questions

| Frage | Wer klärt | Bis wann |
|---|---|---|
| Welche Normen (Offenheit, Tabu) hat eine Kultur je Handlungsart und Ort, und wie verändern sie sich? | beantwortet: Kultur & Erinnerung Regeln 14–17, Formel 6 | 2026-10-26 |
| Wie entsteht der Begehren-Wert b_ij, und welche Vorlieben gibt es? Die Verteilung in Formel 5 ist bis dahin angenommen | GDD Begehren & Lust | mit dem GDD |
| Darf die Gotteshand Beteiligte einer laufenden Szene überhaupt packen, oder sind sie dann nicht greifbar? Vorerst gilt: Packen ist erlaubt und beendet die Szene (Edge Cases) | GDD Gotteshand & Ragdoll | mit dem GDD |
| Stimmen die geschätzten Anteile aus Formel 5 nach der Umsetzung? | Monte-Carlo-Test AC-20 | erste Umsetzung im MVP-0 |
| Wie viele Beteiligte hat ein Ritual mit Kontakt, und was geschieht dabei? Vorerst 2 bis N_gruppe_max, paarweise geprüft | GDD Harem & Rituale | mit dem GDD |
| Wie schnell dürfen sich Normen ändern (Höchständerung pro Geschichtsjahr), damit kein Eingriff Zustimmung im Moment erzwingt? | beantwortet: Kultur & Erinnerung Regeln 14–17, Formel 6 | 2026-10-26 |
| Passen die Vorschläge der Nachtarbeit: F_min 0,30, T_trauer 5 Lebensjahre, T_akkult 60 Geschichtsjahre, T_erschüttert 1 Lebensjahr? | Entwickler | nächste Sitzung |
| Soll ein Blitz auf ein Mitglied die Offenheit seines Stamms senken, damit Auslese durch Töten („Keusche wegblitzen, Neue erschaffen“) nicht wirkt? | GDD Kultur & Erinnerung | mit dem GDD |
| Zählt im Harem die Bindung an das Haremshaupt als k? Ohne Klärung kommt entweder der Status des Haupts durch die Hintertür, oder jeder Harem hebt sich selbst auf | GDD Harem & Rituale | mit dem GDD |
| Braucht Freiheit schon jetzt einen dritten Wert „unfrei“ (Sklaverei) im Zustimmungsprofil, oder reicht die Regel „alles außer frei sperrt“, bis eine solche Gesellschaft kommt? | Entwickler, GDD Menschen & Bevölkerung | vor der Alpha (Weitere Gesellschaftsformen) |
| Soll die Anzeige „kein Begehren“ (g < 1) und „zu wenig Begehren“ unterscheiden? Darf jemand mit Lust 0 bei vollem Begehren zustimmen (heute ja)? | Entwickler | vor dem UX-Spec |
| Die Umschreibungen des `qa-lead` für AC-03, AC-13, AC-14, AC-16, AC-18 und AC-27 sowie ein IL2CPP-Test der Schwellen im fertigen Build | qa-lead | vor `/create-stories` |
