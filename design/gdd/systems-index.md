# Systems Index: Allmacht

> **Status**: Draft
> **Created**: 2026-10-25
> **Last Updated**: 2026-10-25
> **Source Concept**: design/gdd/game-concept.md

---

## Overview

Allmacht ist eine Gottsimulation: Eine abstrakte, deterministische Simulation ohne
Grafik ist die Autorität, die 3D-Welt ist nur eine Ansicht davon (Pillar 1: Die Welt
lebt ohne dich). Der Spieler greift mit der Gotteshand und Götterkräften ein. Jeder
Eingriff, auch jeder Wurf, wird als festes Ereignis in die Erinnerung der Kultur
geschrieben und ist in der Chronik nachvollziehbar (Pillar 2). Lust ist ab MVP-0 Teil
der Systeme und sichtbar: Begehren und Normen pro Kultur, einfache Szenen, Harem und
Rituale, alles hinter einem einzigen Zustimmungsmodell (Pillar 3). Es gibt keine
Verwaltung (Pillar 4).

Die **MVP**-Stufe entspricht dem MVP-0 des Konzepts. Die **Vertical Slice**-Stufe
fasst MVP-1 und v1 des Konzepts zusammen. Regeln für diese Phase
(`production/roadmap.md`): GDDs nur für MVP-Systeme, höchstens 2 Review-Runden pro
GDD.

**MVP-Systeme kurz:**

- **Simulationskern**: fester Takt in simulierter Zeit, Zeitsteuerung (Pause, 1×,
  4×, 16×), Zufall mit Seed, Ereignisse als Daten. Legt die Grenze zur Physik fest:
  Physikergebnisse kommen nur als festes Ereignis (wer, wen, Ergebnis,
  Verletzungsklasse) in die Simulation, nie als roher Zustand. Legt auch fest, ob
  „5–10 Hz pro Agent“ und „1 Hz pro Stamm“ in simulierter Zeit gelten und was bei
  Schnellvorlauf gröber rechnet.
- **Welt & Insel**: die Insel als Daten, mit Gelände, Orten, Nahrung und Dürre.
- **Menschen & Bevölkerung**: ein Datensatz pro Mensch (Eigenschaften, Stamm,
  Zustand). Es gibt nur Erwachsene, keine Kinder. Dazu Tod und die passive Gnade aus
  Ritualen.
- **Zustimmungsmodell**: eine reine Prüffunktion, die im Zweifel ablehnt. Eingaben
  sind nur die Eigenschaften der Figur und die Normen ihrer Kultur, nie Status,
  Hingabe oder ein Eingriff. Sie setzt alle nicht verhandelbaren Grenzen durch und
  wird mit Unit- und Property-Tests geprüft.
- **Stämme & Diplomatie**: Stammeslogik, Abspaltung, Beziehungen und abstrakter
  Krieg. Krieg darf den Kulturwandel nicht überdecken (Prototyp 1: 1620
  Kriegserklärungen gegen 393 Kulturwechsel).
- **Kultur & Erinnerung**: die Kulturachse Lust-Sekte ↔ Gottesstaat, die Normen und
  ein begrenztes Ereignisprotokoll aller Eingriffe mit Verfall.
- **Begehren & Lust**: Begehren, Normen pro Kultur, Feste und einfache sichtbare
  Szenen auf dem Grundkörper (keine Paar-Animationen). Die Wirkungen sind in Pillar 3
  aufgelistet: Gotteskraft, Zusammenhalt, Kulturwandel, Diplomatie.
- **Harem & Rituale**: Harem als soziale Struktur, nur aus freien Mitgliedern, die
  zustimmen. Gefangene fallen sofort heraus, wer befreit wird, kommt nur über das
  Zustimmungsmodell zurück. Dazu Eifersucht und Rituale, die Gotteskraft und neue
  Erwachsene bringen.
- **Gotteskraft & Götterkräfte**: Gotteskraft als Ressource, Zufluss aus Hingabe
  und Ritualen. Vier Kräfte (Blitz, Fest-Segen, Erwachsene erschaffen, eine vierte
  offen), abgestimmt auf etwa einen Eingriff alle 30 Sekunden.
- **Körper & Animation**: offener Grundkörper ohne Morphs, vorhandene Animationen,
  Wegfindung und ein Validierungstest, dass keine Kombination kindliche Proportionen
  ergibt.
