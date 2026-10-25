<!-- PROTOTYPE - NOT FOR PRODUCTION -->
<!-- Question: Does an unattended tribe simulation produce histories worth watching, and can the player trace culture changes back to their own interventions? -->
<!-- Date: 2026-10-25 -->

# Concept Prototype Report: Weltengott (Allmacht, Prototyp 1)

> **Date**: 2026-10-25
> **Prototype Path**: HTML
> **Concept File**: design/gdd/game-concept.md

---

## Hypothesis

Wenn der Spieler zwei Stämmen auf einer Insel über 10 Ären zusieht und etwa 5 Mal
mit zwei Kräften eingreift, hat er das Gefühl, eine sehenswerte Kultur geformt zu
haben. Belegt, wenn:

1. die Welt ohne Eingriffe in jeder Ära mindestens ein großes Ereignis erzeugt und
   in 10 Ären weder ausstirbt noch erstarrt;
2. sich mindestens 2 Kulturveränderungen in der Chronik auf die Tat des Spielers
   zurückführen lassen;
3. der Spieler danach eine neue Welt starten will.

---

## Riskiest Assumption Tested

Eine unbeaufsichtigte Simulation erzeugt von selbst interessante Geschichten
(Abbruchkriterium des Technical Directors für Prototyp 1).

**Ergebnis: teilweise belegt.** Die Welt lebt von selbst. Sie stirbt nie aus, wird
nie früh von einem Stamm beherrscht, und es gibt im Schnitt 4,4 große Ereignisse
pro Ära. Erzählerisch trägt sie aber noch nicht: Der Spieler fand das Spiel „zu
flach“, die Chronik eintönig (Krieg, Frieden, Krieg), und Kriege sind etwa viermal
so häufig wie Kulturwandel.

---

## Approach

Eine einzelne HTML-Datei mit derselben Simulation für den Browser und für
automatische Auswertungen in Node. Bau und Abstimmung innerhalb einer Sitzung am
2026-10-25.

**Path chosen:** HTML
**Reason for path:** Getestet werden Regeln und entstehende Geschichten, nicht das
Spielgefühl. HTML läuft per Doppelklick ohne Unity-Kenntnisse und erlaubt
automatische Läufe über 100 Welten.

**Shortcuts taken (intentional):**
- Punkte und Kästchen statt Grafik, keine Menschenmodelle, kein 3D, kein Ragdoll
- Nur 2 Kräfte (Blitz, Fest-Segen), nur eine Kulturachse (Gottesstaat ↔ Lust-Sekte)
- Kein Besessen-Sein, kein Harem, keine Sklaverei, keine sexuellen Inhalte (Feste nur als Text)
- Kein Speichern, keine Menüs, kein Sound, fest eingetragene Werte
- Bevölkerung nur aus Erwachsenen, die der Gott erschafft: eine passive Gnade aus
  Ritualen plus Fest-Segen

Abstimmung vor dem Playtest: Der erste Lauf hatte keine Kriege, unbegrenztes
Wachstum und alle Kulturen drifteten zur Lust-Sekte. Behoben durch begrenztes
Land, ein eigenes Temperament pro Stamm, Abspaltungen von Andersdenkenden und eine
niedrigere Kriegsschwelle.

---

## Result

**Automatische Auswertung** (100 Welten × 10 Ären ohne Eingriff; der Spieler hat
den Lauf auf dem eigenen PC wiederholt, die Werte sind identisch):

- 0 % Welten sterben aus; 0 % nach Ära V nur noch ein Stamm
- 4,4 große Ereignisse pro Ära; im Schnitt 5,0 Stämme am Ende
- 44 % der Welten haben mindestens eine Ära ohne großes Ereignis (60 von 1000 Ären,
  29 davon in Ära I) → **56 %** erfüllen Prüfpunkt 1 vollständig
- Ereignisarten: Krieg 1620 · Abspaltung 1008 · Eroberung 838 · Frieden 500 · Kulturwandel 393 · Untergang 1
- Programmierter Testspieler (etwa 4 Eingriffe): In 89 von 100 Welten führt die
  Chronik mindestens 2 große Ereignisse auf ihn zurück

**Playtest** (1 Spieler, der Entwickler selbst):

- Hypothese laut Spieler: **BESTÄTIGT**
- „Habe mehrmals Blitz gemacht, der Stamm wurde dadurch sehr schwach. Bei einem
  anderen Stamm habe ich mehrmals Segen gemacht, und dieser lebt immer noch, ist
  also deutlich stärker geworden.“
- Ein Stamm wechselte nach den Eingriffen seine Gesellschaftsform, und die Chronik
  führte das blau mit „weil du …“ auf den Spieler zurück (Prüfpunkt 2: ja).
- Der Spieler wollte danach eine neue Welt starten (Prüfpunkt 3: ja).
- **Bester Moment:** keiner im Sinn von „das funktioniert“, denn „dafür ist das
  Spiel zu flach“. Was ihn freut: „dass der Stamm, den ich die ganze Zeit segne,
  immer noch lebt“. Es ist also eine Bindung an „seinen“ Stamm entstanden.
- **Schlechtester Moment:** Alles Folgende trifft zu: nichts Sichtbares (nur Punkte
  und Kästchen), zu wenige Möglichkeiten (2 Kräfte), eintönige Chronik
  (Krieg, Frieden, Krieg), zu viel Warten und zu wenig Eingreifen.
