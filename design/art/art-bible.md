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

Stimmung entsteht in *Allmacht* auf zwei Ebenen: dem Grundlicht der Insel und den
Zustandslichtern, die an einem Ort entstehen. Drei Regeln halten beides beisammen.

1. **Was lokal geschieht, färbt lokal, und nur was die ganze Insel betrifft, färbt das ganze
   Bild.** Fest, Ritual, Gotteshand, Kräfte und Krieg bekommen ihr Licht dort, wo sie
   stattfinden. Nur die Dürre (wenn sie inselweit herrscht) sowie Chronik und Menüs als
   eigene Bildschirme verändern das ganze Bild. Bei 16× laufen viele Ereignisse
   gleichzeitig. Würde jedes Fest das ganze Bild umfärben, flackerte der Bildschirm, und die
   übrige Welt sähe nicht mehr so aus, als lebte sie weiter (Pillar 1).
2. **Darstellungszeit statt Simulationszeit.** Bei 120 Jahren pro Minute würde ein echter
   Tag-Nacht-Wechsel stroboskopisch flackern. Das Grundlicht ist deshalb ein stehender
   später Nachmittag (Entscheidung des Entwicklers vom 2026-10-26). Die Tageszeit ist ein
   Stimmungsparameter des Zustands: Das Fest bringt eine lokale Dämmerung, die Dürre hebt die
   Sonne an. Übergänge dauern unabhängig vom Zeitraffer mindestens etwa 1,5 s Echtzeit.
3. **Der Zeitraffer verändert die Bewegung, nicht das Licht.** Wolkenschatten, Rauch und
   Menschen werden schneller, Lichtrichtung und Farbe bleiben. So wirkt 16× wie eine
   schnellere Welt und nicht wie eine andere Welt.

Energieniveau: 1 = ruhend, 5 = Ausbruch.

| Zustand | Emotionales Ziel | Lichtcharakter | Atmosphäre | Energie | Trägt die Stimmung |
|---|---|---|---|---|---|
| **Welt zusehen** (Ferne, 1× bis 16×) | Gelassene Übermacht mit Neugier: eine Welt, die mich nicht braucht und die ich trotzdem anfassen will | Tiefe Nachmittagssonne (etwa 25–30° Höhe, ~4000 K), warm, mittlerer Kontrast, natürliche Sättigung; Schatten leicht kühl, aber dunkel | weit, sonnenschwer, geschäftig, landschaftlich, ruhig | 2 (die Bewegung steigt mit dem Zeitraffer, das Licht nicht) | Lange Schlagschatten der Figuren; Wolkenschatten, deren Tempo den Zeitraffer zeigt |
| **Nahblick** (Zoom auf 1–3 Figuren) | Nähe und Verantwortung: Das sind Menschen, und meine Eingriffe stehen ihnen ins Gesicht geschrieben | Dieselbe Sonne als Streif- oder Gegenlicht, das Haut und Zeichen modelliert; weicher Kontrast im Gesicht, Tiefenunschärfe | nah, körperlich, warm, verletzlich | 2 | Streiflicht auf Narben, Schweiß und Blutergüssen; unscharfe Welt dahinter |
| **Gotteshand** (Packen, Heben, Werfen, Ragdoll) | Rohe Macht mit Gewicht und einem Stich Schuld | Die Welt bleibt warm. Nur die gepackte Figur bekommt kaltes blauweißes Kantenlicht, das einzige kalte Licht im Bild; hoher lokaler Kontrast | gespannt, schwer, übermenschlich, kalt in warm | 4, beim Wurf und Aufprall 5 | Blauweißer Bodenring und Lichtfaden; der Schlagschatten verrät die Höhe; der Aufprall hinterlässt Staub und eine Furche, die bleibt |
| **Kraft: Blitz** | Ehrfurcht und Schrecken: eine Strafe, die bleibt | 2–3 Frames weißblauer Spitzenblitz, extremer Kontrast; danach kurz orange Glut, dann dauerhaft Ruß | grell, plötzlich, endgültig, versengt | 5, fällt binnen Sekunden auf 2 | Schwarzer Brandfleck mit Rußrand, der in der Welt bleibt |
| **Kraft: Erwachsene erschaffen** | Staunen: Schöpfung ohne Geburt | Schmale kühle Lichtsäule, in der eine erwachsene Figur steht; die Säule verblasst, und die warme Sonne übernimmt die Figur | still, hell, rein, feierlich | 2 | Ein Ring aus niedergedrücktem Gras, der als Spur bleibt |
| **Fest** (mit dem Fest-Segen als Auftakt) | Wärme, Zugehörigkeit, Verlockung: Lust als Gemeinschaft mit Folgen, nicht als Schauspiel | Lokale Dämmerung um den Festplatz, Kerzenlicht-Amber (~2000 K) aus vielen kleinen Punktquellen; innen niedriger Kontrast, die Ränder fallen ins Dunkel | warm, dunstig, rhythmisch, golden, intim | 3–4, wiegend statt hektisch | Kreis aus Kerzen und Feuerschalen, Stoffbahnen, warmer Dunst; danach bleiben Kerzenreste und Kränze liegen |
| **Ritual** | Andacht und Spannung: Hingabe, die den Gott nährt | Gedämpft, neutral bis kühlgrau (~5000 K, Sonne abgeschwächt), ein heller Fokus in der Mitte, Kontrast zur Mitte hin gebündelt | still, geordnet, senkrecht, feierlich, gespannt | 2 | Senkrechte Rauchsäule; aufsteigende blauweiße Funken als Gotteskraft, die beim Spieler ankommt |
| **Krieg** (in der Simulation abstrakt, sichtbar erst später) | Gewicht und Verlust: Gewalt mit Folgen, kein Spektakel | Sonne durch Staub und Rauch gefiltert, flach und entsättigt; Blut-Rot ist die einzige gesättigte Farbe; harter Kontrast nur am Brand | staubig, roh, chaotisch, rußig, schwer | 5 | Staub- und Rauchschleier, Blut auf Erde. Im MVP-0 nur die Folgen: Ruinen, Ruß, Verletzte, Grabhügel |
| **Dürre und Hunger** | Schleichendes Unbehagen, Druck zum Eingreifen, Schuld durch Zusehen | Steilere, bleiche Sonne (45–55°, ~5800 K), kürzere und härtere Schatten, deutlich weniger Sättigung, gelblicher Hitzedunst | bleich, flirrend, trocken, träge, ausgezehrt | 1 | Bodenrisse, verdorrte Pflanzen, Staubfahnen, langsamere Bewegung; in der Nähe hagere Körper |
| **Kulturwandel** | Erkenntnis: „Das habe ich verursacht.“ | Kein eigenes Licht. Eine Kultur färbt über Material und eigene Lichtquellen, nie über das Color Grading | umbrechend, zögernd, sichtbar, bleibend | 3 | Neue Materialien breiten sich durch die Siedlung aus, und Reste der alten Kultur bleiben stehen (ein alter Schrein unter dem neuen Tempel) |
| **Chronik und Ende einer Ära** | Rückschau mit Gewicht: Stolz oder Schuld | Die Welt dahinter wird abgedunkelt und entsättigt, das Pergament ist warm beleuchtet | still, erinnernd, papieren, gewichtig | 1 | Pergament, Sepiatinte, Siegel und Rubriken in den drei Farbsprachen |
| **Menüs** | Ruhe und Erwartung: die Welt vor dem ersten Eingriff | Die Insel von hoch oben in langsamer Kamerafahrt, späte Dämmerung, gedämpft, niedriger Kontrast | weit, erhaben, still, schlicht | 1 | Langsame Wolkenschatten über der ganzen Insel |

**Begründungen**

- **Welt zusehen** ist der Zustand, in dem der Spieler die meiste Zeit verbringt, und er ist
  der Maßstab, an dem sich alle anderen Zustände absetzen. Die tiefe Sonne ist nicht nur
  Stimmung: Aus der Gottesperspektive sieht man von einem Menschen fast nur Kopf und
  Schultern, und erst der lange Schatten zeigt seine Haltung (Abschnitt 3). Ruhig heißt hier
  nicht leer. Die Bewegung der Menschen trägt die Lebendigkeit, das Licht hält still.
- **Gotteshand** setzt auf ein einziges Mittel: kaltes Licht in einer warmen Welt. Der
  Spieler soll spüren, dass er etwas Fremdes in eine natürliche Welt bringt. Der Schatten am
  Boden ist zugleich Spielinformation, denn er zeigt die Höhe, aus der geworfen wird, und
  damit das Gewicht des Wurfs (Pillar 2).
- **Blitz** ist der einzige Zustand mit Spitzenweiß im ganzen Bild. Dieses Weiß wird
  sparsam eingesetzt, damit es erschreckt. Danach ist die Folge wichtiger als der Effekt, und
  der Ruß bleibt nach Prinzip 1 liegen. Zur Lichtempfindlichkeit: höchstens 3 Hellblitze
  pro Sekunde und eine Option, die den Blitz abmildert.
- **Fest-Segen und Fest** zeigen, wie die Farbsprachen sich abwechseln, ohne sich zu
  mischen: Der Gott spricht blauweiß, die Welt antwortet in ihrer eigenen Farbe. Der
  blauweiße Ring sinkt herab und erlischt, und erst danach entzünden sich die Amber-Lichter.
  Beides ist nie gleichzeitig sichtbar. Das Fest ist warm und weich, aber nicht hell. Es lebt
  von Lichtinseln im Dunkel, weil Intimität Dunkelheit am Rand braucht und weil es sich so im
  Graustufenbild am klarsten vom sonnigen Grundzustand unterscheidet.
