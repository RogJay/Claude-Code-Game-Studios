# Menschen & Bevölkerung

> **Status**: In Design
> **Author**: Entwickler + Claude
> **Last Updated**: 2026-10-25
> **Last Verified**: 2026-10-25
> **Implements Pillar**: Pillar 1 (Die Welt lebt ohne dich), Pillar 3 (Lust ist Kultur, nicht Beiwerk)

## Summary

Menschen & Bevölkerung legt fest, wer auf der Insel lebt: ein Volk pro Stamm als
Kopfzahl wie in Prototyp 1, und daraus 30–50 benannte Einzelne mit Geschlecht, fünf
Eigenschaften, einem Leben von etwa 4 Minuten und einem Zustand. Es gibt nur
Erwachsene, Zuwachs kommt allein aus der Gnade und der Kraft „Erwachsene erschaffen“,
und das Zustimmungsprofil enthält nie Hingabe oder Rang. Die Einzelnen sind die
Gesichter, an denen Lust, Kultur und die Gotteshand sichtbar werden.

> **Quick reference** — Layer: `Core` · Priority: `MVP` · Key deps: `Simulationskern, Welt & Insel`

## Overview

Menschen & Bevölkerung bestimmt, wer auf der Insel lebt. Jeder Stamm hat ein
**Volk**, eine Kopfzahl wie in Prototyp 1, mit der Gnade, Hunger, Krieg und
Abspaltung in der Geschichtszeit rechnen. Aus diesem Volk treten 30–50 **Einzelne**
hervor: benannte Erwachsene mit Geschlecht, fünf Eigenschaften, einem Alter in
Lebensjahren und einem Zustand. Nur sie handeln als Personen. Sie lieben, feiern,
werden gepackt und geworfen und gehören zum Harem. Die übrige Masse erscheint in der
3D-Welt als Statisten ohne feste Identität. Fehlt ein Einzelner, tritt jemand aus der
Masse hervor. Das geschieht auch, wenn die Gotteshand einen Statisten packt. Es gibt
nur Erwachsene: Niemand wird geboren, die Bevölkerung wächst über die passive Gnade
aus den Ritualen und über die Kraft „Erwachsene erschaffen“. Dieses System liefert
die Daten, aus denen das Zustimmungsmodell, Begehren & Lust, Harem & Rituale und die
Gotteshand arbeiten. Ohne es gäbe es niemanden, an dem sich Kultur, Lust und Eingriffe
zeigen.

## Detailed Design

### Core Rules

*`systems-designer`, `gameplay-programmer` and `engine-programmer` not consulted for this section — Lean mode. Review manually before production.*

**Volk und Einzelne** (Entscheidung des Entwicklers vom 2026-10-25: „Masse plus Einzelne“)

1. Jeder lebende Stamm hat ein **Volk** V_t, eine ganze Zahl ≥ 0. Die Einzelnen dieses
   Stamms sind darin mitgezählt. Eine neue Welt beginnt mit 2 Stämmen zu je 48 Köpfen
   wie Prototyp 1.
2. Ein Stamm mit weniger als 3 Köpfen ist ausgestorben (wie Prototyp 1 und Welt & Insel
   Regel 8). Das Aussterben erklärt und meldet Stämme & Diplomatie, dieses System setzt
   nur die Zahl. Bis das GDD Stämme & Diplomatie anderes festlegt, verschwinden die
   letzten 1–2 Menschen wie im Prototyp (V_t = 0); offen in Welt & Insel, Open Questions.
3. Die **Einzelnen** sind benannte Menschen, insgesamt zwischen 0 und N_max. Ziel ist
   N_target (Formulas). Jeder Einzelne gehört zu genau einem Stamm. Der Rest des Volks
   (V_t minus die Einzelnen des Stamms) ist die **Masse**: Sie hat keine Eigenschaften,
   kein Alter und keinen Ort, nur ihre Zahl.

**Daten eines Einzelnen**

4. Jeder Einzelne hat:
   - eine **ID**, die steigt und nie wiederverwendet wird
   - einen **Namen** aus der Namensliste seines Stamms (Datendatei), eindeutig unter
     allen Lebenden aller Stämme (Formel 5)
   - ein **Geschlecht**: Frau oder Mann (Entscheidung vom 2026-10-25)
   - fünf **Eigenschaften** zwischen 0 und 1, beim Hervortreten gewürfelt und danach
     unveränderlich: **Frömmigkeit, Lust, Treue, Mut, Geselligkeit**
   - wechselnde **Zustände**:
     - **Hingabe** an den Gott (0–1)
     - **Gesundheit** (gesund, verletzt mit Verletzungsklasse und dem Schritt der
       Heilung, tot)
     - **Freiheit** (frei oder gefangen; nur „frei“ lässt das Zustimmungsmodell zu, jeder
       spätere Wert wie „unfrei“ sperrt dort automatisch)
     - **im Kampf** (ja oder nein): gesetzt und gelöscht durch Aufträge (Regel 23) von Stämme &
       Diplomatie, wenn ein Einzelner an einem Kampf teilnimmt
     - **„in Gottes Hand“** (vom Simulationskern)
   - einen **Ort**, eine Zelle der Insel
   - den Schritt des Hervortretens t_birth sowie held_closed und t_hold_start für die
     Zeit in Gottes Hand (Formel `lebensjahr` im Simulationskern; gesetzt, wenn der Kern
     Beginn und Ende anwendet)
   - eine persönliche **Lebensdauer** LD_i in Lebensjahren (Formulas)
5. **Rollen** wie Harem-Mitglied, Priesterin oder Anführer gehören den Systemen, die
   sie vergeben. Sie verweisen auf die ID des Einzelnen. Dieses System speichert sie
   nicht selbst.
6. Begehren & Lust darf weitere Vorlieben an einen Einzelnen hängen, zum Beispiel wen
   er begehrt. Diese Vorlieben bleiben Eigentum von Begehren & Lust.

**Nur Erwachsene** (nicht verhandelbare Grenze)

7. Es gibt keine Geburt, keine Kinder und keinen Zustand „Kind“. Ein Einzelner ist
   beim Hervortreten erwachsen. Angezeigt wird sein Alter als 18 plus seine
   Lebensjahre. Neue Menschen entstehen nur auf vier Wegen:
   - beim Erzeugen einer neuen Welt (2 Stämme zu je 48; danach steigen sofort
     N_target_eff Einzelne auf, 20 je Stamm, mit gestreutem Alter, Formel 7)
   - als Zuwachs des Volks durch die passive Gnade
   - als Einzelne durch Aufstieg aus der Masse
   - als neue Einzelne durch die Kraft „Erwachsene erschaffen“

   Kein anderes System darf einen Menschen erzeugen.

**Zuwachs: die passive Gnade**

8. Einmal pro Geschichtsjahr (Takt des Simulationskerns) wächst jedes Volk über die
   Gnade, die der Gott aus den Ritualen schenkt. Die Formel ist die aus Prototyp 1
   (Formulas): Sie wirkt nur, wenn der Stamm Nahrung hat und nicht über seiner
   Tragfähigkeit liegt, und sie steigt mit der Hingabe des Stamms. Die Gnade erzeugt
   Masse, keine Einzelnen.

**Aufstieg aus der Masse**

9. Liegt die Zahl der Einzelnen unter N_target_eff, tritt pro Geschichtsjahr höchstens ein
   Mensch aus der Masse hervor. Er kommt aus dem Stamm, dessen Anteil an Einzelnen am
   stärksten unter seinem Anteil am Volk liegt (Formulas). Er bekommt Geschlecht,
   Eigenschaften, Namen und Lebensdauer gewürfelt und einen Ort (eine Zelle im Gebiet seines
   Stamms, als letzter Wurf).
10. Packt die Gotteshand einen Statisten, tritt dieser sofort hervor, auch über
    N_target hinaus, aber nie über N_max. Ist N_max erreicht, lässt sich kein Statist
    packen, nur Einzelne (Edge Cases). Ein Statist hat in der Simulation keine ID: Die
    Ansicht meldet den Griff mit Stamm und Zelle, der Kern gibt ihn als Auftrag weiter
    (Regel 23), und der Statist tritt mit dieser Zelle als Ort hervor, bevor der Kern
    ihn „in Gottes Hand“ setzt. Seine ID meldet das Ereignis „Hervortreten“ mit dem
    Eingriff als Ursache.
