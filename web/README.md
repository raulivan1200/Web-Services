# EventsHub — Web

The React frontend for EventsHub. Independent app in the same repo as the
.NET backend (`../src`) — separate build, separate package manager, only
connected to it via HTTP at runtime. See
[`../docs/Architecture.md`](../docs/Architecture.md) for how the two fit
together.

## Stack

Vite 8 + React 19 + TypeScript, MUI 9 (`@mui/material`, `@mui/icons-material`,
`@fontsource/roboto`) for UI, `axios` for HTTP. ESLint (flat config) with
`typescript-eslint`, `eslint-plugin-react-hooks`, `eslint-plugin-react-refresh`.
The React Compiler is enabled via `@rolldown/plugin-babel` in `vite.config.ts`.
`vite-plugin-mkcert` issues a local dev cert so the dev server can run on
`https://localhost:3000` (the origin the backend's CORS policy allows).

## Running it

```powershell
cd web
npm install
npm run dev       # https://localhost:3000
npm run build     # tsc -b && vite build
npm run lint      # eslint .
npm run preview   # preview a production build
```

The dev server expects the backend running at `https://localhost:5001`
(`dotnet run --project ../src/EventsHub.Api`) — there's no proxy config, the
frontend calls that absolute URL directly.

## Current state / known rough edges

This is an early-stage app — worth knowing before extending it:

- **`src/app/layout/App.tsx`** fetches `https://localhost:5001/api/v1/events` with the
  URL hardcoded inline (`axios.get<Activity[]>(...)`). There's no `.env` /
  `import.meta.env` base-URL config yet — if you add more API calls or need
  this to work against a non-`localhost:5001` backend, that's the first gap
  to close.
- Only the list endpoint is used for the initial load. Creating, editing,
  and deleting activities update React state only; these changes are lost
  on reload. The API also offers
  `GET /api/v1/Events/{id}`, `POST`, `PUT` and `DELETE` under `/api/v1/Events`
  (handled by MediatR handlers in `EventsHub.Application`), but the UI does
  not call them yet. The generated typed client in `EventsHub.OpenApi` is
  not used by this app either.
- **`src/lib/types/index.d.ts`** declares shared types as *ambient* `type`
  declarations (e.g. `Activity`, which mirrors the backend's `Event`
  entity/DTO — the frontend hasn't been renamed to match yet).
  `Activity` types `latitude`/`longitude` as `string`, matching the backend
  `Event`. Being a
  `.d.ts` ambient declaration file, these types are globally available with
  no `import` statement; add new shared types here the same way rather than
  introducing per-file local type definitions for shapes coming from the API.
- No routing, no state management library, and no test setup yet — `App.tsx`
  manages the list and selection state; the dashboard renders activity
  cards, the selected activity's details, and a creation/editing form.

## Project layout

- `src/app/layout/` — `App.tsx`, navigation bar, and shared styles
- `src/features/activities/` — dashboard, list, cards, and details
- `src/features/activities/form/` — activity creation/editing form
- `src/lib/types/` — ambient shared TypeScript types
- `public/images/categoryImages/` — category images served as-is