- **Weltansicht & Kamera**: 3D als Ansicht der Simulation, Figuren nur nahe der
  Kamera, Detailstufen, eine Zoomstufe.
- **Gotteshand & Ragdoll**: Packen, Werfen, Ablegen und Verletzungen. Jede Aktion
  ist ein Eingriff in die Kultur (Prototyp 2: Werfen allein trägt nur 5–10 Minuten).
  Ragdoll und Verletzung nie in einem sexuellen Kontext.
- **Chronik**: Ereignisse mit Ursache und Wirkung, Gewichtung und Filter. Prüft
  die Lesbarkeit bei etwa einem großen Ereignis alle 3 Sekunden.
- **Speichern & Laden**: Der Zustand der Simulation ist vollständig serialisierbar,
  die Ansicht wird daraus neu aufgebaut.
- **Debug- & Balancing-Werkzeuge**: Schnellvorlauf ohne Grafik, Auswertung über 100
  Welten × 10 Ären wie in Prototyp 1, Debug-Anzeigen.
- **HUD & Chronik-Ansicht**: Gotteskraft, Kräfte-Leiste, Zeitsteuerung, Meldungen
  der Welt und die Chronik. Die Bildschirme entstehen mit `/ux-design`.

---

## Systems Enumeration

| # | System Name | Category | Priority | Status | Design Doc | Depends On |
|---|-------------|----------|----------|--------|------------|------------|
| 1 | Simulationskern | Core | MVP | In Review | design/gdd/simulationskern.md | — |
| 2 | Welt & Insel (inferred) | Core | MVP | In Review | design/gdd/welt-und-insel.md | Simulationskern |
| 3 | Menschen & Bevölkerung | Core | MVP | In Review | design/gdd/menschen-und-bevoelkerung.md | Simulationskern, Welt & Insel, Kultur & Erinnerung (nur Stammeshingabe, lesend) |
| 4 | Zustimmungsmodell | Gameplay | MVP | In Review | design/gdd/zustimmungsmodell.md | Menschen & Bevölkerung, Kultur & Erinnerung (Normen als Eingabe) |
| 5 | Stämme & Diplomatie | Gameplay | MVP | Not Started | — | Menschen & Bevölkerung, Welt & Insel |
| 6 | Kultur & Erinnerung | Gameplay | MVP | Not Started | — | Stämme & Diplomatie, Menschen & Bevölkerung |
| 7 | Begehren & Lust | Gameplay | MVP | Not Started | — | Zustimmungsmodell, Kultur & Erinnerung, Menschen & Bevölkerung, Körper & Animation |
| 8 | Harem & Rituale | Gameplay | MVP | Not Started | — | Begehren & Lust, Zustimmungsmodell, Stämme & Diplomatie, Gotteskraft & Götterkräfte |
| 9 | Gotteskraft & Götterkräfte | Economy | MVP | Not Started | — | Stämme & Diplomatie, Kultur & Erinnerung, Menschen & Bevölkerung |
| 10 | Körper & Animation | Core | MVP | Not Started | — | Menschen & Bevölkerung |
| 11 | Weltansicht & Kamera | Core | MVP | Not Started | — | Simulationskern, Welt & Insel, Menschen & Bevölkerung, Körper & Animation |
| 12 | Gotteshand & Ragdoll | Gameplay | MVP | Not Started | — | Simulationskern, Menschen & Bevölkerung, Kultur & Erinnerung, Weltansicht & Kamera, Körper & Animation |
| 13 | Chronik | Narrative | MVP | Not Started | — | Simulationskern, Kultur & Erinnerung |
| 14 | Speichern & Laden | Persistence | MVP | Not Started | — | Simulationskern |
| 15 | Debug- & Balancing-Werkzeuge | Meta | MVP | Not Started | — | Simulationskern |
| 16 | HUD & Chronik-Ansicht (inferred) | UI | MVP | Not Started | — | Gotteskraft & Götterkräfte, Chronik, Simulationskern, Weltansicht & Kamera |
| 17 | Ziel-Befehle | Gameplay | Vertical Slice | Not Started | — | Stämme & Diplomatie, Kultur & Erinnerung |
| 18 | Erwählte | Gameplay | Vertical Slice | Not Started | — | Menschen & Bevölkerung, Chronik |
| 19 | Besessen-Sein | Gameplay | Vertical Slice | Not Started | — | Erwählte, Weltansicht & Kamera, Körper & Animation |
| 20 | Sichtbarer Krieg | Gameplay | Vertical Slice | Not Started | — | Stämme & Diplomatie, Weltansicht & Kamera, Gotteshand & Ragdoll |
| 21 | Haut & Wunden in Detailstufen | Core | Vertical Slice | Not Started | — | Körper & Animation, Gotteshand & Ragdoll |
| 22 | Körpervariation (Morphs, Look B) | Core | Vertical Slice | Not Started | — | Körper & Animation |
| 23 | Zweite Zoomstufe | Core | Vertical Slice | Not Started | — | Weltansicht & Kamera |
| 24 | Bleibende Spuren in der Welt | Gameplay | Vertical Slice | Not Started | — | Weltansicht & Kamera, Chronik |
| 25 | Audio | Audio | Vertical Slice | Not Started | — | Gotteskraft & Götterkräfte, Gotteshand & Ragdoll, Weltansicht & Kamera |
| 26 | Weitere Gesellschaftsformen | Gameplay | Alpha | Not Started | — | Kultur & Erinnerung, Stämme & Diplomatie, Zustimmungsmodell |
| 27 | Paar-Animationen | Core | Alpha | Not Started | — | Begehren & Lust, Körpervariation (Morphs, Look B) |
| 28 | Zeitalter mit Inhalten | Progression | Alpha | Not Started | — | Kultur & Erinnerung, Welt & Insel |
| 29 | Einstieg und Tutorial (inferred) | Meta | Alpha | Not Started | — | HUD & Chronik-Ansicht, Gotteskraft & Götterkräfte |
| 30 | Einstellungen (inferred) | Persistence | Alpha | Not Started | — | Speichern & Laden |
| 31 | Barrierefreiheit (inferred) | Meta | Alpha | Not Started | — | HUD & Chronik-Ansicht |
| 32 | Fantasy- und Tiervölker | Gameplay | Full Vision | Not Started | — | Menschen & Bevölkerung, Körpervariation (Morphs, Look B), Zustimmungsmodell |
| 33 | Nahtloser Zoom | Core | Full Vision | Not Started | — | Weltansicht & Kamera, Zweite Zoomstufe |

