# Graph Report - Web-Services  (2026-10-24)

## Corpus Check
- 104 files · ~105,631 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 10 file(s) not represented in the graph (top: (none) 7, .config 1, .nswag 1)

## Summary
- 914 nodes · 1402 edges · 63 communities (50 shown, 13 thin omitted)
- Extraction: 88% EXTRACTED · 12% INFERRED · 0% AMBIGUOUS · INFERRED: 169 edges (avg confidence: 0.89)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `4937e025`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- 20260828131904_InitialCreate.Designer.cs
- ADDED Requirements
- EventsHub Architecture
- package.json
- Part 3: Architecture Variants
- Software Testing Fundamentals
- Clean Architecture Fundamentals
- EventsHub.Domain.csproj
- Event
- Git in Practice Guide
- compilerOptions
- devDependencies
- compilerOptions
- Task
- https
- ActivityDashboard.tsx
- EventsHub.OpenApi
- ActivityForm.test.tsx
- EventsHub.Application.Core.Mapping
- App.tsx
- Loading Strategies (Eager/Explicit/Lazy)
- tsconfig.json
- Revisión técnica del segundo parcial
- Part 5: Practice
- 14. Event Sourcing — Advanced
- 9. Validation and error handling — Intermediate
- Part 6 — Practice and Pitfalls
- Proposal
- dependencies
- 21. CQRS in Web APIs — Intermediate
- AppDbContext
- scripts
- AGENTS.md
- tsconfig.tests.json
- 20. Testing CQRS systems — Intermediate
- MapperTests
- TypeMap
- Requirements
- openspec-explore/SKILL.md
- explore.md
- IRequest
- Task
- Part 2 — Layers in a .NET Solution
- Profile
- 11. Separate read and write stores — Advanced
- 23. Evolving toward CQRS — Advanced
- Part 3 — Key Patterns
- 18. Logging, Caching, and Authentication / Authorization — Intermediate
- CqrsFundamentals.md
- Part 1 — Foundations
- 26. Common Mistakes: Anemic Domain, Leaky Abstractions, Over-Engineering — Intermediate
- 13. Domain events and integration events — Intermediate
- 5. When to use it and when not to — Intermediate

## God Nodes (most connected - your core abstractions)
1. `Event` - 43 edges
2. `TypeMap` - 25 edges
3. `MapperTests` - 19 edges
4. `PersonView` - 18 edges
5. `EventsControllerTests` - 18 edges
6. `compilerOptions` - 18 edges
7. `Mapper` - 17 edges
8. `Profile` - 17 edges
9. `Person` - 17 edges
10. `AppDbContext` - 15 edges

## Surprising Connections (you probably didn't know these)
- `Steps` --references--> `Event`  [INFERRED]
  docs/fundamentals/CleanArchitectureFundamentals.md → src/EventsHub.Domain/Event.cs
- `5. New-instance creation` --references--> `Event`  [INFERRED]
  openspec/changes/archive/2026-09-29-replace-automapper-with-custom-mapper/design.md → src/EventsHub.Domain/Event.cs
- `Context` --references--> `Event`  [INFERRED]
  openspec/changes/archive/2026-09-29-replace-automapper-with-custom-mapper/design.md → src/EventsHub.Domain/Event.cs
- `Scenario: Edit copies fields onto a tracked entity` --references--> `Event`  [INFERRED]
  openspec/changes/archive/2026-09-29-replace-automapper-with-custom-mapper/specs/object-mapping/spec.md → src/EventsHub.Domain/Event.cs
- `Scenario: Startup with the existing profile` --references--> `Event`  [INFERRED]
  openspec/changes/archive/2026-09-29-replace-automapper-with-custom-mapper/specs/object-mapping/spec.md → src/EventsHub.Domain/Event.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **OpenAPI document + NSwag typed client regeneration pipeline** — src_eventshub_openapi_readme_application_part_loading, src_eventshub_openapi_readme_openapi_document, src_eventshub_openapi_readme_nswag_codegen, src_eventshub_openapi_readme_eventshubrpcclient, claude_openapi_client_regeneration [EXTRACTED 1.00]
