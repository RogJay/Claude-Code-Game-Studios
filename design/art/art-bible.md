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