11. Die Kraft „Erwachsene erschaffen“ erhöht das Volk und erzeugt dieselbe Zahl neuer
    Einzelner, bis N_max. Darüber hinaus wächst nur die Masse. Wie viele, legt das GDD
    Gotteskraft & Götterkräfte fest.

**Tod**

12. Einzelne sterben an **Alter**: Erreicht ihr Alter die persönliche Lebensdauer LD_i,
    sterben sie in ihrem nächsten Personentakt. Die Masse altert nicht, wie in
    Prototyp 1. Auch dieser Tod senkt das Volk ihres Stamms um 1 (Regel 15).
13. **Verluste des Volks** durch Hunger, Krieg oder Katastrophen setzen andere Systeme
    als Zahl fest (ein Auftrag, Regel 23), etwa „Stamm A verliert 5“. Dieses System wählt die Opfer: Jedes
    Opfer wird aus dem ganzen Volk gezogen, Einzelne mit dem geringeren Gewicht w_E
    (Formulas). Damit sterben Lieblingsfiguren seltener, aber nicht nie. Einzelne „in
    Gottes Hand“ und Beteiligte einer laufenden intimen Handlung (Zustimmungsmodell
    Regel 21) werden nie gezogen, für sie wird ein anderes Opfer gezogen.
14. **Direkter Tod** eines Einzelnen (Blitz auf eine Person, tödlicher Aufprall nach
    einem Wurf) senkt das Volk seines Stamms um 1.
15. Ein toter Einzelner wird entfernt, seine ID wird nie wieder vergeben. **Jeder Tod
    eines Einzelnen** senkt das Volk seines Stamms um 1, ob er an Alter, durch die
    Opferwahl oder durch einen direkten Tod stirbt; ein Toter kehrt nie in die Masse
    zurück. Das Ereignis „Tod“ nennt ID, Name, Stamm und Ursache (die Ursache-ID aus
    dem Ereignis, das ihn getötet hat).

**Hingabe**

16. Jeder Stamm hat eine **Stammeshingabe** D_t (0–1). Sie gehört Kultur & Erinnerung
    (vorläufiger Vertrag, das GDD fehlt noch). Die Gnade und die Rituale rechnen damit
    wie in Prototyp 1.
17. Die persönliche **Hingabe** eines Einzelnen folgt der Stammeshingabe und seiner
    Frömmigkeit (Formulas). Eingriffe des Gottes, die ihn persönlich treffen (Segen,
    Wurf, Verletzung), verändern sie über Ereignisse. Die Höhe legen die GDDs der
    Eingriffe fest.

**Was die Zustimmung lesen darf** (nicht verhandelbare Grenze)

18. Für das Zustimmungsmodell stellt dieses System ein **Zustimmungsprofil** bereit.
    Es enthält **nur**:
    - die fünf Eigenschaften
    - das Geschlecht
    - die Freiheit (frei oder gefangen)
    - die Gesundheit
    - ob der Einzelne gerade im Kampf ist (Merkmal „im Kampf“); ein Verletzungszustand
      steht schon in der Gesundheit
    - die Stammes-ID, über die das Zustimmungsmodell die Normen der Kultur abfragt

    **Hingabe, Rollen, Rang, Gotteskraft und Eingriffe sind nicht im Profil** und
    können es nicht verändern („Hingabe ist keine Zustimmung“). Die Masse hat kein
    Profil und nimmt an keiner sexuellen Handlung teil.

**Freiheit**

19. Ein Einzelner ist **frei** oder **gefangen**. Wer gefangen genommen und wer
    befreit wird, entscheidet Stämme & Diplomatie. Dieses System hält nur den
    Zustand. Gefangene zählen zum Volk des Stamms, der sie hält, wandern im Gebiet
    dieses Stamms und bekommen von Begehren & Lust und Harem & Rituale kein Ziel. Für
    alles Sexuelle sind sie ausgeschlossen (über das Zustimmungsprofil). Sie heilen wie
    alle anderen.

**Abspaltung und Eroberung**

20. Spaltet sich ein Stamm ab oder geht in einem Sieger auf, legt Stämme & Diplomatie
    fest, wie viele Köpfe wechseln. Die Einzelnen werden nach dem Verfahren des
    größten Rests im Verhältnis der neuen Volkszahlen verteilt (Edge Cases). Wer
    wechselt, wird mit dem Zufallsstrom dieses Systems gezogen; Einzelne „in Gottes
    Hand“ werden dabei nie gezogen und bleiben in ihrem Stamm. Jeder Einzelne merkt sich
    Stamm und Schritt seines letzten Wechsels (auch durch Gefangennahme); das
    Zustimmungsmodell nimmt danach eine Zeit lang die strengere Norm beider Kulturen.

**Ort und Bewegung**

21. Jeder Einzelne steht in einer Zelle der Insel (Welt & Insel). Ohne Auftrag
    wandert er im Gebiet seines Stamms. Andere Systeme können ihm ein Ziel geben, zum
    Beispiel ein Fest (Begehren & Lust) oder ein Ritual (Harem & Rituale). Dann bewegt
    er sich im Personentakt Zelle für Zelle dorthin (Formulas). Steht er nach einem
    Stammeswechsel oder einer Gebietsänderung außerhalb des Gebiets seines Stamms,
    geht er ohne anderes Ziel zur nächsten Zelle seines Gebiets (Formel 9), statt
    stehen zu bleiben.
22. Wie Einzelne in 3D laufen, ist Darstellung (Weltansicht und Körper & Animation).
    Wo die Masse in 3D steht, entscheidet die Weltansicht frei im Gebiet des Stamms.

**Aufträge anderer Systeme**

23. Verlust, Abspaltung, Eroberung, Gefangennahme, Befreiung, Verletzung, das Merkmal
    „im Kampf“, „Erwachsene erschaffen“ und vom Simulationskern das Hervortreten eines
    gepackten Statisten (Stamm, Zelle) sowie die Rückgabe aus Gottes Hand (Ort,
    Ergebnis, Verletzungsklasse; beim Beginn wird „im Kampf“ gelöscht) kommen als
    **Aufträge**: synchrone Aufrufe,
    die Menschen sofort ausführt, im Schritt des Auftraggebers, bevor dieser
    weiterrechnet (Entscheidung des Entwicklers vom 2026-10-25). Der Auftraggeber
    liest danach den neuen Zustand (V_t, E_t) noch im selben Schritt; so bleibt die
    Reihenfolge Krieg → Verlust → Abspaltung aus Prototyp 1 erhalten, und das
    Merkmal „im Kampf“, das alles Sexuelle sperrt, gilt ohne Verzögerung. Die Würfe
    der Opferwahl und der Auswahl der Wechselnden kommen beim Aufruf aus dem
    Zufallsstrom dieses Systems (system_id 2). Menschen ruft selbst nie ein anderes
    System auf. Ereignisse, auf die Menschen reagiert (etwa Eingriffe, die die
    Hingabe verändern), landen im Posteingang und werden im Jahresrhythmus
    verarbeitet (Formulas).

### States and Transitions

Zustände eines Einzelnen:

| Zustand | Bedeutung |
|---|---|
| **Masse** | noch kein Einzelner, nur Teil der Kopfzahl |
| **Frei** | lebt, entscheidet im Personentakt, kann an allem teilnehmen, was das Zustimmungsmodell erlaubt |
| **Verletzt** | lebt, Verletzungsklasse gesetzt; Kampf- und Verletzungszustände sperren alles Sexuelle |
| **Gefangen** | lebt, gehört zum Stamm, der ihn hält; von allem Sexuellen ausgeschlossen |
| **Im Kampf** (Merkmal) | nimmt an einem Kampf teil; sperrt alles Sexuelle, gesetzt und gelöscht von Stämme & Diplomatie |
| **In Gottes Hand** | gehalten oder im Flug; keine Entscheidungen, kein Altern, kein Tod (Simulationskern) |
| **Tot** | entfernt, ID gesperrt |

