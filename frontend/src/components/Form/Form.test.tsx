import React, { ButtonHTMLAttributes } from 'react';
import { render, unmountComponentAtNode } from "react-dom";
import { act } from "react-dom/test-utils";
import axios  from "axios";

import Form from './Form';
import { Guid } from 'guid-typescript';

jest.mock('axios');
const mockAxios = axios as jest.Mocked<typeof axios>;

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
        render(<Form />, container);
    });
});

it('has a heading and button', () => {
    act(() => {
        render(<Form />, container);
    })
    expect(container.querySelector("h2.new-event-header").textContent).toBe("Create a New Event");
    expect(container.querySelector("button.submit").textContent).toBe("Create an Event");
});

it('the form is submitted successfully', async () => {
  const fakeFormData = Promise.resolve({
    id: Guid.raw(),
    title: {value: "Fake Event"},
    description: {value: "A really cool fake event"},
    eventLocation: {value: "Your Mum's House"},
    eventStartDate: {value: "2020-01-01"},
    eventEndDate: {value: "2020-02-02"},
    registrationOpenDate: {value: "2019-12-01"},
    registrationCloseDate: {value: "2019-12-24"}
  });

  await act(async () => {
    mockAxios.post.mockResolvedValue(fakeFormData);

    render(<Form />, container);

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

    render(<Form />, container);

    const button = document.querySelector("button.submit");
    if(button != null) {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    }
  });

  expect(container.querySelector(".alert-error").textContent).toBe("Oops, something went wrong");

})