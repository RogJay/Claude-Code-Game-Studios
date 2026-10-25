# Game Concept: Allmacht

*Created: 2026-10-25*
*Status: Draft, überarbeitet nach Design-Review, Prototyp 1 und Prototyp 2 (2026-10-25)*

---

## Elevator Pitch

> Ein 3D-Gottsimulator, in dem du als unsichtbarer Gott zusiehst, wie Stämme von
> Menschen von selbst zu Zivilisationen heranwachsen. Mit Götterkräften und
> deiner eigenen Hand formst du, ob daraus eine Lust-Sekte, ein Gottesstaat
> oder ein Kriegerreich wird.

---

## Core Identity

| Aspect | Detail |
| ---- | ---- |
| **Genre** | Gottsimulator / Zivilisations-Sandbox mit Physik und Erwachseneninhalten |
| **Platform** | PC (zuerst); Android (Draufsicht, per APK) als eigene optionale Stufe |
| **Target Audience** | Privates Projekt: Zielspieler ist die Person, die es entwickelt (siehe Target Player Profile) |
| **Player Count** | Single-player |
| **Session Length** | 30–120 Minuten (bei 1× etwa 120 simulierte Jahre pro Minute, siehe Session-Level-Loop) |
| **Monetization** | Keine (privat, nicht zum Verkauf) |
| **Estimated Scope** | Large (30–48 Monate Vollzeit, solo). Bei unter 10 h/Woche ein Vielfaches davon |
| **Comparable Titles** | WorldBox, Black & White, Wild Life |

---

## Core Fantasy

Macht und Kontrolle, ohne verwalten zu müssen. Du bist ein Gott über einer Welt
voller Menschen, die ihr eigenes Leben führen: Sie lieben, beten, feiern und
führen Krieg. Ein Eingriff von dir genügt, und eine ganze Kultur schlägt eine
andere Richtung ein. Die Welt sorgt für sich selbst, und du sorgst für das
Drama. Du kannst Menschen hochheben, werfen, segnen oder strafen. Am Ende einer
Ära siehst du in der Chronik, welche Zivilisation du geformt hast.

---

## Unique Hook

Wie WorldBox, nur in 3D. **Und außerdem:** Die Gesellschaften, die entstehen,
sind das Ergebnis deiner Eingriffe, und Lust, Hingabe und Körperphysik gehören
dazu, wie Kulturen sich formen. Belohnst du Feste, entsteht eine Lust-Sekte.
Strafst du mit Plagen, entsteht ein ängstlicher Gottesstaat. Die Kultur
erinnert sich, warum sie so geworden ist.

---

## Player Experience Analysis (MDA Framework)

### Target Aesthetics (What the player FEELS)

| Aesthetic | Priority | How We Deliver It |
| ---- | ---- | ---- |
| **Sensation** (sensory pleasure) | 2 | Ragdoll- und Verletzungsphysik, die Gotteshand, halbrealistische Körper (Look „Fleisch und Folgen“) |
| **Fantasy** (make-believe, role-playing) | 3 | Gottsein, Macht, Hingabe der Anhänger, Harem und Rituale |
| **Narrative** (drama, story arc) | 5 | Nur Geschichten, die von selbst entstehen: Kriege, Sektengründungen, Untergänge in der Chronik. Keine geschriebene Story |
| **Challenge** (obstacle course, mastery) | 6 | Gering: Kulturen gezielt in eine Richtung zu lenken ist die einzige Meisterschaft |
| **Fellowship** (social connection) | N/A | Single-player, privat |
| **Discovery** (exploration, secrets) | 4 | Was entsteht, wenn ich X tue? Emergente Systeme |
| **Expression** (self-expression, creativity) | 1 | Die Zivilisation, die am Ende steht, ist das kreative Werk des Spielers |
| **Submission** (relaxation, comfort zone) | 7 | Zusehen, wie die Welt lebt; Zeitraffer |

*Alternativen, die erwogen wurden:* Challenge als Primärästhetik (mit Siegbedingungen
wie bei Black & White) wurde verworfen, weil Kontrolle und Experimentieren gewünscht
sind, nicht Gewinnen. Narrative mit geschriebener Kampagne wurde verworfen (Anti-Pillar).

### Key Dynamics (Emergent player behaviors)

- Der Spieler experimentiert mit Kräften an Stämmen, um zu sehen, welche Kultur
  daraus entsteht („Was passiert, wenn ich nur Feste segne?“).
- Der Spieler baut gezielte Experimente: zwei gegensätzliche Kulturen
  nebeneinander, dann Krieg auslösen.
- Der Spieler hängt sich an einzelne Erwählte und verfolgt ihr Schicksal.
- Der Spieler liest die Chronik nach, um Ursache und Wirkung nachzuvollziehen.
- Der Spieler spielt mit der Physik: Menschen werfen, Katastrophen auslösen,
  zusehen, was passiert.

### Core Mechanics (Systems we build)

1. **Autonome Zivilisations-Simulation**: abstrakt, als Daten in einem
   deterministischen Tick. Stämme wachsen, bilden Gesellschaftsformen, führen
   Krieg. Die großen Entscheidungen fallen auf Stammes- und Zivilisationsebene.
2. **Kulturwandel mit Erinnerung**: Jede Kultur führt ein begrenztes
   Ereignisprotokoll deiner Eingriffe (mit Verfall), das ihre Entwicklung steuert.
3. **Gotteshand plus Ragdoll- und Verletzungsphysik**: hochheben, werfen,
   platzieren. Ragdoll nur für direkt betroffene Figuren. Verletzungen werden
   als Daten in die Simulation zurückgeschrieben.
