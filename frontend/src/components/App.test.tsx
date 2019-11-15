import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import axios from "axios";
import { act } from 'react-dom/test-utils';

jest.mock('axios');
const mockAxios = axios as jest.Mocked<typeof axios>;

it('renders without crashing', () => {
  mockAxios.request.mockResolvedValue([]);
  const div = document.createElement('div');
  act(() => {
    ReactDOM.render(<App />, div);
  });
  ReactDOM.unmountComponentAtNode(div);
});