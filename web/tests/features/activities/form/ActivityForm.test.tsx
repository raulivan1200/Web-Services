import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { describe, expect, it, vi } from "vitest";
import ActivityForm from "../../../../src/features/activities/form/ActivityForm";

const activity: Activity = {
  id: "activity-1",
  title: "Original title",
  date: "2026-09-12T00:00:00",
  description: "Original description",
  category: "music",
  isCancelled: false,
  city: "Seattle",
  venue: "The Hall",
  latitude: "47.6062",
  longitude: "-122.3321",
};

describe("ActivityForm", () => {
  it("preloads existing values and the date input in YYYY-MM-DD format", () => {
    render(<ActivityForm activity={activity} closeForm={vi.fn()} submitForm={vi.fn()} />);

    expect(screen.getByRole("heading", { name: "Edit activity" })).toBeInTheDocument();
    expect(screen.getByRole("textbox", { name: "Title" })).toHaveValue("Original title");
    expect(screen.getByRole("textbox", { name: "Description" })).toHaveValue("Original description");
    expect(document.querySelector<HTMLInputElement>('input[name="date"]')).toHaveValue("2026-09-12");
  });

  it("closes with Cancel without submitting the form", async () => {
    const user = userEvent.setup();
    const closeForm = vi.fn();
    const submitForm = vi.fn();
    render(<ActivityForm closeForm={closeForm} submitForm={submitForm} />);

    await user.click(screen.getByRole("button", { name: "Cancel" }));

    expect(closeForm).toHaveBeenCalledOnce();
    expect(submitForm).not.toHaveBeenCalled();
  });
});