4. **Götterkräfte, gespeist aus Hingabe und Ritualen**: im MVP mindestens vier,
   darunter Blitz, Fest-Segen und das Erschaffen neuer Erwachsener. Die
   Bevölkerung wächst über eine **passive Gnade** aus den Ritualen langsam von
   selbst, damit eine Welt nicht aussterben muss, wenn der Spieler nichts tut
   (Pillar 4: keine Pflichtaufgabe). Die Kraft „Erwachsene erschaffen“
   beschleunigt das gezielt. Kinder gibt es nie.
5. **Lust-Systeme hinter dem Zustimmungsmodell**: sexuelle Normen pro Kultur,
   Lust-Sekte, Harem, Rituale. Jede sexuelle Handlung muss durch ein einziges
   Zustimmungsmodell, das im Zweifel ablehnt (siehe Anti-Pillars).

Später: Zoomstufen bis in die 3D-Nahansicht und Besessen-Sein von Erwählten (PC).

---

## Player Motivation Profile

### Primary Psychological Needs Served

| Need | How This Game Satisfies It | Strength |
| ---- | ---- | ---- |
| **Autonomy** (freedom, meaningful choice) | Völlige Freiheit: Jeder Eingriff ist erlaubt, kein Ziel ist vorgegeben, und jede Entscheidung verändert eine Kultur sichtbar | Core |
| **Competence** (mastery, skill growth) | Lernen, Kulturen gezielt zu formen; die Chronik zeigt, dass ein Plan aufgegangen ist | Supporting |
| **Relatedness** (connection, belonging) | Erwählte als Lieblingsfiguren mit Namen und Geschichte; Anhänger, die dir ergeben sind | Supporting |

### Player Type Appeal (Bartle Taxonomy)

- [x] **Achievers**: Gotteskraft wächst, neue Kräfte werden freigeschaltet, Zivilisationen entwickeln sich über die Zeit
- [x] **Explorers**: Primär. Systeme verstehen und herausfinden, welche Eingriffe welche Kulturen erzeugen
- [ ] **Socializers**: Nicht bedient (privat, single-player)
- [x] **Killers/Competitors**: Teilweise, als Herrschaft über die Welt, Krieg und Zerstörung, nicht als PvP

Quantic Foundry: Fantasy, Destruction und Design stark; Story und Efficiency schwach.

### Flow State Design

- **Onboarding curve**: Die ersten 10 Minuten zeigen einen Stamm, die Gotteshand
  und eine Kraft. Der Spieler wirft jemanden und sieht sofort die Reaktion. Neue
  Kräfte kommen erst mit wachsender Gotteskraft.
- **Difficulty scaling**: Es gibt keine Schwierigkeit im klassischen Sinn. Die
  Komplexität wächst mit der Welt: mehr Stämme, mehr Kulturen, mehr Kräfte.
- **Feedback clarity**: Jeder Eingriff zeigt eine sofortige Reaktion (Pillar 2),
  und die Chronik zeigt, was er langfristig bewirkt hat.
- **Recovery from failure**: Es gibt kein Scheitern. Stirbt eine Kultur aus,
  erschafft der Gott neue Erwachsene, oder du lädst einen Spielstand.

---

## Core Loop

### Moment-to-Moment (30 seconds)
Kraft wählen oder mit der Hand zugreifen (Blitz, Segen, ein Mensch wird
hochgehoben und geworfen), dann sofort die Reaktion sehen: Ragdolls fliegen,
Verletzte humpeln, ein Dorf jubelt oder stürzt sich in ein Fest. Das trägt sich
selbst, weil die Rückmeldung unmittelbar ist, die Physik nie gleich ausfällt
und jede Kraft sowohl körperlich als auch sozial wirkt.

### Short-Term (5-15 minutes)
Einen Stamm beobachten, eingreifen oder einen Befehl geben, der nur ein Ziel
nennt („Erkläre Krieg“, „Baue einen Tempel“, „Erwähle diesen Menschen“). Den
Weg wählt die Simulation. Die Kultur reagiert, verändert sich und merkt sich
deinen Eingriff. „Noch ein Eingriff“ entsteht durch Ereignisse, die die Welt
meldet: Kriegserklärungen, Sektengründungen, Eifersucht im Harem.

*Ziel für die Eingriffsdichte* (Playtest Prototyp 1: „zu viel Warten, zu wenig
Eingreifen“): Der Spieler kann etwa **alle 30 Sekunden** eingreifen. Kosten der
Kräfte und Zufluss der Gotteskraft werden darauf abgestimmt. Die Gotteskraft
bremst die Eingriffe, nicht reine Wartezeit.

### Session-Level (30-120 minutes)
*Zeitskala* (Entscheidung nach Prototyp 1): Bei normaler Geschwindigkeit (1×)
vergehen etwa **120 simulierte Jahre pro Minute**. Eine **Ära** ist ein Kapitel
der Chronik von **25 Jahren**, bei 1× also etwa 12 Sekunden, und keine
Sitzungseinheit. Bei durchgehendem 1× umfasst eine Sitzung (30–120 Minuten) etwa
140–580 Ären; in der Praxis weniger durch Pausen und Eingriffe, mit 4× und 16×
mehr. Über viele Ären werden aus Stämmen Zivilisationen, sie schließen Bündnisse,
führen Kriege und gehen unter. Gute Stopppunkte sind große Wendepunkte (Gründung,
Krieg, Untergang, Wechsel der Gesellschaftsform) mit einem Eintrag in der
Chronik. Der Haken für die nächste Sitzung sind offene Konflikte und Erwählte,
deren Schicksal offen ist.