Masse, Frei und Tot schließen sich aus. Verletzt, Gefangen, Im Kampf und In Gottes Hand
sind **Merkmale** eines lebenden Einzelnen und können gleichzeitig gelten; der Zustand
eines Einzelnen ist also genau einer von Masse, Lebend oder Tot plus diese vier
Merkmale.

| Von | Nach | Auslöser |
|---|---|---|
| Masse | Frei | Aufstieg (Ziel unterschritten), „Erwachsene erschaffen“ |
| Masse | In Gottes Hand | Gotteshand packt einen Statisten: er tritt hervor und wird im selben Eingriff gehalten (Regel 10) |
| Frei | Verletzt | Ereignis mit Verletzungsklasse (Wurf, Blitz, Kampf) |
| Verletzt | Frei | Heilung nach der Dauer der Verletzungsklasse (GDD Gotteshand & Ragdoll) |
| Frei, Verletzt | Gefangen | Gefangennahme (Stämme & Diplomatie) |
| Gefangen | Frei | Befreiung oder Aufnahme in den Stamm (Stämme & Diplomatie) |
| Frei, Verletzt, Gefangen | In Gottes Hand | Ansicht meldet „Beginn in Gottes Hand“ |
| In Gottes Hand | vorheriger Zustand (oder Verletzt, oder Tot) | Ansicht meldet das Endereignis mit Ergebnis |
| beliebig außer In Gottes Hand | Tot | Alter, als Opfer eines Volksverlusts gezogen, direkter Tod |

### Interactions with Other Systems

| System | Was hineingeht | Was herauskommt | Wer besitzt die Schnittstelle |
|---|---|---|---|
| Simulationskern | Personentakt, Jahrestakt, Zufallsstrom (system_id 2), Status „in Gottes Hand“, Umrechnung Lebensjahr; Aufträge: Hervortreten eines gepackten Statisten, Rückgabe aus Gottes Hand | Ereignisse: Hervortreten, Tod | Kern |
| Welt & Insel | Zellen, Gebiet jedes Stamms, Nahrung und Tragfähigkeit pro Stamm, Startplätze; Auftrag „Verlust durch Hunger“ | Orte der Einzelnen, V_t | Welt & Insel (`design/gdd/welt-und-insel.md`, Entwurf) |
| Kultur & Erinnerung | Stammeshingabe D_t | – | Kultur & Erinnerung (vorläufig) |
| Stämme & Diplomatie | Aufträge (Regel 23): Verluste als Zahl, Abspaltung, Eroberung, Gefangennahme, Befreiung, Kampf-Merkmal | V_t, Einzelne pro Stamm | Stämme & Diplomatie gibt die Zahlen vor, Menschen wählt die Personen |
| Zustimmungsmodell | – | Zustimmungsprofil (Regel 18) | Menschen (Profilinhalt ist eine Grenze und darf nicht erweitert werden) |
| Begehren & Lust | Ziele (Fest), Vorlieben | Einzelne mit Eigenschaften, Geschlecht, Ort | Menschen |
| Harem & Rituale | Ziele (Ritual), Rollen per ID | Einzelne, Rituale zählen über D_t und V_t | Menschen |
| Gotteskraft & Götterkräfte | „Erwachsene erschaffen“ (Anzahl, Stamm), Blitz auf Person | neue Einzelne, Tod, persönliche Hingabe h_i lesend (Zufluss aus Hingabe) | Gotteskraft |
| Gotteshand & Ragdoll | Packen eines Statisten (Stamm, Zelle), Ergebnis mit Verletzungsklasse, beides über den Kern | ID des gepackten Einzelnen (Ereignis „Hervortreten“) | Gotteshand |
| Körper & Animation | – | Geschlecht, Eigenschaften (für Körperwahl), Alter für die Darstellung (immer erwachsen) | Menschen |
| Weltansicht & Kamera | – | Einzelne mit Ort, V_t pro Stamm für Statisten | Menschen |
| Chronik | – | Ereignisse Tod, Hervortreten, Gefangennahme mit Ursache | Kern-Ereignisformat |
| Speichern & Laden | kompletter Zustand | V_t, alle Einzelnen mit allen Feldern, Gnade-Akkumulatoren, Posteingang, nächste freie ID | Speichern & Laden |

## Formulas

Vorschlag des `systems-designer` (2026-10-25), im guided-Modus ohne Einzelfreigabe
übernommen; der Entwickler prüft ihn mit dem GDD. Alle Zufallszahlen kommen aus dem
Strom mit system_id 2. Ganzzahlen werden mit `NextInt(lo, hi)` gezogen, hi
ausgeschlossen. Für jeden neuen Einzelnen ist die Reihenfolge der Würfe fest:
Geschlecht, 15 Ziehungen für die Eigenschaften, Lebensdauer, beim Erzeugen der Welt
das Startalter (Formel 7), Name, Ort (`NextInt(0, Anzahl Zellen des Gebiets)` in der
Zellenreihenfolge von Welt & Insel).

**Jahrestakt:** Die Jahresregeln dieses Systems (Gnade, Aufstieg, Hingabe) laufen beim
Versatz O_year = 2, im selben Schritt wie Stämme & Diplomatie und vor ihnen
(Reihenfolge im Schritt laut Simulationskern). So sehen sie Nahrung (Welt, Versatz 0)
und Stammeshingabe (Kultur, Versatz 1) desselben Jahres wie in Prototyp 1. Die
Alters- und Bewegungsregeln laufen im Personentakt. Innerhalb eines Schritts rechnet
zuerst der Posteingang (Ereignisse, die die Hingabe verändern), dann die Jahresregeln
(Gnade, Aufstieg, Hingabe), dann der Personentakt (Altern und Tod, Bewegung).
Aufträge anderer Systeme (Regel 23) laufen sofort beim Aufruf.

### 1. Gnade (unverändert aus Prototyp 1, nur für das Volk)

The Gnade formula is defined as:

`G_t = V_t * (g_base + g_dev * D_t) * prosper` · `prosper = clamp(food_t / max(1, V_t), 0, 1)` · `acc_t = acc_t + G_t` · `born = floor(acc_t)` · `V_t = V_t + born` · `acc_t = acc_t - born`

Die Formel wirkt nur, wenn `food_t > 0` und `V_t * 100 < cap_t * 105` (g_cap = 1,05
als ganzzahliges Verhältnis, in double gerechnet). Sonst bleibt acc_t unverändert.

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| V_t | int | ≥ 0 | calculated | Volk des Stamms (Einzelne mitgezählt) |
| D_t | double | 0,05–1 | calculated | Stammeshingabe (Kultur & Erinnerung) |
| food_t | double | ≥ 0 (Deckel 3 · V_t) | calculated | Nahrung des Stamms (Welt & Insel) |
| cap_t | double | ≥ 0 | calculated | Tragfähigkeit mit Dürrefaktor (Welt & Insel) |
| g_base | float | 0,01 | constant | Grundrate aus Prototyp 1, nicht ändern |
| g_dev | float | 0,03 | constant | Anteil der Hingabe aus Prototyp 1, nicht ändern |
| g_cap | float | 1,05 | constant | Toleranz über der Tragfähigkeit aus Prototyp 1 |
| acc_t | double | 0 ≤ acc_t < 1 nach dem Jahr | calculated | Akkumulator je Stamm, wird gespeichert; double wie im JavaScript-Prototyp |
| born | int | 0 bis ⌈0,04 · V_t⌉ | calculated | neue Masse in diesem Jahr |

**Output Range:** G_t liegt zwischen 0 und 0,04 · V_t. Bei V_t = 0 gibt es keine
Gnade, und ein ausgestorbener Stamm setzt acc_t auf 0. Die Gnade erzeugt nur Masse.
Der Akkumulator wird nie pro Jahr gerundet.
**Example:** V = 48, D = 0,5, food = 60, cap = 60 → prosper = 1,
G = 48 · (0,01 + 0,015) = 1,2. acc 0,3 → 1,5, born = 1, acc = 0,5, V = 49.

