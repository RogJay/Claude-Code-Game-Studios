# Review Log — Kultur & Erinnerung

## Review — 2026-10-26 — Verdict: NEEDS REVISION
Review mode: full · Workflow: standard
Scope signal: L
Specialists: systems-designer, game-designer, qa-lead, creative-director
Blocking items: 6 | Recommended: 20
Summary: The prototype-1 port is correct line by line; only the Formula 2 range was wrong. Blocking were the taboo jump on a form change (against the consent model's obligation), missing test seams, the frozen culture with a lingering taboo, hand-action spam evicting old memories, and missing second-order attribution (Pillar 2). The developer decided D1–D6 the same day; all fixes applied.
Prior verdict resolved: First review
Findings:
- [BLOCKING] Rule 15: a single intervention could create a taboo within a year via a form change [systems-designer]
- [BLOCKING] Acceptance Criteria: no test seams (pure functions, culture-year entry, clock, accessors, intervention stub); AC-06, AC-12, AC-16 untestable [qa-lead]
- [BLOCKING] Rule 1 / AC-11: frozen culture with a calendar-based lingering taboo is not bit-identical [systems-designer, qa-lead]
- [BLOCKING] Rules 6–7: hand actions flood the 8-memory store and push at full strength [game-designer]
- [BLOCKING] Rule 4 / Formula 4: second-order effects (war, hunger caused by the player) not attributable [game-designer]
- [BLOCKING] AC-04 / AC-12 / AC-16 ambiguities and missing ACs for rules and formulas [qa-lead]
- [RECOMMENDED] Formula 2 range ±0.033, memory stacking up to 0.23/yr [systems-designer, qa-lead]
- [RECOMMENDED] Creation order, exact V_t read point, h_i derived in Menschen [systems-designer]
- [RECOMMENDED] Stämme contract wording on memory; Eingriff-erfassen input validation; assign vs add [systems-designer, qa-lead]
- [RECOMMENDED] Ledger keeps text and year of forgotten memories; "weil du du"; "Generationen" [game-designer, systems-designer]
- [RECOMMENDED] Norm() invalid flag instead of a value floor [game-designer, creative-director]
- [RECOMMENDED] Chronicle notices for taboo changes; UI word thresholds [game-designer]
- [RECOMMENDED] Calibration items to Open Questions: devotion without restoring force, one Fest-Segen = +0.93, attribution fades after ~50 years, Gottesstaat has no upside, memory stacking cap [systems-designer, game-designer, creative-director]
Developer decisions (2026-10-26): D1 taboos grow via the slow norms (against the creative director's recommendation of "restrictions may be instant"); D2 β per act type with α = 0.5, ritual contact counts as place Fest; D3 taboo ends after ≥ 5 years without candidacy and n ≥ 0.3; D4 veranlasser shares in the ledger; D5 merge small interventions and evict the weakest memory; D6 Lust interface now with weight 0.
Disagreement: creative-director rejected the game-designer's norm floor of 0.02 (changes values) in favour of an explicit invalid flag; applied.
Note: the stamps below are the content AS REVIEWED (before the revisions), so the next review reads them as CHANGED.
Reviewed-Content-Hash: design/gdd/kultur-und-erinnerung.md 2da228efa5bfdc4e46683dc91e4fad686d6f2319
Reviewed-Content-Hash: design/registry/entities.yaml abcf6d7a8f6bfcc9dc8023aee906f2deb233559a
Reviewed-Content-Hash: design/gdd/zustimmungsmodell.md 5054048ad22f24f7fe5bc7adfe9d1de2b194e22c
Reviewed-Content-Hash: design/gdd/staemme-und-diplomatie.md f32b6d71d02ba2847f81b283bcd532ec345cb49c
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md cca0ec321a0b55d2e9789a9c92a6295b1e277c80
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/simulationskern.md adae01d545f0f1c4d3777b0ce561a8c810167d81
Reviewed-Content-Hash: design/gdd/welt-und-insel.md 27fa1ec3101eba77c0f7c6326ecb48e08bd86134
Reviewed-Content-Hash: design/gdd/game-concept.md 168cdc5ff8f56635a08ebbb674fe517b93ded4c6
Reviewed-Content-Hash: prototypes/weltengott-concept/prototype.html e3c8aa99b445fc83ff5a9ce4fde8919dd9ab26eb
Reviewed-Content-Hash: prototypes/weltengott-concept/REPORT.md e9204c19a61fefb55207be0197cd90bd5576f6e0

## Review — 2026-10-26 — Verdict: NEEDS REVISION
Review mode: lean · Workflow: standard
Scope signal: L
Specialists: none (lean)
Blocking items: 1 | Recommended: 2
Summary: All round-1 blockers and the developer's decisions D1–D6 are in. One new blocker: the text source of a veranlasser was undefined (an intervention may never have touched this tribe) and Welt stored an event ID where the ledger needs an intervention ID. Fixed after the review: Welt stores the intervention ID of a burn, the Chronik resolves texts by intervention ID, a notice hysteresis and the merge rule for forgotten memories were added.
Prior verdict resolved: Yes
Findings:
- [BLOCKING] Rule 12 / Welt Rule 16: veranlasser text source and ID type (event vs intervention)
- [RECOMMENDED] UI Requirements: "Norm kippt" notice may flicker near word thresholds
- [RECOMMENDED] Rule 8: merge target already forgotten or evicted
Note: kultur-und-erinnerung.md and welt-und-insel.md are stamped AS REVIEWED (before the fixes).
Reviewed-Content-Hash: design/gdd/kultur-und-erinnerung.md 0ecc5e8a660212ca544ca0ae6a85b7aae2527f9d
Reviewed-Content-Hash: design/gdd/welt-und-insel.md 27fa1ec3101eba77c0f7c6326ecb48e08bd86134
Reviewed-Content-Hash: design/registry/entities.yaml b9dc1c1acfbeaed6174835e89e5352234767400c
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/zustimmungsmodell.md dbeb7828bf487169193834b305c13617d5676e92
Reviewed-Content-Hash: design/gdd/staemme-und-diplomatie.md 707c5078bfe38515783f2b6dd1b308fa7e419641
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md 030b628584e299a4c9ddb84eeefee08614329816
Reviewed-Content-Hash: design/gdd/simulationskern.md adae01d545f0f1c4d3777b0ce561a8c810167d81

## Approval — 2026-10-26 — Status: Approved
Approved by the developer after 2 review rounds (roadmap cap reached); the blocking finding of round 2 was fixed. Open Questions in the GDD stay open and are not part of the approval.