- **OpenAPI codegen pipeline: doc host -> JSON doc -> NSwag config -> typed client** — docs_guides_openapisetup_eventshub_openapi_host, docs_guides_openapisetup_openapi_document, docs_guides_openapisetup_nswag_config, docs_guides_openapisetup_typed_rpc_client, docs_guides_openapisetup_nswag_local_tool [EXTRACTED 1.00]
- **EF Core tracking and save flow** — docs_fundamentals_dbcontextfundamentals_dbcontext, docs_fundamentals_dbcontextfundamentals_change_tracker, docs_fundamentals_dbcontextfundamentals_entity_states, docs_fundamentals_dbcontextfundamentals_savechanges [EXTRACTED 1.00]
- **Testing pyramid layers** — docs_fundamentals_softwaretestingfundamentals_testing_pyramid, docs_fundamentals_softwaretestingfundamentals_unit_testing, docs_fundamentals_softwaretestingfundamentals_integration_testing, docs_fundamentals_softwaretestingfundamentals_e2e_testing [EXTRACTED 1.00]
- **Event data shape shared across DB, API client, frontend types** — src_eventshub_domain_readme_event_entity, src_eventshub_persistence_readme_initialcreate_migration, src_eventshub_openapi_readme_eventshubrpcclient, web_readme_ambient_types [INFERRED 0.85]
- **Bruno Events CRUD integration suite against EventsController** — tests_eventshub_integrationtests_events_events___list___200, tests_eventshub_integrationtests_events_events___get___200, tests_eventshub_integrationtests_events_events___get___404, tests_eventshub_integrationtests_events_events___create___200, tests_eventshub_integrationtests_events_events___edit___204, tests_eventshub_integrationtests_events_events___delete___200, tests_eventshub_integrationtests_environments_local_baseurl [INFERRED 0.95]

## Communities (63 total, 13 thin omitted)

### Community 0 - "20260828131904_InitialCreate.Designer.cs"
Cohesion: 0.16
Nodes (3): EventsHub.Persistence.Migrations, InitialCreate, AppDbContextModelSnapshot

### Community 1 - "ADDED Requirements"
Cohesion: 0.07
Nodes (26): ADDED Requirements, Purpose, Requirement: Convention-based member matching, Requirement: Edit behavior is preserved, Requirement: Map into a new destination, Requirement: Map onto an existing destination, Requirement: Nested and collection members, Requirement: Per-member overrides (+18 more)

### Community 2 - "EventsHub Architecture"
Cohesion: 0.05
Nodes (52): graphify skill trigger (/graphify), architecture-docs skill, CORS locked to localhost:3000, EventsHub Project (CLAUDE.md), graphify knowledge graph workflow, OpenAPI typed RPC client regeneration pipeline, docs/ reference write-ups (BackendBuildSteps, DbContextFundamentals, OpenApiSetup, ...), EventsHub Architecture (+44 more)

### Community 3 - "package.json"
Cohesion: 0.09
Nodes (26): @babel/core, babel-plugin-react-compiler, @emotion/react, @emotion/styled, eslint, @eslint/js, eslint-plugin-react-hooks, eslint-plugin-react-refresh (+18 more)

### Community 4 - "Part 3: Architecture Variants"
Cohesion: 0.25
Nodes (8): 10. Single database, separated models — Intermediate, 12. Eventual consistency — Advanced, Design principles, Handling it in the user experience, Part 3: Architecture Variants, Variations, What it looks like to a user, Why start here

### Community 5 - "Software Testing Fundamentals"
Cohesion: 0.07
Nodes (30): Testing: in-process unit tests on shared SQLite + Bruno HTTP integration tests, DbContext Fundamentals (EF Core Deep Dive), AddDbContextPool, Change Tracker (snapshot-based), Compiled Queries (EF.CompileQuery), DbContext, DbContext Scoped Lifetime, DbSet<T> (+22 more)

### Community 6 - "Clean Architecture Fundamentals"
Cohesion: 0.25
Nodes (7): 21. Unit Testing the Domain and Application Layers — General, 22. Mocking Boundaries — Intermediate, 23. Integration Testing with WebApplicationFactory and a Test Database — Advanced, 24. Architecture Tests — Advanced, Clean Architecture Fundamentals, Glossary, Part 5 — Testing

### Community 7 - "EventsHub.Domain.csproj"
Cohesion: 0.06
Nodes (33): MediatR (14.2.0), Microsoft.AspNetCore.Mvc.NewtonsoftJson (10.0.11), Microsoft.AspNetCore.OpenApi (10.0.11), Microsoft.EntityFrameworkCore.Design (10.0.11), Microsoft.EntityFrameworkCore.InMemory (10.0.11), Microsoft.EntityFrameworkCore.Sqlite (10.0.11), Moq (4.20.72), Newtonsoft.Json (13.0.4) (+25 more)

### Community 8 - "Event"
Cohesion: 0.15
Nodes (16): Evidencia del trabajo que ya estaba en la rama, Handler, GetEventList, Handler, Query, Event, Category, City (+8 more)