### Long-Term Progression
Die Gotteskraft wächst durch Hingabe und Rituale und schaltet neue Kräfte frei.
Zivilisationen entwickeln sich über viele Ären; größere **Zeitalter** mit eigenen
Inhalten (zum Beispiel von der Steinzeit zum Mittelalter) sind eine Ausbaustufe.
Es gibt kein festes Ende: Eine Welt
ist fertig, wenn du sie speicherst und eine neue beginnst.

### Retention Hooks
- **Curiosity**: Welche Kultur entsteht, wenn ich es diesmal anders mache?
- **Investment**: Die eigene Welt im Spielstand, Erwählte, ein Glaube, der sich ausbreitet
- **Social**: Keine (privat)
- **Mastery**: Eine Kultur gezielt dorthin lenken, wo man sie haben will

---

## Game Pillars

Pillar 1 und Pillar 4 ziehen gegen die Kontrolle des Spielers, Pillar 2 zieht
dafür. Diese Spannung ist das Spiel. Pillar 3 macht es unverwechselbar.

### Pillar 1: Die Welt lebt ohne dich
Menschen und Zivilisationen handeln selbst, auch wenn der Spieler nur zuschaut.

*Design test*: Handgemachte Bausteine, systemische Auslöser. Inhalte (Kulturvorlagen,
Festtypen, Rituale) dürfen von Hand gebaut sein, aber **ob und wann** sie passieren,
entscheidet die Simulation, nie eine Zeitleiste.
*Prüfmaßstab*: Lässt man die Welt allein, passiert bei 1× in jeder Minute
mindestens ein großes Ereignis (Krieg, Frieden, Eroberung, Abspaltung, Untergang
oder Wechsel der Gesellschaftsform). Prototyp 1: 94 % der Ären (je etwa 12
Sekunden) hatten mindestens eines.

### Pillar 2: Jeder Eingriff hinterlässt eine Spur
Macht muss man sehen und spüren: sofort, körperlich und gesellschaftlich, und
die Kultur erinnert sich später daran.

*Design test*: Wenn wir zwischen einer unsichtbaren Wertänderung und einer
sichtbaren Reaktion schwanken, gewinnt die sichtbare. Jeder Eingriff muss sich
später in der Kultur nachverfolgen lassen („Dieser Stamm wurde zum Gottesstaat,
weil ich in der Dürre einen Felsen auf seine Rivalen geworfen habe.“). Das
Gefühl der Gotteshand (hochheben, werfen) ist der Kern der Sensation in diesem
Pillar.

### Pillar 3: Lust ist Kultur, nicht Beiwerk
Sex ist mit den Systemen verwoben und verändert die Welt.

*Design test*: Wenn wir zwischen einer Szene, die für sich steht, und einer
Szene, die etwas in der Welt verändert, schwanken, gewinnt die zweite.
*Wirkungen von Lust in der Welt*: Gotteskraft (über Rituale, daraus entstehen
neue Erwachsene), Zusammenhalt und Glück, Kulturwandel, Diplomatie zwischen
Kulturen mit passenden oder gegensätzlichen Normen.
*Umfangsregel*: **Das System ist geschützt, die Darstellung darf einfach sein.**
Eine schlichte oder angedeutete Szene, die die Welt verändert, erfüllt den
Pillar. Eine aufwendige Szene ohne Wirkung erfüllt ihn nicht.

### Pillar 4: Gott, nicht Verwalter
Der Spieler entscheidet Großes, das Kleine regelt sich selbst.

*Design test*: Wenn der Spieler etwas manuell erledigen **müsste**, wird es
automatisiert oder gestrichen.
*Befehle*: Befehle nennen nur das Ziel, die Simulation wählt den Weg. Braucht
ein Befehl einen Folgebefehl, ist es Verwaltung.
*Besessen-Sein*: Es ist eine Linse und ein Stunt, kein Job. Die Welt läuft
weiter, Erwählte haben keine Bedürfnisse und kein Inventar, und das Verlassen
kostet nichts.

### Anti-Pillars (What This Game Is NOT)

- **NOT Mikromanagement**: keine Müllabfuhr, keine Leichenlogistik, keine
  Lieferketten. Das würde *Gott, nicht Verwalter* untergraben.
- **NOT eine feste Story oder Kampagne**: Das würde *Die Welt lebt ohne dich*
  untergraben.
- **NOT ein Lebenssimulator pro Person**: keine Bedürfnisse wie bei Sims für
  einzelne Figuren, auch nicht beim Besessen-Sein. Das würde *Gott, nicht
  Verwalter* untergraben.
- **NOT Fantasy- und Tiervölker im ersten Kern**: Zuerst Menschen. Fantasy- und
  Tiervölker (menschenähnliche, intelligente, erwachsene Wesen) sind eine spätere
  Erweiterung.

**Nicht verhandelbare Grenzen** (unabhängig von Umfang, Stufe oder Modus):

- **Keine Kinderfiguren.** Es gibt im Spiel keine Kinder. Die Bevölkerung wächst
  nur über Erwachsene, die der Gott erschafft. Technisch erzwungen: Die
  Morph-Regler sind so begrenzt, dass keine Kombination kindliche Proportionen
  ergibt (Größe, Kopf-Körper-Verhältnis). Ein Validierungstest sichert das ab.
- **Nur einvernehmliche sexuelle Inhalte.** Zustimmung entsteht nur aus den
  Eigenschaften der Figur und den Normen ihrer Kultur, nie aus Status, Hingabe
  oder einem Eingriff des Spielers.
- **Keine Götterkraft erzwingt Lust.** Keine Kraft setzt Begehren direkt oder
  zwingt jemanden zu einer sexuellen Handlung. Jemanden auf ein Fest zu setzen
  ist erlaubt. Was die Figur dann tut, entscheidet das Zustimmungsmodell.