- **Ritual** ist das Gegenstück zum Fest: geordnet statt verstreut, senkrecht statt
  kreisend, gedämpft statt golden. Die blauweißen Funken gehören dem Gott, nicht der Kultur.
  Sie zeigen, dass die Hingabe beim Spieler ankommt, und verbinden die Gotteskraft sichtbar
  mit ihrer Quelle. Ein Ritual der Lust-Sekte verwendet Amber, behält aber die Ordnung des
  Rituals.
- **Krieg und Dürre** sind beide entsättigt und müssen sich deshalb über andere Achsen
  trennen. Der Krieg ist dunkel, fleckig, lokal und rot. Die Dürre ist hell, flach,
  flirrend, betrifft eine Gegend oder die ganze Insel und hat nie Rot. Die Dürre ist der
  Zustand mit der niedrigsten Energie in der laufenden Welt, denn sie soll den Spieler durch
  Stillstand zum Eingreifen drängen. Die Sonne steigt nie über etwa 55°, damit die Schatten
  lesbar bleiben.
- **Kulturwandel** bekommt bewusst kein eigenes Licht. Eine Kultur, die über das Grading
  erkannt würde, verschwände, sobald die Kamera weiterzieht. Über Material und Bauten bleibt
  sie dagegen sichtbar. Dass die Reste der alten Kultur stehen bleiben, ist die sichtbare
  Form von „Die Kultur erinnert sich“.
- **Chronik und Ende einer Ära:** Eine Ära dauert bei 1× etwa 12 Sekunden und kann darum
  kein großer Moment sein. Ihr Ende bekommt nur ein leises Zeichen am Bildrand. Die große
  Chronik-Stimmung gibt es nur, wenn der Spieler die Chronik öffnet oder ein Wendepunkt
  eintritt. Ob die Simulation dabei pausiert, entscheidet UX.

**Unterscheidbarkeit.** Jedes Paar von Zuständen unterscheidet sich auf mindestens zwei von
vier Achsen: Lichtquelle (Sonne, Punktlicht, Gott), Farbtemperatur, Kontrast und Sättigung.
Graustufen-Test, der für jeden Zustand gilt: Der Grundzustand ist mittelhell mit langen
Schatten. Das Fest ist dunkel mit hellen Inseln, die Dürre hell und flach, der Krieg dunkel
und fleckig. Der Blitz zeigt Spitzenweiß, die Chronik einen hellen Block über grauer Welt.
Zwei Zustände, die im Graustufenbild gleich aussehen, gelten als nicht unterschieden.

**Pipeline-Hinweise** (URP gegen HDRP ist noch offen):
- Color Grading und lokale Post-Processing-Volumes gibt es in beiden Pipelines.
- *[Pipeline-abhängig]* Volumetrischer Nebel und Lichtschächte sind nur in HDRP eingebaut.
  Dunst und Rauch müssen die Stimmung auch als Partikel oder Billboards tragen.
- Tiefenunschärfe gibt es in beiden Pipelines. Auf Android muss sie abschaltbar sein, ohne
  dass der Nahblick seine Stimmung verliert.
- *[Kosten pipeline-abhängig]* Viele Kerzen sind wenige echte Punktlichter plus emissive
  Kerzen und ein Licht-Decal am Boden.

## 3. Shape Language

**Grundsatz: drei Formfamilien, so getrennt wie die drei Farbsprachen.** Die Welt kennt drei
Herkünfte von Form, und jede sagt dem Spieler etwas anderes. Spuren sind keine vierte
Familie, sondern ein Bruch in einer der beiden irdischen.

| Formfamilie | Wem sie gehört | Formcharakter | Was sie dem Spieler sagt | Pillar |
|---|---|---|---|---|
| **Gewachsenes** | Insel, Gelände, Pflanzen, Körper | Organisch, weich, unregelmäßig, große ruhige Massen | Diese Welt lebt und wächst ohne dich | 1 |
| **Gemachtes** | Bauten, Werkzeug, Kleidung, Siedlungen | Handgemacht und eckig: Geraden, die leicht schief sind, abgenutzte Kanten; je geordneter die Kultur, desto strenger die Form | Hier haben Menschen etwas gebaut, und es kann zerbrechen | 2 |
| **Göttliches** | Zeichen des Gottes, Kräfte, HUD | Perfekte Geometrie: exakter Kreis, Ring, Gerade, Säule, ohne Abnutzung | Das ist nicht von dieser Welt. Das bist du | 4 und Core Fantasy |
| *Spuren (Bruch)* | Krater, Brandschneisen, Ruinen, Narben | Eine gewachsene oder gemachte Form mit scharfkantig gebrochener Kontur | Hier ist etwas geschehen | Visuelle Regel, Pillar 2 |

**Perfekte Geometrie gehört dem Gott, so wie das Blauweiß.** Kein Gebäude und keine Kultur
ist exakt kreisrund oder exakt gerade. Der Gottesstaat ahmt die Strenge des Gottes nach,
erreicht sie aber nie, seine Kanten bleiben handgemacht. Das macht seine Angst als Form
lesbar. Daraus folgt eine Einschränkung für den MVP-0: Die Rasterinsel ist ein Platzhalter,
und ihr Raster darf im Bild nicht als Gitter erscheinen (Höhenrauschen, unregelmäßige
Grenzen zwischen Texturen). Sonst trüge die Welt die Form des Gottes.

**Silhouetten aus der Gottesperspektive**

Aus der Ferne liest der Spieler in dieser Reihenfolge: **Bewegung, dann Licht und Farbe, dann
Umriss von oben und Schlagschatten, dann Material, dann Detail.** Das ist Prinzip 2 als Form:
Was in der Ferne weder Bewegung noch Farbe noch Umriss ist, existiert dort nicht.

- **Von oben ist ein Mensch eine Kopf-Schulter-Ellipse.** Seine Seitensilhouette zeigt nur
  sein Schatten. Die tiefe Sonne aus Abschnitt 2 ist deshalb auch ein Werkzeug für die
  Lesbarkeit: Gebeugt, liegend, getragen oder geworfen erkennt man am Schatten. Merkmale, die
  aus der Ferne wirken sollen, sitzen also am Umriss von Kopf und Schultern oder zeigen sich
  in Haltung und Gang, nie in Gesicht oder Hüfte.
- **Haltung vor Detail.** Jeder Zustand wird zuerst über Körperhaltung und Bewegungsrhythmus
  gezeigt, denn nur diese Merkmale überstehen die Detailstufen und schon die Kapselphase.
  Wunden und Kleidungsdetails kommen nur in der Nähe hinzu.
- **Erwachsene Proportion schon als Kapsel.** Alle Kapseln haben dieselbe Höhe (etwa 1,75 m
  Weltmaß, ±5 %). Keine Kapsel wird verkleinert, und kein Kopfmarker verschiebt das
  Verhältnis von Kopf und Körper ins Kindliche. Die nicht verhandelbare Grenze „Keine
  Kinderfiguren“ gilt schon für Platzhalter, weil eine kleine Kapsel neben einer großen als
  Kind gelesen wird.

**Ein Primärmerkmal je Typ**

| Typ | Primärmerkmal (Ferne) | Kapselphase (MVP-0) | Grundkörperphase | Rückfall ohne Farbe |
|---|---|---|---|---|
| **Stamm** | Farbe von Körper oder Kleidung | Körper in Stammesfarbe | Kleidung im Stammeston, Banner, Bemalung, Haartracht | Stamm B trägt einen Kamm (von oben ein Strich über dem Kopfkreis), Stamm A keinen; Zugehörigkeit zur Siedlung |
| **Kultur** | Form der Siedlung von oben | Oberfläche der Kapsel: rau und matt (Stammesrat), glänzend mit Goldreif (Lust-Sekte), dunkle Haube (Gottesstaat) | Umriss von Kopf und Schultern: zackiger Fellumhang (Stammesrat), weite fließende Stoffe und offenes Haar (Lust-Sekte), Kapuze, verhüllt und eng (Gottesstaat) | Siedlungsplan: Hüttenring um ein Feuer / runde Plätze und offene Höfe / Achse auf den Tempel und Reihen |
| **Verletzt** | Bewegung: Humpeln und Neigung | Kapsel etwa 15° geneigt, ruckender Gang, rotes Mal | Humpeln, Hand an der Wunde, in der Nähe ein Wund-Decal | Bewegung allein reicht; das Mal bleibt später als Narbe |
| **Gefangen** | Gruppierung und Führung | Dunkles Eisenband um die Mitte, steht im Pferch, bewegt sich nur geführt | Arme hinter dem Rücken gebunden, gebeugt, Pferch oder Kette | Ort (Pferch) und Bewegung |
| **Auf dem Fest** | Licht und Rhythmus | Warmes Amber-Kantenlicht, wiegende Bewegung, Gruppe im Kreis | Dazu gelöste Haltung und die Festkleidung der Kultur | Wiegen und Anordnung im Kreis |
| **Erwählt** | Zeichen des Gottes | Dünner blauweißer Ring am Boden | Derselbe Ring | Die Ringform, sie ist die einzige exakte Kreisform unter Menschen |
| **Tot** | Liegen ohne Bewegung | Liegt, entsättigt | Liegt; später ein Grabhügel oder Steinhaufen als vereinfachtes Zeichen | Die Form des Grabhügels |