### Community 9 - "Git in Practice Guide"
Cohesion: 0.07
Nodes (29): End-to-End Testing, Ice Cream Cone Anti-Pattern, Integration Testing, Test Doubles (Dummy/Stub/Fake/Mock/Spy), Testing Pyramid (Mike Cohn), Unit Testing, Git in Practice Guide, Branch Naming Convention (feature/fix/hotfix/chore) (+21 more)

### Community 10 - "compilerOptions"
Cohesion: 0.10
Nodes (19): compilerOptions, allowArbitraryExtensions, allowImportingTsExtensions, erasableSyntaxOnly, jsx, lib, module, moduleDetection (+11 more)

### Community 11 - "devDependencies"
Cohesion: 0.09
Nodes (23): devDependencies, @babel/core, babel-plugin-react-compiler, eslint, @eslint/js, eslint-plugin-react-hooks, eslint-plugin-react-refresh, globals (+15 more)

### Community 12 - "compilerOptions"
Cohesion: 0.12
Nodes (16): compilerOptions, allowImportingTsExtensions, erasableSyntaxOnly, lib, module, moduleDetection, noEmit, noFallthroughCasesInSwitch (+8 more)

### Community 14 - "https"
Cohesion: 0.20
Nodes (9): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, profiles, https (+1 more)

### Community 15 - "ActivityDashboard.tsx"
Cohesion: 0.27
Nodes (8): @mui/material, ActivityCard(), Props, Props, ActivityList(), Props, ActivityDetail(), Props

### Community 16 - "EventsHub.OpenApi"
Cohesion: 0.22
Nodes (8): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, environmentVariables, launchBrowser, profiles, EventsHub.OpenApi, $schema

### Community 17 - "ActivityForm.test.tsx"
Cohesion: 0.22
Nodes (7): @testing-library/jest-dom, @testing-library/react, @testing-library/user-event, vitest, ActivityForm(), Props, activity

### Community 18 - "EventsHub.Application.Core.Mapping"
Cohesion: 0.05
Nodes (22): EventsHub.Domain, EventsHub.Application.Core.Mapping, EventsHub.Application.Events.Queries, EventsHub.Api.Controllers, EventsHub.Application.Events.Commands, EventsHub.UnitTests, EventsHub.Persistence, EventsHub.Application.UnitTests.Mapping (+14 more)

### Community 19 - "App.tsx"
Cohesion: 0.18
Nodes (10): axios, @fontsource/roboto, @mui/icons-material, react, react-dom, App(), NavBar(), Props (+2 more)

### Community 20 - "Loading Strategies (Eager/Explicit/Lazy)"
Cohesion: 0.67
Nodes (3): AsSplitQuery / Cartesian Explosion, Loading Strategies (Eager/Explicit/Lazy), N+1 Query Problem

### Community 22 - "Revisión técnica del segundo parcial"
Cohesion: 0.18
Nodes (9): Base exacta de comparación, Configuración resuelta y límites del alcance, Correcciones realizadas en esta revisión, Desglose de la nota, Entrega indicada en el anuncio, Explicación técnica para defender el avance, Revisión técnica del segundo parcial, Validación ejecutada (+1 more)

### Community 23 - "Part 5: Practice"
Cohesion: 0.20
Nodes (10): 19. Implementing the dispatching pipeline — Intermediate, 22. Common pitfalls and anti-patterns — Intermediate, A minimal dispatcher, A quick self-check, Appendix: Glossary, Building the pipeline, Libraries versus hand-written, Part 5: Practice (+2 more)

### Community 24 - "14. Event Sourcing — Advanced"
Cohesion: 0.08
Nodes (24): 14. Event Sourcing — Advanced, 15. Projections and read model rebuilding — Advanced, 16. Outbox pattern and reliable messaging — Advanced, 17. Sagas and process managers — Advanced, 18. Idempotency and concurrency — Advanced, Benefits, Compensation, Concurrency (+16 more)

### Community 25 - "9. Validation and error handling — Intermediate"
Cohesion: 0.11
Nodes (18): 6. The write side — Intermediate, 7. The read side — Intermediate, 8. Handlers and dispatching — Intermediate, 9. Validation and error handling — Intermediate, Design guidelines, Design guidelines, Guidelines, Part 2: Core Building Blocks (+10 more)

### Community 26 - "Part 6 — Practice and Pitfalls"
Cohesion: 0.20
Nodes (10): 25. Folder and Project Structure: Layer-Based vs. Feature-Based — Intermediate, 27. When *Not* to Use Clean Architecture — General, 28. Migrating an Existing Codebase Incrementally — Advanced, 29. Guided Exercise: Build "Create Event" Through Every Layer — General, Feature-based (vertical slices), Layer-based (by technical role), Part 6 — Practice and Pitfalls, Review questions (+2 more)

