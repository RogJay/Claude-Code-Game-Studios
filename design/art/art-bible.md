# Art Bible: Allmacht

> **Status**: Draft
> **Owned By**: art-director
> **Last Updated**: 2026-10-26
> **Art Director Sign-Off (AD-ART-BIBLE)**: [Not yet reviewed]

`/art-bible` writes these nine sections in this order and replaces each
section's `[To be designed]` line as that section is approved. A section still
holding its placeholder is incomplete; a later run fills only those.

## 1. Visual Identity Statement

**Visuelle Regel:** *Der Körper ist die Chronik: Jedes Ereignis hinterlässt ein ablesbares
Zeichen, an einem Körper, wenn es einen betrifft, sonst in der Welt, und im Zweifel gewinnt
das Zeichen vor dem Detail.*

„Ablesbar“ heißt: ohne Interface und aus der Kameradistanz, in der der Spieler es braucht.
Die Regel baut auf „Fleisch und Folgen“ auf (Konzept, Visual Identity Anchor) und macht aus
der Haltung ein Entscheidungswerkzeug. Sie sagt, wohin das knappe Budget eines Entwicklers
fließt (in Zeichen), und sie gilt für alles, was die Kamera zeigt, vom blauen Fleck bis zum
verbrannten Hang. Das Ergebnis ist die Haltung der Richtung: bodenständig, körperlich,
schwer, natürliches Licht mit tiefer Sonne, Haut, die schwitzt, vernarbt und blutet, und aus
der Ferne eine Welt, die wie eine echte Landschaft von oben aussieht.

**Stützende Prinzipien:**

1. **Zeichen bleiben, und sie werden vereinfacht statt gelöscht.** Wunden, Blutergüsse,
   Narben, Ruß, verbrannter Boden, Ruinen, Denkmäler und veränderte Kleidung sind Zustand
   der Welt, nicht Effekt eines Moments. Weil die Simulation die Autorität ist und 3D nur
   eine Ansicht, kostet das Merken nichts, und nur die Darstellung braucht Budget. Gerät sie
   unter Druck, wird sie gröber, aber sie verschwindet nicht. Eine Welt, die der Spieler
   geformt hat, sieht dadurch anders aus als eine, die er in Ruhe gelassen hat, und genau
   das macht Eingriffe später in der Kultur nachverfolgbar.
   *Test:* Wenn unklar ist, ob eine Folge nach dem Ereignis bleiben oder verschwinden soll,
   bleibt sie. Muss sie aus Budgetgründen weichen, wird sie vereinfacht (Decal oder
   Atlas-Maske, dann Materialparameter, dann Bodenfärbung), nie entfernt.
   *Dient:* Pillar 2, Jeder Eingriff hinterlässt eine Spur.

2. **Die Nähe bekommt Haut, die Ferne bekommt Lesbarkeit.** Volle Haut- und Wunddetails
   bekommen nur die Figur, in der der Spieler steckt, und die ein bis drei Figuren nahe an
   der Kamera. Alle anderen müssen aus der Gottesperspektive als lebendig und handelnd
   erkennbar sein, über Silhouette, Farbe und Bewegung. Eine Welt, die ohne den Spieler
   lebt, muss vor allem in der Ferne glaubhaft sein, wo das Detailbudget nicht hinreicht;
   eine Figur, die in der Ferne nur durch Poren lesbar wäre, ist dort nicht lesbar.
   *Test:* Wenn unklar ist, ob ein Detail für alle Figuren oder nur für die nahen gelten
   soll, gilt es nur für die nahen. Was in der Ferne nicht als Silhouette, Farbe oder
   Bewegung lesbar ist, wird dort weggelassen und nicht verkleinert.
   *Dient:* Pillar 1, Die Welt lebt ohne dich.

3. **Drei Farbsprachen, nie vermischt.** Rot gehört dem Blut, Lust bekommt warmes
   Kerzenlicht-Amber, und das Zeichen des Gottes ist kaltes Blauweiß, das einzige
   unnatürliche Licht, das keine Kultur verwenden darf. Alles Übrige läuft über eine
   natürliche Erdpalette, in der die Haut die meiste Farbe erzählt, und Kulturen zeigen sich
   über Materialien (Seide und Gold, Eisen und Ruß, weißes Leinen) statt über flache
   Farbtöne. Lust erscheint damit als eigene Kultur mit eigenem Licht und eigenen Stoffen,
   nicht als Körperdetail. Das hält die Darstellung einfach und lässt dem System, das
   Pillar 3 schützt, die Aufmerksamkeit. Es trennt zugleich Gewalt und Sex im Bild, so wie
   die nicht verhandelbare Grenze sie im System trennt.
   *Test:* Wenn unklar ist, wie eine Lust-Szene oder eine Lust-Kultur lesbar werden soll,
   wähle Kerzenlicht-Amber, Material und eine sichtbare Folge in der Welt statt mehr
   Körperdetail. Wenn unklar ist, welche Farbe ein Effekt bekommt, entscheidet seine
   Bedeutung: Blut rot, Lust amber, Gott blauweiß, alles andere Erdpalette.
   *Dient:* Pillar 3, Lust ist Kultur, nicht Beiwerk (Umfangsregel: das System ist
   geschützt, die Darstellung darf einfach sein).