- **Überraschung:** Ein Stamm starb trotz Segen plötzlich sehr schnell aus. Die
  Ursache ist nicht überprüft. Möglich wäre, dass strenggläubige Nachbarn den Segen
  als Frevel sahen, Groll aufbauten und den Stamm angriffen. Für den Spieler war der
  Grund nicht sichtbar.

---

## Metrics

| Metric | Value |
|--------|-------|
| Path used | HTML |
| Iterations to playable | N/A |
| Prototype duration | eine Sitzung (Bau, Abstimmung und Playtest am 2026-10-25) |
| Playtesters | 1 intern (der Entwickler), 0 extern |
| Feel assessment | „zu flach“: keine sichtbaren Folgen, 2 Kräfte zu wenig, eintönige Chronik, zu viel Warten |
| Hypothesis verdict | CONFIRMED (Urteil des Spielers); Prüfpunkt 1 messbar nur in 56 % der Welten erfüllt |

---

## Recommendation: PROCEED

Das ist die Entscheidung des Spielers. Alle drei Prüfpunkte wurden erlebt: Die Welt
lebt von selbst, Eingriffe ändern Stärke und Gesellschaftsform, die Chronik führt
das auf den Spieler zurück, und er wollte eine neue Welt starten. Es entstand eine
Bindung an den gesegneten Stamm. **Einschränkung:** Spaß ist nicht belegt. Es gab
keinen „das funktioniert“-Moment, und das Erlebnis war „zu flach“. PROCEED heißt
hier, dass die Simulation als Fundament trägt, nicht, dass das Spiel schon Spaß
macht. Die festgestellten Schwächen werden zu Designanforderungen an die GDDs
(siehe unten). Das Spielgefühl selbst (Gotteshand, Ragdoll, sichtbare Folgen) ist
ungetestet und bleibt Prototyp 2.

---

## If Proceeding

- **Core tuning values discovered:**
  - Begrenztes Land: Kapazität pro Feld ≈ 0,55 × Fruchtbarkeit. Ohne Begrenzung
    wächst alles unbegrenzt, und es gibt keine Konflikte.
  - Bei passiver Gnade von ~1–4 % pro Jahr (nach Hingabe skaliert) stirbt keine
    Welt aus.
  - Gotteskraft aus Ritualen ≈ 0,0025 × Hingabe × Bevölkerung pro Jahr erlaubt
    etwa 4 Eingriffe in 10 Ären. Laut Playtest ist das zu wenig.
  - Ohne eigenes Temperament pro Stamm und ohne Abspaltungen von Andersdenkenden
    landen alle Kulturen an einem Pol.
- **Assumptions confirmed:**
  - Eine Simulation auf Stammesebene erzeugt von selbst laufend Ereignisse.
  - Ein Kulturgedächtnis mit Verfall macht Ursache und Wirkung nachvollziehbar.
  - Der „Gott erschafft Erwachsene“-Kreislauf hält Welten am Leben, ganz ohne
    Kinder.
  - Bindung an einen bevorzugten Stamm entsteht schon ohne Grafik. Das stützt die
    Erwählten.
- **Assumptions disproved:**
  - „Zusehen plus wenige seltene Eingriffe“ reicht nicht. Der Spieler will öfter
    und vielfältiger eingreifen. Das stützt den Pillar „sichtbare Folgen“ und zeigt,
    dass der Core Loop dichter sein muss.
  - Krieg als häufigstes Ereignis wird eintönig. Die Ereignisarten brauchen mehr
    Vielfalt und mehr Gewicht auf Kulturwandel.
  - Ära I ist zu ruhig, weil nur 2 Stämme da sind.
- **Emergent mechanics:**
  - Wenn Gegner einen Segen als Frevel sehen, wird der begünstigte Stamm zur
    Zielscheibe. Das ist eine gute Quelle für Geschichten, muss aber sichtbar
    erklärt werden.
  - Abspaltungen von Andersdenkenden erzeugen Glaubenskriege von selbst.

> HTML wurde verwendet, und das Spielgefühl ist ungetestet. Prototyp 2 (Gotteshand
> und Ragdoll in Unity) sollte vor oder parallel zu den ersten GDDs laufen.

**Next steps:**
1. `/design-review design/gdd/game-concept.md`
2. `/gate-check`
3. `/map-systems`
4. `/design-system [mechanic]` (use learnings in Tuning Knobs and Formulas sections)

---

## If Pivoting

Nicht zutreffend (Urteil: PROCEED).

---

## If Killing

Nicht zutreffend (Urteil: PROCEED).

---

## Lessons Learned

- **What assumptions were broken by actually building this?**
  Passives Zusehen trägt allein nicht. Ohne sichtbare Folgen und mit nur 2 Kräften
  wirkt selbst eine aktive Welt flach.
- **What surprised us that didn't show up in the brainstorm?**
  Ein Segen kann einen Stamm gefährden, weil Nachbarn ihn als Frevel sehen. Außerdem
  dominiert Krieg die Ereignisse, sobald Land knapp ist.
- **What would we test differently next time?**
  Mehr Kräfte, damit es mehr Eingriffe gibt. Ereignisse, die man auf der Karte sieht,
  und eine Chronik, die erklärt, *warum* ein Stamm untergeht. Und wenn möglich einen
  zweiten Spieler, der das Spiel nicht kennt.

---

> *Prototype code location: `prototypes/weltengott-concept/`*
> *This code is throwaway. Never refactor into production.*