### 2. Ziel und Obergrenze der Einzelnen

The Einzelnenziel formula is defined as:

`N_target_eff = min(N_target, floor(r_E_pm * V_total / 1000))` (ganzzahlig gerechnet) mit `N_target ≤ N_max` und immer `E_total ≤ N_max`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| N_target | int | 30–50, Default 40 | data file | Sollzahl der Einzelnen, fest und nicht mit dem Volk skaliert |
| N_max | int | N_target bis 80, Default 60 | data file | harte Obergrenze (Gotteshand, „Erwachsene erschaffen“). 20 über dem Ziel, damit man viele Statisten packen kann (Prototyp 2: Werfen ist der Kern des 30-Sekunden-Loops) |
| r_E_pm | int | 100–600 ‰, Default 500 | data file | größter Anteil der Einzelnen am gesamten Volk, in Promille, damit nichts gerundet werden muss |
| V_total | int | ≥ 0 | calculated | Summe aller V_t |
| E_total | int | 0 bis N_max | calculated | lebende Einzelne |
| N_target_eff | int | 0 bis N_target | calculated | wirksames Ziel |

**Output Range:** 0 bis N_target. Das Ziel ist fest: Bei 96 Köpfen sind 40 Einzelne
42 %, bei 500 Köpfen 8 %. Es bleiben gleich viele Gesichter, die Masse wird zur
Kulisse, und die Zahl sichtbarer Figuren bleibt im Budget. r_E_pm schützt kleine Welten,
damit Masse übrig bleibt.
**Example:** V_total = 96 → min(40, 48) = 40. V_total = 30 → min(40, 15) = 15.

### 3. Aufstieg aus der Masse

The Aufstieg formula is defined as:

`d_t = V_t * max(E_total, 1) - E_t * V_total`

Ist `E_total < N_target_eff`, steigt pro Geschichtsjahr ein Mensch auf: aus dem Stamm
mit dem größten d_t unter den Stämmen mit `V_t - E_t ≥ 1`. Bei Gleichstand gewinnt
die kleinste Stammes-ID.

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| E_t | int | 0 bis V_t | calculated | Einzelne des Stamms |
| d_t | int64 | ± N_max · V_total | calculated | Defizit: Anteil am Volk minus Anteil an den Einzelnen, mit gemeinsamem Nenner, nur Ganzzahlen |
| Stammes-ID | int | ≥ 0 | calculated | Gleichstand |

**Output Range:** ein Stamm oder keiner (kein Stamm hat Masse, oder das Ziel ist
erreicht). Gibt es noch keine Einzelnen, gewinnt der größte Stamm. Packt die
Gotteshand einen Statisten, entfällt die Wahl: Er kommt aus seinem Stamm.
**Example:** A: V 60, E 10; B: V 36, E 10; E_total 20, V_total 96.
d_A = 60 · 20 − 10 · 96 = 240, d_B = 36 · 20 − 960 = −240 → aus A steigt jemand auf.

### 4. Opferwahl bei einem Verlust von k Köpfen

The Opferwahl formula is defined as:

`k' = min(k, V_t - H_t)`; für jedes Opfer: `W = M * b + E_el * a`, ziehe `u = NextInt(0, W)`. Ist `u < M * b`, stirbt jemand aus der Masse (M und V_t sinken um 1). Sonst stirbt der Einzelne Nummer `floor((u - M * b) / a)` in ID-Reihenfolge unter den berechtigten. Gezogen wird ohne Zurücklegen.

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| k | int | ≥ 0 | calculated | Verlust laut Auftrag (Stämme, Welt, Gotteskraft) |
| H_t | int | 0 bis E_t | calculated | Einzelne „in Gottes Hand“ oder in einer laufenden intimen Handlung (Zustimmungsmodell Regel 21): zählen zum Volk, werden nie gezogen |
| M | int | ≥ 0 | calculated | Masse = V_t − E_t |
| E_el | int | 0 bis E_t | calculated | Einzelne, die gezogen werden dürfen |
| w_E = a / b | Bruch | 0,05–1, Default 1/4 (a = 1, b = 4) | data file | Gewicht eines Einzelnen gegenüber einem Kopf der Masse; als ganzzahliger Bruch, damit u ganzzahlig bleibt |