Android ist kein System, sondern eine eigene Plattform-Stufe (siehe Konzept, Scope
Tiers).

---

## Categories

| Category | Description | Typical Systems |
|----------|-------------|-----------------|
| **Core** | Foundation systems everything depends on | Simulationskern, Welt, Menschen, Körper, Weltansicht |
| **Gameplay** | The systems that make the game fun | Stämme, Kultur, Zustimmung, Lust, Harem, Gotteshand |
| **Progression** | How the player grows over time | Zeitalter |
| **Economy** | Resource creation and consumption | Gotteskraft & Götterkräfte |
| **Persistence** | Save state and continuity | Speichern & Laden, Einstellungen |
| **UI** | Player-facing information displays | HUD & Chronik-Ansicht |
| **Audio** | Sound and music systems | Audio |
| **Narrative** | Story and dialogue delivery | Chronik |
| **Meta** | Systems outside the core game loop | Debug- & Balancing-Werkzeuge, Einstieg, Barrierefreiheit |

---

## Priority Tiers

| Tier | Definition | Target Milestone | Design Urgency |
|------|------------|------------------|----------------|
| **MVP** | Required for the core loop to function. Without these, you can't test "is this fun?" Entspricht MVP-0 des Konzepts | First playable prototype | Design FIRST |
| **Vertical Slice** | Required for one complete, polished area. Demonstrates the full experience. Fasst MVP-1 und v1 des Konzepts zusammen | Vertical slice / demo | Design SECOND |
| **Alpha** | All features present in rough form. Complete mechanical scope, placeholder content OK. | Alpha milestone | Design THIRD |
| **Full Vision** | Polish, edge cases, nice-to-haves, and content-complete features. | Beta / Release | Design as needed |

Review gates for this index: TD-SYSTEM-BOUNDARY, PR-SCOPE and CD-SYSTEMS skipped —
Lean mode.

---

## Dependency Map

### Foundation Layer (no dependencies)

1. Simulationskern — alles läuft über seinen Takt und seine Ereignisse; er ist die
   Autorität, 3D ist nur eine Ansicht

### Core Layer (depends on foundation)