**Zustände schließen sich auch im Bild aus.** Eine Figur trägt nie gleichzeitig Eisenband oder
Wundmal und Amber-Kantenlicht. Gefangene und Verletzte in der Nähe eines Festes bekommen kein
Amber-Kantenlicht, und ihre eigene Markierung bleibt sichtbar. So trennt das Bild, was die
Grenzen „Gewalt und Sex überschneiden sich nie“ und „Gefangene sind ausgeschlossen“ im
System trennen.

Die Kultur wird in der Ferne an der Siedlung gelesen, nicht an der einzelnen Figur. Das
entschärft die bekannten Kosten aus Abschnitt 1, dass Kulturen an Materialien aus der Ferne
schwer lesbar sind. Weil eine Kultur einem Stamm gehört, genügt der Siedlungsplan von oben.
Das Material am Körper ist der Lohn für den Nahblick.

**Geometrie der Umgebung**

Das Gewachsene dominiert und nimmt aus der Ferne den größten Teil des Bildes ein. Es besteht
aus großen, ruhigen Massen mit wenig feinem Detail, damit Menschen, Siedlungen und Spuren
davor stehen und nicht in Laub und Felsrauschen untergehen. Das Gemachte sitzt in Inseln
darin, und sein Ordnungsgrad erzählt die Kultur: der Ring des Stammesrats, die runden,
offenen Höfe der Lust-Sekte, die Achse und die Reihen des Gottesstaats. Spuren sind Brüche in
diesen Formen. Krater, Brandschneisen, eingestürzte Wände und Trampelpfade durchschneiden die
weiche Landschaft mit harten Kanten. Die Landschaft wird so selbst zur Chronik: Eine Insel,
in die der Spieler eingegriffen hat, hat mehr harte Kanten als eine unberührte.

**Formgrammatik der UI: zwei Sprachen, keine dritte**

- **HUD und Menüs sprechen die Form des Gottes:** Kreise, Bögen, haarfeine Linien, keine
  Ornamente und keine Rahmen aus Holz oder Stein. Was der Gott tut und wahrnimmt, ist
  göttlich. Ein HUD aus Kreisen und Linien gehört sichtbar nicht zur Welt und muss deshalb
  nicht so tun, als wäre es diegetisch. Es bleibt knapp: keine Tabellen und keine
  Verwaltungsrahmen (Pillar 4). Ob die Kräfte als Ring um den Cursor gewählt werden,
  entscheidet die UX-Spezifikation; die Form würde passen.
- **Die Chronik spricht die Sprache der Welt:** Pergament, unregelmäßige Ränder, Tintenstrich
  und Siegel. Was die Welt erinnert, gehört der Welt. Eine Chronik in Weltsprache macht
  glaubhaft, dass die Kultur sich erinnert, und nicht nur das Spiel (Pillar 2).
- **Ereignismarker in der Welt** (Kriegserklärung, Gründung einer Sekte) sind die
  Aufmerksamkeit des Gottes und folgen deshalb seiner Form: ein pulsierender dünner Ring in
  UI-Knochenweiß. Der statische blauweiße Ring bleibt den Erwählten vorbehalten, damit beide
  Ringe nicht verwechselt werden.

**Helden- und Nebenformen, das Zeichen des Gottes**

Der Blick soll in dieser Reihenfolge wandern:

1. Das Zeichen des Gottes: die einzige perfekte Form und das einzige kalte Licht, also
   maximaler Kontrast in Form und Farbe zugleich.
2. Figuren in einem Zustand: Bewegung, Rot, Amber.
3. Spuren und Siedlungen.
4. Ruhende Figuren.
5. Die Landschaft, die mit geringem Detailkontrast und gedämpfter Sättigung zurücktritt.

Das Zeichen des Gottes ist reine Geometrie aus Licht: der Ring am Boden als Zeiger, ein
senkrechter Lichtfaden zur gehaltenen Figur, die Lichtsäule beim Erschaffen, der sinkende
Ring beim Segen und der Blitz als gezackte Gerade. **Der Gott hat keinen Körper**
(Entscheidung des Entwicklers vom 2026-10-26: keine modellierte Hand, auch nicht beim
Packen und Werfen; Ring, Lichtfaden und Schatten tragen das Gefühl der Gotteshand). Die
Spuren seiner Eingriffe trägt nicht er, sondern die Menschen und die Welt. Emotional teilt das
mit: Du bist anders als alles, was du anfasst.

## 4. Color System

**Primärpalette** (alle Hexwerte sind **Startwerte**; sie werden am Grundkörper, unter dem
endgültigen Licht und nach der Pipeline-Entscheidung kalibriert)

| Farbe | Hex (Start) | Bedeutung in dieser Welt | Regel |
|---|---|---|---|
| **Lehmerde** | `#6B5640` | Der Boden, der alles trägt und alles aufnimmt | Grundton der Insel; nimmt Spuren auf (Ruß, Blut, Pfade) |
| **Gras-Oliv** | `#5E6A3A` | Leben, das ohne den Gott wächst | Gedämpftes Oliv, nie ein leuchtendes Grün, damit Figuren davor lesbar bleiben |
| **Haut (Mitte der Spanne)** | `#B07D5B` | Der Körper als Chronik, er erzählt die meiste Farbe | Nur ein Bezugspunkt; die volle Spanne legt Abschnitt 5 fest |
| **Blut-Rot** | `#8A1A14` frisch, `#5A1E15` getrocknet | Gewalt und ihre Folgen, sonst nichts | Bleibt auch getrocknet rotstichig, wird nie braun, sonst verschwindet es in der Erde |
| **Kerzenlicht-Amber** | `#F0A040` (Licht), Gold-Material `#B8913A` | Lust, Fest, Wärme der Gemeinschaft | Ist vor allem eine Lichtfarbe. Als Oberfläche nur im Gold der Lust-Sekte |
| **Gottes-Blauweiß** | Rand `#D6EBFF`, Kern `#F5FAFF`, emissiv | Der Eingriff des Gottes | Die hellste und einzige kalte, leuchtende Farbe der Welt; keine Kultur darf sie verwenden |
| **Ruß und Asche** | Ruß `#262220`, Asche `#8C877E` | Was nach Feuer, Strafe und Krieg bleibt | Dauerhafte Spurfarbe; wird vereinfacht, nie gelöscht (Prinzip 1) |

Dazu als Nebenfarbe das Meer in dunklem Petrol (`#2F5559`). Natürliches Blau bleibt dunkel
und grünstichig, damit es nie mit dem Gottes-Blauweiß verwechselt wird.

**Semantische Verwendung**

- **Helligkeit und Sättigung sind reserviert.** Natürliche Flächen der Erdpalette bleiben
  unter etwa 55 % Sättigung und unter etwa 80 % Helligkeit (HSV). Darüber liegen nur die drei
  Farbsprachen und die Stammesfarben. Ohne diese Reserve hätten Blut, Amber und Blauweiß
  keinen Raum, um sich abzuheben.
- **Natürliches Licht hat keine kalten Glanzlichter.** Die Sonne ist warm, also sind auch
  Glanz auf Wasser, Metall und Haut warmweiß. Kalt und hell leuchtet nur der Gott. Das
  Gottes-Blauweiß ist über Helligkeit, Kühle und Leuchten zugleich definiert, nicht über den
  Farbton allein.
- **Blut-Rot** steht nur für Blut, Wunden und Spuren des Krieges. Es ist auch in der UI keine
  Warnfarbe (siehe UI-Palette). Ein Rot, das auch „nicht genug Gotteskraft“ bedeutet, wäre
  kein Blut mehr.
- **Kerzenlicht-Amber** ist Licht, nicht Anstrich. Eine Lust-Kultur wird über ihr Licht und
  ihre Stoffe erkannt, nicht über eingefärbte Körper (Prinzip 3, Umfangsregel von Pillar 3).
  Nie über Gewalt, nie auf Gefangenen oder Verletzten.
- **Feuer ist nicht automatisch Amber.** Herdfeuer ist klein, gelblich und schwach und gehört
  zur Erdpalette. Ein Brand ist groß, rauchig und orangerot mit schwarzem Rauch und
  hinterlässt Ruß. Amber ist das gezähmte Licht des Festes: viele kleine, ruhige Quellen in
  Kreisordnung und ohne schwarzen Rauch.
- **Gottes-Blauweiß** gibt es für Kräfte, Gotteshand, Erwählte, die aufsteigende Gotteskraft
  und die Machtelemente der UI, sonst nirgends.

**Kulturmaterialien** (ein Master-Material, je Kultur ein Parametersatz; die Farben bleiben
in der Erdpalette, unterschieden wird über Glanz, Rauheit und Akzent)

| Kultur | Materialien | Glanz und Oberfläche | Akzent (Start) | Verboten |
|---|---|---|---|---|
| **Stammesrat** (Ausgangszustand) | Leder, Fell, Holz, ungefärbte Wolle | Matt, rau | Ocker-Bemalung `#9A6A3A` | Gold, Seide |
| **Lust-Sekte** | Seide, Gold, Honigtöne, Pflaume | Glänzend, fließend | Gold `#B8913A`, Pflaume `#5E2F4A` | Rot in jeder Form, kaltes Weiß |
| **Gottesstaat** | Sackleinen, Asche, Stein, Schiefer | Stumpf, verhüllend | Aschgrau `#8C877E`, Schiefer `#3E4044` | Blauweiß, kühles Weiß, Gold |

Kein Kulturweiß ist kälter als neutral. Leinen und Kalk der späteren Kulturen sind immer
warm gebrochen, damit Weiß in einer Kultur nie nach Gott aussieht.

**Farbtemperatur je Gegend und Zustand** (Kelvin als Startwerte)