**Was die Regel nicht erlaubt:** Keine Figur, die nach einem Ereignis aussieht wie davor, und
kein Zeichen, das nur in einer Zahl, einem Icon oder einem Tooltip lebt: Ein Eingriff, den
man weder am Körper noch in der Welt sieht, gilt als nicht geschehen. Keine Körperlichkeit
ohne Folge und keine vermischten Farbsprachen: Ein Körper, der nur schön, nackt oder
verletzt ist, ohne dass die Welt etwas daran abliest, ist Beiwerk und wird nicht gebaut; kein
Rot oder Blut kommt in eine Lustdarstellung, kein Kerzenlicht-Amber über Gewalt und kein
Blauweiß in eine Kultur.

**Bekannte Kosten und Umgang im Meilenstein:** Das ist die teuerste der drei Richtungen
(Uncanny Valley bei halbrealistischen Ragdolls, schwer auf Android, Kulturen an Materialien
aus der Ferne schwer lesbar). Der Meilenstein „Erste spielbare Version“ zahlt davon nichts,
sondern beweist nur die Regel selbst: Schon Kapseln tragen Zeichen (Blut-Rot, Amber,
Blauweiß, Spuren im Boden). Haut, Uncanny Valley, Android-Machbarkeit und Fernlesbarkeit der
Kulturen werden erst gemessen und entschieden, wenn der lizenzierte Grundkörper (z. B.
MakeHuman/MPFB, CC0) mit Ragdoll steht.

*Abweichungen vom Konzept-Anker (2026-10-26):* Die Regel ist um „sonst in der Welt“ und „das
Zeichen vor dem Detail“ erweitert; „solange das Budget es erlaubt“ ist durch „vereinfachen,
nie entfernen“ ersetzt. Das Prinzip „Ein Skelett, ein Grundkörper, Morphs“ (mit 7 bis 7,5
Kopflängen, nicht verhandelbar) steht als Pipeline-Regel in Abschnitt 5.

## 2. Mood & Atmosphere

[To be designed] — for each major game state (exploration, combat, victory,
defeat, menus — whichever this game has): the emotional target, lighting
character (time of day, colour temperature, contrast), 3–5 atmospheric
descriptors, and energy level. Each state must look distinct from the others.

## 3. Shape Language

[To be designed] — character silhouette philosophy (readable at thumbnail
size, one distinguishing trait per archetype), environment geometry, UI shape
grammar, and hero versus supporting shapes.

## 4. Color System

[To be designed] — a primary palette of 5–7 colours with their meaning in this
world, semantic colour usage, per-area colour temperature rules, the UI palette
and where it diverges, and colourblind safety: which semantic colours need a
shape, icon or sound backup.

## 5. Character Design Direction

[To be designed] — the player character's visual archetype, how players tell
enemies, NPCs and allies apart at a glance, expression and pose style, and how
much detail survives at game camera distance (LOD philosophy).

## 6. Environment Design Language

[To be designed] — architectural style and its tie to the world's culture,
texture philosophy (painted, PBR or stylised, and why), prop density rules per
area type, and environmental storytelling guidelines.

## 7. UI/HUD Visual Direction

[To be designed] — diegetic versus screen-space HUD, typography (personality,
weight, size hierarchy, minimum readable size), iconography style, UI animation
feel, and any art-direction versus readability conflict and how it was decided.

## 8. Asset Standards

[To be designed] — file formats, naming convention, texture resolution tiers,
LOD levels, export settings, and the engine's hard limits (poly budgets per
asset category, texture memory, material slots). Where an art preference and a
technical limit disagree, record both and the trade-off chosen.

## 9. Reference Direction

[To be designed] — 3–5 references (games, films, art styles or artists). For
each: the specific element to take from it, and what to avoid so the result
does not read as a copy.