### Community 27 - "Proposal"
Cohesion: 0.29
Nodes (6): Capabilities, Impact, Modified Capabilities, New Capabilities, Proposal, Why

### Community 28 - "dependencies"
Cohesion: 0.22
Nodes (9): dependencies, axios, @emotion/react, @emotion/styled, @fontsource/roboto, @mui/icons-material, @mui/material, react (+1 more)

### Community 29 - "21. CQRS in Web APIs — Intermediate"
Cohesion: 0.33
Nodes (6): 21. CQRS in Web APIs — Intermediate, Asynchronous commands, Choosing resource styles, Guidelines, Status codes, Verbs and messages

### Community 30 - "AppDbContext"
Cohesion: 0.22
Nodes (6): What Changes, Handler, Handler, Handler, AppDbContext, Events

### Community 31 - "scripts"
Cohesion: 0.33
Nodes (6): scripts, build, dev, lint, preview, test

### Community 33 - "tsconfig.tests.json"
Cohesion: 0.33
Nodes (5): ./tsconfig.app.json, compilerOptions, types, extends, include

### Community 34 - "20. Testing CQRS systems — Intermediate"
Cohesion: 0.40
Nodes (5): 20. Testing CQRS systems — Intermediate, Given / When / Then for aggregates, Guidelines, Testing projections, What to test at each level

### Community 35 - "MapperTests"
Cohesion: 0.08
Nodes (33): Mapper, ChildDest, Name, ChildSource, Name, Immutable, Name, ParentDest (+25 more)

### Community 36 - "TypeMap"
Cohesion: 0.07
Nodes (17): EventsHub.Api, WeatherForecast, Date, Summary, TemperatureC, TemperatureF, IMemberOptions, MemberOptions (+9 more)

### Community 37 - "Requirements"
Cohesion: 0.07
Nodes (26): object-mapping Specification, Purpose, Requirement: Convention-based member matching, Requirement: Edit behavior is preserved, Requirement: Map into a new destination, Requirement: Map onto an existing destination, Requirement: Nested and collection members, Requirement: Per-member overrides (+18 more)

### Community 38 - "openspec-explore/SKILL.md"
Cohesion: 0.17
Nodes (11): Check for context, Ending Discovery, Guardrails, Handling Different Entry Points, OpenSpec Awareness, Planning a Change, The Stance, What You Don't Have To Do (+3 more)

### Community 39 - "explore.md"
Cohesion: 0.18
Nodes (10): Check for context, Ending Discovery, Guardrails, OpenSpec Awareness, Planning a Change, The Stance, What You Don't Have To Do, What You Might Do (+2 more)

### Community 40 - "IRequest"
Cohesion: 0.29
Nodes (6): Command, Event, Command, Id, Command, Event

### Community 41 - "Task"
Cohesion: 0.20
Nodes (3): EventsController, Query, Id

### Community 42 - "Part 2 — Layers in a .NET Solution"
Cohesion: 0.25
Nodes (8): 10. Project References and Enforcing the Dependency Direction — Intermediate, 13. CQRS: Commands vs. Queries — Intermediate, 6. The Domain Layer — Intermediate, 7. The Application Layer — Intermediate, 8. The Infrastructure / Persistence Layer — Intermediate, 9. The Presentation / API Layer — General, Part 2 — Layers in a .NET Solution, CreateEvent

### Community 43 - "Profile"
Cohesion: 0.09
Nodes (28): 1. Namespace and layout, 2. Compiled delegates per map, 3. Members from the source are matched case-sensitively, 4. Nested and collection members, 5. New-instance creation, 6. Errors and lifetime, 7. Startup wiring, 8. Tests (+20 more)

### Community 44 - "11. Separate read and write stores — Advanced"
Cohesion: 0.50
Nodes (4): 11. Separate read and write stores — Advanced, Synchronization strategies, The real cost, Why do it

### Community 45 - "23. Evolving toward CQRS — Advanced"
Cohesion: 0.50
Nodes (4): 23. Evolving toward CQRS — Advanced, A staged approach, Decision checkpoints, Migration techniques

### Community 46 - "Part 3 — Key Patterns"
Cohesion: 0.29
Nodes (7): 11. Dependency Injection and the Composition Root — Intermediate, 12. Repository and Unit of Work — Advanced, 14. MediatR and Pipeline Behaviors — Advanced, 15. The Result Pattern vs. Exceptions — Intermediate, 16. Mapping and DTO Boundaries — Intermediate, 17. Validation with FluentValidation — Intermediate, Part 3 — Key Patterns