| Gegend oder Zustand | Temperatur | Sättigung und Kontrast | Begründung |
|---|---|---|---|
| Insel, Grundlicht | Sonne ~4000 K, Himmelslicht schwach und neutral-kühl | Natürlich, mittlerer Kontrast | Bodenständig und warm. Kühle Schatten sind dunkel und konkurrieren deshalb nicht mit dem Gott |
| Fest | Kerzen ~1900–2200 K lokal, Umgebung abgedunkelt | Innen weich, nach außen hoher Kontrast | Lichtinseln im Dunkel tragen die Intimität |
| Ritual | ~5000 K, gedämpft | Entsättigt, Kontrast zur Mitte gebündelt | Andacht ist kühler und strenger als das Fest |
| Dürre | ~5800 K, steile Sonne | −25 bis −35 % Sättigung, Ocker-Dunst | Bleiche als Mangel |
| Verbrannter Boden | Ohne eigene Temperatur; Glut kurz 1500–1800 K, dann dauerhaft Ruß | Fast schwarz, matt | Die Folge muss im Grundlicht lesbar bleiben, auch wenn das Ereignis längst vorbei ist |
| Krieg (später) | ~4500 K durch Staub | Entsättigt außer Rot | Rot bleibt die einzige laute Farbe |
| Gotteslicht | Wirkt wie ~12000 K, emissiv mit Bloom | Weiß-kühl, hellster Wert im Bild | Das einzige unnatürliche Licht |

**UI-Palette und wo sie von der Welt abweicht**

| Rolle | Hex (Start) | Hinweis |
|---|---|---|
| UI-Grund | `#1C1A17` bei 80–90 % Deckkraft | Warmes Anthrazit aus der Erdpalette, kein neutrales Schwarz |
| Text | `#ECE4D4` Knochenweiß | Kontrast zum Grund ~14:1 |
| Sekundär und gesperrt | `#8C877E` Asche | |
| Machtakzent | `#D6EBFF` Gottes-Blauweiß | Nur für Kräfte, Gotteskraft, Hand und Auswahl |
| Chronik-Pergament | `#D8C7A0` | |
| Chronik-Tinte | `#3A2A1D` Sepia | Kontrast zum Pergament ~9:1 |
| Rubrik Blut | `#8A1A14` | Für Krieg, Tod, Gewalt; nur Initialen und Icons |
| Rubrik Lust | `#A8641E`, dunkleres Amber | Für Fest und Lust; dunkler, damit es auf Papier trägt |
| Gottes-Siegel | Leuchtendes blauweißes Ringsiegel | Für Eingriffe des Gottes |

Drei Abweichungen von der Welt, alle mit Absicht:

1. Die UI darf perfekte Geometrie und das Gotteslicht verwenden, weil sie die Wahrnehmung des
   Gottes ist.
2. Die UI hat keine rote Warnfarbe. Warnungen arbeiten mit Asche-Grau, Form und einem Puls.
   Rot erscheint in der UI nur dort, wo es um Blut geht (Chronik-Rubriken, Marker für Tod und
   Krieg).
3. In der Chronik schreibt der Gott nicht mit Tinte, er siegelt. Helles Blauweiß wäre auf
   Pergament unlesbar, und ein dunkles Blau würde die Gottesfarbe verwässern. Ein leuchtendes
   Ringsiegel neben einem Eintrag zeigt „das war ich“ und macht den Eingriff nachverfolgbar
   (Pillar 2).

Rubrikfarben gibt es nur für Siegel, Initialen und Icons. Der Fließtext bleibt Sepia, weil das
dunkle Amber auf Pergament die 4,5:1 für Text nicht sicher erreicht.

**Stammesfarben für die Kapselphase** (Entscheidung des Entwicklers vom 2026-10-26: Indigo
und Kreide)

Freie Farbtöne sind knapp. Rot, Amber und Gold, helles Blau, Grün (Gras) und Braun (Erde)
sind besetzt. Die Stammesfarben trennen sich deshalb **zuerst über die Helligkeit und erst
dann über den Farbton**:

- **Stamm A, Waid-Indigo** `#2B3D70`: dunkel, matt, gesättigt. Waid ist ein natürlicher
  Farbstoff. Ein Blau unter etwa 40 % Helligkeit, das matt ist und nicht leuchtet, gilt
  nicht als Gottesfarbe.
- **Stamm B, Kreide** `#D8D0BC`: hell, warm, matt, leuchtet nie. Es ist warm gebrochen, damit
  es unter der Sonne golden wird und nicht kühl.
- Ein **dritter Stamm** später bekommt eine mittlere Helligkeit und ein Muster (Streifen),
  denn auf der Helligkeitsachse ist nur noch die Mitte frei.
- Beide Farben gelten nur für Kapseln. Mit dem Grundkörper wandern sie in Kleidung, Banner
  und Bemalung, und die Haut bleibt in ihrer natürlichen Spanne.

**Farbenblind-Sicherheit**

| Farbpaar | Versagt bei | Warum | Nötiger Rückfall |
|---|---|---|---|
| Blut-Rot / Erde und Oliv | Protanopie, Deuteranopie | Rot wird fast schwarzbraun und verschwindet im Boden | Nasser Glanz (Spekular) auf frischem Blut, Form von Spritzern und Tropfspur, Humpeln und Neigung, Tropfen-Icon in der Chronik |
| Blut-Rot / Kerzenlicht-Amber | Protanopie, Deuteranopie | Beide rutschen ins Gelbbraune | Helligkeit (Amber ist helles Licht, Blut ein dunkler Fleck), Form (Lichtpunkt und Lichtkreis gegen Spritzer und Mal), Bewegung (Wiegen gegen Humpeln), ein Tonhinweis, den der Audio Director gestaltet; außerdem schließen sich beide Zustände an einer Figur aus |
| Amber-Licht / Gras-Oliv | Deuteranopie | Das Fest-Licht auf einer Wiese verändert den Farbton kaum | Lokaler Helligkeitsunterschied (dunkle Umgebung, helle Insel), sichtbare Punktquellen, Dunst, Anordnung im Kreis |
| Gottes-Blauweiß / warmes Weiß (Sonnenglanz, Kreide-Stamm, UI-Text) | Tritanopie, Achromatopsie | Ohne Blauanteil wird Blauweiß einfach Weiß | Form: der perfekte Kreis, den es sonst in der Welt nicht gibt; Bewegung: langsames Drehen oder Pulsieren; Bloom |
| Dürre-Ocker / gesundes Oliv | Protanopie, Deuteranopie | Gelbgrün und Ocker fallen zusammen | Sättigung und Helligkeit (bleich), Bodenrisse (Form), Staubfahnen (Bewegung), Silhouetten welker Pflanzen |
| Stamm A / Stamm B | Robust, weil über die Helligkeit getrennt; Restrisiko unter Amber-Licht und im Dürre-Dunst | Warmes Licht hebt Indigo an und gleicht beide Farben an | Kamm-Merkmal von Stamm B, Zugehörigkeit zur Siedlung |
| Gotteskraft voll/leer (Blauweiß / Asche) | Tritanopie | Beide werden hellgrau | Füllform der Leiste und Zahl, nie nur die Farbe |

*Prüfung:* Jeder Zustand aus Abschnitt 2 wird als Screenshot durch eine Simulation von
Protanopie, Deuteranopie und Tritanopie (z. B. Color Oracle) und durch den Graustufen-Test
geschickt. Ein Zustand oder Merkmal, das nur über Farbe lesbar ist, gilt als nicht lesbar.

## 5. Character Design Direction

**Archetyp: keine Spielerfigur, die Menschen sind die Chronik**

*Allmacht* hat keine Spielerfigur. Der Gott hat keinen Körper (Abschnitt 3), und das
Besessen-Sein kommt erst im Vertical Slice. Freund und Feind gibt es nicht, denn der Gott
steht über allen Stämmen. Für das Figurendesign folgen daraus fünf Regeln:

1. **Keine Heldenfigur, nur Menschen.** Keine Figur bekommt ein eigenes Modell, eine eigene
   Silhouette oder ein Detail, das nicht jede andere auch bekommen könnte. Wichtig wird eine
   Figur durch das, was ihr geschieht, nicht durch ihr Design. Die Blickfolge aus Abschnitt 3
   ordnet Zustände, nicht Personen. Auch die Figur, in der der Spieler ab dem Vertical Slice
   steckt, ist derselbe Körper. Sie bekommt nur Vorrang in der Stufe Nah.
2. **Entworfen zum Zusehen, nicht zum Steuern.** Eine Spielerfigur muss von hinten auf
   Schulterhöhe lesbar sein. Diese Figuren werden dagegen von oben und über Jahre betrachtet.
   Sie brauchen eine lesbare Kopf-Schulter-Ellipse, einen lesbaren Schatten und eine
   Erscheinung, an der man sie nach zehn Geschichtsjahren wiedererkennt.
3. **Die Figur ist eine Seite der Chronik.** Jede benannte Figur trägt ihre Geschichte am
   Körper: Narben, Verbände, Hunger, ergrautes Haar, die Kleidung der Kultur, in der sie
   lebt, und Reste der Kultur, aus der sie kommt. Eine Figur, die zwanzig Jahre auf der Insel
   gelebt hat, sieht anders aus als eine, die gerade aus der Lichtsäule getreten ist.
   *Test:* Wer eine Figur in der Stufe Mittel ansieht, kann ohne Tooltip mindestens ein
   Ereignis aus ihrem Leben erraten.
4. **Lust hinterlässt keine Körperzeichen.** Nach Prinzip 3 zeigt sich Lust über Licht, Stoff
   und Folgen in der Welt (Festreste, Harem-Haus, Kleidung der Kultur), nie über ein Zeichen
   am Körper. So bleiben Gewaltzeichen und Lust auch am Körper getrennt.
