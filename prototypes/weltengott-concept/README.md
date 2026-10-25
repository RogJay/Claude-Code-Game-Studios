<!-- PROTOTYPE - NOT FOR PRODUCTION -->
<!-- Question: Does an unattended tribe simulation produce histories worth watching, and can the player trace culture changes back to their own interventions? -->
<!-- Date: 2026-10-25 -->

# Weltengott — Concept Prototype 1

**Status:** concluded 2026-10-25, verdict **PROCEED** (see `REPORT.md`)

## Hypothesis

If the player watches two tribes on an island for 10 eras and intervenes about
5 times with 2 powers, they feel they shaped a culture worth watching. True if:

1. unattended, every era produces at least one major event and the world neither
   dies out nor freezes within 10 eras;
2. at least 2 culture changes can be traced in the Chronik back to the player's
   own act;
3. the player wants to start a new world afterwards.

Riskiest assumption under test: an unattended simulation produces interesting
histories by itself (TD-FEASIBILITY prototype 1 kill criterion, see
`design/gdd/game-concept.md`).

## How to run

Double-click `prototype.html`. It opens in any browser; no server or install.

- Speed: ❚❚ / 1× / 4× / 16× (1× = 2 years per second, one era = 25 years).
- Powers: click **Blitz** or **Fest-Segen**, then click a tribe on the map or in
  the list.
- **Neue Welt** with a seed number gives a reproducible world.
- **Auswertung** runs 100 worlds for 10 eras with no interventions and lists
  extinction, frozen eras, early domination and events per era.

## What is in it (and what is cut)

In: island of fertility cells; tribes with population (adults only, created by
the god: a passive grace fed by rituals plus Fest-Segen), food, drought and
famine; one culture axis (Gottesstaat ↔ Lust-Sekte) with hysteresis; grievance
(border, famine envy, faith conflict, schism, revenge, and god-caused) → war,
peace, conquest; schisms that found new tribes; per-culture memory of god
interventions with decay; a Chronik whose "why" is taken from the largest
recorded cause.

Cut: 3D, ragdoll, possession, harem, slavery, any sexual content (festivals are
abstract text), save/load, menus, sound, polish.

## Automated observations (2026-10-25, before the user's playtest)

Measured by running the same simulation code headless in Node; these are
observations, not the verdict.

Unattended, 100 worlds × 10 eras:

| Observation | Value |
|---|---|
| Worlds where every tribe dies out | 0 % |
| Worlds with at least one era without a major event | 44 % |
| Eras without a major event, all worlds | 60 of 1000 (29 of them in era I) |
| Only one tribe (or none) left after era V | 0 % |
| Major events per era (mean) | 4.4 |
| Tribes alive at the end (mean) | 5.0 |
| Worlds meeting criterion 1 | 56 % |
| Major events by kind (all 100 worlds) | War 1620 · Conquest 838 · Schism 1008 · Peace 500 · Culture change 393 |

Scripted player (5 planned interventions, 100 worlds): on average 4.3
interventions were affordable. 89 of 100 worlds had at least 2 major events whose
Chronik cause is the player's act (criterion 2, mechanically).

Tuning so far: first run had no wars at all, unbounded growth and every culture
drifting to Lust-Sekte; fixed by finite land capacity, per-tribe temperament,
dissenting schisms and a lower war threshold. War count now looks high and may
read as noise; that is for the playtest to judge.

## Findings

See `REPORT.md`. In short: the simulation works as a foundation (alive, traceable
cause and effect, attachment to a favoured tribe), but the playtest found it "too
flat": nothing visible, only 2 powers, a repetitive war-heavy Chronik, too much
waiting. These become design requirements for the GDDs; feel is untested
(prototype 2).