### Community 47 - "18. Logging, Caching, and Authentication / Authorization — Intermediate"
Cohesion: 0.29
Nodes (7): 18. Logging, Caching, and Authentication / Authorization — Intermediate, 19. API Contracts and OpenAPI — Intermediate, 20. Configuration and the Options Pattern — General, Authentication and authorization, Caching, Logging, Part 4 — Cross-Cutting Concerns

### Community 48 - "CqrsFundamentals.md"
Cohesion: 0.18
Nodes (10): 1. What is CQRS? — General, 2. CQS vs. CQRS — General, 3. The problem CQRS solves — General, 4. Commands, Queries, and Events — General, Commands, CQRS Fundamentals: A Tool-Agnostic Reference Guide, Events, Part 1: Foundations (+2 more)

### Community 50 - "Part 1 — Foundations"
Cohesion: 0.33
Nodes (6): 1. What Clean Architecture Is and the Problem It Solves — General, 2. Origins and Related Styles: Hexagonal, Onion, and Clean — General, 3. The Dependency Rule — General, 4. The Concentric-Circles Diagram — General, 5. SOLID Principles as the Basis — Intermediate, Part 1 — Foundations

### Community 51 - "26. Common Mistakes: Anemic Domain, Leaky Abstractions, Over-Engineering — Intermediate"
Cohesion: 0.40
Nodes (5): 26. Common Mistakes: Anemic Domain, Leaky Abstractions, Over-Engineering — Intermediate, Leaky abstractions, Other frequent problems, Over-engineering, The anemic domain model

### Community 52 - "13. Domain events and integration events — Intermediate"
Cohesion: 0.40
Nodes (5): 13. Domain events and integration events — Intermediate, Domain events, Guidelines, Integration events, Translating between them

### Community 53 - "5. When to use it and when not to — Intermediate"
Cohesion: 0.40
Nodes (5): 5. When to use it and when not to — Intermediate, A useful rule of thumb, Signals that CQRS may pay off, Signals that it is probably unnecessary, The costs

## Ambiguous Edges - Review These
- `Bruno: Events - Create - 200 (POST /events)` → `Bruno: Events - Get - 404 (GET /events/non-existing-eventId)`  [AMBIGUOUS]
  tests/EventsHub.IntegrationTests/Events/Events - Create - 200.yml · relation: semantically_similar_to

## Knowledge Gaps
- **136 isolated node(s):** `Mediator`, `net10.0`, `Microsoft.AspNetCore.OpenApi (10.0.11)`, `Microsoft.EntityFrameworkCore.Design (10.0.11)`, `Microsoft.NET.Sdk.Web` (+131 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 471 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **13 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Event` connect `Event` to `20260828131904_InitialCreate.Designer.cs`, `ADDED Requirements`, `MapperTests`, `Requirements`, `IRequest`, `Task`, `Profile`, `Task`, `EventsHub.Application.Core.Mapping`, `Revisión técnica del segundo parcial`, `Part 6 — Practice and Pitfalls`, `Proposal`, `AppDbContext`?**
  _High betweenness centrality (0.114) - this node is a cross-community bridge._
- **Are the 14 inferred relationships involving `Event` (e.g. with `Steps` and `Evidencia del trabajo que ya estaba en la rama`) actually correct?**
  _`Event` has 14 INFERRED edges - model-reasoned connections that need verification._
- **What connects `Mediator`, `net10.0`, `Microsoft.AspNetCore.OpenApi (10.0.11)` to the rest of the system?**
  _136 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `ADDED Requirements` be split into smaller, more focused modules?**
  _Cohesion score 0.07407407407407407 - nodes in this community are weakly interconnected._
- **Why does `Steps` connect `Part 6 — Practice and Pitfalls` to `Event`?**
  _High betweenness centrality (0.028) - this node is a cross-community bridge._
- **Should `EventsHub Architecture` be split into smaller, more focused modules?**
  _Cohesion score 0.05009920634920635 - nodes in this community are weakly interconnected._

### Low-confidence Hints
_AMBIGUOUS edges — the extractor was unsure. Verify before acting on these._

- **What is the exact relationship between `Bruno: Events - Create - 200 (POST /events)` and `Bruno: Events - Get - 404 (GET /events/non-existing-eventId)`?**
  _Edge tagged AMBIGUOUS (relation: semantically_similar_to) - confidence is low._