5. **Blauweiß berührt einen Körper nur als Licht.** Die gehaltene Figur bekommt kaltes
   Kantenlicht, die erwählte einen Ring am Boden. Es gibt kein Material, keine Bemalung und
   keine Narbe in Blauweiß. Die Spuren eines Eingriffs trägt der Körper in den Farben der
   Welt: Blut-Rot, Ruß, Erde.

**Grundkörper und Morphs**

**Ein Skelett, ein Grundkörper, Morphs** (aus Abschnitt 1, nicht verhandelbar). Alle Körper
wirken klar erwachsen und haben 7 bis 7,5 Kopflängen. Vielfalt entsteht über Morphs, Haut-
und Haarpaletten und Kleidung, nie über getrennt modellierte Figuren.
*Test:* Wird ein neuer Körpertyp gebraucht, ist das ein Morph oder ein neues Modell? Ein Morph.

- **Grundkörper:** ein offener, lizenzierter Körper (z. B. MakeHuman/MPFB, CC0; die Lizenz
  wird je Asset geprüft). Ein Skelett gilt für alle: Frau, Mann, Statist, Ragdoll und später
  die Figur, in der der Spieler steckt.
- **Frau und Mann** sind im Meilenstein „Erste spielbare Version“ zwei Exporte desselben
  Grundkörpers mit gleicher Topologie und gleichem Skelett. Das sind keine getrennt
  modellierten Figuren. Ab MVP-1 werden die beiden Exporte die festen Endpunkte des
  Geschlechts-Morphs, es wird also nichts weggeworfen.
- **Kleidung und Haar** sind eigene Meshes auf demselben Skelett. Ab MVP-1 folgen sie den
  Morphs.
- **Eigenschaften prägen nur Haltung und Gang** (Entscheidung des Entwicklers vom
  2026-10-26): Mut aufrecht, Geselligkeit zugewandt, Frömmigkeit mit gesenktem Kopf. Statur
  und Geschlechtsmerkmale hängen nie an den Eigenschaften; eine Kopplung wie „viel Lust gleich
  üppiger Körper“ widerspräche Prinzip 3.

| Ebene der Vielfalt | Inhalt | Ab |
|---|---|---|
| Hautpalette | 7 Stufen plus Unterton (unten) | Grundkörper |
| Haarpalette und Haarform | 8 Farben, je Kultur 3–4 Formen | Grundkörper |
| Kleidung | Je Kultur und Geschlecht 2–3 Teile, Stammesakzent | Grundkörper |
| Morphs | Statur, Gewicht, Muskulatur, Gesichtsform, Alter | MVP-1 |
| Zustandsmorph | Hunger (hager) | MVP-1 |

**Grenzen der Morph-Regler (Absicherung „Keine Kinderfiguren“).** Das ist eine Designvorgabe
dieser Art Bible. Der Validierungstest aus dem Konzept sichert sie ab.

1. **Was nicht exportiert wird, kann kein Regler erreichen.** Aus dem Grundkörper werden
   keine Morph-Ziele exportiert, die jünger als erwachsen wirken. In MakeHuman/MPFB bleibt der
   Alters-Regler beim Export auf oder über dem Wert für 25 Jahre (in der verwendeten Version
   prüfen). Die sichtbare Altersspanne reicht von jung erwachsen bis etwa 70.
2. **Kopflängen:** Jede Kombination liegt zwischen 7,0 und 7,5. Die Kopfgröße ist kein
   eigener Regler, sie skaliert nur mit dem Körper.
3. **Körpergröße je Geschlecht** (Entscheidung des Entwicklers vom 2026-10-26): Frauen
   1,60–1,78 m, Männer 1,68–1,90 m, als harte Grenzen; keine Figur ist kleiner als 1,60 m.
   Die Spannen sind enger als in der Wirklichkeit, weil von oben nur der Schatten die Größe
   verrät und eine sehr kleine Figur neben einer großen als Kind gelesen wird (Abschnitt 3).
   In der Kapselphase gilt weiter für alle 1,75 m ± 5 % (Abschnitt 3).
4. **Geschlecht ist kein freier Regler.** Es gibt zwei Endpunkte, Frau und Mann (Entscheidung
   vom 2026-10-25), und keine Mischwerte. Die Regler für Brust, Hüfte, Schulterbreite und
   Gesichtsreife haben ihre untere Grenze im erwachsenen Bereich.
5. **Zustandsmorphs** (Hunger) machen hager, aber nie kleiner und nie kindlicher. Sie ändern
   Gewicht und Muskulatur, nicht Proportion oder Größe.
6. **Die Grenzen stehen in Daten, sind aber keine Tuning-Knöpfe.** Eine Änderung braucht die
   Freigabe des Entwicklers und einen grünen Validierungstest.

*Test:* Der Validierungstest misst Höhe und Kopflängen über die Knochenpositionen, und zwar
an allen Ecken der Regler, die Größe und Proportion beeinflussen, und an
Zufallskombinationen. Ein Wert außerhalb der Grenzen lässt den Build scheitern. Er läuft ab
dem ersten Export, schon für die zwei festen Körper. Ergänzt wird er durch eine Sichtprüfung:
Die Extremkombinationen stehen als Graustufen-Silhouetten nebeneinander, und jede muss
erwachsen wirken.

*Zuständigkeit* (bei einem Entwickler sind das Rollen und keine Personen, die Grenze gilt
trotzdem):
- **Art Director:** Grenzen, Paletten, Sichtabnahme.
- **Technical Artist:** Export, Morph-Ziele, Klammerung der Regler, Meshes der Detailstufen,
  das System für Masken und Decals.
- **Programmierung:** Validierungstest und Auswahl der Körperwerte im System Körper &
  Animation (aus Startwert, Geschlecht und Eigenschaften, Letztere nur für Haltung und Gang).
- **Entwickler:** Freigabe jeder Änderung an den Grenzen.

**Hautspanne** (Startwerte, die Mitte ist der Bezugspunkt aus Abschnitt 4)

| Stufe | 1 | 2 | 3 | **4 (Mitte)** | 5 | 6 | 7 |
|---|---|---|---|---|---|---|---|
| Hex | `#CDA588` | `#C29576` | `#B8895F` | **`#B07D5B`** | `#966647` | `#7A5038` | `#4A2F22` |

- Alle Stufen halten die Reserve aus Abschnitt 4 ein (unter 55 % Sättigung, unter 80 %
  Helligkeit). Stufe 1 bleibt dunkler als Kreide (`#D8D0BC`), damit Kreide-Stoff vor jeder
  Haut steht.
- Ein zweiter Parameter verschiebt den Unterton (warm, neutral, oliv) um höchstens etwa ±5 %
  im Farbton. Rosa-rote Untertöne nahe am Blut-Rot sind ausgeschlossen.
- Auf den dunklen Stufen 6 und 7 trägt Blut über den nassen Glanz und die Form der Spritzer,
  nicht über den Farbton (Rückfall aus Abschnitt 4).
- Die Haut ist nie ein Merkmal für Stamm oder Kultur. **Beide Stämme ziehen aus der vollen
  Spanne** (Entscheidung des Entwicklers vom 2026-10-26).

**Haarpalette** (Startwerte): Schwarzbraun `#1E1814`, Dunkelbraun `#3A2A1E`, Mittelbraun
`#5C4430`, Kastanie `#6B4632` (gedämpft, nie nahe am Blut), Dunkelblond `#9A7A50`, Blond
`#B89C70`, Grau `#8E8880` und Weiß `#CCC5B8`. Grau und Weiß sind warm gebrochen, weil kühles
Weißhaar nach Gott aussähe. Das Ergrauen setzt etwa ab einem angezeigten Alter von 45 ein.

**Unterscheidung auf einen Blick: was der Grundkörper ergänzt**

Primärmerkmale, Rückfälle und die Zustände Verletzt, Gefangen, Fest, Erwählt und Tot legt
Abschnitt 3 fest. Die Tabelle ergänzt nur, was mit dem Grundkörper neu ist. Wunden behandelt
die Detailstufen-Tabelle unten.

| Merkmal | Fern | Mittel | Nah |
|---|---|---|---|
| **Stamm: wo die Farbe sitzt** | Indigo oder Kreide liegt auf der Kopf-Schulter-Ellipse (Schulterbahn, Kopfband, Haarbemalung) und deckt von oben etwa ein Viertel der Fläche | Dazu Banner an Hütten. Der Kamm von Stamm B ist Haartracht oder Kopfschmuck | Bemalung als Hautdetail (Waid, Kreide) |
| **Kultur im Wandel** | Der Kopf-Schulter-Umriss der neuen Kultur (Abschnitt 3) | Die Kleidung wechselt stückweise: zuerst Schmuck und Umhang, zuletzt das Untergewand. Ältere tragen länger ein Stück der alten Kultur | Materialglanz (Seide gegen Fell) |
| **Geschlecht** | Muss in der Ferne nicht lesbar sein | Körperform, Haar, Schnitt der Kleidung | – |
| **Alter (18 bis etwa 68)** | Etwas langsamerer Gang ab etwa 55 | Ergrauen, leicht gebeugte Haltung | Hautalterung (ab Vertical Slice) |
| **Person** | – | Haar (Form und Farbe), Hautstufe und Statur ergeben eine Kombination, die unter den Lebenden eines Stamms möglichst eindeutig ist. Dazu kommen ihre bleibenden Zeichen | Gesicht |
| **Einzelne und Statisten** | Kein Unterschied, denn die Masse ist nicht weniger lebendig (Pillar 1) | Statisten nutzen feste Kombinationen (Start: 6 je Stamm und Geschlecht), Einzelne eigene. Ein Statist, der hervortritt, behält sein Aussehen und springt nicht um | – |
| **Rolle** (Anführer, Priesterin, Harem-Mitglied) | Ort und Anordnung: vorn im Ritual, in der Mitte des Festkreises | Je Rolle ein Kulturschmuck am Kopf-Schulter-Umriss | – |
| **Hunger** | Langsamerer Gang (Abschnitt 2) | Hagere Statur (Zustandsmorph ab MVP-1, davor nur Gang und Haltung) | Eingefallene Haut |