- **Hingabe ist keine Zustimmung.** Eine sehr ergebene Anhängerin oder ein sehr
  ergebener Anhänger ist dadurch nicht verfügbar.
- **Gefangene und Versklavte sind aus allen sexuellen Systemen ausgeschlossen.**
  Harems bestehen nur aus freien Mitgliedern, die zustimmen. Ein Sklavenstaat kann
  keinen Harem aus Sklaven bilden. Wer als Harem-Mitglied im Krieg gefangen wird,
  fällt sofort heraus. Wer befreit wird, kommt nur über das normale
  Zustimmungsmodell zurück.
- **Gewalt und Sex überschneiden sich nie.** Ragdoll- und Verletzungsphysik gelten
  nie in einem sexuellen Kontext. Kampf- und Verletzungszustände sperren sexuelle
  Zustände und umgekehrt.
- **Keine gewöhnlichen Tiere in sexuellen Inhalten.**
- Umsetzung: eine einzige reine Prüffunktion, die im Zweifel ablehnt. Götterkraft,
  Hingabe und Status bekommt sie gar nicht als Eingabe. Sie wird mit vollständigen
  Unit-Tests und zufallsbasierten Property-Tests abgesichert.

---

## Visual Identity Anchor

**Richtung: Fleisch und Folgen**

**Visuelle Regel:** *Der Körper ist die Chronik. Jedes Ereignis hinterlässt ein
Zeichen an jemandem.*

Bodenständig, körperlich, schwer, näher an RDR2 und Wild Life: natürliches Licht
mit tiefer Sonne, Haut, die blaue Flecken bekommt, schwitzt, vernarbt und
blutet, warme und intime sinnliche Momente, Gewalt mit Gewicht. Aus der Ferne
sieht die Welt aus wie eine echte Landschaft von oben.

**Stützende Prinzipien:**

1. **Ein Skelett, ein Grundkörper, Morphs.** Alle Körper wirken klar erwachsen
   (etwa 7 bis 7,5 Kopflängen). Die Vielfalt entsteht über Morphs, Haut- und
   Haarpaletten und Kleidung, nie über getrennt modellierte Figuren.
   *Test:* Wenn ein neuer Körpertyp gebraucht wird, ist das dann ein Morph oder
   ein neues Modell? Ein Morph.
2. **Detailstufen statt Detail überall.** Volle Haut- und Wunddetails nur für die
   Figur, in der der Spieler steckt, und die ein bis drei Figuren nahe an der
   Kamera. Wunden sind Decals oder Atlas-Masken.
   *Test:* Wenn ein Effekt für alle Figuren oder nur für die nahen gelten soll,
   gilt er für die nahen.
3. **Spuren bleiben.** Verbrannter Boden, Ruinen, Narben, Denkmäler und
   veränderte Kleidung bleiben in der Welt. Eine Welt, die der Spieler geformt
   hat, sieht anders aus als eine, die er in Ruhe gelassen hat.
   *Test:* Wenn eine Folge nach dem Ereignis verschwinden oder bleiben soll,
   bleibt sie, solange das Budget es erlaubt.

**Farbphilosophie:**
- Eine natürliche Erdpalette. Die Haut erzählt die meiste Farbe.
- Kulturen erkennt man an Materialien statt an flachen Farbtönen: Seide und Gold
  für die Lust-Sekte, Eisen und Ruß für den Sklavenstaat, weißes Leinen für die
  Demokratie. Sie laufen über gemeinsame Materialparameter (ein Master-Material,
  Parametersätze).
- Rot gehört dem Blut. Lust bekommt warmes Kerzenlicht-Amber. Die beiden
  Farbsprachen bleiben getrennt, passend zur Grenze „Gewalt und Sex überschneiden
  sich nie“.
- Das Zeichen des Gottes ist kaltes Blauweiß, das einzige unnatürliche Licht in
  einer natürlichen Welt. Diese Farbe darf keine Kultur verwenden.

**Bekannte Kosten:** Das ist die teuerste der drei Richtungen, die der Art
Director vorgeschlagen hat. Halbrealistische Ragdolls rutschen leicht ins Uncanny
Valley, die Richtung ist auf Android schwer umzusetzen, und Kulturen, die man nur
an Materialien erkennt, sind aus der Ferne schwer lesbar (siehe Risks).

---

## Inspiration and References

| Reference | What We Take From It | What We Do Differently | Why It Matters |
| ---- | ---- | ---- | ---- |
| WorldBox | Zivilisationen, die von selbst entstehen, Kriege zwischen ihnen, Götterkräfte | 3D, halbrealistische Körper, Kulturen, die sich an deine Eingriffe erinnern | Belegt, dass das Zuschauen bei emergenten Zivilisationen lange fesselt |
| Black & White | Gotteshand: Menschen hochheben und werfen, Dörfer, die reagieren | Kein Gut/Böse-Schema, sondern Gesellschaftsformen und Lust-Systeme | Belegt, dass die Gotteshand als Kernverb trägt |
| Wild Life | Detaillierte, erwachsene 3D-Körper und sexuelle Inhalte | Sex ist Teil der Systeme, keine eigenständige Szene; Darstellung zuerst einfach | Zeigt, was der Spieler sich vom Detailgrad wünscht, und wie teuer er ist |
| Sims (mit Erwachsenen-Mod) | Menschen steuern, Dates, Beziehungen | Kein Lebenssimulator pro Person, Gott statt Verwalter | Zeigt den Reiz direkter Kontrolle über Menschen |
| Spore (Stammesphase) | Politik zwischen Stämmen | Ohne Evolution, nur Menschen | Zeigt den Reiz von Stammespolitik |
| RDR2 | Ragdoll- und Todesanimationen mit Gewicht | Für eine Gottesperspektive, nur für direkt betroffene Figuren | Maßstab dafür, wie Körperlichkeit sich anfühlen soll |
| People Playground | Verletzungssimulation, mit Körpern experimentieren | In 3D und eingebettet in eine Zivilisation | Belegt die Faszination von Physik-Experimenten |
| SimCity / Cities Skylines | Städte wachsen sehen | **Anti-Referenz:** kein Mikromanagement (Müll, Leichen) | Daran sind die Runs des Spielers bisher gescheitert |

