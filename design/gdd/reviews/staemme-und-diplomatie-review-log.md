# Review Log — Stämme & Diplomatie

## Review — 2026-10-26 — Verdict: NEEDS REVISION
Review mode: full · Workflow: standard
Scope signal: L
Specialists: systems-designer, game-designer, qa-lead, creative-director (night run, no developer sign-off)
Blocking items: 11 | Recommended: 22
Summary: The simulation core is a correct port: every example, constant and RNG step matches prototype 1. What failed was the edges: player-caused wars were labelled with another ground (Pillar 2), conquest and extinction broke the Menschen invariants and had no successor, the split contract could not be executed as written, small-V paths were undefined, and the ACs lacked test doubles and exact numbers. All fixes are outcome-neutral; calibration-changing ideas went to Open Questions.
Prior verdict resolved: First review
Findings:
- [BLOCKING] Rule 8 / Formula 2: largest single ground mislabels player-caused wars; aggregate Gott grounds, counterfactual cause, full grounds list [game-designer]
- [BLOCKING] UI Requirements: only outgoing grudge as absolute stages; need Kriegsnähe with blockers and incoming list [game-designer]
- [BLOCKING] Acceptance Criteria: no AC tests the fantasy (blessing → war cause) [game-designer]
- [BLOCKING] Rules 10, 17, 18: held people and act participants at conquest/extinction contradict Menschen Rule 20, break V ≥ Einzelne, no Tod events, no stored successor [systems-designer]
- [BLOCKING] Rules 15, 19: split contract (zero-cell check impossible for Welt, territory timing, order of orders) [systems-designer]
- [BLOCKING] Rule 19 / Formula 1: V < 3 paths, division by zero in wealth, r_t below 3 undefined; AC-11 premise [systems-designer]
- [BLOCKING] Acceptance Criteria: test doubles undefined (random stream, event sink, order recording, god stub, config, step seam) [qa-lead]
- [BLOCKING] AC-06: 0.140426 vs 1e-9 tolerance [qa-lead]
- [BLOCKING] AC-03: V_t = 11 sub-case does not isolate; grounds and boundaries missing [qa-lead]
- [BLOCKING] AC-05 / AC-09 / AC-13 / AC-14: swapped dice invisible, unreachable rounding clause, 240 Platzsuche draws unscripted, no oracle for order, wrong AC type [qa-lead]
- [BLOCKING] Rule 18 / Edge Cases: release time and successor rule contradictory [systems-designer]
- [RECOMMENDED] Formula 3: losses round to 0 for small tribes (empty wars) [systems-designer]
- [RECOMMENDED] Rule 10: prose "nach Köpfen" vs formula [systems-designer]
- [RECOMMENDED] Achsen mischen parameters and order of orders in a fight [systems-designer]
- [RECOMMENDED] Formula 1: hunger semantics (global richest, then range) and missing variables [systems-designer]
- [RECOMMENDED] Tuning Knobs: missing knobs and unsafe ranges (Kriegsschwelle 0.4, decay 0.98) [systems-designer]
- [RECOMMENDED] Groll storage order and pruning [systems-designer]
- [RECOMMENDED] Calibration: undocumented deviations [systems-designer, qa-lead]
- [RECOMMENDED] Formula 5: Frist blocks only 19 years [systems-designer]
- [RECOMMENDED] Rache listed twice [systems-designer, game-designer, qa-lead]
- [RECOMMENDED] Roman numeral cap; Welt flag lifetime [systems-designer]
- [RECOMMENDED] Chronicle text templates per ground [game-designer]
- [RECOMMENDED] Peace reset semantics and the player's trace [game-designer]
- [RECOMMENDED] "Who would take offence" preview as open creative decision [game-designer]
- [RECOMMENDED] AC-08, AC-07, AC-02, AC-10, AC-11, AC-12, AC-15, AC-16 rewrites and boundaries; rules without AC [qa-lead]
- [RECOMMENDED] Calibration-changing proposals to Open Questions with metrics: envy without outlet, Gott decay vs lag, war dominance, step 0 for V < 3, empty wars, peace wiping Gott grounds, stage-crossing chronicle line [game-designer, systems-designer, creative-director]
Disagreement: systems-designer proposed a step 0 that declares V < 3 tribes dead before fights; creative-director ruled to keep the prototype order (calibration) and move step 0 to Open Questions with metrics.
Note: the stamps below are the content AS REVIEWED (before the revisions above), so the next review reads them as CHANGED.
Reviewed-Content-Hash: design/gdd/staemme-und-diplomatie.md 53e1021f13b1f5f4725c7a4f8fbe14d06c5b943a
Reviewed-Content-Hash: design/registry/entities.yaml abcf6d7a8f6bfcc9dc8023aee906f2deb233559a
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md 9c6cdfa69bb7404eec508b9cfe0197da1fe52422
Reviewed-Content-Hash: design/gdd/welt-und-insel.md 0ca0477ba96352b2550a09a477ecf618f2b884d7
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/simulationskern.md 9c82d078d414a4ec566b8d2f9ca3182a416b594b
Reviewed-Content-Hash: design/gdd/zustimmungsmodell.md 99cf8fc6ad97b7233bc2940a8ddfb09065b3cf3d
Reviewed-Content-Hash: design/gdd/game-concept.md 168cdc5ff8f56635a08ebbb674fe517b93ded4c6
Reviewed-Content-Hash: prototypes/weltengott-concept/prototype.html e3c8aa99b445fc83ff5a9ce4fde8919dd9ab26eb
Reviewed-Content-Hash: prototypes/weltengott-concept/REPORT.md e9204c19a61fefb55207be0197cd90bd5576f6e0
Reviewed-Content-Hash: design/gdd/systems-index.md fc1d7909675793842eb99e55815a1a50e76955a7

