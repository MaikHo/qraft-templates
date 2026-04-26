# AGENTS.md

## Zweck
Dieses Projekt nutzt ein OpenCode-basiertes Agentensystem.

Diese Datei dient als Einstiegspunkt fuer AI-Agenten und andere Tools.

## Start

- Verwende `@openagent` als Standard-Einstieg
- Folge dem Workflow:
  Context -> Plan -> Approval -> Implement -> Validate -> Review -> Document

## Wichtige Regeln

- Keine Aenderungen ohne Approval
- Kein automatisches Commit
- Review ist verpflichtend bei Codeaenderungen
- Dokumentation nur bei Relevanz oder mit Skip-Begruendung

## Architektur

- Die vollstaendige Agentenlogik liegt unter `.opencode/`
- Kontext, Standards und Workflows sind dort definiert

## Dokumentation

- Siehe:
  - docs/opencode-workflows.md (praktische Nutzung)
  - `.opencode/context/...` (Regeln & Standards)

## Hinweis

Diese Datei enthaelt bewusst nur Minimalregeln.
Detailverhalten wird ueber Context und Workflows gesteuert.