**Non-game inspirations**: Historische Kulturen und Herrschaftsformen (Antike,
Byzanz, persische Höfe, mittelalterliche Theokratien) als Vorlage für
Gesellschaftstypen. Ameisenkolonien als Bild für eine Welt, die von selbst lebt.

---

## Target Player Profile

| Attribute | Detail |
| ---- | ---- |
| **Age range** | Erwachsen (18+); privates Projekt |
| **Gaming experience** | Mid-core bis Hardcore |
| **Time availability** | Unregelmäßig; Sitzungen von 30 bis 120 Minuten |
| **Platform preference** | PC, dazu das Handy |
| **Current games they play** | Minecraft, Diablo 4, Space Engineers |
| **What they're looking for** | Eine Sandbox, in der Macht, Kontrolle und sexuelle Fantasien Teil einer lebendigen Welt sind, und genug Inhalt, damit sie nicht schnell langweilig wird |
| **What would turn them away** | Mikromanagement, zu wenig Inhalt, eine geschriebene Story, die die Freiheit einschränkt |

Spielertyp: vor allem Creator und Explorer, mit Achiever-Anteil.

---

## Technical Considerations

| Consideration | Assessment |
| ---- | ---- |
| **Recommended Engine** | **Unity 6.3 LTS (C#)**, entschieden in `/setup-engine` am 2026-10-25: größtes 3D-Umfeld für ein erstes Spiel, Android später am einfachsten; Haut und Charaktere brauchen mehr Eigenarbeit als in Unreal, und URP gegen HDRP fällt per ADR in der Architekturphase (nach der Art Bible). Prototyp 2 lief nur in URP mit Grundkörpern und belegt für HDRP nichts. Kriterien vom Technical Director: Reife des Haut- und Charakter-Renderings, ein datenorientierter Pfad für viele Agenten, Stabilität der Ragdolls, Leistung beim Skinning vieler Meshes mit Blend Shapes, der Android-Renderer und die Lernkurve für ein erstes Spiel. Die Engine, die halbrealistische Haut am besten kann, passt am schlechtesten zu Android und zu einem Einsteiger, und umgekehrt |
| **Key Technical Challenges** | Die Simulation ist die Autorität und abstrakt, 3D ist nur eine Ansicht (Figuren werden nur nahe der Kamera als 3D-Körper erzeugt). Dazu Ragdolls für viele Figuren, die Zivilisations-KI, das Zustimmungsmodell, Haut und Wunden in Detailstufen, Speichern und Laden einer emergenten Welt |
| **Art Style** | 3D halbrealistisch („Fleisch und Folgen“), mit Detailstufen |
| **Art Pipeline Complexity** | High (eigene 3D-Arbeit). Gesenkt durch einen offenen Grundkörper (z. B. MakeHuman/MPFB, CC0) und vorhandene Animationen; die Lizenz wird jeweils geprüft |
| **Audio Needs** | Moderate (Umgebung, Reaktionen der Menschen, Feedback zu den Kräften) |
| **Networking** | None |
| **Content Volume** | MVP: eine Insel, 2 Stämme, 30–50 benannte Einzelne in einem Volk von anfangs 96 Köpfen (`design/gdd/menschen-und-bevoelkerung.md`), 4 Kräfte, 2 Kulturrichtungen, Harem und Rituale, einfache Szenen. Volle Vision: viele Zivilisationen, 5+ Gesellschaftsformen, Ären, Dutzende Kräfte |
| **Procedural Systems** | Zivilisations-Simulation, Kulturwandel, Körpervariation über Morphs; Inseln vorerst von Hand oder einfach generiert |

Geplante kritische Architekturentscheidungen (ADRs), unabhängig von der Engine:
(1) Die Simulation ist die Autorität und läuft deterministisch und ohne Grafik,
3D ist nur eine Ansicht. (2) Das Zustimmungsmodell ist eine reine Funktion, die
im Zweifel ablehnt, und wird mit Property-Tests geprüft.

Startbudgets: PC 60 fps, höchstens 8–16 aktive Ragdolls,
etwa 20–30 Figuren in voller Detailstufe. *Prototyp 2:* 49 aktive Ragdolls aus
Grundkörpern liefen im Editor mit Ø 3,8 ms und 5,0 ms im 99. Perzentil. Das Budget
für Ragdolls ist also mit einfachen Körpern kein Engpass. Mit Skinned Meshes, Blend
Shapes und Haut ist es neu zu messen, bevor es angehoben wird (mit dem offenen
Grundkörper im MVP-0, sobald er mit Ragdoll steht).

Android: 30 fps, 2–4 aktive Ragdolls. Entscheidungen pro Agent mit 5–10 Hz, Logik
für Stämme und Zivilisationen mit etwa 1 Hz.

---

## Risks and Open Questions

### Design Risks
- Emergente Simulationen kippen leicht in langweilige Zustände: Alle sterben aus,
  ein Stamm gewinnt nach 20 Minuten, oder nichts passiert. Gegenmittel: ein
  Schnellvorlauf ohne Grafik für das Balancing und Debug-Anzeigen ab Tag 1.
- Das Besessen-Sein kann das Spiel unbemerkt in einen Lebenssimulator
  verwandeln. Gegenmittel: Pillar 4 und der Anti-Pillar.
- Wenn die Welt sehr belebt ist, gehen die Spuren des Spielers darin unter
  (Pillar 1 gegen Pillar 2).
- Ohne feste Liste von Wirkungen wird Lust zur Dekoration. Gegenmittel: Pillar 3
  mit der Liste der Wirkungen.
- **Krieg überdeckt den Kulturwandel** (Prototyp 1: 1620 Kriegserklärungen gegen
  393 Kulturwechsel in 100 Welten, und die Chronik wirkte eintönig). Anforderung:
  Ereignisarten brauchen Vielfalt, und Kulturwandel darf nicht im Krieg untergehen.
- **Die Chronik kann bei hoher Ereignisdichte überfordern**: Bei 120 Jahren pro
  Minute kommt im Prototyp etwa alle 3 Sekunden ein großes Ereignis. Die Chronik
  braucht Filter (im Prototyp „nur wichtige“) und Gewichtung; die Lesbarkeit ist im
  MVP-0 zu prüfen (Prototyp 2 hatte keine Chronik) und gehört in den UX-Spec zur
  Chronik.
- **Ein Segen kann den Stamm zur Zielscheibe machen**, weil Nachbarn ihn als
  Frevel sehen (im Playtest starb ein gesegneter Stamm plötzlich aus, ohne dass
  der Grund sichtbar war). Als Geschichtenquelle erwünscht, aber die Chronik muss
  die Ursache sichtbar erklären (Pillar 2).
- **Die Gotteshand allein trägt nur 5–10 Minuten** (Prototyp 2: „Nur Leute werfen
  macht nach ein paar Minuten keinen Spaß mehr“). Anforderung: Jeder Wurf muss
  Folgen in der Simulation haben (Erinnerung der Kultur, Reaktion der Stämme,
  Kulturwandel, Krieg). Die Hand ist der 30-Sekunden-Loop, die Folgen tragen die
  längeren Loops (Pillar 2).

### Technical Risks
- **TD-FEASIBILITY: HIGH RISK** für den ursprünglichen Umfang. Reaktion: PC zuerst
  und ein Vertical Slice; Android und nahtloser Zoom kommen später.
- Der Look „Fleisch und Folgen“ für viele Figuren und auf Android: Grafikspeicher,
  Uncanny Valley bei Ragdolls, Kleidung, die zu jedem Morph passen muss.
- Sexuelle Szenen: Paar-Animationen für variable Körper sind das teuerste
  Animationsproblem. Physikbasierte Szenen sind ausgeschlossen; Paar-Animationen
  wandern in den Ausbau.
- Der nahtlose Zoom ist nur mit der Architektur „Simulation ist die Autorität“
  machbar. In v1 gibt es 2 bis 3 feste Zoomstufen mit Übergang.
- Oft unterschätzt: Animationsinhalte, Speichern und Laden, Debug-Werkzeuge,
  Wegfindung für viele Agenten, das Gefühl der Gotteshand, Strategie-UI,
  Kamerasystem.

### Market Risks
- Nicht zutreffend, da das Spiel privat ist und nicht verkauft wird. Sollte es je
  veröffentlicht werden: Store- und Zahlungsanbieter-Regeln für Erwachseneninhalte
  (Steam, itch.io und die Zahlungsanbieter haben 2025 viele Erwachsenenspiele
  entfernt), und auf Android kein Play Store.

### Scope Risks
- **PR-SCOPE: OPTIMISTIC.** Die ursprüngliche MVP-Schätzung von 2–3 Monaten war
  etwa 2,5-mal zu niedrig. Reaktion: das MVP in MVP-0 und MVP-1 geteilt,
  Rigor auf `standard` gesenkt.
- Erstes Spiel, allein, bei 10–20 Stunden pro Woche (Angabe des Entwicklers,
  2026-10-25): Alle Vollzeit-Schätzungen verlängern sich etwa um den Faktor 2 bis 4,
  bei unter 10 Stunden um 4 bis 5. Die Umrechnung steht in `production/roadmap.md`.
- Das wahrscheinlichste Scheitern ist Steckenbleiben im MVP, weil die 3D-Ansicht
  sich zieht und die Motivation sinkt. Gegenmittel: die 3D-Ansicht und die
  Gotteshand mit einfachen Körpern früh bauen, damit es bei jedem Schritt etwas
  Spielbares gibt.
- In v1 dürfen Besessen-Sein und Paar-Animationen nicht gleichzeitig gebaut
  werden.
- Die Producer-Schätzung für MVP-0 (3–4 Monate) galt für **eine** Kraft. Mit 4 Kräften
  (Entscheidung nach Prototyp 1) ist sie neu zu schätzen (`/estimate`).

### Open Questions
- Ist ein fertiger Grundkörper für den Look in MVP-0 gut genug? Die Entscheidung
  trifft der Creative Director, sobald der Prototyp-Körper steht.
- Wie genau funktioniert das Begehrensmodell hinter der Zustimmung (Eigenschaften,
  kulturelle Normen)? Das klärt das GDD zum Zustimmungs- und Lust-System.
- Wie bleiben Kulturen, die man nur an Materialien erkennt, aus der Ferne lesbar?
  Das klärt die Art Bible, eventuell mit einem farbigen Banner-Akzent.
- Welches Android-Gerät ist das Zielgerät? Das wird erst festgelegt, wenn die
  Android-Stufe beginnt.
- Engine: **Unity 6.3 LTS** (C#), entschieden in `/setup-engine` am 2026-10-25.
- URP oder HDRP? Das klärt eine ADR in der Architekturphase, nach der Art Bible.
- Was ist die vierte Kraft neben Blitz, Fest-Segen und Erwachsene erschaffen?
  Das klärt das GDD zum Kräfte-System.

---

## MVP Definition

**Core hypothesis**: Eine Insel mit zwei Stämmen, die von selbst leben und auf die
ich mit der Gotteshand und Kräften einwirke, erzeugt Geschichten, die ich sehen
will. Die Gotteshand mit Ragdoll macht schon nach 30 Sekunden Spaß.
*Stand: Der erste Teil ist durch Prototyp 1 gestützt, der zweite durch Prototyp 2
(beide siehe unten). Offen ist, ob beide Teile zusammen tragen: Das zeigt erst das
MVP-0.*

**Ergebnis Prototyp 1** (HTML, 2026-10-25, Bericht in
`prototypes/weltengott-concept/REPORT.md`, Urteil PROCEED):
- Zeitskala: Die Messwerte gelten für 120 simulierte Jahre pro Minute bei 1× (eine
  Ära von 25 Jahren dauert etwa 12 Sekunden), die auch die Skala des Spiels ist
  (siehe Session-Level-Loop). Bei einer langsameren Skala wären Ereignisse und
  Folgen entsprechend seltener zu sehen und müssten neu geprüft werden.
- Messbare Kriterien: Ohne Eingriff erzeugt jede Ära mindestens 1 großes Ereignis
  und die Welt stirbt nicht aus. Mit Eingriffen lassen sich mindestens 2
  Kulturveränderungen in der Chronik auf den Spieler zurückführen.
- Messwerte (100 Welten × 10 Ären): 0 % sterben aus, 4,4 große Ereignisse pro Ära,
  56 % der Welten erfüllen das erste Kriterium vollständig (die übrigen haben
  mindestens eine ruhige Ära, meist die erste). Mit einem programmierten
  Testspieler (etwa 4 Eingriffe) lassen sich in 89 von 100 Welten mindestens 2
  große Ereignisse auf den Spieler zurückführen.
- Playtest (1 Spieler): Hypothese bestätigt, Bindung an den gesegneten Stamm,
  Wunsch nach einer neuen Welt. Aber: „zu flach“ (nichts Sichtbares, zu wenige
  Kräfte, eintönige Chronik, zu viel Warten). Das führte zu 4 Kräften im MVP und
  zum Ziel der Eingriffsdichte oben.

**Ergebnis Prototyp 2** (Unity, 2026-10-25, Bericht in
`prototypes/gotteshand-concept/REPORT.md`, Urteil PROCEED):
- Messbare Kriterien: erster Wurf innerhalb von 30 Sekunden, mindestens 5 Minuten
  freiwillig weitergespielt, mindestens 3 von 5 Würfen höchstens 3 m vom Zielring,
  Bildzeit im Editor mit 50 Menschen und 16 Ragdolls im Mittel höchstens 16,6 ms
  und im 99. Perzentil höchstens 33 ms.
- Ergebnis: alle vier erfüllt. Erster Wurf nach etwa 20 Sekunden, Spaß für 5–10
  Minuten, alle Würfe im Zielring (Werte nach Angabe des Spielers), Stresstest
  Ø 3,8 ms und 5,0 ms im 99. Perzentil bei bis zu 49 Ragdolls.
- Playtest (1 Spieler): „sehr intuitiv“, der beste Moment ist ein extrem weiter
  Wurf. Aber allein trägt das Werfen nur 5–10 Minuten. Daraus folgt die Anforderung,
  dass jeder Wurf Folgen in der Simulation hat (siehe Design Risks).

**Prototyp-Reihenfolge mit Abbruchkriterien** (vom Technical Director):
1. Die Simulation ohne Grafik, mit Schnellvorlauf. *Abbruch*, wenn in 10
   simulierten Ären keine spannende Geschichte entsteht. *Erledigt: PROCEED.*
2. Gotteshand und Ragdoll mit 50 Menschen am PC. *Abbruch*, wenn der
   30-Sekunden-Loop keinen Spaß macht oder das Budget für die Bildzeit nicht
   hält. *Erledigt: PROCEED.*
3. (Erst für die Android-Stufe) Haut-Test auf dem Zielgerät. *Abbruch* bei unter
   30 fps mit 10 Figuren.

**Required for MVP** (MVP-0, „Gott-Spielzeug“):
1. Die Simulation ohne Grafik: deterministischer Tick, 2 Stämme, Ären und Krieg
   zwischen Stämmen (alles abstrakt, als Daten wie in Prototyp 1)
2. Einfacher Kulturwandel auf einer Achse (Lust-Sekte oder Gottesstaat) mit
   Erinnerung an Eingriffe
3. Das Zustimmungsmodell als reine Funktion mit Tests, von Anfang an
4. Begehren und Lust mit einfachen, sichtbaren Szenen auf dem Grundkörper
   (Ausblende oder einfache Animation, keine Paar-Animationen), dazu Harem und
   Rituale als spielbare Systeme. Vom Entwickler am 2026-10-25 aus v1 vorgezogen;
   die nicht verhandelbaren Grenzen gelten unverändert
5. Ein offener Grundkörper ohne Morphs, mit vorhandenen Animationen
6. Gotteshand mit Ragdoll für gehaltene oder geworfene Figuren. Hand-Aktionen
   (Packen, Werfen, Ablegen, Verletzung) zählen als Eingriffe: Sie gehen ins
   Ereignisprotokoll der betroffenen Kultur und ihre Wirkung ist in der Chronik
   nachvollziehbar. Nur so tragen sie über die ersten Minuten hinaus (Prototyp 2:
   Werfen allein trägt 5–10 Minuten). Die Gewichtung klärt das GDD zur Gotteshand
   und zum Kulturwandel.
7. Vier Kräfte: Blitz, Fest-Segen, Erwachsene erschaffen (gespeist aus Ritualen)
   und eine vierte (offen), dazu die passive Gnade für die Bevölkerung
8. Eine Zoomstufe
9. Speichern und Laden, Zeitsteuerung (Pause, Schnellvorlauf) und eine Chronik

**MVP-1** (danach): weitere Kräfte, eine zweite Zoomstufe, Morphs und Look B,
das volle System für Kulturwandel und Erinnerung.

**Explicitly NOT in MVP** (defer to later):
- Besessen-Sein von Erwählten
- Sichtbarer Krieg: Schlachten und Kampfszenen in 3D (kommt in v1). Abstrakter
  Krieg in der Simulation ist Teil von MVP-0, weil der Beleg aus Prototyp 1 und
  der Prüfmaßstab von Pillar 1 daran hängen
- Paar-Animationen und detaillierte sexuelle Szenen (im MVP nur einfache Szenen auf dem Grundkörper)
- Android
- Nahtloser Zoom
- Weitere Gesellschaftsformen (Demokratie, Monarchie mit Harem, Sklavenstaat)
- Fantasy- und Tiervölker

### Scope Tiers (if budget/time shrinks)

Alle Zeiten sind **Vollzeit-Monate, solo**. Bei 10–20 h/Woche ist mit dem
Faktor 2 bis 4 zu rechnen, bei unter 10 h/Woche mit 4 bis 5. Jede Stufe steht für sich als spielbares Spiel.

| Tier | Content | Features | Timeline |
| ---- | ---- | ---- | ---- |
| **MVP-0 („Gott-Spielzeug“)** | Eine Insel, 2 Stämme, 30–50 Einzelne in einem Volk von anfangs 96 | Simulation, einfacher Kulturwandel, Zustimmungsmodell, Begehren und Lust mit einfachen Szenen, Harem und Rituale, Gotteshand und Ragdoll, 4 Kräfte, 1 Zoomstufe, Speichern, Zeitsteuerung, Chronik | ca. 6,5 Monate Vollzeit (`/estimate` 2026-10-25, siehe `production/roadmap.md`) |
| **MVP-1** | Wie MVP-0 | Weitere Kräfte, zweite Zoomstufe, Morphs und Look B, volles System für Kulturwandel und Erinnerung | +2–3 Monate |
| **Vertical Slice (v1)** | 1–3 Stämme, 30–80 Menschen | In dieser Reihenfolge: sichtbarer Krieg, Haut in Detailstufen, Besessen-Sein (Harem und Rituale sind seit 2026-10-25 im MVP-0). Die Kräfte aus MVP-1, 2–3 Zoomstufen | +6–10 Monate |
| **Alpha (Ausbau)** | Weitere Gesellschaftsformen, Zeitalter mit Inhalten | Mehr Kräfte, Paar-Animationen (mit eigener Entscheidung, ob man sie baut), mehr Animationen | +6–12 Monate |
| **Android (optional)** | Gleiche Simulation | Draufsicht, Touch-Gotteshand, einfacher Look; Einstieg erst nach dem Haut-Test auf dem Gerät | eigene Stufe |
| **Full Vision** | Viele Zivilisationen, Fantasy- und Tiervölker | Nahtloser Zoom, detaillierte Szenen, tiefe Verletzungssimulation | 30–48 Monate insgesamt |

**Wahrscheinlichster Abbruchpunkt:** in v1. Wird v1 in der Reihenfolge oben
gebaut, bleibt nach dem sichtbaren Krieg schon ein gutes Gottspiel übrig, denn
Harem und Rituale sind dann bereits seit MVP-0 drin.

---

## Next Steps

Prototype-first (beide Teile der Kernmechanik sind einzeln bewiesen):

- [x] Engine wählen und konfigurieren (`/setup-engine`): Unity 6.3 LTS
- [x] Prototyp 1 (Simulation ohne Grafik): PROCEED, siehe `prototypes/weltengott-concept/REPORT.md`
- [x] Konzept prüfen (`/design-review design/gdd/game-concept.md`): vier Reviews, jeweils NEEDS REVISION und danach überarbeitet (zuletzt: Hand-Aktionen als Eingriffe im MVP-0)
- [x] Prototyp 2 (Gotteshand und Ragdoll in Unity): PROCEED, siehe `prototypes/gotteshand-concept/REPORT.md`
- [x] Phasen-Check (`/gate-check`): CONCERNS, Risiken akzeptiert, Phase Systems Design (Bericht in `production/gate-checks/2026-10-25-concept-to-systems-design.md`)
- [x] Konzept in Systeme zerlegen (`/map-systems`): 33 Systeme, 16 im MVP, siehe `design/gdd/systems-index.md`
- [ ] MVP-0 neu schätzen (`/estimate`)
- [ ] Art Bible (`/art-bible`), vor der Architektur (URP oder HDRP)
- [ ] Jedes System entwerfen (`/design-system [system-name]`), das Zustimmungs- und Lust-System früh
- [ ] Architektur (`/create-architecture`), dann die kritischen ADRs: Simulation ist die Autorität, Zustimmungsmodell als reine Funktion
- [ ] Vertical Slice in der Pre-Production bauen (`/vertical-slice`)
- [ ] Ersten Meilenstein planen (`/sprint-plan new`)
