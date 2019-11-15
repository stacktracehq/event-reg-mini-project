import React from 'react';
import { render, unmountComponentAtNode } from "react-dom";
import { act } from "react-dom/test-utils";
import axios  from "axios";
import { NewEvent } from './NewEvent';
import { Guid } from 'guid-typescript';
import { EventSaveRequest, Event } from '../../models/models';
import { createMemoryHistory, createLocation } from 'history';
import { match, MemoryRouter } from "react-router-dom";

jest.mock('axios');
const mockAxios = axios as jest.Mocked<typeof axios>;

const history = createMemoryHistory();

const path = `/route/:id`;
const m: match<{ id: string }> = {
    isExact: false,
    path,
    url: path.replace(':id', '1'),
    params: { id: "1" }
};

const location = createLocation(m.url);

const buildComponent = () => {
  return (
    <MemoryRouter>
      <NewEvent history={history} match={m} location={location} />
    </MemoryRouter>
  )
}

let container: any = null;
beforeEach(() => {
  // setup a DOM element as a render target
  container = document.createElement("div");
  document.body.appendChild(container);
});

afterEach(() => {
  // cleanup on exiting
  unmountComponentAtNode(container);
  container.remove();
  container = null;
});

it('renders without crashing', () => {
    act(() => {
        const props = {};
        render(buildComponent(), container);
    });
});

it('has a heading and button', () => {
    act(() => {
        render(buildComponent(), container);
    })
    expect(container.querySelector("h2.new-event-header").textContent).toBe("Create a New Event");
    expect(container.querySelector("button.submit").textContent).toBe("Create an Event");
});

it('the form is submitted successfully', async () => {
  const fakeFormData: Promise<EventSaveRequest> = Promise.resolve({
    event: {
      id: Guid.raw(),
      title: {value: "Fake Event"},
      description: {value: "A really cool fake event"},
      eventLocation: {value: "Your Mum's House"},
      eventStartDate: {value: "2020-01-01"},
      eventEndDate: {value: "2020-02-02"},
      registrationOpenDate: {value: "2019-12-01"},
      registrationCloseDate: {value: "2019-12-24"}
    } as Event | null,
    submitSuccess: false,
    errors: false
  });

  await act(async () => {
    mockAxios.post.mockResolvedValue(fakeFormData);

    render(buildComponent(), container);

    const button = document.querySelector("button.submit");
    if(button != null) {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    }
  });

  expect(container.querySelector(".alert").textContent).toBe("The form was successfully submitted!");
})


it('the form is submitted unsuccessfully', async () => {
  await act(async () => {
    mockAxios.post.mockRejectedValue("");

    render(buildComponent(), container);

    const button = document.querySelector("button.submit");
    if(button != null) {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    }
  });

  expect(container.querySelector(".alert-error").textContent).toBe("Oops, something went wrong");

})