1. Welt & Insel — depends on: Simulationskern
2. Menschen & Bevölkerung — depends on: Simulationskern, Welt & Insel
3. Körper & Animation — depends on: Menschen & Bevölkerung
4. Weltansicht & Kamera — depends on: Simulationskern, Welt & Insel, Menschen & Bevölkerung, Körper & Animation
5. Speichern & Laden — depends on: Simulationskern
6. Debug- & Balancing-Werkzeuge — depends on: Simulationskern

### Feature Layer (depends on core)

1. Stämme & Diplomatie — depends on: Menschen & Bevölkerung, Welt & Insel
2. Kultur & Erinnerung — depends on: Stämme & Diplomatie, Menschen & Bevölkerung
3. Zustimmungsmodell — depends on: Menschen & Bevölkerung, Kultur & Erinnerung (nur die Normen als Eingabe)
4. Begehren & Lust — depends on: Zustimmungsmodell, Kultur & Erinnerung, Menschen & Bevölkerung, Körper & Animation
5. Harem & Rituale — depends on: Begehren & Lust, Zustimmungsmodell, Stämme & Diplomatie, Gotteskraft & Götterkräfte
6. Gotteskraft & Götterkräfte — depends on: Stämme & Diplomatie, Kultur & Erinnerung, Menschen & Bevölkerung
7. Gotteshand & Ragdoll — depends on: Simulationskern, Menschen & Bevölkerung, Kultur & Erinnerung, Weltansicht & Kamera, Körper & Animation
8. Chronik — depends on: Simulationskern, Kultur & Erinnerung

### Presentation Layer (depends on features)

1. HUD & Chronik-Ansicht — depends on: Gotteskraft & Götterkräfte, Chronik, Simulationskern, Weltansicht & Kamera

### Polish Layer (depends on everything)

1. Keine MVP-Systeme. Einstieg und Tutorial, Einstellungen und Barrierefreiheit
   folgen in der Alpha.

**Engpässe:** Simulationskern (alle), Menschen & Bevölkerung (fast alle), Kultur &
Erinnerung (6 Systeme), Zustimmungsmodell (alles Sexuelle).

---

## Recommended Design Order

Zustimmungsmodell und Begehren & Lust werden auf Wunsch des Entwicklers so früh wie
möglich entworfen. Das Zustimmungsmodell kommt direkt nach den Menschen und liest die
Normen der Kultur nur über eine festgelegte Schnittstelle.

| Order | System | Priority | Layer | Agent(s) | Est. Effort |
|-------|--------|----------|-------|----------|-------------|
| 1 | Simulationskern | MVP | Foundation | systems-designer, technical-director | L |
| 2 | Menschen & Bevölkerung | MVP | Core | systems-designer, game-designer | M |
| 3 | Zustimmungsmodell | MVP | Feature | systems-designer, game-designer | M |
| 4 | Welt & Insel | MVP | Core | world-builder, systems-designer | S |
| 5 | Stämme & Diplomatie | MVP | Feature | systems-designer, ai-programmer | M |
| 6 | Kultur & Erinnerung | MVP | Feature | systems-designer, game-designer | L |
| 7 | Begehren & Lust | MVP | Feature | game-designer, systems-designer | L |
| 8 | Harem & Rituale | MVP | Feature | game-designer, systems-designer | M |
| 9 | Gotteskraft & Götterkräfte | MVP | Feature | economy-designer, game-designer | M |
| 10 | Körper & Animation | MVP | Core | technical-artist, gameplay-programmer | M |
| 11 | Weltansicht & Kamera | MVP | Core | gameplay-programmer, engine-programmer | M |
| 12 | Gotteshand & Ragdoll | MVP | Feature | game-designer, gameplay-programmer | M |
| 13 | Chronik | MVP | Feature | game-designer, ux-designer | M |
| 14 | Speichern & Laden | MVP | Core | engine-programmer | S |
| 15 | Debug- & Balancing-Werkzeuge | MVP | Core | tools-programmer | S |
| 16 | HUD & Chronik-Ansicht | MVP | Presentation | ux-designer, ui-programmer | M |

Effort estimates: S = 1 session, M = 2-3 sessions, L = 4+ sessions.

---

## Circular Dependencies

- **Gotteskraft & Götterkräfte ↔ Harem & Rituale**: Rituale speisen die
  Gotteskraft, Rituale brauchen deren Zuflussregeln. Lösung: Die Gotteskraft legt
  fest, über welche Ereignisarten sie Zufluss bekommt (Hingabe, Ritual), und
  Rituale melden diese Ereignisse nur. Die Gotteskraft kennt keine Rituale.