- **Rollenzeichen sind Kulturschmuck und folgen den Ausschlussregeln aus Abschnitt 3.** Wer
  gefangen wird, verliert Gold und Kulturschmuck sichtbar und trägt stattdessen Strick oder
  Eisenband. Ein Harem-Mitglied, das gefangen wird, trägt sofort kein Harem-Zeichen mehr, so
  wie es im System sofort herausfällt.
- **Die Stammesfarben überdauern den Kulturwandel als Akzent.** Die Kultur bestimmt das
  Material, der Stamm den Akzent. Beide Farben sind mit allen Kulturen vereinbar: Kreide ist
  warm und deshalb kein „kaltes Weiß“, Indigo ist dunkel und matt und deshalb kein Blauweiß.

**Ausdruck und Posen**

**Stil: zurückhaltend und realistisch im Körper, deutlich in der Haltung.** Übertriebenes
Cartoon-Acting auf halbrealistischen Körpern verstärkt das Uncanny Valley und passt nicht zu
einer Welt, die aus der Ferne wie eine echte Landschaft aussieht. Was gelesen werden muss,
liest man über die Haltung (Abschnitt 3: Haltung vor Detail).

- **Basisbewegung aus vorhandenen Animationen**, deren Lizenz geprüft ist. Mindestsatz für
  den Meilenstein (etwa 14 Clips): zwei Idles, Gehen, Laufen, Arbeiten, Sitzen, Knien oder
  Beten, Wiegen oder Tanzen, Humpeln, gebundenes Stehen und Gehen, Fallen, Liegen, zwei
  Aufsteh-Clips nach dem Ragdoll.
- **Zustände als Haltungsschicht über der Basisbewegung:** Neigung (verletzt, etwa 15° wie
  die Kapsel), Arme auf dem Rücken (gefangen, nur der Oberkörper wird überschrieben), Wiegen
  (Fest), Krümmung (Hunger, Alter), gesenkter Kopf (Ritual), dazu die Haltung aus den
  Eigenschaften. So entstehen aus wenigen Clips viele Zustände. Die Umsetzung gehört dem
  Technical Artist.
  *Test:* Jede Haltungsschicht verändert den Umriss von oben oder den Schatten so stark, dass
  der Graustufen-Screenshot in der Stufe Fern sie zeigt.
- **Keine gleichtaktige Menge.** Tempo der Clips ±10 % und versetzte Phasen. Eine Menge, die
  im Gleichschritt geht, wirkt wie ein Gitter und trüge damit die Form des Gottes
  (Abschnitt 3).
- **Gewicht.** Das Ragdoll fällt mit physikalischem Gewicht, die Figur bleibt kurz liegen und
  steht mit einem Aufsteh-Clip auf. Ragdolls gibt es nur in den Stufen Mittel und Nah. In der
  Stufe Fern fällt eine Figur mit einem Clip, damit das Budget von 8–16 aktiven Ragdolls hält.
- **Gesicht.** Im Meilenstein ist das Gesicht neutral und entspannt, mit Lidschlag, wenn der
  Grundkörper ihn mitbringt. Es gibt keine Lippensynchronisation und keine Ausdrücke. Ab dem
  Vertical Slice bekommt die Stufe Nah 4–6 Ausdrücke: Schmerz, Angst, Freude, Andacht, Ruhe,
  Erschöpfung. Ruhig ist besser als falsch. Der Nahblick arbeitet mit Streiflicht und
  Tiefenunschärfe (Abschnitt 2) und verweilt nicht frontal auf einem bewegten Gesicht. Das
  Uncanny Valley wird am Grundkörper mit Ragdoll gemessen (Abschnitt 1).
- **Einfache Szenen** kommen ohne Paar-Animationen aus (im MVP ausgeschlossen). Sie erzählen
  über Nähe, Haltung, Kerzenlicht-Amber und den Szenenschnitt, nicht über Körperdetail
  (Prinzip 3).

**Detailstufen**

**Auflösung des offenen Punkts aus Abschnitt 1.** Die beiden Zahlen benennen zwei
verschiedene Stufen. „Volle Haut- und Wunddetails für ein bis drei Figuren“ (Anker) ist die
Stufe **Nah**. „20–30 Figuren in voller Detailstufe“ (Budget im Konzept) ist die Stufe
**Mittel**: voller Körper, voll animiert und ragdollfähig, aber ohne Hautdetail. Die unterste
Stufe heißt **Menge** und nicht „Masse“, weil „Masse“ im GDD Menschen & Bevölkerung schon die
Nicht-Einzelnen bezeichnet.

Gemessen wird der **Bildanteil**, also die Höhe der Hülle einer Figur auf dem Bildschirm. Er
gilt für jede Zoomstufe und jedes Sichtfeld. Die Abstände in Klammern gelten für ein
vertikales Sichtfeld von 40° (Bildanteil ≈ 2,4 m ÷ Abstand).

| Stufe | Bildanteil (Abstand) | Höchstzahl | Zeigt | Fällt weg | Wunden und Zeichen |
|---|---|---|---|---|---|
| **Nah** | ≥ 25 % (bis ~10 m) | 3, ab Vertical Slice 3 plus die Figur, in der der Spieler steckt | Haut mit Schweiß, Poren und Blutergüssen, deren Farbe mit der Heilung wandert; Gesichtsausdruck; volles Haar; Stoffglanz | – | Decals in voller Auflösung über der Maske aus Mittel |
| **Mittel** | 5–25 % (~10–50 m) | 30 (Start 20, Anhebung erst nach Messung) | Voller Körper mit Morphs, Kleidung, vereinfachtes Haar, alle Haltungsschichten, Ragdoll | Hautdetail, Gesicht, Schweiß, Decals | Atlas-Maske: Körperzonen (Start 8) mal Klassen (frisch blutend, Bluterguss, verkrustet, Narbe); Verband als Kleidungsteil |
| **Fern** | 1,5–5 % (~50–160 m) | Alle übrigen Einzelnen; Statisten bis zu einer sichtbaren Obergrenze (Start 100) | Vereinfachtes Mesh, ein Material (Haut, Kleidungston, Stammesakzent), Haltungsschicht, Animation mit gesenkter Rate | Gesicht, Haarform, Masken, Ragdoll | Materialparameter: nur eine frische, schwere Wunde als ein roter Fleck. Bleibende Folgen zeigt die Haltung (Humpeln) |
| **Menge** | < 1,5 % (über ~160 m) oder über der Obergrenze | – | Gruppenbewegung und Schatten. Was über der Obergrenze liegt, zeigt sich in der Größe der Siedlung | Alle Zeichen am einzelnen Körper | Sie wandern in die Welt: Blut am Boden des Ereignisorts, Grabhügel, Pfade (Abschnitt 6) |

1. **Vorrang, wenn eine Stufe voll ist:** In Gottes Hand vor Ausgewählt oder Erwählt, dann
   Figuren in einem Zustand aus Abschnitt 3, dann Einzelne, dann Statisten, bei Gleichstand
   der kleinere Abstand. Wer verdrängt wird, rutscht eine Stufe tiefer.
2. **Kein Springen.** Eine Figur fällt erst 15 % hinter der Grenze in die tiefere Stufe
   zurück, und der Wechsel wird überblendet. Ein Zeichen verschwindet beim Wechsel nie: Seine
   einfachere Form erscheint, bevor die genauere ausgeblendet wird.
3. **Die Daten bleiben, nur die Darstellung wechselt.** Eine Wunde liegt in der Simulation
   als Zone, Verletzungsklasse und Schritt der Heilung (Menschen & Bevölkerung). Jede Stufe
   liest dieselben Daten in ihrer Auflösung. Geht eine Figur von Mittel nach Fern und zurück,
   sieht sie aus wie vorher.
4. **Die Vereinfachungskette für Körper** folgt Prinzip 1: Decal, dann Atlas-Maske, dann
   Materialparameter, dann Haltung, dann das Zeichen in der Welt. Was in der Ferne weder
   Silhouette noch Farbe noch Bewegung ist, lässt die Ansicht dort nach Prinzip 2 weg, der
   Zustand behält es. Eine alte Narbe fehlt in der Stufe Fern und ist in der Stufe Mittel
   wieder da.
5. **Statisten erreichen in der Nähe die Stufe Mittel** (Pillar 1), tragen aber keine Wunden,
   denn die Masse hat keinen Zustand. Wer verletzt wird oder in Gottes Hand gerät, tritt
   hervor.
6. **Bis zur zweiten Zoomstufe ist Mittel die höchste gebaute Stufe.** Im Meilenstein mit
   einer Zoomstufe gibt es nur Mittel, Fern und Menge. Nah wird mit der zweiten Zoomstufe
   (MVP-1) vorbereitet und bekommt mit „Haut in Detailstufen“ (Vertical Slice) seine Haut.