### Revisions after round 1 — 2026-10-26 (night run, no developer sign-off)

All 11 blocking items and all recommended items within the creative director's "fix tonight" list were applied: Gott grounds aggregated with counterfactual cause and full grounds list plus text templates (Rule 8, Formula 2, AC-04); Kriegsnähe with blockers and the incoming list (UI, AC-20); stored successor with order wechsel → Verlust → erloschen, Tod events, held people and act participants follow the successor (Rules 10, 17, 18, Menschen Rules 2 and 20, AC-13); split cancelled on a living tribe's centre before the Teil draw, order Menschen → Kultur → Welt, territory order immediate (Rules 15, 16, 19, Welt Rule 10); r_t for all V, wealth 0 at V = 0, at-war V < 3 conquered as in the prototype (Edge Cases, AC-12); test doubles and every AC rewritten with exact values (AC-01 to AC-22); knobs and safe ranges with cross-constraints; deviations 5 to 7 listed. Calibration-changing items are Open Questions with metrics.

## Review — 2026-10-26 — Verdict: NEEDS REVISION
Review mode: lean · Workflow: standard
Scope signal: L
Specialists: none (lean; contract-focused re-review as the creative director asked); night run, no developer sign-off
Blocking items: 1 | Recommended: 0
Summary: All 11 round-1 blockers are resolved. The contract re-check against Menschen, Welt, Kern and Zustimmungsmodell found one gap: Menschen Rule 23 did not list the new order "Stamm erloschen", and the moment a freed person passes to the successor was undefined for the end of an intimate act. Fixed after the review (Menschen Rules 2 and 23, Stämme Rule 18). This is the last round under the roadmap cap; status In Review, approval is the developer's call.
Prior verdict resolved: Yes
Findings:
- [BLOCKING] Dependencies / Menschen Rule 23: order "Stamm erloschen" missing from the order list; release timing to the successor undefined after an act ends
Note: staemme-und-diplomatie.md and menschen-und-bevoelkerung.md are stamped AS REVIEWED (before the fix above).
Reviewed-Content-Hash: design/gdd/staemme-und-diplomatie.md 545fcd5c2a332f01b31f0f98e1b8217cc362cf69
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md 48f4600b6e512ba4d28ff3924e10e5dfabb8c245
Reviewed-Content-Hash: design/registry/entities.yaml abcf6d7a8f6bfcc9dc8023aee906f2deb233559a
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/welt-und-insel.md da2005a1567aca421bf623578a3cdab17d3e41f5
Reviewed-Content-Hash: design/gdd/simulationskern.md 9c82d078d414a4ec566b8d2f9ca3182a416b594b
Reviewed-Content-Hash: design/gdd/zustimmungsmodell.md 99cf8fc6ad97b7233bc2940a8ddfb09065b3cf3d

## Approval — 2026-10-26 — Status: Approved
Approved by the developer after 2 review rounds (roadmap cap reached); all blocking findings of the last round were fixed. Open Questions in the GDD stay open and are not part of the approval.
