import { fireEvent, render, screen, within } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import axios from "axios";
import { StrictMode } from "react";
import { beforeEach, describe, expect, it, vi } from "vitest";
import App from "../../src/app/layout/App";

vi.mock("axios", () => ({
  default: {
    get: vi.fn(),
    isCancel: vi.fn((error: unknown) =>
      typeof error === "object" && error !== null && "code" in error && error.code === "ERR_CANCELED",
    ),
  },
}));

const seededActivity: Activity = {
  id: "seed-1",
  title: "Seed activity",
  date: "2026-09-12T00:00:00",
  description: "Seed description",
  category: "music",
  isCancelled: false,
  city: "Seattle",
  venue: "The Hall",
  latitude: "47.6062",
  longitude: "-122.3321",
};

function deferred<T>() {
  let resolve!: (value: T) => void;
  let reject!: (reason?: unknown) => void;
  const promise = new Promise<T>((resolvePromise, rejectPromise) => {
    resolve = resolvePromise;
    reject = rejectPromise;
  });
  return { promise, resolve, reject };
}

async function createActivity(user: ReturnType<typeof userEvent.setup>, title: string) {
  await user.click(screen.getByRole("button", { name: "Create activity" }));
  fireEvent.change(document.querySelector<HTMLInputElement>('input[name="title"]')!, {
    target: { value: title },
  });
  fireEvent.change(document.querySelector<HTMLTextAreaElement>('textarea[name="description"]')!, {
    target: { value: `${title} description` },
  });
  fireEvent.change(document.querySelector<HTMLInputElement>('input[name="category"]')!, {
    target: { value: "music" },
  });
  fireEvent.change(document.querySelector<HTMLInputElement>('input[name="date"]')!, {
    target: { value: "2026-11-02" },
  });
  fireEvent.change(document.querySelector<HTMLInputElement>('input[name="city"]')!, {
    target: { value: "Seattle" },
  });
  fireEvent.change(document.querySelector<HTMLInputElement>('input[name="venue"]')!, {
    target: { value: "The Hall" },
  });
  await user.click(screen.getByRole("button", { name: "Submit" }));
}

describe("App activity flows", () => {
  beforeEach(() => {
    vi.clearAllMocks();
    vi.mocked(axios.get).mockResolvedValue({ data: [seededActivity] });
  });

  it("loads the list and keeps create disabled until the GET settles", async () => {
    const request = deferred<{ data: Activity[] }>();
    vi.mocked(axios.get).mockReturnValue(request.promise);

    render(<App />);

    expect(screen.getByRole("progressbar", { name: "Loading activities" })).toBeInTheDocument();
    expect(screen.getByRole("button", { name: "Create activity" })).toBeDisabled();

    request.resolve({ data: [seededActivity] });

    expect(await screen.findByText("Seed activity")).toBeInTheDocument();
    expect(screen.getByRole("button", { name: "Create activity" })).toBeEnabled();
  });

  it("aborts the initial request when unmounted", () => {
    vi.mocked(axios.get).mockReturnValue(new Promise(() => {}));

    const { unmount } = render(<App />);
    const signal = vi.mocked(axios.get).mock.calls[0][1]?.signal;

    unmount();

    expect(signal?.aborted).toBe(true);
  });

  it("aborts StrictMode's discarded request and enables create after the active request", async () => {
    const discardedRequest = deferred<{ data: Activity[] }>();
    const activeRequest = deferred<{ data: Activity[] }>();
    let requestCount = 0;
    vi.mocked(axios.get).mockImplementation(() => [discardedRequest, activeRequest][requestCount++].promise as never);

    render(<StrictMode><App /></StrictMode>);

    expect(axios.get).toHaveBeenCalledTimes(2);
    expect(vi.mocked(axios.get).mock.calls[0][1]?.signal?.aborted).toBe(true);
    expect(screen.getByRole("button", { name: "Create activity" })).toBeDisabled();

    activeRequest.resolve({ data: [seededActivity] });

    expect(await screen.findByText("Seed activity")).toBeInTheDocument();
    expect(screen.getByRole("button", { name: "Create activity" })).toBeEnabled();
  });

  it("shows load failures and still allows session-only creation", async () => {
    const user = userEvent.setup();
    vi.mocked(axios.get).mockRejectedValue(new Error("Network unavailable"));
    vi.spyOn(crypto, "randomUUID").mockReturnValue("00000000-0000-4000-8000-000000000010");

    render(<App />);

    expect(await screen.findByRole("alert")).toHaveTextContent("Activities could not be loaded");
    await createActivity(user, "Offline activity");

    expect(screen.getAllByText("Offline activity")).toHaveLength(2);
  });

  it("creates, edits, and deletes an activity in local state", async () => {
    const user = userEvent.setup();
    vi.spyOn(crypto, "randomUUID").mockReturnValue("00000000-0000-4000-8000-000000000011");
    render(<App />);
    await screen.findByText("Seed activity");

    await createActivity(user, "Created activity");
    expect(screen.getAllByText("Created activity")).toHaveLength(2);

    await user.click(screen.getAllByRole("button", { name: "View" })[1]);
    await user.click(screen.getByRole("button", { name: "Edit" }));
    const title = screen.getByRole("textbox", { name: "Title" });
    expect(title).toHaveValue("Created activity");
    await user.clear(title);
    await user.type(title, "Edited activity");
    await user.click(screen.getByRole("button", { name: "Submit" }));
    expect(screen.getAllByText("Edited activity")).toHaveLength(2);

    await user.click(within(screen.getAllByText("Edited activity")[0].closest(".MuiCard-root")!).getByRole("button", { name: "Delete" }));
    expect(screen.queryAllByText("Edited activity")).toHaveLength(0);
    expect(screen.queryAllByText("Created activity description")).toHaveLength(0);
  });

  it("clears edit values when switching to a new activity form", async () => {
    const user = userEvent.setup();
    render(<App />);
    await screen.findByText("Seed activity");

    await user.click(screen.getByRole("button", { name: "View" }));
    await user.click(screen.getByRole("button", { name: "Edit" }));
    expect(screen.getByRole("textbox", { name: "Title" })).toHaveValue("Seed activity");

    await user.click(screen.getByRole("button", { name: "Create activity" }));

    expect(screen.getByRole("textbox", { name: "Title" })).toHaveValue("");
    expect(screen.getByRole("textbox", { name: "Description" })).toHaveValue("");
  });

  it("uses a fresh UUID after deleting an activity", async () => {
    const user = userEvent.setup();
    const ids = [
      "00000000-0000-4000-8000-000000000001",
      "00000000-0000-4000-8000-000000000002",
      "00000000-0000-4000-8000-000000000003",
    ];
    vi.spyOn(crypto, "randomUUID").mockImplementation(() => ids.shift()! as ReturnType<typeof crypto.randomUUID>);
    render(<App />);
    await screen.findByText("Seed activity");

    await createActivity(user, "First local");
    await createActivity(user, "Second local");
    await user.click(within(screen.getByText("First local").closest(".MuiCard-root")!).getByRole("button", { name: "Delete" }));
    await createActivity(user, "Third local");

    const thirdCard = screen.getAllByText("Third local")[0].closest<HTMLElement>(".MuiCard-root")!;
    await user.click(within(thirdCard).getByRole("button", { name: "View" }));
    expect(screen.getAllByText("Third local description")).toHaveLength(2);
    expect(crypto.randomUUID).toHaveBeenCalledTimes(3);
  });
});