*Pipeline-Hinweise:*
- *[Pipeline-abhängig]* Subsurface Scattering für Haut ist in HDRP eingebaut
  (Diffusionsprofile). URP hat keines und braucht eine Näherung oder einen eigenen Shader.
- *[Pipeline-abhängig]* HDRP hat einen Haar-Shader, URP arbeitet mit Haar-Karten im
  Standardmaterial.
- *[Kosten pipeline-abhängig]* Decals gibt es in beiden Pipelines.

**Kapselphase und Grundkörperphase**

| Aspekt | Kapselphase | Grundkörper (Meilenstein, ohne Morphs) | MVP-1 | Vertical Slice |
|---|---|---|---|---|
| **Proportion** | 1,75 m ± 5 %, kein Kopfmarker (Abschnitt 3) | 7–7,5 Kopflängen, Frau und Mann als zwei Exporte, Validierungstest läuft | Morphs innerhalb der Grenzen | – |
| **Stamm** | Körper in Indigo oder Kreide | Farbe auf der Kopf-Schulter-Ellipse, Kamm als Haartracht | Banner, Bemalung | Bemalung als Hautdetail |
| **Kultur** | Oberfläche der Kapsel (Abschnitt 3) | Kleidung und Kopf-Schulter-Umriss je Kultur | Stückweiser Wechsel, Reste der alten Kultur | Materialglanz in Nah |
| **Geschlecht** | Schmalere Kapsel für Frauen (Radius 0,9×, gleiche Höhe; Entscheidung des Entwicklers vom 2026-10-26) | Körper, Haar, Schnitt | – | – |
| **Vielfalt** | Keine | Haut, Haar, Kleidung | Morphs, Hunger-Morph | Gesicht |
| **Zustände** | Kapselspalte aus Abschnitt 3 | Haltungsschichten | – | Gesichtsausdruck in Nah |
| **Wunden** | Rotes Mal, das als dunkle Narbenmarke bleibt | Atlas-Maske (Mittel), Materialparameter (Fern) | – | Decals und Haut (Nah) |
| **Detailstufen** | Die Stufenlogik (Bildanteil, Höchstzahlen, Vorrang, Hysterese) wird schon mit Kapseln gebaut und ist im Debug als Tönung sichtbar | Mittel, Fern, Menge | Nah vorbereitet | Nah mit Haut |
| **Messung** | – | Skinning mit Blend Shapes, Uncanny Valley, Ragdoll-Budget | Morph-Kosten | Hautkosten, Android |

Weil die Stufenlogik schon mit Kapseln steht, tauscht der Wechsel zum Grundkörper nur die
Darstellung und nicht die Regeln. Wie jede Figurensilhouette gegen den Boden lesbar bleibt,
legt Abschnitt 6 fest: Helligkeitsbühne, Lücke im Figurenmaßstab, die Bewegung gehört den
Menschen.

## 6. Environment Design Language

**Grundsatz: Die Insel ist Bühne und Archiv.** Die Umgebung hat zwei Aufgaben. Als Bühne
lässt sie die Menschen vor sich lesbar stehen. Als Archiv behält sie jeden Eingriff. Das
Gewachsene dominiert, das Gemachte sitzt in Inseln darin, und Spuren sind Brüche
(Abschnitt 3). Eine unberührte Insel ist ruhig und weich. Eine Insel, in die der Spieler
eingegriffen hat, ist voller Kanten, Narben und Reste.

**Bau und Siedlung je Kultur**

| Kultur | Bauweise und Material | Siedlung von oben | Mitte | Woher sie kommt | Was von ihr bleibt |
|---|---|---|---|---|---|
| **Stammesrat** (Ausgangszustand) | Rundliche Hütten aus Flechtwerk und Lehm, Strohdach, Fell vor dem Eingang, Pfähle mit Bannern im Stammeston; niedrig, erdnah, matt | Unregelmäßiger Hüttenring, Eingänge zur Mitte, Pfade sternförmig nach außen | Das Ratsfeuer mit Sitzsteinen ist der erste Ritualplatz (Welt & Insel, Regel 17: anfangs die Mitte) | Gleichheit im Rat: Kein Bau ist höher als die anderen | Der Feuerring aus Steinen |
| **Lust-Sekte** | Offene Höfe mit niedrigen Mauern, Pavillons mit Stoffbahnen (Seide, Honig, Pflaume), Gold an Pfosten und Lampen; Glanz statt Masse | Gruppen rundlicher Höfe, nach innen geschlossen und zueinander offen, keine Achse | Der Festplatz als größter Hof mit Feuerschalen und Kerzenkreis, meist dort, wo das Ratsfeuer brannte | Aus dem Rat hervorgegangen: Der Kreis bleibt, wird aber weich und offen | Kerzennischen, Wachsflecken, verblasste Stoffreste, Gold in den Trümmern |
| **Gottesstaat** | Stein und Schiefer, Sackleinen vor den Öffnungen, Mauern; ein senkrechter Tempel, höher als alles andere; die Kanten bleiben handgemacht (Abschnitt 3) | Eine Achse vom alten Ritualplatz zum Tempel, Häuser in Reihen beidseits, Pferch am Rand. Die Achse liegt nie parallel zum Raster | Tempel mit Altar und Rauchsäule. Der alte Feuerring oder Schrein steht im Tempel oder unter ihm | Angst vor dem Gott: Ordnung als Bitte um Schonung. Die Achse beginnt am alten Ort, der Plan erinnert sich | Mauerreste in Linien, das Tempelfundament |

- **Wandel geschieht stückweise.** Eine neue Kultur ersetzt Bau für Bau, von ihrem neuen Ort
  aus nach außen. Eine Mischsiedlung ist ein normaler, sichtbarer Zustand. Technisch ist sie
  ein Wechsel der Parametersätze je Bau.
- **Jede Kultur hinterlässt mindestens einen bleibenden Rest.** So sieht „Die Kultur erinnert
  sich“ aus.
- **Die Größe der Siedlung folgt dem Volk.** Die Zahl der Häuser ergibt sich aus V_t (Start:
  ein Haus je 4–6 Köpfe). Ein verlassenes Haus verfällt und verschwindet nicht.
- **Ruine** (Mitte eines ausgestorbenen oder eroberten Stamms) bewahrt den Grundriss ihrer
  Kultur. Sie verfällt in Stufen: Brandbalken und Mauerstümpfe, dann überwachsene
  Grundmauern, dann ein Steinkreis oder eine Steinlinie im Gras, zuletzt nur noch eine
  Bodenfärbung. Das ist dieselbe Kette wie in Prinzip 1, und das Ende ist nie leeres Gras.

**Texturphilosophie: gezähmtes PBR**

Geprüft wurden drei Wege. Handgemalte Texturen fallen aus, weil sie Licht und Schatten fest
einbacken. Hier ist das Licht aber ein Zustand (Festdämmerung, Dürresonne, Ritual,
Abschnitt 2), und eine gemalte Welt hätte darin das falsche Licht. Sie hat außerdem keinen
Glanz, der auf Kerzen reagiert. Fotorealistische Scans fallen aus, weil sie im Figurenmaßstab
rauschen und die Reserve aus Abschnitt 4 sprengen. Gewählt ist **PBR mit physikalisch
plausiblen Werten, aber gezähmt**. Kulturen werden über Glanz und Rauheit erkannt
(Abschnitt 4), und das funktioniert nur, wenn Materialien physikalisch auf Licht reagieren.
Halbrealistische Haut braucht dasselbe Lichtmodell wie die Welt, sonst wirken die Körper
aufgeklebt.

1. **Albedo ohne Licht.** Kein eingebackener Schatten und keine Ambient Occlusion über Fugen
   im Zentimeterbereich hinaus. Alle Werte liegen in der Erdpalette und halten die Reserve
   ein.
2. **Lücke im Figurenmaßstab.** Boden und Props variieren groß (über ~4 m: Farbfelder nach
   Fruchtbarkeit und Höhe) und fein (unter ~10 cm, nur in den Stufen Mittel und Nah
   sichtbar). Im Figurenmaßstab (0,3–1,5 m) haben sie wenig Kontrast. Die Startgrenze für die
   Grundzoomstufe: In einem Feld von 2 × 2 m schwankt die Helligkeit des Bodens um höchstens
   etwa ±10 %. So ist die Kopf-Schulter-Ellipse mit ihrem Schatten das einzige, was in dieser
   Größe auf dem Boden auffällt.
3. **Helligkeitsbühne.** Böden, auf denen Menschen gehen (Plätze, Pfade, Siedlung), liegen in
   mittlerer Helligkeit (etwa 40–65 %). Getretene Erde ist von Natur aus heller als der Boden
   ringsum. Indigo liegt darunter, Kreide darüber, so heben sich beide Stämme im
   Graustufenbild ab. In der Dürre steigt der Boden nicht über etwa 70 % Helligkeit.
   Brandstellen sind nicht flächig schwarz, sondern Ruß mit Ascheflecken (`#8C877E`), damit
   eine mittlere Helligkeit bleibt. Indigo auf Ruß bleibt trotzdem das schwächste Paar und
   lebt dort von Bewegung, Sonnenkante und Farbton.
4. **Quellen.** CC0-Bibliotheken (z. B. ambientCG, Poly Haven, Lizenz je Asset prüfen) sind der
   Ausgangspunkt. Ihre Texturen werden in die Palette gezogen: entsättigt, mit gesenkter
   Normalstärke und gemilderten feinen Kontrasten.