**Output Range:** w_E = 0 ist verboten (Minimum 0,05). Bei M = 0 sterben Einzelne mit
gleichem Gewicht. Was von k nicht erfüllbar ist (k − k'), entfällt und wird als
Ereignis gemeldet.
**Example:** V = 48, E = 20, M = 28, w_E = 1/4. Das erste Opfer ist mit 20/132 =
15,2 % ein Einzelner (ohne Gewicht wären es 41,7 %). Pro Kopf hat ein Einzelner also ein
Viertel des Risikos. Bei w_E = 1/2 wären es 26,3 %.

### 5. Eigenschaften und Geschlecht

The Eigenschaftswurf formula is defined as:

`trait = (r1 + r2 + r3) / (3 * R_res)` mit `r_i = NextInt(0, R_res + 1)` · `Frau = NextInt(0, 1000) < p_frau_pm` · `Name = freie_Namen[NextInt(0, Anzahl freie_Namen)]`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| R_res | int | 1000 | constant | Auflösung; eine einzige Division am Ende |
| trait | float | 0–1 | calculated | jeweils Frömmigkeit, Lust, Treue, Mut, Geselligkeit; danach fest |
| p_frau_pm | int | 100–900 ‰, Default 500 | data file | Wahrscheinlichkeit in Promille, dass ein neuer Einzelner eine Frau ist (ganzzahlig, damit nichts gerundet werden muss) |
| freie_Namen | Liste | ≥ 0 Einträge | data file / calculated | Namensliste des Stamms in Listenreihenfolge, ohne die Namen aller lebenden Einzelnen aller Stämme; ist sie leer, gilt der Edge Case „Namensliste erschöpft“ |

**Output Range:** 0–1, ohne Klemmen. Mittelwert 0,5, Standardabweichung 0,167:
86 % der Werte liegen zwischen 0,25 und 0,75, je etwa 1,5 % unter 0,15 und über 0,85. Die
Menschen sind also verschieden, aber selten extrem. Eine Gleichverteilung brächte zu
viele Extreme. Das Geschlecht wird unabhängig gewürfelt. Bei 40 Einzelnen ist ein
Verhältnis wie 25 : 15 möglich, gegensteuern lässt sich über p_frau_pm.
**Example:** r = 700, 400, 850 → 1950 / 3000 = 0,65.

### 6. Persönliche Lebensdauer

The Lebensdauer formula is defined as:

`LD_i = L_Lj - Δ_L + NextInt(0, 2 * Δ_L + 1)`; Tod im nächsten Personentakt, sobald `age_Lj ≥ LD_i`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| L_Lj | int | Default 40 | data file | mittlere Lebensdauer in Lebensjahren (dieses System besitzt den Wert, Simulationskern rechnet um) |
| Δ_L | int | 0 bis L_Lj − 1, Default 10 | data file | Streuung (± 25 %) |
| LD_i | int | 30–50 beim Default | calculated | persönliche Lebensdauer |

**Output Range:** 30–50 Lebensjahre, also 180–300 s bei 1× (Entscheidung des
Entwicklers: etwa 4 Minuten im Mittel). Der Mittelwert bleibt 40.
**Example:** NextInt ergibt 7 → LD_i = 30 + 7 = 37 → Tod, sobald age_steps ≥ 2220
(ohne Zeit in Gottes Hand).

### 7. Startalter der ersten Einzelnen

The Startalter formula is defined as:

`a_0 = NextInt(0, LD_i)`; `t_birth = -(a_0 * S_Lj)` für die Einzelnen, die beim Erzeugen der Welt hervortreten

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| a_0 | int | 0 bis LD_i − 1 | calculated | Alter beim Start der Welt in Lebensjahren |
| t_birth | int64 | ≤ 0 | calculated | vorverlegter Schritt des Hervortretens |

**Output Range:** Die ersten Einzelnen sind gleichmäßig über ihre Lebensspanne
verteilt. Ihr Tod kommt dann nicht als Welle nach 3–5 Minuten. Später Aufgestiegene
beginnen bei Alter 0. (Festgelegt in dieser Sitzung, um die Todeswelle zu
vermeiden.)
**Example:** LD_i = 44, a_0 = 30 → t_birth = −1800. Er stirbt nach 14 Lebensjahren,
84 s nach dem Start (im Personentakt: Schritt 840 bei gerader, Schritt 841 bei
ungerader ID).
**Welterzeugung** (Entscheidung des Entwicklers vom 2026-10-25): Nach dem Anlegen der
Stämme (2 × 48) läuft Formel 3 wiederholt, ohne Jahresgrenze, bis E_total =
N_target_eff; bei 2 × 48 sind das 40 Einzelne, 20 je Stamm (der Gleichstand geht an
die kleinste Stammes-ID, danach wechseln die Stämme ab). Jeder von ihnen bekommt a_0
nach dieser Formel, die Würfe fallen in der Reihenfolge der Aufstiege.

### 8. Persönliche Hingabe

The Hingabe formula is defined as:

`h_i = clamp(D_t + k_F * (F_i - 0.5) + O_i, 0, 1)`; jährlich `O_i = O_i * λ_h + Σ Ereignisse`, auf 0 gesetzt, sobald `|O_i| < 0,001`

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| D_t | float | 0,05–1 | calculated | Stammeshingabe |
| F_i | float | 0–1 | calculated | Frömmigkeit des Einzelnen |
| k_F | float | 0–1, Default 0,4 | data file | Gewicht der Frömmigkeit (± 0,2) |
| O_i | float | −1 bis 1 (geklemmt) | calculated | Nachwirkung persönlicher Eingriffe, wird gespeichert |
| λ_h | double | 0,94–0,998, Default 0,98851 | data file | Verfall pro Geschichtsjahr; der Default ergibt eine Halbwertszeit von 60 Geschichtsjahren = 5 Lebensjahren, bei 1× 30 s (vorher 0,94 mit nur 11 Jahren, also 6 s, und damit kaum sichtbar) |
| Ereignis | float | −1 bis 1 | data file | Beträge legen die GDDs der Eingriffe fest |

**Output Range:** 0–1 (geklemmt). Die Hingabe steht nicht im Zustimmungsprofil.
**Example:** D 0,6, F 0,9, O 0,1 → 0,6 + 0,16 + 0,1 = 0,86. Ein Segen von +0,3 wirkt
nach 12 Jahren (einem Lebensjahr) noch mit 0,3 · 0,98851¹² = 0,261 und nach 60 Jahren
mit 0,15. Ein persönlicher Eingriff bleibt damit etwa ein Achtel eines Lebens deutlich
sichtbar (Pillar 2). Gelesen wird h_i von Gotteskraft & Götterkräfte (Zufluss aus
Hingabe) und von der Anzeige eines Einzelnen.

### 9. Bewegung

The Bewegung formula is defined as:

`1 Zelle (Chebyshev-Abstand) je M_travel Personentakte mit Ziel, je M_wander ohne Ziel`; `T_reise = d * M_travel / (R / P_person)` Sekunden bei 1×

**Variables:**
| Variable | Type | Range | Source | Description |
|----------|------|-------|--------|-------------|
| M_travel | int | ≥ 1, Default 1 | data file | Personentakte je Zelle mit Ziel |
| M_wander | int | ≥ 1, Default 4 | data file | Personentakte je Zelle ohne Auftrag |
| R / P_person | int | 5 beim Default | calculated | Personentakte pro echter Sekunde bei 1×, abgeleitet aus R = 2 · S_year und P_person (Simulationskern) |
| d | int | 0–39 | calculated | Abstand in Zellen (Karte von Prototyp 1: 40 × 28) |

Ohne Ziel wählt ein Einzelner bei jedem Wanderschritt eine der Nachbarzellen im
Gebiet seines Stamms mit `NextInt(0, Anzahl)`, in fester Reihenfolge (N, NO, O, SO, S,
SW, W, NW). Gibt es keine, bleibt er stehen. Steht er außerhalb des Gebiets seines
Stamms, ist die nächste Zelle seines Gebiets sein Ziel (kleinster Chebyshev-Abstand,
bei Gleichstand die kleinste Zellen-ID); gewürfelt wird dann nicht. Mit Ziel geht er auf die Nachbarzelle,
die den Chebyshev-Abstand zum Ziel am stärksten verringert, bei Gleichstand die erste
in derselben Reihenfolge.

**Output Range:** Mit Ziel 5 Zellen pro Sekunde, quer über die Insel in 8 s
(1,3 Lebensjahre). Ohne Ziel 1,25 Zellen pro Sekunde. Die Ansicht glättet die
Bewegung.
**Example:** Ein Fest 12 Zellen entfernt erreicht man in 12 Takten = 24 Schritten =
2,4 s bei 1×.

### Kalibrierung gegen Prototyp 1

Gnade und Konstanten bleiben unverändert, das Volk startet mit 2 × 48 Köpfen wie in
Prototyp 1. Neu sind nur die Einzelnen mit Alter. Weil die Masse nicht altert und
Verluste wie im Prototyp aus dem ganzen Volk kommen, sollte die Aussterberate sich
kaum ändern. Gemessen wird sie trotzdem neu mit den Debug- und Balancing-Werkzeugen.

## Edge Cases

*`systems-designer` not consulted for this section — Lean mode. Review manually before production.*

- **If ein Verlust k größer ist als das Volk ohne die Gehaltenen** (k > V_t − H_t):
  Es sterben V_t − H_t Menschen. Der Rest entfällt und wird als Ereignis
  „Verlust nicht vollständig“ gemeldet. Einzelne in Gottes Hand sterben nie durch
  Volksverluste.
- **If die letzten Menschen eines Stamms in Gottes Hand sind**: Das Volk sinkt nie
  unter H_t, der Stamm stirbt also nicht aus, solange jemand von ihm gehalten wird. Ist
  der Mensch zurück und das Volk danach 0, stirbt der Stamm aus.
- **If die ganze Welt ausstirbt** (V_total = 0): Ereignis „Die Welt ist leer“. Nur die
  Kraft „Erwachsene erschaffen“ kann sie wieder bevölkern. Wo dabei ein neuer Stamm
  entsteht, legen Gotteskraft & Götterkräfte und Stämme & Diplomatie fest. Die Gnade
  wirkt bei V_t = 0 nicht.
- **If die Gotteshand einen Statisten packen will, aber N_max erreicht ist**: Der
  Griff gelingt nicht. Die Gotteshand zeigt das an (GDD Gotteshand & Ragdoll), und
  Einzelne kann sie weiter packen. Weil Einzelne nach etwa 4 Minuten sterben, werden
  laufend Plätze frei.
- **If mehr Einzelne leben als N_target_eff** (nach Griffen oder „Erwachsene
  erschaffen“): Es steigt niemand auf, bis die Zahl unter das Ziel fällt. Niemand wird
  zurück in die Masse gestuft.
- **If ein Stamm erobert wird**: Stämme & Diplomatie legt fest, wie viele sterben und
  wie viele zum Sieger wechseln (in Prototyp 1 die Hälfte). Die Toten wählt die
  Opferwahl (Formel 4). Einzelne, die wechseln, behalten alles außer dem Stamm. Ob sie
  dabei gefangen werden, entscheidet Stämme & Diplomatie.
- **If sich ein Stamm abspaltet und die Einzelnen nicht genau aufgehen**: Verteilt
  wird nach dem Verfahren des größten Rests. Bei Gleichstand bekommt der Stamm mit der
  kleineren ID den Einzelnen, und wer wechselt, wird mit dem Zufallsstrom gezogen.
- **If die Namensliste eines Stamms erschöpft ist**: Ein Name wird erneut vergeben,
  mit römischer Ziffer („Ilsa II“), sodass er unter den Lebenden eindeutig bleibt. Der
  Grundname wird mit einem Wurf `NextInt(0, Länge der ganzen Liste)` gewählt, die Ziffer
  ist die kleinste ab II, die unter den Lebenden frei ist. Es bleibt also bei genau einem
  Wurf für den Namen.
- **If ein Einzelner stirbt, während er ein Ziel hat** (Fest, Ritual) oder eine Rolle
  innehat: Die Systeme, die Ziel oder Rolle vergeben haben, bekommen das Ereignis
  „Tod“ und entfernen ihn selbst.
- **If ein Einzelner gleichzeitig verletzt und gefangen ist**: Beides gilt. Er heilt
  wie sonst auch, ist aber weiter gefangen.
- **If ein Einzelner seine Lebensdauer erreicht, während er in Gottes Hand ist**:
  Er stirbt nicht in der Hand, sondern im ersten Personentakt nach der Rückgabe
  (Simulationskern).
- **If p_frau_pm auf 0 oder 1000 gesetzt wird**: Das ist nicht erlaubt. Erlaubt ist 100–900 ‰,
  damit es immer beide Geschlechter gibt. Begehren & Lust und Harem & Rituale gehen
  davon aus.
- **If niemand aufsteigen kann, weil kein Stamm Masse hat**: Das Ziel bleibt
  unerreicht, bis die Gnade neue Masse bringt. Das ist kein Fehler.
- **If ein Eingriff die Hingabe eines Einzelnen über 1 oder unter 0 treiben würde**:
  h_i wird geklemmt, O_i auf −1 bis 1. Ein Eingriff kann die Hingabe also nie
  dauerhaft auf 1 festsetzen.
- **If ein System einem Menschen der Masse etwas Persönliches antun will** (zum
  Beispiel ein Blitz auf einen Statisten): Der Statist steigt vorher auf, wie beim
  Griff der Gotteshand (bis N_max). Ist N_max erreicht, wirkt der Eingriff als Verlust
  des Volks um 1 statt auf eine Person.

## Dependencies

**Hängt ab von:**

| System | Art | Was gebraucht wird | GDD |
|---|---|---|---|
| Simulationskern | hart | Personentakt, Jahrestakt beim Versatz 2, Zufallsstrom (system_id 2), Status „in Gottes Hand“, Umrechnung Lebensjahr | ✓ `design/gdd/simulationskern.md` |
| Welt & Insel | hart | Zellen, Gebiet je Stamm, Nahrung food_t und Tragfähigkeit cap_t je Stamm, Startplätze, Hunger als Auftrag | ✓ `design/gdd/welt-und-insel.md` (Entwurf) |
| Kultur & Erinnerung | hart für die Gnade, nur lesend | Stammeshingabe D_t | ✗ noch keins (vorläufiger Vertrag) |

Kultur & Erinnerung hängt seinerseits von Menschen ab (Systemübersicht). Diese
Kreisabhängigkeit wird über die Schnittstelle gelöst: Kultur & Erinnerung stellt D_t
nur lesbar bereit, und Menschen schreibt nie in die Kultur.

**Wird gebraucht von:** Zustimmungsmodell (Zustimmungsprofil), Stämme & Diplomatie,
Kultur & Erinnerung, Begehren & Lust, Harem & Rituale, Gotteskraft & Götterkräfte,
Gotteshand & Ragdoll, Körper & Animation, Weltansicht & Kamera, Chronik, Speichern &
Laden, HUD & Chronik-Ansicht. Ein GDD haben davon bisher das Zustimmungsmodell
(`design/gdd/zustimmungsmodell.md`) und der Simulationskern; beide nennen Menschen &
Bevölkerung als Abhängigkeit.

## Tuning Knobs

| Regler | Default | Sicherer Bereich | Wirkung |
|---|---|---|---|
| Startvolk je Stamm | 48 | fest bis zur Messung | wie Prototyp 1; eine Änderung bricht die Kalibrierung |
| N_target | 40 | 30–50 | Zahl der Gesichter; mehr kostet Figuren in voller Detailstufe |
| N_max | 60 | N_target bis 80 | wie viele Statisten man packen kann, bevor ein Griff fehlschlägt |
| r_E_pm | 500 ‰ | 100–600 ‰ | wie viel Masse in kleinen Welten bleibt |
| w_E = a / b | 1/4 | 0,05–1 | wie oft Einzelne bei Verlusten sterben |
| p_frau_pm | 500 ‰ | 100–900 ‰ | Geschlechterverhältnis neuer Einzelner |
| L_Lj, Δ_L | 40, 10 | L_Lj 30–60, Δ_L bis L_Lj − 1 | Lebensdauer und Streuung, etwa 4 Minuten bei 1× |
| k_F | 0,4 | 0–1 | wie stark die Frömmigkeit die persönliche Hingabe hebt |
| λ_h | 0,98851 | 0,94–0,998 | wie lange ein persönlicher Eingriff in der Hingabe nachwirkt |
| M_travel, M_wander | 1, 4 | je ≥ 1 | Tempo mit und ohne Ziel |
Jedes neue GDD muss Menschen & Bevölkerung nennen und die Verträge aus den
Interactions übernehmen.

## Visual/Audio Requirements

Keine eigenen. Wie Einzelne und Statisten aussehen, legen Körper & Animation und die
Weltansicht fest. Vorgaben aus diesem System: Alle Figuren wirken klar erwachsen
(Konzept: etwa 7 bis 7,5 Kopflängen). Frau und Mann haben je einen Grundkörper, und
Einzelne und Statisten nutzen dieselben Körper, damit ein gepackter Statist beim
Aufstieg nicht seine Gestalt wechselt.

## Game Feel

Die Welt soll sich voll und lebendig anfühlen, aber mit Gesichtern, die man
wiedererkennt. Ein Einzelner lebt im Mittel etwa 4 Minuten bei 1×. Das ist lang genug,
um ihn zu verfolgen, und kurz genug, dass ständig neue Gesichter kommen. Packt man
einen Statisten, wird er spätestens im nächsten Schritt (bei 1× höchstens 0,1 s) zu einer
Person mit Namen; die Ansicht hält die Figur in der Zwischenzeit schon in der Hand. Das ist die
Brücke zwischen dem Spielzeug aus Prototyp 2 und der Simulation.

## UI Requirements

- **Ansehen eines Einzelnen** (Hover oder Klick, auch in der Pause): Name, Stamm,
  Geschlecht, Alter (18 + Lebensjahre), Gesundheit, Freiheit und Hingabe. Ob die fünf
  Eigenschaften als Zahl, als Wort („sehr fromm“) oder gar nicht gezeigt werden, ist
  eine offene Frage für den UX-Spec.
- **Stamm ansehen:** Volk V_t und die Zahl der Einzelnen.
- **Ereignisse** (über die Chronik): Tod mit Ursache, Hervortreten, Gefangennahme.

> **📌 UX Flag — Menschen & Bevölkerung**: This system has UI requirements. In Phase 4
> (Pre-Production), run `/ux-design` to create a UX spec for each screen or HUD
> element this system contributes to **before** writing epics. Stories that
> reference UI should cite `design/ux/[screen].md`, not the GDD directly.

## Cross-References

| Dokument | Was hier verwendet wird |
|---|---|
| `design/gdd/simulationskern.md` | Personentakt, Jahrestakt (Versatz 2, dort nachgetragen), Zufallsstrom 2, Status „in Gottes Hand“, Formel `lebensjahr`, t_birth auch negativ (dort angepasst) |
| `design/gdd/game-concept.md` | Nur Erwachsene, Gnade aus Ritualen, „Erwachsene erschaffen“, Hingabe ist keine Zustimmung, Gefangene ausgeschlossen, 30–50 Menschen im MVP |
| `prototypes/weltengott-concept/prototype.html` | Gnade-Formel und ihre Konstanten, Startvolk 2 × 48, Reihenfolge im Jahresschritt |
| `prototypes/gotteshand-concept/REPORT.md` | Werfen als Kern des 30-Sekunden-Loops (Grund für N_max = 60) |
| `design/gdd/zustimmungsmodell.md` | liest das Zustimmungsprofil (Regel 18) mit genau diesen 10 Feldern |
| `design/gdd/systems-index.md` | Abhängigkeiten |

## Acceptance Criteria

Entwurf des `qa-lead` (2026-10-25), mit den eingearbeiteten Korrekturen. Testwurzel:
`Assets/Tests/EditMode/MenschenUndBevoelkerung/`.

### Kern, Daten, Grenzen
- **AC-01 [Logic/EditMode]** **GIVEN** eine neue Welt, **WHEN** 500 Geschichtsjahre mit zufälligen Ereignissen aus einem Testgenerator mit festem Startwert laufen, **THEN** beginnt sie mit 2 Stämmen zu je 48 und 20 Einzelnen je Stamm, und nach jedem Schritt gilt E_t ≤ V_t, E_total ≤ N_max und jeder Einzelne gehört genau einem Stamm (Property-Test).
- **AC-02 [Logic/EditMode]** **GIVEN** V_t = 0 und acc_t = 0,7, **WHEN** das Jahr läuft, **THEN** bleibt V_t = 0, acc_t wird 0, und es entsteht niemand.
- **AC-03 [Logic/EditMode]** **GIVEN** 1.000 Tode und neue Einzelne, **WHEN** die IDs verglichen werden, **THEN** steigen sie streng und kommen nie doppelt vor; Namen sind unter den Lebenden eindeutig, eine erschöpfte Liste ergibt „Ilsa II“, und die fünf Eigenschaften sind nach 100 Jahren unverändert.
- **AC-04 [Logic/EditMode] Grenze „nur Erwachsene“, streng** **GIVEN** die Assembly des Systems, **WHEN** sie per Reflection geprüft wird, **THEN** gibt es genau einen Weg, Einzelne zu erzeugen (intern, mit dem Grund Welterzeugung, Aufstieg oder Erwachsene erschaffen); jeder Anstieg von V_total pro Schritt ist durch die Gnade, die Welterzeugung oder „Erwachsene erschaffen“ erklärt (Property-Test, 1.000 Startwerte, zufällige Eingriffe), und kein Typ, Feld oder Enum-Wert heißt Kind, Child oder Geburt.
- **AC-05 [Logic/EditMode] Grenze „nur Erwachsene“, streng** **GIVEN** beliebige Einzelne (1.000 Startwerte, je 500 Jahre), **WHEN** das angezeigte Alter berechnet wird, **THEN** gilt jederzeit age_steps ≥ 0, das angezeigte Alter ist genau 18 im Schritt des Hervortretens auf allen vier Wegen, und jeder Einzelne ist genau einer von Masse, Lebend oder Tot und trägt nur die vier Merkmale aus States and Transitions.
- **AC-06 [Logic/EditMode] Grenze „Hingabe ist keine Zustimmung“, streng** **GIVEN** der Typ `Zustimmungsprofil` (unveränderliche Struktur in eigener Assembly), **WHEN** seine Felder per Reflection gelesen werden, **THEN** sind es genau Frömmigkeit, Lust, Treue, Mut, Geselligkeit, Geschlecht, Freiheit, Gesundheit, im Kampf und Stammes-ID; jedes weitere Feld, jeder Verweis auf einen Einzelnen und jeder Name wie Hingabe, Rolle, Rang, Kraft oder Eingriff lässt den Test scheitern, und die Assembly des Zustimmungsmodells referenziert nur die Profil-Assembly, nicht die Menschen-Assembly.
- **AC-07 [Logic/EditMode] Grenze, streng** **GIVEN** 1.000 Einzelne, **WHEN** Hingabe, Nachwirkung O_i, Rolle oder Rang beliebig geändert werden, **THEN** ist ihr Zustimmungsprofil bitgleich (Property-Test), und die Masse liefert kein Profil.
- **AC-08 [Logic/EditMode]** **GIVEN** ein gefangener und verletzter Einzelner, **WHEN** er heilt, **THEN** bleibt er gefangen, sein Profil zeigt Freiheit = gefangen, und er zählt zum Volk des Stamms, der ihn hält.
- **AC-28 Welterzeugung [Logic/EditMode]** **GIVEN** ein Startwert, **WHEN** eine neue Welt erzeugt wird, **THEN** hat jeder Stamm V = 48 und 20 Einzelne (E_total = N_target_eff = 40, die Aufstiege wechseln ab, Gleichstand an die kleinste Stammes-ID), jeder Einzelne hat 0 ≤ a_0 < LD_i und eine Zelle im Gebiet seines Stamms, und derselbe Startwert ergibt denselben Hash; bei einer Testwelt mit V_total = 30 sind es 15.
- **AC-29 Tod und Volk [Logic/EditMode]** **GIVEN** V = 48, E = 20 (Masse 28), **WHEN** ein Einzelner an Alter, durch die Opferwahl oder durch einen direkten Tod stirbt, **THEN** ist danach V = 47, E = 19 und die Masse unverändert 28; über 1.000 Startwerte mit je 500 Jahren wächst die Masse nie durch einen Tod, und V_total sinkt nur durch Tod oder Verlust.

### Formeln
- **AC-09 Formel 1, Kalibrierung [Logic/EditMode]** **GIVEN** V = 48, D = 0,5, food = 60, cap = 60, acc = 0,3, **WHEN** ein Jahr läuft, **THEN** ist born = 1, V = 49 und acc = 0,5 (Toleranz 1e-9).
- **AC-10 Formel 1, Bedingungen [Logic/EditMode]** **GIVEN** food = 0 oder V · 100 ≥ cap · 105, **WHEN** ein Jahr läuft, **THEN** bleiben acc und V unverändert; bei born > 0 bleibt E_total gleich.
- **AC-11 Formel 2 [Logic/EditMode]** **GIVEN** N_target = 40, **WHEN** V_total = 96, 30 und 0 ist, **THEN** ist das wirksame Ziel 40, 15 und 0.
- **AC-12 Formel 3 [Logic/EditMode]** **GIVEN** Stamm A (V 60, E 10) und B (V 36, E 10), **WHEN** der Aufstieg läuft, **THEN** ist d_A = 240, d_B = −240, und der Aufsteiger kommt aus A; ohne Einzelne gewinnt der größte Stamm, bei Gleichstand die kleinste Stammes-ID; es steigt höchstens einer pro Jahr auf, keiner bei erreichtem Ziel oder ohne Masse, und niemand wird in die Masse zurückgestuft.
- **AC-13 Statist packen [Logic/EditMode]** **GIVEN** E_total = 59 und N_max = 60, **WHEN** ein Statist gepackt wird, **THEN** steigt er sofort aus seinem Stamm auf (E_total = 60); bei E_total = 60 schlägt der Griff fehl, Einzelne bleiben packbar, und ein Blitz auf einen Statisten wird zum Verlust des Volks um 1.
- **AC-14 „Erwachsene erschaffen“ [Logic/EditMode]** **GIVEN** E_total = 58 und N_max = 60, **WHEN** n = 5 Erwachsene erschaffen werden, **THEN** steigt V um 5 und E_total auf 60 (2 neue Einzelne), der Rest wird Masse.
- **AC-15 Formel 4, statistisch [Logic/EditMode]** **GIVEN** V = 48, E = 20, w_E = 1/4 und ein fester Startwert, **WHEN** 10.000 einzelne Verluste (k = 1) jeweils vom selben Ausgangszustand gezogen werden, **THEN** ist der Anteil der Einzelnen unter den Opfern 15,2 % ± 1,5 Prozentpunkte.
- **AC-16 Formel 4, Grenzen [Logic/EditMode]** **GIVEN** k > V − H, **WHEN** der Verlust angewendet wird, **THEN** sterben genau V − H, und das Ereignis „Verlust nicht vollständig“ nennt k − k'; ein Einzelner in Gottes Hand wird in 10.000 Läufen nie gezogen, k = 5 ergibt keine doppelten Opfer, und bei M = 0 haben alle Einzelnen gleiches Gewicht.
- **AC-17 Direkter Tod [Logic/EditMode]** **GIVEN** ein Blitz tötet einen Einzelnen, **WHEN** das verarbeitet wird, **THEN** sinkt das Volk seines Stamms um 1, das Ereignis „Tod“ nennt ID, Name, Stamm und Ursache-ID, und die ID wird nie neu vergeben.
- **AC-18 Formel 5, statistisch [Logic/EditMode]** **GIVEN** ein fester Startwert, **WHEN** 10.000 Eigenschaften gewürfelt werden, **THEN** ist der Mittelwert 0,50 ± 0,005, die Standardabweichung 0,167 ± 0,005, der Anteil in [0,25; 0,75] 86 % ± 1,5 Punkte und der Anteil unter 0,15 1,5 % ± 0,5 Punkte; r = 700, 400, 850 ergibt 0,65, die Wurfreihenfolge ist Geschlecht, 15 Eigenschaften, Lebensdauer, (Startalter), Name, Ort, und bei p_frau_pm = 500 sind 50 % ± 1,5 Punkte Frauen.
- **AC-19 Formel 6 [Logic/EditMode]** **GIVEN** 10.000 Würfe, **WHEN** LD_i gezogen wird, **THEN** liegt jeder Wert in 30–50, alle 21 Werte kommen vor, und der Mittelwert ist 40 ± 0,3; **GIVEN** LD_i = 37, **THEN** stirbt der Einzelne im ersten Personentakt mit age_steps ≥ 2.220, Zeit in Gottes Hand verschiebt das, und in der Hand stirbt niemand.
- **AC-20 Formel 7 [Logic/EditMode]** **GIVEN** LD_i = 44 und a_0 = 30, **WHEN** die Welt erzeugt wird, **THEN** ist t_birth = −1.800 und der Tod kommt in Schritt 840 (gerade ID) bzw. 841 (ungerade ID); für alle Start-Einzelnen gilt 0 ≤ a_0 < LD_i, und bei t = 0 stirbt niemand.
- **AC-21 Formel 8 [Logic/EditMode]** **GIVEN** D = 0,6, F = 0,9 und O = 0,1, **WHEN** h_i berechnet wird, **THEN** ist es 0,86 ± 1e-6; ein Segen von +0,3 ergibt nach 12 Jahren O = 0,261 ± 0,001, |O| < 0,001 wird 0, O bleibt in [−1; 1] und h in [0; 1].
- **AC-22 Formel 9 [Logic/EditMode]** **GIVEN** ein Ziel 12 Zellen entfernt (Chebyshev) und M_travel = 1, **WHEN** der Einzelne geht, **THEN** kommt er nach genau 12 eigenen Personentakten an; ohne Ziel geht er 1 Zelle je 4 Takte, bleibt immer im Gebiet seines Stamms und wählt bei gleichem Startwert dieselben Zellen; d = 0 ergibt keine Bewegung.
- **AC-30 Wechsel ins fremde Gebiet [Logic/EditMode]** **GIVEN** ein Einzelner wechselt per Abspaltung in einen Stamm, dessen gesamtes Gebiet nicht an seine Zelle grenzt (Abstand d zur nächsten Zelle), **WHEN** d Personentakte laufen, **THEN** steht er in einer Zelle seines neuen Gebiets und wandert danach nur dort; er bleibt in keinem Fall außerhalb stehen, und bei gleichem Startwert sind die Wege gleich.

### Stamm, Takt, Speichern, Determinismus
- **AC-23 Abspaltung und Eroberung [Logic/EditMode]** **GIVEN** ein Wechsel von m Köpfen, **WHEN** verteilt wird, **THEN** bleiben V_total und E_total bis auf die Toten erhalten, die Einzelnen folgen dem größten Rest, bei Gleichstand bekommt die kleinere Stammes-ID, und wer wechselt, behält alles außer dem Stamm.
- **AC-24 Jahrestakt [Integration/EditMode, mit Stubs]** **GIVEN** S_year = 5, **WHEN** t mod 5 = 2 ist, **THEN** laufen Gnade, Aufstieg und Hingabe genau einmal, vor Stämme & Diplomatie, und sehen Nahrung (Versatz 0) und D_t (Versatz 1) desselben Jahres.
- **AC-31 Aufträge [Logic/EditMode]** **GIVEN** ein Stub von Stämme & Diplomatie fordert in Schritt t einen Verlust von k = 5 für Stamm A und liest danach im selben Schritt V_A, **WHEN** der Schritt läuft, **THEN** ist V_A schon um 5 gesunken, die Opferwahl hat ihre Würfe beim Aufruf aus Strom 2 gezogen, und ein zweiter Lauf ergibt denselben Hash; dasselbe gilt für Abspaltung (die Einzelne sind sofort verteilt), Gefangennahme (Freiheit = gefangen sofort sichtbar) und das Merkmal „im Kampf“ (im selben Schritt im Zustimmungsprofil).
- **AC-25 Speichern und Laden [Logic/EditMode]** **GIVEN** ein Zustand nach 100 Jahren, **WHEN** gespeichert und geladen wird, **THEN** ist der Zustands-Hash gleich (alle Felder, acc_t, O_i, nächste freie ID).
- **AC-26 Determinismus [Logic/EditMode]** **GIVEN** gleicher Startwert und gleiche Ereignisfolge, **WHEN** zweimal 500 Geschichtsjahre laufen, **THEN** sind die Hashes (V_t, alle Einzelnen, acc_t, O_i, nächste ID) identisch, und ein anderer Startwert ergibt einen anderen Hash.
- **AC-27 [Manual]** **GIVEN** Einzelne im Spiel, **WHEN** ihre Anzeige geprüft wird, **THEN** zeigt jede Figur ein Alter ≥ 18; Screenshots liegen in `production/qa/evidence/`.
- **AC-32 Aufträge des Kerns [Logic/EditMode]** **GIVEN** E_total = 59, N_max = 60 und ein Griff auf einen Statisten aus Stamm A in Zelle c, **WHEN** der Kern den Beginn anwendet, **THEN** tritt der Statist mit Ort c hervor (E_total = 60), das Ereignis „Hervortreten“ nennt den Eingriff als Ursache, er ist „in Gottes Hand“, und ein „im Kampf“ ist gelöscht; bei E_total = 60 wird der Griff ein Eingriff ins Leere und niemand tritt hervor; **WHEN** das Ende mit Ort c', Ergebnis „verletzt“ und Verletzungsklasse k angewendet wird, **THEN** steht er in c', ist verletzt mit k und dem Schritt der Heilung, und held_closed ist gewachsen.
- **AC-33 Namen und Gefangene [Logic/EditMode]** **GIVEN** zwei Stämme mit derselben Namensliste, **WHEN** 200 Einzelne hervortreten und Einzelne zwischen den Stämmen wechseln, **THEN** ist jeder Name unter allen Lebenden eindeutig, und für jeden Namen fällt genau ein Wurf; **GIVEN** ein Gefangener, **THEN** wandert er nur im Gebiet des Stamms, der ihn hält, und ein Einzelner „in Gottes Hand“ wird bei einer Abspaltung nie zum Wechsel gezogen.

## Open Questions

| Frage | Wer klärt | Bis wann |
|---|---|---|
| Das Konzept nennt „30–50 Menschen“ im MVP. Jetzt sind es 30–50 Einzelne plus ein Volk von anfangs 96 Köpfen. Konzept anpassen? | Entwickler | nach diesem GDD |
| Zeigt die Ansicht die fünf Eigenschaften als Zahl, als Wort oder gar nicht? | UX-Spec zur Ansicht eines Einzelnen | `/ux-design` in der Pre-Production |
| Wie viele Erwachsene erschafft die Kraft, und in welchem Stamm? | GDD Gotteskraft & Götterkräfte | mit dem GDD |
| Wer nimmt an einem Kampf teil („im Kampf“), und wann wird gefangen genommen? | GDD Stämme & Diplomatie | mit dem GDD |
| Wie wirkt die Stammeshingabe D_t, und wer verändert sie? (vorläufig: Kultur & Erinnerung) | GDD Kultur & Erinnerung | mit dem GDD |
| Bleibt die Aussterberate mit den alternden Einzelnen wie in Prototyp 1 bei 0 %? | Debug- & Balancing-Werkzeuge | erste Messung im MVP-0 |
