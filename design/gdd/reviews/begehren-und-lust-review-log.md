# Review Log — Begehren & Lust

## Review — 2026-10-26 — Verdict: NEEDS REVISION
Review mode: lean · Workflow: standard
Scope signal: L
Specialists: none (lean)
Blocking items: 6 | Recommended: 12
Summary: The core holds: b_ij as a pure pair function, the consent contract (Beginne only, IDs only), the Fest frequencies (111 s / 200 s / 17 min, ratio about 7x) and the aging arithmetic are correct, and no registry value conflicts. What blocks is the turn structure: when a Handlung or a Fest ends, who counts as a Teilnehmer, how refused requests are retried, how groups are sized, and the Zufriedenheit rhythm are missing or conflict with the Kern. Pillar 3 is one-directional until calibration (deliberate).
Prior verdict resolved: First review
Findings:
- [BLOCKING] Rule 14 / Rule 20: no step ends a Handlung at its Dauer or marks it vollendet; voluntary end (Zustimmung Rule 15) undefined; same-step boundary with the Sperrenprüfung undefined
- [BLOCKING] Rule 12 / Rule 20: "Teilnehmer" undefined and the end conditions "unter 2 Teilnehmer" and "Stamm untergegangen" have no place in the step order; no AC
- [BLOCKING] Rule 5 / Formulas 2, 5: argmax on fixed b_ij plus the Prüfsumme rule leaves an initiator stuck on a refusing first choice; hashed "Werte" and checksum lifetime undefined; no group fallback
- [BLOCKING] Formula 5 / AC-10: group size can exceed available candidates; AC-10 second clause contradicts "mindestens 2 weitere"; candidate pool, pair tie-break and position vs. Teilnehmer gate undefined
- [BLOCKING] Formula 8: Z_t update per Geschichtsjahr (5 steps) is not a multiple of P_person (Kern Formel 5) and has no place in Rule 20; counting rules undefined
- [BLOCKING] Rule 16 vs. Edge Cases: Rule 16 lets a mourner form a new Bindung, the Edge Case forbids it; timer reset by Gruppe and expiry step undefined
- [RECOMMENDED] Rule 8: unlimited Segen extensions allow an endless half-aging Fest without cost
- [RECOMMENDED] Rule 13 / States: who ages at half speed is defined three ways (position, Teilnehmer, "In Handlung" exclusive); "im Kampf" missing; no AC for held/fest overlap or mid-Fest save
- [RECOMMENDED] Rule 6: participants' positions during a Handlung unspecified
- [RECOMMENDED] Rule 10: late joiners roll Formula 4 or join automatically?
- [RECOMMENDED] Formula 5 Output Range counts initiatives, not Handlungen; groups mostly refused outside a Lust-Sekte
- [RECOMMENDED] Tuning Knobs: literals 0,3 + 0,6·G, 0,5·G, radii 8 and 2, 5 Lebensjahre not data-driven; T_Fest and +150 not in the registry; T_trauer not cited
- [RECOMMENDED] Formula 1 / AC-01, AC-02: no golden reference values; stream construction should cite Kern Formel 6
- [RECOMMENDED] Rule 8 / Rule 20: Segen order runs in the Eingriffsphase; not documented
- [RECOMMENDED] Rule 7: name the flags "hungert in diesem Jahr" and "im Krieg"
- [RECOMMENDED] Dependencies: Stämme & Diplomatie does not list this system or the order "Groll mindern" (bidirectionality)
- [RECOMMENDED] Acceptance Criteria: missing ACs for end conditions, two Feste, Stamm untergegangen, Segen in Krieg/Hunger or without Verfügbare, thrown person, Rules 15, 17, 18
- [RECOMMENDED] Performance: no note on caching b_ij as an N×N table
Reviewed-Content-Hash: design/gdd/begehren-und-lust.md 7866930129d2198f0f8077cb0be78a1d328b84ec
Reviewed-Content-Hash: design/registry/entities.yaml 26939dbe7263d00cf8772c2132688ddb6cba235e
Reviewed-Content-Hash: .claude/skills/design-review/SKILL.md a63d8fba9b7142eeb43791f061e2e1533c429b43
Reviewed-Content-Hash: design/gdd/zustimmungsmodell.md 698aec4333722e787afc796dc2f6e776e1c61605
Reviewed-Content-Hash: design/gdd/kultur-und-erinnerung.md ccb557d67df3afbf643f1a6fc9e2580e109741d0
Reviewed-Content-Hash: design/gdd/menschen-und-bevoelkerung.md 47920010ccfe528f61227fdb14e08e667aab2e98
Reviewed-Content-Hash: design/gdd/simulationskern.md d448d70aacb6f33d7266fad85ade41a14bb97c7d
Reviewed-Content-Hash: design/gdd/welt-und-insel.md 24111591c0f179ea1c8f480a109662f8a63fb930
Reviewed-Content-Hash: design/gdd/staemme-und-diplomatie.md b23baab0a5730d0bc6e61e998dd1dbc2da11cbb5
Reviewed-Content-Hash: design/gdd/game-concept.md 168cdc5ff8f56635a08ebbb674fe517b93ded4c6