5. **Ein Master-Material für Bauten und Props**, mit Parametersätzen je Kultur und
   Zustandsparametern (Ruß, Abnutzung, Verfall). Spuren an Bauten sind dadurch billige
   Parameterwechsel: Ein Haus neben einer Brandstelle bekommt Ruß, ohne dass ein neues Asset
   entsteht.
6. **Gelände aus Daten.** Wenige Schichten, gesteuert von der Simulation: Lehm und Gras-Oliv
   (nach f_c), Fels (nach der Steigung aus h_c), Sand (Küste), getretene Erde (Plätze und
   Pfade), Ruß und Asche (Brand), Risse (Dürre). *[Pipeline-abhängig]* URP rechnet 4
   Geländeschichten je Durchgang, HDRP 8. Wie viele Schichten an einer Stelle zusammenkommen,
   kostet also je nach Pipeline anderes.
7. **Figuren bekommen eine höhere Texeldichte als die Umgebung.** Die Zahlen legt Abschnitt 8
   fest.

**Dichte der Props je Gebiet**

Die Dichte treiben drei Dinge in dieser Reihenfolge: die Daten (Fruchtbarkeit, Volk,
Ereignisse), die Lesbarkeit und das Budget. Props in Figurengröße (Körbe, Fässer, Steine,
Büsche, 0,3–1,5 m) sind die größte Gefahr für die Lesbarkeit, weil sie von oben wie Köpfe
aussehen. Sie stehen deshalb in Gruppen an Wänden und Rändern, nie lose auf Plätzen und
Pfaden, und sie bewegen sich nicht.

| Gebiet | Dichte | Treiber | Regel |
|---|---|---|---|
| **Unberührtes Land** | Niedrig, in Massen | f_c, h_c | Wald und Busch in großen Gruppen auf fruchtbarem Land, Fels und Gras auf kargem; Lichtungen statt Streu |
| **Küste** | Sehr niedrig | – | Sand, Treibholz, Fels; das Meer in Petrol |
| **Siedlung** | Mittel bis hoch, nur am Rand der Bauten | V_t, Kultur | Plätze und Pfade sind zu mindestens etwa 60 % frei, Hausrat steht an den Wänden |
| **Festplatz** | Mittel, als Ring | Fest | Kerzen, Feuerschalen und Stoffbahnen im Kreis, das Innere bleibt frei; die Anordnung ist das Zeichen |
| **Ritualplatz** | Niedrig, geordnet | Kultur | Ein senkrechter Mittelpunkt (Feuer, Altar, Säule) und kaum mehr; die Leere trägt die Andacht |
| **Brandstelle** | Sehr niedrig | Blitz | Verkohlte Stümpfe und Asche; die Leere ist das Zeichen |
| **Ruine** | Mittel, verstreut | Verfallsstufe | Trümmer, die mit der Zeit überwachsen; die Dichte sinkt, die Form bleibt |
| **Pferch** | Niedrig | Gefangene | Zaun oder Mauer und Pfosten, immer am Rand der Siedlung |

**Umwelt-Erzählung**

Jede Spur beantwortet ohne Text drei Fragen. **Was** geschah, sagt die Form. **Wer** es war,
sagen Material und Farbe (Kultur, Stamm, Gott). **Wie lange** es her ist, sagt die
Verwitterung.

| Spur oder Ort | Erzählt | Frisch | Gealtert (vereinfacht, nie gelöscht) |
|---|---|---|---|
| **Brandstelle** (Welt & Insel, Regeln 16 und 17) | Die Strafe des Gottes | Glut, Ruß, Asche, verkohlte Stümpfe | Verblasst während der Frist von 39 Ernten. Danach bleibt sie als alte Narbe: dunkleres, anderes Gras und Stümpfe, nie ganz gelöscht (Welt & Insel) |
| **Ruine** (Regel 17) | Untergang oder Eroberung | Brandbalken, eingestürzte Wände | Verfallsstufen bis zur Bodenfärbung |
| **Festplatz** (Regel 17) | Lust als Kultur | Kerzenkreis, Stoffbahnen, Amber | Kerzenreste, Kränze, Wachs; zuletzt ein dunklerer Kreis im Gras |
| **Ritualplatz** (Regel 17) | Hingabe | Rauchsäule, blauweiße Funken | Ein aufgegebener Platz bleibt als Steinsetzung |
| **Siedlung** (Regel 17) | Stamm, Kultur, Größe | – | Mischsiedlung, Reste der alten Kultur |
| **Erschaffungsring** (Abschnitt 2) | Schöpfung ohne Geburt | Ring aus niedergedrücktem Gras | Ein blasser Ring |
| **Wurffurche** (Abschnitt 2) | Die Gotteshand | Furche und Staub | Eine flache Mulde |
| **Grab** | Tod, je Kultur in anderer Form | Steinhaufen (Stammesrat), Hügel mit Stoffband (Lust-Sekte), Reihengrab (Gottesstaat) | Bewachsener Hügel; die Zahl der Gräber erzählt Krieg und Hunger |
| **Pfade** | Wohin Menschen gehen: Ritual, Fest, zum anderen Stamm | Entstehen aus der Bewegung der Figuren | Wachsen langsam zu, wenn niemand sie nutzt |
| **Dürre** | Hunger und Zusehen | Risse, welke Pflanzen, Staub | Abgestorbene Bäume bleiben als Gerippe |
| **Pferch** | Gefangenschaft | Besetzt | Ein leerer Pferch bleibt stehen |

- **Ereignisse der Masse hinterlassen Zeichen in der Welt.** Was viele trifft (Krieg, Hunger,
  Untergang), zeigt sich an Gräbern, Ruinen und Pfaden, nicht an einzelnen Körpern. Das ist
  das Gegenstück zur Stufe Menge in Abschnitt 5.
- **Die Spuren haben ein Budget.** Ist es erreicht, rutschen die ältesten Spuren eine Stufe der
  Kette hinab: Mesh, dann Decal, dann Materialparameter, dann Bodenfärbung. Entfernt wird
  keine. Die letzte Stufe kostet fast nichts, denn Welt & Insel speichert Jahr und Ursache des
  letzten Brands je Zelle für immer. Eine Narbenkarte als Textur in der Auflösung des Rasters
  oder feiner reicht dafür (Umsetzung: Technical Artist).

**Was die Rasterinsel im Meilenstein mindestens leisten muss**

Die Simulation rechnet in Zellen, die Ansicht zeigt keine. Sonst trüge die Welt die Form des
Gottes (Abschnitt 3).

1. **Höhe:** eine durchgehende Fläche, aus h_c weich interpoliert, mit Rauschen unterhalb der
   Zellgröße. Keine Stufen und keine Plateaus je Zelle.
2. **Küste:** eine weiche, verrauschte Kontur um das Land mit einem Strandband. Das Meer ist
   Petrol und wird zur Tiefe hin dunkler.
3. **Bodenübergänge:** Die Schichten der Fruchtbarkeit werden über mindestens eine halbe
   Zellbreite mit einer Rauschmaske überblendet. Keine Zelle erscheint als Farbquadrat.
4. **Brandstellen:** Das 3 × 3-Feld erscheint als ausgefranster Fleck um den Einschlag, nicht
   als Quadrat. Jede verbrannte Zelle ist auf dem größten Teil ihrer Fläche dunkel, der Rand
   darf bis etwa eine Viertelzelle darüber hinausgreifen. Das Bild und die Ansicht „Zelle
   ansehen“ dürfen sich nicht widersprechen.
5. **Gebietsgrenzen:** Pfähle mit Bannern in der Welt oder eine weiche Linie in der UI, nie die
   Treppenkante der Zellen.
6. **Platzierung:** Positionen innerhalb der Zellen versetzt, Drehungen zufällig. Keine Reihe
   von Bäumen oder Hütten entlang einer Rasterachse; die Achse des Gottesstaats ist gegen das
   Raster gedreht.
7. **Wege:** Figuren gehen weiche Kurven zwischen ihren Zielen und nicht von Zellmitte zu
   Zellmitte. Chebyshev-Wege zeigten sich sonst als Zickzack in 45°.

*Test:* Ein Screenshot der Grundzoomstufe geht an jemanden, der das Spiel nicht kennt.
Erkennt diese Person ein Gitter oder kann sie Zellen zählen, ist der Test nicht bestanden.
Ergänzend gilt: Im Gelände gibt es keine gerade Kante, die länger als zwei Zellen ist und
nicht gebaut wurde.

**Lesbarkeit gegen Abschnitt 5**

Drei Regeln halten die Figuren vor dieser Umgebung lesbar.
- **Helligkeitsbühne:** Der Boden liegt mittelhell, Indigo darunter, Kreide darüber.
- **Lücke im Figurenmaßstab:** Kein Bodenmuster und kein loses Prop konkurriert mit der
  Kopf-Schulter-Ellipse.
- **Die Bewegung gehört den Menschen:** Bewegung in der Umgebung ist entweder groß und langsam
  (Wolkenschatten, Rauch) oder winzig (Gras). Im Figurenmaßstab bewegt sich schnell nur ein
  Mensch.

*Test:* Graustufen-Screenshot der Grundzoomstufe auf Gras, auf Lehm, auf einem Platz, auf
einer Brandstelle und in der Dürre, jeweils mit beiden Stämmen. In jedem Bild ist jede Figur
als Figur zu finden.

*Offen für das GDD Weltansicht & Kamera:* Wo die eine Zoomstufe des MVP-0 liegt. Vorschlag des
Art Directors: Figuren in der Bildmitte nehmen etwa 5–8 % der Bildhöhe ein (Stufe Mittel in
der Mitte, Fern am Rand), und die Startansicht ist nicht parallel zum Raster ausgerichtet.

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