- **Kultur & Erinnerung ↔ Begehren & Lust**: Lust verändert die Kultur (Lust-Sekte),
  die Kultur legt die Normen für Lust fest. Lösung: Die Kultur besitzt die Normen und
  nimmt Ereignisse entgegen. Lust liest die Normen und meldet Ereignisse.
- **Kultur & Erinnerung ↔ Menschen & Bevölkerung**: Die Kultur braucht die Menschen,
  die Gnade der Menschen braucht die Stammeshingabe D_t der Kultur. Lösung: Kultur &
  Erinnerung stellt D_t nur lesbar bereit, Menschen schreibt nie in die Kultur
  (nachgetragen mit dem GDD Menschen & Bevölkerung).
- **Kultur & Erinnerung ↔ Gotteshand & Ragdoll / Gotteskraft & Götterkräfte**:
  Eingriffe verändern die Kultur, die Kultur reagiert auf Eingriffe. Lösung: Die
  Kultur bietet „Eingriff erfassen“ an, Hand und Kräfte benutzen das. Aus der Physik
  kommt nur ein festes Ereignis in die Simulation (Simulationskern).

---

## High-Risk Systems

| System | Risk Type | Risk Description | Mitigation |
|--------|-----------|-----------------|------------|
| Simulationskern | Technical | Ragdoll-Physik ist nicht deterministisch, Schnellvorlauf und Speichern brechen, wenn rohe Physik in die Simulation gelangt. Zeitbasis der Taktraten ist offen | Grenze Physik → festes Ereignis im GDD festlegen, Taktraten in simulierter Zeit; ADR in der Architekturphase |
| Zustimmungsmodell | Design | Muss jede sexuelle Handlung prüfen und im Zweifel ablehnen; eine Lücke würde eine nicht verhandelbare Grenze brechen | Reine Funktion ohne Gotteskraft, Hingabe oder Status als Eingabe; Unit- und Property-Tests von Anfang an; als erstes Gameplay-GDD |
| Begehren & Lust | Scope | Sichtbare Szenen schon im MVP-0 brauchen passende Animationen für den Grundkörper; Uncanny Valley | Einfache Szenen (Ausblende oder einfache Animation), keine Paar-Animationen; vorhandene Animationen mit geprüfter Lizenz |
| Harem & Rituale | Scope | Im MVP-0 statt in v1: deutlich mehr Inhalt, Regeln für Eifersucht, Gefangene und Befreiung | Nur freie Mitglieder, Gefangene fallen heraus; MVP-0 mit `/estimate` neu schätzen |
| Gotteshand & Ragdoll | Design | Werfen allein trägt nur 5–10 Minuten (Prototyp 2) | Jede Hand-Aktion als Eingriff in Kultur & Erinnerung, messbares MVP-0-Kriterium für das Zusammenspiel von Hand und Simulation |
| Kultur & Erinnerung | Design | Krieg überdeckt den Kulturwandel (Prototyp 1), die Spuren des Spielers gehen in einer belebten Welt unter | Vielfalt der Ereignisarten, Gewichtung in Chronik und Erinnerung; Messung mit den Debug-Werkzeugen wiederholen |
| Körper & Animation | Technical | Ragdoll-Budget bisher nur mit Grundkörpern gemessen; Wegfindung für viele Agenten oft unterschätzt | Mit dem offenen Grundkörper neu messen, bis dahin 8–16 aktive Ragdolls |

---

## Progress Tracker

| Metric | Count |
|--------|-------|
| Total systems identified | 33 |
| Design docs started | 4 |
| Design docs reviewed | 4 |
| Design docs approved | 0 |
| MVP systems designed | 4/16 |
| Vertical Slice systems designed | 0/9 |

---

## Next Steps

- [ ] Review and approve this systems enumeration
- [ ] `/estimate` für MVP-0 (beschlossen am 2026-10-25, MVP-0 ist um Harem, Szenen, Krieg und 3 Kräfte gewachsen)
- [ ] Design MVP-tier systems first (use `/design-system [system-name]`), beginnend mit dem Simulationskern
- [ ] Run `/design-review` on each completed GDD (höchstens 2 Runden)
- [ ] Run `/gate-check technical-setup` when MVP systems are designed
- [ ] Validate the highest-risk systems with `/vertical-slice` before committing to Production
